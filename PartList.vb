Imports System.Xml

Public Class PartList
    'Loads the list of Parts and then works through the list processing each part.

    Protected Friend mPartList As New Collection 'Collection to hold the Document Parts

    Public Sub New(xPartList As XmlElement)
        'Console.WriteLine("    --- Content of the PartList ---> " & xPartList.InnerXml)
        If xPartList.HasChildNodes Then

            If xPartList.GetElementsByTagName("doc_part").Count > 0 Then 'Otherwise empty part_list

                For Each xCurPart As XmlElement In xPartList.ChildNodes     'Process each doc_part
                    'Console.WriteLine("Document Part")
                    If xCurPart.NodeType = XmlNodeType.Element Then 'Skip comments

                        Dim objCurPart As New Part(xCurPart)
                        'Console.Write(" CurPart.Subject ---> " & objCurPart.Subject)
                        mPartList.Add(objCurPart)
                    End If
                Next

            End If
        End If

    End Sub

    Public ReadOnly Property DocPartList As Collection
        Get
            Return mPartList
        End Get
    End Property
End Class
