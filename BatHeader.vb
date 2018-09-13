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
                Dim i As Integer
                For i = 0 To xBatHeader.ChildNodes.Count - 1

                    If xBatHeader.ChildNodes.Item(i).NodeType = XmlNodeType.Element Then 'Skip comments

                        Dim xBatHdrProp As XmlElement = xBatHeader.ChildNodes.Item(i)

                        Select Case xBatHdrProp.Name
                            Case "bat_filename"
                                mFileName = xBatHdrProp.InnerText

                            Case "created_date"
                                mCreatedDate = Date.ParseExact(xBatHdrProp.InnerText, "yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo)

                        End Select
                    End If
                Next

            End If

        End If

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
End Class
