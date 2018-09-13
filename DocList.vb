Imports System.Xml

Public Class DocList
    Protected Friend mDocList As New Collection 'Collection to hold the Document Bodies

    Public Sub New(xDocBatch As XmlDocument)
        If xDocBatch.DocumentElement.HasChildNodes Then

            If xDocBatch.GetElementsByTagName("doc_list").Count = 1 Then 'Otherwise error

                'New element from the tagged "doc_list"
                Dim xDocBatchList As XmlElement = xDocBatch.GetElementsByTagName("doc_list").Item(0)

                If xDocBatchList.HasChildNodes Then
                    Dim i As Integer
                    For i = 0 To xDocBatchList.ChildNodes.Count - 1
                        If xDocBatchList.ChildNodes.Item(i).NodeType = XmlNodeType.Element Then 'Skip comments
                            Dim xDocBody As XmlElement = xDocBatchList.ChildNodes.Item(i)
                            Dim objDocBody As New Doc(xDocBody)
                            mDocList.Add(objDocBody)

                        End If
                    Next
                Else
                    MsgBox("malformed XML file")
                End If

            End If
        End If

        Call Me.Dump() 'Dump contents to the Console

    End Sub

    Public ReadOnly Property DocList As Collection
        Get
            Return mDocList
        End Get
    End Property

    Public Sub Dump()
        'Dump the contents of the Doc *(Document) List to the console

        Console.WriteLine("    ---- Contents of DocList ----- ")
        Console.WriteLine("    --- num Docs = " & Me.DocList.Count.ToString)

        For Each Doc As Doc In Me.DocList
            Call Doc.Dump() 'Dump the Doc to the Console
        Next
        Console.WriteLine()

    End Sub
End Class
