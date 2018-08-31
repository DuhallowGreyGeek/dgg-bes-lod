Module BesLodMain
    Public params As New BesParam 'Declare the parameters object
    Public mlodSQL As New BesLodSQL
    Public gLodXML As New BesLodXml

    Public frmMain As New frmBuntWunMain

    Sub main()
        frmMain.ShowDialog()
    End Sub

End Module
