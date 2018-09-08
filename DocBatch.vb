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
        '*** I expected this to work with a For Each. Instead it seems that I have to Dim the object here
        '*** to get it understood properly here.
        '
        'For Each xDocBody In xDocList.DocBodyList
        'For Each DocBody In xDocList.DocBodyList
        'Console.WriteLine("     ----> A document Body")
        'Console.Write(xDocBody.Name & " Value: ")
        'Console.WriteLine(xDocBody.InnerXml)

        'Console.WriteLine("     filename ---------> " & DocBody.ToString)
        'Console.WriteLine("     date     ---------> ")
        'Console.WriteLine("     title    ---------> ")

        'Next

        Dim i As Integer
        For i = 1 To xDocList.DocBodyList.Count
            Console.WriteLine("     ----> A document Body")
            'Console.WriteLine("     filename ---------> " & xDocList.DocBodyList.Item(i).filename)
            'Console.WriteLine("     date     ---------> " & xDocList.DocBodyList.Item(i).docdate)
            'Console.WriteLine("     title    ---------> " & xDocList.DocBodyList.Item(i).doctitle)

            Dim currentBody As DocBody = xDocList.DocBodyList.Item(i)
            Console.WriteLine("     filename ---------> " & currentBody.FileName)
            Console.WriteLine("     date     ---------> " & currentBody.DocDate)
            Console.WriteLine("     title    ---------> " & currentBody.DocTitle)
            'currentBody.

            Console.WriteLine(" ")
        Next

    End Sub
End Class
