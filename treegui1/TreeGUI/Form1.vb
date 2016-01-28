Imports System.IO

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        Dim vs As New ArrayList
        vs = Me.getSettingsFromXmlToArrayList("configuration/options/XPStyle")
        If vs.Count > 0 Then
            If vs(0) = "True" Then
                Application.EnableVisualStyles()
            End If
        Else
            Application.EnableVisualStyles()
        End If
        Application.DoEvents()
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents chkIndexFileInEachDir As System.Windows.Forms.CheckBox
    Friend WithEvents chkIndexFileInOneDir As System.Windows.Forms.CheckBox
    Friend WithEvents txtOutputDir As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseOutputDir As System.Windows.Forms.Button
    Friend WithEvents btnIndexNow As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents chkAscii As System.Windows.Forms.CheckBox
    Friend WithEvents chkIndexFiles As System.Windows.Forms.CheckBox
    Friend WithEvents pbBar As System.Windows.Forms.ProgressBar
    Friend WithEvents gbTreeOptions As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents gbOutputOptions As System.Windows.Forms.GroupBox
    Friend WithEvents gbFolders As System.Windows.Forms.GroupBox
    Friend WithEvents cboExt As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents chkSingleFile As System.Windows.Forms.CheckBox
    Friend WithEvents chkWindowsService As System.Windows.Forms.CheckBox
    Friend WithEvents chkTrayOnlyWhenLoad As System.Windows.Forms.CheckBox
    Friend WithEvents chkStartupItem As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents nudInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkVisualStyles As System.Windows.Forms.CheckBox
    Friend WithEvents niTray As System.Windows.Forms.NotifyIcon
    Friend WithEvents cmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents cmiShowApp As System.Windows.Forms.MenuItem
    Friend WithEvents cmiSep1 As System.Windows.Forms.MenuItem
    Friend WithEvents cmiIndexNow As System.Windows.Forms.MenuItem
    Friend WithEvents sbar As System.Windows.Forms.StatusBar
    Friend WithEvents sbarLeft As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbarRight As System.Windows.Forms.StatusBarPanel
    Friend WithEvents miFoldersBrowse As System.Windows.Forms.MenuItem
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents lbFolders As System.Windows.Forms.ListBox
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents chkRunSchedule As System.Windows.Forms.CheckBox
    Friend WithEvents chkTrayIcon As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents tmrInterval As System.Windows.Forms.Timer
    Friend WithEvents cmMenuForm As System.Windows.Forms.ContextMenu
    Friend WithEvents cmAbout As System.Windows.Forms.MenuItem
    Friend WithEvents cmCheckUpdates As System.Windows.Forms.MenuItem
    Friend WithEvents miVersionHistory As System.Windows.Forms.MenuItem
    Friend WithEvents miReportBugs As System.Windows.Forms.MenuItem
    Friend WithEvents miSep1 As System.Windows.Forms.MenuItem
    Friend WithEvents miSupportForums As System.Windows.Forms.MenuItem
    Friend WithEvents cmMenuForListBox As System.Windows.Forms.ContextMenu
    Friend WithEvents cmMenuForTray As System.Windows.Forms.ContextMenu
    Friend WithEvents chkRemoveBranches As System.Windows.Forms.CheckBox
    Friend WithEvents chkTreeGuiConfigFileReg As System.Windows.Forms.CheckBox
    Friend WithEvents cmFoldersRemove As System.Windows.Forms.MenuItem
    Friend WithEvents miOpenIndexFile As System.Windows.Forms.MenuItem
    Friend WithEvents cmMenuAdd As System.Windows.Forms.ContextMenu
    Friend WithEvents miAddFolder As System.Windows.Forms.MenuItem
    Friend WithEvents miAddSubfolders As System.Windows.Forms.MenuItem
    Friend WithEvents miAddSubfoldersRecursive As System.Windows.Forms.MenuItem
    Friend WithEvents cmMenuRemove As System.Windows.Forms.ContextMenu
    Friend WithEvents miRemove As System.Windows.Forms.MenuItem
    Friend WithEvents miRemoveAll As System.Windows.Forms.MenuItem
    Friend WithEvents chkCloseToTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkDontShowInTaskbar As System.Windows.Forms.CheckBox
    Friend WithEvents cmExit As System.Windows.Forms.MenuItem
    Friend WithEvents miReleaseNotes As System.Windows.Forms.MenuItem
    Friend WithEvents miSetDefaultConfig As System.Windows.Forms.MenuItem
    Friend WithEvents miSep2 As System.Windows.Forms.MenuItem
    Friend WithEvents gbLoadWithWindows As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents btnAssoTgcExt As System.Windows.Forms.Button
    Friend WithEvents btnUninstallWindowsService As System.Windows.Forms.Button
    Friend WithEvents miIndexThisFolder As System.Windows.Forms.MenuItem
    Friend WithEvents gbFrequency As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents dtpNow As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSchedule As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.btnAdd = New System.Windows.Forms.Button
        Me.cmMenuAdd = New System.Windows.Forms.ContextMenu
        Me.miAddFolder = New System.Windows.Forms.MenuItem
        Me.miAddSubfolders = New System.Windows.Forms.MenuItem
        Me.miAddSubfoldersRecursive = New System.Windows.Forms.MenuItem
        Me.btnIndexNow = New System.Windows.Forms.Button
        Me.chkIndexFileInEachDir = New System.Windows.Forms.CheckBox
        Me.chkIndexFileInOneDir = New System.Windows.Forms.CheckBox
        Me.txtOutputDir = New System.Windows.Forms.TextBox
        Me.btnBrowseOutputDir = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnRemove = New System.Windows.Forms.Button
        Me.cmMenuRemove = New System.Windows.Forms.ContextMenu
        Me.miRemove = New System.Windows.Forms.MenuItem
        Me.miRemoveAll = New System.Windows.Forms.MenuItem
        Me.gbFolders = New System.Windows.Forms.GroupBox
        Me.btnMoveDown = New System.Windows.Forms.Button
        Me.btnMoveUp = New System.Windows.Forms.Button
        Me.lbFolders = New System.Windows.Forms.ListBox
        Me.cmMenuForListBox = New System.Windows.Forms.ContextMenu
        Me.miFoldersBrowse = New System.Windows.Forms.MenuItem
        Me.miOpenIndexFile = New System.Windows.Forms.MenuItem
        Me.miIndexThisFolder = New System.Windows.Forms.MenuItem
        Me.cmFoldersRemove = New System.Windows.Forms.MenuItem
        Me.pbBar = New System.Windows.Forms.ProgressBar
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.gbLoadWithWindows = New System.Windows.Forms.GroupBox
        Me.chkWindowsService = New System.Windows.Forms.CheckBox
        Me.chkStartupItem = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkVisualStyles = New System.Windows.Forms.CheckBox
        Me.chkRunSchedule = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkCloseToTray = New System.Windows.Forms.CheckBox
        Me.chkTrayOnlyWhenLoad = New System.Windows.Forms.CheckBox
        Me.chkTrayIcon = New System.Windows.Forms.CheckBox
        Me.chkDontShowInTaskbar = New System.Windows.Forms.CheckBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cboExt = New System.Windows.Forms.ComboBox
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.gbOutputOptions = New System.Windows.Forms.GroupBox
        Me.chkSingleFile = New System.Windows.Forms.CheckBox
        Me.gbTreeOptions = New System.Windows.Forms.GroupBox
        Me.chkRemoveBranches = New System.Windows.Forms.CheckBox
        Me.chkAscii = New System.Windows.Forms.CheckBox
        Me.chkIndexFiles = New System.Windows.Forms.CheckBox
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.gbFrequency = New System.Windows.Forms.GroupBox
        Me.CheckBox7 = New System.Windows.Forms.CheckBox
        Me.CheckBox6 = New System.Windows.Forms.CheckBox
        Me.CheckBox5 = New System.Windows.Forms.CheckBox
        Me.CheckBox4 = New System.Windows.Forms.CheckBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.CheckBox8 = New System.Windows.Forms.CheckBox
        Me.CheckBox9 = New System.Windows.Forms.CheckBox
        Me.dtpNow = New System.Windows.Forms.DateTimePicker
        Me.dtpSchedule = New System.Windows.Forms.DateTimePicker
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.nudInterval = New System.Windows.Forms.NumericUpDown
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpTime = New System.Windows.Forms.DateTimePicker
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.btnUninstallWindowsService = New System.Windows.Forms.Button
        Me.btnAssoTgcExt = New System.Windows.Forms.Button
        Me.tmrInterval = New System.Windows.Forms.Timer(Me.components)
        Me.niTray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmMenuForTray = New System.Windows.Forms.ContextMenu
        Me.cmiShowApp = New System.Windows.Forms.MenuItem
        Me.cmiIndexNow = New System.Windows.Forms.MenuItem
        Me.cmiSep1 = New System.Windows.Forms.MenuItem
        Me.cmiExit = New System.Windows.Forms.MenuItem
        Me.sbar = New System.Windows.Forms.StatusBar
        Me.sbarLeft = New System.Windows.Forms.StatusBarPanel
        Me.sbarRight = New System.Windows.Forms.StatusBarPanel
        Me.cmMenuForm = New System.Windows.Forms.ContextMenu
        Me.miSetDefaultConfig = New System.Windows.Forms.MenuItem
        Me.miSep2 = New System.Windows.Forms.MenuItem
        Me.cmAbout = New System.Windows.Forms.MenuItem
        Me.cmCheckUpdates = New System.Windows.Forms.MenuItem
        Me.miReportBugs = New System.Windows.Forms.MenuItem
        Me.miReleaseNotes = New System.Windows.Forms.MenuItem
        Me.miSupportForums = New System.Windows.Forms.MenuItem
        Me.miVersionHistory = New System.Windows.Forms.MenuItem
        Me.miSep1 = New System.Windows.Forms.MenuItem
        Me.cmExit = New System.Windows.Forms.MenuItem
        Me.chkTreeGuiConfigFileReg = New System.Windows.Forms.CheckBox
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.gbFolders.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.gbLoadWithWindows.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gbOutputOptions.SuspendLayout()
        Me.gbTreeOptions.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.gbFrequency.SuspendLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.sbarLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbarRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAdd
        '
        Me.btnAdd.ContextMenu = Me.cmMenuAdd
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnAdd.Location = New System.Drawing.Point(16, 256)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(136, 23)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "&Add"
        '
        'cmMenuAdd
        '
        Me.cmMenuAdd.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAddFolder, Me.miAddSubfolders, Me.miAddSubfoldersRecursive})
        '
        'miAddFolder
        '
        Me.miAddFolder.Index = 0
        Me.miAddFolder.Text = "Add &Folder..."
        '
        'miAddSubfolders
        '
        Me.miAddSubfolders.Index = 1
        Me.miAddSubfolders.Text = "Add Folder and its &Subfolders..."
        '
        'miAddSubfoldersRecursive
        '
        Me.miAddSubfoldersRecursive.Index = 2
        Me.miAddSubfoldersRecursive.Text = "Add Folder and its Subfolders (&Recursive)..."
        '
        'btnIndexNow
        '
        Me.btnIndexNow.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnIndexNow.Location = New System.Drawing.Point(320, 256)
        Me.btnIndexNow.Name = "btnIndexNow"
        Me.btnIndexNow.Size = New System.Drawing.Size(128, 23)
        Me.btnIndexNow.TabIndex = 2
        Me.btnIndexNow.Text = "&Index Now"
        '
        'chkIndexFileInEachDir
        '
        Me.chkIndexFileInEachDir.Checked = True
        Me.chkIndexFileInEachDir.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIndexFileInEachDir.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkIndexFileInEachDir.Location = New System.Drawing.Point(16, 24)
        Me.chkIndexFileInEachDir.Name = "chkIndexFileInEachDir"
        Me.chkIndexFileInEachDir.Size = New System.Drawing.Size(400, 16)
        Me.chkIndexFileInEachDir.TabIndex = 3
        Me.chkIndexFileInEachDir.Text = "Create index in the same directory"
        '
        'chkIndexFileInOneDir
        '
        Me.chkIndexFileInOneDir.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkIndexFileInOneDir.Location = New System.Drawing.Point(16, 48)
        Me.chkIndexFileInOneDir.Name = "chkIndexFileInOneDir"
        Me.chkIndexFileInOneDir.Size = New System.Drawing.Size(208, 24)
        Me.chkIndexFileInOneDir.TabIndex = 4
        Me.chkIndexFileInOneDir.Text = "Create index in the following directory"
        '
        'txtOutputDir
        '
        Me.txtOutputDir.Location = New System.Drawing.Point(16, 80)
        Me.txtOutputDir.Name = "txtOutputDir"
        Me.txtOutputDir.ReadOnly = True
        Me.txtOutputDir.Size = New System.Drawing.Size(352, 20)
        Me.txtOutputDir.TabIndex = 5
        Me.txtOutputDir.Text = ""
        '
        'btnBrowseOutputDir
        '
        Me.btnBrowseOutputDir.Enabled = False
        Me.btnBrowseOutputDir.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnBrowseOutputDir.Location = New System.Drawing.Point(376, 80)
        Me.btnBrowseOutputDir.Name = "btnBrowseOutputDir"
        Me.btnBrowseOutputDir.Size = New System.Drawing.Size(32, 23)
        Me.btnBrowseOutputDir.TabIndex = 6
        Me.btnBrowseOutputDir.Text = "..."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(17, 16)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(471, 352)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnRemove)
        Me.TabPage1.Controls.Add(Me.gbFolders)
        Me.TabPage1.Controls.Add(Me.pbBar)
        Me.TabPage1.Controls.Add(Me.btnIndexNow)
        Me.TabPage1.Controls.Add(Me.btnAdd)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(463, 326)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Folders"
        '
        'btnRemove
        '
        Me.btnRemove.ContextMenu = Me.cmMenuRemove
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnRemove.Location = New System.Drawing.Point(168, 256)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(136, 23)
        Me.btnRemove.TabIndex = 1
        Me.btnRemove.Text = "&Remove"
        '
        'cmMenuRemove
        '
        Me.cmMenuRemove.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miRemove, Me.miRemoveAll})
        '
        'miRemove
        '
        Me.miRemove.Index = 0
        Me.miRemove.Text = "&Remove"
        '
        'miRemoveAll
        '
        Me.miRemoveAll.Index = 1
        Me.miRemoveAll.Text = "Remove &All"
        '
        'gbFolders
        '
        Me.gbFolders.Controls.Add(Me.btnMoveDown)
        Me.gbFolders.Controls.Add(Me.btnMoveUp)
        Me.gbFolders.Controls.Add(Me.lbFolders)
        Me.gbFolders.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gbFolders.Location = New System.Drawing.Point(16, 16)
        Me.gbFolders.Name = "gbFolders"
        Me.gbFolders.Size = New System.Drawing.Size(432, 232)
        Me.gbFolders.TabIndex = 5
        Me.gbFolders.TabStop = False
        Me.gbFolders.Text = "Folders to Index"
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Enabled = False
        Me.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnMoveDown.Font = New System.Drawing.Font("Wingdings 3", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnMoveDown.Location = New System.Drawing.Point(392, 112)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(32, 32)
        Me.btnMoveDown.TabIndex = 5
        Me.btnMoveDown.Text = "È"
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Enabled = False
        Me.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnMoveUp.Font = New System.Drawing.Font("Wingdings 3", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnMoveUp.Location = New System.Drawing.Point(392, 72)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(32, 32)
        Me.btnMoveUp.TabIndex = 4
        Me.btnMoveUp.Text = "Ç"
        '
        'lbFolders
        '
        Me.lbFolders.ContextMenu = Me.cmMenuForListBox
        Me.lbFolders.HorizontalScrollbar = True
        Me.lbFolders.Location = New System.Drawing.Point(16, 24)
        Me.lbFolders.Name = "lbFolders"
        Me.lbFolders.Size = New System.Drawing.Size(368, 186)
        Me.lbFolders.TabIndex = 3
        '
        'cmMenuForListBox
        '
        Me.cmMenuForListBox.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miFoldersBrowse, Me.miOpenIndexFile, Me.miIndexThisFolder, Me.cmFoldersRemove})
        '
        'miFoldersBrowse
        '
        Me.miFoldersBrowse.DefaultItem = True
        Me.miFoldersBrowse.Enabled = False
        Me.miFoldersBrowse.Index = 0
        Me.miFoldersBrowse.Shortcut = System.Windows.Forms.Shortcut.CtrlE
        Me.miFoldersBrowse.Text = "Show in Windows &Explorer..."
        '
        'miOpenIndexFile
        '
        Me.miOpenIndexFile.Index = 1
        Me.miOpenIndexFile.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.miOpenIndexFile.Text = "&Open Index File in this folder..."
        '
        'miIndexThisFolder
        '
        Me.miIndexThisFolder.Index = 2
        Me.miIndexThisFolder.Shortcut = System.Windows.Forms.Shortcut.CtrlG
        Me.miIndexThisFolder.Text = "&Index this folder"
        '
        'cmFoldersRemove
        '
        Me.cmFoldersRemove.Index = 3
        Me.cmFoldersRemove.Shortcut = System.Windows.Forms.Shortcut.CtrlR
        Me.cmFoldersRemove.Text = "&Remove this folder"
        '
        'pbBar
        '
        Me.pbBar.Location = New System.Drawing.Point(16, 288)
        Me.pbBar.Name = "pbBar"
        Me.pbBar.Size = New System.Drawing.Size(432, 23)
        Me.pbBar.TabIndex = 4
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.gbLoadWithWindows)
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Controls.Add(Me.GroupBox1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(463, 326)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Options"
        '
        'gbLoadWithWindows
        '
        Me.gbLoadWithWindows.Controls.Add(Me.chkWindowsService)
        Me.gbLoadWithWindows.Controls.Add(Me.chkStartupItem)
        Me.gbLoadWithWindows.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gbLoadWithWindows.Location = New System.Drawing.Point(16, 16)
        Me.gbLoadWithWindows.Name = "gbLoadWithWindows"
        Me.gbLoadWithWindows.Size = New System.Drawing.Size(432, 80)
        Me.gbLoadWithWindows.TabIndex = 18
        Me.gbLoadWithWindows.TabStop = False
        Me.gbLoadWithWindows.Text = "Load with Windows"
        '
        'chkWindowsService
        '
        Me.chkWindowsService.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkWindowsService.Location = New System.Drawing.Point(16, 48)
        Me.chkWindowsService.Name = "chkWindowsService"
        Me.chkWindowsService.Size = New System.Drawing.Size(168, 16)
        Me.chkWindowsService.TabIndex = 1
        Me.chkWindowsService.Text = "Run as a &Windows Service"
        '
        'chkStartupItem
        '
        Me.chkStartupItem.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkStartupItem.Location = New System.Drawing.Point(16, 24)
        Me.chkStartupItem.Name = "chkStartupItem"
        Me.chkStartupItem.Size = New System.Drawing.Size(200, 16)
        Me.chkStartupItem.TabIndex = 2
        Me.chkStartupItem.Text = "Run as a &Startup Item"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkVisualStyles)
        Me.GroupBox3.Controls.Add(Me.chkRunSchedule)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox3.Location = New System.Drawing.Point(16, 104)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(432, 80)
        Me.GroupBox3.TabIndex = 17
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Graphical User Interface"
        '
        'chkVisualStyles
        '
        Me.chkVisualStyles.Checked = True
        Me.chkVisualStyles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVisualStyles.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkVisualStyles.Location = New System.Drawing.Point(16, 48)
        Me.chkVisualStyles.Name = "chkVisualStyles"
        Me.chkVisualStyles.Size = New System.Drawing.Size(160, 16)
        Me.chkVisualStyles.TabIndex = 11
        Me.chkVisualStyles.Text = "Enable &XP Visual Styles"
        '
        'chkRunSchedule
        '
        Me.chkRunSchedule.Enabled = False
        Me.chkRunSchedule.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkRunSchedule.Location = New System.Drawing.Point(16, 24)
        Me.chkRunSchedule.Name = "chkRunSchedule"
        Me.chkRunSchedule.Size = New System.Drawing.Size(200, 16)
        Me.chkRunSchedule.TabIndex = 12
        Me.chkRunSchedule.Text = "Run Scheduled &Tasks"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkCloseToTray)
        Me.GroupBox1.Controls.Add(Me.chkTrayOnlyWhenLoad)
        Me.GroupBox1.Controls.Add(Me.chkTrayIcon)
        Me.GroupBox1.Controls.Add(Me.chkDontShowInTaskbar)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(16, 192)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(432, 104)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "TreeGUI System Tray"
        '
        'chkCloseToTray
        '
        Me.chkCloseToTray.Enabled = False
        Me.chkCloseToTray.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkCloseToTray.Location = New System.Drawing.Point(248, 72)
        Me.chkCloseToTray.Name = "chkCloseToTray"
        Me.chkCloseToTray.Size = New System.Drawing.Size(160, 16)
        Me.chkCloseToTray.TabIndex = 16
        Me.chkCloseToTray.Text = "&Close to System Tray"
        '
        'chkTrayOnlyWhenLoad
        '
        Me.chkTrayOnlyWhenLoad.Enabled = False
        Me.chkTrayOnlyWhenLoad.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkTrayOnlyWhenLoad.Location = New System.Drawing.Point(16, 48)
        Me.chkTrayOnlyWhenLoad.Name = "chkTrayOnlyWhenLoad"
        Me.chkTrayOnlyWhenLoad.Size = New System.Drawing.Size(184, 16)
        Me.chkTrayOnlyWhenLoad.TabIndex = 3
        Me.chkTrayOnlyWhenLoad.Text = "Do not show TreeGUI on Load"
        '
        'chkTrayIcon
        '
        Me.chkTrayIcon.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkTrayIcon.Location = New System.Drawing.Point(16, 24)
        Me.chkTrayIcon.Name = "chkTrayIcon"
        Me.chkTrayIcon.Size = New System.Drawing.Size(184, 16)
        Me.chkTrayIcon.TabIndex = 14
        Me.chkTrayIcon.Text = "Enable System Tray Icon"
        '
        'chkDontShowInTaskbar
        '
        Me.chkDontShowInTaskbar.Enabled = False
        Me.chkDontShowInTaskbar.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkDontShowInTaskbar.Location = New System.Drawing.Point(16, 72)
        Me.chkDontShowInTaskbar.Name = "chkDontShowInTaskbar"
        Me.chkDontShowInTaskbar.Size = New System.Drawing.Size(160, 16)
        Me.chkDontShowInTaskbar.TabIndex = 15
        Me.chkDontShowInTaskbar.Text = "&Minimize to System Tray"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.gbOutputOptions)
        Me.TabPage2.Controls.Add(Me.gbTreeOptions)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(463, 326)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Configuration"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboExt)
        Me.GroupBox2.Controls.Add(Me.txtFileName)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(16, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(432, 56)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Index File Name"
        '
        'cboExt
        '
        Me.cboExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExt.Items.AddRange(New Object() {".txt", ".log", ".wri", ".rtf", ".doc"})
        Me.cboExt.Location = New System.Drawing.Point(344, 24)
        Me.cboExt.Name = "cboExt"
        Me.cboExt.Size = New System.Drawing.Size(72, 21)
        Me.cboExt.TabIndex = 8
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(16, 24)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(312, 20)
        Me.txtFileName.TabIndex = 7
        Me.txtFileName.Text = "index"
        '
        'gbOutputOptions
        '
        Me.gbOutputOptions.Controls.Add(Me.chkSingleFile)
        Me.gbOutputOptions.Controls.Add(Me.chkIndexFileInOneDir)
        Me.gbOutputOptions.Controls.Add(Me.btnBrowseOutputDir)
        Me.gbOutputOptions.Controls.Add(Me.txtOutputDir)
        Me.gbOutputOptions.Controls.Add(Me.chkIndexFileInEachDir)
        Me.gbOutputOptions.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gbOutputOptions.Location = New System.Drawing.Point(16, 80)
        Me.gbOutputOptions.Name = "gbOutputOptions"
        Me.gbOutputOptions.Size = New System.Drawing.Size(432, 120)
        Me.gbOutputOptions.TabIndex = 11
        Me.gbOutputOptions.TabStop = False
        '
        'chkSingleFile
        '
        Me.chkSingleFile.Enabled = False
        Me.chkSingleFile.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkSingleFile.Location = New System.Drawing.Point(232, 48)
        Me.chkSingleFile.Name = "chkSingleFile"
        Me.chkSingleFile.Size = New System.Drawing.Size(184, 24)
        Me.chkSingleFile.TabIndex = 7
        Me.chkSingleFile.Text = "&Append to Single File"
        '
        'gbTreeOptions
        '
        Me.gbTreeOptions.Controls.Add(Me.chkRemoveBranches)
        Me.gbTreeOptions.Controls.Add(Me.chkAscii)
        Me.gbTreeOptions.Controls.Add(Me.chkIndexFiles)
        Me.gbTreeOptions.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gbTreeOptions.Location = New System.Drawing.Point(16, 208)
        Me.gbTreeOptions.Name = "gbTreeOptions"
        Me.gbTreeOptions.Size = New System.Drawing.Size(432, 104)
        Me.gbTreeOptions.TabIndex = 10
        Me.gbTreeOptions.TabStop = False
        Me.gbTreeOptions.Text = "Tree Options"
        '
        'chkRemoveBranches
        '
        Me.chkRemoveBranches.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkRemoveBranches.Location = New System.Drawing.Point(16, 72)
        Me.chkRemoveBranches.Name = "chkRemoveBranches"
        Me.chkRemoveBranches.Size = New System.Drawing.Size(384, 16)
        Me.chkRemoveBranches.TabIndex = 10
        Me.chkRemoveBranches.Text = "&Remove Tree Branches"
        '
        'chkAscii
        '
        Me.chkAscii.Checked = True
        Me.chkAscii.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAscii.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkAscii.Location = New System.Drawing.Point(16, 48)
        Me.chkAscii.Name = "chkAscii"
        Me.chkAscii.Size = New System.Drawing.Size(384, 16)
        Me.chkAscii.TabIndex = 8
        Me.chkAscii.Text = " Use ASCII instead of extended characters"
        '
        'chkIndexFiles
        '
        Me.chkIndexFiles.Checked = True
        Me.chkIndexFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIndexFiles.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkIndexFiles.Location = New System.Drawing.Point(16, 24)
        Me.chkIndexFiles.Name = "chkIndexFiles"
        Me.chkIndexFiles.Size = New System.Drawing.Size(384, 16)
        Me.chkIndexFiles.TabIndex = 9
        Me.chkIndexFiles.Text = "Display the names of the files in each folder."
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.gbFrequency)
        Me.TabPage5.Controls.Add(Me.CheckBox1)
        Me.TabPage5.Controls.Add(Me.CheckBox2)
        Me.TabPage5.Controls.Add(Me.nudInterval)
        Me.TabPage5.Controls.Add(Me.Label2)
        Me.TabPage5.Controls.Add(Me.dtpTime)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(463, 326)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Schedule"
        '
        'gbFrequency
        '
        Me.gbFrequency.Controls.Add(Me.CheckBox7)
        Me.gbFrequency.Controls.Add(Me.CheckBox6)
        Me.gbFrequency.Controls.Add(Me.CheckBox5)
        Me.gbFrequency.Controls.Add(Me.CheckBox4)
        Me.gbFrequency.Controls.Add(Me.CheckBox3)
        Me.gbFrequency.Controls.Add(Me.CheckBox8)
        Me.gbFrequency.Controls.Add(Me.CheckBox9)
        Me.gbFrequency.Controls.Add(Me.dtpNow)
        Me.gbFrequency.Controls.Add(Me.dtpSchedule)
        Me.gbFrequency.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gbFrequency.Location = New System.Drawing.Point(16, 88)
        Me.gbFrequency.Name = "gbFrequency"
        Me.gbFrequency.Size = New System.Drawing.Size(424, 200)
        Me.gbFrequency.TabIndex = 12
        Me.gbFrequency.TabStop = False
        Me.gbFrequency.Text = "Schedule"
        Me.gbFrequency.Visible = False
        '
        'CheckBox7
        '
        Me.CheckBox7.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox7.Location = New System.Drawing.Point(128, 56)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.TabIndex = 16
        Me.CheckBox7.Text = "S&unday"
        '
        'CheckBox6
        '
        Me.CheckBox6.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox6.Location = New System.Drawing.Point(128, 24)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.TabIndex = 15
        Me.CheckBox6.Text = "&Saturday"
        '
        'CheckBox5
        '
        Me.CheckBox5.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox5.Location = New System.Drawing.Point(16, 152)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.TabIndex = 14
        Me.CheckBox5.Text = "&Friday"
        '
        'CheckBox4
        '
        Me.CheckBox4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox4.Location = New System.Drawing.Point(16, 120)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.TabIndex = 13
        Me.CheckBox4.Text = "T&hursday"
        '
        'CheckBox3
        '
        Me.CheckBox3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox3.Location = New System.Drawing.Point(16, 88)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.TabIndex = 12
        Me.CheckBox3.Text = "&Wednesday"
        '
        'CheckBox8
        '
        Me.CheckBox8.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox8.Location = New System.Drawing.Point(16, 56)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.TabIndex = 11
        Me.CheckBox8.Text = "&Tuesday"
        '
        'CheckBox9
        '
        Me.CheckBox9.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox9.Location = New System.Drawing.Point(16, 24)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.TabIndex = 10
        Me.CheckBox9.Text = "&Monday"
        '
        'dtpNow
        '
        Me.dtpNow.CustomFormat = "HHmmss"
        Me.dtpNow.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNow.Location = New System.Drawing.Point(232, 112)
        Me.dtpNow.Name = "dtpNow"
        Me.dtpNow.Size = New System.Drawing.Size(72, 20)
        Me.dtpNow.TabIndex = 19
        '
        'dtpSchedule
        '
        Me.dtpSchedule.CustomFormat = "HHmmss"
        Me.dtpSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSchedule.Location = New System.Drawing.Point(328, 112)
        Me.dtpSchedule.Name = "dtpSchedule"
        Me.dtpSchedule.Size = New System.Drawing.Size(72, 20)
        Me.dtpSchedule.TabIndex = 18
        '
        'CheckBox1
        '
        Me.CheckBox1.Location = New System.Drawing.Point(24, 16)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(96, 24)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "I&ndex every"
        '
        'CheckBox2
        '
        Me.CheckBox2.Location = New System.Drawing.Point(24, 48)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(168, 24)
        Me.CheckBox2.TabIndex = 8
        Me.CheckBox2.Text = "I&ndex at a specified time"
        '
        'nudInterval
        '
        Me.nudInterval.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudInterval.Location = New System.Drawing.Point(136, 16)
        Me.nudInterval.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.nudInterval.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudInterval.Name = "nudInterval"
        Me.nudInterval.ReadOnly = True
        Me.nudInterval.Size = New System.Drawing.Size(56, 20)
        Me.nudInterval.TabIndex = 5
        Me.nudInterval.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(200, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "minutes"
        '
        'dtpTime
        '
        Me.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpTime.Location = New System.Drawing.Point(208, 48)
        Me.dtpTime.Name = "dtpTime"
        Me.dtpTime.ShowUpDown = True
        Me.dtpTime.Size = New System.Drawing.Size(96, 20)
        Me.dtpTime.TabIndex = 17
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.btnUninstallWindowsService)
        Me.TabPage4.Controls.Add(Me.btnAssoTgcExt)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(463, 326)
        Me.TabPage4.TabIndex = 5
        Me.TabPage4.Text = "Advanced"
        '
        'btnUninstallWindowsService
        '
        Me.btnUninstallWindowsService.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnUninstallWindowsService.Location = New System.Drawing.Point(16, 48)
        Me.btnUninstallWindowsService.Name = "btnUninstallWindowsService"
        Me.btnUninstallWindowsService.Size = New System.Drawing.Size(232, 23)
        Me.btnUninstallWindowsService.TabIndex = 1
        Me.btnUninstallWindowsService.Text = "&Uninstall McoreIndexer Windows Service..."
        '
        'btnAssoTgcExt
        '
        Me.btnAssoTgcExt.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnAssoTgcExt.Location = New System.Drawing.Point(16, 16)
        Me.btnAssoTgcExt.Name = "btnAssoTgcExt"
        Me.btnAssoTgcExt.Size = New System.Drawing.Size(232, 23)
        Me.btnAssoTgcExt.TabIndex = 0
        Me.btnAssoTgcExt.Text = "Associate .tgc files with TreeGUI"
        '
        'tmrInterval
        '
        Me.tmrInterval.Interval = 300000
        '
        'niTray
        '
        Me.niTray.ContextMenu = Me.cmMenuForTray
        Me.niTray.Icon = CType(resources.GetObject("niTray.Icon"), System.Drawing.Icon)
        Me.niTray.Text = ""
        '
        'cmMenuForTray
        '
        Me.cmMenuForTray.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmiShowApp, Me.cmiIndexNow, Me.cmiSep1, Me.cmiExit})
        '
        'cmiShowApp
        '
        Me.cmiShowApp.DefaultItem = True
        Me.cmiShowApp.Index = 0
        Me.cmiShowApp.Text = "&Show"
        '
        'cmiIndexNow
        '
        Me.cmiIndexNow.Index = 1
        Me.cmiIndexNow.Text = "Start &Indexing"
        '
        'cmiSep1
        '
        Me.cmiSep1.Index = 2
        Me.cmiSep1.Text = "-"
        '
        'cmiExit
        '
        Me.cmiExit.Index = 3
        Me.cmiExit.Text = "E&xit"
        '
        'sbar
        '
        Me.sbar.Location = New System.Drawing.Point(0, 386)
        Me.sbar.Name = "sbar"
        Me.sbar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbarLeft, Me.sbarRight})
        Me.sbar.ShowPanels = True
        Me.sbar.Size = New System.Drawing.Size(506, 22)
        Me.sbar.SizingGrip = False
        Me.sbar.TabIndex = 8
        Me.sbar.Text = "StatusBar1"
        '
        'sbarLeft
        '
        Me.sbarLeft.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbarLeft.Icon = CType(resources.GetObject("sbarLeft.Icon"), System.Drawing.Icon)
        Me.sbarLeft.Width = 253
        '
        'sbarRight
        '
        Me.sbarRight.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbarRight.Icon = CType(resources.GetObject("sbarRight.Icon"), System.Drawing.Icon)
        Me.sbarRight.Width = 253
        '
        'cmMenuForm
        '
        Me.cmMenuForm.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSetDefaultConfig, Me.miSep2, Me.cmAbout, Me.cmCheckUpdates, Me.miReportBugs, Me.miReleaseNotes, Me.miSupportForums, Me.miVersionHistory, Me.miSep1, Me.cmExit})
        '
        'miSetDefaultConfig
        '
        Me.miSetDefaultConfig.Index = 0
        Me.miSetDefaultConfig.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.miSetDefaultConfig.Text = "Set this Configuration as &Default"
        '
        'miSep2
        '
        Me.miSep2.Index = 1
        Me.miSep2.Text = "-"
        '
        'cmAbout
        '
        Me.cmAbout.Index = 2
        Me.cmAbout.Text = "&About..."
        '
        'cmCheckUpdates
        '
        Me.cmCheckUpdates.Index = 3
        Me.cmCheckUpdates.Text = "&Check for Update..."
        '
        'miReportBugs
        '
        Me.miReportBugs.Index = 4
        Me.miReportBugs.Text = "&Report Bugs..."
        '
        'miReleaseNotes
        '
        Me.miReleaseNotes.Index = 5
        Me.miReleaseNotes.Text = "Release &Notes..."
        '
        'miSupportForums
        '
        Me.miSupportForums.Index = 6
        Me.miSupportForums.Text = "Support &Forums..."
        '
        'miVersionHistory
        '
        Me.miVersionHistory.Index = 7
        Me.miVersionHistory.Text = "&Version History..."
        '
        'miSep1
        '
        Me.miSep1.Index = 8
        Me.miSep1.Text = "-"
        '
        'cmExit
        '
        Me.cmExit.Index = 9
        Me.cmExit.Text = "E&xit"
        '
        'chkTreeGuiConfigFileReg
        '
        Me.chkTreeGuiConfigFileReg.Checked = True
        Me.chkTreeGuiConfigFileReg.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTreeGuiConfigFileReg.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkTreeGuiConfigFileReg.Location = New System.Drawing.Point(88, 416)
        Me.chkTreeGuiConfigFileReg.Name = "chkTreeGuiConfigFileReg"
        Me.chkTreeGuiConfigFileReg.Size = New System.Drawing.Size(240, 16)
        Me.chkTreeGuiConfigFileReg.TabIndex = 13
        Me.chkTreeGuiConfigFileReg.Text = "Associate TreeGUI &Config File Extension"
        '
        'Form1
        '
        Me.AcceptButton = Me.btnAdd
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(506, 408)
        Me.ContextMenu = Me.cmMenuForm
        Me.Controls.Add(Me.sbar)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.chkTreeGuiConfigFileReg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.gbFolders.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.gbLoadWithWindows.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.gbOutputOptions.ResumeLayout(False)
        Me.gbTreeOptions.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.gbFrequency.ResumeLayout(False)
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.sbarLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbarRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim currentProfileName As String

    Private fileName As String
    Private appinf As New McoreSystem.AppInfo(Application.ProductName, Application.ProductVersion)
    Private resext As New McoreSystem.ResourceExtracter
    Private svcadmin As New McoreSystem.ServiceAdmin
    Private targetFilePath As String
    Private isWindowNormal As Boolean = False
    Private isGuiReady As Boolean = False
    Private pbValue As Integer

    Private glbConfigFilePath As String
    Private glbDefaultConfigFilePath As String = Application.ExecutablePath + ".tgc"
    Private glbTree

    'Const con_START As String = "&Start"
    'Const con_STOP As String = "&Stop"
    'Const con_INSTALL As String = "&Install"
    'Const con_UNINSTALL As String = "&Uninstall"

    Const MCOREINDEXER As String = "McoreIndexer"

    Private Const ASCII As String = "Ascii"
    Private Const INDEX_FILES As String = "IndexFiles"

    Private Const RUN_SCHEDULE As String = "RunSchedule"

    Private Const TRAY_ICON As String = "TrayIconIsEnabled"
    Private Const TRAY_ONLY_WHEN_LOAD As String = "TrayOnlyWhenLoad"
    Private Const SHOW_IN_TASKBAR As String = "ShowInTaskbar"
    Private Const CLOSE_TO_TRAY As String = "CloseToTray"

    Private Enum IndexingMode
        INDEX_FILE_IN_EACH_DIR
        INDEX_FILE_IN_ONE_FOLDER
    End Enum

    Public Sub setConfigFile(ByVal filePath As String)
        glbConfigFilePath = filePath
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        addFolder()

    End Sub

    Private Sub addFolder()
        Dim dlg As New McoreSystem.FolderBrowser
        dlg.Title = "Choose directory to index"
        dlg.Flags = McoreSystem.BrowseFlags.BIF_NEWDIALOGSTYLE Or _
                    McoreSystem.BrowseFlags.BIF_STATUSTEXT Or _
                    McoreSystem.BrowseFlags.BIF_EDITBOX

        If dlg.ShowDialog = DialogResult.OK Then
            '1.4.0.1 It was possible to add My Computer and cause Tree.com to crash
            If dlg.DirectoryPath.Length > 0 Then
                pbBar.Value = 0
                lbFolders.Items.Add(dlg.DirectoryPath)
                Me.updateGuiControls()
            End If

        End If
    End Sub

    Private Sub addSubfolders(ByVal recursive As Boolean)
        Dim dlg As New McoreSystem.FolderBrowser
        If recursive Then
            dlg.Title = "Choose directory to index subfolders (recursive)"
        Else
            dlg.Title = "Choose directory to index subfolders"
        End If


        dlg.Flags = McoreSystem.BrowseFlags.BIF_NEWDIALOGSTYLE Or _
                    McoreSystem.BrowseFlags.BIF_STATUSTEXT Or _
                    McoreSystem.BrowseFlags.BIF_EDITBOX
        If dlg.ShowDialog = DialogResult.OK Then
            lbFolders.Items.Add(dlg.DirectoryPath)
            addSubfolders(dlg.DirectoryPath, recursive)
            Me.updateGuiControls()
        End If
    End Sub
    Private Sub addSubfolders(ByVal rootFolderPath As String, ByVal recursive As Boolean)

        'initial root folder is the dlg.SelectedPath

        Dim strDir As String
        For Each strDir In Directory.GetDirectories(rootFolderPath)
            lbFolders.Items.Add(strDir)
            If recursive Then
                addSubfolders(strDir, True)
            End If
        Next
    End Sub


    Private Sub indexNow(ByVal mode As IndexingMode, ByVal folderList As ArrayList)

        Dim tree As New McoreSystem.TreeLib(glbConfigFilePath)

        If chkIndexFileInEachDir.Checked And chkIndexFileInOneDir.Checked Then
            pbBar.Value = Me.pbValue
            pbBar.Maximum = folderList.Count * 2
        Else
            pbBar.Maximum = folderList.Count
            pbBar.Value = 0
        End If

        'For i As Integer = 0 To lbFolders.Items.Count - 1


        For i As Integer = 0 To folderList.Count - 1

            Dim TEMP_FILE As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\temp" + i.ToString + ".bat"
            Dim CURRENT_DIR As String = folderList.Item(i)

            Dim TREE_COMMAND As String = "%windir%\system32\tree.com " + tree.getSourceSwitch(CURRENT_DIR) + tree.getAsciiSwitch() + tree.getAddFilesSwitch() + tree.getOutputSwitch(CURRENT_DIR, mode)
            Console.WriteLine(TREE_COMMAND)
            FileOpen(1, TEMP_FILE, OpenMode.Output)
            PrintLine(1, TREE_COMMAND)

            '1.5.3.4 Didn't tag index files created in the same folder witout appending
            If mode = IndexingMode.INDEX_FILE_IN_EACH_DIR Or i = folderList.Count - 1 _
               Or (tree.isChkSingleFile = False And mode = IndexingMode.INDEX_FILE_IN_ONE_FOLDER) Then
                PrintLine(1, tree.getFooterText)
            End If

            PrintLine(1, "DEL " + Chr(34) + TEMP_FILE + Chr(34))
            FileClose(1)

            Dim proc As New Process
            Dim psi As New ProcessStartInfo(TEMP_FILE)
            psi.WindowStyle = ProcessWindowStyle.Hidden
            proc.StartInfo = psi
            proc.Start()
            sbarRight.Text = "Indexing " + folderList.Item(i)
            proc.WaitForExit()

            If chkRemoveBranches.CheckState = CheckState.Checked Then
                tree.removeTreeBranches(tree.getCurrentIndexFilePath)
            End If

            If proc.HasExited Then
                pbBar.Increment(1)
            End If



        Next

        sbarLeft.Text = "Last Indexed on " + Now.ToString("yyyy-MM-ddTHH:mm:ss")
        sbarRight.Text = "Ready"

    End Sub

    Public Sub IndexNow(ByVal folderList As ArrayList)


        Me.updateGuiControls()
        'If Me.isGuiReady = True Then Call setSettingsToXML()

        If lbFolders.Items.Count > 0 Then

            If chkIndexFileInEachDir.CheckState = CheckState.Checked Then
                Me.pbValue = 0
                Me.indexNow(IndexingMode.INDEX_FILE_IN_EACH_DIR, folderList)
            End If

            If chkIndexFileInOneDir.CheckState = CheckState.Checked Then
                Me.pbValue = pbBar.Value
                Me.indexNow(IndexingMode.INDEX_FILE_IN_ONE_FOLDER, folderList)
            End If

            Dim MyLog As New EventLog(Application.ProductName)  ' create a new event log 
            ' Check if the the Event Log Exists 
            If Not MyLog.SourceExists("McoreIndexer") Then
                MyLog.CreateEventSource("McoreIndexer", "McoreIndexer Log") ' Create Log 
            End If
            MyLog.Source = "McoreIndexer"

            Try
                Dim log As String
                For Each item As String In lbFolders.Items
                    log += "Indexed " + item + vbCrLf
                Next
                'BUG: Only display scheduled time if actually scheduled
                'BUG: Refixed
                If chkStartupItem.Enabled = True And chkStartupItem.Checked = True And chkRunSchedule.Checked = True Then
                    log += vbCrLf
                    'log += String.Format("TreeGUI is scheduled to index folders in another {0} minutes", glbTree.getTimeIntervalToMinutes)
                    Dim dtp As New DateTimePicker
                    log += glbTree.getEventLogText
                End If
                MyLog.WriteEntry("TreeGUI Log", log, EventLogEntryType.Information)
            Catch ex As Exception

            End Try


        End If



    End Sub

    Private Sub btnIndexNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIndexNow.Click


        Call Me.indexNow(glbTree.getFolderListToArrayList())


    End Sub
    Private Sub createBackupConfig()
        File.Copy(glbConfigFilePath, glbConfigFilePath + ".bak", True)
    End Sub

    <STAThread()> Public Shared Sub Main(ByVal args() As String)

        Dim appinf As New McoreSystem.AppInfo(Application.ProductName, Application.ProductVersion)
        Dim myForm As New Form1


        If args.Length > 0 Then
            myForm.glbConfigFilePath = args(0).ToString
        Else
            ' User loads TreeGUI; not double clicking a .tgc file
            myForm.glbConfigFilePath = myForm.glbDefaultConfigFilePath
        End If

        Application.Run(myForm)

    End Sub

    Private Sub lbNames_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbFolders.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim pt As Point
            pt.X = e.X
            pt.Y = e.Y
            lbFolders.SelectedIndex = lbFolders.IndexFromPoint(pt)
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        glbTree = New McoreSystem.TreeLib(glbConfigFilePath)

        chkTrayIcon.Text = Application.ProductName + " System Tray &Icon"

        If Me.glbConfigFilePath = Me.glbDefaultConfigFilePath Then
            currentProfileName = "Default"
        Else
            currentProfileName = IO.Path.GetFileNameWithoutExtension(Me.glbConfigFilePath)
        End If

        ' Set Form1 Title
        Me.Text = String.Format("{0} - [{1}]", appinf.GetApplicationTitle(), currentProfileName)
        niTray.Text = Me.Text

        gbOutputOptions.Text = txtFileName.Text + " Options"
        cmiShowApp.Text = String.Format("&Show {0}...", niTray.Text)
        cboExt.SelectedIndex = 0
        btnAssoTgcExt.Text = String.Format("&Associate .tgc files with {0}", Application.ProductName)

        sbarRight.Text = "Ready"

        'Disable Load with Windows groupbox if profile isn't default 
        gbLoadWithWindows.Enabled = (Me.glbConfigFilePath = Me.glbDefaultConfigFilePath)

        'BUG: 1.2.8.1 Enabling Windows Service sometimes caused an error
        Dim tree As New McoreSystem.TreeLib(glbConfigFilePath)
        If Not tree.isProperlyInstalledService(Application.StartupPath, MCOREINDEXER) Then
            tree.setProperServicePath(Application.StartupPath, MCOREINDEXER)
        End If



        ' Get Folder List if there any 
        Dim array As ArrayList = New ArrayList
        array = glbTree.getSettingsFromXmlToArrayList("/configuration/folders/folder")
        For Each item As String In array
            lbFolders.Items.Add(item)
        Next

        array = Me.getSettingsFromXmlToArrayList("configuration/options/" + RUN_SCHEDULE)
        If array.Count > 0 Then chkRunSchedule.Checked = array(0)

        ' Load with Windows : Get Options from Registry
        chkStartupItem.Checked = tree.isLoadedAsStartup(Application.ProductName, Application.ExecutablePath)
        chkWindowsService.Checked = tree.isRunningService(MCOREINDEXER)

        ' Tray Settings : Get from XML

        array = Me.getSettingsFromXmlToArrayList("configuration/options/" + TRAY_ICON)
        If array.Count > 0 Then chkTrayIcon.Checked = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/options/" + TRAY_ONLY_WHEN_LOAD)
        If array.Count > 0 Then chkTrayOnlyWhenLoad.Checked = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/options/" + SHOW_IN_TASKBAR)
        If array.Count > 0 Then chkDontShowInTaskbar.Checked = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/options/" + CLOSE_TO_TRAY)
        If array.Count > 0 Then chkCloseToTray.Checked = array(0)

        niTray.Visible = chkTrayIcon.Checked

        If chkTrayOnlyWhenLoad.Checked = True Then
            Me.WindowState = FormWindowState.Minimized
            Me.ShowInTaskbar = False
        End If

        ' Tree Settings : Get from XML
        array = Me.getSettingsFromXmlToArrayList("configuration/style/Ascii")
        If array.Count > 0 Then chkAscii.Checked = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/style/" + glbTree.REMOVE_TREE_BRANCHES)
        If array.Count > 0 Then chkRemoveBranches.Checked = array(0)

        array = Me.getSettingsFromXmlToArrayList("configuration/style/IndexFiles")
        If array.Count > 0 Then chkIndexFiles.Checked = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/style/Extension")
        If array.Count > 0 Then cboExt.Text = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/options/Interval")
        If array.Count > 0 Then nudInterval.Value = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/style/SameDir")
        If array.Count > 0 Then chkIndexFileInEachDir.Checked = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/style/DiffDir")
        If array.Count > 0 Then chkIndexFileInOneDir.Checked = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/style/SingleFile")
        If array.Count > 0 Then chkSingleFile.Checked = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/style/OutputDir")
        If array.Count > 0 Then txtOutputDir.Text = array(0)
        array = Me.getSettingsFromXmlToArrayList("configuration/options/XPStyle")
        If array.Count > 0 Then chkVisualStyles.Checked = array(0)

        ' Version History : Get from Embedded Resource
        Dim currentAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
        'Me.txtVer.Text = Me.resext.GetText(currentAssembly, "VersionHistory.txt")

        ' All done before Form loads
        Me.isGuiReady = True
        Me.updateGuiControls()
        Me.createBackupConfig()

    End Sub

    Private Sub btnBrowseOutputDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseOutputDir.Click

        Dim dlg As New McoreSystem.FolderBrowser
        dlg.Title = "Choose directory to index"
        dlg.Flags = McoreSystem.BrowseFlags.BIF_NEWDIALOGSTYLE Or _
                    McoreSystem.BrowseFlags.BIF_STATUSTEXT Or _
                    McoreSystem.BrowseFlags.BIF_EDITBOX

        If dlg.ShowDialog = DialogResult.OK Then
            If dlg.DirectoryPath.Length > 0 Then
                txtOutputDir.Text = dlg.DirectoryPath
            End If
        End If

    End Sub

    Private Sub txtFileName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFileName.TextChanged
        gbOutputOptions.Text = txtFileName.Text + " Options"
    End Sub

    Private Sub rbDiffDir_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIndexFileInOneDir.CheckedChanged

        btnBrowseOutputDir.Enabled = chkIndexFileInOneDir.Checked
        chkSingleFile.Enabled = chkIndexFileInOneDir.Checked
        Dim array As New ArrayList
        If chkIndexFileInOneDir.CheckState = CheckState.Unchecked Then
            txtOutputDir.Text = String.Empty
        Else
            txtOutputDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        End If
    End Sub

    Private Sub removeFolder()
        lbFolders.Items.Remove(lbFolders.SelectedItem)
        Me.updateGuiControls()
    End Sub
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Call removeFolder()
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        e.Cancel = chkCloseToTray.Checked

        If e.Cancel Then
            Me.WindowState = FormWindowState.Minimized
            Me.ShowInTaskbar = False
            'Me.adminBackgroundServices()
        End If

    End Sub

    Private Function getSettingsFromXmlToArrayList(ByVal xpath As String) As ArrayList

        Dim array As New ArrayList
        If File.Exists(Me.glbConfigFilePath) Then
            Dim configFile As New Xml.XmlDocument
            configFile.Load(glbConfigFilePath)
            Dim Node_list As Xml.XmlNodeList = configFile.SelectNodes(xpath)
            For Each nd As Xml.XmlNode In Node_list
                Debug.WriteLine(nd.Attributes("value").Value)
                array.Add(nd.Attributes("value").Value.ToString)
            Next
        End If
        Return array

    End Function

#Region "Saving XML File"

    Private Sub setSettingsToXML()

        Dim configFile As New Xml.XmlDocument

        ' Root
        Dim Node_root As Xml.XmlNode = configFile.AppendChild(configFile.CreateElement("configuration"))

        '*********
        ' Folders
        '*********
        Dim Node_folders As Xml.XmlNode = configFile.CreateElement("folders")
        Dim Element_folders(lbFolders.Items.Count - 1) As Xml.XmlElement
        For i As Integer = 0 To lbFolders.Items.Count - 1
            Element_folders(i) = configFile.CreateElement("folder")
            Element_folders(i).Attributes.Append(configFile.CreateAttribute("id"))
            Element_folders(i).Attributes("id").Value = (i + 1).ToString
            Element_folders(i).Attributes.Append(configFile.CreateAttribute("value"))
            Element_folders(i).Attributes("value").Value = lbFolders.Items(i)

            Node_folders.AppendChild(Element_folders(i))
        Next
        Node_root.AppendChild(Node_folders)


        '************
        ' Style
        '************
        Dim Node_style As Xml.XmlNode = configFile.CreateElement("style")

        ' Extension
        Dim Element_FileExt As Xml.XmlElement
        Element_FileExt = configFile.CreateElement("Extension")
        Element_FileExt.Attributes.Append(configFile.CreateAttribute("value"))
        Element_FileExt.Attributes("value").Value = cboExt.Text
        Node_style.AppendChild(Element_FileExt)

        ' Index File Name
        Dim Element_IndexFileName As Xml.XmlElement
        Element_IndexFileName = configFile.CreateElement("IndexFileName")
        Element_IndexFileName.Attributes.Append(configFile.CreateAttribute("value"))
        Element_IndexFileName.Attributes("value").Value = txtFileName.Text
        Node_style.AppendChild(Element_IndexFileName)

        ' Single File
        Dim Element_SingleFile As Xml.XmlElement
        Element_SingleFile = configFile.CreateElement("SingleFile")
        Element_SingleFile.Attributes.Append(configFile.CreateAttribute("value"))
        Element_SingleFile.Attributes("value").Value = chkSingleFile.Checked
        Node_style.AppendChild(Element_SingleFile)

        ' Output Dir
        Dim Element_OutputDir As Xml.XmlElement
        Element_OutputDir = configFile.CreateElement("OutputDir")
        Element_OutputDir.Attributes.Append(configFile.CreateAttribute("value"))
        Element_OutputDir.Attributes("value").Value = txtOutputDir.Text
        Node_style.AppendChild(Element_OutputDir)

        ' Diff Dir
        Dim Element_DiffDir As Xml.XmlElement
        Element_DiffDir = configFile.CreateElement("DiffDir")
        Element_DiffDir.Attributes.Append(configFile.CreateAttribute("value"))
        Element_DiffDir.Attributes("value").Value = chkIndexFileInOneDir.Checked
        Node_style.AppendChild(Element_DiffDir)

        ' Same Dir
        Dim Element_SameDir As Xml.XmlElement
        Element_SameDir = configFile.CreateElement("SameDir")
        Element_SameDir.Attributes.Append(configFile.CreateAttribute("value"))
        Element_SameDir.Attributes("value").Value = chkIndexFileInEachDir.Checked
        Node_style.AppendChild(Element_SameDir)

        ' Index Files
        Dim Element_IndexFiles As Xml.XmlElement
        Element_IndexFiles = configFile.CreateElement("IndexFiles")
        Element_IndexFiles.Attributes.Append(configFile.CreateAttribute("value"))
        Element_IndexFiles.Attributes("value").Value = chkIndexFiles.Checked
        Node_style.AppendChild(Element_IndexFiles)

        ' REMOVE_TREE_BRANCHES
        Dim Element_REMOVE_TREE_BRANCHES As Xml.XmlElement
        Element_REMOVE_TREE_BRANCHES = configFile.CreateElement(glbTree.REMOVE_TREE_BRANCHES)
        Element_REMOVE_TREE_BRANCHES.Attributes.Append(configFile.CreateAttribute("value"))
        Element_REMOVE_TREE_BRANCHES.Attributes("value").Value = chkRemoveBranches.Checked
        Node_style.AppendChild(Element_REMOVE_TREE_BRANCHES)

        ' Ascii Style
        Dim Element_Ascii As Xml.XmlElement
        Element_Ascii = configFile.CreateElement("Ascii")
        Element_Ascii.Attributes.Append(configFile.CreateAttribute("value"))
        Element_Ascii.Attributes("value").Value = chkAscii.Checked
        Node_style.AppendChild(Element_Ascii)

        Node_root.AppendChild(Node_style)


        '************
        ' Options
        '************
        Dim Node_options As Xml.XmlNode = configFile.CreateElement("options")


        ' TimeInterval
        Dim Element_XP As Xml.XmlElement
        Element_XP = configFile.CreateElement("XPStyle")
        Element_XP.Attributes.Append(configFile.CreateAttribute("value"))
        Element_XP.Attributes("value").Value = chkVisualStyles.Checked
        Node_options.AppendChild(Element_XP)

        ' Run Schedule Tasks
        Dim Element_RUN_SCHEDULE As Xml.XmlElement
        Element_RUN_SCHEDULE = configFile.CreateElement(RUN_SCHEDULE)
        Element_RUN_SCHEDULE.Attributes.Append(configFile.CreateAttribute("value"))
        Element_RUN_SCHEDULE.Attributes("value").Value = chkRunSchedule.Checked
        Node_options.AppendChild(Element_RUN_SCHEDULE)

        ' TimeInterval
        Dim Element_Interval As Xml.XmlElement
        Element_Interval = configFile.CreateElement("Interval")
        Element_Interval.Attributes.Append(configFile.CreateAttribute("value"))
        Element_Interval.Attributes("value").Value = nudInterval.Value
        Node_options.AppendChild(Element_Interval)

        ' SHOW_IN_TASKBAR
        Dim Element_SHOW_IN_TASKBAR As Xml.XmlElement
        Element_SHOW_IN_TASKBAR = configFile.CreateElement(SHOW_IN_TASKBAR)
        Element_SHOW_IN_TASKBAR.Attributes.Append(configFile.CreateAttribute("value"))
        Element_SHOW_IN_TASKBAR.Attributes("value").Value = chkDontShowInTaskbar.CheckState
        Node_options.AppendChild(Element_SHOW_IN_TASKBAR)


        ' TRAY_ICON
        Dim Element_TrayIconIsEnabled As Xml.XmlElement
        Element_TrayIconIsEnabled = configFile.CreateElement(TRAY_ICON)
        Element_TrayIconIsEnabled.Attributes.Append(configFile.CreateAttribute("value"))
        Element_TrayIconIsEnabled.Attributes("value").Value = chkTrayIcon.CheckState
        Node_options.AppendChild(Element_TrayIconIsEnabled)

        'CLOSE_TO_TRAY
        Dim Elem_CLOSE_TO_TRAY As Xml.XmlElement
        Elem_CLOSE_TO_TRAY = configFile.CreateElement(CLOSE_TO_TRAY)
        Elem_CLOSE_TO_TRAY.Attributes.Append(configFile.CreateAttribute("value"))
        Elem_CLOSE_TO_TRAY.Attributes("value").Value = chkCloseToTray.CheckState
        Node_options.AppendChild(Elem_CLOSE_TO_TRAY)
        Node_root.AppendChild(Node_options)

        ' Tray
        Dim Element_Tray As Xml.XmlElement
        Element_Tray = configFile.CreateElement(TRAY_ONLY_WHEN_LOAD)
        Element_Tray.Attributes.Append(configFile.CreateAttribute("value"))
        Element_Tray.Attributes("value").Value = chkTrayOnlyWhenLoad.CheckState
        Node_options.AppendChild(Element_Tray)
        Node_root.AppendChild(Node_options)

        Dim myWriter As Xml.XmlTextWriter
        myWriter = New Xml.XmlTextWriter(Me.glbConfigFilePath, System.Text.Encoding.UTF8)
        myWriter.Formatting = Xml.Formatting.Indented
        configFile.Save(myWriter)
        myWriter.Close()

        'myWriter = New Xml.XmlTextWriter(Application.ExecutablePath + ".tgc", System.Text.Encoding.UTF8)
        'myWriter.Formatting = Xml.Formatting.Indented
        'configFile.Save(myWriter)
        'myWriter.Close()

    End Sub

#End Region

    Private Sub chkTray_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStartupItem.CheckedChanged
        updateGuiControls()
    End Sub

    Private Sub updateGuiControls()

        If Me.isGuiReady = True Then

            Call Me.updateButtons()

            If chkWindowsService.CheckState = CheckState.Checked Then
                installNrunService()
            Else
                glbTree.stopServiceInternally(MCOREINDEXER)
            End If

            sbarLeft.Text = lbFolders.Items.Count & " folders ready to Index"
            chkRunSchedule.Enabled = chkStartupItem.Checked
            btnUninstallWindowsService.Enabled = glbTree.isInstalledService(MCOREINDEXER)

            If chkStartupItem.Checked Then
                If chkStartupItem.Enabled = True Then
                    If chkRunSchedule.Checked = True Then
                        tmrInterval.Enabled = True
                        tmrInterval.Start()
                    End If
                End If
            Else
                tmrInterval.Stop()
                tmrInterval.Enabled = False
            End If

            'MessageBox.Show("Saving File")
            Me.setSettingsToXML()

        End If

    End Sub

#Region "Service Controllers"

    Private Function isFoundService(ByVal ControlSetIndex As Integer, ByVal ServiceName As String) As Boolean
        If ControlSetIndex < 10 Then
            Dim regMcoreIndexerRoot As Microsoft.Win32.RegistryKey
            Try
                regMcoreIndexerRoot = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(String.Format("SYSTEM\ControlSet00{0}\Services", ControlSetIndex))
                Dim strServices() As String = regMcoreIndexerRoot.GetSubKeyNames
                regMcoreIndexerRoot.Close()
                For Each svc As String In strServices
                    If svc = ServiceName Then
                        Console.WriteLine(svc & " was Found in: ControlSet00" & ControlSetIndex)
                        Return True
                    End If
                Next
                Console.WriteLine("Service was not in: ControlSet00" & ControlSetIndex)
                ControlSetIndex += 1
                Return isFoundService(ControlSetIndex, ServiceName)
            Catch ex As Exception
                ' Try the next ControlSet
                Console.WriteLine("Service was not in: ControlSet00" & ControlSetIndex)
                ControlSetIndex += 1
                Return isFoundService(ControlSetIndex, ServiceName)
            End Try
        Else
            Return False
        End If

        'Usage: Return isFoundService(0, ServiceName)

    End Function

    Private Sub uninstallService()
        If glbTree.isInstalledService(MCOREINDEXER) Then
            svcadmin.RemoveServiceDescriptionFromRegistry(MCOREINDEXER)
            svcadmin.UnInstallService(MCOREINDEXER)
        End If
    End Sub
    Private Sub installService()
        If Not glbTree.isInstalledService(MCOREINDEXER) Then
            svcadmin.InstallService(Application.StartupPath + "\" + MCOREINDEXER + ".exe", MCOREINDEXER, MCOREINDEXER)
            svcadmin.AddServiceDescriptionToRegistry(MCOREINDEXER, "Directory Indexing service for TreeGUI")
        End If
    End Sub
    Private Sub installNrunService()
        installService()
        glbTree.startServiceExternally(MCOREINDEXER)
    End Sub
    Private Sub stopNuninstallService()
        glbTree.stopServiceExternally(MCOREINDEXER)
        uninstallService()
    End Sub

#End Region

    Private Sub btnAdminSvc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.setSettingsToXML()

    End Sub


    Private Sub tmrInterval_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrInterval.Tick

        ' NOT using TreeLib.dll but real time value from NudUpDown
        tmrInterval.Interval = glbTree.getTimeInterval
        'MessageBox.Show(tmrInterval.Interval)
        Call Me.indexNow(glbTree.getFolderListToArrayList())

    End Sub

    Private Sub adminBackgroundServices()

        Dim regKey As Microsoft.Win32.RegistryKey = _
              Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)

        If chkStartupItem.Checked Then
            regKey.SetValue(Application.ProductName, Application.ExecutablePath)
        Else
            regKey.DeleteValue(Application.ProductName, False)
        End If

        If chkWindowsService.Checked Then
            Me.installNrunService()
        Else
            glbTree.stopService(MCOREINDEXER)
        End If

        regKey.Close()

    End Sub

    Private Sub safelyExitApplication()

        'Me.updateGuiControls()
        'Me.adminBackgroundServices()
        'If chkWindowsService.Checked = False Then
        '    Me.stopNuninstallService()
        'End If

        Me.setSettingsToXML()
        Application.Exit()

    End Sub

    Private Sub cmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiExit.Click
        safelyExitApplication()
    End Sub

    Private Sub cmiShowApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiShowApp.Click
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
    End Sub

    Private Sub cmiIndexNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiIndexNow.Click

        Call Me.indexNow(glbTree.getFolderListToArrayList())

    End Sub

    Private Sub niTray_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles niTray.DoubleClick

        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Minimized
        ElseIf Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Normal
            Me.ShowInTaskbar = True
        End If

        'If chkTrayOnlyWhenLoad.Checked = False Then
        '    Me.ShowInTaskbar = True
        'End If

    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            If isGuiReady = True Then Me.setSettingsToXML()
            Me.ShowInTaskbar = Not chkDontShowInTaskbar.Checked And Not chkCloseToTray.Checked
        End If
    End Sub

    Private Sub cmFoldersRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmFoldersRemove.Click
        Call Me.removeFolder()
    End Sub

    Private Sub browseFolder()
        Dim proc As New Process
        Dim psi As New ProcessStartInfo("explorer.exe")
        proc.StartInfo = psi
        psi.Arguments = lbFolders.SelectedItem
        proc.Start()
    End Sub
    Private Sub miFoldersBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFoldersBrowse.Click
        browseFolder()
    End Sub


    Private Sub btnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
        ' only if the first item isn't the current one
        ' add a duplicate item up in the listbox

        lbFolders.Items.Insert(lbFolders.SelectedIndex - 1, lbFolders.SelectedItem)
        'MessageBox.Show(lbFolders.SelectedIndex)
        ' make it the current item
        lbFolders.SelectedIndex = lbFolders.SelectedIndex - 2
        ' delete the old occurrence of this item
        lbFolders.Items.RemoveAt(lbFolders.SelectedIndex + 2)

        Me.updateGuiControls()

    End Sub


    Private Sub btnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        ' only if the last item isn't the current one
        ' add a duplicate item down in the listbox
        lbFolders.Items.Insert(lbFolders.SelectedIndex + 2, lbFolders.SelectedItem)
        ' make it the current item
        lbFolders.SelectedIndex += 2
        ' delete the old occurrence of this item
        lbFolders.Items.RemoveAt(lbFolders.SelectedIndex - 2)

        Me.updateGuiControls()

    End Sub

    Private Sub lbFolders_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbFolders.SelectedIndexChanged
        updateButtons()
    End Sub

    Private Sub updateButtons() 'Always called by updateGuiControls

        miSetDefaultConfig.Enabled = (Me.glbConfigFilePath <> Me.glbDefaultConfigFilePath)

        ' Buttons UP and DOWN
        btnMoveUp.Enabled = (lbFolders.Items.Count > 1 And lbFolders.SelectedIndex > 0)
        btnMoveDown.Enabled = (lbFolders.Items.Count > 1 And lbFolders.SelectedIndex <> lbFolders.Items.Count - 1 And lbFolders.SelectedIndex <> -1)

        ' Button Remove
        btnRemove.Enabled = (lbFolders.Items.Count > 0 And lbFolders.SelectedIndex <> -1)
        ' Button IndexNow and Context MenuItem IndexNow
        btnIndexNow.Enabled = (lbFolders.Items.Count > 0)
        cmiIndexNow.Enabled = btnIndexNow.Enabled

        ' Context Menu Browse Folder
        For Each item As MenuItem In cmMenuForListBox.MenuItems
            item.Enabled = (lbFolders.Items.Count > 0 And lbFolders.SelectedIndex <> -1)
        Next
        For Each item As MenuItem In cmMenuRemove.MenuItems
            item.Enabled = btnRemove.Enabled
        Next

    End Sub

    Private Sub updateTrayCheckBoxes()
        niTray.Visible = chkTrayIcon.Checked
        If chkTrayIcon.Checked = False Then
            chkTrayOnlyWhenLoad.Checked = False
            chkDontShowInTaskbar.Checked = False
            chkCloseToTray.Checked = False
        End If
        chkTrayOnlyWhenLoad.Enabled = chkTrayIcon.Checked
        chkDontShowInTaskbar.Enabled = chkTrayIcon.Checked And Not chkCloseToTray.Checked
        chkCloseToTray.Enabled = chkTrayIcon.Checked
        If chkCloseToTray.Checked = True Then
            chkDontShowInTaskbar.Checked = True
        End If

    End Sub

    Private Sub chkTrayIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTrayIcon.CheckedChanged
        updateTrayCheckBoxes()
    End Sub

    Private Sub chkRunSchedule_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRunSchedule.CheckedChanged
        Me.updateGuiControls()
    End Sub


    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmExit.Click
        safelyExitApplication()
    End Sub

    Private Sub checkUpdate()

        Dim app As New McoreSystem.AppInfo(Application.ProductName, Application.ProductVersion)
        Dim strUrl As String = "http://microsoftuse.temp.powweb.com/mcored/downloads/"
        If (app.isUpdated(strUrl, "updates.txt") = True) Then
            If (MessageBox.Show(Application.ProductName + " " + app.GetNewVersion + " is available. Do you want to download it now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = DialogResult.Yes Then
                Process.Start(strUrl)
            End If
        Else
            MessageBox.Show("The current version is up-to-date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub cmCheckUpdates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmCheckUpdates.Click

        'Dim threadCheckUpdate As New System.Threading.Thread(AddressOf checkUpdate)
        'threadCheckUpdate.IsBackground = True
        'threadCheckUpdate.Start()

        Process.Start("http://microsoftuse.temp.powweb.com/mcored/downloads/")

    End Sub

    Private Sub miReportBugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miReportBugs.Click
        Dim strSubject As String = Application.ProductName + " " + Application.ProductVersion
        Dim strMessageBody As String = Application.ProductName
        System.Diagnostics.Process.Start("mailto:mcored@ii.net?subject=" + strSubject + "&body=" + strMessageBody)
    End Sub

    Private Sub miVersionHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miVersionHistory.Click
        Dim ver As New frmVer
        ver.ShowDialog()
    End Sub

    Private Sub cmAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmAbout.Click
        MessageBox.Show(Application.ProductName + vbCrLf + "Version " + Application.ProductVersion + vbCrLf + "Coded by McoreD", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub miSupportForums_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miSupportForums.Click
        Process.Start("http://www.betaone.net")
    End Sub

    Private Sub cboExt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboExt.SelectedIndexChanged
        chkAscii.Checked = (cboExt.SelectedIndex < 3)
    End Sub

    Private Sub chkAscii_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAscii.CheckedChanged
        chkRemoveBranches.Checked = chkAscii.Checked
        chkRemoveBranches.Enabled = chkAscii.Checked
    End Sub

    Private Sub openIndexFile()
        Try
            Process.Start(lbFolders.SelectedItem & "\" & txtFileName.Text & cboExt.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub miOpenIndexFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miOpenIndexFile.Click
        openIndexFile()
    End Sub

    Private Sub miAddFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miAddFolder.Click
        Me.addFolder()
    End Sub

    Private Sub miAddSubfolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miAddSubfolders.Click
        Me.addSubfolders(False)
    End Sub

    Private Sub miAddSubfoldersRecursive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miAddSubfoldersRecursive.Click
        Me.addSubfolders(True)
    End Sub


    Private Sub miRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miRemove.Click
        removeFolder()
    End Sub

    Private Sub miRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miRemoveAll.Click
        lbFolders.Items.Clear()
        Me.updateGuiControls()
    End Sub

    Private Sub chkCloseToTray_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCloseToTray.CheckedChanged
        updateTrayCheckBoxes()
    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        safelyExitApplication()
    End Sub

    Private Sub miSetDefaultConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miSetDefaultConfig.Click

        Me.setSettingsToXML()
        File.Copy(Me.glbConfigFilePath, Me.glbDefaultConfigFilePath, True)

    End Sub

    Private Sub miReleaseNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miReleaseNotes.Click
        Dim f As New frmRelNotes
        f.ShowDialog()
    End Sub

    Private Sub chkWindowsService_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWindowsService.CheckedChanged
        Me.updateGuiControls()
    End Sub

    Private Sub btnUninstallWindowsService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUninstallWindowsService.Click
        If (MessageBox.Show(String.Format("To uninstall McoreIndexer, {0} has to be closed.", Application.ProductName), Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.OK) Then
            Me.stopNuninstallService()
            Me.safelyExitApplication()
        End If
    End Sub

    Private Sub btnAssoTgcExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssoTgcExt.Click
        Dim fam As New McoreSystem.FileExtensionManager2
        fam.CreateFileAssociation(".tgc", "TreeGUI.Config", Application.ProductName & " Config File", Application.ExecutablePath, 0)

    End Sub

    Private Sub miIndexThisFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miIndexThisFolder.Click
        Dim folder As New ArrayList
        folder.Add(lbFolders.SelectedItem)
        Me.indexNow(folder)
    End Sub

    Private Sub lbFolders_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFolders.DoubleClick
        openIndexFile()
    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub
End Class

#Region "Archive"

'************************
'* Method 1
'***********************

'<STAThread()> Public Shared Sub Main()

'    Dim c As Process = Process.GetCurrentProcess()
'    Dim spi As New McoreSystem.SingleProgramInstance(c.ProcessName)
'    If spi.IsSingleInstance Then
'        Application.Run(New Form1)
'    Else
'        Application.EnableVisualStyles()
'        Application.DoEvents()
'        MessageBox.Show(String.Format("There is already an instance of {0} running.", _
'        Application.ProductName), Application.ProductName, MessageBoxButtons.OK, _
'        MessageBoxIcon.Information)
'        spi.RaiseOtherProcess()
'    End If

'End Sub


'************************
'* Method 2
'***********************

'Dim RunningProcesses As Process() = Process.GetProcessesByName(c.ProcessName)
''Change the name above to suit your application

'If (RunningProcesses.Length = 1) Then
'    Application.Run(New Form1)
'Else
'    'ShowWindowAsync(RunningProcesses(0).MainWindowHandle, ShowWindowConstants.SW_SHOW)
'    Application.EnableVisualStyles()
'    Application.DoEvents()
'    

'End If

'Public Enum ShowWindowConstants

'    SW_HIDE = 0
'    SW_SHOWNORMAL = 1
'    SW_NORMAL = 1
'    SW_SHOWMINIMIZED = 2
'    SW_SHOWMAXIMIZED = 3
'    SW_MAXIMIZE = 3
'    SW_SHOWNOACTIVATE = 4
'    SW_SHOW = 5
'    SW_MINIMIZE = 6
'    SW_SHOWMINNOACTIVE = 7
'    SW_SHOWNA = 8
'    SW_RESTORE = 9
'    SW_SHOWDEFAULT = 10
'    SW_FORCEMINIMIZE = 11
'    SW_MAX = 11

'End Enum

'Imports System.Runtime.InteropServices
'<DllImport("User32.dll")> Public Shared Function ShowWindowAsync(ByVal hWnd As IntPtr, ByVal swCommand As Integer) As Integer
'End Function

'Dim strDir As String

'For Each strDir In Directory.GetDirectories(lbFolders.Items(count))

'    If Directory.GetFiles(strDir).Length > 0 Then

'        Dim strDirName As String = Path.GetFileName(strDir)
'        Console.WriteLine("**********************************************************")
'        Console.WriteLine(strDir)
'        Console.WriteLine("**********************************************************")

'        FileOpen(1, "C:\McoreIndexer.txt", OpenMode.Append)
'        PrintLine(1, "**********************************************************")
'        PrintLine(1, strDir)
'        PrintLine(1, "**********************************************************")
'        FileClose(1)


'        Dim strFile As String


'        For Each strFile In Directory.GetFiles(strDir)
'            Console.WriteLine(Path.GetFileName(strFile))
'            FileOpen(1, "C:\McoreIndexer.txt", OpenMode.Append)
'            PrintLine(1, Path.GetFileName(strFile))
'            FileClose(1)

'        Next

'    End If

'Next


'Public Function getAsciiSwitch() As String
'    If chkAscii.CheckState = CheckState.Checked Then
'        Return " /a"
'    End If
'    Return Nothing
'End Function

'Public Function getAddFilesSwitch() As String
'    If chkIndexFiles.CheckState = CheckState.Checked Then
'        Return " /f"
'    End If
'    Return Nothing
'End Function

'Public Function getOutputSwitch(ByVal folderPath As String) As String

'    If glbMode = IndexingMode.INDEX_FILE_IN_EACH_DIR Then
'        Return ">" + Chr(34) + folderPath + "\" + txtFileName.Text + cboExt.Text + Chr(34)
'    End If

'    If glbMode = IndexingMode.INDEX_FILE_IN_ONE_FOLDER Then

'        If chkSingleFile.CheckState = CheckState.Checked Then
'            Me.targetFilePath = txtOutputDir.Text + "\" + txtFileName.Text + cboExt.Text
'            If isFirstEntryToSingleFile = True Then
'                isFirstEntryToSingleFile = False
'                Return ">" + Chr(34) + targetFilePath + Chr(34)
'            Else
'                Return ">>" + Chr(34) + targetFilePath + Chr(34)
'            End If
'        Else
'            Dim strOutputFileName As String
'            If Path.GetFileName(folderPath).Length > 0 Then
'                strOutputFileName = Path.GetPathRoot(folderPath).Substring(0, 1) + "-" + Path.GetFileName(folderPath) + "-" + txtFileName.Text + cboExt.Text
'            Else
'                strOutputFileName = Path.GetPathRoot(folderPath).Substring(0, 1) + "-" + txtFileName.Text + cboExt.Text
'            End If
'            Return ">" + Chr(34) + txtOutputDir.Text + "\" + strOutputFileName + Chr(34)
'        End If

'    End If

'End Function

'Public Function getSourceSwitch(ByVal folderPath As String) As String
'    Return Chr(34) + folderPath + Chr(34)
'End Function

#End Region
