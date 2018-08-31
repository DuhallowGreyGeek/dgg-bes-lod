Imports System.IO
'Following only required while I am testing XML stuff in here
Imports System.Text
Imports System.Xml

Public Class frmBuntWunMain
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

    Private Sub cmdShowFileHeader_Click(sender As Object, e As EventArgs) Handles cmdShowFileHeader.Click
        'Create the DocBatch object (there can only ever be one) and display the header into
        mDocBatch = New DocBatch

    End Sub

    Private Sub cmdLoadDocs_Click(sender As Object, e As EventArgs) Handles cmdLoadDocs.Click
        'Load documents into the database
        'Temporarilly using this to run a SELECT

    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        'Clear the status window
        'Temporarilly using this to run an INSERT or an UPDATE

        Call mLodSQL.augTable_insert()

    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        'Close the application
        Me.Close()
    End Sub

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click
        'This is a temporary function and button for testing fragments of code.
        lstLoadProgress.Items.Add("Test Button clicked")

        'Process the XML document - will this approach be acceptable with a large XML file?
        Dim xDoc As New XmlDocument
        xDoc.Load("C:\Users\user\Documents\Bessie_20180824\BesTestLoad.xml")

        Dim s1 As String = xDoc.SelectSingleNode("doc_batch/batch_header/batch_filename").InnerText
        lstLoadProgress.Items.Add("_filename---> " & s1)

        Dim s2 As String = xDoc.SelectSingleNode("doc_batch/batch_header/created_date").InnerText
        lstLoadProgress.Items.Add("_created_date---> " & s2)

        lstLoadProgress.Items.Add("---now loop through the **documents** --- ")

        Dim documentList As XmlNodeList = xDoc.GetElementsByTagName("doc_body")
        Dim nodeText As String
        Dim nodeName As String

        For Each thisNode As XmlNode In documentList
            If (thisNode.Name = "doc_body") Then
                lstLoadProgress.Items.Add("--New Document--")

                'Note the FirstChild and LastChild properties and the ItemOf index is 0 based

                nodeText = thisNode.ChildNodes.ItemOf(0).InnerText.ToString
                lstLoadProgress.Items.Add("    --_/doc_filename --> " & nodeText)

                nodeName = thisNode.ChildNodes.ItemOf(1).Name.ToString
                nodeText = thisNode.ChildNodes.ItemOf(1).InnerText.ToString
                lstLoadProgress.Items.Add("    --_/" & nodeName & " --> " & nodeText)


                nodeText = thisNode.ChildNodes.ItemOf(2).InnerText.ToString
                lstLoadProgress.Items.Add("    --_/doc_title --> " & nodeText)


                Console.Write(thisNode.LastChild.InnerText.ToString)
                'Now get the review or revision data
                Dim revList As XmlNodeList = xDoc.GetElementsByTagName("doc_rev")
                For Each thisReview As XmlNode In revList
                    lstLoadProgress.Items.Add("     --New Review--")
                    'Note the FirstChild and LastChild properties and the ItemOf index is 0 based
                    Dim j As Integer

                    For j = 0 To thisReview.ChildNodes.Count - 1
                        nodeName = thisReview.ChildNodes.ItemOf(j).Name.ToString
                        nodeText = thisReview.ChildNodes.ItemOf(j).InnerText.ToString
                        lstLoadProgress.Items.Add("         --_/" & nodeName & " --> " & nodeText)
                    Next

                Next

                'nodeText = thisNode.FirstChild.InnerText.ToString
                'nodeText = thisNode.LastChild.InnerText.ToString

            End If
            If (thisNode.Name = "doc_rev") Then
                lstLoadProgress.Items.Add("     --New Review--")

                'Note the FirstChild and LastChild properties and the ItemOf index is 0 based
                Dim j As Integer

                For j = 0 To thisNode.ChildNodes.Count
                    nodeName = thisNode.ChildNodes.ItemOf(1).Name.ToString
                    nodeText = thisNode.ChildNodes.ItemOf(1).InnerText.ToString
                    lstLoadProgress.Items.Add("         --_/" & nodeName & " --> " & nodeText)
                Next

            End If
            lstLoadProgress.Items.Add(" ")
        Next

        lstLoadProgress.Items.Add(" ")
        lstLoadProgress.Items.Add("--Number of documents found ---> " & documentList.Count.ToString)

    End Sub

    Private Sub cmdSQLTest_Click(sender As Object, e As EventArgs) Handles cmdSQLTest.Click
        'Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder

        Call mLodSQL.augTable_fail()

        
    End Sub

    Private Sub cmdTempSelect_Click(sender As Object, e As EventArgs) Handles cmdTempSelect.Click
        'Temporary function to test "SELECT" from the database
        Call mLodSQL.augTable_select()

        '        Dim conString As New System.Data.SqlClient.SqlConnectionStringBuilder
        '
        'Get Connection string data
        'conString.DataSource = mParams.SQLDataSource
        'conString.IntegratedSecurity = mParams.SQLIntegratedSecurity
        'conString.InitialCatalog = mParams.SQLInitCatalogDB

        'Try
        'Using Con As New SqlConnection(conString.ConnectionString)
        'Con.Open()
        'Using Com As New SqlCommand("Select * From dbo.AugTable", Con)
        'Using RDR = Com.ExecuteReader()
        'If RDR.HasRows Then
        'Do While RDR.Read

        'lstLoadProgress.Items.Add("--- " & RDR.Item("AugId").ToString() & " --- " & RDR.Item("DatetimeString").ToString())

        'Loop
        'End If
        'End Using
        'End Using
        'Con.Close()
        'End Using

        'Catch ex As SqlException
        ' Dim i As Integer = 0
        'For i = 0 To ex.Errors.Count - 1
        'Console.WriteLine("Index#: " & i.ToString & vbNewLine & "Error: " & ex.Errors(i).ToString & vbNewLine)
        'Next
        'MsgBox("SQL Exception trapped - Look at the console")

        'Catch ex As Exception

        'Console.WriteLine("Error: " & ex.Message.ToString & " is not a valid column" & vbNewLine)
        'Console.WriteLine(ex.ToString & vbNewLine)

        'MsgBox("Non-SQL exception - Look at the console")
        'End Try

    End Sub
End Class
