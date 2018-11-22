Public Class DGGUtilities
    'Simple utilities to make code elsewhere tidier

    Public Sub WriteConsMsg(msg As String)
        'Depending on the value of the setting, write a status message to the Console.
        'Hardly worth doing except that it takes the "ifs" out of the main code.

        If My.Settings.DocsToConsole Then
            Console.WriteLine(msg)
        End If
    End Sub

End Class
