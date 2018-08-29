Imports System
Imports System.IO
Imports System.Text
Imports System.Xml

Public Class DocBatch
    Protected Friend Const THISCLASS As String = "DocBatch"

    Public Sub New()
        Const METHOD As String = "New"
        Dim e As New System.EventArgs
        Dim strApplicationVersion As String

        ' Read an existing XML parameter file
        ' Elements must be present in exactly the order I ask for them

        Dim settings As New XmlReaderSettings()
        settings.ConformanceLevel = ConformanceLevel.Fragment
        settings.IgnoreWhitespace = True
        settings.IgnoreComments = True

        MsgBox("Wow!")

        Dim path As String = "C:\Users\user\Documents\Bessie_20180824\BesTestLoad.xml"
        'Dim path As String = "BesTestLoad.xml"        'Use the parameters file with the executable
        Dim reader As XmlReader = XmlReader.Create(path, settings)

        Try

            Using readerr As XmlReader = XmlReader.Create(path)
                ' Parse the XML document.  ReadString is used to 
                ' read the text content of the elements.
                reader.Read()
                reader.ReadStartElement("document_batch")
                reader.ReadStartElement("batch_header")

                '
                reader.ReadStartElement("batch_filename")
                strApplicationVersion = reader.ReadString()
                'Console.Write("The content of the applicationVersion element:  ")
                'Console.WriteLine(mstrApplicationVersion)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("created_date")
                strApplicationVersion = reader.ReadString()
                'Console.Write("The content of the applicationVersion element:  ")
                'Console.WriteLine(mstrApplicationVersion)
                reader.ReadEndElement()
                '
                'reader.ReadEndElement()     'Read the end of the <parameters>
            End Using
        Catch exp As Exception
            Console.WriteLine(THISCLASS & "." & METHOD & " The process failed: {0}", e.ToString())
        End Try

    End Sub

End Class
