Imports System.IO


Public Class frmOptions

    Private Const tipIndexingEngine As String = "Modifying the Indexing Engine will require you to resave Configuration file."

    Private Sub btnBrowseJobsFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseJobsFolder.Click

        Dim dlg As New McoreSystem.FolderBrowser
        dlg.Title = "Browse Jobs Folder Path"
        dlg.Flags = McoreSystem.BrowseFlags.BIF_NEWDIALOGSTYLE Or _
                    McoreSystem.BrowseFlags.BIF_STATUSTEXT Or _
                    McoreSystem.BrowseFlags.BIF_EDITBOX
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            '1.4.0.1 It was possible to add My Computer and cause Tree.com to crash
            If dlg.DirectoryPath.Length > 0 Then
                txtJobsFolderPath.Text = dlg.DirectoryPath
            End If
        End If
    End Sub

    Private Sub frmOptions_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        My.Forms.frmForm1.sbarLeft.Text = "Ready"
    End Sub



    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'updateGuiControls()

        Me.Text = Application.ProductName + " Options"
        Me.Icon = My.Forms.frmForm1.Icon
        If My.Forms.frmForm1.TopMost = True Then
            My.Forms.frmForm1.ToggleFormAlwaysOnTop()
        End If

        'Me.ttTip.SetToolTip(Me.cboIndexingEngine, tipIndexingEngine)

        dtpTime.Format = DateTimePickerFormat.Time

        chkMonday.Tag = "Monday"
        chkTuesday.Tag = "Tuesday"
        chkWednesday.Tag = "Wednesday"
        chkThursday.Tag = "Thursday"
        chkFriday.Tag = "Friday"
        chkSaturday.Tag = "Saturday"
        chkSunday.Tag = "Sunday"





        mSettings.LoadOptionsForm()

    End Sub


    Private Sub chkIndexTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIndexTime.CheckedChanged

        updateGuiControls()

    End Sub

    Private Sub updateGuiControls()



        gbDaysOfWeek.Enabled = chkIndexTime.Checked
        ' gbAdminOptions.Enabled = My.User.IsInRole("Administrators") 'Or My.Computer.Info.OSFullName.IndexOf("Vista") <> -1
        chkWindowsService.Enabled = My.User.IsInRole("Administrators") 'Or My.Computer.Info.OSFullName.IndexOf("Vista") <> -1

        chkMinimizeToTray.Enabled = chkSystemTray.Checked
        chkCloseToTray.Enabled = chkSystemTray.Checked
        chkLoadToTray.Enabled = chkSystemTray.Checked

        'gpOpenDefaultConfig.Enabled = chkOpenDefaultConfig.Checked
        nudInterval.Enabled = chkIndexInterval.Checked

    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged

        chkMonday.CheckState = chkSelectAll.CheckState
        chkTuesday.CheckState = chkSelectAll.CheckState
        chkWednesday.CheckState = chkSelectAll.CheckState
        chkThursday.CheckState = chkSelectAll.CheckState
        chkFriday.CheckState = chkSelectAll.CheckState
        chkSaturday.CheckState = chkSelectAll.CheckState
        chkSunday.CheckState = chkSelectAll.CheckState

    End Sub

    Public Sub ShowNeedAdministratorMsgBox()
        MessageBox.Show("You have to be logged in as an Administrator", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    Private Sub chkWindowsService_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWindowsService.CheckedChanged

        ' My.Forms.frmForm1.updateGuiControls()

        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            'If My.User.IsInRole("Administrators") Then
            Dim svc As New TreeGUI.cAdminService
            If chkWindowsService.CheckState = CheckState.Checked Then
                FixWindowsService() ' Fix if broken
                Dim servicePath As String = Application.StartupPath + "\McoreIndexer.exe"
                svc.InstallAndRunService(servicePath, "McoreIndexer")
            Else
                svc.stopServiceInternally("McoreIndexer")
            End If
        End If

    End Sub


    Private Sub frmOptions_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        updateGuiControls()

    End Sub

    Private Sub chkSystemTray_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSystemTray.CheckedChanged

        My.Forms.frmForm1.niTray.Visible = chkSystemTray.Checked
        updateGuiControls()
        'mSettings.SaveOptionsForm()

    End Sub

    Private Sub chkOneDir_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        updateGuiControls()

    End Sub

    Private ReadOnly DLG_FILTER_OPTIONSFILE As String = Application.ProductName + " Options File (*" + mSettings.GetAdapter.OPTIONS_FILE_EXT + ")|*" + mSettings.GetAdapter.OPTIONS_FILE_EXT


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        mSettings.SaveOptionsForm()
        mSettings.GetAdapter.WriteOptionsFile()

        'MsgBox(mSettings.GetReader.GetConfig.IndexFileExtension)

        Me.Close()
        My.Forms.frmForm1.sbarLeft.Text = "Saved " & mSettings.GetAdapter.GetOptionsFilePath

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()

    End Sub

    Private Sub btnIndexAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIndexAll.Click
        mSettings.IndexAll()
    End Sub

    Private Sub btnBackupOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackupOptions.Click
        Dim dlg As New SaveFileDialog
        dlg.Filter = Me.DLG_FILTER_OPTIONSFILE
        dlg.FileName = IO.Path.GetFileName(mSettings.GetAdapter.GetOptionsFilePath)
        dlg.InitialDirectory = Application.StartupPath
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            'Dim f As String = "\" + Now.ToString("yyyy-MM-dd")
            My.Computer.FileSystem.CopyFile(mSettings.GetAdapter.GetOptionsFilePath, dlg.FileName, True)
        End If
    End Sub


    Private Sub btnAssociateTgc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssociateTgc.Click

        Dim fam As New FileExtensionManager2
        fam.CreateFileAssociation(".tgc", "TreeGUI.Config", Application.ProductName & " Config File", Application.ExecutablePath, 0)
        btnAssociateTgc.Enabled = False

    End Sub

    Private Sub btnUninstallWindowsService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUninstallWindowsService.Click
        If (MessageBox.Show(String.Format("To uninstall McoreIndexer, {0} has to be closed.", Application.ProductName), Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK) Then
            Dim svc As New cAdminService
            svc.StopAndUninstallService("McoreIndexer")
            My.Forms.frmForm1.Close()
        End If
    End Sub

    Private Sub btnFixWindowsServicePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFixWindowsServicePath.Click

        FixWindowsService()
    End Sub

    Private Sub cboIndexingEngine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If cboIndexingEngine.Enabled Then
        '    SaveConfigForms()
        '    'WriteConfigFile(GetConfigFilePath)
        'End If
    End Sub

    Private Sub btnRestoreOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreOptions.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = Me.DLG_FILTER_OPTIONSFILE
        dlg.FileName = IO.Path.GetFileName(mSettings.GetAdapter.GetOptionsFilePath)
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            mSettings.GetAdapter.LoadOptionsFile(dlg.FileName)
            LoadOptionsForm()
        End If
    End Sub

    Private Sub chkIgnoreHSFilesFolders_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub chkEnableFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        updateGuiControls()
    End Sub

    Private Sub chkOpenDefaultConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOpenDefaultConfig.CheckedChanged
        updateGuiControls()
    End Sub

    Private Sub btnBrowseDefaultConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDefaultConfig.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = DLG_FILTER_TGC
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtDefaultConfigFilePath.Text = dlg.FileName
        End If
    End Sub

    Private Sub chkIndexInterval_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIndexInterval.CheckedChanged
        nudInterval.Enabled = chkIndexInterval.Checked
    End Sub

    Private Sub btnContextMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContextMenu.Click

        Dim key1 As Microsoft.Win32.RegistryKey
        key1 = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\Classes\Folder\shell\Index Using TreeGUI\command")
        key1.SetValue("", Application.ExecutablePath & " ""%1""")
        key1.Close()

    End Sub
End Class
