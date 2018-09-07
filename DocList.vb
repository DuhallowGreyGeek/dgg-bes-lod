Imports System.Xml

Public Class DocList
    Protected Friend mDocBodyList As New Collection 'Collection to hold the Document Bodies

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
                            mDocBodyList.Add(xDocBody)

                        End If
                    Next
                Else
                    MsgBox("malformed XML file")
                End If

            End If
        End If

    End Sub

    Public ReadOnly Property DocBodyList As Collection
        Get
            Return mDocBodyList
        End Get
    End Property
End Class
