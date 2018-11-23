Imports System.Data.SqlClient

Public Class Usage
    'Contains the function around Usage, in particular the function to insert a new Usage record
    'into the database.

    Const MODNAME As String = "Usage"
    Protected mRoutineName As String = ""      'To hold the name of the routine which generates an exception

    Public Sub New()
        'Set up the Usage object. Probably nothing to do here.
    End Sub

    Public Sub Add(part As Part, fieldIdent As Integer, wordSeqNum As Integer, wordId As Integer)
        'Persist a Usage record associating a Word with the Field which contains it in a Document Part
        'Return the UsageId
        mRoutineName = "Add(part As Part, fieldIdent As Integer, wordSeqNum As Integer, wordId As Integer)"
        
        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "INSERT INTO dbo.Usage ( "
        queryString = queryString & "DocumentId, PartNum, FieldIdent, WordSeqNum, WordId) "
        queryString = queryString & "VALUES( "
        queryString = queryString & "@DocumentId, @PartNum, @FieldIdent, @WordSeqNum, @WordId"
        queryString = queryString & " )"


        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@DocumentId", part.DocumentId)
        sqlCommand.Parameters.AddWithValue("@PartNum", part.PartNum)
        sqlCommand.Parameters.AddWithValue("@FieldIdent", fieldIdent)
        sqlCommand.Parameters.AddWithValue("@WordSeqNum", wordSeqNum)
        sqlCommand.Parameters.AddWithValue("@WordId", wordId)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            Dim iRows As Integer = sqlCommand.ExecuteNonQuery()
            'MsgBox("Number Usage rows affected = " & iRows.ToString)
            sqlConnection.Close()

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

        End Try


    End Sub

    Public ReadOnly Property FieldContentsCollection(part As Part, fieldIdent As Integer) As Collection
        Get
            'Return the words in the Field referenced (as a collection). 
            mRoutineName = "FieldContentsCollection(...)"
            Dim fieldContents As New Collection

            Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

            'Get Connection string data
            conString.DataSource = params.SQLDataSource
            conString.IntegratedSecurity = params.SQLIntegratedSecurity
            conString.InitialCatalog = params.SQLInitCatalogDB

            'Construct the query string
            Dim queryString As String = "Select wuse.WordSeqNum, wuse.WordText From dbo.WordUsage as wuse WHERE "
            queryString = queryString & "wuse.DocumentId = @DocumentId "
            queryString = queryString & "AND wuse.PartNum = @PartNum "
            queryString = queryString & "AND wuse.FieldIdent = @FieldIdent "
            queryString = queryString & "ORDER BY wuse.WordSeqNum ASC "

            Try
                Using sqlConnection As New SqlConnection(conString.ConnectionString)
                    sqlConnection.Open()
                    Using sqlCommand As New SqlCommand(queryString, sqlConnection)

                        'Substitute the parameter into the query command
                        sqlCommand.Parameters.AddWithValue("@DocumentId", part.DocumentId)
                        sqlCommand.Parameters.AddWithValue("@PartNum", part.PartNum)
                        sqlCommand.Parameters.AddWithValue("@FieldIdent", fieldIdent)

                        Using reader = sqlCommand.ExecuteReader()
                            If reader.HasRows Then
                                Do While reader.Read

                                    fieldContents.Add(reader.Item("WordText"))

                                Loop
                            Else
                                'Empty Field
                            End If
                        End Using
                    End Using
                    sqlConnection.Close()
                End Using

                Return fieldContents

            Catch ex As SqlException
                Call Me.handleSQLException(ex)
                Return fieldContents


            Catch ex As Exception
                Call Me.handleGeneralException(ex)
                Return fieldContents

            End Try
        End Get
    End Property

    Public ReadOnly Property FieldContentsString(part As Part, fieldIdent As Integer) As String
        Get
            'Return the words in the Field referenced (as a string, separated by spaces).
            mRoutineName = "FieldContentsString(...)"
            FieldContentsString = ""
            Dim word As String

            For Each word In Me.FieldContentsCollection(part, fieldIdent)
                FieldContentsString = FieldContentsString & word & " "
            Next
            Return FieldContentsString

        End Get
    End Property


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
