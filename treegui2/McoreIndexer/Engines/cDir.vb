
Public Class cDir

    Private mSubDirCol As New List(Of cDir)
    Private mSizeOfFiles As Double = 0.0 'Kibibytes
    Private mDirPath As String
    Private mFiles As New List(Of cFile)

    Public Sub New(ByVal dirPath As String)
        mDirPath = dirPath
    End Sub

    Public Function DirectoryPath() As String
        Return mDirPath
    End Function

    Public Function DirectoryName() As String
        Return IO.Path.GetFileName(DirectoryPath)
    End Function
    Public Function SetFile(ByVal filePath As String, Optional ByVal prefix As BinaryPrefix = BinaryPrefix.Kibibytes) As String

        Dim f As New cFile(filePath)
        mFiles.Add(f)

        mSizeOfFiles += f.GetSize(BinaryPrefix.Kibibytes)

        Select Case prefix
            Case BinaryPrefix.Mebibytes
                Return f.GetSize(BinaryPrefix.Mebibytes).ToString("N") & " MiB"
            Case BinaryPrefix.Gibibytes
                Return f.GetSize(BinaryPrefix.Gibibytes).ToString("N") & " GiB"
            Case Else
                Return f.GetSize(BinaryPrefix.Kibibytes).ToString("N") & " KiB"
        End Select

    End Function


    Public Function GetFilesColl() As List(Of cFile)

        Return mFiles
    End Function

    Public Sub AddDir(ByVal mySubDir As cDir)

        mSubDirCol.Add(mySubDir)

    End Sub

    Public Function GetSubDirColl() As List(Of cDir)
        Return mSubDirCol
    End Function

    Public Function DirectorySize() As Double

        Dim dirSize As Double = Me.mSizeOfFiles

        For Each dir As cDir In Me.GetSubDirColl
            dirSize += dir.DirectorySize
        Next

        Return dirSize

    End Function

    Public Function DirectorySizeToString(ByVal prefix As BinaryPrefix) As String

        Select Case prefix
            Case BinaryPrefix.Gibibytes
                Return (Me.DirectorySize / (1024 * 1024)).ToString("N") & " GiB"
            Case BinaryPrefix.Mebibytes
                Return (Me.DirectorySize / 1024).ToString("N") & " MiB"
            Case BinaryPrefix.Kibibytes
                Return (Me.DirectorySize).ToString("N") & " KiB"
        End Select

        Return Nothing

    End Function

    Enum BinaryPrefix
        Bytes
        Kibibits
        Kibibytes
        Mebibytes
        Gibibytes
    End Enum

    Private Function GetSizeOfFiles() As Double
        Return mSizeOfFiles
    End Function

End Class
