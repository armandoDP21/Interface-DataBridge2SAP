Public Class FichaCostosT1
    Private GastosTipo As DataView
    'Private ClaveActual As String
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

    End Sub
#Region "Metodos"
    Public WriteOnly Property EmbarqueClave As String
        Set(ByVal value As String)
            'ClaveActual = value
            If Not IsNothing(GastosTipo) Then
                GastosTipo.Dispose()
            End If
            GastosTipo = New DataView(InfoCache.EmbarquesDS.EMBARQUESGASTOS)
            GastosTipo.RowFilter = "EMBARQUE_CLAVE='" & value & "'" & " and TIPO=1"
            Me.GridGastos.DataSource = Me.GastosTipo
            AddHandler InfoCache.EmbarquesDS.EMBARQUESGASTOS.TotalT1, AddressOf PresentarTotales
        End Set
    End Property
    Private Sub ConfigurarGrid()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridGastos.Width - 16

        Dim DescripcionColumn As New DataGridViewTextBoxColumn()
        Dim MontoColumn As New DataGridViewTextBoxColumn()

        With DescripcionColumn
            .HeaderText = "Descripcion"
            .Name = "Descripcion"
            .DataPropertyName = "NOMBRE"
            .Width = CInt(TotalGridWidth * 0.72)
            .DefaultCellStyle.SelectionBackColor = Color.Red
            .ReadOnly = True
        End With

        With MontoColumn
            .HeaderText = "Monto"
            .Name = "Monto"
            .DataPropertyName = "Monto"
            .Width = CInt(TotalGridWidth * 0.25)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,#.00"
            .DefaultCellStyle.SelectionBackColor = Color.Red

        End With

        With Me.GridGastos

            .Columns.Add(DescripcionColumn)
            .Columns.Add(MontoColumn)

            .AutoSizeColumnsMode = _
                DataGridViewAutoSizeColumnsMode.AllCells

            .RowHeadersWidth = CInt(TotalGridWidth * 0.09)
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .ColumnHeadersDefaultCellStyle = columnHeaderStyle
            .ReadOnly = False
            .RowsDefaultCellStyle.BackColor = Color.Ivory
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
        End With

    End Sub
    Private Sub PresentarTotales(ByVal Totales As Decimal)
        Me.TotalGL.Text = FormatNumber(Totales, 2)
    End Sub
#End Region

    Public Sub New()
        InitializeComponent()
        ConfigurarGrid()
    End Sub

    Private Sub FichaCostosT1_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed

    End Sub
End Class
