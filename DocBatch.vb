Imports System.Xml

Public Class DocBatch
    Public Sub New(docBatchFname As String)
        Console.WriteLine("Processing file: " & docBatchFname)

        Dim xDocBatch As New XmlDocument
        xDocBatch.Load(docBatchFname) 'Load the document
        Console.WriteLine("--Loaded the document--")

        Dim xDocHeader As New BatchHeader(xDocBatch)
        Console.WriteLine("--Processed the Batch Header--")

        Dim xDocList As New DocList(xDocBatch)
        Console.WriteLine("--Loaded the DocList--")

        'Process each document
        Console.WriteLine("number of documents ---> " & xDocList.DocBodyList.Count)
        For Each xDocBody In xDocList.DocBodyList
            Console.WriteLine("     ----> A document Body")
            Console.Write(xDocBody.Name & " Value: ")
            Console.WriteLine(xDocBody.InnerXml)

            Console.WriteLine(" ")
        Next

    End Sub
End Class
