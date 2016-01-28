Public Class cInitializer

    Private mInitMode As cAdapter.InitializationMode
    Private mSettings As New cAdapter

    Public Property InitilizationMode() As cAdapter.InitializationMode
        Get
            Return mInitMode
        End Get
        Set(ByVal value As cAdapter.InitializationMode)
            mInitMode = value
        End Set
    End Property

    Public Property Settings() As cAdapter
        Get
            Return mSettings
        End Get
        Set(ByVal value As cAdapter)
            mSettings = value
        End Set
    End Property

End Class
