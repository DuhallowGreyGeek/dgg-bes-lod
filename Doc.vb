﻿Imports System.Xml
Imports System.Text.RegularExpressions

Public Class Doc
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

        Console.WriteLine("        --- .DocDate ----- " & Me.DocDate)
        Console.WriteLine("        --- .FileName ---- " & Me.FileName)
        Console.WriteLine("        --- .FilePath ---- " & Me.FilePath)
        Console.WriteLine("        --- .ExternalName- " & Me.DocTitle)
        Console.WriteLine("      ---- parts -------------- ")
        Console.WriteLine("        --- Num Parts = " & Me.Parts.Count.ToString)

        For Each Part As Part In Me.Parts
            Call Part.Dump() 'Dump contents of the part to the Console
        Next



        Console.WriteLine()


    End Sub

    Private ReadOnly Property ParseString(myString As String) As Collection
        Get
            'Function which returns a collection of all the words which we've identified,
            'in the order they were encountered and including duplicates.

            'The parser is simple but probably ineffient. It uses Microsoft written code, so it is probably as good
            'as it is possible to get.

            Dim wordList As New Collection

            'Define the characters which will be used to split the string into words
            'This will subsequently come from a system wide parameter.
            'A parameter will be used so that I can adjust the characters used without recompiling the program

            'Line ends will always cause a word-split.
            Const LINEEND As String = vbCr & vbCrLf
            '
            Dim splitChars As String = "\/@;:?!, " & LINEEND  'Allow imbedded full-stops to preserve file names (maybe)


            'Do the actual split
            Dim words = myString.Split(splitChars.ToCharArray, StringSplitOptions.RemoveEmptyEntries)

            'Significant punctuation should have caused word-splits.
            'Use a _regular expression_ to remove everything which isn't A-Z or a-z or exceptions.
            'Allow full-stops to remain (to try and preserve file-names ha! ha!) 
            'and then remove leading or trailing periods
            'and finally, if SQL LIKE needs it, fold to lower case.
            'No matter how this is done it is never going to be efficient, 
            'but it is only done once when the document is loaded.

            For Each candidateWord In words
                candidateWord = Regex.Replace(candidateWord, "[^A-Za-z.']+", String.Empty)

                Dim trimChars As String = ". " 'Characters to remove from the beginning and end of the candidate

                candidateWord = candidateWord.Trim(trimChars.ToCharArray)

                If candidateWord.Length > 0 Then    'Skip any candidates which have become empty

                    'Console.WriteLine(candidateWord)
                    wordList.Add(candidateWord.ToLower)
                End If
            Next
            Return wordList
        End Get
    End Property


End Class
