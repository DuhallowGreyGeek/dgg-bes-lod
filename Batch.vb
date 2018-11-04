Imports System.Xml
Imports System.IO

Public Class Batch
    Public Event DocLoadStarted(numDocs As Integer) 'File loaded, document processing starting
    Public Event ProcDocStarted(docNum As Integer)  'Processing of Document num has started
    Public Event ProcDocFinished(docNum As Integer) 'Processing of Document num has finished
    Public Event AllDocsProcessed()                 'All documents in this batch have been processed, successfully or not

    Private mNumDocs As Integer = 0                 'Number of documents in this batch
    Private mCurDocNum As Integer = 0               'Number of document currently being processed

    Public Sub New()
        '
    End Sub

    Public Sub Load(docBatchFname As String)
        'Process the xml file

        'Check if we have loaded a DocBatch with this filename before
        Dim fname As String = System.IO.Path.GetFileName(docBatchFname)

        Dim prgList As Object = frmMain.lstLoadProgress.Items

        If mlodSQL.DocBatch_IsThereExisting(fname) = False Then 'Process the batch
            Call ProcessBatch(docBatchFname)

        Else
            'Will eventually prompt giving the possibility of overwriting it but not yet
            prgList.Add("---- Loading DocumentBatch file: " & fname & " FAILED!")
            Call MsgBox("Duplicate Document (or other serious error)")
        End If

    End Sub

    Private Sub ProcessBatch(docBatchFname As String)
        'Actually process the Xml file containing the batch of documents
        Dim fname As String = System.IO.Path.GetFileName(docBatchFname)

        Dim xDocBatch As New XmlDocument
        xDocBatch.Load(docBatchFname) 'Load the document
        Dim xBatHeader As New BatHeader(xDocBatch)

        Call mlodSQL.DocBatch_Insert(xBatHeader)    'Insert the DocBatch row
        Dim docBatchId As Integer = mlodSQL.DocBatch_IDofRecord(fname)

        If My.Settings.DocsToConsole Then
            Console.WriteLine("---- Current row .DocBatchId = " & docBatchId.ToString)
        End If

        'Process each document in the DocBatch
        Dim xDocList As New DocList(xDocBatch)

        mNumDocs = xDocList.DocList.Count
        If My.Settings.DocsToConsole Then
            Console.WriteLine("number of documents ---> " & xDocList.DocList.Count)
        End If

        RaiseEvent DocLoadStarted(xDocList.DocList.Count)

        Dim iDocCount As Integer = 0
        For Each doc As Doc In xDocList.DocList
            iDocCount = iDocCount + 1
            If My.Settings.DocsToConsole Then
                Console.WriteLine(" --Document # --> " & iDocCount.ToString)
            End If

            RaiseEvent ProcDocStarted(iDocCount)

            'Call doc.Dump() 'Dump contents to the console
            'Console.WriteLine("---- Invoke the SQL Insert -----")
            Dim DocId As Integer = mlodSQL.Doc_Insert(docBatchId, doc)
            If My.Settings.DocsToConsole Then
                Console.WriteLine("--- Document.DocId = " & DocId.ToString)

                Console.WriteLine("  ------Now the parts---")
            End If

            '----Now write the parts 
            Dim jPartNum As Integer = 0
            For Each curPart As Part In doc.Parts
                'Set the Identifiers in Part
                jPartNum = jPartNum + 1
                curPart.DocumentId = DocId
                curPart.PartNum = jPartNum
                Call mlodSQL.Part_Insert(curPart)       'Insert into the database

                'Call curPart.Dump() 'Dump contents to console

                'Add the words referred to in the fields of the current part to the dictionary
                'and then persist the Usage information

                'Process the Part.Subject field
                Dim fieldIdent As Integer = 1 'arbitrarilly calling "Subject" = 1
                Dim wordSeqNum As Integer = 0
                For Each word In dict.ParseString(curPart.Subject)
                    wordSeqNum = wordSeqNum + 1

                    Dim wordid = dict.GetWordId(word)       'Gets the WordId and maybe adds the word to the dictionary

                    If My.Settings.WordsToConsole Then
                        Console.WriteLine(" word seq -- " & wordSeqNum.ToString & " --> " & word & " id => " & wordid)
                    End If

                    Dim usage As New Usage
                    Call usage.Add(curPart, fieldIdent, wordSeqNum, wordid)

                Next
                'Dim junk As New Usage
                'Console.WriteLine(junk.FieldContentsString(curPart, fieldIdent))

                'Process the Part.Synopsis field
                fieldIdent = 2 'arbitrarilly calling "Synopsis" = 2
                wordSeqNum = 0
                For Each word In dict.ParseString(curPart.Synopsis)
                    wordSeqNum = wordSeqNum + 1

                    Dim wordid = dict.GetWordId(word)       'Gets the WordId and maybe adds the word to the dictionary

                    If My.Settings.WordsToConsole Then
                        Console.WriteLine(" word seq -- " & wordSeqNum.ToString & " --> " & word & " id => " & wordid)
                    End If

                    Dim usage As New Usage
                    Call usage.Add(curPart, fieldIdent, wordSeqNum, wordid)
                Next
                'Dim junk2 As New Usage
                'Console.WriteLine(junk2.FieldContentsString(curPart, fieldIdent))

            Next
            'Console.WriteLine()
            RaiseEvent ProcDocFinished(iDocCount)

        Next
        Console.WriteLine()
        Console.WriteLine("--------- All Documents Processed ----------")
        'prgList.Add("---- All: " & xDocList.DocList.Count & " xxxx documents processed ---- ")    'Status message ****
        RaiseEvent AllDocsProcessed()

    End Sub

    Public ReadOnly Property NumDocs As Integer
        Get
            Return mNumDocs
        End Get
    End Property
    Public ReadOnly Property CurrentDocNum As Integer
        Get
            Return CurrentDocNum
        End Get
    End Property
    Public Sub Dump()
        'Dump Contents to Console
        Console.WriteLine(" ************* The Batch Itself **************** ")
        Console.WriteLine("           Nothing to Dump Directly             ")
        Console.WriteLine(" ************* Now all the Documents **************** ")
        Console.WriteLine("           Nothing to Dump Directly             ")
    End Sub

End Class
