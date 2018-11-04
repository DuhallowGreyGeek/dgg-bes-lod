Module BesLodMain
    'Start  point for the BesLod application. Sets up things and launches the frmMain form.

    Public params As New BesParam   'Declare the parameters object
    Public mlodSQL As New BesLodSQL 'The SQL function *** Consolidating the SQL in one place like this was a BAD idea!
    Public dict As New DBDictionary 'The dictionary containing the "vocabulary"

    Public frmMain As New frmBesLodMain

    Sub main()
        'Shouldn't need to do this but splash screen option of Project doesn't seem to work properly
        'Probably means I'm not doing it right!
        Dim frmSplash As New frmBesLodSplash
        frmSplash.ShowDialog()

        'Show the Main screen
        frmMain.ShowDialog()

        'Clean up
        frmMain.Dispose()
    End Sub

End Module
