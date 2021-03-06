Imports System.IO
Imports System.Windows.Forms

Public Class cTreeNetLib
    Inherits cEngine

    Private mSettings As cAdapter
    Private mFilter As cFilter
    Private mBooFirstDir As Boolean = True

    ' 2.7.1.1 TreeGUI failed to index when Decimal Symbol set to a charactor other than dot
    Dim mDecimalSymbol As Char = CChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)


    Public Sub New(ByVal settings As cAdapter)
        MyBase.New(settings)
        mSettings = New cAdapter
        mSettings = settings
        mFilter = New cFilter(mSettings)
    End Sub

    Private Function Analyze(ByVal rootDirPath As String) As cDir

       
        Dim dirRoot As cDir = New cDir(rootDirPath)
        dirRoot = GetFiles(dirRoot.DirectoryPath)
        If mSettings.GetConfig.mSortBySize Then
            dirRoot.GetSubDirColl.Sort(New cDirComparer)
            If 1 = mSettings.GetConfig.mSortBySizeMode Then
                dirRoot.GetSubDirColl.Reverse()
            End If
        End If
        
        Return dirRoot

    End Function

    Private Function GetFiles(ByVal dirPath As String) As cDir

        Dim dir As New cDir(dirPath)

        Try
            For Each filepath As String In Directory.GetFiles(dirPath)
                dir.SetFile(filepath, cDir.BinaryPrefix.Kibibytes)
            Next
        Catch ex As System.UnauthorizedAccessException

        End Try

        Try
            For Each subDirPath As String In Directory.GetDirectories(dirPath)

                Dim subdir As New cDir(subDirPath)
                subdir = GetFiles(subDirPath)
                dir.AddDir(subdir)
            Next
        Catch ex As System.UnauthorizedAccessException

        End Try

        Return dir

    End Function


    Private Function fGetDirPath(ByVal dir As cDir) As String
        Dim dirName As String
        Dim c As String() = dir.DirectoryPath.Split(Path.DirectorySeparatorChar)
        If c(1).Length = 0 Then
            ' Root Drive
            dirName = dir.DirectoryPath
        Else
            If mSettings.GetConfig.ShowFolderPath Then
                dirName = dir.DirectoryPath
            Else
                dirName = dir.DirectoryName
            End If

        End If
        Return dirName
    End Function

    Private Function fGetDirSizeString(ByVal dir As cDir) As String

        Dim dirSize As String

        If mBooFirstDir Then
            dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Gibibytes)
            If CDbl(dirSize.Split(mDecimalSymbol)(0)) = 0 Then
                dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Mebibytes)
                If CDbl(dirSize.Split(CChar(" "))(0)) = 0 Then
                    dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Kibibytes)
                End If
            End If
        Else
            dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Mebibytes)
            If CDbl(dirSize.Split(mDecimalSymbol)(0)) = 0 Then
                dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Kibibytes)
            End If
        End If

        Return dirSize

    End Function

    Private Function fGetVirtualDirName(ByVal filePath As String) As String
        For Each line As String In mSettings.GetConfig.VirtualFolderList
            Dim spline As String() = line.Split(CChar("|"))
            If filePath.IndexOf(spline(0)) <> -1 Then
                Return filePath.Replace(spline(0), spline(1))
            End If
        Next
        Return filePath
    End Function

    Private Function fDivWrap(ByVal dir As cDir) As Boolean

        Dim y As Boolean
        y = (rootDir <> dir.DirectoryPath) AndAlso (dir.GetSubDirColl.Count > 0 Or mSettings.GetConfig.ShowFileCount)

        Return y

    End Function

    '' DOMCOLLAPSE RULES
    '' If a folder has subfolders then wrap the folder with div
    '' If a folder has no files then don't have trigger
    'TODO war59312 - Hide folders from output in which all its files are ignored
    Dim rootDir As String = String.Empty
    Private Function IndexToHtmlFile(ByVal dir As cDir, ByVal where As StreamWriter) As cDir

        Dim html As New cHtml

        Dim isNotIndexableDir As Boolean = mFilter.isBannedFolder(dir) ' (c(1).Length <> 0 And di.Attributes = FileAttributes.Hidden + FileAttributes.System + FileAttributes.Directory) And mSettings.GetOptions.mIgnoreHiddenFilesFolders
        'System.Windows.Forms.MessageBox.Show(isNotIndexableDir)

        Dim dirName As String = fGetDirPath(dir)
        Dim dirSize As String = CStr(fGetDirSizeString(dir))

        Dim dirTitle As String

        If mSettings.GetConfig.EnabledFiltering AndAlso mSettings.GetConfig.IgnoreEmptyFolders AndAlso dir.DirectorySize() = 0.0 Then 'war59312 - dont show empty folders

            dirTitle = ""

        Else

            If mSettings.GetConfig.ShowDirSize Then
                dirTitle = html.GetValidXhtmlLine(String.Format("{0} [{1}]", dirName, dirSize))
            Else
                dirTitle = html.GetValidXhtmlLine(dirName)
            End If

        End If

        If mBooFirstDir Then
            rootDir = dir.DirectoryPath
            where.WriteLine("<h1>" + dirTitle + "</h1>")
            mBooFirstDir = False
            mNumTabs = 1
        Else

            If Not isNotIndexableDir Then

                If mSettings.GetConfig.EnabledFiltering AndAlso mSettings.GetConfig.IgnoreEmptyFolders AndAlso dir.DirectorySize() = 0.0 Then 'war59312 - dont show empty folders

                Else

                    If mSettings.GetConfig.ShowFolderPathOnStatusBar Then
                        Dim hyperlinkDir As String = Nothing
                        If mSettings.GetConfig.ShowVirtualFolders Then
                            ' Virtual Folders
                            hyperlinkDir = mSettings.GetConfig.ServerInfo + "/" + fGetVirtualDirName(dir.DirectoryPath).Replace("\", "/")
                        Else
                            ' Locally Browse
                            hyperlinkDir = "file://" + dir.DirectoryPath
                        End If
                        hyperlinkDir = "<a href=" + Chr(34) + hyperlinkDir + Chr(34) + ">" + dirTitle + "</a>"
                        where.WriteLine(GetHeadingOpen(dir) + hyperlinkDir + GetHeadingClose())
                    Else
                        where.WriteLine(GetHeadingOpen(dir) + dirTitle + GetHeadingClose())
                    End If

                End If

            End If

        End If

        If Not isNotIndexableDir Then

            If mSettings.GetConfig.EnabledFiltering AndAlso mSettings.GetConfig.IgnoreEmptyFolders AndAlso dir.DirectorySize() = 0.0 Then 'war59312 - dont show empty folders

            Else

                If fDivWrap(dir) Then
                    where.WriteLine(html.OpenDiv)
                End If
                'If (rootDir <> dir.GetDirPath) AndAlso dir.GetSubDirColl.Count > 0 Then
                '    ' dir has sub dirs

                'End If

                If CDbl(dir.DirectorySizeToString(cDir.BinaryPrefix.Kibibytes).Split(CChar(" "))(0)) > 0 Or dir.GetFilesColl.Count > 0 Then

                    If mSettings.GetConfig.ShowFileCount Then
                        Dim fc As Integer = mFilter.GetFilesCollFiltered(dir).Count
                        If fc > 0 Then
                            where.WriteLine(html.OpenPara("foldercount"))
                            where.WriteLine("Files Count: " + fc.ToString)
                            where.WriteLine(html.ClosePara)
                        End If

                    End If

                Else

                    'Note: 
                    ' dir.GetFilesColl.Count = 0 DOESNT ALWAYS MEAN THAT 
                    ' it is an empty directory because there can be subfolders 
                    ' with files
                    'System.Windows.Forms.MessageBox.Show(dir.GetFilesColl.Count)
                    where.WriteLine(html.OpenPara)
                    where.WriteLine(mSettings.GetOptions.EmptyFolderMessage + html.AddBreak)
                    where.WriteLine(html.ClosePara)

                End If

                If dir.GetFilesColl.Count > 0 Then

                    ' Check if there is AT LEAST ONE valid file
                    Dim booPrintList As Boolean = False
                    For Each fp As cFile In dir.GetFilesColl
                        If Not mFilter.IsBannedFile(fp.GetFilePath) Then
                            booPrintList = True
                            Exit For
                        End If
                    Next

                    If mSettings.GetConfig.ShowFilesTreeNet Then

                        If booPrintList Then
                            Select Case mSettings.GetConfig.ListType
                                Case Config.eListType.Bullets
                                    where.WriteLine(html.OpenBulletedList())
                                Case Config.eListType.Numbered
                                    where.WriteLine(html.OpenNumberedList())
                            End Select
                        End If

                        If mSettings.GetConfig.RevereFileOrder Then
                            dir.GetFilesColl.Reverse()
                        End If

                        For Each f As cFile In dir.GetFilesColl

                            Dim lLine As String = Nothing

                            If Not mFilter.IsBannedFile(f.GetFilePath) Then
                                Dim strFilePath As String

                                If mSettings.GetConfig.ShowFilePath Then
                                    If mSettings.GetConfig.ShowVirtualFolders Then
                                        strFilePath = fGetVirtualDirName(f.GetFilePath)
                                    Else
                                        strFilePath = f.GetFilePath
                                    End If

                                Else
                                    If mSettings.GetConfig.HideExtension Then
                                        strFilePath = f.GetFileNameWithoutExtension
                                    Else
                                        strFilePath = f.GetFileName
                                    End If
                                End If

                                If mSettings.GetConfig.ShowFileSize Then
                                    Dim fileSize As String = f.GetSizeToString(cDir.BinaryPrefix.Mebibytes)
                                    If CDbl(fileSize.Split(mDecimalSymbol)(0)) = 0 Then
                                        fileSize = f.GetSizeToString(cDir.BinaryPrefix.Kibibytes)
                                    End If
                                    lLine = html.GetValidXhtmlLine(strFilePath) + " " + html.GetSpan(String.Format(" [{0}]", fileSize), "filesize")
                                Else
                                    lLine = html.GetValidXhtmlLine(strFilePath)
                                End If


                                If mSettings.GetConfig.AudioInfo AndAlso fIsAudio(f.GetFileExtension.ToLower) = True Then

                                    Try
                                        Dim audioFile As TagLib.File = TagLib.File.Create(f.GetFilePath)
                                        Dim fsize As Double = f.GetSize(cDir.BinaryPrefix.Kibibits)
                                        Dim dura As Double = audioFile.Properties.Duration.TotalSeconds

                                        If dura > 0 Then
                                            Console.WriteLine(fsize / dura)
                                            lLine += html.GetSpan(String.Format(" [{0} Kibit/s]", (fsize / dura).ToString("0.00"), fGetHMS(audioFile.Properties.Duration.TotalSeconds)), "audioinfo")
                                            lLine += html.GetSpan(String.Format(" [{0}]", fGetHMS(audioFile.Properties.Duration.TotalSeconds)), "audiolength")
                                        End If

                                    Catch ex As Exception
                                        Console.WriteLine(f.GetFilePath)
                                    End Try

                                End If

                                where.WriteLine("<li>" + lLine + "</li>")

                            End If

                        Next

                        If booPrintList Then
                            Select Case mSettings.GetConfig.ListType
                                Case Config.eListType.Bullets
                                    where.WriteLine(html.CloseBulletedList())
                                Case Config.eListType.Numbered
                                    where.WriteLine(html.CloseNumberedList())
                            End Select
                        End If

                    End If ' Show Files for TreeNet

                End If

                mNumTabs += 1

                For Each d As cDir In dir.GetSubDirColl

                    Dim sd As New cDir(d.DirectoryPath)
                    sd = IndexToHtmlFile(d, where)

                Next

                If fDivWrap(dir) Then
                    where.WriteLine(html.CloseDiv)
                End If
                'If (rootDir <> dir.GetDirPath) AndAlso dir.GetSubDirColl.Count > 0 Then
                '    ' dir has sub dirs

                'End If

                mNumTabs -= 1

            End If

        End If
        Return dir

    End Function

    Public Function fIsAudio(ByVal ext As String) As Boolean

        Dim exts() As String = {".mp3", ".m4a", ".flac", ".wma"}

        For Each ex As String In exts
            If ext.Equals(ex.ToLower) Then
                Return True
            End If
        Next

        Return False

    End Function

    Public Function fGetHMS(ByVal sec As Double) As String

        Dim hms() As Double = fGetDurationInHoursMS(sec)
        Return String.Format("{0}:{1}:{2}", hms(0).ToString("00"), hms(1).ToString("00"), hms(2).ToString("00"))

    End Function

    Public Function fGetHMS2(ByVal sec As Double) As String

        Dim hms() As Double = fGetDurationInHoursMS(sec)
        Return String.Format("{0} Hours {1} Minutes {2} Seconds", hms(0), hms(1), hms(2))

    End Function

    Public Function fGetDurationInHoursMS(ByVal seconds As Double) As Double()

        Dim arrayHoursMinutesSeconds(3) As Double
        Dim SecondsLeft As Double = seconds
        Dim hours As Integer = 0
        Dim minutes As Integer = 0

        While SecondsLeft >= 3600
            SecondsLeft -= 3600
            hours += 1
        End While

        arrayHoursMinutesSeconds(0) = hours

        While SecondsLeft >= 60
            SecondsLeft -= 60
            minutes += 1
        End While

        arrayHoursMinutesSeconds(1) = minutes
        arrayHoursMinutesSeconds(2) = SecondsLeft

        Return arrayHoursMinutesSeconds

    End Function

    Private mBooFirstIndexFile As Boolean = True
    Private mBooMoreFilesToCome As Boolean = False

    Private Sub IndexRootFolderToHtml(ByVal folderPath As String, ByVal sw As StreamWriter, ByVal AddFooter As Boolean)

        Dim html As New cHtml

        If mBooFirstIndexFile Then

            sw.WriteLine(html.GetDocType)
            If mSettings.GetConfig.CollapseFolders Then
                sw.WriteLine(html.GetCollapseJs)
                sw.WriteLine(html.GetCollapseCss)
            End If
            sw.WriteLine(html.GetCssStyle(mSettings.GetConfig.CssFilePath))

            If mBooMoreFilesToCome Then
                sw.WriteLine(html.GetTitle(mSettings.GetConfig.MergedHtmlTitle))
                mBooMoreFilesToCome = False
            Else
                Dim c As String() = folderPath.Split(Path.DirectorySeparatorChar)
                If c(1).Length = 0 Then
                    sw.WriteLine(html.GetTitle("Index for " + folderPath))
                Else
                    sw.WriteLine(html.GetTitle("Index for " + IO.Path.GetFileName(folderPath)))
                End If

            End If

            sw.WriteLine(html.CloseHead)
            sw.WriteLine(html.OpenBody())
            mBooFirstIndexFile = False

        End If

        Dim rootDir As New cDir(folderPath)

        rootDir = Analyze(rootDir.DirectoryPath)

        Me.IndexToHtmlFile(rootDir, sw)

        If AddFooter Then
            sw.WriteLine(html.OpenDiv)
            sw.WriteLine("____" + html.AddBreak)
            sw.WriteLine(mSettings.getFooterText(Nothing, cAdapter.IndexingEngine.TreeNetLib, True))
            sw.WriteLine(html.CloseDiv)
            sw.WriteLine(html.CloseBody())
        End If

        mBooFirstDir = True

    End Sub

    Private Sub IndexFolderToTxt(ByVal folderPath As String, ByVal sw As StreamWriter, ByVal AddFooter As Boolean)

        If Directory.Exists(folderPath) Then

            ' 2.7.1.6 TreeGUI crashed on Could not find a part of the path 

            Dim dir As New cDir(folderPath)
            dir = Analyze(dir.DirectoryPath)
            Me.IndexToTxtFile(dir, sw)
            If AddFooter Then
                sw.WriteLine("____")
                sw.WriteLine(mSettings.getFooterText(Nothing, cAdapter.IndexingEngine.TreeNetLib))
            End If

        End If

    End Sub

    Private Function IndexToTxtFile(ByVal dir As cDir, ByVal where As StreamWriter) As cDir

        Dim dirSize As String = fGetDirSizeString(dir)

        Dim dirTitle As String = String.Format("{0} [{1}]", dir.DirectoryName, dirSize)

        Dim strStars As String = ""
        Dim styleArray() As Char = mSettings.GetConfig.FolderHeadingStyle.ToCharArray

        For i As Integer = 0 To dirTitle.Length - 1
            strStars += styleArray(i Mod styleArray.Length)
        Next

        'For Each chr As Char In dirTitle
        '    strStars += mSettings.GetConfig.FolderHeadingStyle
        'Next

        where.WriteLine("")
        where.WriteLine(GetTabs() + strStars)
        where.WriteLine(GetTabs() + dirTitle)
        where.WriteLine(GetTabs() + strStars)

        If CDbl(dir.DirectorySizeToString(cDir.BinaryPrefix.Kibibytes).Split(CChar(" "))(0)) = 0 Then
            where.WriteLine(GetTabs() + mSettings.GetOptions.EmptyFolderMessage)
        End If

        If mSettings.GetConfig.ShowFilesTreeNet Then

            For Each fp As cFile In dir.GetFilesColl

                Dim fileDesc As String = GetTabs() + "  "

                If mSettings.GetConfig.ShowFileSize Then
                    Dim fileSize As String = fp.GetSizeToString(cDir.BinaryPrefix.Mebibytes)
                    If CDbl(fileSize.Split(mDecimalSymbol)(0)) = 0 Then
                        fileSize = fp.GetSizeToString(cDir.BinaryPrefix.Kibibytes)
                    End If
                    fileDesc += String.Format("{0} [{1}]", fp.GetFileName, fileSize)
                Else
                    fileDesc += fp.GetFileName
                End If

                If mSettings.GetConfig.AudioInfo AndAlso fIsAudio(fp.GetFileExtension.ToLower) = True Then

                    Try
                        Dim audioFile As TagLib.File = TagLib.File.Create(fp.GetFilePath)
                        Dim fsize As Double = fp.GetSize(cDir.BinaryPrefix.Kibibits)
                        Dim dura As Double = audioFile.Properties.Duration.TotalSeconds



                        If dura > 0 Then
                            Console.WriteLine(fsize / dura)
                            fileDesc += String.Format(" [{0} Kibit/s]", (fsize / dura).ToString("0.00"), fGetHMS(audioFile.Properties.Duration.TotalSeconds))
                            fileDesc += String.Format(" [{0}]", fGetHMS(audioFile.Properties.Duration.TotalSeconds))
                        End If

                    Catch ex As Exception
                        Console.WriteLine(fp.GetFilePath)
                    End Try

                End If


                where.WriteLine(fileDesc)

            Next

        End If

        mNumTabs += 1

        For Each d As cDir In dir.GetSubDirColl

            Dim sd As New cDir(d.DirectoryPath)
            sd = IndexToTxtFile(d, where)
        Next
        mNumTabs -= 1

        Return dir

    End Function

    Dim mNumTabs As Integer = 0

    Public Overrides Sub IndexNow(ByVal IndexMode As cAdapter.IndexingMode)

        Dim where As String

        Dim folderList As New List(Of String)
        folderList = mSettings.GetConfig.FolderList
        Dim treeNetLib As New cTreeNetLib(mSettings)

        Dim ext As String = mSettings.GetConfig.IndexFileExtension

        Select Case IndexMode

            Case cAdapter.IndexingMode.IN_EACH_DIRECTORY

                IndexInEachDir(mSettings)

            Case cAdapter.IndexingMode.IN_ONE_FOLDER_MERGED

                If mSettings.GetConfig.isMergeFiles Then

                    'where = mSettings.GetConfig.GetIndexFilePath
                    where = mSettings.fGetIndexFilePath(-1, cAdapter.IndexingMode.IN_ONE_FOLDER_MERGED)
                    'MsgBox(mSettings.GetConfig.GetIndexFilePath = where)

                    Dim sw As New StreamWriter(where, False)

                    If mSettings.GetConfig.FolderList.Count > 1 Then
                        For i As Integer = 0 To mSettings.GetConfig.FolderList.Count - 2
                            Dim strDirPath As String = mSettings.GetConfig.FolderList.Item(i)
                            Dim dir As New cDir(strDirPath)

                            Me.CurrentDirMessage = "Indexing " + strDirPath

                            If ext.Contains(".html") Then
                                treeNetLib.mBooMoreFilesToCome = True
                                treeNetLib.IndexRootFolderToHtml(strDirPath, sw, False)
                            Else
                                treeNetLib.IndexFolderToTxt(strDirPath, sw, False)
                            End If

                            Me.Progress += 1

                        Next
                    End If

                    Dim lastDir As New cDir(mSettings.GetConfig.FolderList.Item(mSettings.GetConfig.FolderList.Count - 1))
                    Me.CurrentDirMessage = "Indexing " + lastDir.DirectoryPath

                    If ext.Contains(".html") Then
                        treeNetLib.mBooFirstIndexFile = False
                        treeNetLib.IndexRootFolderToHtml(lastDir.DirectoryPath, sw, True)
                    Else
                        treeNetLib.IndexFolderToTxt(lastDir.DirectoryPath, sw, True)
                    End If

                    sw.Close()
                    If mSettings.GetConfig.ZipMergedFile Then
                        mSettings.ZipAdminFile(where)
                    End If

                    Me.Progress += 1

                End If

            Case cAdapter.IndexingMode.IN_ONE_FOLDER_SEPERATE

                ' DO NOT MERGE INDEX FILES 
                If Not Directory.Exists(mSettings.GetConfig.OutputDir) Then
                    MessageBox.Show(String.Format("{0} does not exist." + Environment.NewLine + Environment.NewLine + _
                              "Please change the Output folder in Configuration." + _
                              Environment.NewLine + "The index file will be created in the same folder you chose to index.", _
                              mSettings.GetConfig.OutputDir), Application.ProductName, MessageBoxButtons.OK)
                End If

                For i As Integer = 0 To mSettings.GetConfig.FolderList.Count - 1

                    Dim strDirPath As String = mSettings.GetConfig.FolderList.Item(i)

                    Dim sDrive As String = Path.GetPathRoot(strDirPath).Substring(0, 1)
                    Dim sDirName As String = Path.GetFileName(strDirPath)
                    Dim sep As String = mSettings.GetOptions.IndividualIndexFileWordSeperator

                    'where = mSettings.GetConfig.OutputDir + "\" + sDrive + sep + sDirName + sep + mSettings.GetConfig.GetIndexFileName
                    ' New Behavior for getting where location
                    where = mSettings.fGetIndexFilePath(i, cAdapter.IndexingMode.IN_ONE_FOLDER_SEPERATE)
                    'MsgBox(where = mSettings.GetConfig.OutputDir + "\" + sDrive + sep + sDirName + sep + mSettings.GetConfig.GetIndexFileName)

                    Dim sw As New StreamWriter(where, False)

                    Me.CurrentDirMessage = "Indexing " + strDirPath

                    If ext.Contains(".html") Then
                        treeNetLib.mBooFirstIndexFile = True
                        treeNetLib.IndexRootFolderToHtml(strDirPath, sw, True)
                    Else
                        treeNetLib.IndexFolderToTxt(strDirPath, sw, True)
                    End If

                    sw.Close()
                    If mSettings.GetConfig.ZipFilesInOutputDir Then
                        mSettings.ZipAdminFile(where)
                    End If

                    Me.Progress += 1

                Next

        End Select

    End Sub

    Private Sub IndexInEachDir(ByVal myReader As cAdapter)

        Dim where As String
        Dim folderList As New List(Of String)
        folderList = myReader.GetConfig.FolderList
        Dim treeNetLib As New cTreeNetLib(myReader)

        Dim ext As String = myReader.GetConfig.IndexFileExtension

        For i As Integer = 0 To myReader.GetConfig.FolderList.Count - 1

            Dim strDirPath As String = myReader.GetConfig.FolderList.Item(i)
            ' 2.5.1.1 Indexer halted if a configuration file had non-existent folders paths
            If Directory.Exists(strDirPath) Then

                where = myReader.fGetIndexFilePath(i, cAdapter.IndexingMode.IN_EACH_DIRECTORY)
                If Directory.Exists(Path.GetDirectoryName(where)) = False Then
                    Directory.CreateDirectory(Path.GetDirectoryName(where))
                End If

                Dim sw As New StreamWriter(where, False)

                Try
                    Me.CurrentDirMessage = "Indexing " + strDirPath

                    If ext.Contains("html") Then
                        'MessageBox.Show(myReader.GetConfig.mCssFilePath)
                        treeNetLib.mBooFirstIndexFile = True
                        treeNetLib.IndexRootFolderToHtml(strDirPath, sw, True)
                    Else
                        treeNetLib.IndexFolderToTxt(strDirPath, sw, True)
                    End If

                    Me.Progress += 1

                Catch ex As System.UnauthorizedAccessException
                    MsgBox(ex.Message + vbCrLf + "Please Run TreeGUI As Administrator or Change Output Directory.", MsgBoxStyle.Critical)
                    Exit Sub
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                    Exit Sub
                Finally
                    sw.Close()
                End Try

                ' Zip after sw is closed
                If myReader.GetConfig.ZipFilesInEachDir Then
                    myReader.ZipAdminFile(where)
                End If

            End If

        Next

    End Sub

    Private Function GetTabs() As String
        Dim tabs As String = ""
        For i As Integer = 1 To mNumTabs
            tabs += vbTab
        Next
        Return tabs
    End Function

    Private Function GetHeadingOpen(ByVal dir As cDir) As String

        Dim cName As String = "trigger"
        Select Case mNumTabs
            Case Is > mSettings.GetConfig.FolderExpandLevel
                cName = "expanded"
            Case Else
                cName = "trigger"
        End Select

        Dim tabs As String = String.Empty

        If mNumTabs < 7 Then
            If dir.GetSubDirColl.Count > 0 OrElse mFilter.GetFilesCollFiltered(dir).Count > 0 Then
                tabs = String.Format("<h{0} class=""{1}"">", mNumTabs.ToString, cName)
            Else
                tabs = String.Format("<h{0}>", mNumTabs.ToString)
            End If
        Else
            If dir.GetSubDirColl.Count > 0 OrElse mFilter.GetFilesCollFiltered(dir).Count > 0 Then
                tabs = String.Format("<h6 class=""{0}"">", cName)
            Else
                tabs = "<h6>"
            End If
        End If

        Return tabs

    End Function

    Private Function GetHeadingClose() As String

        If mNumTabs < 7 Then
            Dim tabs As String = "</h" + mNumTabs.ToString + ">"
            Return tabs
        Else
            Dim tabs As String = "</h6>"
            Return tabs
        End If

    End Function

End Class
