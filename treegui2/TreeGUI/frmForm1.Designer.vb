<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmForm1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmForm1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.miNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.miOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.miFolders = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenIndexFileOfThisFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowInWindowsExplorerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndexThisFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetVirtualFolderNameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFolderAndItsSubfoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFolderAndItsSubfoldersRecursiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cboSubFolderLevels = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RemoveThisFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveAllFolderEntriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServiceStartsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndexerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndexerToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.BrowseTasksFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.miToolsProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.AlwaysOnTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAllIndexFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupportForumsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReleaseNotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportBugsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckForUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sbarLeft = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sbarRight = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnIndexNow = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.lbFolders = New System.Windows.Forms.CheckedListBox()
        Me.niTray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmTray = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmShowApp = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmiStartIndexing = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.bwIndexer = New System.ComponentModel.BackgroundWorker()
        Me.tmrScheduleTime = New System.Windows.Forms.Timer(Me.components)
        Me.tmrScheduleInterval = New System.Windows.Forms.Timer(Me.components)
        Me.tmrUpdateProgressBar = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.cmTray.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.miFolders, Me.LogsToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(532, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miNew, Me.miOpen, Me.ToolStripSeparator5, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripSeparator4, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'miNew
        '
        Me.miNew.Image = CType(resources.GetObject("miNew.Image"), System.Drawing.Image)
        Me.miNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.miNew.Name = "miNew"
        Me.miNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.miNew.Size = New System.Drawing.Size(155, 22)
        Me.miNew.Text = "&New"
        '
        'miOpen
        '
        Me.miOpen.Image = CType(resources.GetObject("miOpen.Image"), System.Drawing.Image)
        Me.miOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.miOpen.Name = "miOpen"
        Me.miOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.miOpen.Size = New System.Drawing.Size(155, 22)
        Me.miOpen.Text = "&Open..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(152, 6)
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Image = CType(resources.GetObject("SaveToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save &As..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(152, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Image = CType(resources.GetObject("ExitToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Enabled = False
        Me.UndoToolStripMenuItem.Image = CType(resources.GetObject("UndoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        Me.UndoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.UndoToolStripMenuItem.Text = "&Undo"
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Enabled = False
        Me.RedoToolStripMenuItem.Image = CType(resources.GetObject("RedoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        Me.RedoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.RedoToolStripMenuItem.Text = "&Redo"
        '
        'miFolders
        '
        Me.miFolders.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenIndexFileOfThisFolderToolStripMenuItem, Me.ShowInWindowsExplorerToolStripMenuItem, Me.IndexThisFolderToolStripMenuItem, Me.SetVirtualFolderNameToolStripMenuItem, Me.ToolStripSeparator1, Me.AddFolderToolStripMenuItem, Me.AddFolderAndItsSubfoldersToolStripMenuItem, Me.AddFolderAndItsSubfoldersRecursiveToolStripMenuItem, Me.ToolStripSeparator2, Me.RemoveThisFolderToolStripMenuItem, Me.RemoveAllFolderEntriesToolStripMenuItem})
        Me.miFolders.Name = "miFolders"
        Me.miFolders.Size = New System.Drawing.Size(57, 20)
        Me.miFolders.Text = "&Folders"
        '
        'OpenIndexFileOfThisFolderToolStripMenuItem
        '
        Me.OpenIndexFileOfThisFolderToolStripMenuItem.Image = CType(resources.GetObject("OpenIndexFileOfThisFolderToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenIndexFileOfThisFolderToolStripMenuItem.Name = "OpenIndexFileOfThisFolderToolStripMenuItem"
        Me.OpenIndexFileOfThisFolderToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenIndexFileOfThisFolderToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.OpenIndexFileOfThisFolderToolStripMenuItem.Text = "&Open Index File of this Folder..."
        '
        'ShowInWindowsExplorerToolStripMenuItem
        '
        Me.ShowInWindowsExplorerToolStripMenuItem.Image = CType(resources.GetObject("ShowInWindowsExplorerToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowInWindowsExplorerToolStripMenuItem.Name = "ShowInWindowsExplorerToolStripMenuItem"
        Me.ShowInWindowsExplorerToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.ShowInWindowsExplorerToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.ShowInWindowsExplorerToolStripMenuItem.Text = "Show in Windows &Explorer..."
        '
        'IndexThisFolderToolStripMenuItem
        '
        Me.IndexThisFolderToolStripMenuItem.Name = "IndexThisFolderToolStripMenuItem"
        Me.IndexThisFolderToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.IndexThisFolderToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.IndexThisFolderToolStripMenuItem.Text = "&Index Checked Folders"
        '
        'SetVirtualFolderNameToolStripMenuItem
        '
        Me.SetVirtualFolderNameToolStripMenuItem.Name = "SetVirtualFolderNameToolStripMenuItem"
        Me.SetVirtualFolderNameToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.SetVirtualFolderNameToolStripMenuItem.Text = "Set &Virtual Folder Name..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(308, 6)
        '
        'AddFolderToolStripMenuItem
        '
        Me.AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem"
        Me.AddFolderToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AddFolderToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.AddFolderToolStripMenuItem.Text = "&Add Folder..."
        '
        'AddFolderAndItsSubfoldersToolStripMenuItem
        '
        Me.AddFolderAndItsSubfoldersToolStripMenuItem.Name = "AddFolderAndItsSubfoldersToolStripMenuItem"
        Me.AddFolderAndItsSubfoldersToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.AddFolderAndItsSubfoldersToolStripMenuItem.Text = "Add Folder and its &Subfolders..."
        '
        'AddFolderAndItsSubfoldersRecursiveToolStripMenuItem
        '
        Me.AddFolderAndItsSubfoldersRecursiveToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cboSubFolderLevels})
        Me.AddFolderAndItsSubfoldersRecursiveToolStripMenuItem.Name = "AddFolderAndItsSubfoldersRecursiveToolStripMenuItem"
        Me.AddFolderAndItsSubfoldersRecursiveToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.AddFolderAndItsSubfoldersRecursiveToolStripMenuItem.Text = "Add Folder and its Subfolders (&Recursive)..."
        '
        'cboSubFolderLevels
        '
        Me.cboSubFolderLevels.Name = "cboSubFolderLevels"
        Me.cboSubFolderLevels.Size = New System.Drawing.Size(121, 23)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(308, 6)
        '
        'RemoveThisFolderToolStripMenuItem
        '
        Me.RemoveThisFolderToolStripMenuItem.Image = CType(resources.GetObject("RemoveThisFolderToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RemoveThisFolderToolStripMenuItem.Name = "RemoveThisFolderToolStripMenuItem"
        Me.RemoveThisFolderToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RemoveThisFolderToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.RemoveThisFolderToolStripMenuItem.Text = "&Remove this Folder Entry"
        '
        'RemoveAllFolderEntriesToolStripMenuItem
        '
        Me.RemoveAllFolderEntriesToolStripMenuItem.Image = CType(resources.GetObject("RemoveAllFolderEntriesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RemoveAllFolderEntriesToolStripMenuItem.Name = "RemoveAllFolderEntriesToolStripMenuItem"
        Me.RemoveAllFolderEntriesToolStripMenuItem.Size = New System.Drawing.Size(311, 22)
        Me.RemoveAllFolderEntriesToolStripMenuItem.Text = "Remove All Folder Entries"
        '
        'LogsToolStripMenuItem
        '
        Me.LogsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServiceStartsToolStripMenuItem, Me.IndexerToolStripMenuItem, Me.IndexerToolStripMenuItem1, Me.DebugToolStripMenuItem, Me.ToolStripSeparator8, Me.BrowseTasksFolderToolStripMenuItem})
        Me.LogsToolStripMenuItem.Name = "LogsToolStripMenuItem"
        Me.LogsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.LogsToolStripMenuItem.Text = "&Logs"
        '
        'ServiceStartsToolStripMenuItem
        '
        Me.ServiceStartsToolStripMenuItem.Name = "ServiceStartsToolStripMenuItem"
        Me.ServiceStartsToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ServiceStartsToolStripMenuItem.Text = "&Service Startup..."
        '
        'IndexerToolStripMenuItem
        '
        Me.IndexerToolStripMenuItem.Name = "IndexerToolStripMenuItem"
        Me.IndexerToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.IndexerToolStripMenuItem.Text = "Settings &Reader..."
        '
        'IndexerToolStripMenuItem1
        '
        Me.IndexerToolStripMenuItem1.Name = "IndexerToolStripMenuItem1"
        Me.IndexerToolStripMenuItem1.Size = New System.Drawing.Size(188, 22)
        Me.IndexerToolStripMenuItem1.Text = "&Indexer..."
        '
        'DebugToolStripMenuItem
        '
        Me.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem"
        Me.DebugToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.DebugToolStripMenuItem.Text = "&Debug..."
        Me.DebugToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(185, 6)
        '
        'BrowseTasksFolderToolStripMenuItem
        '
        Me.BrowseTasksFolderToolStripMenuItem.Name = "BrowseTasksFolderToolStripMenuItem"
        Me.BrowseTasksFolderToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.BrowseTasksFolderToolStripMenuItem.Text = "Browse &Tasks Folder..."
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem, Me.miToolsProperties, Me.ToolStripSeparator7, Me.AlwaysOnTopToolStripMenuItem, Me.DeleteAllIndexFilesToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Image = CType(resources.GetObject("OptionsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.OptionsToolStripMenuItem.Text = "&Options..."
        '
        'miToolsProperties
        '
        Me.miToolsProperties.Name = "miToolsProperties"
        Me.miToolsProperties.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.miToolsProperties.Size = New System.Drawing.Size(190, 22)
        Me.miToolsProperties.Text = "&Properties..."
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(187, 6)
        '
        'AlwaysOnTopToolStripMenuItem
        '
        Me.AlwaysOnTopToolStripMenuItem.Name = "AlwaysOnTopToolStripMenuItem"
        Me.AlwaysOnTopToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.AlwaysOnTopToolStripMenuItem.Text = "Always On &Top"
        '
        'DeleteAllIndexFilesToolStripMenuItem
        '
        Me.DeleteAllIndexFilesToolStripMenuItem.Name = "DeleteAllIndexFilesToolStripMenuItem"
        Me.DeleteAllIndexFilesToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.DeleteAllIndexFilesToolStripMenuItem.Text = "&Delete All Index Files..."
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupportForumsToolStripMenuItem, Me.ReleaseNotesToolStripMenuItem, Me.VersionHistoryToolStripMenuItem, Me.ReportBugsToolStripMenuItem, Me.CheckForUpdateToolStripMenuItem, Me.ToolStripSeparator3, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'SupportForumsToolStripMenuItem
        '
        Me.SupportForumsToolStripMenuItem.Name = "SupportForumsToolStripMenuItem"
        Me.SupportForumsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SupportForumsToolStripMenuItem.Text = "Support &Forums..."
        '
        'ReleaseNotesToolStripMenuItem
        '
        Me.ReleaseNotesToolStripMenuItem.Name = "ReleaseNotesToolStripMenuItem"
        Me.ReleaseNotesToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ReleaseNotesToolStripMenuItem.Text = "Release &Notes..."
        '
        'VersionHistoryToolStripMenuItem
        '
        Me.VersionHistoryToolStripMenuItem.Name = "VersionHistoryToolStripMenuItem"
        Me.VersionHistoryToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.VersionHistoryToolStripMenuItem.Text = "&Version History..."
        '
        'ReportBugsToolStripMenuItem
        '
        Me.ReportBugsToolStripMenuItem.Name = "ReportBugsToolStripMenuItem"
        Me.ReportBugsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ReportBugsToolStripMenuItem.Text = "&Report Bugs..."
        '
        'CheckForUpdateToolStripMenuItem
        '
        Me.CheckForUpdateToolStripMenuItem.Name = "CheckForUpdateToolStripMenuItem"
        Me.CheckForUpdateToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CheckForUpdateToolStripMenuItem.Text = "&Check for Updates..."
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(177, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AboutToolStripMenuItem.Text = "&About..."
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbarLeft, Me.sbarRight, Me.pbBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 394)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(532, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'sbarLeft
        '
        Me.sbarLeft.Image = CType(resources.GetObject("sbarLeft.Image"), System.Drawing.Image)
        Me.sbarLeft.Name = "sbarLeft"
        Me.sbarLeft.Size = New System.Drawing.Size(65, 17)
        Me.sbarLeft.Text = "sbarLeft"
        '
        'sbarRight
        '
        Me.sbarRight.Image = CType(resources.GetObject("sbarRight.Image"), System.Drawing.Image)
        Me.sbarRight.Name = "sbarRight"
        Me.sbarRight.Size = New System.Drawing.Size(350, 17)
        Me.sbarRight.Spring = True
        Me.sbarRight.Text = "sbarRight"
        '
        'pbBar
        '
        Me.pbBar.Name = "pbBar"
        Me.pbBar.Size = New System.Drawing.Size(100, 16)
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(12, 356)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "&Add..."
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.Location = New System.Drawing.Point(93, 356)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnRemove.TabIndex = 4
        Me.btnRemove.Text = "&Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnIndexNow
        '
        Me.btnIndexNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnIndexNow.Location = New System.Drawing.Point(174, 356)
        Me.btnIndexNow.Name = "btnIndexNow"
        Me.btnIndexNow.Size = New System.Drawing.Size(75, 23)
        Me.btnIndexNow.TabIndex = 5
        Me.btnIndexNow.Text = "&Index Now"
        Me.btnIndexNow.UseVisualStyleBackColor = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMoveDown.Location = New System.Drawing.Point(445, 356)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveDown.TabIndex = 6
        Me.btnMoveDown.Text = "&Move Down"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMoveUp.Location = New System.Drawing.Point(364, 356)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveUp.TabIndex = 7
        Me.btnMoveUp.Text = "&Move Up"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'lbFolders
        '
        Me.lbFolders.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbFolders.FormattingEnabled = True
        Me.lbFolders.Location = New System.Drawing.Point(12, 42)
        Me.lbFolders.Name = "lbFolders"
        Me.lbFolders.Size = New System.Drawing.Size(508, 304)
        Me.lbFolders.TabIndex = 8
        '
        'niTray
        '
        Me.niTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.niTray.ContextMenuStrip = Me.cmTray
        Me.niTray.Icon = CType(resources.GetObject("niTray.Icon"), System.Drawing.Icon)
        Me.niTray.Text = "NotifyIcon1"
        '
        'cmTray
        '
        Me.cmTray.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmShowApp, Me.cmiStartIndexing, Me.ToolStripSeparator6, Me.ExitToolStripMenuItem1})
        Me.cmTray.Name = "cmTray"
        Me.cmTray.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.cmTray.Size = New System.Drawing.Size(177, 76)
        '
        'cmShowApp
        '
        Me.cmShowApp.Name = "cmShowApp"
        Me.cmShowApp.Size = New System.Drawing.Size(176, 22)
        Me.cmShowApp.Text = "&Show Application..."
        '
        'cmiStartIndexing
        '
        Me.cmiStartIndexing.Name = "cmiStartIndexing"
        Me.cmiStartIndexing.Size = New System.Drawing.Size(176, 22)
        Me.cmiStartIndexing.Text = "Start &Indexing"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(173, 6)
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(176, 22)
        Me.ExitToolStripMenuItem1.Text = "E&xit"
        '
        'bwIndexer
        '
        Me.bwIndexer.WorkerReportsProgress = True
        Me.bwIndexer.WorkerSupportsCancellation = True
        '
        'tmrScheduleTime
        '
        Me.tmrScheduleTime.Enabled = True
        Me.tmrScheduleTime.Interval = 1000
        '
        'tmrScheduleInterval
        '
        Me.tmrScheduleInterval.Interval = 300000
        '
        'tmrUpdateProgressBar
        '
        '
        'frmForm1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 416)
        Me.Controls.Add(Me.lbFolders)
        Me.Controls.Add(Me.btnMoveUp)
        Me.Controls.Add(Me.btnMoveDown)
        Me.Controls.Add(Me.btnIndexNow)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(480, 400)
        Me.Name = "frmForm1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TreeGUI"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.cmTray.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sbarLeft As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnIndexNow As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents sbarRight As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents miFolders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowInWindowsExplorerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenIndexFileOfThisFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexThisFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveThisFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbFolders As System.Windows.Forms.CheckedListBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AddFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFolderAndItsSubfoldersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFolderAndItsSubfoldersRecursiveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RemoveAllFolderEntriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckForUpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportBugsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReleaseNotesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SupportForumsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionHistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents niTray As System.Windows.Forms.NotifyIcon
    Friend WithEvents miNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents miOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlwaysOnTopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Friend WithEvents bwIndexer As System.ComponentModel.BackgroundWorker
    Friend WithEvents cmTray As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmShowApp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmiStartIndexing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrScheduleTime As System.Windows.Forms.Timer
    Friend WithEvents tmrScheduleInterval As System.Windows.Forms.Timer
    Friend WithEvents miToolsProperties As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmrUpdateProgressBar As System.Windows.Forms.Timer
    Friend WithEvents SetVirtualFolderNameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteAllIndexFilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ServiceStartsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexerToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboSubFolderLevels As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BrowseTasksFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DebugToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
