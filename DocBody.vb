Imports System.Xml

Public Class DocBody
    Private mFilename As String
    Private mDate As Date
    Private mTitle As String

    Public Sub New(xDocBody As XmlElement)
        'Console.Write(xDocBody.Name & " Value: ")
        'Console.WriteLine(xDocBody.InnerXml)

        If xDocBody.HasChildNodes Then
            Dim i As Integer
            For i = 0 To xDocBody.ChildNodes.Count - 1

                If xDocBody.ChildNodes.Item(i).NodeType = XmlNodeType.Element Then 'Skip comments
                    Dim xBodyProp As XmlElement = xDocBody.ChildNodes.Item(i)

                    Console.Write(xBodyProp.Name & " Value: ")
                    Console.Write(xBodyProp.InnerText)

                    Select Case xBodyProp.Name
                        Case "doc_filename"
                            mFilename = xBodyProp.InnerText

                        Case "doc_date"
                            mDate = xBodyProp.InnerText

                        Case "doc_title"
                            mTitle = xBodyProp.InnerText

                    End Select

                    If xBodyProp.Attributes.Count > 0 Then
                        Console.WriteLine(" has: " & xBodyProp.Attributes.Count.ToString & " attributes: ")

                        Dim j As Integer
                        For j = 0 To xBodyProp.Attributes.Count - 1
                            Console.Write("    attribute: " & j.ToString & ": ")
                            Console.Write(xBodyProp.Attributes.Item(j).Name.ToString)
                            Console.WriteLine(" = " & xBodyProp.Attributes.Item(j).Value.ToString)
                        Next

                    End If

                    Console.WriteLine(xBodyProp.Value)
                End If
            Next

        End If

        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!! ")
    End Sub

    Public ReadOnly Property FileName As String
        Get
            FileName = mFilename
            Return mFilename
        End Get
    End Property

    Public ReadOnly Property DocDate As Date
        Get
            DocDate = mDate
            Return mDate
        End Get
    End Property

    Public ReadOnly Property DocTitle As String
        Get
            DocTitle = mTitle
            Return mTitle
        End Get
    End Property


End Class
