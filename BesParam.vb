Imports System
Imports System.IO
Imports System.Text
Imports System.Xml

Public Class BesParam
    'Loads the application level parameter file.

    Protected Friend Const THISCLASS As String = "BesParam"

    Private mstrApplicationVersion As String
    Private mstrDatabaseVersion As String
    Private mstrSQLDataSource As String
    Private mstrSQLIntegratedSecurity As String
    Private mblnSQLIntegratedSecurity As Boolean
    Private mstrSQLInitCatalogDB As String

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
        Dim path As String = "BesParms.xml"        'Use the parameters file with the executable
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
                If mstrSQLIntegratedSecurity.ToUpper.Trim = "TRUE" Then
                    mblnSQLIntegratedSecurity = True
                ElseIf mstrSQLIntegratedSecurity.ToUpper.Trim = "FALSE" Then
                    mblnSQLIntegratedSecurity = False
                Else
                    'Console.Write("SQLIntegratedSecurity Invalid: " & mstrSQLIntegratedSecurity)
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

    Public Sub Dump()
        Console.WriteLine("__Dump Parameters:__")
        Console.WriteLine("__.appVersion --->" & Me.ApplicationVersion)
        Console.WriteLine("__.dbVersion ---->" & Me.DatabaseVersion)
        Console.WriteLine("__.SQLDataSource --->" & Me.SQLDataSource)
        Console.WriteLine("__.SQLIntegratedSecurity->" & Me.SQLIntegratedSecurity)
        Console.WriteLine("__.SQLInitCatalogDB -->" & Me.SQLInitCatalogDB)
    End Sub


End Class
