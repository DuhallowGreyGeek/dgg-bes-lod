Imports System.Xml

Public Class BatchHeader
    Public Sub New(xDocBatch As XmlDocument)

        If xDocBatch.DocumentElement.HasChildNodes Then
            Console.WriteLine(xDocBatch.GetElementsByTagName("batch_header").ToString())

            Dim xBatchHeader As XmlElement = xDocBatch.GetElementsByTagName("batch_header").Item(0)

            If xBatchHeader.HasChildNodes Then
                Dim i As Integer
                For i = 0 To xBatchHeader.ChildNodes.Count - 1

                    If xBatchHeader.ChildNodes.Item(i).NodeType = XmlNodeType.Element Then 'Skip comments
                        Dim xHdrProp As XmlElement = xBatchHeader.ChildNodes.Item(i)

                        Console.Write(xHdrProp.Name & " Value: ")
                        Console.Write(xHdrProp.InnerText)

                        If xHdrProp.Attributes.Count > 0 Then
                            Console.WriteLine(" has: " & xHdrProp.Attributes.Count.ToString & " attributes: ")

                            Dim j As Integer
                            For j = 0 To xHdrProp.Attributes.Count - 1
                                Console.Write("    attribute: " & j.ToString & ": ")
                                Console.Write(xHdrProp.Attributes.Item(j).Name.ToString)
                                Console.WriteLine(" = " & xHdrProp.Attributes.Item(j).Value.ToString)
                            Next

                        End If

                        Console.WriteLine(xHdrProp.Value)
                    End If
                Next

            End If

        End If

    End Sub
End Class
