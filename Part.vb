Imports System.Xml
Imports System.Text.RegularExpressions

Public Class Part
    'Part is the place where all the information which will be of interest to people 
    'searching the documents will be focussed. 
    'A document may have 1 or more parts, which could be several reviews (as implied in this specific version)
    'or several chapters (another possibility I'm considering. 
    'A documents with zero parts would not be useful!

    Const MODNAME As String = "Part"
    Friend mRoutineName As String = ""      'To hold the name of the routine which generates an exception

    Private mDocumentId As Integer 'Foreign key to the containing document
    Private mPartNum As Integer     'Identifying sequence number within the document
    Private mSubject As String
    Private mDate As Date
    Private mFrom As String
    Private mTo As String
    Private mSynopsis As String

    Private mPartList As Collection

    Public Sub New(xDocBody As XmlElement)

        If xDocBody.HasChildNodes Then
            
            For Each node As XmlNode In xDocBody.ChildNodes
                If node.NodeType = XmlNodeType.Element Then 'Skip comments
                    Select Case node.Name
                        Case "subject"
                            mSubject = node.InnerText

                        Case "part_date"
                            mDate = node.InnerText

                        Case "from"
                            mFrom = node.InnerText

                        Case "to"
                            mTo = node.InnerText

                        Case "subject"
                            mSubject = node.InnerText

                        Case "synopsis"
                            mSynopsis = node.InnerText

                    End Select

                End If
            Next


            'If xBodyProp.Attributes.Count > 0 Then
            'Console.WriteLine(" has: " & xBodyProp.Attributes.Count.ToString & " attributes: ")

            'Dim j As Integer
            'For j = 0 To xBodyProp.Attributes.Count - 1
            'Console.Write("    attribute: " & j.ToString & ": ")
            'Console.Write(xBodyProp.Attributes.Item(j).Name.ToString)
            'Console.WriteLine(" = " & xBodyProp.Attributes.Item(j).Value.ToString)
            'Next

        End If

        '*** Temporary invoke the ParseString function
        Dim word As String

        'Data used for testing
        Const APST As String = "'"
        Dim myString = "Richard of York gave battle in vain!" + vbCrLf + "Peter Piper picked a peck of pickled pepper."
        myString = myString & ", , , and here is some??? more text after some empty words! "
        myString = myString & "The dreaded O" & APST & "Brien and O" & APST & "Toole should be handled! "
        myString = myString & "Of course McTavish, MacDonald will be handled, but Mac Donald and Mc Donald will be split. "
        myString = myString & " ... ...... filename.txt and ...filename.txt... "

        For Each word In ParseString(myString)
            '    Console.WriteLine("----------word--------> " & word)
        Next

        'Console.WriteLine(xBodyProp.Value)
        'End If
        '   Next

        'End If

    End Sub

    Public ReadOnly Property Subject As String
        Get
            Return mSubject
        End Get
    End Property

    Public ReadOnly Property DocDate As Date
        Get
            Return mDate
        End Get
    End Property

    Public ReadOnly Property DocFrom As String
        Get
            Return mFrom
        End Get
    End Property

    Public ReadOnly Property DocTo As String
        Get
            Return mTo
        End Get
    End Property

    Public ReadOnly Property Synopsis As String
        Get
            Return mSynopsis
        End Get
    End Property

    Public Sub Dump()
        Console.WriteLine("    --Dump-- Contents of part. ----- ")
        Console.WriteLine("        ---- .DocumentId---- " & Me.DocumentId.ToString)
        Console.WriteLine("        ---- .PartNum ----- " & Me.PartNum.ToString)
        Console.WriteLine("        ---- .DocDate ----- " & Me.DocDate.ToString)
        Console.WriteLine("        ---- .DocFrom ----- " & Me.DocFrom)
        Console.WriteLine("        ---- .DocTo ------- " & Me.DocTo)
        Console.WriteLine("        ---- .Subject ----- " & Me.Subject)
        'Console.WriteLine("        ---- .Synopsis ---- " & Me.Synopsis)
    End Sub

    Public Property DocumentId As Integer
        'Foreign key to the containing document
        'Set as Document is being added to the database, NOT by the xml processing.
        Get
            Return mDocumentId
        End Get
        Set(value As Integer)
            mDocumentId = value
        End Set
    End Property

    Public Property PartNum As Integer
        'Identifying sequence number within the document
        'Set as Document is being added to the database, NOT by the xml processing.
        Get
            Return mPartNum
        End Get
        Set(value As Integer)
            mPartNum = value
        End Set
    End Property

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

                Dim trimChars As String = ". " 'Characters to remove from the beginning and end (but not the middle) of the candidate

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
