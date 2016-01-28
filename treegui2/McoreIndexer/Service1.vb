Imports System.IO

Public Class McoreIndexer

    Private mSettings As New TreeGUI.cAdapter
    Private mIndexer As cIndexer

    Protected Overrides Sub OnStart(ByVal args() As String)

        Dim wmi As New WMI_Library
        Dim log As New StreamWriter(mSettings.LOG_PATH_ONSTART, True)
        log.WriteLine()
        log.WriteLine("##################################")
        log.WriteLine("Start Time: " & Now.ToString("yyyy-MM-dd 'at' HH:mm:ss"))
        log.WriteLine("##################################")
        log.WriteLine("Logged on User: " & WMI_Library.GetLoggedInUser)
        log.WriteLine("Reading Options file: " + mSettings.GetOptionsFilePath)
        mSettings.LoadOptionsFile(mSettings.GetOptionsFilePath) 'Load Options for the first time

        log.WriteLine("Interval Indexing is enabled? " & mSettings.GetOptions.IsIndexingIntervalEnabled)
        log.WriteLine("DateTime Indexing is enabled? " & mSettings.GetOptions.IsIndexAccordingToTime)

        log.Close()

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
    End Sub

    Private Sub tmrInterval_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrInterval.Elapsed

        ' New instance of Indexer
        mIndexer = New cIndexer()
        ' Specify Initialization Mode
        Me.mSettings.mInitMode = cAdapter.InitializationMode.INTERVAL_BASED_SERVICE
        ' Load Settings and Index all Configs
        mIndexer.IndexAllConfigs(Me.mSettings)
        ' Update Timer Interval
        tmrInterval.Interval = mSettings.getIntervalInMilliseconds

    End Sub

    Private Sub tmrDateTime_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrDateTime.Elapsed

        If mSettings.GetOptions.IsIndexAccordingToTime AndAlso _
                    mSettings.GetOptions.IsScheduledForToday AndAlso _
                    mSettings.GetOptions.ScheduleTime = Now.ToString("HH:mm:ss") Then

            ' New instance of Indexer
            mIndexer = New cIndexer()
            ' Specify Initialization Mode
            Me.mSettings.mInitMode = cAdapter.InitializationMode.DATETIME_BASED_SERVICE
            ' Load Settings and Index all Configs
            mIndexer.IndexAllConfigs(Me.mSettings)
        End If

    End Sub

    Private Sub tmrSettingsReader_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrSettingsReader.Elapsed

        '***********
        'This order has to be maintained otherwise it won't start indexing.
        Dim log As New StreamWriter(mSettings.LOG_PATH_READER, False)
        '****************
        log.WriteLine()
        log.WriteLine("##################################")
        log.WriteLine("Start Time: " & Now.ToString("yyyy-MM-dd 'at' HH:mm:ss"))
        log.WriteLine("##################################")
        log.WriteLine("Re-reading Options file: " + mSettings.GetOptionsFilePath)

        mSettings.LoadOptionsFile(mSettings.GetOptionsFilePath) 'Need uptodate options

        log.WriteLine("Interval Indexing is enabled? " & mSettings.GetOptions.IsIndexingIntervalEnabled)
        log.WriteLine("DateTime Indexing is enabled? " & mSettings.GetOptions.IsIndexAccordingToTime)
        log.WriteLine("Indexing Interval set to: " & mSettings.getIntervalInMilliseconds)        
        If mSettings.GetOptions.IsIndexingIntervalEnabled Then
            log.WriteLine("Interval Indexing will start at: " & Now.AddMilliseconds(mSettings.getIntervalInMilliseconds).ToString("HH:mm:ss"))
        End If
        log.WriteLine("DateTime indexing scheduled at: " & mSettings.GetOptions.ScheduleTime)
        log.Close()

        ' This has to be checked everytime Option files settings are re-read
        ' Not only when Service is started. 
        ' BUG: 2.2.5.2 Indexing according to interval setting was only checked on Service start
        If mSettings.GetOptions.IsIndexingIntervalEnabled Then
            ' If Timer is not already enabled then enable
            If Not tmrInterval.Enabled Then
                tmrInterval.Enabled = True
                tmrInterval.Start()
            End If
        Else
            ' If option file says Interval Indexing is disabled
            tmrInterval.Stop()
            tmrInterval.Enabled = False
        End If

    End Sub
End Class
