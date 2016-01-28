Imports System.ServiceProcess

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class McoreIndexer
    Inherits System.ServiceProcess.ServiceBase

    'UserService overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    <System.Diagnostics.DebuggerNonUserCode()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New McoreIndexer}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tmrInterval = New System.Timers.Timer
        Me.tmrDateTime = New System.Timers.Timer
        Me.tmrSettingsReader = New System.Timers.Timer
        CType(Me.tmrInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmrDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmrSettingsReader, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'tmrInterval
        '
        Me.tmrInterval.Interval = 300000
        '
        'tmrDateTime
        '
        Me.tmrDateTime.Enabled = True
        Me.tmrDateTime.Interval = 1000
        '
        'tmrSettingsReader
        '
        Me.tmrSettingsReader.Enabled = True
        Me.tmrSettingsReader.Interval = 600000
        '
        'McoreIndexer
        '
        Me.ServiceName = "TreeGUI"
        CType(Me.tmrInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmrDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmrSettingsReader, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents tmrInterval As System.Timers.Timer
    Friend WithEvents tmrDateTime As System.Timers.Timer
    Friend WithEvents tmrSettingsReader As System.Timers.Timer

End Class
