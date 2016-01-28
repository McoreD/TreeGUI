Imports system.IO

Public Class cFilter

    Private mSettings As cAdapter
    Private mBannedFilter As String()

    Public Sub New(ByVal settings As cAdapter)

        mSettings = settings
        mBannedFilter = mSettings.GetConfig.IgnoreFilesList.Split(CChar(mSettings.GetOptions.IgnoreFilesListDelimiter))

    End Sub

    Public Function GetFilesCollFiltered(ByVal dir As cDir) As List(Of cFile)

        Dim temp As New List(Of cFile)

        For Each f As cFile In dir.GetFilesColl()
            If IsBannedFile(f.GetFilePath) = False Then
                temp.Add(f)
            End If
        Next

        Return temp

    End Function

    Public Function isBannedFolder(ByVal dir As cDir) As Boolean

        ' Check if Option set to Enable Filtering
        If mSettings.GetConfig.EnabledFiltering Then

            Dim di As New IO.DirectoryInfo(dir.DirectoryPath)

            Dim c As String() = dir.DirectoryPath.Split(CChar("\"))
            ' If Options says to filter protected OS folders 
            If mSettings.GetConfig.HideProtectedOperatingSystemFilesFolders Then
                'MsgBox(di.FullName + " is " + di.Attributes.ToString)
                Return (c(1).Length <> 0 And di.Attributes = FileAttributes.Hidden + FileAttributes.System + FileAttributes.Directory) And mSettings.GetConfig.HideProtectedOperatingSystemFilesFolders
            End If

            ' If Config says to filter Hidden Folders
            If mSettings.GetConfig.IgnoreHiddenFolders Then
                If di.Attributes = FileAttributes.Directory + IO.FileAttributes.Hidden Then
                    'MsgBox("Hidden Folder Check: " + di.FullName + " is " + di.Attributes.ToString)
                    Return True
                End If
            End If

            ' If Config says to filter System Folders 
            If mSettings.GetConfig.IgnoreSystemFolders Then
                If di.Attributes = FileAttributes.Directory + IO.FileAttributes.System Then
                    'MsgBox("System Folder Check: " + di.FullName + " is " + di.Attributes.ToString)
                    Return True
                End If
            End If

            'war59312 If Config says to filter Empty Folders 
            If mSettings.GetConfig.IgnoreEmptyFolders AndAlso dir.DirectorySize() = 0.0 Then
                Return True
            End If

        End If

        Return False

    End Function

    Public Function IsBannedFile(ByVal filePath As String) As Boolean

        ' Check if Option set to Enable Filtering
        If mSettings.GetConfig.EnabledFiltering Then

            ' Establish an FileInfo, we need for the checks below
            Dim fi As New IO.FileInfo(filePath)

            ' If Options says to filter protected OS files 
            If mSettings.GetConfig.HideProtectedOperatingSystemFilesFolders Then
                If (fi.Attributes = IO.FileAttributes.Archive + IO.FileAttributes.Hidden + IO.FileAttributes.System) Or _
                (fi.Attributes = IO.FileAttributes.ReadOnly + IO.FileAttributes.Hidden + IO.FileAttributes.System) Then
                    'Console.WriteLine("HideProtectedOperatingSystemFilesFolders Check: HS when " + fi.FullName + " is " + fi.Attributes.ToString)
                    Return True
                End If
            End If

            ' If Config says to filter Hidden Files
            If mSettings.GetConfig.IgnoreHiddenFiles Then
                If fi.Attributes = IO.FileAttributes.Archive + IO.FileAttributes.Hidden Then
                    Return True
                End If
            End If

            ' If Config says to filter System Files 
            If mSettings.GetConfig.IgnoreSystemFiles Then
                If fi.Attributes = IO.FileAttributes.Archive + IO.FileAttributes.System Then
                    Return True
                End If
            End If

            ' If Config says to filter following files 
            If mSettings.GetConfig.IgnoreFollowingFiles Then
                For Each item As String In mBannedFilter
                    If IO.Path.GetFileName(filePath).ToLower = item.ToLower Then
                        Return True
                    End If
                    If item.IndexOf("*.") <> -1 AndAlso item.IndexOf(IO.Path.GetExtension(filePath)) <> -1 Then
                        Return True
                    End If
                Next
            End If

        End If

        Return False

    End Function

End Class
