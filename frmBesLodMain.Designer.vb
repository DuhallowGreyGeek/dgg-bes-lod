<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBesLodMain
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
        Me.TestSQLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestConnectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExceptionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestXMLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadXMLfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestDuplicatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GetDocIdToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.statStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.statMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dlgInputFile = New System.Windows.Forms.OpenFileDialog()
        Me.dlgOutputFile = New System.Windows.Forms.SaveFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lstLoadProgress = New System.Windows.Forms.ListBox()
        Me.prgLoadProgress = New System.Windows.Forms.ProgressBar()
        Me.cmdLoadXML = New System.Windows.Forms.Button()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.DeleteDocToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteDocUsagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteDocSynopsesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteDocPartsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveDocAndDependentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMenu.SuspendLayout()
        Me.statStatusStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMenu
        '
        Me.mnuMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem, Me.TestSQLToolStripMenuItem, Me.TestXMLToolStripMenuItem, Me.TestDuplicatesToolStripMenuItem})
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
        'TestSQLToolStripMenuItem
        '
        Me.TestSQLToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TestConnectionToolStripMenuItem, Me.SelectToolStripMenuItem, Me.InsertToolStripMenuItem, Me.ExceptionToolStripMenuItem})
        Me.TestSQLToolStripMenuItem.Enabled = False
        Me.TestSQLToolStripMenuItem.Name = "TestSQLToolStripMenuItem"
        Me.TestSQLToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.TestSQLToolStripMenuItem.Text = "TestSQL"
        '
        'TestConnectionToolStripMenuItem
        '
        Me.TestConnectionToolStripMenuItem.Name = "TestConnectionToolStripMenuItem"
        Me.TestConnectionToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.TestConnectionToolStripMenuItem.Text = "Test Connection"
        '
        'SelectToolStripMenuItem
        '
        Me.SelectToolStripMenuItem.Name = "SelectToolStripMenuItem"
        Me.SelectToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.SelectToolStripMenuItem.Text = "Select"
        '
        'InsertToolStripMenuItem
        '
        Me.InsertToolStripMenuItem.Name = "InsertToolStripMenuItem"
        Me.InsertToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.InsertToolStripMenuItem.Text = "Insert"
        '
        'ExceptionToolStripMenuItem
        '
        Me.ExceptionToolStripMenuItem.Name = "ExceptionToolStripMenuItem"
        Me.ExceptionToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.ExceptionToolStripMenuItem.Text = "Exception"
        '
        'TestXMLToolStripMenuItem
        '
        Me.TestXMLToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadXMLfileToolStripMenuItem})
        Me.TestXMLToolStripMenuItem.Enabled = False
        Me.TestXMLToolStripMenuItem.Name = "TestXMLToolStripMenuItem"
        Me.TestXMLToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
        Me.TestXMLToolStripMenuItem.Text = "TestXML"
        '
        'LoadXMLfileToolStripMenuItem
        '
        Me.LoadXMLfileToolStripMenuItem.Name = "LoadXMLfileToolStripMenuItem"
        Me.LoadXMLfileToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.LoadXMLfileToolStripMenuItem.Text = "LoadXMLfile"
        '
        'TestDuplicatesToolStripMenuItem
        '
        Me.TestDuplicatesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GetDocIdToolStripMenuItem, Me.DeleteDocToolStripMenuItem, Me.DeleteDocUsagesToolStripMenuItem, Me.DeleteDocSynopsesToolStripMenuItem, Me.DeleteDocPartsToolStripMenuItem, Me.RemoveDocAndDependentsToolStripMenuItem})
        Me.TestDuplicatesToolStripMenuItem.Name = "TestDuplicatesToolStripMenuItem"
        Me.TestDuplicatesToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.TestDuplicatesToolStripMenuItem.Text = "TestDuplicates"
        '
        'GetDocIdToolStripMenuItem
        '
        Me.GetDocIdToolStripMenuItem.Name = "GetDocIdToolStripMenuItem"
        Me.GetDocIdToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.GetDocIdToolStripMenuItem.Text = "GetDocId"
        '
        'statStatusStrip
        '
        Me.statStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statMsg})
        Me.statStatusStrip.Location = New System.Drawing.Point(0, 240)
        Me.statStatusStrip.Name = "statStatusStrip"
        Me.statStatusStrip.Size = New System.Drawing.Size(538, 22)
        Me.statStatusStrip.TabIndex = 1
        Me.statStatusStrip.Text = "statStatusStrip"
        '
        'statMsg
        '
        Me.statMsg.Name = "statMsg"
        Me.statMsg.Size = New System.Drawing.Size(0, 17)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.prgLoadProgress)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdLoadXML)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdTest)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdClose)
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
        'prgLoadProgress
        '
        Me.prgLoadProgress.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prgLoadProgress.Location = New System.Drawing.Point(0, 40)
        Me.prgLoadProgress.Name = "prgLoadProgress"
        Me.prgLoadProgress.Size = New System.Drawing.Size(538, 23)
        Me.prgLoadProgress.TabIndex = 8
        '
        'cmdLoadXML
        '
        Me.cmdLoadXML.Location = New System.Drawing.Point(3, 4)
        Me.cmdLoadXML.Name = "cmdLoadXML"
        Me.cmdLoadXML.Size = New System.Drawing.Size(75, 23)
        Me.cmdLoadXML.TabIndex = 7
        Me.cmdLoadXML.Text = "LoadXML"
        Me.cmdLoadXML.UseVisualStyleBackColor = True
        '
        'cmdTest
        '
        Me.cmdTest.Location = New System.Drawing.Point(287, 4)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(75, 23)
        Me.cmdTest.TabIndex = 4
        Me.cmdTest.Text = "Test"
        Me.cmdTest.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(109, 2)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'DeleteDocToolStripMenuItem
        '
        Me.DeleteDocToolStripMenuItem.Name = "DeleteDocToolStripMenuItem"
        Me.DeleteDocToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DeleteDocToolStripMenuItem.Text = "DeleteDoc"
        '
        'DeleteDocUsagesToolStripMenuItem
        '
        Me.DeleteDocUsagesToolStripMenuItem.Name = "DeleteDocUsagesToolStripMenuItem"
        Me.DeleteDocUsagesToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.DeleteDocUsagesToolStripMenuItem.Text = "DeleteDocUsages"
        '
        'DeleteDocSynopsesToolStripMenuItem
        '
        Me.DeleteDocSynopsesToolStripMenuItem.Name = "DeleteDocSynopsesToolStripMenuItem"
        Me.DeleteDocSynopsesToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.DeleteDocSynopsesToolStripMenuItem.Text = "DeleteDocSynopses"
        '
        'DeleteDocPartsToolStripMenuItem
        '
        Me.DeleteDocPartsToolStripMenuItem.Name = "DeleteDocPartsToolStripMenuItem"
        Me.DeleteDocPartsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.DeleteDocPartsToolStripMenuItem.Text = "DeleteDocParts"
        '
        'RemoveDocAndDependentsToolStripMenuItem
        '
        Me.RemoveDocAndDependentsToolStripMenuItem.Name = "RemoveDocAndDependentsToolStripMenuItem"
        Me.RemoveDocAndDependentsToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.RemoveDocAndDependentsToolStripMenuItem.Text = "RemoveDocAndDependents"
        '
        'frmBesLodMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 262)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.statStatusStrip)
        Me.Controls.Add(Me.mnuMenu)
        Me.MainMenuStrip = Me.mnuMenu
        Me.Name = "frmBesLodMain"
        Me.Text = "Bessie - Load Document Database"
        Me.mnuMenu.ResumeLayout(False)
        Me.mnuMenu.PerformLayout()
        Me.statStatusStrip.ResumeLayout(False)
        Me.statStatusStrip.PerformLayout()
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
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents cmdLoadXML As System.Windows.Forms.Button
    Friend WithEvents TestSQLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InsertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExceptionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestXMLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadXMLfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents prgLoadProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents TestConnectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents statMsg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TestDuplicatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetDocIdToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteDocToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteDocUsagesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteDocSynopsesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteDocPartsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveDocAndDependentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
