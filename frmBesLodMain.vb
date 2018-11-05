Imports System.IO
'Following only required while I am testing XML stuff in here
'Imports System.Text
'Imports System.Xml

Public Class frmBesLodMain
    Friend mFileName As String  'The name of the file of documents we are loading
    Friend WithEvents mDocBatch As Batch   'The document batch we are loading

    Private Sub frmBesLodMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.cmdTest.Visible = False
        Me.cmdLoadXML.Enabled = False
        'NB Settings on tool-strip entries for TestSQL and TestXML

        Me.lstLoadProgress.Items.Add("Select the file containing the batch of documents to load")
        Me.lstLoadProgress.Items.Add("using 'File - Open File' and click 'Load XML'")
        Me.lstLoadProgress.Items.Add("")

        Me.prgLoadProgress.Minimum = 0      'Set the start of the Progress bar to zero
    End Sub

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
            Me.cmdLoadXML.Enabled = True
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
        'Dim word As String

        'Data used for testing
        Const APST As String = "'"
        Dim myString = "Richard of York gave battle in vain!" + vbCrLf + "Peter Piper picked a peck of pickled pepper."
        myString = myString & ", , , and here is some??? more text after some empty words! "
        myString = myString & "The dreaded O" & APST & "Brien and O" & APST & "Toole should be handled! "
        myString = myString & "Of course McTavish, MacDonald will be handled, but Mac Donald and Mc Donald will be split. "
        myString = myString & " ... ...... filename.txt and ...filename.txt... "

        'myString = "one two three four five FIVE FOUR THREE TWO ONE"


        'For Each word In dict.ParseString(myString)
        'Console.WriteLine("----------word--------> " & word & " --WordId--> " & dict.GetWordId(word).ToString)

        'Next
        '
    End Sub

    Private Sub cmdLoadXML_Click(sender As Object, e As EventArgs) Handles cmdLoadXML.Click
        'Load and process an xml file containing several documents

        Dim batchFileName As String = "C:\Users\user\Documents\Bessie_20180824\BesTestLoad_03.xml"
        'Use the proper file
        batchFileName = mFileName
        Dim fname As String = System.IO.Path.GetFileName(mFileName)

        Me.statMsg.Text = "Processing: " & fname

        'Dim docBatch As New Batch(batchFileName)
        mDocBatch = New Batch()
        Call mDocBatch.Load(batchFileName)

        'Then clean up ready for the next file
        Me.prgLoadProgress.Maximum = 0
        Me.statMsg.Text = " "
        Me.cmdLoadXML.Enabled = False
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

    Private Sub FrmBesLodMain_DocLoadStarted(numDocs) Handles mDocBatch.DocLoadStarted
        'Handle the message that the current batch file has been opened,
        'and processing of the documents is starting.
        'Produces status information.
        Dim fname As String = System.IO.Path.GetFileName(mFileName)

        'Status messages
        Me.lstLoadProgress.Items.Add("---- Loading DocumentBatch file: " & fname)
        Me.lstLoadProgress.Items.Add("    ---- contains: " & numDocs.ToString & " documents")

        Me.prgLoadProgress.Maximum = numDocs    'Set maximum value of progress bar 
    End Sub

    Private Sub FrmBesLodMain_ProcDocStarted(docNum) Handles mDocBatch.ProcDocStarted
        'Handle the message that Document number docNum has started processing.
        'Produces status information
        Me.lstLoadProgress.Items.Add("        ---- Processing document: " & docNum.ToString & " of " & Me.prgLoadProgress.Maximum & " -------")    'Status message ****
        Application.DoEvents()      '*** This is supposed to be bad form!
    End Sub

    Private Sub FrmBesLodMain_DocBatchDuplicate(fname) Handles mDocBatch.DocBatchDuplicate
        'Handle the message that a duplicate Document Batch has been found
        'Report it. Decision making is done in the DocBatch object.
        Me.lstLoadProgress.Items.Add("        ---- Duplicate Document Batch: " & fname)
    End Sub

    Private Sub FrmBesLodMain_DocBatchDupCancelled(fname) Handles mDocBatch.DocBatchDupCancelled
        'Handle the message that the user has cancelled loading the duplicate Document Batch
        Me.lstLoadProgress.Items.Add("        ---- Loading Duplicate Document Batch: " & fname & " cancelled by user.")
    End Sub

    Private Sub FrmBesLodMain_ProcDocFinished(docNum) Handles mDocBatch.ProcDocFinished
        'Handle the message that Document number docNum has finished processing.
        'Updates the progress bar
        Me.prgLoadProgress.PerformStep()
        Application.DoEvents()      '*** This is supposed to be bad form!
    End Sub

    Private Sub FrmBesLodMain_AllDocsProcessed() Handles mDocBatch.AllDocsProcessed
        'Handle the message that all documents in the current batch have been processed,
        'successfully or not.
        'Produces status information.
        Me.lstLoadProgress.Items.Add("---- All: " & mDocBatch.NumDocs() & " documents processed ---- ")    'Status message ****
        Me.prgLoadProgress.PerformStep() 'Clean-up
    End Sub

End Class
