Imports System.IO
Imports System.ServiceProcess

Public Class cAdminService

    Private m_SvcAdmin As New McoreSystem.ServiceAdmin

    Public Sub startServiceInternally(ByVal ServiceName As String)

        Dim controller As New ServiceController
        controller.MachineName = "."
        If isInstalledService(ServiceName) Then
            If Not isRunningService(ServiceName) Then
                controller.ServiceName = ServiceName
                controller.Start()
            End If
        End If

    End Sub

    Public Sub startServiceExternally(ByVal ServiceName As String)

        Dim proc As New System.Diagnostics.Process
        Dim psi As New ProcessStartInfo("net", "start " + Chr(34) + ServiceName + Chr(34))
        proc.StartInfo = psi
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        proc.Start()
        proc.WaitForExit()

    End Sub

    Public Sub stopServiceExternally(ByVal ServiceName As String)

        Dim proc As New System.Diagnostics.Process
        Dim psi As New ProcessStartInfo("net", "stop " + Chr(34) + ServiceName + Chr(34))
        proc.StartInfo = psi
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        proc.Start()
        proc.WaitForExit()

    End Sub

    Public Sub stopServiceInternally(ByVal ServiceName As String)

        Dim controller As New ServiceController
        controller.MachineName = "."
        controller.ServiceName = ServiceName
        If isInstalledService(ServiceName) Then
            If isRunningService(ServiceName) Then
                controller.Stop()
            End If
        End If

    End Sub

    Public Function isInstalledService(ByVal ServiceName As String) As Boolean

        'BUG: 1.2.6.1 Incorrectly determined McoreIndexer installed state
        Dim regMcoreIndexerRoot As Microsoft.Win32.RegistryKey = _
          Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services")
        Dim strServices() As String = regMcoreIndexerRoot.GetSubKeyNames
        For Each svc As String In strServices
            If svc = ServiceName Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function isRunningService(ByVal ServiceName As String) As Boolean

        If isInstalledService(ServiceName) Then
            Dim controller As New ServiceController
            controller.MachineName = "."
            controller.ServiceName = ServiceName
            Dim status As String = controller.Status.ToString
            Console.WriteLine(status)
            Return (status = "Running")
        End If

    End Function

    Public Sub InstallAndRunService(ByVal ServicePath As String, ByVal ServiceName As String)

        InstallService(ServicePath, ServiceName)
        startServiceExternally(ServiceName)

    End Sub

    Public Sub InstallService(ByVal ServicePath As String, ByVal ServiceName As String)
        If Not isInstalledService(ServiceName) Then
            m_SvcAdmin.InstallService(ServicePath, ServiceName, ServiceName)
            m_SvcAdmin.AddServiceDescriptionToRegistry(ServiceName, "Directory Indexing service for TreeGUI")
        End If
    End Sub

    Public Sub StopAndUninstallService(ByVal ServiceName As String)
        stopServiceExternally(ServiceName)
        uninstallService(ServiceName)
    End Sub

    Private Sub uninstallService(ByVal ServiceName As String)
        If isInstalledService(ServiceName) Then
            m_SvcAdmin.RemoveServiceDescriptionFromRegistry(ServiceName)
            m_SvcAdmin.UnInstallService(ServiceName)
        End If
    End Sub

    Public Function isProperlyInstalledService(ByVal TreeGuiPath As String, ByVal ServiceName As String) As Boolean
        Return Me.getProperServicePath(TreeGuiPath, ServiceName) = Me.getCurrentServicePath(ServiceName)
    End Function

    Public Function getProperServicePath(ByVal TreeGuiPath As String, ByVal ServiceName As String) As String
        Return TreeGuiPath + "\" + ServiceName + ".exe"
    End Function

    Public Function getCurrentServicePath(ByVal ServiceName As String) As String
        Dim regMcoreIndexerRoot As Microsoft.Win32.RegistryKey = _
          Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services")
        Dim strServices() As String = regMcoreIndexerRoot.GetSubKeyNames
        For Each svc As String In strServices
            If svc = ServiceName Then
                Dim regMcoreIndexer As Microsoft.Win32.RegistryKey = _
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\" + svc)
                Return CStr(regMcoreIndexer.GetValue("ImagePath", ""))
            End If
        Next
        Return Nothing
    End Function

    Public Function setProperServicePath(ByVal TreeGuiPath As String, ByVal ServiceName As String) As Boolean

        stopServiceExternally(ServiceName)
        Dim regMcoreIndexerRoot As Microsoft.Win32.RegistryKey = _
            Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services")
        Dim strServices() As String = regMcoreIndexerRoot.GetSubKeyNames
        For Each svc As String In strServices
            If svc = ServiceName Then
                Dim regMcoreIndexer As Microsoft.Win32.RegistryKey = _
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\" + svc, True)
                regMcoreIndexer.SetValue("ImagePath", getProperServicePath(TreeGuiPath, ServiceName))
                startServiceExternally(ServiceName)
                Return True
            End If
        Next

        Return False
    End Function


End Class
