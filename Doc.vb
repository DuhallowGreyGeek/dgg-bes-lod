Imports System.Xml

Public Class Doc
    'The Document class. This represents the physical document a user would recognise.
    'A lot of the work like "Reviews" or "Chapters" is handled by the Part object.

    Private mDocLabel As String
    Private mFilename As String
    Private mPath As String
    Private mDate As Date
    Private mTitle As String

    Private mPartList As Collection

    Public Sub New(xDocBody As XmlElement)

        If xDocBody.HasChildNodes Then
            Dim i As Integer
            For i = 0 To xDocBody.ChildNodes.Count - 1

                If xDocBody.ChildNodes.Item(i).NodeType = XmlNodeType.Element Then 'Skip comments

                    Dim xBodyPart As XmlElement = xDocBody.ChildNodes.Item(i)

                    Select Case xBodyPart.Name
                        Case "doc_header"
                            Dim xBodyHeader As XmlElement = xDocBody.GetElementsByTagName("doc_header").Item(i)
                            Dim curBodyHeader As New DocHeader(xBodyHeader)

                            mDocLabel = curBodyHeader.DocLabel
                            mFilename = curBodyHeader.FileName
                            mPath = curBodyHeader.FilePath
                            mDate = curBodyHeader.DocDate
                            mTitle = curBodyHeader.ExternalName

                        Case "part_list"

                            Dim xPartList As XmlElement = xBodyPart
                            Dim curPartList As New PartList(xPartList)
                            mPartList = curPartList.mPartList

                    End Select

                    'If xBodyPart.Attributes.Count > 0 Then
                    'Console.WriteLine(" has: " & xBodyPart.Attributes.Count.ToString & " attributes: ")

                    'Dim j As Integer
                    'For j = 0 To xBodyPart.Attributes.Count - 1
                    'Console.Write("    attribute: " & j.ToString & ": ")
                    'Console.Write(xBodyPart.Attributes.Item(j).Name.ToString)
                    'Console.WriteLine(" = " & xBodyPart.Attributes.Item(j).Value.ToString)
                    'Next

                    'End If

                End If
            Next

        End If

        'Call Me.Dump() 'Dump the contents to the console

    End Sub

    Public ReadOnly Property DocLabel As String
        Get
            Return mDocLabel
        End Get
    End Property

    Public ReadOnly Property FileName As String
        Get
            FileName = mFilename
            Return mFilename
        End Get
    End Property

    Public ReadOnly Property FilePath As String
        Get
            FilePath = mPath
            Return mPath
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

    Public ReadOnly Property Parts As Collection
        Get
            Return mPartList
        End Get
    End Property

    Public Sub Dump()
        'Dump the contents of the Doc (Document) to the console

        Console.WriteLine("    ---- Contents of Doc. ----- ")

        Console.WriteLine("        --- .DocLabel ---- " & Me.DocLabel)
        Console.WriteLine("        --- .DocDate ----- " & Me.DocDate)
        Console.WriteLine("        --- .FileName ---- " & Me.FileName)
        Console.WriteLine("        --- .FilePath ---- " & Me.FilePath)
        Console.WriteLine("        --- .ExternalName- " & Me.DocTitle)
        Console.WriteLine("      ---- parts -------------- ")
        Console.WriteLine("        --- Num Parts = " & Me.Parts.Count.ToString)

        For Each Part As Part In Me.Parts
            'Call Part.Dump() 'Dump contents of the part to the Console
        Next

        Console.WriteLine()

    End Sub

End Class
