Imports System.Text.RegularExpressions

Public Class DBDictionary
    'Contains the dictionary of words which have been used in the vocabulary of the documents added to the DB.
    'Returns the WordId of a word
    'Also contains the function to parse a string into words according to rules.
    Const MODNAME As String = "DBDictionary"
    Protected mRoutineName As String = ""      'To hold the name of the routine which generates an exception
    Protected mDictionary As New Dictionary(Of String, Integer)
    Protected mTempWordId As Integer = 0

    Public Sub New()
        'Setup for the dictionary. May choose to load from the database at start-up.
        'More likely to populate "on-demand"
    End Sub

    Public ReadOnly Property GetWordId(Word As String) As Integer
        'Return the WordId for the word
        Get
            mRoutineName = "GetWordId_Get"
            Const ERRORKEY As Integer = -99999

            If mDictionary.ContainsKey(Word) Then
                Return mDictionary.Item(Word)
            Else
                mTempWordId = mTempWordId + 1
                mDictionary.Item(Word) = mTempWordId
            End If

            Return mTempWordId

        End Get
    End Property

    Public ReadOnly Property ParseString(myString As String) As Collection
        Get
            mRoutineName = "ParseString_Get"
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
