Public Class MruList

    Private m_ApplicationName As String
    Private m_FileMenu As ToolStripMenuItem
    Private m_NumEntries As Integer
    Private m_FileNames As Collection
    Private m_MenuItems As Collection

    Public Event OpenFile(ByVal file_name As String)

    Public Sub New(ByVal application_name As String, ByVal file_menu As ToolStripMenuItem, ByVal num_entries As Integer)

        m_ApplicationName = application_name
        m_FileMenu = file_menu
        m_NumEntries = num_entries
        m_FileNames = New Collection
        m_MenuItems = New Collection

        ' Load saved file names from the Registry.
        LoadMruList()
        ' Display the MRU list.
        DisplayMruList()
    End Sub

    ' Load previously saved file names from the Registry.
    Private Sub LoadMruList()
        Dim file_name As String
        For i As Integer = 1 To m_NumEntries
            ' Get the next file name and title.
            file_name = GetSetting(m_ApplicationName, _
                "MruList", "FileName" & i, "")

            ' See if we got anything.
            If file_name.Length > 0 Then
                ' Save this file name.
                m_FileNames.Add(file_name, file_name)
            End If
        Next i
    End Sub

    ' Save the MRU list into the Registry.
    Private Sub SaveMruList()
        ' Remove previous entries.
        If GetSetting(m_ApplicationName, "MruList", "FileName1", "").Length > 0 Then
            DeleteSetting(m_ApplicationName, "MruList")
        End If

        ' Make the new entries.
        'Dim reg As New McoreSystem.AppSettings.RegConfig
        'reg.SetRegPath(Application.CompanyName, Application.ProductName)

        For i As Integer = 1 To m_FileNames.Count

            'reg.SaveSettings("FileName" & i, m_FileNames(i).ToString)

            SaveSetting(m_ApplicationName, _
                "MruList", "FileName" & i, _
                m_FileNames(i).ToString)
        Next i
    End Sub

    ' MRU menu item event handler.
    Private Sub MruItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim mnu As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)

        ' Find the menu item that raised this event.
        For i As Integer = 1 To m_FileNames.Count
            ' See if this is the item. (Add 1 for the separator.)
            If m_MenuItems(i) Is mnu Then
                ' This is the item. Raise the OpenFile 
                ' event for its file name.
                RaiseEvent OpenFile(m_FileNames(i).ToString)
                Exit For

            End If
        Next i
    End Sub

    ' Return the file's name without its path.
    Public Function FileTitle(ByVal file_name As String) As String
        If file_name = Nothing Then Return ""
        Dim pos As Integer = file_name.LastIndexOf("\")
        If pos >= 0 Then Return file_name.Substring(pos + 1)
        Return file_name
    End Function

    Private bAddSeperatorOnce As Boolean = False

    ' Display the MRU list.
    Private Sub DisplayMruList()
        ' Remove old menu items from the File menu.
        For Each item As Object In m_MenuItems
            If TypeOf (item) Is ToolStripMenuItem Then
                m_FileMenu.DropDownItems.Remove(CType(item, ToolStripItem))
            End If
        Next item
        m_MenuItems = New Collection

        ' See if we have any file names.
        If m_FileNames.Count > 0 Then
            Dim mnu As New ToolStripMenuItem
            If Not bAddSeperatorOnce Then
                ' Make the separator.
                Dim sep As New ToolStripSeparator
                'm_MenuItems.Add(sep)
                m_FileMenu.DropDownItems.Add(sep)
                bAddSeperatorOnce = True
            End If
            ' Make the other menu items.
            For i As Integer = 1 To m_FileNames.Count
                mnu = New ToolStripMenuItem("&" & i & " " & FileTitle(m_FileNames(i).ToString), Nothing, New System.EventHandler(AddressOf MruItem_Click))
                m_MenuItems.Add(mnu)
                m_FileMenu.DropDownItems.Add(mnu)
            Next i
        End If
    End Sub

    ' Add a file to the MRU list.
    Public Sub Add(ByVal file_name As String)
        ' Remove this file from the MRU list
        ' if it is present.
        Dim i As Integer = FileNameIndex(file_name)
        If i > 0 Then m_FileNames.Remove(i)

        ' Add the item to the begining of the list.
        If m_FileNames.Count > 0 Then
            m_FileNames.Add(file_name, file_name, m_FileNames.Item(1))
        Else
            m_FileNames.Add(file_name, file_name)
        End If

        ' If the list is too long, remove the last item.
        If m_FileNames.Count > m_NumEntries Then
            m_FileNames.Remove(m_NumEntries + 1)
        End If

        ' Display the list.
        DisplayMruList()

        ' Save the updated list.
        SaveMruList()
    End Sub

    ' Return the index of this file in the list.
    Private Function FileNameIndex(ByVal file_name As String) As Integer
        For i As Integer = 1 To m_FileNames.Count
            If m_FileNames(i).ToString = file_name Then Return i
        Next i
        Return 0
    End Function

    ' Remove a file from the MRU list.
    Public Sub Remove(ByVal file_name As String)
        ' See if the file is present.
        Dim i As Integer = FileNameIndex(file_name)
        If i > 0 Then
            ' Remove the file.
            m_FileNames.Remove(i)

            ' Display the list.
            DisplayMruList()

            ' Save the updated list.
            SaveMruList()
        End If
    End Sub

End Class
