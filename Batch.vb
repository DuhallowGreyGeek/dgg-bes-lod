Imports System.Xml

Public Class Batch
    Private mNumDocs As Integer = 0                 'Number of documents in this batch
    Private mCurDocNum As Integer = 0               'Number of document currently being processed

    Public Sub New(docBatchFname As String)

        Dim xDocBatch As New XmlDocument
        xDocBatch.Load(docBatchFname) 'Load the document

        Dim xBatHeader As New BatHeader(xDocBatch)

        Dim xDocList As New DocList(xDocBatch)

        'Process each document
        mNumDocs = xDocList.DocBodyList.Count
        Console.WriteLine("number of documents ---> " & xDocList.DocBodyList.Count)
        '*** I expected this to work with a For Each, but I have to Dim the object here
        '*** to get it understood properly here.

        Dim i As Integer
        For i = 1 To xDocList.DocBodyList.Count
            Console.WriteLine(" -------------> A document Body")

            Dim currentBody As Doc = xDocList.DocBodyList.Item(i)
            Console.WriteLine("       filename ---------> " & currentBody.FileName)
            Console.WriteLine("       filepath ---------> " & currentBody.FilePath)
            Console.WriteLine("       date     ---------> " & currentBody.DocDate)
            Console.WriteLine("       title    ---------> " & currentBody.DocTitle)

            Console.WriteLine("  ------Now the parts---")

            Dim j As Integer
            For j = 1 To currentBody.Parts.Count
                Console.WriteLine("     ---- Part --- j= " & j.ToString)
                Dim curPart As Part = currentBody.Parts.Item(j)
                Console.WriteLine("       --Subj---> " & curPart.Subject)
                Console.WriteLine("       --From---> " & curPart.DocFrom)
                Console.WriteLine("       --To-----> " & curPart.DocTo)
                Console.WriteLine("       --Synop--> " & curPart.Synopsis)
            Next
            Console.WriteLine()

            'For Each curDocPart As DocPart In currentBody.Parts
            'Console.WriteLine("        ----> " & curDocPart.Subject)
            'Next

        Next
        Console.WriteLine()
        Console.WriteLine("--------- All Documents Processed ----------")

    End Sub
    Public ReadOnly Property NumDocs As Integer
        Get
            Return mNumDocs
        End Get
    End Property
    Public ReadOnly Property CurrentDocNum As Integer
        Get
            Return CurrentDocNum
        End Get
    End Property
End Class
