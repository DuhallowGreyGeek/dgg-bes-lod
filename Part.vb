Imports System.Xml
'Imports System.Text.RegularExpressions

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

End Class
