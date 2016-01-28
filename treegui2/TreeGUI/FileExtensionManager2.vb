Public Class FileExtensionManager2

    <System.Runtime.InteropServices.DllImport("shell32.dll")> Shared Sub _
SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer, _
ByVal dwItem1 As Integer, ByVal dwItem2 As Integer)

    End Sub

    ' Create the new file association
    '
    ' Extension is the extension to be registered (eg ".tgc"
    ' ClassName is the name of the associated class (eg "TreeGUI.Config")
    ' Description is the textual description (eg "TreeGUI Config File"
    ' ExeProgram is the app that manages that extension (eg "c:\Cad\MyCad.exe")

    Public Function CreateFileAssociation(ByVal extension As String, _
        ByVal className As String, ByVal description As String, _
        ByVal exeProgram As String, ByVal iconIndex As Integer) As Boolean
        Const SHCNE_ASSOCCHANGED As Integer = &H8000000
        Const SHCNF_IDLIST As Integer = 0

        ' ensure that there is a leading dot
        If extension.Substring(0, 1) <> "." Then
            extension = "." & extension
        End If

        Dim key1, key2, key3, key4 As Microsoft.Win32.RegistryKey
        key1 = Nothing
        key2 = Nothing
        key3 = Nothing
        key4 = Nothing

        Try
            ' create a value for this key that contains the classname
            key1 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(extension)
            key1.SetValue("", className)
            ' create a new key for the Class name
            key2 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(className)
            key2.SetValue("", description)
            ' associate the program to open the files with this extension
            key3 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(className & _
                "\Shell\Open\Command")
            key3.SetValue("", exeProgram & " ""%1""")
            key4 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(className & _
                "\DefaultIcon")
            key4.SetValue(Nothing, String.Format("{0},{1}", exeProgram, iconIndex))
        Catch e As Exception
            Console.WriteLine(e.Message)
            Return False
        Finally
            If Not key1 Is Nothing Then key1.Close()
            If Not key2 Is Nothing Then key2.Close()
            If Not key3 Is Nothing Then key3.Close()
            If Not key4 Is Nothing Then key4.Close()
        End Try

        ' notify Windows that file associations have changed
        SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0)
        Return True
    End Function

    Public Sub SetIcon(ByVal className As String, ByVal iconFilePath As String, ByVal iconIndex As Integer)
        Try
            Dim key4 As Microsoft.Win32.RegistryKey
            key4 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(className & _
           "\DefaultIcon")
            key4.SetValue(Nothing, String.Format("{0},{1}", iconFilePath, iconIndex))
        Catch ex As Exception
        End Try
    End Sub

    Const SHCNE_ASSOCCHANGED As Integer = &H8000000
    Const SHCNF_IDLIST As Integer = 0

    ' Destroy a file association
    '
    ' NOTE: requires the GetRegistryValue and DeleteRegistryKey functions

    Public Function DeleteFileAssociation(ByVal Extension As String) As Boolean
        Dim ClassName As String

        ' Const HKEY_CLASSES_ROOT = &H80000000

        ' ensure that there is a leading dot
        If Left(Extension, 1) <> "." Then
            Extension = "." & Extension
        End If

        Dim key1 As Microsoft.Win32.RegistryKey

        Try
            key1 = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Extension)
            ' read the associated class name
            ClassName = CStr(key1.GetValue(Extension, ""))
            ' exit if there is no extension association
            If Len(ClassName) = 0 Then Return False
            ' delete this key
            key1.DeleteSubKey(Extension)
            ' delete the other key where registration data is found
            key1.DeleteSubKey(ClassName)
            ' notify Windows that file associations have changed
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0)
        Catch ex As Exception
            Return False
        End Try


    End Function

End Class

