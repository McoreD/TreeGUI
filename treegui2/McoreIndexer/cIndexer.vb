Imports System.IO
Imports System.Windows.Forms

Public Class cIndexer

    Private mEngine As cEngine


    Public Function GetEngine() As cEngine
        Return Me.mEngine
    End Function

    Public Sub IndexAllConfigs(ByVal mSettings As cAdapter)

        Dim log As New IO.StreamWriter(mSettings.LogPathIndexer, True)

        Try
            log.WriteLine()
            log.WriteLine("##################################")
            log.WriteLine("Start Time: " & Now.ToString("yyyy-MM-dd 'at' HH:mm:ss"))
            log.WriteLine("##################################")
            ' 2.5.1.2 Display TreeGUI version in TreeGUI.Indexer.log
            log.WriteLine(Application.ProductName & " v." & Application.ProductVersion)
            log.WriteLine("Initialized indexing...")

            Dim strTasksFolderPath As String = mSettings.GetOptions.TasksFolderPath
            log.WriteLine("Tasks folder path: " + strTasksFolderPath)

            For Each strFile As String In Directory.GetFiles(strTasksFolderPath)

                Dim strFileExt As String = Path.GetExtension(strFile)
                If strFileExt.ToLower = ".tgc" Then
                    Dim r As New cAdapter
                    r.mInitMode = mSettings.mInitMode
                    r.LoadOptionsFile(mSettings.GetOptionsFilePath)
                    r.LoadConfigFile(strFile)

                    log.WriteLine("Indexing priority: " & r.GetConfig.ProcessPriority.ToString)
                    System.Threading.Thread.CurrentThread.Priority = r.GetConfig.ProcessPriority
                    log.WriteLine("First Folder: " + r.GetConfig.FolderList.Item(0).ToString)
                    log.WriteLine("Folder count: " + r.GetConfig.FolderList.Count.ToString)
                    ' 2.7.0.1 Display Indexer Engine name for each config in TreeGUI.Indexer.log
                    log.WriteLine("Indexing Engine: " + r.GetConfig.GetIndexingEngineName)
                    IndexConfig(r)
                    log.WriteLine("Indexed folders in " + strFile)
                End If

                ' 2.5.0.0 Windows Service can now execute any Command Scripts located in Task Folder
                If strFileExt.ToLower = ".bat" Or strFileExt.ToLower = ".cmd" Then
                    mSettings.StartHiddenProcess(strFile, False)
                    log.WriteLine("Executed " + strFile)
                End If

            Next
            log.WriteLine("Finalized indexing for this session")
        Catch ex As Exception
            ' 2.5.1.0 Log exceptions while indexing to TreeGUI.Indexer.log
            log.WriteLine(ex.Message)
            log.WriteLine(ex.StackTrace)
        Finally
            log.Close()
        End Try

    End Sub


    Private mDoneSoFar As Integer = 0

    Public Sub IndexConfig(ByVal settings As cAdapter)

        Dim folderList As New List(Of String)
        folderList = settings.GetConfig.FolderList

        If folderList.Count > 0 Then

            Dim isIndexFileInSameDir As Boolean = settings.GetConfig.isIndexFileInSameDir
            Dim isIndexFileInOneDir As Boolean = settings.GetConfig.isIndexFileInOneDir
            Dim isStartupItem As Boolean = settings.GetOptions.StartupItem
            Dim isRunTasksInGUI As Boolean = settings.GetOptions.RunTasksInGUI
            Dim isIndividualFiles As Boolean = settings.GetConfig.CreateIndividualFilesInOutputDir
            Dim isMergeFiles As Boolean = settings.GetConfig.isMergeFiles

            Dim isEventLoggable As Boolean = False

            Select Case settings.GetConfig.IndexingEngineID
                Case cAdapter.IndexingEngine.TreeLib
                    mEngine = New cTreeLib(settings)
                Case cAdapter.IndexingEngine.TreeNetLib
                    mEngine = New cTreeNetLib(settings)
            End Select

            mEngine.Progress = Me.mDoneSoFar

            If isIndexFileInSameDir Then
                mEngine.IndexNow(cAdapter.IndexingMode.IN_EACH_DIRECTORY)
                Me.mDoneSoFar = mEngine.Progress
                isEventLoggable = True
            End If

            If isIndexFileInOneDir And isIndividualFiles Then
                mEngine.IndexNow(cAdapter.IndexingMode.IN_ONE_FOLDER_SEPERATE)
                Me.mDoneSoFar = mEngine.Progress
                isEventLoggable = True
            End If

            If isIndexFileInOneDir And isMergeFiles Then
                mEngine.IndexNow(cAdapter.IndexingMode.IN_ONE_FOLDER_MERGED)
                Me.mDoneSoFar = mEngine.Progress
                isEventLoggable = True
            End If

            If isEventLoggable Then
                settings.WriteEventLog(folderList, settings.mInitMode)
            End If

        End If 'Folder Count > 0

    End Sub

End Class
