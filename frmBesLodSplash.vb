Public NotInheritable Class frmBesLodSplash

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).


    Private Sub frmBesLodSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' *** My Stuff - Don't think I should need to do this, but "Revision" is not picked up from Project properties dialog.
        ' *** Update this value for each build, to get it shown on the Splash screen (see code at end too)
        '
        Dim myRevision As String = 1

        'Set up the dialog text at runtime according to the application's assembly information.  

        'TODO: Customize the application's assembly information in the "Application" pane of the project 
        '  properties dialog (under the "Project" menu).

        'Application title
        If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            'If the application title is missing, use the application name, without the extension
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If

        'Format the version information using the text set into the Version control at design time as the
        '  formatting string.  This allows for effective localization if desired.
        '  Build and revision information could be included by using the following code and changing the 
        '  Version control's designtime text to "Version {0}.{1:00}.{2}.{3}" or something similar.  See
        '  String.Format() in Help for more information.
        '
        Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, myRevision)

        'Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)

        'Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

        'Copyright info
        Copyright.Text = My.Application.Info.Copyright

        ' *** My stuff - I don't think I should need to do this, 
        ' *** as I don't think I should need to launch the splash screen.
        ' *** See also seeing of "Revision" at start
        'Console.WriteLine("Major ---> " & My.Application.Info.Version.Major)
        'Console.WriteLine("Minor ---> " & My.Application.Info.Version.Minor)
        'Console.WriteLine("Build ---> " & My.Application.Info.Version.Build)
        'Console.WriteLine("Revision ---> " & My.Application.Info.Version.Revision)
        'Doesn't seem to be picking up "Revision" - I'm not going to bother with it.
        '
        timTimer1.Interval = 2000 'Time I want the splash screen to show 2000 milliseconds = 2 seconds
        timTimer1.Start()

    End Sub

    Private Sub frmBesLodSplash_Expired(ByVal sender As Object, ByVal e As System.EventArgs) Handles timTimer1.Tick
        Me.Close()
        Me.Dispose()
    End Sub

End Class
