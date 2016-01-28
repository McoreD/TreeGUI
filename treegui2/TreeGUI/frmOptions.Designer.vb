<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Me.components = New System.ComponentModel.Container
        Me.txtJobsFolderPath = New System.Windows.Forms.TextBox
        Me.btnBrowseJobsFolder = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tpTasks = New System.Windows.Forms.TabPage
        Me.gpOpenDefaultConfig = New System.Windows.Forms.GroupBox
        Me.txtDefaultConfigFilePath = New System.Windows.Forms.TextBox
        Me.btnBrowseDefaultConfig = New System.Windows.Forms.Button
        Me.chkOpenDefaultConfig = New System.Windows.Forms.CheckBox
        Me.chkStartup = New System.Windows.Forms.CheckBox
        Me.chkWindowsService = New System.Windows.Forms.CheckBox
        Me.tpScheduleGlb = New System.Windows.Forms.TabPage
        Me.dtpTime = New System.Windows.Forms.DateTimePicker
        Me.chkIndexInterval = New System.Windows.Forms.CheckBox
        Me.chkScheduelTasksForGui = New System.Windows.Forms.CheckBox
        Me.gbDaysOfWeek = New System.Windows.Forms.GroupBox
        Me.chkSaturday = New System.Windows.Forms.CheckBox
        Me.chkWednesday = New System.Windows.Forms.CheckBox
        Me.chkSunday = New System.Windows.Forms.CheckBox
        Me.chkFriday = New System.Windows.Forms.CheckBox
        Me.chkThursday = New System.Windows.Forms.CheckBox
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.chkTuesday = New System.Windows.Forms.CheckBox
        Me.chkMonday = New System.Windows.Forms.CheckBox
        Me.nudInterval = New System.Windows.Forms.NumericUpDown
        Me.lblMinutes = New System.Windows.Forms.Label
        Me.chkIndexTime = New System.Windows.Forms.CheckBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.chkRemWinSize = New System.Windows.Forms.CheckBox
        Me.chkRemWinLocation = New System.Windows.Forms.CheckBox
        Me.chkStartMaximized = New System.Windows.Forms.CheckBox
        Me.chkCloseToTray = New System.Windows.Forms.CheckBox
        Me.chkMinimizeToTray = New System.Windows.Forms.CheckBox
        Me.chkSystemTray = New System.Windows.Forms.CheckBox
        Me.chkLoadToTray = New System.Windows.Forms.CheckBox
        Me.tpAdvanced = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnRestoreOptions = New System.Windows.Forms.Button
        Me.btnBackupOptions = New System.Windows.Forms.Button
        Me.btnIndexAll = New System.Windows.Forms.Button
        Me.gbAdminOptions = New System.Windows.Forms.GroupBox
        Me.btnFixWindowsServicePath = New System.Windows.Forms.Button
        Me.btnUninstallWindowsService = New System.Windows.Forms.Button
        Me.btnAssociateTgc = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.hpHelp = New System.Windows.Forms.HelpProvider
        Me.ttTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnContextMenu = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tpTasks.SuspendLayout()
        Me.gpOpenDefaultConfig.SuspendLayout()
        Me.tpScheduleGlb.SuspendLayout()
        Me.gbDaysOfWeek.SuspendLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.tpAdvanced.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.gbAdminOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtJobsFolderPath
        '
        Me.txtJobsFolderPath.Location = New System.Drawing.Point(6, 19)
        Me.txtJobsFolderPath.Name = "txtJobsFolderPath"
        Me.txtJobsFolderPath.ReadOnly = True
        Me.txtJobsFolderPath.Size = New System.Drawing.Size(239, 20)
        Me.txtJobsFolderPath.TabIndex = 0
        '
        'btnBrowseJobsFolder
        '
        Me.btnBrowseJobsFolder.Location = New System.Drawing.Point(251, 17)
        Me.btnBrowseJobsFolder.Name = "btnBrowseJobsFolder"
        Me.btnBrowseJobsFolder.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseJobsFolder.TabIndex = 1
        Me.btnBrowseJobsFolder.Text = "&Browse"
        Me.btnBrowseJobsFolder.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtJobsFolderPath)
        Me.GroupBox1.Controls.Add(Me.btnBrowseJobsFolder)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(335, 54)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tasks Folder"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpTasks)
        Me.TabControl1.Controls.Add(Me.tpScheduleGlb)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.tpAdvanced)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(375, 285)
        Me.TabControl1.TabIndex = 7
        '
        'tpTasks
        '
        Me.tpTasks.Controls.Add(Me.gpOpenDefaultConfig)
        Me.tpTasks.Controls.Add(Me.chkOpenDefaultConfig)
        Me.tpTasks.Controls.Add(Me.chkStartup)
        Me.tpTasks.Controls.Add(Me.chkWindowsService)
        Me.tpTasks.Controls.Add(Me.GroupBox1)
        Me.tpTasks.Location = New System.Drawing.Point(4, 22)
        Me.tpTasks.Name = "tpTasks"
        Me.tpTasks.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTasks.Size = New System.Drawing.Size(367, 259)
        Me.tpTasks.TabIndex = 1
        Me.tpTasks.Text = "General"
        Me.tpTasks.UseVisualStyleBackColor = True
        '
        'gpOpenDefaultConfig
        '
        Me.gpOpenDefaultConfig.Controls.Add(Me.txtDefaultConfigFilePath)
        Me.gpOpenDefaultConfig.Controls.Add(Me.btnBrowseDefaultConfig)
        Me.gpOpenDefaultConfig.Location = New System.Drawing.Point(17, 147)
        Me.gpOpenDefaultConfig.Name = "gpOpenDefaultConfig"
        Me.gpOpenDefaultConfig.Size = New System.Drawing.Size(335, 54)
        Me.gpOpenDefaultConfig.TabIndex = 3
        Me.gpOpenDefaultConfig.TabStop = False
        Me.gpOpenDefaultConfig.Text = "Default config file (also used for Shell Context Menu)"
        '
        'txtDefaultConfigFilePath
        '
        Me.txtDefaultConfigFilePath.Location = New System.Drawing.Point(6, 19)
        Me.txtDefaultConfigFilePath.Name = "txtDefaultConfigFilePath"
        Me.txtDefaultConfigFilePath.ReadOnly = True
        Me.txtDefaultConfigFilePath.Size = New System.Drawing.Size(239, 20)
        Me.txtDefaultConfigFilePath.TabIndex = 0
        '
        'btnBrowseDefaultConfig
        '
        Me.btnBrowseDefaultConfig.Location = New System.Drawing.Point(251, 17)
        Me.btnBrowseDefaultConfig.Name = "btnBrowseDefaultConfig"
        Me.btnBrowseDefaultConfig.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseDefaultConfig.TabIndex = 1
        Me.btnBrowseDefaultConfig.Text = "&Browse"
        Me.btnBrowseDefaultConfig.UseVisualStyleBackColor = True
        '
        'chkOpenDefaultConfig
        '
        Me.chkOpenDefaultConfig.AutoSize = True
        Me.chkOpenDefaultConfig.Location = New System.Drawing.Point(17, 124)
        Me.chkOpenDefaultConfig.Name = "chkOpenDefaultConfig"
        Me.chkOpenDefaultConfig.Size = New System.Drawing.Size(301, 17)
        Me.chkOpenDefaultConfig.TabIndex = 5
        Me.chkOpenDefaultConfig.Text = "Automatically open Default config file when TreeGUI loads"
        Me.chkOpenDefaultConfig.UseVisualStyleBackColor = True
        '
        'chkStartup
        '
        Me.chkStartup.AutoSize = True
        Me.chkStartup.Location = New System.Drawing.Point(17, 18)
        Me.chkStartup.Name = "chkStartup"
        Me.chkStartup.Size = New System.Drawing.Size(129, 17)
        Me.chkStartup.TabIndex = 3
        Me.chkStartup.Text = "Run as a Startup Item"
        Me.chkStartup.UseVisualStyleBackColor = True
        '
        'chkWindowsService
        '
        Me.chkWindowsService.AutoSize = True
        Me.chkWindowsService.Location = New System.Drawing.Point(17, 41)
        Me.chkWindowsService.Name = "chkWindowsService"
        Me.chkWindowsService.Size = New System.Drawing.Size(155, 17)
        Me.chkWindowsService.TabIndex = 4
        Me.chkWindowsService.Text = "Run as a Windows Service"
        Me.chkWindowsService.UseVisualStyleBackColor = True
        '
        'tpScheduleGlb
        '
        Me.tpScheduleGlb.Controls.Add(Me.dtpTime)
        Me.tpScheduleGlb.Controls.Add(Me.chkIndexInterval)
        Me.tpScheduleGlb.Controls.Add(Me.chkScheduelTasksForGui)
        Me.tpScheduleGlb.Controls.Add(Me.gbDaysOfWeek)
        Me.tpScheduleGlb.Controls.Add(Me.nudInterval)
        Me.tpScheduleGlb.Controls.Add(Me.lblMinutes)
        Me.tpScheduleGlb.Controls.Add(Me.chkIndexTime)
        Me.tpScheduleGlb.Location = New System.Drawing.Point(4, 22)
        Me.tpScheduleGlb.Name = "tpScheduleGlb"
        Me.tpScheduleGlb.Padding = New System.Windows.Forms.Padding(3)
        Me.tpScheduleGlb.Size = New System.Drawing.Size(367, 259)
        Me.tpScheduleGlb.TabIndex = 3
        Me.tpScheduleGlb.Text = "Schedule"
        Me.tpScheduleGlb.UseVisualStyleBackColor = True
        '
        'dtpTime
        '
        Me.dtpTime.CustomFormat = "HH:mm:ss"
        Me.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpTime.Location = New System.Drawing.Point(164, 65)
        Me.dtpTime.Name = "dtpTime"
        Me.dtpTime.ShowUpDown = True
        Me.dtpTime.Size = New System.Drawing.Size(102, 20)
        Me.dtpTime.TabIndex = 5
        '
        'chkIndexInterval
        '
        Me.chkIndexInterval.AutoSize = True
        Me.chkIndexInterval.Location = New System.Drawing.Point(18, 42)
        Me.chkIndexInterval.Name = "chkIndexInterval"
        Me.chkIndexInterval.Size = New System.Drawing.Size(81, 17)
        Me.chkIndexInterval.TabIndex = 0
        Me.chkIndexInterval.Text = "Index every"
        Me.chkIndexInterval.UseVisualStyleBackColor = True
        '
        'chkScheduelTasksForGui
        '
        Me.chkScheduelTasksForGui.AutoSize = True
        Me.chkScheduelTasksForGui.Location = New System.Drawing.Point(18, 18)
        Me.chkScheduelTasksForGui.Name = "chkScheduelTasksForGui"
        Me.chkScheduelTasksForGui.Size = New System.Drawing.Size(208, 17)
        Me.chkScheduelTasksForGui.TabIndex = 2
        Me.chkScheduelTasksForGui.Text = "Enable Scheduled Tasks in GUI Mode"
        Me.chkScheduelTasksForGui.UseVisualStyleBackColor = True
        '
        'gbDaysOfWeek
        '
        Me.gbDaysOfWeek.Controls.Add(Me.chkSaturday)
        Me.gbDaysOfWeek.Controls.Add(Me.chkWednesday)
        Me.gbDaysOfWeek.Controls.Add(Me.chkSunday)
        Me.gbDaysOfWeek.Controls.Add(Me.chkFriday)
        Me.gbDaysOfWeek.Controls.Add(Me.chkThursday)
        Me.gbDaysOfWeek.Controls.Add(Me.chkSelectAll)
        Me.gbDaysOfWeek.Controls.Add(Me.chkTuesday)
        Me.gbDaysOfWeek.Controls.Add(Me.chkMonday)
        Me.gbDaysOfWeek.Location = New System.Drawing.Point(18, 91)
        Me.gbDaysOfWeek.Name = "gbDaysOfWeek"
        Me.gbDaysOfWeek.Size = New System.Drawing.Size(269, 98)
        Me.gbDaysOfWeek.TabIndex = 4
        Me.gbDaysOfWeek.TabStop = False
        Me.gbDaysOfWeek.Text = "Days of the Week"
        '
        'chkSaturday
        '
        Me.chkSaturday.AutoSize = True
        Me.chkSaturday.Location = New System.Drawing.Point(196, 19)
        Me.chkSaturday.Name = "chkSaturday"
        Me.chkSaturday.Size = New System.Drawing.Size(68, 17)
        Me.chkSaturday.TabIndex = 7
        Me.chkSaturday.Text = "Saturday"
        Me.chkSaturday.UseVisualStyleBackColor = True
        '
        'chkWednesday
        '
        Me.chkWednesday.AutoSize = True
        Me.chkWednesday.Location = New System.Drawing.Point(19, 42)
        Me.chkWednesday.Name = "chkWednesday"
        Me.chkWednesday.Size = New System.Drawing.Size(83, 17)
        Me.chkWednesday.TabIndex = 6
        Me.chkWednesday.Text = "Wednesday"
        Me.chkWednesday.UseVisualStyleBackColor = True
        '
        'chkSunday
        '
        Me.chkSunday.AutoSize = True
        Me.chkSunday.Location = New System.Drawing.Point(196, 42)
        Me.chkSunday.Name = "chkSunday"
        Me.chkSunday.Size = New System.Drawing.Size(62, 17)
        Me.chkSunday.TabIndex = 5
        Me.chkSunday.Text = "Sunday"
        Me.chkSunday.UseVisualStyleBackColor = True
        '
        'chkFriday
        '
        Me.chkFriday.AutoSize = True
        Me.chkFriday.Location = New System.Drawing.Point(19, 65)
        Me.chkFriday.Name = "chkFriday"
        Me.chkFriday.Size = New System.Drawing.Size(54, 17)
        Me.chkFriday.TabIndex = 4
        Me.chkFriday.Text = "Friday"
        Me.chkFriday.UseVisualStyleBackColor = True
        '
        'chkThursday
        '
        Me.chkThursday.AutoSize = True
        Me.chkThursday.Location = New System.Drawing.Point(106, 42)
        Me.chkThursday.Name = "chkThursday"
        Me.chkThursday.Size = New System.Drawing.Size(70, 17)
        Me.chkThursday.TabIndex = 3
        Me.chkThursday.Text = "Thursday"
        Me.chkThursday.UseVisualStyleBackColor = True
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(106, 65)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(70, 17)
        Me.chkSelectAll.TabIndex = 2
        Me.chkSelectAll.Text = "Select &All"
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'chkTuesday
        '
        Me.chkTuesday.AutoSize = True
        Me.chkTuesday.Location = New System.Drawing.Point(106, 19)
        Me.chkTuesday.Name = "chkTuesday"
        Me.chkTuesday.Size = New System.Drawing.Size(67, 17)
        Me.chkTuesday.TabIndex = 1
        Me.chkTuesday.Text = "Tuesday"
        Me.chkTuesday.UseVisualStyleBackColor = True
        '
        'chkMonday
        '
        Me.chkMonday.AutoSize = True
        Me.chkMonday.Location = New System.Drawing.Point(19, 19)
        Me.chkMonday.Name = "chkMonday"
        Me.chkMonday.Size = New System.Drawing.Size(64, 17)
        Me.chkMonday.TabIndex = 0
        Me.chkMonday.Text = "&Monday"
        Me.chkMonday.UseVisualStyleBackColor = True
        '
        'nudInterval
        '
        Me.nudInterval.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudInterval.Location = New System.Drawing.Point(112, 41)
        Me.nudInterval.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.nudInterval.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudInterval.Name = "nudInterval"
        Me.nudInterval.Size = New System.Drawing.Size(100, 20)
        Me.nudInterval.TabIndex = 1
        Me.nudInterval.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'lblMinutes
        '
        Me.lblMinutes.AutoSize = True
        Me.lblMinutes.Location = New System.Drawing.Point(218, 42)
        Me.lblMinutes.Name = "lblMinutes"
        Me.lblMinutes.Size = New System.Drawing.Size(43, 13)
        Me.lblMinutes.TabIndex = 2
        Me.lblMinutes.Text = "minutes"
        '
        'chkIndexTime
        '
        Me.chkIndexTime.AutoSize = True
        Me.chkIndexTime.Location = New System.Drawing.Point(18, 65)
        Me.chkIndexTime.Name = "chkIndexTime"
        Me.chkIndexTime.Size = New System.Drawing.Size(140, 17)
        Me.chkIndexTime.TabIndex = 3
        Me.chkIndexTime.Text = "Index at a specified time"
        Me.chkIndexTime.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.chkRemWinSize)
        Me.TabPage3.Controls.Add(Me.chkRemWinLocation)
        Me.TabPage3.Controls.Add(Me.chkStartMaximized)
        Me.TabPage3.Controls.Add(Me.chkCloseToTray)
        Me.TabPage3.Controls.Add(Me.chkMinimizeToTray)
        Me.TabPage3.Controls.Add(Me.chkSystemTray)
        Me.TabPage3.Controls.Add(Me.chkLoadToTray)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(367, 259)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Appearance"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'chkRemWinSize
        '
        Me.chkRemWinSize.AutoSize = True
        Me.chkRemWinSize.Location = New System.Drawing.Point(17, 155)
        Me.chkRemWinSize.Name = "chkRemWinSize"
        Me.chkRemWinSize.Size = New System.Drawing.Size(142, 17)
        Me.chkRemWinSize.TabIndex = 6
        Me.chkRemWinSize.Text = "Remember Window &Size"
        Me.chkRemWinSize.UseVisualStyleBackColor = True
        '
        'chkRemWinLocation
        '
        Me.chkRemWinLocation.AutoSize = True
        Me.chkRemWinLocation.Location = New System.Drawing.Point(17, 132)
        Me.chkRemWinLocation.Name = "chkRemWinLocation"
        Me.chkRemWinLocation.Size = New System.Drawing.Size(163, 17)
        Me.chkRemWinLocation.TabIndex = 5
        Me.chkRemWinLocation.Text = "Remember Window &Location"
        Me.chkRemWinLocation.UseVisualStyleBackColor = True
        '
        'chkStartMaximized
        '
        Me.chkStartMaximized.AutoSize = True
        Me.chkStartMaximized.Location = New System.Drawing.Point(17, 109)
        Me.chkStartMaximized.Name = "chkStartMaximized"
        Me.chkStartMaximized.Size = New System.Drawing.Size(100, 17)
        Me.chkStartMaximized.TabIndex = 4
        Me.chkStartMaximized.Text = "Start &Maximized"
        Me.chkStartMaximized.UseVisualStyleBackColor = True
        '
        'chkCloseToTray
        '
        Me.chkCloseToTray.AutoSize = True
        Me.chkCloseToTray.Location = New System.Drawing.Point(17, 86)
        Me.chkCloseToTray.Name = "chkCloseToTray"
        Me.chkCloseToTray.Size = New System.Drawing.Size(125, 17)
        Me.chkCloseToTray.TabIndex = 3
        Me.chkCloseToTray.Text = "Close to System Tray"
        Me.chkCloseToTray.UseVisualStyleBackColor = True
        '
        'chkMinimizeToTray
        '
        Me.chkMinimizeToTray.AutoSize = True
        Me.chkMinimizeToTray.Location = New System.Drawing.Point(17, 63)
        Me.chkMinimizeToTray.Name = "chkMinimizeToTray"
        Me.chkMinimizeToTray.Size = New System.Drawing.Size(139, 17)
        Me.chkMinimizeToTray.TabIndex = 2
        Me.chkMinimizeToTray.Text = "Minimise to System Tray"
        Me.chkMinimizeToTray.UseVisualStyleBackColor = True
        '
        'chkSystemTray
        '
        Me.chkSystemTray.AutoSize = True
        Me.chkSystemTray.Location = New System.Drawing.Point(17, 17)
        Me.chkSystemTray.Name = "chkSystemTray"
        Me.chkSystemTray.Size = New System.Drawing.Size(188, 17)
        Me.chkSystemTray.TabIndex = 0
        Me.chkSystemTray.Text = "Enable TreeGUI System Tray Icon"
        Me.chkSystemTray.UseVisualStyleBackColor = True
        '
        'chkLoadToTray
        '
        Me.chkLoadToTray.AutoSize = True
        Me.chkLoadToTray.Location = New System.Drawing.Point(17, 40)
        Me.chkLoadToTray.Name = "chkLoadToTray"
        Me.chkLoadToTray.Size = New System.Drawing.Size(172, 17)
        Me.chkLoadToTray.TabIndex = 1
        Me.chkLoadToTray.Text = "Do not show TreeGUI on Load"
        Me.chkLoadToTray.UseVisualStyleBackColor = True
        '
        'tpAdvanced
        '
        Me.tpAdvanced.Controls.Add(Me.GroupBox3)
        Me.tpAdvanced.Controls.Add(Me.gbAdminOptions)
        Me.tpAdvanced.Location = New System.Drawing.Point(4, 22)
        Me.tpAdvanced.Name = "tpAdvanced"
        Me.tpAdvanced.Padding = New System.Windows.Forms.Padding(3)
        Me.tpAdvanced.Size = New System.Drawing.Size(367, 259)
        Me.tpAdvanced.TabIndex = 4
        Me.tpAdvanced.Text = "Advanced"
        Me.tpAdvanced.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnRestoreOptions)
        Me.GroupBox3.Controls.Add(Me.btnBackupOptions)
        Me.GroupBox3.Controls.Add(Me.btnIndexAll)
        Me.GroupBox3.Location = New System.Drawing.Point(15, 16)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(324, 82)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "User Options"
        '
        'btnRestoreOptions
        '
        Me.btnRestoreOptions.Location = New System.Drawing.Point(163, 48)
        Me.btnRestoreOptions.Name = "btnRestoreOptions"
        Me.btnRestoreOptions.Size = New System.Drawing.Size(154, 23)
        Me.btnRestoreOptions.TabIndex = 4
        Me.btnRestoreOptions.Text = "&Restore Options File..."
        Me.btnRestoreOptions.UseVisualStyleBackColor = True
        '
        'btnBackupOptions
        '
        Me.btnBackupOptions.Location = New System.Drawing.Point(6, 48)
        Me.btnBackupOptions.Name = "btnBackupOptions"
        Me.btnBackupOptions.Size = New System.Drawing.Size(151, 23)
        Me.btnBackupOptions.TabIndex = 3
        Me.btnBackupOptions.Text = "&Backup Options File..."
        Me.btnBackupOptions.UseVisualStyleBackColor = True
        '
        'btnIndexAll
        '
        Me.btnIndexAll.Location = New System.Drawing.Point(6, 19)
        Me.btnIndexAll.Name = "btnIndexAll"
        Me.btnIndexAll.Size = New System.Drawing.Size(313, 23)
        Me.btnIndexAll.TabIndex = 2
        Me.btnIndexAll.Text = "&Index using all config files in Tasks folder"
        Me.btnIndexAll.UseVisualStyleBackColor = True
        '
        'gbAdminOptions
        '
        Me.gbAdminOptions.Controls.Add(Me.btnContextMenu)
        Me.gbAdminOptions.Controls.Add(Me.btnFixWindowsServicePath)
        Me.gbAdminOptions.Controls.Add(Me.btnUninstallWindowsService)
        Me.gbAdminOptions.Controls.Add(Me.btnAssociateTgc)
        Me.gbAdminOptions.Location = New System.Drawing.Point(15, 104)
        Me.gbAdminOptions.Name = "gbAdminOptions"
        Me.gbAdminOptions.Size = New System.Drawing.Size(324, 140)
        Me.gbAdminOptions.TabIndex = 4
        Me.gbAdminOptions.TabStop = False
        Me.gbAdminOptions.Text = "Administrator Options"
        '
        'btnFixWindowsServicePath
        '
        Me.btnFixWindowsServicePath.Location = New System.Drawing.Point(6, 19)
        Me.btnFixWindowsServicePath.Name = "btnFixWindowsServicePath"
        Me.btnFixWindowsServicePath.Size = New System.Drawing.Size(312, 23)
        Me.btnFixWindowsServicePath.TabIndex = 3
        Me.btnFixWindowsServicePath.Text = "&Fix McoreIndexer Windows Service Path"
        Me.btnFixWindowsServicePath.UseVisualStyleBackColor = True
        '
        'btnUninstallWindowsService
        '
        Me.btnUninstallWindowsService.Location = New System.Drawing.Point(6, 48)
        Me.btnUninstallWindowsService.Name = "btnUninstallWindowsService"
        Me.btnUninstallWindowsService.Size = New System.Drawing.Size(312, 23)
        Me.btnUninstallWindowsService.TabIndex = 1
        Me.btnUninstallWindowsService.Text = "&Uninstall McoreIndexer Windows Service..."
        Me.btnUninstallWindowsService.UseVisualStyleBackColor = True
        '
        'btnAssociateTgc
        '
        Me.btnAssociateTgc.Location = New System.Drawing.Point(5, 77)
        Me.btnAssociateTgc.Name = "btnAssociateTgc"
        Me.btnAssociateTgc.Size = New System.Drawing.Size(312, 23)
        Me.btnAssociateTgc.TabIndex = 0
        Me.btnAssociateTgc.Text = "Associate .tgc files with TreeGUI"
        Me.btnAssociateTgc.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(285, 303)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(204, 303)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnContextMenu
        '
        Me.btnContextMenu.Location = New System.Drawing.Point(6, 108)
        Me.btnContextMenu.Name = "btnContextMenu"
        Me.btnContextMenu.Size = New System.Drawing.Size(311, 23)
        Me.btnContextMenu.TabIndex = 4
        Me.btnContextMenu.Text = "Integrate TreeGUI into Shell Context Menu"
        Me.btnContextMenu.UseVisualStyleBackColor = True
        '
        'frmOptions
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(399, 341)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmOptions"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tpTasks.ResumeLayout(False)
        Me.tpTasks.PerformLayout()
        Me.gpOpenDefaultConfig.ResumeLayout(False)
        Me.gpOpenDefaultConfig.PerformLayout()
        Me.tpScheduleGlb.ResumeLayout(False)
        Me.tpScheduleGlb.PerformLayout()
        Me.gbDaysOfWeek.ResumeLayout(False)
        Me.gbDaysOfWeek.PerformLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.tpAdvanced.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.gbAdminOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtJobsFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseJobsFolder As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents nudInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkIndexInterval As System.Windows.Forms.CheckBox
    Friend WithEvents lblMinutes As System.Windows.Forms.Label
    Friend WithEvents chkIndexTime As System.Windows.Forms.CheckBox
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents gbDaysOfWeek As System.Windows.Forms.GroupBox
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkTuesday As System.Windows.Forms.CheckBox
    Friend WithEvents chkMonday As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaturday As System.Windows.Forms.CheckBox
    Friend WithEvents chkWednesday As System.Windows.Forms.CheckBox
    Friend WithEvents chkSunday As System.Windows.Forms.CheckBox
    Friend WithEvents chkFriday As System.Windows.Forms.CheckBox
    Friend WithEvents chkThursday As System.Windows.Forms.CheckBox
    Friend WithEvents chkLoadToTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkSystemTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkCloseToTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkMinimizeToTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkScheduelTasksForGui As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpTasks As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents chkStartup As System.Windows.Forms.CheckBox
    Friend WithEvents chkWindowsService As System.Windows.Forms.CheckBox
    Friend WithEvents tpScheduleGlb As System.Windows.Forms.TabPage
    Friend WithEvents tpAdvanced As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBackupOptions As System.Windows.Forms.Button
    Friend WithEvents btnIndexAll As System.Windows.Forms.Button
    Friend WithEvents gbAdminOptions As System.Windows.Forms.GroupBox
    Friend WithEvents btnFixWindowsServicePath As System.Windows.Forms.Button
    Friend WithEvents btnUninstallWindowsService As System.Windows.Forms.Button
    Friend WithEvents btnAssociateTgc As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents hpHelp As System.Windows.Forms.HelpProvider
    Friend WithEvents ttTip As System.Windows.Forms.ToolTip
    Friend WithEvents btnRestoreOptions As System.Windows.Forms.Button
    Friend WithEvents gpOpenDefaultConfig As System.Windows.Forms.GroupBox
    Friend WithEvents txtDefaultConfigFilePath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseDefaultConfig As System.Windows.Forms.Button
    Friend WithEvents chkOpenDefaultConfig As System.Windows.Forms.CheckBox
    Friend WithEvents chkStartMaximized As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemWinSize As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemWinLocation As System.Windows.Forms.CheckBox
    Friend WithEvents btnContextMenu As System.Windows.Forms.Button
End Class
