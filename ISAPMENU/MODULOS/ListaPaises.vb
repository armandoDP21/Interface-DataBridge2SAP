Public Structure Paises
    Private PaisName As String
    Private PaisClave As String
    Public Sub New(ByVal name As String, ByVal id As String)
        PaisName = name
        PaisClave = id
    End Sub
    Public ReadOnly Property Name() As String
        Get
            Return PaisName
        End Get
    End Property
    Public ReadOnly Property Clave() As String
        Get
            Return PaisClave
        End Get
    End Property
End Structure