Imports System.Xml
Imports System.IO
Imports System.Data.SqlClient

Public Class Batch
    Public Event DocLoadStarted(numDocs As Integer) 'File loaded, document processing starting
    Public Event DocBatchDuplicate(fname As String) 'Batch with this filename has already been added to DB
    Public Event DocBatchDupCancel(fname As String) 'User cancelled loading of this duplicate batch
    Public Event DocBatchDupReplace(fname As String) 'User chose to replace duplicate batch
    Public Event DocumentDupCancel(docNum As Integer, DocLabel As String) 'Use cancelled loading batch following dup doc
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
            util.WriteConsMsg("--- Duplicate Batch: " & fname & " detected.")

            Dim prompt As String = "A document batch with the filename: " & fname & " has already been added to the database."
            prompt = prompt & vbCrLf & "Do you want to overwrite it?"

            Dim result As MsgBoxResult
            result = MsgBox(prompt, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "Duplicate Document Batch")

            Select Case result
                Case MsgBoxResult.Ok
                    'Replace the existing records
                    util.WriteConsMsg("--- User replacing Duplicate Batch: " & fname)
                    RaiseEvent DocBatchDupReplace(fname)

                    Call Me.RemoveBatchAndDependents(fname)
                    Call ProcessBatch(docBatchFname) 'Finally process the new batch

                Case MsgBoxResult.Cancel
                    'Stop! Keep changes which have already been made but exit process
                    util.WriteConsMsg("--- User cancelled loading Duplicate Batch: " & fname)
                    RaiseEvent DocBatchDupCancel(fname)

                Case Else
                    'Shouldn't happen
                    Console.WriteLine("--- Unxepected Response handling duplicate Document Batch: " & fname)
                    Call MsgBox("Unxepected response handling duplicate Document Batch: " & fname)
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

        util.WriteConsMsg("---- Current row .DocBatchId = " & docBatchId.ToString)

        'Process each document in the DocBatch
        Dim xDocList As New DocList(xDocBatch)

        mNumDocs = xDocList.DocList.Count
        util.WriteConsMsg("number of documents ---> " & xDocList.DocList.Count)
        RaiseEvent DocLoadStarted(xDocList.DocList.Count)

        Dim iDocCount As Integer = 0
        For Each doc As Doc In xDocList.DocList
            iDocCount = iDocCount + 1
            util.WriteConsMsg(" --Document # --> " & iDocCount.ToString)
            RaiseEvent ProcDocStarted(iDocCount)

            'Check if a Document with this DocLabel exists.
            If Me.IsThereExistingDocument(doc.DocLabel) Then        'There is an existing document - Ask the user what they want to do with it.

                'Display a dialog like MsgBox returning Skip/Replace/Halt
                Dim dlg As New dlgSkipReplace

                dlg.Text = "Duplicate Document: " & doc.DocLabel
                dlg.Message = "Document: " & doc.DocLabel & " already exists in the database. " & vbCrLf
                dlg.Message = dlg.Message & " do you want to: Replace it (use the new version), " & vbCrLf
                dlg.Message = dlg.Message & " Skip it (use the old version) or Halt adding the batch? "

                Dim result As System.Windows.Forms.DialogResult = dlg.ShowDialog

                Select Case result
                    Case Windows.Forms.DialogResult.OK
                        'Replace the existing records
                        util.WriteConsMsg("--- User Replaced Document: " & iDocCount & ": " & doc.DocLabel)

                        Call RemoveDocAndDependents(doc.DocLabel)
                        Call AddDocAndDependents(docBatchId, doc)
                        '
                        RaiseEvent ProcDocFinished(iDocCount)
                        '
                    Case Windows.Forms.DialogResult.Ignore
                        'Skip the new record and retain the existing records
                        util.WriteConsMsg("--- User skipped Document: " & iDocCount & ": " & doc.DocLabel)
                        Continue For
                        '
                    Case Windows.Forms.DialogResult.Abort
                        'Stop! Keep changes which have already been made but exit process
                        util.WriteConsMsg("--- User cancelled processing @ Document: " & iDocCount & ": " & doc.DocLabel)
                        RaiseEvent DocumentDupCancel(iDocCount, doc.DocLabel)
                        Exit For
                        '
                    Case Else
                        'Shouldn't happen
                        Console.WriteLine("--- Unxepected Response handling duplicate Document: " & doc.DocLabel)
                        Call MsgBox("Unxepected response handling duplicate Document: " & doc.DocLabel)
                End Select
            Else                                                    'There is no existing document - get on and add it.

                Call AddDocAndDependents(docBatchId, doc)
                RaiseEvent ProcDocFinished(iDocCount)
            End If
        Next
        Console.WriteLine()
        Console.WriteLine("--------- All Documents Processed ----------")
        '
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
        mRoutineName = "IsThereExistingDocBatch(FileName As String)"

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

    Private Function IsThereExistingDocument(DocLabel As String) As Boolean
        'Return True if there is an existing row and False if there isn't
        'There is a unique index on Document.DocumentLabel so there will only ever be zero or 1 rows
        mRoutineName = "IsThereExistingDocument(DocLabel As String)"

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        'Construct the query string
        Dim queryString As String = "Select * From dbo.Document as doc WHERE "
        queryString = queryString & "doc.DocumentLabel = @DocLabel "

        'Console.WriteLine(queryString)

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Using sqlCommand As New SqlCommand(queryString, sqlConnection)
                    sqlCommand.Parameters.AddWithValue("@DocLabel", DocLabel)

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

    Private Sub RemoveBatchAndDependents(fname As String)
        'Remove the existing DocBatch and the dependent Document, Part and Usage records.
        'A duplicate batch was detected. The user has decided to overwrite it.
        mRoutineName = "RemoveBatchAndDependents(fname As String)"
        util.WriteConsMsg("*** RemovingBatch---> " & fname)

        Dim DocBatchId As Integer = mlodSQL.DocBatch_IDofRecord(fname)

        For Each DocumentLabel In Me.ListDocumentsInBatch(DocBatchId)
            util.WriteConsMsg("   ----> " & DocumentLabel)

            Call RemoveDocAndDependents(DocumentLabel)
        Next

        Call Me.DeleteBatch(DocBatchId)

    End Sub

    Private Sub AddDocAndDependents(docBatchId As Integer, doc As Doc)
        'Add a Document and the associated dependent records: Part, Synopis
        mRoutineName = "AddDocAndDependents(docBatchId As Integer, doc As Doc)"

        'Console.WriteLine("---- Invoke the SQL Insert -----")
        Dim DocId As Integer = mlodSQL.Doc_Insert(docBatchId, doc)
        util.WriteConsMsg("--- Document.DocId = " & DocId.ToString)
        util.WriteConsMsg("  ------Now the parts---")

        '----Now write the parts 
        Dim jPartNum As Integer = 0
        For Each curPart As Part In doc.Parts
            'Set the Identifiers in Part
            jPartNum = jPartNum + 1
            curPart.DocumentId = DocId
            curPart.PartNum = jPartNum
            Call mlodSQL.Part_Insert(curPart)       'Insert into the database

            'Add the words referred to in the fields of the current part to the dictionary
            'and then persist the Usage information

            'Process the Part.Subject field
            Dim fieldIdent As Integer = 1 'arbitrarilly calling "Subject" = 1
            Dim wordSeqNum As Integer = 0
            For Each word In dict.ParseString(curPart.Subject)
                wordSeqNum = wordSeqNum + 1

                Dim wordid = dict.GetWordId(word)       'Gets the WordId and maybe adds the word to the dictionary

                util.WriteConsWords(" word seq -- " & wordSeqNum.ToString & " --> " & word & " id => " & wordid)

                Dim usage As New Usage
                Call usage.Add(curPart, fieldIdent, wordSeqNum, wordid)

            Next

            'Process the Part.Synopsis field
            fieldIdent = 2 'arbitrarilly calling "Synopsis" = 2
            wordSeqNum = 0
            For Each word In dict.ParseString(curPart.Synopsis)
                wordSeqNum = wordSeqNum + 1

                Dim wordid = dict.GetWordId(word)       'Gets the WordId and maybe adds the word to the dictionary

                util.WriteConsWords(" word seq -- " & wordSeqNum.ToString & " --> " & word & " id => " & wordid)

                Dim usage As New Usage
                Call usage.Add(curPart, fieldIdent, wordSeqNum, wordid)
            Next

        Next

    End Sub

    Public Function RemoveDocAndDependents(DocLabel As String) As Integer
        'Delete a Document and the dependents: Part, Synopsis and Usage from the database
        'Used as part of deleting a DocBatch or prior to replacing a duplicate document.
        mRoutineName = "RemoveDocAndDependents(DocLabel As String)"

        Dim iRows As Integer = 0 'Number of Documents which have been deleted (should be 0 or 1)

        Try
            'MsgBox("Deleting All the records associated with Document: " & DocLabel)
            'Any failures result in an exception being thrown
            Dim DocumentId As Integer = Me.GetDocId(DocLabel)
            '
            Dim numUsagesDeleted As Integer = Me.DeleteDocUsages(DocumentId)
            Dim numSynopsesDeleted As Integer = Me.DeleteDocSynopses(DocumentId)
            Dim numPartsDeleted As Integer = Me.DeleteParts(DocumentId)
            '
            iRows = Me.DeleteDoc(DocumentId)    'Finally delete the Document itself

            Return iRows

        Catch ex As SqlException
            Return iRows

        Catch ex As Exception
            Return iRows

        End Try

    End Function

    Public Function GetDocId(DocLabel As String) As Integer
        'Get the DocId corresponding to a DocLabel. 
        'Used as part of deleting a DocBatch or prior to replacing a duplicate document.
        'Normally used after we have found a duplicate, so not finding something will throw an exception.
        mRoutineName = "GetDocId(DocLabel As String)"

        Const ERRORKEY As Integer = -9999

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        'Construct the query string
        Dim queryString As String = "Select doc.DocumentId From dbo.Document as doc WHERE "
        queryString = queryString & "doc.DocumentLabel = @DocLabel "

        'Console.WriteLine(queryString)

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Using sqlCommand As New SqlCommand(queryString, sqlConnection)
                    sqlCommand.Parameters.AddWithValue("@DocLabel", DocLabel)

                    Using reader = sqlCommand.ExecuteReader()
                        If reader.HasRows Then
                            Do While reader.Read
                                Return reader.Item("DocumentId")
                            Loop
                        Else
                            Throw New Exception("Document.DocLabel: " & DocLabel & " not found!")
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

        Return ERRORKEY

    End Function

    Public Function DeleteDoc(DocumentId As Integer) As Integer
        'Delete a Document identified by its DocumentId key. Return the number of documents deleted.
        'Used as part of deleting a DocBatch or prior to replacing a duplicate document.
        'Normally used after we have found a duplicate, so not finding something will throw an exception.
        mRoutineName = "DeleteDoc(DocumentId As Integer)"

        Const ERRORROWCOUNT As Integer = -9999

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "DELETE FROM dbo.Document "
        queryString = queryString & "WHERE DocumentId = @DocumentId "

        'Console.WriteLine(queryString)

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@DocumentId", DocumentId)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            Dim iRows As Integer = sqlCommand.ExecuteNonQuery()
            '
            Return iRows
            sqlCommand.Connection.Close()

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

            Return ERRORROWCOUNT
        End Try
        Return ERRORROWCOUNT
    End Function

    Public Function DeleteDocUsages(DocumentId As Integer) As Integer
        'Delete all the Usage (Word Usage) records associated with a Document. 
        'Return the number of Usage records deleted. This could be any number from 0 (probably an empty document) upwards.
        'Used as part of deleting a DocBatch or prior to replacing a duplicate document.
        mRoutineName = "DeleteDocUsages(DocumentId As Integer)"

        Const ERRORROWCOUNT As Integer = -9999

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "DELETE FROM dbo.Usage "
        queryString = queryString & "WHERE DocumentId = @DocumentId "

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@DocumentId", DocumentId)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            Dim iRows As Integer = sqlCommand.ExecuteNonQuery()
            '
            Return iRows
            sqlCommand.Connection.Close()

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

            Return ERRORROWCOUNT
        End Try
        Return ERRORROWCOUNT
    End Function

    Public Function DeleteDocSynopses(DocumentId As Integer) As Integer
        'Delete all the DocSynopis records associated with a Document. 
        'Return the number of DocSynopsis records deleted. This could be any number from 0 (probably an empty document) upwards.
        'Normally there will be one Synopsis record per Part. Documents usually have a minimum of 1 part.
        'Used as part of deleting a DocBatch or prior to replacing a duplicate document.
        mRoutineName = "DeleteDocSynopses(DocumentId As Integer)"

        Const ERRORROWCOUNT As Integer = -9999

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "DELETE FROM dbo.DocSynopsis "
        queryString = queryString & "WHERE DocumentId = @DocumentId "

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@DocumentId", DocumentId)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            Dim iRows As Integer = sqlCommand.ExecuteNonQuery()
            '
            Return iRows
            sqlCommand.Connection.Close()

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

            Return ERRORROWCOUNT
        End Try
        Return ERRORROWCOUNT
    End Function

    Private Function ListDocumentsInBatch(DocBatchId As Integer) As Collection
        'Return a collection of the DocumentLabelss of all the Documents in a Batch
        Dim DocumentLabelsCollection As New Collection

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB

        'Construct the query string
        Dim queryString As String = "SELECT DISTINCT doc.DocumentLabel From dbo.Document as doc WHERE "
        queryString = queryString & "doc.DocBatchId = @DocBatchId "

        Try
            Using sqlConnection As New SqlConnection(conString.ConnectionString)
                sqlConnection.Open()
                Using sqlCommand As New SqlCommand(queryString, sqlConnection)
                    sqlCommand.Parameters.AddWithValue("@DocBatchId", DocBatchId)

                    Using reader = sqlCommand.ExecuteReader()
                        If reader.HasRows Then
                            Do While reader.Read
                                DocumentLabelsCollection.Add(reader.Item("DocumentLabel"))
                            Loop
                        End If
                    End Using
                End Using
                sqlConnection.Close()
                Return DocumentLabelsCollection
            End Using

            'Should never reach this point!

            Call DocumentLabelsCollection.Clear()
            Return DocumentLabelsCollection 'Which will stop anything bad happening!

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

        Catch ex As Exception
            Call Me.handleGeneralException(ex)

        End Try

        Return DocumentLabelsCollection
    End Function

    Public Function DeleteParts(DocumentId As Integer) As Integer
        'Delete all the Part records associated with a Document. 
        'Return the number of Part records deleted. This could be any number from 0 (probably an empty document) upwards.
        'Documents usually have a minimum of 1 part and may have many.
        'Used as part of deleting a DocBatch or prior to replacing a duplicate document.
        mRoutineName = "DeleteParts(DocumentId As Integer)"

        Const ERRORROWCOUNT As Integer = -9999

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "DELETE FROM dbo.Part "
        queryString = queryString & "WHERE DocumentId = @DocumentId "

        'Console.WriteLine(queryString)

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@DocumentId", DocumentId)

        Try
            Dim numRows As Integer = 0

            sqlCommand.Connection.Open()
            Dim iRows As Integer = sqlCommand.ExecuteNonQuery()
            '
            Return iRows
            sqlCommand.Connection.Close()

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

            Return ERRORROWCOUNT
        End Try
        Return ERRORROWCOUNT
    End Function

    Private Sub DeleteBatch(DocBatchId As Integer)
        'Delete the DocumentBatch record for a DocBatchId 
        'There is a unique index on DocBatchId, so there 0 or 1.
        'Used as part of deleting a DocBatch or prior to replacing a duplicate batch.
        mRoutineName = "DeleteBatch(DocBatchId As Integer)"

        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        'Get Connection string data
        conString.DataSource = params.SQLDataSource
        conString.IntegratedSecurity = params.SQLIntegratedSecurity
        conString.InitialCatalog = params.SQLInitCatalogDB
        Dim sqlConnection As New System.Data.SqlClient.SqlConnection(conString.ConnectionString)

        Dim queryString As String = "DELETE FROM dbo.DocBatch "
        queryString = queryString & "WHERE DocBatchId = @DocBatchId "

        Dim sqlCommand = New SqlCommand(queryString, sqlConnection)

        'Now substitute the values into the command
        sqlCommand.Parameters.AddWithValue("@DocBatchId", DocBatchId)

        Try

            sqlCommand.Connection.Open()
            Dim iRows As Integer = sqlCommand.ExecuteNonQuery()
            '
            sqlCommand.Connection.Close()

        Catch ex As SqlException
            Call Me.handleSQLException(ex)

        End Try

    End Sub

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

        Console.WriteLine("Error: " & ex.Message.ToString & vbNewLine)
        Console.WriteLine(ex.ToString & vbNewLine)

        MsgBox("Non-SQL exception - Look at the console", MsgBoxStyle.Critical, "Bessie SQL")

    End Sub

End Class
