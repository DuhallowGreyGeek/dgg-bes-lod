'Following required for SQL functions
'Imports Microsoft.SqlServer
'Imports System.Data.Sql
Imports System.Data.SqlClient
'Imports System.Data.SqlTypes

Public Class BesLodSQL
    'Object bringing together all the function Bessie Load uses to access the SQL database.
    Const MODNAME As String = "BesLodSQL"
    Friend mRoutineName As String = ""      'To hold the name of the routine which generates an exception

    Public Sub New()

    End Sub

    Public Sub augTable_insert()
        Const QUOT As String = "'"                              'SQL is expecting literals enclosed in single quotes - I predict confusion!
        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        Console.WriteLine(conString.ConnectionString)

        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim dateString As String = QUOT & Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & QUOT   'Using a string purely to get an updating string

        Dim queryString As String = "INSERT INTO dbo.TestAugTable (AugId, DateTimeString) VALUES(9999," & dateString & " )"
        'Dim queryString As String = "INSERT INTO dbo.TestAugTable (AugId) VALUES(9999)"

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            MsgBox("Number rows affected = " & sqlCommand.ExecuteNonQuery().ToString)

        Catch ex As SqlException

            Call Me.handleSQLException(ex)

        End Try

    End Sub
    Public Sub augTable_select()
        'Dim mParams As New BesParam 'Will declaring the parameters object locally fix my problem?

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Using sqlCommand As New SqlCommand("Select * From dbo.TestAugTable", sqlConnection)
                    Using reader = sqlCommand.ExecuteReader()
                        If reader.HasRows Then
                            Do While reader.Read

                                frmMain.lstLoadProgress.Items.Add("--- " & reader.Item("AugId").ToString() & " --- " & reader.Item("DatetimeString").ToString())

                            Loop
                        End If
                    End Using
                End Using
                sqlConnection.Close()
            End Using

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

        Catch ex As Exception
            Call Me.handleGeneralException(ex)

        End Try


    End Sub
    Public Sub augTable_fail()
        'Dim mParams As New BesParam 'Will declaring the parameters object locally fix my problem? 
        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        Console.WriteLine(conString.ConnectionString)
        Dim connection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "EXECUTE NonExistantStoredProcedure"

        Dim mySqlCommand = New SqlCommand(queryString, connection)

        Try
            Dim numRows As Integer = 0

            mySqlCommand.Connection.Open()
            MsgBox("Number rows affected = " & mySqlCommand.ExecuteNonQuery().ToString)

        Catch ex As SqlException
            Call Me.handleSQLException(ex)
        End Try

    End Sub

    Public Function SysStateVals_Select_OK() As Boolean
        'Call MsgBox("SysStateVals_Select")
        'Get the values in the SystemStateValues table as a test of the DB Connection
        'Return True if OK (even if warnings) otherwise return False
        mRoutineName = "SysStateVals_Select_OK()"

        Dim result As Boolean = False 'Default to failed result

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Dim queryText As String = "SELECT * FROM dbo.SystemStateValues"

                Using sqlCommand As New SqlCommand(queryText, sqlConnection)
                    Using reader = sqlCommand.ExecuteReader()
                        Dim rowCount As Integer = 0 'There should only ever be one row in the SystemStateValues table

                        If reader.HasRows Then

                            Do While reader.Read
                                rowCount = rowCount + 1
                                If rowCount = 1 Then

                                    Console.WriteLine(" -- Test DB Connection --")
                                    Console.WriteLine("Appl Version : " & reader.Item("ApplicationVersion").ToString())
                                    Console.WriteLine("DB Version   : " & reader.Item("DatabaseVersion").ToString())
                                    Console.WriteLine()

                                Else
                                    Console.WriteLine("Multiple rows in System State Values table. Only (random) first row used.")
                                    Call MsgBox("Invalid SystemState", MsgBoxStyle.Information, "Bessie - Test Connection")
                                End If
                                result = True 'Connection made
                            Loop
                        Else
                            Console.WriteLine("Empty System State Values table!")
                            Call MsgBox("Empty SystemState", MsgBoxStyle.Information, "Bessie - Test Connection")
                            result = True 'Connection made
                        End If
                    End Using
                End Using
                sqlConnection.Close()
            End Using

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

        Catch ex As Exception
            Call Me.handleGeneralException(ex)

        End Try

        Return result

    End Function

    Public Sub DocBatch_Insert(BatHeader As BatHeader)
        'Insert the row for the containing the BatchHeader information 
        mRoutineName = "DocBatch_Insert(BatHeader As BatHeader)"
        Const QUOT As String = "'"                              'SQL is expecting literals enclosed in single quotes - I predict confusion!
        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        'The *parameters* for the row being added
        Dim tmpFileName As String = BatHeader.FileName                        '--- FileName, 
        Dim tmpDateCreated As String = QUOT & BatHeader.CreatedDate.ToString("yyyy/MM/dd") & QUOT   '--- BatchDateCreated, 
        '--- DateLoaded -- Not required 
        Dim tmpDescription As String = "Long rambling description"         '--- Description

        Dim dateString As String = QUOT & Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & QUOT   'Using a string purely to get an updating string

        Dim queryString As String = "INSERT INTO dbo.DocBatch (FileName, DateCreated, DateLoaded, Description) VALUES( "
        queryString = queryString & QUOT & tmpFileName & QUOT                  '--- FileName, 
        queryString = queryString & "," & tmpDateCreated                       '--- BatchDateCreated, 
        queryString = queryString & "," & dateString                          '--- DateLoaded, 
        queryString = queryString & "," & QUOT & tmpDescription & QUOT  '--- Description
        queryString = queryString & " )"

        'Console.WriteLine(queryString)

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            MsgBox("Number DocBatch rows affected = " & sqlCommand.ExecuteNonQuery().ToString)

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

        End Try

    End Sub

    Public Function DocBatch_IsThereExisting(FileName As String) As Boolean
        'Return True if there is an existing row and False if there isn't
        'There is a unique index on DocBatch.Filename so there will only ever be zero or 1 rows
        mRoutineName = "DocBatch_IsThereExisting(FileName As String)"
        Const QUOT As String = "'"

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        'Construct the query string
        Dim queryString As String = "Select * From dbo.DocBatch as bat WHERE "
        queryString = queryString & "bat.FileName =" & QUOT & FileName & QUOT

        'Console.WriteLine(queryString)

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Using sqlCommand As New SqlCommand(queryString, sqlConnection)
                    Using reader = sqlCommand.ExecuteReader()
                        If reader.HasRows Then
                            Return True
                        Else
                            Return False
                        End If
                    End Using
                End Using
                sqlConnection.Close()
            End Using

            'Should never reach this point!
            Return True 'Which will stop anything bad happening!

        Catch ex As SqlException
            Call Me.handleSQLException(ex)
            Return True

        Catch ex As Exception
            Call Me.handleGeneralException(ex)
            Return True
        End Try

    End Function

    Public Function DocBatch_IDofRecord(FileName As String) As Integer
        'Return the integer DocBatchId matching the FileName
        'There is a unique index on DocBatch.Filename so there will only ever be zero or 1 rows
        mRoutineName = "DocBatch_IDofRecord(FileName As String)"
        Const QUOT As String = "'"
        Const ERRORKEY As Integer = -99999

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        'Construct the query string
        Dim queryString As String = "Select bat.DocBatchId From dbo.DocBatch as bat WHERE "
        queryString = queryString & "bat.FileName =" & QUOT & FileName & QUOT

        'Console.WriteLine(queryString)

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Using sqlCommand As New SqlCommand(queryString, sqlConnection)
                    Using reader = sqlCommand.ExecuteReader()
                        If reader.HasRows Then
                            Do While reader.Read

                                Return reader.Item("DocBatchId")

                            Loop
                        Else
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

    End Function

    Public Function Doc_Insert(docBatchId As Integer, doc As Doc) As Integer
        'Insert a row into the Document table
        'Returns the DocumentID identifier of the document just added
        mRoutineName = "Doc_Insert(docBatchId As Integer, doc As Doc)"
        Const ERRORKEY As Integer = -99999

        Const QUOT As String = "'"                              'SQL is expecting literals enclosed in single quotes - I predict confusion!
        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        'The *parameters* for the Document row being added
        Dim FileName As String = doc.FileName                        '--- FileName
        'Dim DateCreated As String = QUOT & BatHeader.CreatedDate.ToString("yyyy/MM/dd") & QUOT   '--- BatchDateCreated, 


        Dim dateString As String = QUOT & Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & QUOT   'Using a string purely to get an updating string

        'Build the query command structure
        Dim queryString As String = "INSERT INTO dbo.Document ("
        queryString = queryString & "DocumentLabel, FileName, Path, Title, DateOnDoc, DocBatchId )"
        queryString = queryString & " VALUES( "
        queryString = queryString & " @DocumentLabel, @FileName, @Path, @Title, @DateOnDoc, @DocBatchId "
        queryString = queryString & " )"

        'Console.WriteLine(queryString)

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@DocumentLabel", doc.FileName)
        sqlCommand.Parameters.AddWithValue("@FileName", doc.FileName)
        sqlCommand.Parameters.AddWithValue("@Path", doc.FilePath)
        sqlCommand.Parameters.AddWithValue("@Title", doc.DocTitle)
        sqlCommand.Parameters.AddWithValue("@DateOnDoc", doc.DocDate.ToString("yyyy/MM/dd"))
        sqlCommand.Parameters.AddWithValue("@DocBatchId", docBatchId)

        'Console.WriteLine("--sqlCommand--> " & sqlCommand.CommandText)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            MsgBox("Number Doc rows inserted = " & sqlCommand.ExecuteNonQuery().ToString)

            'Now get the DocId of the Document we just added Doc_Insert
            Return mlodSQL.Doc_IDofRecord(doc.FileName)

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

            Return ERRORKEY
        End Try

        Return ERRORKEY
    End Function

    Private Function Doc_IDofRecord(DocumentLabel As String) As Integer
        'Return the DocID of the Document we have just added. Used by Doc_Insert
        'Label (and FileName) is an external identifier. It will have a unique index.
        mRoutineName = "Doc_IDofRecord(DocumentLabel As String)"
        Const ERRORKEY As Integer = -99999

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        'Construct the query string
        Dim queryString As String = "Select doc.DocumentId From dbo.Document as doc WHERE "
        queryString = queryString & "doc.DocumentLabel = @DocumentLabel"

        'Console.WriteLine("----> " & queryString)

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Using sqlCommand As New SqlCommand(queryString, sqlConnection)

                    'Substitute the parameter into the query command
                    sqlCommand.Parameters.AddWithValue("@DocumentLabel", DocumentLabel)

                    Using reader = sqlCommand.ExecuteReader()
                        If reader.HasRows Then
                            Do While reader.Read

                                Return reader.Item("DocumentId")

                            Loop
                        Else
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

    End Function

    Public Sub Part_Insert(part As Part)
        'Return the DocID of the Document we have just added. Used by Doc_Insert
        'Label (and FileName) is an external identifier. It will have a unique index.
        mRoutineName = "Part_Insert(part As Part)"

        part.Dump() 'Dump the contents to the console

        Const QUOT As String = "'"                              'SQL is expecting literals enclosed in single quotes - I predict confusion!
        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        'The *parameters* for the Document row being added
        'Dim DateCreated As String = QUOT & BatHeader.CreatedDate.ToString("yyyy/MM/dd") & QUOT   '--- BatchDateCreated, 


        Dim dateString As String = QUOT & Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & QUOT   'Using a string purely to get an updating string

        'Build the query command structure
        Dim queryString As String = "INSERT INTO dbo.Part ("
        queryString = queryString & "DocumentID, PartNum, DocDate, DocFrom, DocTo, DocSubject )"
        queryString = queryString & " VALUES( "
        queryString = queryString & " @DocumentID, @PartNum, @DocDate, @DocFrom, @DocTo, @DocSubject "
        queryString = queryString & " )"

        'Console.WriteLine(queryString)

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@DocumentID", part.DocumentId)
        sqlCommand.Parameters.AddWithValue("@PartNum", part.PartNum)
        sqlCommand.Parameters.AddWithValue("@DocDate", part.DocDate.ToString("yyyy/MM/dd"))
        sqlCommand.Parameters.AddWithValue("@DocFrom", part.DocFrom)
        sqlCommand.Parameters.AddWithValue("@DocTo", part.DocTo)
        sqlCommand.Parameters.AddWithValue("@DocSubject", part.Subject)

        'Console.WriteLine("--sqlCommand--> " & sqlCommand.CommandText)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            MsgBox("Number Part rows inserted = " & sqlCommand.ExecuteNonQuery().ToString)

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

        End Try

    End Sub

    Private Sub handleSQLException(ex As SqlException)
        Console.WriteLine("*** Error *** in Module: " & MODNAME)
        Console.Writeline("*** Exception *** in routine: " & mRoutineName)

        Dim i As Integer = 0
        For i = 0 To ex.Errors.Count - 1
            Console.WriteLine("Index#: " & i.ToString & vbNewLine & "Error: " & ex.Errors(i).ToString & vbNewLine)
        Next
        MsgBox("SQL Exception trapped - Look at the console", MsgBoxStyle.Critical, "Bessie SQL")
    End Sub

    Private Sub handleGeneralException(ex As Exception)
        Console.WriteLine("*** Error *** in Module: " & MODNAME)
        Console.Writeline("*** Exception *** in routine: " & mRoutineName)

        Console.WriteLine("Error: " & ex.Message.ToString & " is not a valid column" & vbNewLine)
        Console.WriteLine(ex.ToString & vbNewLine)

        MsgBox("Non-SQL exception - Look at the console", MsgBoxStyle.Critical, "Bessie SQL")

    End Sub

End Class
