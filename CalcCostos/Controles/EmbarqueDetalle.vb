Public Class EmbarqueDetalle
    Private EMBARQUE_CLAVELOCAL As String
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

    End Sub
    Public WriteOnly Property EmbarqueClave As String
        Set(ByVal value As String)
            EMBARQUE_CLAVELOCAL = value
            InfoCache.EmbarquesDS.EMBARQUESDETALLES.DefaultView.RowFilter = "EMBARQUE_CLAVE='" & value & "'"
            PresentarTotales()
            AddHandler InfoCache.EmbarquesDS.EMBARQUESDETALLES.Totales, AddressOf PresentarTotales
        End Set
    End Property

    Private Sub PresentarTotales()
        Me.FOBTotal.Text = FormatNumber((From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES Where p.EMBARQUE_CLAVE = EMBARQUE_CLAVELOCAL Select p.PFOB).Sum, 2)
        Me.KILOSTotal.Text = FormatNumber((From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES Where p.EMBARQUE_CLAVE = EMBARQUE_CLAVELOCAL Select p.KILOS).Sum, 4)
    End Sub
    Private Sub ConfigurarGrid()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridDatos.Width - 48

        Dim PRO_CLAVEColumn As New DataGridViewTextBoxColumn()
        Dim PRO_DESCRIColumn As New DataGridViewTextBoxColumn()
        Dim DMP_CANTIDColumn As New DataGridViewTextBoxColumn()
        Dim DMP_PRECIOColumn As New DataGridViewTextBoxColumn()
        Dim PRO_GRAMAJColumn As New DataGridViewTextBoxColumn()
        Dim PFOBColumn As New DataGridViewTextBoxColumn()
        Dim KilosColumn As New DataGridViewTextBoxColumn()


        With PRO_CLAVEColumn
            .HeaderText = "Codigo"
            .Name = "PRO_CLAVE"
            .DataPropertyName = "PRO_CLAVE"
            .Width = CInt(TotalGridWidth * 0.12)
            .DefaultCellStyle.SelectionBackColor = Color.Red
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .ReadOnly = True
        End With
        With PRO_DESCRIColumn
            .HeaderText = "Descripcion"
            .Name = "PRO_DESCRI"
            .DataPropertyName = "PRO_DESCRI"
            .Width = CInt(TotalGridWidth * 0.4)
            '.DefaultCellStyle.SelectionBackColor = Color.Red
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .ReadOnly = True
        End With
        With DMP_CANTIDColumn
            .HeaderText = "Cantidad"
            .Name = "DMP_CANTID"
            .DataPropertyName = "DMP_CANTID"
            .Width = CInt(TotalGridWidth * 0.11)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.Format = "ddd dd/MM/yyyy"
            .ReadOnly = True
        End With
        With DMP_PRECIOColumn
            .HeaderText = "P. Unit"
            .Name = "DMP_PRECIO"
            .DataPropertyName = "DMP_PRECIO"
            .Width = CInt(TotalGridWidth * 0.12)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = False
        End With
        With PFOBColumn
            .HeaderText = "FOB"
            .Name = "PFOB"
            .DataPropertyName = "PFOB"
            .Width = CInt(TotalGridWidth * 0.14)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
            .DefaultCellStyle.Format = "#,#.00"
        End With
        With PRO_GRAMAJColumn
            .HeaderText = "Gramos"
            .Name = "PRO_GRAMAJ"
            .DataPropertyName = "PRO_GRAMAJ"
            .Width = CInt(TotalGridWidth * 0.14)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = False
        End With

        With KilosColumn
            .HeaderText = "Kilos"
            .Name = "KILOS"
            .DataPropertyName = "KILOS"
            .Width = CInt(TotalGridWidth * 0.14)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,#.0000"
            .ReadOnly = True
        End With


        With Me.GridDatos
            .Columns.Add(PRO_CLAVEColumn)
            .Columns.Add(PRO_DESCRIColumn)
            .Columns.Add(DMP_CANTIDColumn)
            .Columns.Add(DMP_PRECIOColumn)
            .Columns.Add(PFOBColumn)
            .Columns.Add(PRO_GRAMAJColumn)
            .Columns.Add(KilosColumn)
            .AutoSizeColumnsMode = _
                DataGridViewAutoSizeColumnsMode.AllCells

            .RowHeadersWidth = CInt(TotalGridWidth * 0.09)
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .ColumnHeadersDefaultCellStyle = columnHeaderStyle
            .ReadOnly = False
            .RowsDefaultCellStyle.BackColor = Color.Beige

            .AlternatingRowsDefaultCellStyle.BackColor = Color.Honeydew

            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
        End With

    End Sub

    Public Sub New()
        InitializeComponent()
        ConfigurarGrid()
    End Sub
    Public Sub Bind2Control()
        'Me.DetallesDR.DataSource = InfoCache.EmbarquesDS.EMBARQUESDETALLES.DefaultView

        'Me.PRO_CLAVE.DataBindings.Clear()
        'Me.PRO_DESCRI.DataBindings.Clear()
        ''Me.EMBARQUE_CLAVE.DataBindings.Clear()
        'Me.DMP_CANTID.DataBindings.Clear()
        'Me.DMP_PRECIO.DataBindings.Clear()
        'Me.PRO_GRAMAJ.DataBindings.Clear()
        'Me.PFOB.DataBindings.Clear()
        'Me.Kilos.DataBindings.Clear()

        'Me.AVG_FLETE.DataBindings.Clear()
        'Me.AVG_SEGUROS.DataBindings.Clear()
        'Me.AVG_OG.DataBindings.Clear()
        'Me.CostoTotal.DataBindings.Clear()
        'Me.CU.DataBindings.Clear()
        'Me.ML_CU.DataBindings.Clear()
        'Me.ML_CIF.DataBindings.Clear()


        'Me.PRO_CLAVE.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PRO_CLAVE")
        'Me.PRO_DESCRI.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PRO_DESCRI")
        ''Me.EMBARQUE_CLAVE.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "EMBARQUE_CLAVE")
        'Me.DMP_CANTID.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "DMP_CANTID")
        'Me.DMP_PRECIO.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "DMP_PRECIO")
        'Me.PRO_GRAMAJ.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PRO_GRAMAJ")
        'Me.PFOB.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PFOB")
        'Me.Kilos.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "KILOS")

        'Me.AVG_FLETE.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "AVG_FLETE")
        'Me.AVG_SEGUROS.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "AVG_SEGUROS")
        'Me.AVG_OG.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "AVG_OG")
        'Me.CostoTotal.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "CostoTotal")
        'Me.CU.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "CU")

        'Me.ML_CU.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "ML_CU")
        'Me.ML_CIF.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "ML_CIF")

    End Sub

End Class
