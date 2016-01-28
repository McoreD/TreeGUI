Imports System.ServiceProcess
Imports System.IO


Public Class Service1
    Inherits System.ServiceProcess.ServiceBase

#Region " Component Designer generated code "

    Public Sub New()
        MyBase.New()

        ' This call is required by the Component Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call

    End Sub

    'UserService overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New Service1}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    Friend WithEvents tmrInterval As System.Timers.Timer
    Private Sub InitializeComponent()
        Me.tmrInterval = New System.Timers.Timer
        CType(Me.tmrInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'tmrInterval
        '
        Me.tmrInterval.Interval = 150000
        '
        'Service1
        '
        Me.ServiceName = "McoreIndexer"
        CType(Me.tmrInterval, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

#End Region



    Private TASKS_FOLDER As String = System.Windows.Forms.Application.StartupPath
    Dim m_Tree As McoreSystem.TreeLib

    Private CONFIG_FILEPATH As String = System.Windows.Forms.Application.StartupPath + "\TreeGUI.exe.tgc"
    Dim glbDefaultTree As New McoreSystem.TreeLib(CONFIG_FILEPATH)

    Dim chkIndexFileInEachDir As New ArrayList
    Dim chkIndexFileInOneDir As New ArrayList
    Dim txtFileName As New ArrayList
    Dim cboExt As New ArrayList
    Dim chkSingleFile As New ArrayList
    Dim txtOutputDir As New ArrayList
    Dim chkRemoveTreeBranches As ArrayList
    Private currentIndexFilePath As String
    Private targetSingleFilePath As String
    Dim currentBatchFilePath As String
  
    Enum IndexingMode
        INDEX_FILE_IN_EACH_DIR = 0
        INDEX_FILE_IN_ONE_FOLDER = 1
    End Enum

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        tmrInterval.Interval = 5 * 60 * 1000 'glbDefaultTree.getTimeInterval 'BUG: 1.6.3.2 McoreIndexer sometimes used to index folders in short time intervals
        tmrInterval.Enabled = True
    End Sub

    Private Sub Timer1_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrInterval.Elapsed

        Dim strFile As String

        For Each strFile In Directory.GetFiles(Me.TASKS_FOLDER)

            Dim strFileExt As String = Path.GetExtension(strFile)

            If strFileExt.ToLower = ".tgc" Then

                Console.WriteLine("TGC: " & strFile)
                m_Tree = New McoreSystem.TreeLib(strFile)
                '1.3.4.2 Single Index file was not overwritten prior to appending in TreeLib.dll
                chkIndexFileInEachDir = m_Tree.getSettingsFromXmlToArrayList("configuration/style/SameDir")
                chkIndexFileInOneDir = m_Tree.getSettingsFromXmlToArrayList("configuration/style/DiffDir")
                txtFileName = m_Tree.getSettingsFromXmlToArrayList("configuration/style/IndexFileName")
                cboExt = m_Tree.getSettingsFromXmlToArrayList("configuration/style/Extension")
                chkSingleFile = m_Tree.getSettingsFromXmlToArrayList("configuration/style/SingleFile")
                txtOutputDir = m_Tree.getSettingsFromXmlToArrayList("configuration/style/OutputDir")
                chkRemoveTreeBranches = m_Tree.getSettingsFromXmlToArrayList("configuration/style/" + m_Tree.REMOVE_TREE_BRANCHES)
                Call Me.indexNow()

            End If

        Next

        tmrInterval.Interval = glbDefaultTree.getTimeInterval

    End Sub

    Private Sub indexNow()

        Dim lbFolders As New ArrayList
        lbFolders = m_Tree.getSettingsFromXmlToArrayList("/configuration/folders/folder")

        If lbFolders.Count > 0 Then

            If chkIndexFileInOneDir(0) = "True" Then
                'MyLog.WriteEntry("INDEX_FILE_IN_ONE_FOLDER", chkIndexFileInOneDir(0), EventLogEntryType.SuccessAudit)
                Me.indexNow(IndexingMode.INDEX_FILE_IN_ONE_FOLDER)
            End If

            If chkIndexFileInEachDir(0) = "True" Then
                'MyLog.WriteEntry("INDEX_FILE_IN_EACH_DIR", chkIndexFileInEachDir(0), EventLogEntryType.SuccessAudit)
                Me.indexNow(IndexingMode.INDEX_FILE_IN_EACH_DIR)
            End If

            Dim MyLog As New EventLog  ' create a new event log 
            ' Check if the the Event Log Exists 
            If Not MyLog.SourceExists("McoreIndexer") Then
                MyLog.CreateEventSource("McoreIndexer", String.Format("McoreIndexer {0} Log", m_Tree.getConfigFileName)) ' Create Log 
            End If
            MyLog.Source = "McoreIndexer"

            Dim log As String
            For Each item As String In lbFolders
                log += "Indexed " + item + vbCrLf
            Next
            log += vbCrLf
            log += m_Tree.getEventLogText
            MyLog.WriteEntry(String.Format("McoreIndexer {0} Log", m_Tree.getConfigFileName), log, EventLogEntryType.Information)

        End If

    End Sub

    Private Sub indexNow(ByVal mode As IndexingMode)

        Dim lbFolders As ArrayList = New ArrayList
        lbFolders = m_Tree.getSettingsFromXmlToArrayList("/configuration/folders/folder")

        For i As Integer = 0 To lbFolders.Count - 1 ' Didn't notice it wasn't -1 and that's why McoreIndexer failed.

            Me.setCurrentBatchFilePath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\temp" + i.ToString + ".bat")
            Dim CURRENT_DIR As String = lbFolders.Item(i)
            Dim TREE_COMMAND As String = "tree " + m_Tree.getSourceSwitch(CURRENT_DIR) + m_Tree.getAsciiSwitch() + m_Tree.getAddFilesSwitch() + m_Tree.getOutputSwitch(CURRENT_DIR, mode)
            FileOpen(1, Me.getCurrentBatchFilePath, OpenMode.Output)
            PrintLine(1, TREE_COMMAND)

            '1.5.3.4 Didn't tag index files created in the same folder witout appending
            If mode = IndexingMode.INDEX_FILE_IN_EACH_DIR Or i = lbFolders.Count - 1 _
               Or (m_Tree.isChkSingleFile = False And mode = IndexingMode.INDEX_FILE_IN_ONE_FOLDER) Then
                PrintLine(1, m_Tree.getfootertext)
            End If

            PrintLine(1, "DEL " + Chr(34) + Me.getCurrentBatchFilePath + Chr(34))
            FileClose(1)

            Dim proc As New Process
            Dim psi As New ProcessStartInfo(Me.getCurrentBatchFilePath)
            psi.WindowStyle = ProcessWindowStyle.Hidden
            proc.StartInfo = psi
            proc.Start()
            proc.WaitForExit()

            If m_Tree.isChkRemoveTreeBrances = True Then
                m_Tree.removeTreeBranches(m_Tree.getCurrentIndexFilePath)
            End If
        Next

    End Sub

    Private Sub setCurrentBatchFilePath(ByVal filePath As String)
        Me.currentBatchFilePath = filePath
    End Sub

    Private Function getCurrentBatchFilePath() As String
        Return Me.currentBatchFilePath
    End Function

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        tmrInterval.Stop()
    End Sub


End Class
