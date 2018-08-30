Imports System          'is this needed?
Imports System.IO
'Following only required while I am testing XML stuff in here
Imports System.Text
Imports System.Xml
'Following only required while I am testing SQL stuff here
Imports Microsoft.SqlServer
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class BesLodSQL
    'Object bringing together all the function Bessie Load uses to access the SQL database.
    Public Sub New()

    End Sub
    'Object bringing together all the function Bessie Load uses to access the SQL database.

    Public Sub augTable_insert()
        Const QUOT As String = "'"                              'SQL is expecting literals enclosed in single quotes - I predict confusion!
        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder
        'Dim mParams As New BesParam 'Will declaring the parameters object locally fix my problem?

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        Console.WriteLine(conString.ConnectionString)

        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim dateString As String = QUOT & Now.ToString & QUOT   'Using a string purely to get an updating string

        Dim queryString As String = "INSERT INTO dbo.TestAugTable (AugId, DateTimeString) VALUES(9999," & dateString & " )"
        'Dim queryString As String = "INSERT INTO dbo.TestAugTable (AugId) VALUES(9999)"

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            MsgBox("Number rows affected = " & sqlCommand.ExecuteNonQuery().ToString)

        Catch ex As SqlException
            Dim i As Integer = 0
            For i = 0 To ex.Errors.Count - 1
                Console.WriteLine("Index#: " & i.ToString & vbNewLine & "Error: " & ex.Errors(i).ToString & vbNewLine)
            Next
            MsgBox("SQL Exception trapped - Look at the console")
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
            Dim i As Integer = 0
            For i = 0 To ex.Errors.Count - 1
                Console.WriteLine("Index#: " & i.ToString & vbNewLine & "Error: " & ex.Errors(i).ToString & vbNewLine)
            Next
            MsgBox("SQL Exception trapped - Look at the console")

        Catch ex As Exception

            Console.WriteLine("Error: " & ex.Message.ToString & " is not a valid column" & vbNewLine)
            Console.WriteLine(ex.ToString & vbNewLine)

            MsgBox("Non-SQL exception - Look at the console")
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
            Dim i As Integer = 0
            For i = 0 To ex.Errors.Count - 1
                Console.WriteLine("Index#: " & i.ToString & vbNewLine & "Error: " & ex.Errors(i).ToString & vbNewLine)
            Next
            MsgBox("SQL Exception trapped - Look at the console")
        End Try

    End Sub

End Class
