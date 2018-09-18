Imports System.IO
'Following only required while I am testing XML stuff in here
'Imports System.Text
'Imports System.Xml

Public Class frmBesLodMain
    Friend mFileName As String 'The name of the file of documents we are loading
    Friend mDocBatch As Object

    Private Sub DumpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DumpToolStripMenuItem.Click
        params.Dump()
        MsgBox("Arghhh!")
        Console.WriteLine("Write to the console")
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        'Select the file containg the batch of documents to be loaded to the database
        Dim inputFileDlg As New OpenFileDialog
        Dim loadFileName As String
        Dim shortFileName As String

        inputFileDlg.Title = "Select the file containing the document batch"
        inputFileDlg.Filter = "Document Batch files (*.xml)|*.xml"

        If inputFileDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            loadFileName = inputFileDlg.FileName
            shortFileName = inputFileDlg.SafeFileName

            mFileName = loadFileName
            lstLoadProgress.Items.Add("File selected ---> " & shortFileName)
        End If

    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        'Close the application
        Me.Close()
    End Sub

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click
        'This is a temporary function and button for testing fragments of code.
        lstLoadProgress.Items.Add("Test Button clicked")

        '*** Temporary invoke the ParseString function
        Dim word As String

        'Data used for testing
        Const APST As String = "'"
        Dim myString = "Richard of York gave battle in vain!" + vbCrLf + "Peter Piper picked a peck of pickled pepper."
        myString = myString & ", , , and here is some??? more text after some empty words! "
        myString = myString & "The dreaded O" & APST & "Brien and O" & APST & "Toole should be handled! "
        myString = myString & "Of course McTavish, MacDonald will be handled, but Mac Donald and Mc Donald will be split. "
        myString = myString & " ... ...... filename.txt and ...filename.txt... "

        For Each word In dict.ParseString(myString)
            Console.WriteLine("----------word--------> " & word)
        Next



        '
    End Sub

    Private Sub cmdLoadXML_Click(sender As Object, e As EventArgs) Handles cmdLoadXML.Click
        'Load and process an xml file containing several documents

        Dim batchFileName As String = "C:\Users\user\Documents\Bessie_20180824\BesTestLoad_03.xml"
        Me.statStatusStrip.Text = "Processing: " & batchFileName

        Dim docBatch As New Batch(batchFileName)

        prgLoadProgress.Minimum = 0
        prgLoadProgress.Maximum = docBatch.NumDocs() + 1


        'Then clean up ready for the next file
    End Sub

    Private Sub SelectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectToolStripMenuItem.Click
        'Temporary function to test "SELECT" from the database
        Call mlodSQL.augTable_select()
    End Sub

    Private Sub InsertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertToolStripMenuItem.Click
        'Temporarilly using this to run an INSERT or an UPDATE

        Call mlodSQL.augTable_insert()
    End Sub

    Private Sub ExceptionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExceptionToolStripMenuItem.Click
        'Force SQL Exception

        Call mlodSQL.augTable_fail()
    End Sub

    Private Sub TestConnectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestConnectionToolStripMenuItem.Click
        'Function to test the connection to the - Simplest thing which could possibly work!
        If mlodSQL.SysStateVals_Select_OK() Then
            'Connection made so do whatever necessary
            Dim msg As String = "Database connection OK." & vbCrLf
            msg = msg & "Source: " & params.SQLDataSource & vbCrLf
            msg = msg & "Database: " & params.SQLInitCatalogDB

            Call MsgBox(msg, MsgBoxStyle.Information, "Bessie - DB Connection")
        Else
            'Connection failed so do whatever required
            Dim msg As String = "Database connection Failed!" & vbCrLf
            msg = msg & "Source: " & params.SQLDataSource & vbCrLf
            msg = msg & "Database: " & params.SQLInitCatalogDB

            Call MsgBox(msg, MsgBoxStyle.Critical, "Bessie - DB Connection")
        End If
    End Sub

End Class
