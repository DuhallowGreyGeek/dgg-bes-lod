Imports System.Windows.Forms

Public Class dlgSkipReplace

    Private Sub cmdReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReplace.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdSkip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSkip.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Ignore
        Me.Close()
    End Sub

    Private Sub cmdHalt_Click(sender As Object, e As EventArgs) Handles cmdHalt.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Abort
        Me.Close()
    End Sub

    Public Property Message As String
        Get
            Return Me.lblMsgText.Text
        End Get
        Set(value As String)
            Me.lblMsgText.Text = value
        End Set
    End Property

    Private Sub dlgSkipReplace_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.MinimizeBox = False
        Me.ControlBox = False
    End Sub

    Private Sub Dummy_DocDumplicate()
        ' Only here to make moving the invoking code around easy
        'Display a dialog like MsgBox returning Skip/Replace/Halt
        Dim dlg As New dlgSkipReplace

        dlg.Text = "Aargh!"
        dlg.Message = "I have a message for you!"

        Dim result As System.Windows.Forms.DialogResult = dlg.ShowDialog

        Select Case result
            Case Windows.Forms.DialogResult.OK
                'Replace the existing records
                Call MsgBox("Replace")
            Case Windows.Forms.DialogResult.Ignore
                'Skip the new record and retain the existing records
                Call MsgBox("Skip")
            Case Windows.Forms.DialogResult.Abort
                'Stop! Keep changes which have already been made but exit process
                Call MsgBox("Halt")
            Case Else
                'Shouldn't happen
                Call MsgBox("Oops!")
        End Select
    End Sub

End Class
