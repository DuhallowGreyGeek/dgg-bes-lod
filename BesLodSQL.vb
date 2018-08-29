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
    Public Sub New()
        'Dim mParams As New BesParam 'Will declaring the parameters object locally fix my problem?

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

        Dim dbConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim dateString As String = QUOT & Now.ToString & QUOT   'Using a string purely to get an updating string

        Dim queryString As String = "INSERT INTO dbo.AugTable (AugId, DateTimeString) VALUES(9999," & dateString & " )"
        'Dim queryString As String = "INSERT INTO dbo.AugTable (AugId) VALUES(9999)"

        Dim mySqlCommand = New SqlCommand(queryString, dbConnection)

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
    Public Sub augTable_select()
        'Dim mParams As New BesParam 'Will declaring the parameters object locally fix my problem?

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        Try
            Using Con As New SqlConnection(conString.ConnectionString)
                Con.Open()
                Using Com As New SqlCommand("Select * From dbo.AugTable", Con)
                    Using RDR = Com.ExecuteReader()
                        If RDR.HasRows Then
                            Do While RDR.Read

                                frmMain.lstLoadProgress.Items.Add("--- " & RDR.Item("AugId").ToString() & " --- " & RDR.Item("DatetimeString").ToString())

                            Loop
                        End If
                    End Using
                End Using
                Con.Close()
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
        Dim dbConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "EXECUTE NonExistantStoredProcedure"

        Dim mySqlCommand = New SqlCommand(queryString, dbConnection)

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
