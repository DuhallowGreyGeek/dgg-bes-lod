Imports System.Text.RegularExpressions
Imports System.Data.SqlClient

Public Class DBDictionary
    'Contains the dictionary of words which have been used in the vocabulary of the documents added to the DB.
    'Returns the WordId of a word
    'Also contains the function to parse a string into words according to rules.
    Const MODNAME As String = "DBDictionary"
    Protected mRoutineName As String = ""      'To hold the name of the routine which generates an exception
    Protected mDictionary As New Dictionary(Of String, Integer)

    Public Sub New()
        'Setup for the dictionary. May choose to load from the database at start-up.
        'More likely to populate "on-demand"
    End Sub

    Public ReadOnly Property GetWordId(Word As String) As Integer
        'Return the WordId for the word

        Get
            mRoutineName = "GetWordId_Get"
            Dim WordId As Integer = 0

            If mDictionary.ContainsKey(Word) Then       '1) check if word is already in the dictionary
                Return mDictionary.Item(Word)

            Else
                If Me.SQLWordExists(Word) Then          '2) check if word is in the db (but not in dictionary)
                    WordId = Me.GetSQLWordId(Word)

                    mDictionary.Item(Word) = WordId
                Else                                    '3) if not, add it to the database

                    Call Me.DictWord_Insert(Word)
                    WordId = Me.GetSQLWordId(Word)

                    mDictionary.Item(Word) = WordId

                End If

            End If

            Return WordId

        End Get
    End Property

    Private ReadOnly Property GetSQLWordId(Word As String) As Integer
        Get
            'Return the integer WordId matching the word
            'There is a unique index on DictWord.WordText so there will only ever be zero or 1 rows
            mRoutineName = "GetSQLWordId(word As String)"
            Const ERRORKEY As Integer = -99999

            Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

            'Get Connection string data
            conString.DataSource = params.SQLDataSource
            conString.IntegratedSecurity = params.SQLIntegratedSecurity
            conString.InitialCatalog = params.SQLInitCatalogDB

            'Construct the query string
            Dim queryString As String = "Select wrd.WordId From dbo.DictWord as wrd WHERE "
            queryString = queryString & "wrd.WordText = @word "

            'Console.WriteLine(queryString)

            Dim wordId As Integer

            Try
                Using sqlConnection As New SqlConnection(conString.ConnectionString)
                    sqlConnection.Open()
                    Using sqlCommand As New SqlCommand(queryString, sqlConnection)
                        sqlCommand.Parameters.AddWithValue("@word", Word)

                        Using reader = sqlCommand.ExecuteReader()
                            If reader.HasRows Then
                                Do While reader.Read

                                    wordId = reader.Item("WordId")

                                    sqlConnection.Close()
                                    Return wordId

                                Loop
                            Else
                                sqlConnection.Close()
                                Return ERRORKEY
                            End If
                        End Using
                    End Using
                    sqlConnection.Close()
                End Using

                'Should never reach this point!
                Return ERRORKEY 'Which will stop anything bad happening!

            Catch ex As SqlException
                Call Me.handleSQLException(ex)
                Return ERRORKEY

            Catch ex As Exception
                Call Me.handleGeneralException(ex)
                Return ERRORKEY
            End Try

        End Get
    End Property

    Private ReadOnly Property SQLWordExists(Word As String) As Boolean
        Get
            'Return True if the word is already in the database
            'There is a unique index on DictWord.WordText so there will only ever be zero or 1 rows
            mRoutineName = "SQLWordExists(word As String)"
            Const ERRORKEY As Integer = -99999

            Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

            'Get Connection string data
            conString.DataSource = params.SQLDataSource
            conString.IntegratedSecurity = params.SQLIntegratedSecurity
            conString.InitialCatalog = params.SQLInitCatalogDB

            'Construct the query string
            Dim queryString As String = "Select wrd.WordId From dbo.DictWord as wrd WHERE "
            queryString = queryString & "wrd.WordText = @word "

            'Console.WriteLine(queryString)

            Try
                Using sqlConnection As New SqlConnection(conString.ConnectionString)
                    sqlConnection.Open()
                    Using sqlCommand As New SqlCommand(queryString, sqlConnection)
                        sqlCommand.Parameters.AddWithValue("@word", Word)

                        Using reader = sqlCommand.ExecuteReader()
                            If reader.HasRows Then
                                sqlConnection.Close()
                                Return True
                            Else
                                sqlConnection.Close()
                                Return False
                            End If
                        End Using
                    End Using
                    sqlConnection.Close()
                End Using

                'Should never reach this point!
                Return ERRORKEY 'Which will stop anything bad happening!

            Catch ex As SqlException
                Call Me.handleSQLException(ex)
                Return ERRORKEY

            Catch ex As Exception
                Call Me.handleGeneralException(ex)
                Return ERRORKEY
            End Try

        End Get
    End Property

    Public ReadOnly Property ParseString(myString As String) As Collection
        Get
            mRoutineName = "ParseString_Get"
            'Function which returns a collection of all the words which we've identified,
            'in the order they were encountered and including duplicates.

            'The parser is simple but probably ineffient. It uses Microsoft written code, so it is probably as good
            'as it is possible to get.

            Dim wordList As New Collection

            'Define the characters which will be used to split the string into words
            'This will subsequently come from a system wide parameter.
            'A parameter will be used so that I can adjust the characters used without recompiling the program

            'Line ends will always cause a word-split.
            Const LINEEND As String = vbCr & vbCrLf
            '
            Dim splitChars As String = "\/@;:?!, " & LINEEND  'Allow imbedded full-stops to preserve file names (maybe)


            'Do the actual split
            Dim words = myString.Split(splitChars.ToCharArray, StringSplitOptions.RemoveEmptyEntries)

            'Significant punctuation should have caused word-splits.
            'Use a _regular expression_ to remove everything which isn't A-Z or a-z or exceptions.
            'Allow full-stops to remain (to try and preserve file-names ha! ha!) 
            'and then remove leading or trailing periods
            'and finally, if SQL LIKE needs it, fold to lower case.
            'No matter how this is done it is never going to be efficient, 
            'but it is only done once when the document is loaded.

            For Each candidateWord In words
                candidateWord = Regex.Replace(candidateWord, "[^A-Za-z.']+", String.Empty)

                Dim trimChars As String = ". " 'Characters to remove from the beginning and end of the candidate

                candidateWord = candidateWord.Trim(trimChars.ToCharArray)

                If candidateWord.Length > 0 Then    'Skip any candidates which have become empty

                    'Console.WriteLine(candidateWord)
                    wordList.Add(candidateWord.ToLower)
                End If
            Next
            Return wordList
        End Get
    End Property

    Private Sub DictWord_Insert(word As String)
        'Insert the row for the containing the DictWord information 
        mRoutineName = "DictWord_Insert(word As String)"

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "INSERT INTO dbo.DictWord (WordText) VALUES( "
        queryString = queryString & "@WordText "
        queryString = queryString & " )"

        'Console.WriteLine(queryString)

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@WordText", word)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            Dim iRows As Integer = sqlCommand.ExecuteNonQuery()
            'MsgBox("Number DocBatch rows affected = " & iRows.ToString)

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

        End Try

    End Sub

    Private Sub handleSQLException(ex As SqlException)
        Console.WriteLine("*** Error *** in Module: " & MODNAME)
        Console.WriteLine("*** Exception *** in routine: " & mRoutineName)

        Dim i As Integer = 0
        For i = 0 To ex.Errors.Count - 1
            Console.WriteLine("Index#: " & i.ToString & vbNewLine & "Error: " & ex.Errors(i).ToString & vbNewLine)
        Next
        MsgBox("SQL Exception trapped - Look at the console", MsgBoxStyle.Critical, "Bessie SQL")
    End Sub

    Private Sub handleGeneralException(ex As Exception)
        Console.WriteLine("*** Error *** in Module: " & MODNAME)
        Console.WriteLine("*** Exception *** in routine: " & mRoutineName)

        Console.WriteLine("Error: " & ex.Message.ToString & " is not a valid column" & vbNewLine)
        Console.WriteLine(ex.ToString & vbNewLine)

        MsgBox("Non-SQL exception - Look at the console", MsgBoxStyle.Critical, "Bessie SQL")

    End Sub



End Class
