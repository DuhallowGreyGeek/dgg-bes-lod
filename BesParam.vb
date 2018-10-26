Imports System
Imports System.IO
Imports System.Text
Imports System.Xml

Public Class BesParam
    'Loads the application level parameter file.

    Protected Friend Const THISCLASS As String = "BesParam"

    Private mstrApplicationVersion As String
    Private mstrDatabaseVersion As String
    '
    Private mstrSQLDataSource As String
    Private mstrSQLIntegratedSecurity As String
    Private mblnSQLIntegratedSecurity As Boolean
    Private mstrSQLInitCatalogDB As String
    Private mstrFilePathHome As String
    '
    Private mstrWordSplitChars As String        'Characters which will cause a split between words
    Private mstrWordPermChars As String         'Characters which are permitted within words
    Private mstrWordRemoveTermChars As String   'Characters which are removed from beginning and end of words

    Public Sub New()
        Const METHOD As String = "New"
        'Dim e As New System.EventArgs

        Call ReadDBParms()      'Read the Database Parameters
        Call ReadLoaderParms()  'Read the parameters for the Loader process
    End Sub

    
    Private Sub ReadDBParms()
        Const METHOD As String = "ReadDBParms"
        Dim e As New System.EventArgs

        'Settings specifying the database to be used
        ' Read an existing XML parameter file
        ' Elements must be present in exactly the order I ask for them

        Dim settings As New XmlReaderSettings()
        settings.ConformanceLevel = ConformanceLevel.Fragment
        settings.IgnoreWhitespace = True
        settings.IgnoreComments = True

        'Use parameter file identified in settings.
        Dim path As String = My.Settings.ParmsPath & My.Settings.DBParmsFname

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
                reader.ReadStartElement("SQLDataSource")
                mstrSQLDataSource = reader.ReadString()
                'Console.Write("The content of the SQLDataSource element:  ")
                'Console.WriteLine(mstrSQLDataSource)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("SQLIntegratedSecurity")
                mstrSQLIntegratedSecurity = reader.ReadString()
                '
                'Console.Write("The content of the SQLIntegratedSecurity element:  ")
                'Console.WriteLine(mstrSQLIntegratedSecurity)
                '
                If mstrSQLIntegratedSecurity.ToUpper.Trim = "TRUE" Then
                    mblnSQLIntegratedSecurity = True
                ElseIf mstrSQLIntegratedSecurity.ToUpper.Trim = "FALSE" Then
                    mblnSQLIntegratedSecurity = False
                Else
                    Console.Write("SQLIntegratedSecurity Invalid: " & mstrSQLIntegratedSecurity)
                    MsgBox("SQLIntegratedSecurity Invalid: " & mstrSQLIntegratedSecurity)

                End If
                '
                'Console.Write("The content of the SQLIntegratedSecurity element:  ")
                'Console.WriteLine(mstrSQLIntegratedSecurity)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("SQLInitCatalogDB")
                mstrSQLInitCatalogDB = reader.ReadString()
                'Console.Write("The content of the SQLSQLInitCatalogDB element:  ")
                'Console.WriteLine(mstrSQLInitCatalogDB)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("file-path-home")
                mstrFilePathHome = reader.ReadString()
                'Console.Write("The content of the file-path-Home element:  ")
                'Console.WriteLine(mstrFilePathHome)
                reader.ReadEndElement()
                '
                reader.ReadEndElement()     'Read the end of the <parameters>
            End Using
        Catch exp As Exception
            Console.WriteLine(THISCLASS & "." & METHOD & " The process failed: {0}", e.ToString())
        End Try

    End Sub

    Private Sub ReadLoaderParms()
        Const METHOD As String = "ReadLoaderParms"
        Dim e As New System.EventArgs

        'Settings specifying the database to be used
        ' Read an existing XML parameter file
        ' Elements must be present in exactly the order I ask for them

        Dim settings As New XmlReaderSettings()
        settings.ConformanceLevel = ConformanceLevel.Fragment
        settings.IgnoreWhitespace = True
        settings.IgnoreComments = True

        'Use parameter file identified in settings.
        Dim path As String = My.Settings.ParmsPath & My.Settings.LdrParmsFname

        Dim reader As XmlReader = XmlReader.Create(path, settings)

        Try

            Using readerr As XmlReader = XmlReader.Create(path)
                ' Parse the XML document.  ReadString is used to 
                ' read the text content of the elements.
                reader.Read()
                reader.ReadStartElement("parameters")
                '
                reader.ReadStartElement("word-split-chars")
                mstrWordSplitChars = reader.ReadString()
                Console.Write("The content of the mstrWordSplitChars element:  ")
                Console.WriteLine(mstrWordSplitChars)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("word-perm-chars")
                mstrWordPermChars = reader.ReadString()
                Console.Write("The content of the mstrWordPermChars element:  ")
                Console.WriteLine(mstrWordPermChars)
                reader.ReadEndElement()
                '
                reader.ReadStartElement("word-remove-term-chars")
                mstrWordRemoveTermChars = reader.ReadString()
                Console.Write("The content of the mstrWordRemoveTermChars element:  ")
                Console.WriteLine(mstrWordRemoveTermChars)
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

    Public ReadOnly Property SQLDataSource As String
        Get
            Return mstrSQLDataSource
        End Get
    End Property

    Public ReadOnly Property SQLIntegratedSecurity As Boolean
        Get
            Return mblnSQLIntegratedSecurity
        End Get
    End Property

    Public ReadOnly Property SQLInitCatalogDB As String
        Get
            Return mstrSQLInitCatalogDB
        End Get

    End Property

    Public ReadOnly Property WordSplitChars As String
        Get
            Return mstrWordSplitChars
        End Get
    End Property

    Public ReadOnly Property WordPermChars As String
        Get
            Return mstrWordPermChars
        End Get
    End Property

    Public ReadOnly Property WordRemoveTermChars As String
        Get
            Return mstrWordRemoveTermChars
        End Get
    End Property

    Public Sub Dump()
        Console.WriteLine("__Dump Parameters:__")
        Console.WriteLine("__.appVersion --->" & Me.ApplicationVersion)
        Console.WriteLine("__.dbVersion ---->" & Me.DatabaseVersion)
        Console.WriteLine("__.SQLDataSource --->" & Me.SQLDataSource)
        Console.WriteLine("__.SQLIntegratedSecurity->" & Me.SQLIntegratedSecurity)
        Console.WriteLine("__.SQLInitCatalogDB -->" & Me.SQLInitCatalogDB)
    End Sub


End Class
