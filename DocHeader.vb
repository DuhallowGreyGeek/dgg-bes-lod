Imports System.Xml

Public Class DocHeader
    'The information and processing for the document as a whole.
    'Most of this data is stuff "applied from the outside" to identify or describe the document,
    'rather than the content of the document itself, which is in the (doc_)Parts..

    Private mFilename As String
    Private mDate As Date
    Private mFilePath As String
    Private mTitle As String

    Public Sub New(xBodyHeader As XmlElement)
        If xBodyHeader.HasChildNodes Then
            Dim i As Integer
            For i = 0 To xBodyHeader.ChildNodes.Count - 1

                If xBodyHeader.ChildNodes.Item(i).NodeType = XmlNodeType.Element Then 'Skip comments
                    Dim xDocHeaderProp As XmlElement = xBodyHeader.ChildNodes.Item(i)

                    Select Case xDocHeaderProp.Name
                        Case "doc_filename"
                            mFilename = xDocHeaderProp.InnerText

                        Case "doc_date"
                            mDate = Date.ParseExact(xDocHeaderProp.InnerText, "yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo)

                        Case "doc_path"
                            mFilePath = xDocHeaderProp.InnerText

                        Case "ext_name"
                            mTitle = xDocHeaderProp.InnerText

                    End Select

                    If xDocHeaderProp.Attributes.Count > 0 Then
                        Console.WriteLine(" has: " & xDocHeaderProp.Attributes.Count.ToString & " attributes: ")

                        Dim j As Integer
                        For j = 0 To xDocHeaderProp.Attributes.Count - 1
                            Console.Write("    attribute: " & j.ToString & ": ")
                            Console.Write(xDocHeaderProp.Attributes.Item(j).Name.ToString)
                            Console.WriteLine(" = " & xDocHeaderProp.Attributes.Item(j).Value.ToString)
                        Next

                    End If
                End If
            Next
        End If

        'Call Me.Dump() 'Dump contents to the console
    End Sub

    Public ReadOnly Property FileName As String
        Get
            Return mFilename
        End Get
    End Property

    Public ReadOnly Property DocDate As Date
        Get
            Return mDate
        End Get
    End Property

    Public ReadOnly Property FilePath As String
        Get
            Return mFilePath
        End Get
    End Property

    Public ReadOnly Property ExternalName As String
        Get
            Return mTitle
        End Get
    End Property

    Public Sub Dump()
        'Dump the contents of the DocHeader to the console

        Console.WriteLine("    ---- Contents of DocHeader. ----- ")

        Console.WriteLine("        --- .DocDate ----- " & Me.DocDate.ToString)
        Console.WriteLine("        --- .FileName ---- " & Me.FileName)
        Console.WriteLine("        --- .FilePath ---- " & Me.FilePath)
        Console.WriteLine("        --- .ExternalName- " & Me.ExternalName)

        Console.WriteLine()

    End Sub

End Class
