
Public Class cFile

    Private mSize As Double
    Private mPath As String

    Public Sub New(ByVal filePath As String)

        Me.mPath = filePath

        If IO.File.Exists(filePath) Then
            Dim fi As New System.IO.FileInfo(GetFilePath)
            SetSize(fi.Length)
        End If

    End Sub

    Public Function GetFileExtension() As String
        Return IO.Path.GetExtension(mPath)
    End Function
    Public Function GetFilePath() As String
        Return Me.mPath
    End Function

    Public Function GetFileName() As String
        Return System.IO.Path.GetFileName(GetFilePath)
    End Function

    Public Function GetFileNameWithoutExtension() As String
        Return System.IO.Path.GetFileNameWithoutExtension(GetFilePath)
    End Function
    Public Sub SetSize(ByVal size As Double)
        Me.mSize = size
    End Sub

    Public Function GetSize(ByVal prefix As cDir.BinaryPrefix) As Double
        Select Case prefix
            Case cDir.BinaryPrefix.Bytes
                Return mSize
            Case cDir.BinaryPrefix.Kibibits
                Return (mSize / 128)
            Case cDir.BinaryPrefix.Kibibytes
                Return (mSize / 1024)
            Case cDir.BinaryPrefix.Mebibytes
                Return (mSize / (1024 * 1024))
            Case cDir.BinaryPrefix.Gibibytes
                Return (mSize / (1024 * 1024 * 1024))
        End Select
    End Function

    Public Function GetSizeToString(ByVal prefix As cDir.BinaryPrefix) As String

        Dim bp As String = ""
        Select Case prefix
            Case cDir.BinaryPrefix.Gibibytes
                bp = " GiB"
            Case cDir.BinaryPrefix.Mebibytes
                bp = " MiB"
            Case cDir.BinaryPrefix.Kibibytes
                bp = " KiB"
        End Select
        Return GetSize(prefix).ToString("N") + bp
    End Function

End Class

