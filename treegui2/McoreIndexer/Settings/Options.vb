Imports System.ComponentModel
<Serializable()> Public Class Options

    Private m_TasksFolderPath As String

    Private m_ProcessPriorityId As Integer
    Private m_EnableScheduleInGuiMode As Boolean
    Private m_AlwaysOnTop As Boolean = False
    Public MaximizedWindow As Boolean = False
    Public RememberWindowSize As Boolean = False 'war59312
    Public RememberWindowLocation As Boolean = False 'war59312

    ' General Configuration > Appearance
    Private m_MinimizeToTray As Boolean = True
    Private m_TrayIcon As Boolean = True
    Private m_CloseToTray As Boolean
    Private m_TrayOnLoad As Boolean

    Private m_StartupItem As Boolean
    Private m_RunTasksInGui As Boolean

    ' General Configuration > Schedule 
    Private m_IndexingInterval As Integer = 30
    Private m_IndexingIntervalIsEnabled As Boolean
    Private m_IndexAccordingToTime As Boolean

    Public ReadOnly IgnoreFilesListDelimiter As String = "|"

    Public OnMonday As Boolean
    Public OnTuesday As Boolean
    Public OnWednesday As Boolean
    Public OnThursday As Boolean
    Public OnFriday As Boolean
    Public OnSaturday As Boolean = True
    Public OnSunday As Boolean = True

    Public ScheduleTime As String = "03:00:00"
    <Xml.Serialization.XmlIgnore()> _
    Public IsScheduledForToday As Boolean



    <Xml.Serialization.XmlElement("EmptyFolderMessage")> _
    Public EmptyFolderMessage As String = "Folder is Empty."

    Public IndividualIndexFileWordSeperator As String = "-"

    ' Misc

    Public ForceSave As Boolean = False
    Public OpenDefaultConfig As Boolean = False
    
    Private mDefaultConfigFilePath As String

    Private mTimeInUTC As Boolean = True

    Private mRecentFiles As New List(Of String)

    Public Property RecentFiles() As List(Of String)
        Get
            Return mRecentFiles
        End Get
        Set(ByVal value As List(Of String))
            mRecentFiles = value
        End Set
    End Property

    Public Property IndexedTimeInUTC() As Boolean
        Get
            Return mTimeInUTC
        End Get
        Set(ByVal value As Boolean)
            mTimeInUTC = value
        End Set
    End Property


    Public Property DefaultConfigFilePath() As String
        Get
            Return mDefaultConfigFilePath
        End Get
        Set(ByVal value As String)
            mDefaultConfigFilePath = value
        End Set
    End Property


    Public ReadOnly Property ServiceName() As String
        Get
            Return "McoreIndexer"
        End Get
    End Property

    Public Property IsIndexAccordingToTime() As Boolean
        Get
            Return m_IndexAccordingToTime
        End Get
        Set(ByVal value As Boolean)
            m_IndexAccordingToTime = value
        End Set
    End Property
    Public Property IsIndexingIntervalEnabled() As Boolean
        Get
            Return m_IndexingIntervalIsEnabled
        End Get
        Set(ByVal value As Boolean)
            m_IndexingIntervalIsEnabled = value
        End Set
    End Property

    Public Property TrayIconIsEnabled() As Boolean
        Get
            Return m_TrayIcon
        End Get
        Set(ByVal value As Boolean)
            m_TrayIcon = value
        End Set
    End Property

    Public Property TrayOnLoad() As Boolean
        Get
            Return m_TrayOnLoad And TrayIconIsEnabled
        End Get
        Set(ByVal value As Boolean)
            m_TrayOnLoad = value
        End Set
    End Property

    Public Property CloseToTray() As Boolean
        Get
            Return m_CloseToTray And TrayIconIsEnabled
        End Get
        Set(ByVal value As Boolean)
            m_CloseToTray = value
        End Set
    End Property

    Public Property MinimizeToTray() As Boolean
        Get
            Return m_MinimizeToTray And TrayIconIsEnabled
        End Get
        Set(ByVal value As Boolean)
            m_MinimizeToTray = value
        End Set
    End Property

    Public Property IndexingInterval() As Integer
        Get
            Return Me.m_IndexingInterval
        End Get
        Set(ByVal value As Integer)
            m_IndexingInterval = value
        End Set
    End Property

    Public Property RunTasksInGUI() As Boolean
        Get
            Return Me.m_RunTasksInGui
        End Get
        Set(ByVal value As Boolean)
            m_RunTasksInGui = value
        End Set
    End Property

    Public Property StartupItem() As Boolean
        Get
            Return Me.m_StartupItem
        End Get
        Set(ByVal value As Boolean)
            Me.m_StartupItem = value
        End Set
    End Property

    Public Property AlwaysOnTop() As Boolean
        Get
            Return m_AlwaysOnTop
        End Get
        Set(ByVal value As Boolean)
            m_AlwaysOnTop = value
        End Set
    End Property
    Public Property TasksFolderPath() As String
        Get
            Return Me.m_TasksFolderPath
        End Get
        Set(ByVal value As String)
            m_TasksFolderPath = value
        End Set
    End Property



End Class
