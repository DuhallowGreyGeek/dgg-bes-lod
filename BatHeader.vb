Imports System.Xml

Public Class BatHeader
    'Information and function relating to the header of the batch of documents.
    'This is all "administrative stuff". It's of interest to the people loading the 
    'documents and maintaining the database, but not usually to people searching the documents.

    Private mFileName As String
    Private mCreatedDate As Date

    Public Sub New(xDocBatch As XmlDocument)

        If xDocBatch.DocumentElement.HasChildNodes Then

            Dim xBatHeader As XmlElement = xDocBatch.GetElementsByTagName("bat_header").Item(0)

            If xBatHeader.HasChildNodes Then

                For Each node As XmlNode In xBatHeader.ChildNodes
                    If node.NodeType = XmlNodeType.Element Then 'Skip comments

                        Select Case node.Name
                            Case "bat_filename"
                                mFileName = node.InnerText

                            Case "created_date"
                                mCreatedDate = Date.ParseExact(node.InnerText, "yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo)

                        End Select

                    End If
                Next

            End If

        End If

        'Call Me.Dump() 'Dump contents to the console

    End Sub

    Public ReadOnly Property FileName As String
        Get
            Return mFileName
        End Get
    End Property

    Public ReadOnly Property CreatedDate As Date
        Get
            Return mCreatedDate
        End Get
    End Property

    Public Sub Dump()
        'Dump the contents of the BatHeader to the console

        Console.WriteLine("----- Contents of BatHeader. ----- ")

        Console.WriteLine("    --- .CreatedDate ----- " & Me.CreatedDate)
        Console.WriteLine("    --- .FileName -------- " & Me.FileName)
       
        Console.WriteLine()

    End Sub
End Class
