Imports System.IO

Module mSettings

    '*************************************
    '* Directly access Form Controls 
    '* and save them as Config Properties 
    '* or load Config Properties to them
    '*************************************

    Dim mReader As New TreeGUI.cAdapter
    Dim m_AdminService As New TreeGUI.cAdminService

    Dim mSvc As New TreeGUI.cAdminService

    Private m_OptionsFilePath As String = mReader.GetOptionsFilePath
    Private mConfigFileEdited As Boolean = False
    Public Const NEW_CONFIG_NAME As String = "Config1"
    Private m_ConfigFilePath As String = NEW_CONFIG_NAME

    Private Const STR_EMAIL As String = "mailto:mcored@ii.net?subject="
    Public Const DLG_FILTER_TGC As String = "TreeGUI Config Files (*.tgc)|*.tgc|All Files (*.*)|*.*"

    Public Sub BugReport(ByVal bug As String)

        If MessageBox.Show("Something unexpected happened and " + _
        Application.ProductName + " crashed. Would you like to report the bug?", _
        Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) _
        = DialogResult.Yes Then
            Email(bug)
        End If
     
    End Sub

    Public Sub Email(ByVal msg As String)
        Dim strSubject As String = Application.ProductName + " " + Application.ProductVersion
        System.Diagnostics.Process.Start(STR_EMAIL + strSubject + "&body=" + msg)
    End Sub

    Public Sub FixWindowsService()
        If Not mSvc.isProperlyInstalledService(Application.StartupPath, mSettings.GetAdapter.GetOptions.ServiceName) Then
            If mSvc.setProperServicePath(Application.StartupPath, mSettings.GetAdapter.GetOptions.ServiceName) Then
                MessageBox.Show("Succesfully fixed Windows Service path.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Public Function GetTruncatedString(ByVal text As String, ByVal length As Integer) As String
        'TODO: Text in the middle of long strings should be replaced with ...
        If text.Length < length Then
            Return text
        End If
        Return text
    End Function

    Public Sub IndexAll()
        Try
            If Not My.Forms.frmForm1.bwIndexer.IsBusy Then
                Dim init As New cInitializer
                init.InitilizationMode = cAdapter.InitializationMode.MANUAL
                init.Settings = Nothing
                My.Forms.frmForm1.InitializeIndexing(init)
            Else
                MessageBox.Show("Indexer is busy. Please try again later", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As System.UnauthorizedAccessException
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Public Sub DeleteAllIndexFiles()

        If MessageBox.Show("Are you sure you want to delete all the index files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

            Dim strTasksFolderPath As String = mReader.GetOptions.TasksFolderPath
            Dim lExtensions As New List(Of String)
            lExtensions.AddRange(mExtensions)
            lExtensions.Add(".gz")
            lExtensions.Add(".html")
            lExtensions.Add(".zip")

            For Each strFile As String In Directory.GetFiles(strTasksFolderPath)
                Dim strFileExt As String = Path.GetExtension(strFile)
                If strFileExt.ToLower = ".tgc" Then
                    Dim r As New cAdapter
                    r.LoadConfigFile(strFile)
                    For i As Integer = 0 To r.GetConfig.FolderList.Count - 1
                        ' Attempt deleting index files with all extensions
                        For Each ext As String In lExtensions
                            ' In Each Dir
                            Dim filePath As String = r.fGetIndexFilePath(i, cAdapter.IndexingMode.IN_EACH_DIRECTORY)
                            Dim fileExt As String() = filePath.Split(CChar("."))
                            filePath = filePath.Replace("." & fileExt(fileExt.Length - 1), ext)
                            If File.Exists(filePath) Then
                                Try
                                    My.Computer.FileSystem.DeleteFile(filePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                                    Console.WriteLine("Deleting " & filePath)
                                Catch ex As Exception

                                End Try
                            End If
                            ' Merged file
                            filePath = r.fGetIndexFilePath(i, cAdapter.IndexingMode.IN_ONE_FOLDER_MERGED)
                            filePath = filePath.Replace("." & fileExt(fileExt.Length - 1), ext)
                            If File.Exists(filePath) Then
                                Try
                                    My.Computer.FileSystem.DeleteFile(filePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                                    Console.WriteLine("Deleting " & filePath)
                                Catch ex As Exception

                                End Try
                            End If
                            ' Individual Index files in Output directory
                            filePath = r.fGetIndexFilePath(i, cAdapter.IndexingMode.IN_ONE_FOLDER_SEPERATE)
                            filePath = filePath.Replace("." & fileExt(fileExt.Length - 1), ext)
                            If File.Exists(filePath) Then
                                Try
                                    My.Computer.FileSystem.DeleteFile(filePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                                    Console.WriteLine("Deleting " & filePath)
                                Catch ex As Exception

                                End Try
                            End If
                        Next
                    Next
                End If
            Next

        End If

    End Sub

    Public Function GetText(ByVal strName As String) As String

        Try
            ' get the current assembly
            Dim oAsm As System.Reflection.Assembly = _
            System.Reflection.Assembly.GetExecutingAssembly()
            Dim oStrm As IO.Stream = _
            oAsm.GetManifestResourceStream(oAsm.GetName.Name + "." + strName)
            ' read contents of embedded file
            Dim oRdr As IO.StreamReader = New IO.StreamReader(oStrm)
            Return oRdr.ReadToEnd

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetConfigFileName() As String
        If m_ConfigFilePath = NEW_CONFIG_NAME Then
            Return NEW_CONFIG_NAME
        Else
            Return IO.Path.GetFileNameWithoutExtension(m_ConfigFilePath)
        End If
    End Function

    Public Sub LoadConfigNew()

        SetConfigFilePath(NEW_CONFIG_NAME)
        GetAdapter.LoadNewConfig()
        IsConfigFileEdited = False
        'GetAdapter.GetConfig.FolderList.Clear()
        'GetAdapter.GetConfig.VirtualFolderList.Clear()
        ' 2.7.1.2 File > New still carried over settings from previously opened tgc file
        LoadConfigForms()

    End Sub

    Public Property IsConfigFileEdited() As Boolean

        Get            
            Return mConfigFileEdited
        End Get
        Private Set(ByVal value As Boolean)
            mConfigFileEdited = value
        End Set

    End Property

    Public Function GetOptionsFilePath() As String
        Return m_OptionsFilePath
    End Function

    Public Sub SetConfigFilePath(ByVal filePath As String)
        m_ConfigFilePath = filePath
    End Sub

    Public Function GetAdapter() As TreeGUI.cAdapter
        Return mReader
    End Function

    Public Function GetConfigFilePath() As String
        Return m_ConfigFilePath
    End Function

#Region "Loading from File to m_Config"

    Public Sub LoadOptionsFile()

        ' This is the first method called by TreeGUI when it is loaded 
        ' Some Config entity objects should not be null at this point 
        ' They are initialized here...

        mReader.LoadOptionsFile(m_OptionsFilePath)

        Console.WriteLine("Loaded Options File")

    End Sub

    Public Function LoadConfigFile(ByVal myConfigFilePath As String) As Boolean

        If IO.File.Exists(myConfigFilePath) Then
            m_ConfigFilePath = myConfigFilePath
            mReader.LoadConfigFile(myConfigFilePath)
            If mReader.Upgraded Then
                IsConfigFileEdited = True
                Console.WriteLine("Upgraded")
            End If

            Console.WriteLine(mReader.GetConfig.CssFilePath)
            Console.WriteLine("Loaded Config File")
        End If

    End Function

#End Region

 


    Public Sub SaveOptionsForm()

        'Global Configuration > Tasks
        mReader.GetOptions.TasksFolderPath = My.Forms.frmOptions.txtJobsFolderPath.Text
        mReader.GetOptions.DefaultConfigFilePath = My.Forms.frmOptions.txtDefaultConfigFilePath.Text
        mReader.GetOptions.OpenDefaultConfig = My.Forms.frmOptions.chkOpenDefaultConfig.Checked

        'Global Configuration > Appearance
        mReader.GetOptions.TrayIconIsEnabled = My.Forms.frmOptions.chkSystemTray.Checked
        mReader.GetOptions.TrayOnLoad = My.Forms.frmOptions.chkLoadToTray.Checked
        mReader.GetOptions.MinimizeToTray = My.Forms.frmOptions.chkMinimizeToTray.Checked
        mReader.GetOptions.CloseToTray = My.Forms.frmOptions.chkCloseToTray.Checked
        mReader.GetOptions.MaximizedWindow = My.Forms.frmOptions.chkStartMaximized.Checked
        mReader.GetOptions.RememberWindowSize = My.Forms.frmOptions.chkRemWinSize.Checked 'war59312
        mReader.GetOptions.RememberWindowLocation = My.Forms.frmOptions.chkRemWinLocation.Checked 'war59312


       



        Dim regKey As Microsoft.Win32.RegistryKey = _
          Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        If My.Forms.frmOptions.chkStartup.Checked Then
            regKey.SetValue(Application.ProductName, Application.ExecutablePath)
        Else
            regKey.DeleteValue(Application.ProductName, False)
        End If
        mReader.GetOptions.StartupItem = My.Forms.frmOptions.chkStartup.Checked

        'Global Configuration > Indexer

    
        Select Case mReader.GetConfig.IndexingEngineID
            Case Is = 0 'Tree Walk Utility
                ' Cannot have .html
                If mReader.GetConfig.IndexFileExtension = ".html" Then
                    mReader.GetConfig.IndexFileExtension = ".txt"
                End If
        End Select




        'Global Configuration > Schedule

        mReader.GetOptions.IndexingInterval = CInt(My.Forms.frmOptions.nudInterval.Value)
        mReader.GetOptions.IsIndexingIntervalEnabled = My.Forms.frmOptions.chkIndexInterval.Checked
        mReader.GetOptions.IsIndexAccordingToTime = My.Forms.frmOptions.chkIndexTime.Checked
        mReader.GetOptions.RunTasksInGUI = My.Forms.frmOptions.chkScheduelTasksForGui.Checked

        mReader.GetOptions.OnMonday = My.Forms.frmOptions.chkMonday.Checked
        mReader.GetOptions.OnTuesday = My.Forms.frmOptions.chkTuesday.Checked
        mReader.GetOptions.OnWednesday = My.Forms.frmOptions.chkWednesday.Checked
        mReader.GetOptions.OnThursday = My.Forms.frmOptions.chkThursday.Checked
        mReader.GetOptions.OnFriday = My.Forms.frmOptions.chkFriday.Checked
        mReader.GetOptions.OnSaturday = My.Forms.frmOptions.chkSaturday.Checked
        mReader.GetOptions.OnSunday = My.Forms.frmOptions.chkSunday.Checked

        If My.Forms.frmOptions.dtpTime.Text <> "" Then
            mReader.GetOptions.ScheduleTime = My.Forms.frmOptions.dtpTime.Text
        End If

        Console.WriteLine("Saved Options Form")

    End Sub


    Public Sub SaveConfigForms()

        ' Folder List
        mReader.GetConfig.FolderList.Clear() 'Clear the Collection otherwise List keeps growing
        IsConfigFileEdited = True
        For Each item As String In My.Forms.frmForm1.lbFolders.Items
            mReader.GetConfig.FolderList.Add(item)
        Next

        'Output
        mReader.GetConfig.IndexFileName = My.Forms.frmConfig.txtIndexFileName.Text
        mReader.GetConfig.IndexFileExtension = My.Forms.frmConfig.cboIndexFileExt.Text
        mReader.GetConfig.isIndexFileInOneDir = My.Forms.frmConfig.chkOneDir.Checked
        mReader.GetConfig.isIndexFileInSameDir = My.Forms.frmConfig.chkSameDir.Checked
        mReader.GetConfig.isMergeFiles = My.Forms.frmConfig.chkMergeFiles.Checked
        mReader.GetConfig.OutputDir = My.Forms.frmConfig.txtOutputDir.Text
        mReader.GetConfig.CreateIndividualFilesInOutputDir = My.Forms.frmConfig.chkIndividualIndexFiles.Checked

        mReader.GetConfig.ProcessPriority = CType(My.Forms.frmConfig.cboProcessPriority.SelectedIndex, Threading.ThreadPriority)
        mReader.GetConfig.IndexingEngineID = My.Forms.frmConfig.cboIndexingEngine.SelectedIndex

        'Engine > Tree Walk Utility
        mReader.GetConfig.isAddFiles = My.Forms.frmConfig.chkIndexFiles.Checked
        mReader.GetConfig.isAscii = My.Forms.frmConfig.chkAscii.Checked
        mReader.GetConfig.isRemoveTreeBranches = My.Forms.frmConfig.chkRemoveBranches.Checked
        mReader.GetConfig.FolderHeadingStyle = My.Forms.frmConfig.txtFolderHeadingStyle.Text

        'Engine > Tree.NET
        mReader.GetConfig.ShowFilesTreeNet = My.Forms.frmConfig.chkShowFilesTreeNet.Checked
        mReader.GetConfig.ShowFileSize = My.Forms.frmConfig.chkFileSize.Checked
        mReader.GetConfig.ShowDirSize = My.Forms.frmConfig.chkShowDirSize.Checked
        mReader.GetConfig.ShowFilePath = My.Forms.frmConfig.chkShowFileFullPath.Checked
        mReader.GetConfig.ShowFolderPath = My.Forms.frmConfig.chkShowFullFolderPath.Checked
        mReader.GetConfig.CssFilePath = My.Forms.frmConfig.txtCssFilePath.Text
        mReader.GetConfig.HideExtension = My.Forms.frmConfig.chkHideExtension.Checked
        mReader.GetConfig.ShowFolderPathOnStatusBar = My.Forms.frmConfig.chkFolderPathOnStatusBar.Checked
        mReader.GetConfig.ShowVirtualFolders = My.Forms.frmConfig.chkShowVirtualFolders.Checked
        mReader.GetConfig.RevereFileOrder = My.Forms.frmConfig.chkReverseFileOrder.Checked
        mReader.GetConfig.ShowFileCount = My.Forms.frmConfig.chkShowFileCount.Checked
        mReader.GetConfig.CollapseFolders = My.Forms.frmConfig.chkCollapseFolders.Checked
        mReader.GetConfig.LogoPath = My.Forms.frmConfig.txtLogoPath.Text

        mReader.GetConfig.AudioInfo = My.Forms.frmConfig.chkAudioInfo.Checked
        'mReader.GetConfig.AudioQuickScan = My.Forms.frmConfig.chkAudioQuickScan.Checked
        mReader.GetConfig.FolderExpandLevel = CInt(My.Forms.frmConfig.nudFolderExpandLevel.Value)

        mReader.GetConfig.mSortBySize = My.Forms.frmConfig.chkFolderSortSize.Checked
        mReader.GetConfig.mSortBySizeMode = My.Forms.frmConfig.cboFolderSortMode.SelectedIndex

        mReader.GetConfig.ServerInfo = My.Forms.frmConfig.txtServerInfo.Text

        If My.Forms.frmConfig.chkNumberFiles.CheckState = CheckState.Checked Then
            mReader.GetConfig.ListType = Config.eListType.Numbered
        Else
            mReader.GetConfig.ListType = Config.eListType.Bullets
        End If

        ' Compression 
        mReader.GetConfig.ZipAfterIndexed = My.Forms.frmConfig.chkZipFiles.Checked
        mReader.GetConfig.ZipAndDeleteFile = My.Forms.frmConfig.chkZipAndDelete.Checked
        mReader.GetConfig.ZipMergedFile = My.Forms.frmConfig.chkZipMergedFile.Checked
        mReader.GetConfig.ZipFilesInEachDir = My.Forms.frmConfig.chkZipFileInEachDir.Checked
        mReader.GetConfig.ZipFilesInOutputDir = My.Forms.frmConfig.chkZipFilesInOutputDir.Checked

        ' Filtering

        mReader.GetConfig.EnabledFiltering = My.Forms.frmConfig.chkEnableFilter.Checked
        mReader.GetConfig.HideProtectedOperatingSystemFilesFolders = My.Forms.frmConfig.chkHideProtectedOperingSystemFiles.Checked

        mReader.GetConfig.IgnoreHiddenFiles = My.Forms.frmConfig.chkIgnoreHiddenFiles.Checked
        mReader.GetConfig.IgnoreHiddenFolders = My.Forms.frmConfig.chkIgnoreHiddenFolders.Checked
        mReader.GetConfig.IgnoreSystemFiles = My.Forms.frmConfig.chkIgnoreSystemFiles.Checked
        mReader.GetConfig.IgnoreSystemFolders = My.Forms.frmConfig.chkIgnoreSystemFolders.Checked
        'war59312        
        mReader.GetConfig.IgnoreEmptyFolders = My.Forms.frmConfig.chkIgnoreEmptyFolders.Checked
        mReader.GetConfig.IgnoreFollowingFiles = My.Forms.frmConfig.chkIgnoreFollowingFiles.Checked
        mReader.GetConfig.IgnoreFilesList = My.Forms.frmConfig.txtIgnoreFiles.Text


        IsConfigFileEdited = True
        Console.WriteLine("Saved Config Form")

    End Sub

    Public Function WriteConfigFile(ByVal filePath As String) As Boolean

        If File.Exists(filePath) Then
            Dim fileBkPath As String = Path.GetDirectoryName(filePath) + "\" + Path.GetFileName(filePath) + ".bak"
            My.Computer.FileSystem.CopyFile(filePath, fileBkPath, True)
        End If

        'Return WriteConfigFileBF(filePath)
        Return WriteConfigFileXML(filePath)

    End Function

    Private Function WriteConfigFileBF(ByVal filePath As String) As Boolean

        If isConfigFileEdited Then
            If mReader.IsConfigFileReadOnly(filePath) Then
                MessageBox.Show(String.Format("Make sure {0} is not Read-Only.", filePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            Else
                'mSettings.SaveConfigForm() ' No Need
                Dim fs As New IO.FileStream(filePath, IO.FileMode.Create)
                Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
                bf.Serialize(fs, mReader.GetConfig)
                fs.Close()
                IsConfigFileEdited = False
                Return True
            End If

        Else
            'MessageBox.Show("NOTHING WAS EDITED!")
            Return False
        End If
    End Function

    Private Function WriteConfigFileXML(ByVal filePath As String) As Boolean

        If isConfigFileEdited Then
            If mReader.IsConfigFileReadOnly(filePath) Then
                MessageBox.Show(String.Format("Make sure {0} is not Read-Only.", filePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            Else
                Dim fs As New IO.FileStream(filePath, FileMode.Create)
                Dim xs As New System.Xml.Serialization.XmlSerializer(mReader.GetConfig.GetType)
                xs.Serialize(fs, mReader.GetConfig)
                IsConfigFileEdited = False
                fs.Close()
                Return True
            End If
        End If

    End Function

    Private Function isLoadedAsStartup(ByVal ProductName As String, ByVal FilePath As String) As Boolean
        Dim regKey As Microsoft.Win32.RegistryKey = _
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        If regKey.GetValue(ProductName) IsNot Nothing Then
            Return regKey.GetValue(ProductName).ToString = FilePath
        Else
            Return False
        End If

    End Function

    Public Sub LoadOptionsForm()

        My.Forms.frmOptions.btnFixWindowsServicePath.Enabled = Not mSvc.isProperlyInstalledService(Application.StartupPath, mSettings.GetAdapter.GetOptions.ServiceName)

        'Global Configuration > Tasks         
        If mReader.GetOptions.TasksFolderPath <> "" Then
            My.Forms.frmOptions.txtJobsFolderPath.Text = mReader.GetOptions.TasksFolderPath
        Else
            mReader.GetOptions.TasksFolderPath = Application.StartupPath
            My.Forms.frmOptions.txtJobsFolderPath.Text = mReader.GetOptions.TasksFolderPath
        End If

        My.Forms.frmOptions.txtDefaultConfigFilePath.Text = mReader.GetOptions.DefaultConfigFilePath
        My.Forms.frmOptions.chkOpenDefaultConfig.Checked = mReader.GetOptions.OpenDefaultConfig

        'Global Configuration > Appearance
        My.Forms.frmOptions.chkSystemTray.Checked = mReader.GetOptions.TrayIconIsEnabled
        My.Forms.frmOptions.chkLoadToTray.Checked = mReader.GetOptions.TrayOnLoad
        My.Forms.frmOptions.chkMinimizeToTray.Checked = mReader.GetOptions.MinimizeToTray
        My.Forms.frmOptions.chkCloseToTray.Checked = mReader.GetOptions.CloseToTray

        My.Forms.frmOptions.chkStartup.Checked = isLoadedAsStartup(Application.ProductName, Application.ExecutablePath)
        My.Forms.frmOptions.chkWindowsService.Checked = m_AdminService.isRunningService(mReader.GetOptions.ServiceName)
        My.Forms.frmOptions.chkStartMaximized.Checked = mReader.GetOptions.MaximizedWindow
        My.Forms.frmOptions.chkRemWinSize.Checked = mReader.GetOptions.RememberWindowSize 'war59312
        My.Forms.frmOptions.chkRemWinLocation.Checked = mReader.GetOptions.RememberWindowLocation 'war59312
        
        'Global Configuration > Schedule

        My.Forms.frmOptions.nudInterval.Value = mReader.GetOptions.IndexingInterval
        My.Forms.frmOptions.chkIndexInterval.Checked = mReader.GetOptions.IsIndexingIntervalEnabled
        My.Forms.frmOptions.chkIndexTime.Checked = mReader.GetOptions.IsIndexAccordingToTime
        My.Forms.frmOptions.chkScheduelTasksForGui.Checked = mReader.GetOptions.RunTasksInGUI

        My.Forms.frmOptions.chkMonday.Checked = mReader.GetOptions.OnMonday
        My.Forms.frmOptions.chkTuesday.Checked = mReader.GetOptions.OnTuesday
        My.Forms.frmOptions.chkWednesday.Checked = mReader.GetOptions.OnWednesday
        My.Forms.frmOptions.chkThursday.Checked = mReader.GetOptions.OnThursday
        My.Forms.frmOptions.chkFriday.Checked = mReader.GetOptions.OnFriday
        My.Forms.frmOptions.chkSaturday.Checked = mReader.GetOptions.OnSaturday
        My.Forms.frmOptions.chkSunday.Checked = mReader.GetOptions.OnSunday

        'MessageBox.Show(m_Reader.GetOptions.mScheduleTime)
        My.Forms.frmOptions.dtpTime.Text = mReader.GetOptions.ScheduleTime

        ' MessageBox.Show("Scheduled for today: " & mReader.GetOptions.IsScheduledForToday)

        'Global Configuration > Appearance 
        My.Forms.frmOptions.chkMinimizeToTray.Checked = mReader.GetOptions.MinimizeToTray
        My.Forms.frmOptions.chkSystemTray.Checked = mReader.GetOptions.TrayIconIsEnabled
        My.Forms.frmOptions.chkCloseToTray.Checked = mReader.GetOptions.CloseToTray
        My.Forms.frmOptions.chkLoadToTray.Checked = mReader.GetOptions.TrayOnLoad

    End Sub

    Private mExtensions As String() = {".txt", ".log", ".wri", ".rtf", ".doc"}

    Public Sub LoadConfigForms()

        Console.WriteLine("Loaded Config Form")

        ' Folder List
        My.Forms.frmForm1.lbFolders.Items.Clear()
        For Each item As String In mReader.GetConfig.FolderList
            'frmForm1.lbFolders.Items.Add(item, True)
            My.Forms.frmForm1.lbFolders.Items.Add(item, True)
        Next

        ' Output
        My.Forms.frmConfig.txtIndexFileName.Text = mReader.GetConfig.IndexFileName

        My.Forms.frmConfig.cboIndexingEngine.Items.Clear()
        For Each tpEngine As TabPage In My.Forms.frmConfig.tcEngines.Controls
            My.Forms.frmConfig.cboIndexingEngine.Items.Add(tpEngine.Text.ToString)
        Next

        My.Forms.frmConfig.cboProcessPriority.Items.Clear()
        My.Forms.frmConfig.cboProcessPriority.Items.Add("Lowest")
        My.Forms.frmConfig.cboProcessPriority.Items.Add("Below Normal")
        My.Forms.frmConfig.cboProcessPriority.Items.Add("Normal")
        My.Forms.frmConfig.cboProcessPriority.Items.Add("Above Normal")
        My.Forms.frmConfig.cboProcessPriority.Items.Add("Highest")

        My.Forms.frmConfig.cboProcessPriority.SelectedIndex = mReader.GetConfig.ProcessPriority
        My.Forms.frmConfig.cboIndexingEngine.SelectedIndex = mReader.GetConfig.IndexingEngineID

        My.Forms.frmConfig.cboIndexFileExt.Items.Clear()
        If mReader.GetConfig.IndexingEngineID = 1 Then
            My.Forms.frmConfig.cboIndexFileExt.Items.Add(".xhtml")
            My.Forms.frmConfig.cboIndexFileExt.Items.Add(".html")
        End If
        For Each ext As String In mExtensions
            My.Forms.frmConfig.cboIndexFileExt.Items.Add(ext)
        Next

        If My.Forms.frmConfig.cboIndexFileExt.Items.Contains(mReader.GetConfig.IndexFileExtension) Then
            My.Forms.frmConfig.cboIndexFileExt.Text = mReader.GetConfig.IndexFileExtension
        Else
            My.Forms.frmConfig.cboIndexFileExt.SelectedIndex = 0
            mReader.GetConfig.IndexFileExtension = My.Forms.frmConfig.cboIndexFileExt.Text
        End If

        My.Forms.frmConfig.chkOneDir.Checked = mReader.GetConfig.isIndexFileInOneDir
        My.Forms.frmConfig.chkSameDir.Checked = mReader.GetConfig.isIndexFileInSameDir
        My.Forms.frmConfig.chkMergeFiles.Checked = mReader.GetConfig.isMergeFiles
        My.Forms.frmConfig.txtOutputDir.Text = mReader.GetConfig.OutputDir
        My.Forms.frmConfig.chkIndividualIndexFiles.Checked = mReader.GetConfig.CreateIndividualFilesInOutputDir

        'Engine > Tree Walk Utility
        My.Forms.frmConfig.chkIndexFiles.Checked = mReader.GetConfig.isAddFiles
        My.Forms.frmConfig.chkAscii.Checked = mReader.GetConfig.isAscii
        My.Forms.frmConfig.chkRemoveBranches.Checked = mReader.GetConfig.isRemoveTreeBranches

        My.Forms.frmConfig.txtFolderHeadingStyle.Text = mReader.GetConfig.FolderHeadingStyle


        'Engine > Tree.NET

        ' 2.7.1.4 Default CSS file path set to Application startup path default.css
        ' 2.7.1.3 Set CSS file path to default if CSS file does not exist

        If Not File.Exists(mReader.GetConfig.CssFilePath) Then
            mReader.GetConfig.CssFilePath = Application.StartupPath + "\default.css"
        End If

        'If Not File.Exists(mReader.GetConfig.CssFilePath) Or mReader.GetConfig.CssFilePath = "" Then
        '        mReader.GetConfig.CssFilePath = Application.StartupPath + "\default.css"
        'End If

        My.Forms.frmConfig.chkShowFilesTreeNet.Checked = mReader.GetConfig.ShowFilesTreeNet
        My.Forms.frmConfig.chkShowFileFullPath.Checked = mReader.GetConfig.ShowFilePath
        My.Forms.frmConfig.chkShowFullFolderPath.Checked = mReader.GetConfig.ShowFolderPath
        My.Forms.frmConfig.chkFileSize.Checked = mReader.GetConfig.ShowFileSize
        My.Forms.frmConfig.chkShowDirSize.Checked = mReader.GetConfig.ShowDirSize
        My.Forms.frmConfig.chkHideExtension.Checked = mReader.GetConfig.HideExtension
        My.Forms.frmConfig.chkFolderPathOnStatusBar.Checked = mReader.GetConfig.ShowFolderPathOnStatusBar
        My.Forms.frmConfig.chkShowVirtualFolders.Checked = mReader.GetConfig.ShowVirtualFolders
        My.Forms.frmConfig.chkReverseFileOrder.Checked = mReader.GetConfig.RevereFileOrder
        My.Forms.frmConfig.chkShowFileCount.Checked = mReader.GetConfig.ShowFileCount
        My.Forms.frmConfig.chkCollapseFolders.Checked = mReader.GetConfig.CollapseFolders

        My.Forms.frmConfig.cboFolderSortMode.Text = CStr(mReader.GetConfig.mSortBySize)
        My.Forms.frmConfig.txtServerInfo.Text = mReader.GetConfig.ServerInfo

        My.Forms.frmConfig.chkFolderSortSize.Checked = mReader.GetConfig.mSortBySize
        My.Forms.frmConfig.cboFolderSortMode.SelectedIndex = mReader.GetConfig.mSortBySizeMode

        My.Forms.frmConfig.chkAudioInfo.Checked = mReader.GetConfig.AudioInfo
        'My.Forms.frmConfig.chkAudioQuickScan.Checked = mReader.GetConfig.AudioQuickScan
        My.Forms.frmConfig.nudFolderExpandLevel.Value = mReader.GetConfig.FolderExpandLevel

        My.Forms.frmConfig.txtCssFilePath.Text = mReader.GetConfig.CssFilePath
        My.Forms.frmConfig.txtLogoPath.Text = mReader.GetConfig.LogoPath

        If mReader.GetConfig.ListType = Config.eListType.Numbered Then
            My.Forms.frmConfig.chkNumberFiles.CheckState = CheckState.Checked
        Else
            My.Forms.frmConfig.chkNumberFiles.CheckState = CheckState.Unchecked
        End If

        ' Compression 
        My.Forms.frmConfig.chkZipFiles.Checked = mReader.GetConfig.ZipAfterIndexed
        My.Forms.frmConfig.chkZipAndDelete.Checked = mReader.GetConfig.ZipAndDeleteFile
        My.Forms.frmConfig.chkZipMergedFile.Checked = mReader.GetConfig.ZipMergedFile
        My.Forms.frmConfig.chkZipFileInEachDir.Checked = mReader.GetConfig.ZipFilesInEachDir
        My.Forms.frmConfig.chkZipFilesInOutputDir.Checked = mReader.GetConfig.ZipFilesInOutputDir

        ' Filtering   

        My.Forms.frmConfig.chkEnableFilter.Checked = mReader.GetConfig.EnabledFiltering
        My.Forms.frmConfig.chkHideProtectedOperingSystemFiles.Checked = mReader.GetConfig.HideProtectedOperatingSystemFilesFolders

        'For Each c As Control In My.Forms.frmConfig.tpFilter.Controls
        '    c.Enabled = mReader.GetConfig.EnabledFiltering
        'Next

        My.Forms.frmConfig.gbFileFolderFiltering.Enabled = True
        My.Forms.frmConfig.chkEnableFilter.Enabled = True

        My.Forms.frmConfig.chkIgnoreHiddenFiles.Checked = mReader.GetConfig.IgnoreHiddenFiles
        My.Forms.frmConfig.chkIgnoreHiddenFolders.Checked = mReader.GetConfig.IgnoreHiddenFolders
        My.Forms.frmConfig.chkIgnoreSystemFiles.Checked = mReader.GetConfig.IgnoreSystemFiles
        My.Forms.frmConfig.chkIgnoreSystemFolders.Checked = mReader.GetConfig.IgnoreSystemFolders
        'war59312
        My.Forms.frmConfig.chkIgnoreEmptyFolders.Checked = mReader.GetConfig.IgnoreEmptyFolders

        My.Forms.frmConfig.chkIgnoreFollowingFiles.Checked = mReader.GetConfig.IgnoreFollowingFiles
        My.Forms.frmConfig.txtIgnoreFiles.Text = mReader.GetConfig.IgnoreFilesList

        'My.Forms.frmConfig.lblFilterNote.Visible = Not mReader.GetConfig.EnabledFiltering

        IsConfigFileEdited = False 'Bug: 2.0.30.2
        Console.WriteLine("Loaded Options Form")

    End Sub

End Module
