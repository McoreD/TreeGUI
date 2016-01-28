Imports System.IO
Imports McoreSystem

Public Class frmForm1

    Const URL_UPDATE_DOWNLOAD As String = "http://optusnet.dl.sourceforge.net/sourceforge/treegui/"
    ' URL_UPDATE_CHECK has to be a very reliable link
    Const URL_UPDATE_CHECK As String = "http://wmwiki.com/mcored/"

    Private Const URL_SUPPORT_FORUM As String = "http://sourceforge.net/forum/forum.php?forum_id=524605"

    Private mExitApplication As Boolean = False

    Private mAppInfo As New McoreSystem.AppInfo(Application.ProductName,
                        Application.ProductVersion,
                        AppInfo.SoftwareCycle.BETA)

    Private Const READY As String = "Ready"

    Private m_UndoRedoStack As New cUndoRedoStack

    Private WithEvents m_MruList As MruList

    Dim mIndexer As cIndexer

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        mExitApplication = True
        Me.Close()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        My.Forms.frmAboutBox1.ShowDialog()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click

        ' Since v2.4.1.0
        ' Load Latest Options File Settings from File

        mSettings.LoadOptionsFile()
        Me.sbarLeft.Text = "Loaded " & mSettings.GetAdapter.GetOptionsFilePath
        My.Forms.frmOptions.Focus()
        My.Forms.frmOptions.ShowDialog()

    End Sub

    Private Sub sProcessArguments()

        Try
            Dim args() As String = Environment.GetCommandLineArgs()
            If args.Length > 1 Then
                Dim firstArg As String = args(1).ToString
                If Directory.Exists(firstArg) Then
                    If fExportIndex(firstArg) = True Then
                        Me.Close()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmForm1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop

        Dim DropContents As String() = CType(e.Data.GetData(DataFormats.FileDrop, True), String())

        Dim listDirs As New List(Of String)
        For Each fileOrDir As String In DropContents
            If Directory.Exists(fileOrDir) Then
                listDirs.Add(fileOrDir)
            End If
        Next

        For Each dir As String In listDirs
            ssAddFolder(dir)
        Next

    End Sub

    Private Sub frmForm1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub frmForm1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If mSettings.GetAdapter.GetOptions.ForceSave Then
            If mSettings.GetConfigFilePath Is Nothing Then
                mSettings.SetConfigFilePath(mSettings.GetAdapter.GetOptions.TasksFolderPath + "\" + mSettings.NEW_CONFIG_NAME)
            End If
            mSettings.WriteConfigFile(mSettings.GetConfigFilePath)
        Else
            If mExitApplication OrElse Not mSettings.GetAdapter.GetOptions.CloseToTray Then
                e.Cancel = fCheckFileNotSaved()
            Else
                e.Cancel = mSettings.GetAdapter.GetOptions.CloseToTray
                If e.Cancel Then
                    Me.WindowState = FormWindowState.Minimized
                    Me.ShowInTaskbar = False
                End If
            End If

        End If

        ' Save Main Form
        sSettingsSave()

    End Sub

    Private Sub sSettingsSave()

        My.Settings.FormLeft = Me.Left
        My.Settings.FormTop = Me.Top
        My.Settings.FormWidth = Me.Width
        My.Settings.FormHeight = Me.Height

        mSettings.GetAdapter.GetOptions.AlwaysOnTop = Me.AlwaysOnTopToolStripMenuItem.Checked

    End Sub

    Private Sub updateAppTitle(Optional ByVal filePath As String = Nothing)

        Me.Text = Me.mAppInfo.GetApplicationTitle '+ APP_TAG
        miToolsProperties.Text = String.Format("{0} Properties...", mSettings.GetConfigFileName)

        If Not filePath = Nothing Then

            Dim fileName As String = IO.Path.GetFileNameWithoutExtension(filePath)

            If mSettings.GetAdapter.IsConfigFileReadOnly(mSettings.GetConfigFilePath) Then
                Me.Text = String.Format("{0} - [{1} ({2})]", Me.Text, fileName, "Read-Only")
            Else
                Me.Text = String.Format("{0} - [{1}]", Me.Text, fileName)
            End If

            miToolsProperties.Text = String.Format("{0} Properties...", fileName)

        End If

        niTray.Text = mAppInfo.GetApplicationTitle
        cmShowApp.Text = String.Format("&Show {0}...", niTray.Text)

    End Sub

    Private Sub frmForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        mSettings.LoadOptionsFile()
        sProcessArguments()

        If mSettings.GetAdapter.GetOptions.RememberWindowSize Then 'war59312
            If My.Settings.FormHeight > Me.Height AndAlso My.Settings.FormWidth > Me.Width Then
                Me.Size = New Size(My.Settings.FormWidth, My.Settings.FormHeight)
            End If
        End If

        If mSettings.GetAdapter.GetOptions.RememberWindowLocation Then 'war59312
            If My.Settings.FormLeft > 0 AndAlso My.Settings.FormTop > 0 Then
                Me.Left = My.Settings.FormLeft
                Me.Top = My.Settings.FormTop
            End If
        End If

        m_MruList = New MruList(Application.ProductName, FileToolStripMenuItem, 4)

        ' Load Main Form

        Me.AlwaysOnTopToolStripMenuItem.Checked = mSettings.GetAdapter.GetOptions.AlwaysOnTop
        Me.TopMost = Me.AlwaysOnTopToolStripMenuItem.Checked

        niTray.Visible = mSettings.GetAdapter.GetOptions.TrayIconIsEnabled
        If mSettings.GetAdapter.GetOptions.TrayOnLoad Then
            Me.WindowState = FormWindowState.Minimized
            Me.ShowInTaskbar = False
        Else
            If mSettings.GetAdapter.GetOptions.MaximizedWindow Then
                Me.WindowState = FormWindowState.Maximized
            Else
                Me.WindowState = FormWindowState.Normal
            End If
        End If

        If mSettings.GetAdapter.GetOptions.RunTasksInGUI Then
            If mSettings.GetAdapter.GetOptions.IsIndexingIntervalEnabled Then
                Console.WriteLine("Indexing according to Interval...")
                tmrScheduleInterval.Enabled = True
                tmrScheduleInterval.Start()
            End If
        End If

        'updateAppTitle()
        updateStatusBar()
        'Let Form_Shown to do the rest

        For i As Integer = 2 To 15
            cboSubFolderLevels.Items.Add(i & " Levels")
        Next
        cboSubFolderLevels.SelectedIndex = 1

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call addFolder()
    End Sub

    Private Sub addFolder()

        Dim dlg As New McoreSystem.FolderBrowser()
        dlg.Title = "Add directory to index"
        dlg.Flags = McoreSystem.BrowseFlags.BIF_NEWDIALOGSTYLE Or
                    McoreSystem.BrowseFlags.BIF_STATUSTEXT Or
                    McoreSystem.BrowseFlags.BIF_EDITBOX
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

            '1.4.0.1 It was possible to add My Computer and cause Tree.com to crash

            If dlg.DirectoryPath.Length > 0 Then

                ssAddFolder(dlg.DirectoryPath)

            End If
        End If

    End Sub

    Private Sub ssAddFolder(ByVal folderPath As String)

        lbFolders.Items.Add(folderPath, True)
        'mSettings.isConfigFileEdited = True
        mSettings.SaveConfigForms() 'Keep Memory Up-to-date
        Call AddFoldersToHistory()
        Me.updateGuiControls()

    End Sub

    Private Sub AddFoldersToHistory()
        Dim temp As New ArrayList
        For Each item As String In lbFolders.Items
            temp.Add(item)
        Next
        m_UndoRedoStack.Folders = temp
        UndoToolStripMenuItem.Enabled = m_UndoRedoStack.CanUndo
        RedoToolStripMenuItem.Enabled = m_UndoRedoStack.CanRedo
    End Sub

    Dim mLevels As Integer = 0
    Private Sub addSubfolders(ByVal recursive As Boolean)

        Dim dlg As New McoreSystem.FolderBrowser
        If recursive Then
            dlg.Title = "Choose a directory to index subfolders (recursive)"
        Else
            dlg.Title = "Choose a directory to index subfolders"
        End If

        dlg.Flags = McoreSystem.BrowseFlags.BIF_NEWDIALOGSTYLE Or
                    McoreSystem.BrowseFlags.BIF_STATUSTEXT Or
                    McoreSystem.BrowseFlags.BIF_EDITBOX

        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

            If recursive = True Then

                lbFolders.Items.Add(dlg.DirectoryPath, True) ' add root folder
                For Each strDir As String In Directory.GetDirectories(dlg.DirectoryPath)
                    lbFolders.Items.Add(strDir, True) ' add level 1
                    addSubfolders(strDir, 0)
                Next

            Else

                lbFolders.Items.Add(dlg.DirectoryPath, True)
                For Each strDir As String In Directory.GetDirectories(dlg.DirectoryPath)
                    lbFolders.Items.Add(strDir, True)
                Next

            End If

            Call AddFoldersToHistory()
            Me.updateGuiControls()
            mSettings.SaveConfigForms() 'Keep Memory Up-to-date

        End If
    End Sub

    Private Sub addSubfolders(ByVal rootFolderPath As String, ByVal depth As Integer)

        If depth < mLevels Then

            depth += 1

            Dim strDir As String = ""

            Try

                For Each strDir In Directory.GetDirectories(rootFolderPath)
                    lbFolders.Items.Add(strDir, True) ' add level 2
                    addSubfolders(strDir, depth)
                Next

            Catch ex As System.UnauthorizedAccessException

            End Try

        End If

    End Sub

    Private Sub GetFoldersFromHistory()

        lbFolders.Items.Clear()
        Dim temp As New ArrayList
        temp = m_UndoRedoStack.Folders
        For Each item As String In temp
            lbFolders.Items.Add(item, True)
        Next
        UndoToolStripMenuItem.Enabled = m_UndoRedoStack.CanUndo
        RedoToolStripMenuItem.Enabled = m_UndoRedoStack.CanRedo

    End Sub

    Private Sub updateStatusBar()

        '* Status Bar
        sbarLeft.Text = "Ready"
        If lbFolders.Items.Count > 1 OrElse lbFolders.Items.Count = 0 Then
            sbarRight.Text = String.Format("{0} folders ready to index", lbFolders.Items.Count)
        Else
            sbarRight.Text = String.Format("{0} folder ready to index", lbFolders.Items.Count)
        End If

    End Sub

    Private Sub updateGuiControls()

        '* Update Buttons

        updateUpDownButtons()

        '* Updating Folders menu
        ShowInWindowsExplorerToolStripMenuItem.Enabled = lbFolders.SelectedIndex <> -1
        OpenIndexFileOfThisFolderToolStripMenuItem.Enabled = lbFolders.SelectedIndex <> -1
        IndexThisFolderToolStripMenuItem.Enabled = lbFolders.SelectedIndex <> -1
        RemoveThisFolderToolStripMenuItem.Enabled = lbFolders.SelectedIndex <> -1
        RemoveAllFolderEntriesToolStripMenuItem.Enabled = lbFolders.Items.Count > 0
        SetVirtualFolderNameToolStripMenuItem.Enabled = lbFolders.SelectedIndex <> -1
        lbFolders.Enabled = lbFolders.Items.Count > 0

        '* Updating Buttons
        btnRemove.Enabled = lbFolders.SelectedIndex <> -1
        btnIndexNow.Enabled = lbFolders.Items.Count > 0 And Not bwIndexer.IsBusy

        updateStatusBar()

        Console.WriteLine("Updated GUI Controls")

    End Sub

    Private Sub updateUpDownButtons()

        ' Buttons UP and DOWN
        btnMoveUp.Enabled = (lbFolders.Items.Count > 1 And lbFolders.SelectedIndex > 0)
        btnMoveDown.Enabled = (lbFolders.Items.Count > 1 And lbFolders.SelectedIndex <> lbFolders.Items.Count - 1 And lbFolders.SelectedIndex <> -1)

    End Sub

    Private Sub AddFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFolderToolStripMenuItem.Click
        Call addFolder()
    End Sub

    Private Sub AddFolderAndItsSubfoldersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFolderAndItsSubfoldersToolStripMenuItem.Click

        Me.addSubfolders(False)

    End Sub

    Private Sub AddFolderAndItsSubfoldersRecursiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFolderAndItsSubfoldersRecursiveToolStripMenuItem.Click

        mLevels = cboSubFolderLevels.SelectedIndex + 1 ' 0 + 2 is 2 levels
        Me.addSubfolders(True)

    End Sub

    Private Sub lbFolders_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFolders.DoubleClick
        OpenIndexFile()
    End Sub

    Private Sub lbFolders_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbFolders.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pt As Point
            pt.X = e.X
            pt.Y = e.Y
            lbFolders.SelectedIndex = lbFolders.IndexFromPoint(pt)
        End If
    End Sub

    Private Sub lbFolders_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbFolders.SelectedIndexChanged
        updateGuiControls()
    End Sub

    Private Sub OpenIndexFile()

        If lbFolders.SelectedIndex > -1 Then

            Dim filepath As String
            ' Try opening index.html in folder
            filepath = mSettings.GetAdapter.fGetIndexFilePath(lbFolders.SelectedIndex, cAdapter.IndexingMode.IN_EACH_DIRECTORY)
            If File.Exists(filepath) Then
                Process.Start(filepath)
            Else
                ' Try opening index.html in the Output folder
                filepath = mSettings.GetAdapter.fGetIndexFilePath(lbFolders.SelectedIndex, cAdapter.IndexingMode.IN_ONE_FOLDER_SEPERATE)
                If File.Exists(filepath) Then
                    Process.Start(filepath)
                Else
                    ' Try opening index.gz in folder
                    filepath = mSettings.GetAdapter.fGetIndexFilePath(lbFolders.SelectedIndex, cAdapter.IndexingMode.IN_EACH_DIRECTORY)
                    filepath = Path.ChangeExtension(filepath, ".gz")
                    If File.Exists(filepath) Then
                        Process.Start(filepath)
                    Else
                        ' Try opening index.gz in Output folder
                        filepath = mSettings.GetAdapter.fGetIndexFilePath(lbFolders.SelectedIndex, cAdapter.IndexingMode.IN_ONE_FOLDER_SEPERATE)
                        filepath = Path.ChangeExtension(filepath, ".gz")
                        Try
                            Process.Start(filepath)
                        Catch ex As Exception
                            'MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            sbarLeft.Text = "Index first!"
                        End Try
                    End If
                End If
            End If
        End If

        Return

    End Sub

    Private Sub OpenIndexFileOfThisFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenIndexFileOfThisFolderToolStripMenuItem.Click
        OpenIndexFile()
    End Sub

    Private Sub ShowInWindowsExplorerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowInWindowsExplorerToolStripMenuItem.Click
        Process.Start(lbFolders.SelectedItem.ToString)
    End Sub

    Private Sub RemoveFolder()

        Call AddFoldersToHistory()
        lbFolders.Items.Remove(lbFolders.SelectedItem)
        'mSettings.isConfigFileEdited = True
        Me.updateGuiControls()
        mSettings.SaveConfigForms() 'Keep Memory Up-to-date

    End Sub

    Private Sub RemoveAllFolders()

        Call AddFoldersToHistory()
        lbFolders.Items.Clear()
        'mSettings.isConfigFileEdited = True
        Me.updateGuiControls()
        mSettings.SaveConfigForms() 'Keep Memory Up-to-date

    End Sub
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        RemoveFolder()
    End Sub

    ' Open a file selected from the MRU list.
    Private Sub m_MruList_OpenFile(ByVal file_name As String) Handles m_MruList.OpenFile
        sOpenConfigFile(file_name)
    End Sub

    Private Sub OpenToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miOpen.Click

        If Not fCheckFileNotSaved() Then

            Dim dlg As New OpenFileDialog
            dlg.Filter = DLG_FILTER_TGC
            dlg.InitialDirectory = mSettings.GetAdapter.GetOptions.TasksFolderPath

            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                sOpenConfigFile(dlg.FileName)
            End If

        End If

    End Sub

    Private Sub sOpenConfigFile(ByVal filePath As String)

        If Not fCheckFileNotSaved() Then

            If IO.File.Exists(filePath) Then
                Console.WriteLine("Initialzed File > Open")
                mSettings.SetConfigFilePath(filePath)
                mSettings.LoadConfigFile(mSettings.GetConfigFilePath)
                Call updateAppTitle(mSettings.GetConfigFilePath)
                mSettings.LoadConfigForms()
                Call Me.updateGuiControls()
                ' Update the MRU list.
                m_MruList.Add(filePath)
            Else
                sbarLeft.Text = filePath & " does not exist."
                m_MruList.Remove(filePath)
            End If

        End If

    End Sub

    '***************************************
    '* Check if the File is Not Saved
    '* Return False if File is Saved
    '* Return True otherwise
    '**************************************
    Private Function fCheckFileNotSaved() As Boolean

        If (mSettings.IsConfigFileEdited = True) Then

            Dim tempFile As String = mSettings.GetConfigFilePath
            If tempFile = Nothing Then tempFile = mSettings.GetConfigFileName

            Dim msg As DialogResult = MessageBox.Show("Do you want to save changes to " + tempFile + "?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If (msg = Windows.Forms.DialogResult.Yes) Then
                Return Not fSave()
            ElseIf msg = Windows.Forms.DialogResult.No Then
                Return False
            Else
                Return True  ' Unsafe to Close the Application
            End If

        End If

    End Function

    '***************************************
    '* Method to Save the Building
    '****************************************
    Private Function fSave() As Boolean
        ' If file has not been save already, do Save As
        If File.Exists(mSettings.GetConfigFilePath) = False Or mSettings.GetAdapter.IsConfigFileReadOnly(mSettings.GetConfigFilePath) Then
            Return fSaveAs()
        Else
            mSettings.SaveConfigForms() 'Save didn't really save without this.
            Return mSettings.WriteConfigFile(mSettings.GetConfigFilePath)
        End If
    End Function

    '***************************************************
    '* Method to Save the Building As a Different File
    '****************************************************
    Private Function fSaveAs() As Boolean

        Dim dlg As New SaveFileDialog
        dlg.InitialDirectory = mSettings.GetAdapter.GetOptions.TasksFolderPath
        dlg.Filter = DLG_FILTER_TGC
        dlg.FileName = mSettings.GetConfigFileName()
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            mSettings.SaveConfigForms()
            mSettings.SetConfigFilePath(dlg.FileName)
            m_MruList.Add(dlg.FileName)
            updateAppTitle(mSettings.GetConfigFilePath)
            Return mSettings.WriteConfigFile(mSettings.GetConfigFilePath)  'Return Function outputinstead of True
        Else
            Return False
        End If
    End Function

    Private Sub OpenToolStripMenuItem1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles miOpen.MouseEnter
        Me.sbarLeft.Text = String.Format("Open a {0} config file", Application.ProductName)
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miNew.Click

        If fCheckFileNotSaved() = False Then       'BUG: 0.9.5.4, 0.9.5.5

            lbFolders.Items.Clear()
            'mSettings.isConfigFileEdited = False
            mSettings.LoadConfigNew()

            Call updateAppTitle()
            updateGuiControls()

        End If

    End Sub

    Private Sub NewToolStripMenuItem_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles miNew.MouseEnter
        Me.sbarLeft.Text = String.Format("Create a new {0} config file", Application.ProductName)
    End Sub

    Private Sub VersionHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionHistoryToolStripMenuItem.Click
        My.Forms.frmVer.Focus()
        My.Forms.frmVer.Show()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click

        Call fSave()

    End Sub

    Private Sub SaveToolStripMenuItem_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.MouseEnter
        Me.sbarLeft.Text = String.Format("Save the {0} config file", Application.ProductName)
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click

        Call fSaveAs()
    End Sub

    Private Sub SaveAsToolStripMenuItem_DropDownOpened(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.DropDownOpened

    End Sub

    Private Sub SaveAsToolStripMenuItem_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.MouseEnter
        Me.sbarLeft.Text = String.Format("Save the {0} config file as...", Application.ProductName)
    End Sub

    Private Sub ExitToolStripMenuItem_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.MouseEnter
        Me.sbarLeft.Text = String.Format("Save config file and Close {0}", Application.ProductName)
    End Sub

    Private Sub frmForm1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

        Select Case Me.WindowState
            Case FormWindowState.Minimized
                Me.ShowInTaskbar = Not mSettings.GetAdapter.GetOptions.MinimizeToTray
            Case FormWindowState.Normal
                'Me.Left = My.Settings.FormLeft
                'Me.Top = My.Settings.FormTop
        End Select

    End Sub

    Private Sub AlwaysOnTopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlwaysOnTopToolStripMenuItem.Click
        ToggleFormAlwaysOnTop()
    End Sub

    Public Sub ToggleFormAlwaysOnTop()
        AlwaysOnTopToolStripMenuItem.Checked = Not AlwaysOnTopToolStripMenuItem.Checked
        Me.TopMost = AlwaysOnTopToolStripMenuItem.Checked
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

        Call AddFoldersToHistory()
        Me.updateGuiControls()

    End Sub

    Private Sub btnIndexNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIndexNow.Click

        Dim init As New cInitializer
        init.InitilizationMode = cAdapter.InitializationMode.MANUAL
        init.Settings = mSettings.GetAdapter
        InitializeIndexing(init)

    End Sub

    Private Sub InitilizeForConfig(ByVal myInitMode As cAdapter.InitializationMode, ByVal myReader As cAdapter)

        Dim isIndexFileInSameDir As Boolean
        Dim isIndexFileInOneDirMerged As Boolean
        Dim isIndexFileInOneDirSeperate As Boolean

        ' Called by GUI to Index a SINGLE TGC file
        isIndexFileInSameDir = myReader.GetConfig.isIndexFileInSameDir
        isIndexFileInOneDirMerged = myReader.GetConfig.isIndexFileInOneDir
        isIndexFileInOneDirSeperate = myReader.GetConfig.CreateIndividualFilesInOutputDir

        If isIndexFileInSameDir And isIndexFileInOneDirMerged And isIndexFileInOneDirSeperate Then
            pbBar.Maximum = myReader.GetConfig.FolderList.Count * 3
        ElseIf isIndexFileInSameDir And isIndexFileInOneDirMerged Then
            pbBar.Maximum = myReader.GetConfig.FolderList.Count * 2
        Else
            pbBar.Maximum = myReader.GetConfig.FolderList.Count
        End If

    End Sub

    Private Sub InitilizeForAllConfigs(ByVal myInitMode As cAdapter.InitializationMode, ByVal mSettings As cAdapter)

        Dim isIndexFileInSameDir As Boolean
        Dim isIndexFileInOneDirMerged As Boolean
        Dim isIndexFileInOneDirSeperate As Boolean

        Dim max As Integer
        Dim strTasksFolderPath As String = mSettings.GetOptions.TasksFolderPath
        For Each strFile As String In Directory.GetFiles(strTasksFolderPath)
            Dim strFileExt As String = Path.GetExtension(strFile)
            If strFileExt.ToLower = ".tgc" Then
                Dim r As New cAdapter
                r.LoadConfigFile(strFile)
                isIndexFileInSameDir = r.GetConfig.isIndexFileInSameDir
                isIndexFileInOneDirMerged = r.GetConfig.isIndexFileInOneDir
                isIndexFileInOneDirSeperate = r.GetConfig.CreateIndividualFilesInOutputDir

                If isIndexFileInSameDir And isIndexFileInOneDirMerged And isIndexFileInOneDirSeperate Then
                    max += r.GetConfig.FolderList.Count * 3
                    'MsgBox(max & "thrice")
                ElseIf isIndexFileInSameDir And isIndexFileInOneDirMerged Then
                    max += r.GetConfig.FolderList.Count * 2
                    'MsgBox(max & " twice")
                Else
                    max += r.GetConfig.FolderList.Count
                    'MsgBox(max)
                End If
            End If
        Next

        pbBar.Maximum = max
        'MsgBox(max)

    End Sub

    Public Sub InitializeIndexing(ByVal myInitializer As cInitializer)

        pbBar.Value = 0 ' Needed

        If Not myInitializer.InitilizationMode = cAdapter.InitializationMode.MANUAL Then
            ' Scheduled + Index All Configs
            InitilizeForAllConfigs(myInitializer.InitilizationMode, mSettings.GetAdapter)
        Else
            ' Manual Operation
            If myInitializer.Settings IsNot Nothing Then
                ' Manual + Index Single Config
                InitilizeForConfig(cAdapter.InitializationMode.MANUAL, myInitializer.Settings)
            Else
                ' Manual + Index All Configs
                InitilizeForAllConfigs(myInitializer.InitilizationMode, mSettings.GetAdapter)
            End If
        End If

        My.Forms.frmOptions.btnIndexAll.Enabled = False
        cmiStartIndexing.Enabled = False
        btnIndexNow.Enabled = False
        miNew.Enabled = False
        miOpen.Enabled = False

        bwIndexer.RunWorkerAsync(myInitializer)
        tmrUpdateProgressBar.Enabled = True
        tmrUpdateProgressBar.Start()

    End Sub

    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        m_UndoRedoStack.Undo()
        Call GetFoldersFromHistory()
        updateGuiControls()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        m_UndoRedoStack.Redo()
        Call GetFoldersFromHistory()
        updateGuiControls()
    End Sub

    Private Sub btnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        ' only if the last item isn't the current one
        ' add a duplicate item down in the listbox
        lbFolders.Items.Insert(lbFolders.SelectedIndex + 2, lbFolders.SelectedItem)
        ' make it the current item
        lbFolders.SelectedIndex += 2
        ' delete the old occurrence of this item
        lbFolders.Items.RemoveAt(lbFolders.SelectedIndex - 2)
        Call AddFoldersToHistory()
        Me.updateGuiControls()
    End Sub

    Private Sub RemoveThisFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveThisFolderToolStripMenuItem.Click
        RemoveFolder()
    End Sub

    Private Sub RemoveAllFolderEntriesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveAllFolderEntriesToolStripMenuItem.Click
        RemoveAllFolders()
    End Sub

    Private Sub bwIndex_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwIndexer.DoWork

        Dim init As New cInitializer
        init = CType(e.Argument, cInitializer)

        mIndexer = New cIndexer()

        If init.Settings Is Nothing Then
            ' Called by GUI to Index ALL TGC files
            init.Settings = mSettings.GetAdapter
            init.Settings.mInitMode = init.InitilizationMode
            mIndexer.IndexAllConfigs(init.Settings)
        Else
            ' Called by GUI to Index a SINGLE TGC file
            mIndexer.IndexConfig(init.Settings)
        End If

    End Sub

    Private Sub bwIndex_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwIndexer.RunWorkerCompleted

        sbarLeft.Text = "Last Indexed on " + Now.ToString("yyyy-MM-dd 'at' HH:mm:ss")
        sbarRight.Text = "Indexing Complete" 'war59312 - its done, not ready haha

        btnIndexNow.Enabled = True
        My.Forms.frmOptions.btnIndexAll.Enabled = True
        cmiStartIndexing.Enabled = True
        miNew.Enabled = True
        miOpen.Enabled = True

        tmrUpdateProgressBar.Stop()
        tmrUpdateProgressBar.Enabled = False
        pbBar.Value = pbBar.Maximum ' To get rid of Timer Inaccuracies

        If lbFolders.Items.Count = 1 Then
            OpenIndexFile()
        End If

    End Sub

    Private WithEvents mBgUpdateCheck As New System.ComponentModel.BackgroundWorker

    Private mcUpdateDownloadDir As String() = New String() {URL_UPDATE_DOWNLOAD}
    Private mcUpdateCheckUrl As String() = New String() {"http://wmwiki.com/mcored/updates.txt"} ' HAS TO RELIABLE
    Const mcDownloadFileSuffix As String = "-setup.zip"
    Private mAppAbbr As String = "TreeGUI"

    Private Sub bwTorrentSpy_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mBgUpdateCheck.DoWork

        Dim appInfo As New McoreSystem.AppInfo(Application.ProductName,
                    Application.ProductVersion)

        appInfo.CheckUpdates(mcUpdateCheckUrl.ToString, mcUpdateDownloadDir.ToString, mAppAbbr, AppInfo.OutdatedMsgStyle.NewVersionOfAppAvailable)

        'Dim app As New McoreSystem.AppInfo(Application.ProductName, Application.ProductVersion)
        'Dim strUrl As String = URL_UPDATE_CHECK
        'If (app.isUpdated(strUrl, "updates.txt") = True) Then
        '    If (MessageBox.Show(app.getMsgOutdated(Application.ProductName, McoreSystem.AppInfo.OutdatedMsgStyle.AppVerNumberAvailable), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = Windows.Forms.DialogResult.Yes Then
        '        Process.Start(URL_UPDATE_DOWNLOAD)
        '    End If
        'Else
        '    MessageBox.Show(app.getMsgUpToDate, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If

    End Sub

    Private Sub CheckForUpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdateToolStripMenuItem.Click

        If Not mBgUpdateCheck.IsBusy Then
            mBgUpdateCheck.RunWorkerAsync()
        End If
    End Sub

    Private Sub SupportForumsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupportForumsToolStripMenuItem.Click
        Process.Start(URL_SUPPORT_FORUM)
    End Sub

    Private Sub ReportBugsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportBugsToolStripMenuItem.Click
        Email(Application.ProductName)
    End Sub

    Private Sub ReleaseNotesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReleaseNotesToolStripMenuItem.Click
        My.Forms.frmRel.Focus()
        My.Forms.frmRel.Show()
    End Sub

    Private Sub cmShowApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmShowApp.Click
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
    End Sub

    Private Sub StartIndexingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiStartIndexing.Click
        mSettings.IndexAll()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem1.Click

        mExitApplication = True
        Me.Close()

    End Sub

    Private Sub niTray_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles niTray.MouseDoubleClick
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Minimized
        ElseIf Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Normal
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub tmrScheduleTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrScheduleTime.Tick

        If mSettings.GetAdapter.GetOptions.RunTasksInGUI Then 'ALPHA 18
            If mSettings.GetAdapter.GetOptions.IsIndexAccordingToTime Then
                If mSettings.GetAdapter.GetOptions.IsScheduledForToday Then
                    If mSettings.GetAdapter.GetOptions.ScheduleTime = Now.ToString("HH:mm:ss") Then
                        Dim init As New cInitializer
                        init.InitilizationMode = cAdapter.InitializationMode.DATETIME_BASED_GUI
                        init.Settings = Nothing
                        InitializeIndexing(init)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub tmrScheduleInterval_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrScheduleInterval.Tick

        tmrScheduleInterval.Interval = CInt(mSettings.GetAdapter.getIntervalInMilliseconds)
        Dim init As New cInitializer
        init.InitilizationMode = cAdapter.InitializationMode.INTERVAL_BASED_GUI
        init.Settings = Nothing
        InitializeIndexing(init)
        If Not mSettings.GetAdapter.GetOptions.IsIndexingIntervalEnabled Then
            tmrScheduleInterval.Enabled = False
            tmrScheduleInterval.Stop()
        End If

    End Sub

    Private Sub sCopyFolderList(ByVal fromList As List(Of String), ByVal toList As List(Of String))

        toList.Clear()
        For Each s As String In fromList
            toList.Add(s)
        Next

    End Sub

    Private Sub IndexThisFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IndexThisFolderToolStripMenuItem.Click

        Dim stack As New List(Of String)
        sCopyFolderList(mSettings.GetAdapter.GetConfig.FolderList, stack)

        mSettings.GetAdapter.GetConfig.FolderList.Clear()
        'mSettings.GetReader.GetConfig.FolderList.AddRange(lbFolders.CheckedItems)
        'BUG: 2.2.5.6 Index Checked Items crashed TreeGUI after the change in Folder List format
        For Each o As Object In lbFolders.CheckedItems
            mSettings.GetAdapter.GetConfig.FolderList.Add(o.ToString)
        Next
        Dim init As New cInitializer
        init.InitilizationMode = cAdapter.InitializationMode.MANUAL
        init.Settings = mSettings.GetAdapter
        InitializeIndexing(init)
        'MessageBox.Show(mSettings.GetReader.GetConfig.FolderList.Count)
        'MsgBox(init.Settings.GetConfig.FolderList.Count)
        sCopyFolderList(stack, mSettings.GetAdapter.GetConfig.FolderList)

        'MsgBox(mSettings.GetReader.GetConfig.FolderList.Count)

    End Sub

    Public Function fExportIndex(ByVal dirPath As String) As Boolean

        Dim success As Boolean = False

        Try
            Dim tgApp As New TreeGUI.cAdapter

            If File.Exists(mSettings.GetAdapter.GetOptions.DefaultConfigFilePath) Then
                tgApp.LoadConfigFile(mSettings.GetAdapter.GetOptions.DefaultConfigFilePath)
            End If

            'tgApp.GetConfig.IndexFileName = fGetFileNameFromPattern(My.Settings.IndexFileNamePattern, lDisc.FirstTrack)
            'tgApp.GetConfig.IndexFileExtension = My.Settings.IndexFileExt
            tgApp.GetConfig.FolderList.Add(dirPath)
            'tgApp.GetConfig.CssFilePath = My.Settings.IndexCSS
            tgApp.GetConfig.CollapseFolders = False

            Dim tnl As New TreeGUI.cTreeNetLib(tgApp)
            tnl.IndexNow(TreeGUI.cAdapter.IndexingMode.IN_EACH_DIRECTORY)

            'sWriteDebugLog("Exported Index to " & tgApp.GetConfig.GetIndexFilePaths(0))
            success = True
        Catch ex As Exception

        End Try

        Return success

    End Function

    Private Sub frmForm1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Dim args() As String = Environment.GetCommandLineArgs()

        'Dim sb As New System.Text.StringBuilder
        'For Each arg As String In args
        '    sb.AppendLine(arg)
        'Next
        'MsgBox(sb.ToString)

        If args.Length > 1 Then

            Dim firstArg As String = args(1).ToString

            If Path.GetExtension(firstArg) = ".tgc" Then
                mSettings.SetConfigFilePath(firstArg)
                Console.WriteLine("A TGC file has been double clicked...")
                mSettings.LoadConfigFile(mSettings.GetConfigFilePath)
                Call AddFoldersToHistory()

            End If

        Else
            If mSettings.GetAdapter.GetOptions.OpenDefaultConfig And File.Exists(mSettings.GetAdapter.GetOptions.DefaultConfigFilePath) Then
                mSettings.SetConfigFilePath(mSettings.GetAdapter.GetOptions.DefaultConfigFilePath)
                mSettings.LoadConfigFile(mSettings.GetConfigFilePath)
            End If
        End If

        mSettings.LoadConfigForms() ' Needed for Default and file double click
        Call updateAppTitle(mSettings.GetConfigFilePath)
        updateGuiControls()

    End Sub

    Private Sub miToolsProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miToolsProperties.Click

        Me.sbarLeft.Text = "Loaded " & mSettings.GetConfigFilePath
        My.Forms.frmConfig.Focus()
        My.Forms.frmConfig.ShowDialog()
    End Sub

    Private Sub tmrUpdateProgressBar_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUpdateProgressBar.Tick
        Try
            If Not mIndexer.GetEngine Is Nothing Then
                pbBar.Value = mIndexer.GetEngine.Progress
                sbarRight.Text = mIndexer.GetEngine.CurrentDirMessage
            End If
        Catch ex As Exception
            ' Generated a lot of error messages when error raised
            ' BUG: 2.2.5.7 Disabled BugReport function for progress bar errors
            ' MsgBox(pbBar.Maximum & "," & pbBar.Value & "," & mIndexer.GetEngine.Progress)
            'BugReport(ex.Message)
        End Try
    End Sub

    Private Sub SetVirtualFolderNameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetVirtualFolderNameToolStripMenuItem.Click

        Dim rf As String = lbFolders.SelectedItem.ToString()
        Dim dvfn As String = "-= " + IO.Path.GetFileName(rf) + " =-"
        Dim dirPath As String = lbFolders.SelectedItem.ToString
        Dim vf As String = InputBox("Set Virtual Folder Name", Application.ProductName, dvfn)

        Dim rfvf As String = rf + "|" + vf

        For Each l As String In mSettings.GetAdapter.GetConfig.VirtualFolderList
            If l.IndexOf(rf) <> -1 Then
                ' Already exists
                mSettings.GetAdapter.GetConfig.VirtualFolderList.Remove(l)
                Exit For
            End If
        Next

        If Not mSettings.GetAdapter.GetConfig.VirtualFolderList.Contains(rfvf) Then
            mSettings.GetAdapter.GetConfig.VirtualFolderList.Add(rfvf)
        End If
    End Sub

    Private Sub AbortIndexingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        bwIndexer.CancelAsync()
    End Sub

    Private Sub DeleteAllIndexFilesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAllIndexFilesToolStripMenuItem.Click
        mSettings.DeleteAllIndexFiles()
    End Sub

    Private Sub ServiceStartsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServiceStartsToolStripMenuItem.Click

        Dim f As String = mSettings.GetAdapter.LOG_PATH_ONSTART
        If File.Exists(f) Then Process.Start(f)

    End Sub

    Private Sub IndexerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IndexerToolStripMenuItem.Click

        Dim f As String = mSettings.GetAdapter.LOG_PATH_READER
        If File.Exists(f) Then Process.Start(f)

    End Sub

    Private Sub IndexerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IndexerToolStripMenuItem1.Click

        Dim f As String = mSettings.GetAdapter.LogPathIndexer
        If File.Exists(f) Then Process.Start(f)

    End Sub

    Private Sub BrowseTasksFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseTasksFolderToolStripMenuItem.Click
        If Directory.Exists(mSettings.GetAdapter.GetOptions.TasksFolderPath) Then
            Process.Start(mSettings.GetAdapter.GetOptions.TasksFolderPath)
        End If
    End Sub

    Private Sub DebugToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DebugToolStripMenuItem.Click

        Dim f As String = mSettings.GetAdapter.LogPathDebug
        If File.Exists(f) Then Process.Start(f)

    End Sub
End Class