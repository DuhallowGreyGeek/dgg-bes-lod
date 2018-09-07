<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBuntWunMain
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
        Me.mnuMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParmValuesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParmsToStringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DumpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.statStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.dlgInputFile = New System.Windows.Forms.OpenFileDialog()
        Me.dlgOutputFile = New System.Windows.Forms.SaveFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lstLoadProgress = New System.Windows.Forms.ListBox()
        Me.cmdTempSelect = New System.Windows.Forms.Button()
        Me.cmdSQLTest = New System.Windows.Forms.Button()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdLoadDocs = New System.Windows.Forms.Button()
        Me.cmdShowFileHeader = New System.Windows.Forms.Button()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.cmdLoadXML = New System.Windows.Forms.Button()
        Me.mnuMenu.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMenu
        '
        Me.mnuMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.mnuMenu.Location = New System.Drawing.Point(0, 0)
        Me.mnuMenu.Name = "mnuMenu"
        Me.mnuMenu.Size = New System.Drawing.Size(538, 24)
        Me.mnuMenu.TabIndex = 0
        Me.mnuMenu.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.OpenToolStripMenuItem.Text = "Open File"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ParmValuesToolStripMenuItem, Me.ParmsToStringToolStripMenuItem, Me.DumpToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ParmValuesToolStripMenuItem
        '
        Me.ParmValuesToolStripMenuItem.Name = "ParmValuesToolStripMenuItem"
        Me.ParmValuesToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.ParmValuesToolStripMenuItem.Text = "ParmValues"
        '
        'ParmsToStringToolStripMenuItem
        '
        Me.ParmsToStringToolStripMenuItem.Name = "ParmsToStringToolStripMenuItem"
        Me.ParmsToStringToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.ParmsToStringToolStripMenuItem.Text = "ParmsToString"
        '
        'DumpToolStripMenuItem
        '
        Me.DumpToolStripMenuItem.Name = "DumpToolStripMenuItem"
        Me.DumpToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.DumpToolStripMenuItem.Text = "Dump"
        '
        'statStatusStrip
        '
        Me.statStatusStrip.Location = New System.Drawing.Point(0, 240)
        Me.statStatusStrip.Name = "statStatusStrip"
        Me.statStatusStrip.Size = New System.Drawing.Size(538, 22)
        Me.statStatusStrip.TabIndex = 1
        Me.statStatusStrip.Text = "StatusStrip1"
        '
        'dlgInputFile
        '
        Me.dlgInputFile.FileName = "OpenFileDialog1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstLoadProgress)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdLoadXML)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdTempSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdSQLTest)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdTest)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdClear)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdLoadDocs)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdShowFileHeader)
        Me.SplitContainer1.Size = New System.Drawing.Size(538, 216)
        Me.SplitContainer1.SplitterDistance = 149
        Me.SplitContainer1.TabIndex = 2
        '
        'lstLoadProgress
        '
        Me.lstLoadProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstLoadProgress.FormattingEnabled = True
        Me.lstLoadProgress.Location = New System.Drawing.Point(0, 0)
        Me.lstLoadProgress.Name = "lstLoadProgress"
        Me.lstLoadProgress.Size = New System.Drawing.Size(538, 149)
        Me.lstLoadProgress.TabIndex = 0
        '
        'cmdTempSelect
        '
        Me.cmdTempSelect.Location = New System.Drawing.Point(81, 33)
        Me.cmdTempSelect.Name = "cmdTempSelect"
        Me.cmdTempSelect.Size = New System.Drawing.Size(75, 23)
        Me.cmdTempSelect.TabIndex = 6
        Me.cmdTempSelect.Text = "Select 2"
        Me.cmdTempSelect.UseVisualStyleBackColor = True
        '
        'cmdSQLTest
        '
        Me.cmdSQLTest.Location = New System.Drawing.Point(324, 33)
        Me.cmdSQLTest.Name = "cmdSQLTest"
        Me.cmdSQLTest.Size = New System.Drawing.Size(75, 23)
        Me.cmdSQLTest.TabIndex = 5
        Me.cmdSQLTest.Text = "SQLTest"
        Me.cmdSQLTest.UseVisualStyleBackColor = True
        '
        'cmdTest
        '
        Me.cmdTest.Location = New System.Drawing.Point(324, 4)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(75, 23)
        Me.cmdTest.TabIndex = 4
        Me.cmdTest.Text = "Test"
        Me.cmdTest.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(243, 4)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 40)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(162, 2)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(75, 40)
        Me.cmdClear.TabIndex = 2
        Me.cmdClear.Text = "Temp-Update"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'cmdLoadDocs
        '
        Me.cmdLoadDocs.Location = New System.Drawing.Point(81, 4)
        Me.cmdLoadDocs.Name = "cmdLoadDocs"
        Me.cmdLoadDocs.Size = New System.Drawing.Size(75, 23)
        Me.cmdLoadDocs.TabIndex = 1
        Me.cmdLoadDocs.Text = "Load Docs"
        Me.cmdLoadDocs.UseVisualStyleBackColor = True
        '
        'cmdShowFileHeader
        '
        Me.cmdShowFileHeader.Location = New System.Drawing.Point(0, 4)
        Me.cmdShowFileHeader.Name = "cmdShowFileHeader"
        Me.cmdShowFileHeader.Size = New System.Drawing.Size(75, 39)
        Me.cmdShowFileHeader.TabIndex = 0
        Me.cmdShowFileHeader.Text = "Show Header"
        Me.cmdShowFileHeader.UseVisualStyleBackColor = True
        '
        'cmdLoadXML
        '
        Me.cmdLoadXML.Location = New System.Drawing.Point(406, 4)
        Me.cmdLoadXML.Name = "cmdLoadXML"
        Me.cmdLoadXML.Size = New System.Drawing.Size(75, 23)
        Me.cmdLoadXML.TabIndex = 7
        Me.cmdLoadXML.Text = "LoadXML"
        Me.cmdLoadXML.UseVisualStyleBackColor = True
        '
        'frmBuntWunMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 262)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.statStatusStrip)
        Me.Controls.Add(Me.mnuMenu)
        Me.MainMenuStrip = Me.mnuMenu
        Me.Name = "frmBuntWunMain"
        Me.Text = "Bessie - Load Document Database"
        Me.mnuMenu.ResumeLayout(False)
        Me.mnuMenu.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents statStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents dlgInputFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dlgOutputFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ParmValuesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParmsToStringToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DumpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lstLoadProgress As System.Windows.Forms.ListBox
    Friend WithEvents cmdShowFileHeader As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdLoadDocs As System.Windows.Forms.Button
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents cmdSQLTest As System.Windows.Forms.Button
    Friend WithEvents cmdTempSelect As System.Windows.Forms.Button
    Friend WithEvents cmdLoadXML As System.Windows.Forms.Button

End Class
