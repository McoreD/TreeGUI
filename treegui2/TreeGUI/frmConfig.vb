Public Class frmConfig



    Private mIsGuiReady As Boolean = False



    Private Sub frmConfig_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        mIsGuiReady = False

        cboIndexingEngine.Enabled = False

        My.Forms.frmForm1.sbarLeft.Text = "Ready"

    End Sub



    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.tpTree.Text = mSettings.GetAdapter.GetConfig.GetIndexingEngineName(0)
        Me.tpTreeNet.Text = mSettings.GetAdapter.GetConfig.GetIndexingEngineName(1)



        Me.Text = mSettings.GetConfigFileName + " Properties"

        Me.Icon = My.Forms.frmForm1.Icon

        If My.Forms.frmForm1.TopMost = True Then

            My.Forms.frmForm1.ToggleFormAlwaysOnTop()

        End If



        Me.tcConfig.SelectedTab = Me.tpEngines

        Me.tcEngines.SelectedTab = Me.tpTreeNet
        Me.tcTreeNet.SelectedTab = Me.tpXHTML

        LoadConfigForms()

    End Sub



    Private Sub sUpdateGuiControls()


        chkMergeFiles.Enabled = chkOneDir.Checked


        txtOutputDir.Enabled = chkOneDir.Checked

        btnBrowseSingleDir.Enabled = chkOneDir.Checked

        chkIndividualIndexFiles.Enabled = chkOneDir.Checked



        If mSettings.GetAdapter.GetConfig.FolderList.Count = 1 Then

            chkOneDir.Text = "Create index file in the following directory"

        Else

            chkOneDir.Text = "Create index files in the following directory"

        End If



        chkZipFileInEachDir.Enabled = chkZipFiles.Checked

        chkZipAndDelete.Enabled = chkZipFiles.Checked

        chkZipFilesInOutputDir.Enabled = chkZipFiles.Checked

        chkZipMergedFile.Enabled = chkZipFiles.Checked



        chkShowFullFolderPath.Enabled = chkShowFilesTreeNet.Checked

        chkFileSize.Enabled = chkShowFilesTreeNet.Checked

        chkShowFileFullPath.Enabled = chkShowFilesTreeNet.Checked

        chkNumberFiles.Enabled = chkShowFilesTreeNet.Checked



        txtIgnoreFiles.Enabled = chkIgnoreFollowingFiles.Checked



        gbFiles.Enabled = chkEnableFilter.Checked

        gbFolders.Enabled = chkEnableFilter.Checked



        chkShowVirtualFolders.Enabled = chkFolderPathOnStatusBar.Checked



        'chkAudioQuickScan.Enabled = chkAudioInfo.Checked



        chkHideProtectedOperingSystemFiles.Enabled = chkEnableFilter.Checked

        nudFolderExpandLevel.Enabled = chkCollapseFolders.Checked

        sEnableApplyButton()

        cboFolderSortMode.Enabled = chkFolderSortSize.Checked

    End Sub



    Public Function sBrowseOutputDir() As String



        Dim dlg As New McoreSystem.FolderBrowser



        dlg.Title = "Choose directory to index"

        dlg.Flags = McoreSystem.BrowseFlags.BIF_NEWDIALOGSTYLE Or _
                    McoreSystem.BrowseFlags.BIF_STATUSTEXT Or _
                    McoreSystem.BrowseFlags.BIF_EDITBOX

        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

            If dlg.DirectoryPath.Length > 0 Then

                txtOutputDir.Text = dlg.DirectoryPath

                btnApply.Enabled = True

            End If

        End If



        Return txtOutputDir.Text



    End Function





    Private Sub btnBrowseSingleDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseSingleDir.Click

        sBrowseOutputDir()

    End Sub



    Private Const DLG_FILTER_CSS As String = "Cascading Style Sheet (*.css)|*.css"



    Private Sub btnImportCss_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportCss.Click

        Dim dlg As New OpenFileDialog

        dlg.InitialDirectory = Application.StartupPath

        dlg.Filter = DLG_FILTER_CSS

        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

            txtCssFilePath.Text = dlg.FileName

            btnApply.Enabled = True

        End If

    End Sub



    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click

        SaveConfigForms()

        btnApply.Enabled = False

    End Sub



    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me.Close()



    End Sub



    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        SaveConfigForms()

        Me.Close()



    End Sub



    Private Sub chkOneDir_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOneDir.CheckedChanged

        sUpdateGuiControls()

    End Sub



    Private Sub frmConfig_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        sUpdateGuiControls()

        btnApply.Enabled = False

        mIsGuiReady = True

        cboIndexingEngine.Enabled = True

    End Sub



    Private Sub chkZipFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkZipFiles.CheckedChanged

        sUpdateGuiControls()

    End Sub



    Private Sub chkIndividualIndexFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIndividualIndexFiles.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkIgnoreFollowingFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIgnoreFollowingFiles.CheckedChanged



        sUpdateGuiControls()



        If chkIgnoreFollowingFiles.CheckState = CheckState.Checked Then

            If txtIgnoreFiles.Text.Length = 0 Then

                txtIgnoreFiles.Text = (New cAdapter).GetConfig.IgnoreFilesList

            End If

        End If



    End Sub





    Private Sub chkIgnoreHiddenFolders_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIgnoreHiddenFolders.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkShowFilesTreeNet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowFilesTreeNet.CheckedChanged

        sUpdateGuiControls()

    End Sub



    Private Sub chkIgnoreSystemFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIgnoreSystemFiles.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkAscii_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAscii.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkIgnoreSystemFolders_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIgnoreSystemFolders.CheckedChanged

        sEnableApplyButton()

    End Sub



    'Enables Apply Button When Changed

    Private Sub sEnableApplyButton()

        'Console.WriteLine("wtf")

        btnApply.Enabled = True

    End Sub



    Private Sub chkIgnoreHiddenFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIgnoreHiddenFiles.CheckedChanged

        sEnableApplyButton()

    End Sub



    'war59312

    Private Sub chkIgnoreEmptyFolders_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIgnoreEmptyFolders.CheckedChanged



        sEnableApplyButton()



    End Sub



    Private Sub chkZipAndDelete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkZipAndDelete.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkZipMergedFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkZipMergedFile.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkZipFileInEachDir_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkZipFileInEachDir.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkZipFilesInOutputDir_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkZipFilesInOutputDir.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkIndexFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIndexFiles.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkRemoveBranches_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRemoveBranches.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkFileSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileSize.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkShowFullFolderPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowFullFolderPath.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkShowFileFullPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowFileFullPath.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkNumberFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNumberFiles.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkShowDirSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowDirSize.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkHideExtension_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHideExtension.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkSameDir_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSameDir.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkShowVirtualFolders_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowVirtualFolders.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkFolderPathOnStatusBar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFolderPathOnStatusBar.CheckedChanged



        sEnableApplyButton()

        chkShowVirtualFolders.Enabled = chkFolderPathOnStatusBar.Checked



    End Sub



    Private Sub txtFolderHeadingStyle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolderHeadingStyle.TextChanged

        sEnableApplyButton()

    End Sub



    Private Sub txtServerInfo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServerInfo.TextChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkAudioInfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAudioInfo.CheckedChanged



        sEnableApplyButton()



        If mIsGuiReady AndAlso chkAudioInfo.CheckState = CheckState.Checked Then

            If MessageBox.Show("Scanning for MP3 tags can take a very long time and is not recommended for folders with large a MP3 collection." & Environment.NewLine & "As a rough guide, files will be scanned at 1000 MP3s per minute." & Environment.NewLine & "Do you want to turn this feature on?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then

                chkAudioInfo.CheckState = CheckState.Unchecked

                btnApply.Enabled = False

            End If

        End If



        'chkAudioQuickScan.Enabled = chkAudioInfo.Checked



    End Sub



    Private Sub chkAudioQuickScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        sEnableApplyButton()

    End Sub



    Private Sub chkShowFileCount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowFileCount.CheckedChanged

        sEnableApplyButton()

    End Sub



    Private Sub txtIgnoreFiles_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIgnoreFiles.TextChanged

        sEnableApplyButton()

    End Sub



    Private Sub chkEnableFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableFilter.CheckedChanged



        chkHideProtectedOperingSystemFiles.Enabled = chkEnableFilter.Checked

        sUpdateGuiControls()



    End Sub



    Private Sub cboIndexingEngine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboIndexingEngine.SelectedIndexChanged

        sEnableApplyButton()

    End Sub



    Private Sub cboProcessPriority_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProcessPriority.SelectedIndexChanged

        sEnableApplyButton()

    End Sub

    Private Sub chkCollapseFolders_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCollapseFolders.CheckedChanged
        sUpdateGuiControls()
    End Sub

    Private Sub btnLogoBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogoBrowse.Click

        Dim dlg As New OpenFileDialog

        dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)

        dlg.Filter = "Images (*.PNG;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"

        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

            txtLogoPath.Text = dlg.FileName

            btnApply.Enabled = True

        End If

    End Sub

    Private Sub chkFolderSortSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFolderSortSize.CheckedChanged
        cboFolderSortMode.Enabled = chkFolderSortSize.Checked
    End Sub
End Class