<Serializable()> Public Class cUndoRedoStack
    Inherits McoreData.Entity

    Dim m_Folders As New ArrayList

    Public Property Folders() As ArrayList
        Get
            Return m_Folders
        End Get
        Set(ByVal Value As ArrayList)
            AddHistory("Folders", m_Folders)
            m_Folders = Value
        End Set
    End Property

End Class
