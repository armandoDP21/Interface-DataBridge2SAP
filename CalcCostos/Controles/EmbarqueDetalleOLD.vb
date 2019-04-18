Public Class EmbarqueDetalleOLD
    Private Const DataMember As String = "EMBARQUESDETALLES."
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

    End Sub
    Public Sub Bind2Control()
        'Me.DetallesDR.DataSource = InfoCache.EmbarquesDS.EMBARQUESDETALLES.DefaultView

        Me.PRO_CLAVE.DataBindings.Clear()
        Me.PRO_DESCRI.DataBindings.Clear()
        'Me.EMBARQUE_CLAVE.DataBindings.Clear()
        Me.DMP_CANTID.DataBindings.Clear()
        Me.DMP_PRECIO.DataBindings.Clear()
        Me.PRO_GRAMAJ.DataBindings.Clear()
        Me.PFOB.DataBindings.Clear()
        Me.Kilos.DataBindings.Clear()

        Me.AVG_FLETE.DataBindings.Clear()
        Me.AVG_SEGUROS.DataBindings.Clear()
        Me.AVG_OG.DataBindings.Clear()
        Me.CostoTotal.DataBindings.Clear()
        Me.CU.DataBindings.Clear()
        Me.ML_CU.DataBindings.Clear()
        Me.ML_CIF.DataBindings.Clear()


        Me.PRO_CLAVE.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PRO_CLAVE")
        Me.PRO_DESCRI.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PRO_DESCRI")
        'Me.EMBARQUE_CLAVE.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "EMBARQUE_CLAVE")
        Me.DMP_CANTID.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "DMP_CANTID")
        Me.DMP_PRECIO.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "DMP_PRECIO")
        Me.PRO_GRAMAJ.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PRO_GRAMAJ")
        Me.PFOB.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PFOB")
        Me.Kilos.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "KILOS")

        Me.AVG_FLETE.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "AVG_FLETE")
        Me.AVG_SEGUROS.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "AVG_SEGUROS")
        Me.AVG_OG.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "AVG_OG")
        Me.CostoTotal.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "CostoTotal")
        Me.CU.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "CU")

        Me.ML_CU.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "ML_CU")
        Me.ML_CIF.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "ML_CIF")

    End Sub
    Public WriteOnly Property EmbarqueClave As String
        Set(ByVal value As String)
            InfoCache.EmbarquesDS.EMBARQUESDETALLES.DefaultView.RowFilter = "EMBARQUE_CLAVE='" & value & "'"
        End Set
    End Property


End Class
