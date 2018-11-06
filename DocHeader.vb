Imports System.Xml

Public Class DocHeader
    'The information and processing for the document as a whole.
    'Most of this data is stuff "applied from the outside" to identify or describe the document,
    'rather than the content of the document itself, which is in the (doc_)Parts..

    Private mDocLabel As String
    Private mFilename As String
    Private mDate As Date
    Private mFilePath As String
    Private mTitle As String

    Public Sub New(xDocHeader As XmlElement)

        If xDocHeader.HasChildNodes Then

            For Each node As XmlNode In xDocHeader.ChildNodes
                If node.NodeType = XmlNodeType.Element Then ' Skip comments
                    Select Case node.Name
                        Case "doc_label"
                            mDocLabel = node.InnerText

                        Case "doc_filename"
                            mFilename = node.InnerText

                        Case "doc_date"
                            mDate = Date.ParseExact(node.InnerText, "yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo)

                        Case "doc_path"
                            mFilePath = node.InnerText

                        Case "ext_name"
                            mTitle = node.InnerText
                            '
                    End Select

                End If
            Next


            'If xDocHeaderProp.Attributes.Count > 0 Then
            'Console.WriteLine(" has: " & xDocHeaderProp.Attributes.Count.ToString & " attributes: ")

            'Dim j As Integer
            'For j = 0 To xDocHeaderProp.Attributes.Count - 1
            'Console.Write("    attribute: " & j.ToString & ": ")
            'Console.Write(xDocHeaderProp.Attributes.Item(j).Name.ToString)
            'Console.WriteLine(" = " & xDocHeaderProp.Attributes.Item(j).Value.ToString)
            'Next

        End If

        'Call Me.Dump() 'Dump contents to the console
    End Sub

    Public ReadOnly Property DocLabel As String
        Get
            Return mDocLabel
        End Get
    End Property

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

        Console.WriteLine("        --- .DocLabel ---- " & Me.DocLabel)
        Console.WriteLine("        --- .DocDate ----- " & Me.DocDate.ToString)
        Console.WriteLine("        --- .FileName ---- " & Me.FileName)
        Console.WriteLine("        --- .FilePath ---- " & Me.FilePath)
        Console.WriteLine("        --- .ExternalName- " & Me.ExternalName)

        Console.WriteLine()

    End Sub

End Class
