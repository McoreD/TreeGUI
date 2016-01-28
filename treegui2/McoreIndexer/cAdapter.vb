Imports System.IO
Imports ICSharpCode.SharpZipLib
Imports System.Windows.Forms 'For MessageBox
Imports TreeGUI.Config

'*******************************
'* Reads .tgc files and returns 
'* a Config object with settings
'*******************************

Public Class cAdapter

    Private Const MSG_INIT_SERVICE As String = "Initialized using McoreIndexer Service"
    Private Const MSG_INIT_TREEGUI As String = "Initialized using TreeGUI"
    Private Const MSG_DATETIME_BASED As String = "Date and Time based Schedule."
    Private Const MSG_MANUAL As String = "Manual Operation"

    Private mConfig As New TreeGUI.Config
    Private mOptions As New TreeGUI.Options
    Private m_Count As Int16 = 0
    Private m_isUpgraded As Boolean = False

    Public ReadOnly OPTIONS_DIR As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\TreeGUI"
    Public ReadOnly OPTIONS_FILE_EXT As String = ".xml"
    Private ReadOnly OPTIONS_FILENAME As String = "\TreeGUI" + OPTIONS_FILE_EXT

    Public ReadOnly LOG_PATH_ONSTART As String = System.Windows.Forms.Application.StartupPath + "\TreeGUI.log"
    Public ReadOnly LOG_PATH_READER As String = System.Windows.Forms.Application.StartupPath + "\TreeGUI.Reader.log"

    Public ReadOnly Property LogPathIndexer() As String
        Get
            Return GetOptions.TasksFolderPath + "\TreeGUI.Indexer.log"
        End Get
    End Property

    Public ReadOnly Property LogPathDebug() As String
        Get
            Return GetOptions.TasksFolderPath + "\TreeGUI.Debug.log"
        End Get
    End Property

    Public Function GetOptionsFilePath() As String

        Dim strStandaloneOptionsPath As String = Application.StartupPath + OPTIONS_FILENAME
        If File.Exists(strStandaloneOptionsPath) Then
            Return strStandaloneOptionsPath
        Else
            IO.Directory.CreateDirectory(OPTIONS_DIR)
            Return OPTIONS_DIR + OPTIONS_FILENAME
        End If

    End Function

    Private mCurrentConfigFilePath As String
    Public Property CurrentConfigFilePath() As String
        Get
            Return mCurrentConfigFilePath
        End Get
        Set(ByVal value As String)
            mCurrentConfigFilePath = value
        End Set
    End Property


    Enum InitializationMode

        INTERVAL_BASED_SERVICE
        INTERVAL_BASED_GUI
        DATETIME_BASED_SERVICE
        DATETIME_BASED_GUI
        MANUAL

    End Enum

    Enum IndexingMode

        IN_EACH_DIRECTORY
        IN_ONE_FOLDER_MERGED
        IN_ONE_FOLDER_SEPERATE

    End Enum

    Enum IndexingEngine

        TreeLib
        TreeNetLib

    End Enum

    Public mInitMode As InitializationMode = InitializationMode.MANUAL

    Public Function fGetIndexFilePath(ByVal folderID As Integer, ByVal mode As IndexingMode) As String

        'Dim strDirPath As String = GetConfig.FolderList.Item(folderID)
        Dim lPath As String = String.Empty ' strDirPath + "\" + GetConfig.GetIndexFileName

        Select Case mode

            Case IndexingMode.IN_ONE_FOLDER_SEPERATE

                Dim strDirPath As String = GetConfig.FolderList.Item(folderID)
                Dim sDrive As String = Path.GetPathRoot(strDirPath).Substring(0, 1)
                Dim sDirName As String = Path.GetFileName(strDirPath)
                Dim sep As String = GetOptions.IndividualIndexFileWordSeperator
                If Directory.Exists(GetConfig.OutputDir) Then
                    lPath = GetConfig.OutputDir + Path.DirectorySeparatorChar + sDirName + sep + sDrive + sep + GetConfig.GetIndexFileName
                End If

            Case IndexingMode.IN_EACH_DIRECTORY

                ' this is also what it is first initialized to
                Dim strDirPath As String = GetConfig.FolderList.Item(folderID)
                lPath = strDirPath + "\" + GetConfig.GetIndexFileName

            Case Else

                lPath = GetConfig.GetIndexFilePath

        End Select

        Return lPath

    End Function

    Public Sub ZipAdminFile(ByVal strFile As String, Optional ByVal filesExtra As List(Of String) = Nothing)

        If File.Exists(strFile) Then

            If GetConfig.ZipAfterIndexed = True Then

                Try

                    Dim zipFilePath As String = Path.ChangeExtension(strFile, ".zip")
                    If File.Exists(zipFilePath) Then File.Delete(zipFilePath)

                    If filesExtra Is Nothing Then
                        filesExtra = New List(Of String)
                    End If
                    If strFile IsNot Nothing Then
                        filesExtra.Add(strFile)
                    End If

                    Dim strmZipOutputStream As Zip.ZipOutputStream
                    strmZipOutputStream = New Zip.ZipOutputStream(File.Create(zipFilePath))

                    If GetConfig.CollapseFolders Then
                        ' minus.gif
                        Dim f1 As String = Application.StartupPath + Path.DirectorySeparatorChar + "plus.gif"
                        If File.Exists(f1) Then filesExtra.Add(f1)
                        Dim f2 As String = Application.StartupPath + Path.DirectorySeparatorChar + "minus.gif"
                        If File.Exists(f2) Then filesExtra.Add(f2)
                    End If

                    If File.Exists(GetConfig.LogoPath) Then
                        filesExtra.Add(GetConfig.LogoPath)
                    End If

                    For Each filePath As String In filesExtra

                        Dim strmFile As FileStream = File.OpenRead(filePath)
                        Dim abyBuffer(CInt(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer, 0, abyBuffer.Length)

                        Dim objZipEntry As Zip.ZipEntry = New Zip.ZipEntry(Path.GetFileName(filePath))
                        objZipEntry.DateTime = DateTime.Now
                        objZipEntry.Size = strmFile.Length
                        strmFile.Close()

                        strmZipOutputStream.PutNextEntry(objZipEntry)
                        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)

                    Next

                    ''''''''''''''''''''''''''''''''''''
                    ' Finally Close strmZipOutputStream
                    ''''''''''''''''''''''''''''''''''''
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                    If GetConfig.ZipAndDeleteFile = True Then
                        My.Computer.FileSystem.DeleteFile(strFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                    End If

                Catch ex As System.UnauthorizedAccessException
                    ' Do Nothing
                End Try

            End If

        End If

    End Sub

    Public Function StartHiddenProcess(ByVal filePath As String, ByVal wait As Boolean) As Process
        Dim proc As New Process
        Dim psi As New ProcessStartInfo(filePath)
        psi.WindowStyle = ProcessWindowStyle.Hidden
        proc.StartInfo = psi
        proc.Start()
        If wait Then proc.WaitForExit()
        Return proc
    End Function

    Public ReadOnly Property IsConfigFileReadOnly(ByVal filePath As String) As Boolean

        Get
            If File.Exists(filePath) Then
                Dim fi As New FileInfo(filePath)
                Return fi.Attributes = FileAttribute.ReadOnly + FileAttribute.Archive
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property Upgraded() As Boolean
        Get
            Return m_isUpgraded
        End Get
    End Property

    Public Function GetConfig() As TreeGUI.Config
        Return mConfig
    End Function

    Public Function GetOptions() As TreeGUI.Options
        Return mOptions
    End Function

    Private Function RenameFile(ByVal filePath As String, ByVal oldFileName As String, ByVal i As Integer) As String

        Try
            'MessageBox.Show(oldFileName + i.ToString)
            My.Computer.FileSystem.RenameFile(filePath, oldFileName + i.ToString)
        Catch err As Exception
            m_Count = CShort(m_Count + 1)
            RenameFile(filePath, oldFileName, m_Count)
        End Try

        Return oldFileName + m_Count.ToString

    End Function

    Public Sub WriteOptionsFile()

        Try
            WriteOptionsFileXML(GetOptionsFilePath)
        Catch ex As System.UnauthorizedAccessException
            If (MessageBox.Show(ex.Message & vbCrLf & "Do you want to browse for the Options file to change Security Settings?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = DialogResult.Yes Then
                Process.Start(IO.Path.GetDirectoryName(GetOptionsFilePath))
            End If
        End Try

    End Sub

    Private Sub WriteOptionsFileBF(ByVal filePath As String)

        Dim fs As New IO.FileStream(filePath, IO.FileMode.Create)
        Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
        bf.Serialize(fs, GetOptions)
        fs.Close()

    End Sub

    Private Sub WriteOptionsFileXML(ByVal filePath As String)

        Using fs As New IO.FileStream(filePath, IO.FileMode.Create)
            Dim xs As New System.Xml.Serialization.XmlSerializer(GetOptions.GetType)
            xs.Serialize(fs, GetOptions)
        End Using

    End Sub

    Public Sub LoadOptionsFile(ByVal filePath As String)

        If LoadOptionsFileXML(filePath) = False Then
            If LoadOptionsFileBF(filePath) = False Then
                mOptions = New Options
            End If
        End If

        ' Since v2.4.0.3
        ' Internal Logic to Determine if Date&Time Indexing is 
        ' Scheduled for Today
        ' 2.4.0.3 IsScheduledForToday was determined from CheckBoxes rather than XML 

        If mOptions.OnMonday And Now.DayOfWeek.ToString = "Monday" Or _
           mOptions.OnTuesday And Now.DayOfWeek.ToString = "Tuesday" Or _
           mOptions.OnWednesday And Now.DayOfWeek.ToString = "Wednesday" Or _
           mOptions.OnThursday And Now.DayOfWeek.ToString = "Thursday" Or _
           mOptions.OnFriday And Now.DayOfWeek.ToString = "Friday" Or _
           mOptions.OnSaturday And Now.DayOfWeek.ToString = "Saturday" Or _
           mOptions.OnSunday And Now.DayOfWeek.ToString = "Sunday" Then
            mOptions.IsScheduledForToday = True
        Else
            mOptions.IsScheduledForToday = False
        End If

    End Sub

    Private Function LoadOptionsFileXML(ByVal filePath As String) As Boolean

        Try
            Using fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)
                Dim xs As New System.Xml.Serialization.XmlSerializer(GetOptions.GetType)
                mOptions = TryCast(xs.Deserialize(fs), TreeGUI.Options)
                fs.Close()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function LoadOptionsFileBF(ByVal filePath As String) As Boolean

        Try
            Dim fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
            mOptions = TryCast(bf.Deserialize(fs), TreeGUI.Options)
            fs.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Sub LoadNewConfig()
        mConfig = New Config
    End Sub

    Public Sub LoadConfigFile(ByVal filePath As String)

        Me.CurrentConfigFilePath = filePath

        If LoadConfigFileXML(filePath) = False Then
            m_isUpgraded = True
            If LoadConfigFileBF(filePath) = False Then
                Dim oldFileName As String = RenameFile(filePath, IO.Path.GetFileName(filePath) + ".bk", 0)
                LoadTgcV1File(IO.Path.GetDirectoryName(filePath) + "\" + oldFileName)
                MessageBox.Show(String.Format("Old {0} Config file has been upgraded to new format and renamed to {1}", Application.ProductName, oldFileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        If mConfig.IndexingEngineID = 0 And mConfig.IndexFileExtension.Contains("html") Then
            mConfig.IndexFileExtension = ".txt"
            'MsgBox("FIXED!")
        End If

    End Sub

    Private Function LoadConfigFileXML(ByVal filePath As String) As Boolean

        If File.Exists(filePath) Then

            Console.WriteLine("Accessed XML TGC Reader...")

            Dim sr As StreamReader
            sr = New StreamReader(filePath)
            Dim xmlText As String = sr.ReadToEnd
            sr.Close()

            If xmlText.Contains("</anyType>") Then

                Dim arrayString As String = "<anyType xsi:type=" + Chr(34) + "xsd:string" + Chr(34) + ">"
                'MsgBox(arrayString)
                xmlText = xmlText.Replace(arrayString, "<string>")
                xmlText = xmlText.Replace("</anyType>", "</string>")
                ' Rename v2.2.04.0 Config File 
                Dim oldFileName As String = RenameFile(filePath, IO.Path.GetFileName(filePath) + ".v2.2.04.0.bk", 0)
                MessageBox.Show(String.Format("Old {0} Config file has been upgraded to a new format and renamed to {1}", Application.ProductName, oldFileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Write new Config File
                Dim sw As New StreamWriter(filePath)
                sw.WriteLine(xmlText)
                sw.Close()

            End If

            sr = New StreamReader(filePath)

            Try
                Dim xs As New System.Xml.Serialization.XmlSerializer(GetConfig.GetType)
                mConfig = DirectCast(xs.Deserialize(sr), TreeGUI.Config)
                Return True
            Catch ex As Exception
                sr.Close()
                Return False
            Finally
                sr.Close()
            End Try

        End If

    End Function

    Private Function LoadConfigFileBF(ByVal filePath As String) As Boolean

        If File.Exists(filePath) Then

            Console.WriteLine("Accessed BF TGC Reader...")

            Dim fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)

            Try
                Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
                mConfig = DirectCast(bf.Deserialize(fs), TreeGUI.Config)
                Return True
            Catch ex As Exception
                fs.Close()
                Return False
            Finally
                fs.Close()
            End Try

        End If

    End Function

    Public Function LoadTgcV1File(ByVal filePath As String) As Boolean

        ' folders
        Try
            mConfig.FolderList = getSettingsFromXmlToArrayList(filePath, "/configuration/folders/folder")
        Catch ex As Exception

        End Try
        ' Extension
        Try
            mConfig.IndexFileExtension = getSettingsFromXmlToArrayList(filePath, "/configuration/style/Extension").Item(0).ToString
        Catch ex As Exception

        End Try
        ' IndexFileName
        Try
            mConfig.IndexFileName = getSettingsFromXmlToArrayList(filePath, "/configuration/style/IndexFileName").Item(0).ToString
        Catch ex As Exception
        End Try

        ' MergeFiles
        Try
            mConfig.isMergeFiles = getSettingsFromXmlToArrayList(filePath, "/configuration/style/SingleFile").Item(0).ToString = "True"
        Catch ex As Exception

        End Try

        ' Output Dir
        Try
            mConfig.OutputDir = getSettingsFromXmlToArrayList(filePath, "/configuration/style/OutputDir").Item(0).ToString
        Catch ex As Exception

        End Try

        ' isIndexFileIn One Dir
        Try
            mConfig.isIndexFileInOneDir = getSettingsFromXmlToArrayList(filePath, "/configuration/style/DiffDir").Item(0).ToString = "True"
        Catch ex As Exception

        End Try

        ' isIndexFileInSameDir
        Try
            mConfig.isIndexFileInSameDir = getSettingsFromXmlToArrayList(filePath, "/configuration/style/SameDir").Item(0).ToString = "True"
        Catch ex As Exception

        End Try

        ' Add Files
        Try
            mConfig.isAddFiles = getSettingsFromXmlToArrayList(filePath, "/configuration/style/IndexFiles").Item(0).ToString = "True"
        Catch ex As Exception

        End Try

        ' isRemoveTreeBranches
        Try
            mConfig.isRemoveTreeBranches = getSettingsFromXmlToArrayList(filePath, "/configuration/style/RemoveTreeBranches").Item(0).ToString = "True"
        Catch ex As Exception

        End Try

        ' isAscii
        Try
            mConfig.isAscii = getSettingsFromXmlToArrayList(filePath, "/configuration/style/Ascii").Item(0).ToString = "True"
        Catch ex As Exception

        End Try

        Try
            mOptions.IndexingInterval = CInt(getSettingsFromXmlToArrayList(filePath, "configuration/options/Interval").Item(0))
        Catch ex As Exception

        End Try

        Try
            mOptions.RunTasksInGUI = CDbl(getSettingsFromXmlToArrayList(filePath, "configuration/options/RunSchedule").Item(0)) = 1
        Catch ex As Exception

        End Try
        Try
            mOptions.MinimizeToTray = CDbl(getSettingsFromXmlToArrayList(filePath, "configuration/options/ShowInTaskbar").Item(0)) = 0
        Catch ex As Exception

        End Try

        Try
            mOptions.TrayIconIsEnabled = CDbl(getSettingsFromXmlToArrayList(filePath, "configuration/options/TrayIconIsEnabled").Item(0)) = 1
        Catch ex As Exception

        End Try
        Try
            mOptions.CloseToTray = CDbl(getSettingsFromXmlToArrayList(filePath, "configuration/options/CloseToTray").Item(0)) = 1
        Catch ex As Exception

        End Try
        Try
            mOptions.TrayOnLoad = CDbl(getSettingsFromXmlToArrayList(filePath, "configuration/options/TrayOnlyWhenLoad").Item(0)) = 1
        Catch ex As Exception

        End Try


    End Function

    Private Function getSettingsFromXmlToArrayList(ByVal filePath As String, _
                                                    ByVal xpath As String) As List(Of String)

        Dim array As New List(Of String)

        If File.Exists(filePath) Then

            Dim configFile As New Xml.XmlDocument
            configFile.Load(filePath)
            Dim Node_list As Xml.XmlNodeList = configFile.SelectNodes(xpath)
            For Each nd As Xml.XmlNode In Node_list
                Debug.WriteLine(nd.Attributes("value").Value)
                array.Add(nd.Attributes("value").Value.ToString)
            Next
        End If

        Return array

    End Function

    Public Function getFooterText(ByVal myCurrentIndexFilePath As String, ByVal myEngine As IndexingEngine, Optional ByVal html As Boolean = False) As String

        Dim appName As String = String.Format("{0} v{1}", Application.ProductName, Application.ProductVersion)
        Dim appUrl As String = "http://sourceforge.net/project/showfiles.php?group_id=156257"

        'v2.0.31.0
        Dim strDateTime As String = Date.UtcNow.ToString("yyyy-MM-dd 'at' HH:mm:ss 'UTC'")
        If Not mOptions.IndexedTimeInUTC Then
            strDateTime = Now.ToString("yyyy-MM-dd 'at ' HH:mm:ss 'local time'")
        End If

        Dim lineBreak As String = Environment.NewLine

        If html Then
            appUrl = "<a href=" + Chr(34) + appUrl + Chr(34) + ">SourceForge</a>."
            lineBreak = "<br />"
        End If
        Dim footer As String = String.Format("Generated on {0} using {1}.{3}Latest version of the Indexer can be downloaded from {2}", strDateTime, appName, appUrl, lineBreak)

        Select Case myEngine
            Case IndexingEngine.TreeLib
                Return "ECHO " + footer + " >> " + Chr(34) + myCurrentIndexFilePath + Chr(34)
            Case Else
                Return footer
        End Select
    End Function

    Public Function getBlankLine(ByVal myCurrentIndexFilePath As String) As String
        Return String.Format("ECHO ____") + " >> " + Chr(34) + myCurrentIndexFilePath + Chr(34)
    End Function

    Public Function getNextScheduledRunTime() As String
        Return String.Format("Next Scheduled Run Time: {0}", Now.AddMinutes(mOptions.IndexingInterval).ToString("yyyy-MM-dd 'at' HH:mm:ss"))
    End Function

    Public Function getIntervalInMilliseconds() As Double
        Return Me.GetOptions.IndexingInterval * 60 * 1000
    End Function

    Public Sub WriteEventLog(ByVal folderList As List(Of String), ByVal myInitMode As InitializationMode)

        Try
            Dim MyLog As New EventLog(Application.ProductName)  ' create a new event log 
            ' Check if the the Event Log Exists 
            If Not EventLog.SourceExists("TreeGUI") Then
                EventLog.CreateEventSource("TreeGUI", "TreeGUI Log") ' Create Log 
            End If
            MyLog.Source = "TreeGUI"

            Dim log As String = Nothing
            For i As Integer = 0 To folderList.Count - 1
                log += "Indexed " & folderList.Item(i) + vbCrLf
            Next

            Select Case myInitMode
                Case InitializationMode.MANUAL
                    log += vbCrLf + MSG_MANUAL
                Case InitializationMode.INTERVAL_BASED_SERVICE
                    log += vbCrLf + getNextScheduledRunTime()
                    log += vbCrLf + MSG_INIT_SERVICE
                Case InitializationMode.INTERVAL_BASED_GUI
                    log += vbCrLf + getNextScheduledRunTime()
                    log += vbCrLf + MSG_INIT_TREEGUI
                Case InitializationMode.DATETIME_BASED_SERVICE
                    log += vbCrLf + MSG_DATETIME_BASED
                    log += vbCrLf + MSG_INIT_SERVICE
                Case InitializationMode.DATETIME_BASED_GUI
                    log += vbCrLf + MSG_DATETIME_BASED
                    log += vbCrLf + MSG_INIT_TREEGUI
            End Select

            ' BUG: 2.2.3.3 Writing Event Log while logged on as Limited caused program crash
            EventLog.WriteEntry("TreeGUI Log", log, EventLogEntryType.Information)

        Catch ex As Exception

        End Try

    End Sub


End Class
