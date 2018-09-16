Module BesLodMain
    'Start  point for the BesLod application. Sets up things and launches the frmMain form.

    Public params As New BesParam 'Declare the parameters object
    Public mlodSQL As New BesLodSQL 'The SQL function

    Public frmMain As New frmBesLodMain

    Sub main()
        frmMain.ShowDialog()
    End Sub

End Module
