Imports System.Windows.Forms

Public Class cEngine

    Private mProgress As Integer = 0
    Private mCurrentDirMsg As String = "Analyzing..."

    Private mSettings As New cAdapter

    Public Sub New(ByVal settings As cAdapter)

        mSettings = settings

    End Sub

    Public Function GetSettings() As cAdapter
        Return mSettings
    End Function

    Public Property Progress() As Integer
        Get
            Return mProgress
        End Get
        Set(ByVal value As Integer)
            mProgress = value
        End Set
    End Property

    Public Property CurrentDirMessage() As String
        Get
            Return mCurrentDirMsg
        End Get
        Protected Set(ByVal value As String)
            mCurrentDirMsg = value
        End Set
    End Property


    Public Overridable Sub IndexNow(ByVal mIndexMode As cAdapter.IndexingMode)

        ' Does not seem to reach here 

        Dim fixedFolderList As New List(Of String)
        Dim dneFolderList As New List(Of String)

        For Each dirPath As String In mSettings.GetConfig.FolderList
            If IO.Directory.Exists(dirPath) Then
                fixedFolderList.Add(dirPath)
            Else
                dneFolderList.Add(dirPath)
            End If
        Next

        If dneFolderList.Count > 0 Then
            Dim sb As New System.Text.StringBuilder
            For Each dp As String In dneFolderList
                sb.AppendLine(dp)
            Next
            System.Windows.Forms.MessageBox.Show("Following Index folders do not exist:" + Environment.NewLine + Environment.NewLine + sb.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        mSettings.GetConfig.FolderList = fixedFolderList

        ' Overrides by Sub Classes

    End Sub

End Class
