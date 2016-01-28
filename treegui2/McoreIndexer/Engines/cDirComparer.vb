Imports System.Collections.Generic

Public Class cDirComparer
    Implements IComparer(Of cDir)

    Public Function Compare(ByVal x As cDir, ByVal y As cDir) As Integer Implements System.Collections.Generic.IComparer(Of cDir).Compare

        Return String.Compare(x.DirectorySize.ToString("0000000000"), y.DirectorySize.ToString("0000000000"))

    End Function

End Class
