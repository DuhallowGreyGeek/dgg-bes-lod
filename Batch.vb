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
        mNumDocs = xDocList.DocList.Count
        Console.WriteLine("number of documents ---> " & xDocList.DocList.Count)

        'Dim i As Integer
        For Each currentbody As Doc In xDocList.DocList

            Console.WriteLine(" -------------> A document")

            Call currentbody.Dump() 'Dump contents to the console

            Console.WriteLine("  ------Now the parts---")

            For Each curPart As Part In currentbody.Parts
                'j = j + 1
                Call curPart.Dump() 'Dump contents to console
            Next
            Console.WriteLine()

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
    Public Sub Dump()
        'Dump Contents to Console
        Console.WriteLine(" ************* The Batch Itself **************** ")
        Console.WriteLine("           Nothing to Dump Directly             ")
        Console.WriteLine(" ************* Now all the Documents **************** ")
        Console.WriteLine("           Nothing to Dump Directly             ")
    End Sub
End Class
