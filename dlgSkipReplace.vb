Imports System.Windows.Forms

Public Class dlgSkipReplace
    'Custom dialog to ask the question "Replace (OK), Skip (Ignore) or Halt?"

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

End Class
