Imports System.Management

Public Class WMI_Library

    Public Enum LoggedInUserReturn
        Successful = 0
        AccessDenied = 2
        InsufficientPrivilege = 3
        UnknownFailure = 8
        PathNotFound = 9
        InvalidParameter = 21
    End Enum 'LoggedInUserReturn'

    Public Shared Function GetLoggedInUser() As String

        Dim mc As New System.Management.ManagementClass("Win32_Process")
        Dim moc As System.Management.ManagementObjectCollection = mc.GetInstances
        Dim mo As System.Management.ManagementObject

        For Each mo In moc

            Dim argList As String() = {String.Empty}

            Dim objReturn As Object = mo.InvokeMethod("GetOwner", argList)

            Dim returnValue As LoggedInUserReturn = CType(Convert.ToInt32(objReturn), LoggedInUserReturn)

            If returnValue = LoggedInUserReturn.Successful Then
                Dim userName As String = argList(0)

                If Not userName.Equals("SYSTEM") AndAlso _
                  Not userName.Equals("LOCAL SERVICE") AndAlso _
                  Not userName.Equals("NETWORK SERVICE") Then

                    Return userName

                End If

            End If

        Next

        Return Nothing

    End Function 'GetLoggedInUser'

End Class