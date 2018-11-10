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

    Private Sub FrmBesLodMain_DocBatchDupCancel(fname) Handles mDocBatch.DocBatchDupCancel
        'Handle the message that the user has cancelled loading the duplicate Document Batch
        Me.lstLoadProgress.Items.Add("        ---- Loading Duplicate Document Batch: " & fname & " cancelled by user.")
    End Sub

    Private Sub FrmBesLodMain_DocBatchDupReplace(fname) Handles mDocBatch.DocBatchDupReplace
        'Handle the message that the user has chosen to replace the duplicate Document Batch
        Me.lstLoadProgress.Items.Add("        ---- Replacing Duplicate Document Batch: " & fname)
    End Sub

    Private Sub FrmBesLodMain_ProcDocFinished(docNum) Handles mDocBatch.ProcDocFinished
        'Handle the message that Document number docNum has finished processing.
        'Updates the progress bar
        Me.prgLoadProgress.PerformStep()
        Application.DoEvents()      '*** This is supposed to be bad form!
    End Sub

    Private Sub FrmBesLodMain_DocumentDupCancel(docNum, docLabel) Handles mDocBatch.DocumentDupCancel
        'Handle the message that User has cancelled processing this batch because a duplicate Document has been encountered.
        Me.prgLoadProgress.Maximum = 0
        Me.lstLoadProgress.Items.Add("        ---- User cancelled batch @ Document: " & docNum & ": " & docLabel)
        Me.statMsg.Text = "User cancelled batch following duplicate document"
    End Sub

    Private Sub FrmBesLodMain_AllDocsProcessed() Handles mDocBatch.AllDocsProcessed
        'Handle the message that all documents in the current batch have been processed,
        'successfully or not.
        'Produces status information.
        Me.lstLoadProgress.Items.Add("---- All: " & mDocBatch.NumDocs() & " documents processed ---- ")    'Status message ****
        Me.statMsg.Text = "All: " & mDocBatch.NumDocs() & " documents processed ---- "

        Me.prgLoadProgress.PerformStep() 'Clean-up
    End Sub

    Private Sub GetDocIdToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetDocIdToolStripMenuItem.Click
        mDocBatch = New Batch

        Dim testDocLabel As String = InputBox("Enter the Document Label", "Get the DocumentId for a Label", "ALPH X_9999")

        MsgBox("GetDocumentId( " & testDocLabel & " ) ---> " & mDocBatch.GetDocId(testDocLabel).ToString)
    End Sub

    Private Sub DeleteDocToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteDocToolStripMenuItem.Click
        mDocBatch = New Batch


        Dim testDocIdString As String = InputBox("Enter the DocumentId", "Delete a Document based on DocumentId", "9999")
        Dim testDocId As Integer = -9999

        If Integer.TryParse(testDocIdString, testDocId) Then
            Dim numDocsDeleted As Integer = mDocBatch.DeleteDoc(testDocId)
            If numDocsDeleted = 1 Then
                MsgBox("Document.DocumentId: " & testDocId.ToString & " deleted!")
            Else
                MsgBox("Document.DocumentId: " & testDocId.ToString & " deletion FAILED! Look at console.")
            End If
        Else
            MsgBox("Invalid DocumentId: " & testDocIdString)
        End If

    End Sub

    Private Sub DeleteDocUsagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteDocUsagesToolStripMenuItem.Click
        mDocBatch = New Batch

        Dim testDocIdString As String = InputBox("Enter the DocumentId", "Delete all the Usage records for a Document based on DocumentId", "9999")
        Dim testDocId As Integer = -9999

        If Integer.TryParse(testDocIdString, testDocId) Then
            Dim numUsagesDeleted As Integer = mDocBatch.DeleteDocUsages(testDocId)
            If numUsagesDeleted >= 0 Then
                MsgBox("Document.DocumentId: " & testDocId.ToString & " Number Usage records deleted: " & numUsagesDeleted.ToString)
            Else
                MsgBox("Document.DocumentId: " & testDocId.ToString & " Usage deletion FAILED! Look at console.")
            End If
        Else
            MsgBox("Invalid DocumentId: " & testDocIdString)
        End If

    End Sub

    Private Sub DeleteDocSynopsesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteDocSynopsesToolStripMenuItem.Click
        mDocBatch = New Batch

        Dim testDocIdString As String = InputBox("Enter the DocumentId", "Delete all the Synopsis records for a Document based on DocumentId", "9999")
        Dim testDocId As Integer = -9999

        If Integer.TryParse(testDocIdString, testDocId) Then
            Dim numSynopsesDeleted As Integer = mDocBatch.DeleteDocSynopses(testDocId)
            If numSynopsesDeleted >= 0 Then
                MsgBox("Document.DocumentId: " & testDocId.ToString & " Number Synopsis records deleted: " & numSynopsesDeleted.ToString)
            Else
                MsgBox("Document.DocumentId: " & testDocId.ToString & " Usage deletion FAILED! Look at console.")
            End If
        Else
            MsgBox("Invalid DocumentId: " & testDocIdString)
        End If

    End Sub

    Private Sub DeleteDocPartsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteDocPartsToolStripMenuItem.Click
        mDocBatch = New Batch

        Dim testDocIdString As String = InputBox("Enter the DocumentId", "Delete all the Part records for a Document based on DocumentId", "9999")
        Dim testDocId As Integer = -9999

        If Integer.TryParse(testDocIdString, testDocId) Then
            Dim numSynopsesDeleted As Integer = mDocBatch.DeleteParts(testDocId)
            If numSynopsesDeleted >= 0 Then
                MsgBox("Document.DocumentId: " & testDocId.ToString & " Number Part records deleted: " & numSynopsesDeleted.ToString)
            Else
                MsgBox("Document.DocumentId: " & testDocId.ToString & " Part deletion FAILED! Look at console.")
            End If
        Else
            MsgBox("Invalid DocumentId: " & testDocIdString)
        End If

    End Sub

    Private Sub RemoveDocAndDependentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveDocAndDependentsToolStripMenuItem.Click
        mDocBatch = New Batch

        Dim testDocLabel As String = InputBox("Enter the Document Label", "Delete a Document and all its dependent records based on DocLabel", "Label")

        Dim numDocsDeleted As Integer = mDocBatch.RemoveDocAndDependents(testDocLabel)
        If numDocsDeleted >= 0 Then
            MsgBox("Document.DocumentId: " & testDocLabel & " Number Documents removed: " & numDocsDeleted.ToString)
        Else
            MsgBox("Document.DocumentId: " & testDocLabel & " Document removal FAILED! Look at console.")
        End If

    End Sub
End Class
