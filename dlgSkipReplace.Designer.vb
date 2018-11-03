<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSkipReplace
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdHalt = New System.Windows.Forms.Button()
        Me.cmdReplace = New System.Windows.Forms.Button()
        Me.cmdSkip = New System.Windows.Forms.Button()
        Me.lblMsgText = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdHalt, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdReplace, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdSkip, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(25, 109)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(490, 55)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'cmdHalt
        '
        Me.cmdHalt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdHalt.Location = New System.Drawing.Point(355, 10)
        Me.cmdHalt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmdHalt.Name = "cmdHalt"
        Me.cmdHalt.Size = New System.Drawing.Size(106, 35)
        Me.cmdHalt.TabIndex = 2
        Me.cmdHalt.Text = "Halt"
        '
        'cmdReplace
        '
        Me.cmdReplace.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdReplace.Location = New System.Drawing.Point(17, 10)
        Me.cmdReplace.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmdReplace.Name = "cmdReplace"
        Me.cmdReplace.Size = New System.Drawing.Size(129, 35)
        Me.cmdReplace.TabIndex = 0
        Me.cmdReplace.Text = "Replace"
        '
        'cmdSkip
        '
        Me.cmdSkip.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSkip.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdSkip.Location = New System.Drawing.Point(194, 10)
        Me.cmdSkip.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmdSkip.Name = "cmdSkip"
        Me.cmdSkip.Size = New System.Drawing.Size(100, 35)
        Me.cmdSkip.TabIndex = 1
        Me.cmdSkip.Text = "Skip"
        '
        'lblMsgText
        '
        Me.lblMsgText.AutoSize = True
        Me.lblMsgText.Location = New System.Drawing.Point(38, 48)
        Me.lblMsgText.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMsgText.Name = "lblMsgText"
        Me.lblMsgText.Size = New System.Drawing.Size(84, 20)
        Me.lblMsgText.TabIndex = 1
        Me.lblMsgText.Text = "lblMsgText"
        '
        'dlgSkipReplace
        '
        Me.AcceptButton = Me.cmdReplace
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdSkip
        Me.ClientSize = New System.Drawing.Size(534, 182)
        Me.Controls.Add(Me.lblMsgText)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgSkipReplace"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "dlgSkipReplace"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdReplace As System.Windows.Forms.Button
    Friend WithEvents cmdSkip As System.Windows.Forms.Button
    Friend WithEvents cmdHalt As System.Windows.Forms.Button
    Friend WithEvents lblMsgText As System.Windows.Forms.Label

End Class
