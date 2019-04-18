Public Class UsuarioFicha
    Public Event ShowError(ByVal ErrorString As String)
    Public Event ActualizarLista()
    Private CurrentInfo As UsuariosData
#Region "Metodos"
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
    End Sub
    Public WriteOnly Property USUARIO_CLAVE() As String
        Set(ByVal value As String)
            Me.CurrentInfo = (New ISapAdminDatos).DataSelect(value)
        End Set
    End Property
    Private Sub Save()

    End Sub
#End Region





#Region " ConfigurarGrids "
    Private Sub ConfigurarGrid()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridDatos.Width ' - 48

        Dim EliminarColumn As New DataGridViewCheckBoxColumn()

        Dim PAIS_CLAVEColumn As New DataGridViewTextBoxColumn()
        Dim PAIS_NOMBREColumn As New DataGridViewTextBoxColumn()
        Dim MONEDAColumn As New DataGridViewTextBoxColumn()
        Dim CLAVE_CIAColumn As New DataGridViewTextBoxColumn()
        Dim ICCColumn As New DataGridViewCheckBoxColumn()
        Dim ICVColumn As New DataGridViewCheckBoxColumn()
        Dim IMPColumn As New DataGridViewCheckBoxColumn()
        Dim IFCColumn As New DataGridViewCheckBoxColumn()
        Dim ILIColumn As New DataGridViewCheckBoxColumn()
        Dim IMFColumn As New DataGridViewCheckBoxColumn()



        With PAIS_CLAVEColumn
            .HeaderText = "Pais"
            .Name = "PAIS_CLAVE"
            .DataPropertyName = "PAIS_CLAVE"
            .Width = CInt(TotalGridWidth * 0.05)

        End With


        With PAIS_NOMBREColumn
            .HeaderText = "Nombre"
            .Name = "PAIS_NOMBRE"
            .DataPropertyName = "PAIS_NOMBRE"
            .Width = CInt(TotalGridWidth * 0.06)
        End With

        With MONEDAColumn
            .HeaderText = "Moneda"
            .Name = "MONEDA"
            .DataPropertyName = "MONEDA"
            .Width = CInt(TotalGridWidth * 0.07)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        End With

        With CLAVE_CIAColumn
            .HeaderText = "ClaveCia"
            .Name = "CLAVE_CIA"
            .DataPropertyName = "CLAVE_CIA"
            .Width = CInt(TotalGridWidth * 0.28)
            .ReadOnly = True
        End With

        With ICCColumn
            .HeaderText = "ICC"
            .Name = "ICC"
            .DataPropertyName = "ICC"
            .Width = CInt(TotalGridWidth * 0.066)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With
        With ICVColumn
            .HeaderText = "ICV"
            .Name = "ICV"
            .DataPropertyName = "ICV"
            .Width = CInt(TotalGridWidth * 0.066)
            .ReadOnly = True

            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With IFCColumn
            .HeaderText = "IFC"
            .Name = "IFC"
            .DataPropertyName = "IFC"
            .Width = CInt(TotalGridWidth * 0.066)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With
        With ILIColumn
            .HeaderText = "ILI"
            .Name = "ILI"
            .DataPropertyName = "ILI"
            .Width = CInt(TotalGridWidth * 0.066)
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With

        With IMFColumn
            .HeaderText = "IMF"
            .Name = "IMF"
            .DataPropertyName = "IMF"
            .Width = CInt(TotalGridWidth * 0.066)
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With

        With Me.GridDatos
            .Columns.Add(EliminarColumn)
            .Columns.Add(PAIS_CLAVEColumn)
            .Columns.Add(PAIS_NOMBREColumn)
            .Columns.Add(CLAVE_CIAColumn)
            .Columns.Add(MONEDAColumn)
            .Columns.Add(ICCColumn)
            .Columns.Add(ICVColumn)
            .Columns.Add(IFCColumn)
            .Columns.Add(ILIColumn)
            .Columns.Add(IMFColumn)


            .AutoSizeColumnsMode = _
                DataGridViewAutoSizeColumnsMode.AllCells

            .RowHeadersWidth = CInt(TotalGridWidth * 0.09)
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .ColumnHeadersDefaultCellStyle = columnHeaderStyle
            .ReadOnly = False
            .RowsDefaultCellStyle.BackColor = Color.PowderBlue
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
        End With
    End Sub
#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ConfigurarGrid()

    End Sub
End Class
