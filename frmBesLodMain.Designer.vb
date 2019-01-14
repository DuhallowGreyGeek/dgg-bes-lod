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
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocBatchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveByFileNameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveByIdToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveByLabelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveByIdToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParmValuesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParmsToStringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DumpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestSQLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestConnectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestDuplicatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GetDocIdToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteDocToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteDocUsagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteDocSynopsesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteDocPartsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.mnuMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem, Me.TestSQLToolStripMenuItem, Me.TestDuplicatesToolStripMenuItem})
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
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DocBatchToolStripMenuItem, Me.DocumentToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'DocBatchToolStripMenuItem
        '
        Me.DocBatchToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveByFileNameToolStripMenuItem, Me.RemoveByIdToolStripMenuItem})
        Me.DocBatchToolStripMenuItem.Name = "DocBatchToolStripMenuItem"
        Me.DocBatchToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DocBatchToolStripMenuItem.Text = "Doc Batch"
        '
        'RemoveByFileNameToolStripMenuItem
        '
        Me.RemoveByFileNameToolStripMenuItem.Name = "RemoveByFileNameToolStripMenuItem"
        Me.RemoveByFileNameToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.RemoveByFileNameToolStripMenuItem.Text = "Remove by FileName"
        '
        'RemoveByIdToolStripMenuItem
        '
        Me.RemoveByIdToolStripMenuItem.Enabled = False
        Me.RemoveByIdToolStripMenuItem.Name = "RemoveByIdToolStripMenuItem"
        Me.RemoveByIdToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.RemoveByIdToolStripMenuItem.Text = "Remove by Id"
        '
        'DocumentToolStripMenuItem
        '
        Me.DocumentToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveByLabelToolStripMenuItem, Me.RemoveByIdToolStripMenuItem1})
        Me.DocumentToolStripMenuItem.Name = "DocumentToolStripMenuItem"
        Me.DocumentToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DocumentToolStripMenuItem.Text = "Document"
        '
        'RemoveByLabelToolStripMenuItem
        '
        Me.RemoveByLabelToolStripMenuItem.Name = "RemoveByLabelToolStripMenuItem"
        Me.RemoveByLabelToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.RemoveByLabelToolStripMenuItem.Text = "Remove by Label"
        '
        'RemoveByIdToolStripMenuItem1
        '
        Me.RemoveByIdToolStripMenuItem1.Name = "RemoveByIdToolStripMenuItem1"
        Me.RemoveByIdToolStripMenuItem1.Size = New System.Drawing.Size(164, 22)
        Me.RemoveByIdToolStripMenuItem1.Text = "Remove by Id"
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
        Me.TestSQLToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TestConnectionToolStripMenuItem})
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
        'TestDuplicatesToolStripMenuItem
        '
        Me.TestDuplicatesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GetDocIdToolStripMenuItem, Me.DeleteDocToolStripMenuItem, Me.DeleteDocUsagesToolStripMenuItem, Me.DeleteDocSynopsesToolStripMenuItem, Me.DeleteDocPartsToolStripMenuItem})
        Me.TestDuplicatesToolStripMenuItem.Name = "TestDuplicatesToolStripMenuItem"
        Me.TestDuplicatesToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.TestDuplicatesToolStripMenuItem.Text = "TestDuplicates"
        '
        'GetDocIdToolStripMenuItem
        '
        Me.GetDocIdToolStripMenuItem.Name = "GetDocIdToolStripMenuItem"
        Me.GetDocIdToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.GetDocIdToolStripMenuItem.Text = "GetDocId"
        '
        'DeleteDocToolStripMenuItem
        '
        Me.DeleteDocToolStripMenuItem.Name = "DeleteDocToolStripMenuItem"
        Me.DeleteDocToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.DeleteDocToolStripMenuItem.Text = "DeleteDoc"
        '
        'DeleteDocUsagesToolStripMenuItem
        '
        Me.DeleteDocUsagesToolStripMenuItem.Name = "DeleteDocUsagesToolStripMenuItem"
        Me.DeleteDocUsagesToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
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
    Friend WithEvents prgLoadProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents TestConnectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents statMsg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TestDuplicatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetDocIdToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteDocToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteDocUsagesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteDocSynopsesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteDocPartsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DocBatchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveByFileNameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveByIdToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveByLabelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveByIdToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem

End Class
