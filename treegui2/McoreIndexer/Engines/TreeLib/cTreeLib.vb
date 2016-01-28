Imports System.IO

Public Class cTreeLib
    Inherits cEngine

    '* Indexes a Root Folder using tree.com

    Private mSettings As New TreeGUI.cAdapter
    Private m_CurrentIndexFilePath As String
    Private isFirstEntryToSingleFile As Boolean = True

    Private Function GetReader() As TreeGUI.cAdapter
        Return Me.mSettings
    End Function

    Private Sub setCurrentIndexFilePath(ByVal filePath As String)
        Me.m_CurrentIndexFilePath = filePath
    End Sub

    Public Sub New(ByVal myReader As TreeGUI.cAdapter)
        MyBase.New(myReader)
        Me.mSettings = myReader
        If Me.mSettings.GetConfig.IndexFileExtension.Contains("html") Then
            Me.mSettings.GetConfig.IndexFileExtension = ".txt"
        End If
    End Sub

    Private Function getAddFilesSwitch() As String

        If mSettings.GetConfig.isAddFiles Then
            Return " /f"
        End If
        Return Nothing

    End Function


    Private Function getAsciiSwitch() As String

        If mSettings.GetConfig.isAscii = True Then
            Return " /a"
        End If
        Return Nothing

    End Function

    Private Function getOutputSwitch(ByVal folderPath As String, ByVal mode As cAdapter.IndexingMode) As String

        Select Case mode

            Case cAdapter.IndexingMode.IN_EACH_DIRECTORY
                Me.setCurrentIndexFilePath(folderPath + Path.DirectorySeparatorChar + mSettings.GetConfig.GetIndexFileName)
                Return ">" + Chr(34) + Me.getCurrentIndexFilePath + Chr(34)

            Case cAdapter.IndexingMode.IN_ONE_FOLDER_SEPERATE

                Dim strOutputFileName As String
                Dim sep As String = mSettings.GetOptions.IndividualIndexFileWordSeperator
                If Path.GetFileName(folderPath).Length > 0 Then
                    strOutputFileName = Path.GetPathRoot(folderPath).Substring(0, 1) + sep + Path.GetFileName(folderPath) + sep + mSettings.GetConfig.GetIndexFileName
                Else
                    strOutputFileName = Path.GetPathRoot(folderPath).Substring(0, 1) + sep + mSettings.GetConfig.GetIndexFileName
                End If
                Me.setCurrentIndexFilePath(mSettings.GetConfig.OutputDir + Path.DirectorySeparatorChar + strOutputFileName)
                Return ">" + Chr(34) + Me.getCurrentIndexFilePath + Chr(34)

            Case cAdapter.IndexingMode.IN_ONE_FOLDER_MERGED
                'If chkSingleFile.CheckState = CheckState.Checked Then
                If mSettings.GetConfig.isMergeFiles Then 'Fixed after ALPHA 16
                    'Me.targetFilePath = txtOutputDir.Text + "\" + txtFileName.Text + cboExt.Text
                    Me.setCurrentIndexFilePath(Me.mSettings.GetConfig.GetIndexFilePath)
                    If isFirstEntryToSingleFile = True Then
                        isFirstEntryToSingleFile = False
                        Return ">" + Chr(34) + Me.getCurrentIndexFilePath + Chr(34)
                    Else
                        Return ">>" + Chr(34) + Me.getCurrentIndexFilePath + Chr(34)
                    End If
                End If


        End Select

        Return Nothing

    End Function

    Public Overrides Sub IndexNow(ByVal mIndexMode As cAdapter.IndexingMode)

        Dim tree As New cTreeLib(mSettings)
        Dim isMergeFile As Boolean = mSettings.GetConfig.isMergeFiles
        Dim isRemoveBranches As Boolean = mSettings.GetConfig.isRemoveTreeBranches

        For i As Integer = 0 To mSettings.GetConfig.FolderList.Count - 1

            Dim TEMP_FILE As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\temp" + i.ToString + ".bat"
            Dim CURRENT_DIR As String = mSettings.GetConfig.FolderList.Item(i)

            Dim TREE_COMMAND As String = "%windir%\system32\tree.com " + tree.getSourceSwitch(CURRENT_DIR) + tree.getAsciiSwitch() + tree.getAddFilesSwitch() + tree.getOutputSwitch(CURRENT_DIR, mIndexMode)
            Console.WriteLine(TREE_COMMAND)
            FileOpen(1, TEMP_FILE, OpenMode.Output)
            PrintLine(1, TREE_COMMAND)

            '1.5.3.4 Didn't tag index files created in the same folder witout appending
            If mIndexMode = cAdapter.IndexingMode.IN_EACH_DIRECTORY Or i = mSettings.GetConfig.FolderList.Count - 1 _
               Or (isMergeFile = False And mIndexMode = cAdapter.IndexingMode.IN_ONE_FOLDER_MERGED) Then
                PrintLine(1, mSettings.getBlankLine(tree.getCurrentIndexFilePath))
                PrintLine(1, mSettings.getFooterText(tree.getCurrentIndexFilePath, cAdapter.IndexingEngine.TreeLib))
            End If

            PrintLine(1, "DEL " + Chr(34) + TEMP_FILE + Chr(34))
            FileClose(1)

            Dim proc As New Process
            proc = mSettings.StartHiddenProcess(TEMP_FILE, True)

            If isRemoveBranches Then
                tree.removeTreeBranches(tree.getCurrentIndexFilePath)
            End If

            If mIndexMode = cAdapter.IndexingMode.IN_EACH_DIRECTORY Or i = mSettings.GetConfig.FolderList.Count - 1 _
             Or (isMergeFile = False And mIndexMode = cAdapter.IndexingMode.IN_ONE_FOLDER_MERGED) Then

                If mSettings.GetConfig.ZipFilesInEachDir Then
                    mSettings.ZipAdminFile(tree.getCurrentIndexFilePath)
                End If

                If mSettings.GetConfig.ZipMergedFile Then
                    mSettings.ZipAdminFile(tree.getCurrentIndexFilePath)
                End If

            End If

            If mIndexMode = cAdapter.IndexingMode.IN_ONE_FOLDER_SEPERATE Then
                If mSettings.GetConfig.ZipFilesInOutputDir Then
                    'MsgBox(tree.getCurrentIndexFilePath)
                    mSettings.ZipAdminFile(tree.getCurrentIndexFilePath)
                End If

            End If

            If proc.HasExited Then
                Me.Progress += 1
                Me.CurrentDirMessage = "Indexed " + mSettings.GetConfig.FolderList.Item(i)
            End If
        Next

    End Sub


    Private Sub removeTreeBranches(ByVal filePath As String)
        Try
            Dim sr As New System.IO.StreamReader(filePath)
            Dim file As String = sr.ReadToEnd()
            file = file.Replace("---", " ")
            file = file.Replace(" +", "  ")
            file = file.Replace("+ ", "  ")
            file = file.Replace("|", "    ") ' 4 spaces
            file = file.Replace(" \", "  ")
            file = file.Replace("\ ", "  ")
            sr.Close()
            Dim sw As New System.IO.StreamWriter(filePath)
            sw.WriteLine(file)
            sw.Close()
        Catch ex As Exception
            ' Do Nothing
        End Try

    End Sub

    Private Function getSourceSwitch(ByVal folderPath As String) As String
        Return Chr(34) + folderPath + Chr(34)
    End Function

    Private Function getCurrentIndexFilePath() As String
        Return Me.m_CurrentIndexFilePath
    End Function

End Class
