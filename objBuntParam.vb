Imports System
Imports System.IO
Imports System.Text
Imports System.Xml

Public Class BuntParam
    Protected Friend Const THISCLASS As String = "BuntParam"

    Private mstrApplicationVersion As String
    Private mstrDatabaseVersion As String
    Private mstrUsePhrases As String
    Private mblnUsePhrases As Boolean
    Private mstrMaxPhraseSize As String
    Private mintMaxPhraseSize As Integer
    Private mstrRespectClausePunc As String
    Private mblnRespectClausePunc As Boolean

    Public Sub New()
        Const METHOD As String = "New"
        Dim e As New System.EventArgs

        ' Read an existing XML parameter file
        ' Elements must be present in exactly the order I ask for them

        Dim settings As New XmlReaderSettings()
        settings.ConformanceLevel = ConformanceLevel.Fragment
        settings.IgnoreWhitespace = True
        settings.IgnoreComments = True

        'Dim path As String = "C:\Users\Tom\Documents\Bunter_20170601\BunterApp\BuntWun\BuntParms.xml"
        Dim path As String = "BuntParms.xml"        'Use the parameters file with the executable
        Dim reader As XmlReader = XmlReader.Create(path, settings)

        Try

            Using readerr As XmlReader = XmlReader.Create(path)
                ' Parse the XML document.  ReadString is used to 
                ' read the text content of the elements.
                reader.Read()
                reader.ReadStartElement("parameters")
                '
                reader.ReadStartElement("applicationVersion")
                mstrApplicationVersion = reader.ReadString()
                'Console.Write("The content of the applicationVersion element:  ")
                'Console.WriteLine(mstrApplicationVersion)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("databaseVersion")
                mstrDatabaseVersion = reader.ReadString()
                'Console.Write("The content of the databaseVersion element:  ")
                'Console.WriteLine(mstrDatabaseVersion)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("usePhrases")
                mstrUsePhrases = reader.ReadString()
                If mstrUsePhrases.ToUpper = "TRUE" Then
                    mblnUsePhrases = True
                ElseIf mstrUsePhrases.ToUpper = "FALSE" Then
                    mblnUsePhrases = False
                Else
                    MsgBox("Invalid usePhrases parm: " & mstrUsePhrases)
                End If
                'Console.Write("The content of the usePhrases element:  ")
                'Console.WriteLine(mstrUsePhrases)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("maxPhraseSize")
                mstrMaxPhraseSize = reader.ReadString()
                'Console.Write("The content of the maxPhraseSize element:  ")
                'Console.WriteLine(mstrMaxPhraseSize)
                reader.ReadEndElement()
                mintMaxPhraseSize = Int(mstrMaxPhraseSize)
                '
                reader.ReadStartElement("respectClausePunc")
                mstrRespectClausePunc = reader.ReadString()
                If mstrRespectClausePunc.ToUpper = "TRUE" Then
                    mblnRespectClausePunc = True
                ElseIf mstrRespectClausePunc.ToUpper = "FALSE" Then
                    mblnRespectClausePunc = False
                Else
                    MsgBox("Invalid respectClausePunc parm: " & mstrRespectClausePunc)
                End If

                'Console.Write("The content of the respectClausePunc element:  ")
                'Console.WriteLine(mstrRespectClausePunc)
                reader.ReadEndElement()
                '
                reader.ReadEndElement()     'Read the end of the <parameters>
            End Using
        Catch exp As Exception
            Console.WriteLine(THISCLASS & "." & METHOD & " The process failed: {0}", e.ToString())
        End Try

    End Sub

    Public ReadOnly Property ApplicationVersion As String
        Get
            Return mstrApplicationVersion
        End Get
    End Property

    Public ReadOnly Property DatabaseVersion As String
        Get
            Return mstrDatabaseVersion
        End Get
    End Property

    Public ReadOnly Property UsePhrases As Boolean
        Get
            Return mblnUsePhrases
        End Get
    End Property

    Public ReadOnly Property MaxPhraseSize As Integer
        Get
            Return mintMaxPhraseSize
        End Get
    End Property

    Public ReadOnly Property RespectClausePunc As Boolean
        Get
            Return mblnRespectClausePunc
        End Get

    End Property

    Public Sub Dump()
        Console.WriteLine("__Dump Parameters:__")
        Console.WriteLine("__.appVersion --->" & Me.ApplicationVersion)
        Console.WriteLine("__.dbVersion ---->" & Me.DatabaseVersion)
        Console.WriteLine("__.UsePhrases --->" & Me.UsePhrases)
        Console.WriteLine("__.MaxPhraseSize->" & Me.MaxPhraseSize)
        Console.WriteLine("__.respectPunc -->" & Me.RespectClausePunc)
    End Sub


End Class
