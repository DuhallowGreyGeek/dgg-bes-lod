Imports System.Xml
Imports System.IO
Imports System.Data.SqlClient

Public Class Batch
    Public Event DocLoadStarted(numDocs As Integer) 'File loaded, document processing starting
    Public Event DocBatchDuplicate(fname As String) 'Batch with this filename has already been added to DB
    Public Event DocBatchDupCancelled(fname As String) 'User cancelled loading of this duplicate batch
    Public Event ProcDocStarted(docNum As Integer)  'Processing of Document num has started
    Public Event ProcDocFinished(docNum As Integer) 'Processing of Document num has finished
    Public Event AllDocsProcessed()                 'All documents in this batch have been processed, successfully or not

    Const MODNAME As String = "Batch"
    Friend mRoutineName As String = ""      'To hold the name of the routine which generates an exception

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

        If IsThereExistingDocBatch(fname) = False Then 'Process the batch
            'This is the first batch with this filename. Process the batch.

            Call ProcessBatch(docBatchFname)

        Else
            'Detected that there is a DocBatch with the same file name
            'This may be a mistake. Report it and ask the user what they want to do about it.

            RaiseEvent DocBatchDuplicate(fname)
            Console.WriteLine("--- Duplicate Batch: " & fname & " detected.")

            Dim prompt As String = "A document batch with the filename: " & fname & " has already been added to the database."
            prompt = prompt & vbCrLf & "Do you want to overwrite it?"

            Dim result As MsgBoxResult
            result = MsgBox(prompt, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "Duplicate Document Batch")

            Select Case result
                Case MsgBoxResult.Ok
                    'Replace the existing records
                    Call MsgBox("Replace")
                Case MsgBoxResult.Cancel
                    'Stop! Keep changes which have already been made but exit process
                    Console.WriteLine("--- User cancelled loading Duplicate Batch: " & fname)
                    RaiseEvent DocBatchDupCancelled(fname)

                    'Call MsgBox("Halt")
                Case Else
                    'Shouldn't happen
                    Call MsgBox("Oops!")
            End Select

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

    Private Function IsThereExistingDocBatch(FileName As String) As Boolean
        'Return True if there is an existing row and False if there isn't
        'There is a unique index on DocBatch.Filename so there will only ever be zero or 1 rows
        mRoutineName = "DocBatch_IsThereExisting(FileName As String)"

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        'Construct the query string
        Dim queryString As String = "Select * From dbo.DocBatch as bat WHERE "
        queryString = queryString & "bat.FileName = @FileName "

        'Console.WriteLine(queryString)

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Using sqlCommand As New SqlCommand(queryString, sqlConnection)
                    sqlCommand.Parameters.AddWithValue("@FileName", FileName)

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
