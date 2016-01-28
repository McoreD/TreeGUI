Imports System.Windows.Forms

<Serializable()> Public Class Config

    Private m_IndexingEngineId As Integer = 1 'Default to Tree.NET
    Public ProcessPriority As Threading.ThreadPriority

    ' Main Form 
    Private m_FolderList As New List(Of String)

    Private m_IndexFileExtension As String = ".html" 'Because default is Tree.NET
    Private m_IndexFileName As String = "index"

    ' Config > Engine > Tree 
    Private m_AddFilesTree As Boolean = True
    Private m_RemoveTreeBranches As Boolean
    Private m_Ascii As Boolean = True

    ' Config > Output 
    Private m_OutputDir As String

    Private m_IndexFileInSameDir As Boolean = True
    Private m_IndexFileInOneDir As Boolean = False
    Private m_MergeFiles As Boolean = False
    Public CreateIndividualFilesInOutputDir As Boolean = True

    ' Engine > TreeNet 
    Public ShowFilesTreeNet As Boolean = True
    Public ShowFileSize As Boolean = True
    Public ShowDirSize As Boolean = True
    Public ShowFilePath As Boolean
    Public ShowFolderPath As Boolean
    Public HideExtension As Boolean = False
    Public ShowVirtualFolders As Boolean = False
    Public ShowFolderPathOnStatusBar As Boolean = False

    Public FolderHeadingStyle As String = "*~"

    ' Engine > TreeNet > XHTML Output

    Private mListType As eListType = eListType.Bullets

    ' 


    ' Zip Operations
    Public ZipAfterIndexed As Boolean = False
    Public ZipAndDeleteFile As Boolean = False
    Public ZipMergedFile As Boolean = False
    Public ZipFilesInEachDir As Boolean = True
    Public ZipFilesInOutputDir As Boolean = False

    ' Filter

    Public EnabledFiltering As Boolean = True
    Public HideProtectedOperatingSystemFilesFolders As Boolean = False

    Public IgnoreHiddenFiles As Boolean = False
    Public IgnoreSystemFiles As Boolean = False
    Public IgnoreFollowingFiles As Boolean = True
    Private mDefaultCssFileName As String = "Default.css"
    Public IgnoreFilesList As String = ".DS_Store|*.db|index.html|*.ini"
    Public IgnoreHiddenFolders As Boolean = False
    Public IgnoreSystemFolders As Boolean = False
    Public IgnoreEmptyFolders As Boolean = False 'war59312

    ' Misc
    Public MergedHtmlTitle As String = "Site Index"
    Public ServerInfo As String = "ftp://127.0.0.1:21"

    Public RevereFileOrder As Boolean = False
    Public ShowFileCount As Boolean = False

    Private mVirtualFolderList As New List(Of String)

    Public mSortBySize As Boolean = False
    Public mSortBySizeMode As Integer = 1 '  0 = Ascending, 1 = Descending

    Public AudioQuickScan As Boolean = True
    Public AudioInfo As Boolean = True

    ' 2.7.1.0 Indexing Engine Names are now read by Config
    Private mEngineNames() As String = {"Tree Walk Utility", _
                                        "Tree.NET"}

    Private mFolderExpandLevel As Integer = 2
    Public Property FolderExpandLevel() As Integer
        Get
            Return mFolderExpandLevel
        End Get
        Set(ByVal value As Integer)
            mFolderExpandLevel = value
        End Set
    End Property

    Private mCollapseFolders As Boolean = False
    Public Property CollapseFolders() As Boolean
        Get
            Return mCollapseFolders
        End Get
        Set(ByVal value As Boolean)
            mCollapseFolders = value
        End Set
    End Property

    Private mLogoPath As String
    Public Property LogoPath() As String
        Get
            Return mLogoPath
        End Get
        Set(ByVal value As String)
            mLogoPath = value
        End Set
    End Property



    ' 2.7.1.4 Default CSS file is read from Application startup path 
    ' default.css when current file does not exit
    Private mCssFilePath As String = Application.StartupPath + mDefaultCssFileName

    Public ReadOnly Property CssFileName() As String
        Get
            Return mDefaultCssFileName
        End Get
    End Property

    Public Property CssFilePath() As String

        Get
            Return Me.mCssFilePath
        End Get

        Set(ByVal value As String)
            If IO.File.Exists(value) Then
                Me.mCssFilePath = value
            Else
                mCssFilePath = Application.StartupPath + IO.Path.DirectorySeparatorChar + mDefaultCssFileName
            End If
        End Set

    End Property


    Public Property VirtualFolderList() As List(Of String)
        Get
            Return mVirtualFolderList
        End Get
        Set(ByVal value As List(Of String))
            mVirtualFolderList = value
        End Set
    End Property


    Enum eListType
        Bullets
        Numbered
    End Enum

    Public Property ListType() As eListType
        Get
            Return mListType
        End Get
        Set(ByVal value As eListType)
            mListType = value
        End Set
    End Property

    Public Function GetIndexFileName() As String
        Return Me.m_IndexFileName + Me.m_IndexFileExtension
    End Function

    Public Function GetIndexFilePaths() As String()

        Dim paths(FolderList.Count - 1) As String

        For i As Integer = 0 To FolderList.Count - 1
            paths(i) = FolderList(i) + IO.Path.DirectorySeparatorChar + Me.GetIndexFileName
        Next

        Return paths

    End Function

    Public Function GetIndexFilePath() As String
        Return Me.m_OutputDir + IO.Path.DirectorySeparatorChar + Me.GetIndexFileName
    End Function

    Public Property FolderList() As List(Of String)
        Get
            Return m_FolderList
        End Get
        Set(ByVal value As List(Of String))
            Me.m_FolderList = value
        End Set

    End Property

    Public Property IndexFileName() As String
        Get
            Return Me.m_IndexFileName
        End Get
        Set(ByVal value As String)
            Me.m_IndexFileName = value
        End Set
    End Property

    Public Property IndexFileExtension() As String
        Get

            Return Me.m_IndexFileExtension
        End Get
        Set(ByVal value As String)
            m_IndexFileExtension = value
        End Set
    End Property

    Public Property isIndexFileInSameDir() As Boolean
        Get
            Return m_IndexFileInSameDir
        End Get
        Set(ByVal value As Boolean)
            Me.m_IndexFileInSameDir = value
        End Set
    End Property

    Public Property isIndexFileInOneDir() As Boolean
        Get
            Return m_IndexFileInOneDir
        End Get
        Set(ByVal value As Boolean)
            Me.m_IndexFileInOneDir = value
        End Set
    End Property

    Public Property isRemoveTreeBranches() As Boolean
        Get
            Return Me.m_RemoveTreeBranches
        End Get
        Set(ByVal value As Boolean)
            Me.m_RemoveTreeBranches = value
        End Set
    End Property

    Public Property isMergeFiles() As Boolean
        Get
            Return Me.m_MergeFiles
        End Get
        Set(ByVal value As Boolean)
            Me.m_MergeFiles = value
        End Set
    End Property

    Public Property isAscii() As Boolean
        Get
            Return Me.m_Ascii
        End Get
        Set(ByVal value As Boolean)
            m_Ascii = value
        End Set
    End Property

    Public Property isAddFiles() As Boolean
        Get
            Return Me.m_AddFilesTree
        End Get
        Set(ByVal value As Boolean)
            m_AddFilesTree = value
        End Set
    End Property

    Public Property OutputDir() As String
        Get
            Return Me.m_OutputDir
        End Get
        Set(ByVal value As String)
            Me.m_OutputDir = value
        End Set
    End Property

    Public Property IndexingEngineID() As Integer
        Get
            Return Me.m_IndexingEngineId
        End Get
        Set(ByVal value As Integer)
            m_IndexingEngineId = value
        End Set
    End Property

    Public Function GetIndexingEngineName(ByVal id As Integer) As String
        Return mEngineNames(id)
    End Function

    Public Function GetIndexingEngineName() As String
        Return mEngineNames(IndexingEngineID)
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub SetSingleIndexPath(ByVal fpath As String)

        OutputDir = IO.Path.GetDirectoryName(fpath)
        IndexFileName = IO.Path.GetFileNameWithoutExtension(fpath)
        IndexFileExtension = IO.Path.GetExtension(fpath)
        isMergeFiles = True

    End Sub

End Class
