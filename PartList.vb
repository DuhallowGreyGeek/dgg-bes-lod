Imports System.Xml

Public Class PartList
    'Loads the list of Parts and then works through the list processing each part.

    Protected Friend mPartList As New Collection 'Collection to hold the Document Parts

    Public Sub New(xPartList As XmlElement)

        If xPartList.HasChildNodes Then

            If xPartList.GetElementsByTagName("part").Count > 0 Then 'Otherwise empty part_list

                For Each part As XmlElement In xPartList.ChildNodes     'Process each document part

                    If part.NodeType = XmlNodeType.Element Then 'Skip comments

                        'Create a Part object from the xml and add it to the PartList
                        Dim objPart As New Part(part)
                        mPartList.Add(objPart)

                    End If
                Next

            End If
        End If
        'Call Me.Dump() 'Dump the contents of the PartList to the Console
    End Sub

    Public ReadOnly Property DocPartList As Collection
        Get
            Return mPartList
        End Get
    End Property

    Public Sub Dump()
        'Dump the contents of the Part List to the console

        Console.WriteLine("    ---- Contents of PartList ----- ")
        Console.WriteLine("    --- num parts = " & Me.DocPartList.Count.ToString)

        For Each Part As Part In Me.DocPartList
            Call Part.Dump() 'Dump the part to the Console
        Next
        Console.WriteLine()

    End Sub
End Class
