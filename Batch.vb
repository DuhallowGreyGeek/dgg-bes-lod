Imports System.Xml
Imports System.IO

Public Class Batch
    Private mNumDocs As Integer = 0                 'Number of documents in this batch
    Private mCurDocNum As Integer = 0               'Number of document currently being processed

    Public Sub New(docBatchFname As String)
        'Process the xml file


        'Check if we have loaded a DocBatch with this filename before
        Dim fname As String = System.IO.Path.GetFileName(docBatchFname)
        'Console.WriteLine("-----> " & fname)
        Dim prgList As Object = frmMain.lstLoadProgress.Items

        If mlodSQL.DocBatch_IsThereExisting(fname) = False Then 'Process the batch
            prgList.Add("---- Loading DocumentBatch file: " & fname)    'Status message ****

            Dim xDocBatch As New XmlDocument
            xDocBatch.Load(docBatchFname) 'Load the document
            Dim xBatHeader As New BatHeader(xDocBatch)

            Call mlodSQL.DocBatch_Insert(xBatHeader)    'Insert the DocBatch row
            Dim docBatchId As Integer = mlodSQL.DocBatch_IDofRecord(fname)

            Console.WriteLine("---- Current row .DocBatchId = " & docBatchId.ToString)

            'Process each document in the DocBatch
            Dim xDocList As New DocList(xDocBatch)

            mNumDocs = xDocList.DocList.Count
            Console.WriteLine("number of documents ---> " & xDocList.DocList.Count)
            prgList.Add("    ---- contains: " & xDocList.DocList.Count & " documents")    'Status message ****

            Dim iDocCount As Integer = 0
            For Each doc As Doc In xDocList.DocList
                iDocCount = iDocCount + 1
                Console.WriteLine(" --Document # --> " & iDocCount.ToString)
                prgList.Add("        ---- Processing document: " & iDocCount.ToString & " of " & xDocList.DocList.Count & " -------")    'Status message ****

                'Call doc.Dump() 'Dump contents to the console
                Console.WriteLine("---- Invoke the SQL Insert -----")
                Dim DocId As Integer = mlodSQL.Doc_Insert(docBatchId, doc)
                Console.WriteLine("--- Document.DocId = " & DocId.ToString)

                'Now write the Part to the database ---------------
                Console.WriteLine("  ------Now the parts---")

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
                        Console.WriteLine(" word seq -- " & wordSeqNum.ToString & " --> " & word & " id => " & wordid)

                        Dim usage As New Usage
                        Call usage.Add(curPart, fieldIdent, wordSeqNum, wordid)

                    Next

                    'Process the Part.Synopsis field
                    fieldIdent = 2 'arbitrarilly calling "Synopsis" = 2
                    wordSeqNum = 0
                    For Each word In dict.ParseString(curPart.Synopsis)
                        wordSeqNum = wordSeqNum + 1

                        Dim wordid = dict.GetWordId(word)       'Gets the WordId and maybe adds the word to the dictionary
                        Console.WriteLine(" word seq -- " & wordSeqNum.ToString & " --> " & word & " id => " & wordid)

                        Dim usage As New Usage
                        Call usage.Add(curPart, fieldIdent, wordSeqNum, wordid)

                    Next

                Next
                Console.WriteLine()

            Next
            Console.WriteLine()
            Console.WriteLine("--------- All Documents Processed ----------")
            prgList.Add("---- All: " & xDocList.DocList.Count & " documents processed ---- ")    'Status message ****

        Else
            'Will eventually prompt giving the possibility of overwriting it but not yet
            prgList.Add("---- Loading DocumentBatch file: " & fname & " FAILED!")
            Call MsgBox("Duplicate Document (or other serious error)")
        End If

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
