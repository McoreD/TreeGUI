Imports System.IO

Public Class cFolder

    Private m_FolderSize As Single

    Public Function GetFolderSize() As Single
        Return m_FolderSize
    End Function

    Public Function GetFilePaths(ByVal folderPath As String) As ArrayList

        Dim pathsArray As New ArrayList
        For Each path As String In Directory.GetFiles(folderPath)
            pathsArray.Add(path)
        Next
        Return pathsArray

    End Function


    Public Function GetDirectoryPaths(ByVal folderPath As String) As ArrayList

        Dim pathsArray As New ArrayList
        For Each path As String In Directory.GetDirectories(folderPath)
            pathsArray.Add(path)
        Next
        Return pathsArray

    End Function

End Class
