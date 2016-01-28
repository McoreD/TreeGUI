Imports System.IO

Public Class cOptions

    ' Creating a New Instances of Options will 
    ' automatically create a new instance of Config
    Private m_Config As New cConfig
    Private m_FilePath As String

    Public Sub New(ByVal filePath As String)
        Me.m_FilePath = filePath
    End Sub
    Public Function getConfig() As cConfig
        Return Me.m_Config
    End Function

    Public Sub Save()
        Me.Save(m_FilePath)
    End Sub

    Public Sub Save(ByVal filePath As String)

        '*********************************
        '* Saving Gui Controls to m_Config
        '*********************************
        '* Folder List
        'Clear the Collection otherwise List keeps growing
        m_Config.FolderList.Clear()
        For Each item As String In frmForm1.lbFolders.Items
            m_Config.FolderList.Add(item)
        Next
        '* Jobs Folder Path 
        m_Config.JobsFolderPath = frmOptions.BackDoor.txtJobsFolderPath.Text

        Dim fs As New IO.FileStream(filePath, IO.FileMode.Create)
        Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
        bf.Serialize(fs, m_Config)
        fs.Close()

    End Sub

    Public Function Load() As Boolean
        Me.Load(m_FilePath)
    End Function

    Public Function Load(ByVal filePath As String) As Boolean

        Dim fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)

        Try
            Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
            m_Config = DirectCast(bf.Deserialize(fs), cConfig)
            Return True
        Catch ex As Exception
            fs.Close()
            Dim oldFileName As String = IO.Path.GetFileName(filePath) + ".old"
            My.Computer.FileSystem.RenameFile(filePath, oldFileName)
            Me.LoadXml(IO.Path.GetDirectoryName(filePath) + "\" + oldFileName)
            MessageBox.Show(String.Format("Old {0} Config File has been upgraded to new format and renamed to {1}.", Application.ProductName, oldFileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            '* Close File
            fs.Close()
            '*********************************
            '* Loading Options to Gui Controls
            '*********************************
            For Each item As String In m_Config.FolderList
                frmForm1.lbFolders.Items.Add(item, True)
            Next
            '* Jobs Folder
            frmOptions.BackDoor.txtJobsFolderPath.Text = m_Config.JobsFolderPath

        End Try

    End Function

    Public Function LoadXml(ByVal filePath As String) As Boolean

        Const RUN_SCHEDULE As String = "RunSchedule"

        Try
            m_Config.FolderList = getSettingsFromXmlToArrayList(filePath, "/configuration/folders/folder")
            m_Config.RunSchedule = getSettingsFromXmlToArrayList(filePath, "configuration/options/" + RUN_SCHEDULE).Count > 0

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function getSettingsFromXmlToArrayList(ByVal filePath As String, _
                                                    ByVal xpath As String) As ArrayList

        Dim array As New ArrayList

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

End Class
