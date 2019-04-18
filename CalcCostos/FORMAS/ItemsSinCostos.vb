Public Class ItemsSinCostos

    Private Costos As InterfaseCostosData
    Private NoPlanta As String
    Private Periodo As String
    'Private FiscalYear As Integer
    Private IdCompania As String

#Region "Metodos"
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        Me.FiscalYear.Text = Now.Year.ToString
        Me.PeriodCB.SelectedIndex = Now.Month - 1

        'Me.FechaDesde.Value = "13/5/2011"
        'Me.FechaHasta.Value = "19/5/2011"
        Select Case InfoCache.PaisClave
            Case 2
                NoPlanta = "7901"
            Case 4
                NoPlanta = "7902"
            Case Else
                NoPlanta = String.Empty
        End Select


    End Sub
    Private Sub GuardarCambiosItemsNulos()
        'Periodo = "012"
        'FiscalYear = "2001"
        With Costos.ItemsSinCosto
            Dim tablerow As InterfaseCostosData.ItemsSinCostoRow
            For Each tablerow In .Rows
                If tablerow.SAP_ACTUAL_COST > 0 Then
                    With Costos.SAP_INTERFASE_COSTOS
                        Dim NewDataRow As InterfaseCostosData.SAP_INTERFASE_COSTOSRow = .NewSAP_INTERFASE_COSTOSRow
                        With NewDataRow
                            .COSTO_ADUANAL_POR_UNIDAD = 0
                            .COSTO_FLETE_POR_UNIDAD = 0
                            .SAP_ACTUAL_COST = tablerow.SAP_ACTUAL_COST
                            .SAP_COST_DIFFERENCES = tablerow.SAP_ACTUAL_COST
                            .SAP_CURRENCY = InfoCache.CurrencyCode
                            .SAP_FISCAL_YEAR = CInt(FiscalYear.Text)
                            .SAP_ID_COMPANIA = InfoCache.ClaveCompania
                            .SAP_ID_MATERIAL = String.Empty
                            .SAP_ID_SISTEMA_LEGADO = "ODSCA"
                            .SAP_NUM_MATERIAL_LEGADO = tablerow.PRO_CLAVE
                            .SAP_PERIOD = Periodo
                            .SAP_PLANTA = NoPlanta
                            .SAP_PREVIOUS_COST = 0
                            .SAP_TEXTO_MATERIAL = tablerow.PRO_DESCRI
                            .SAP_UOM_BASE = tablerow.SAP_UOM_BASE
                            .TIPO_DE_PRODUCTO = "PI"
                            .TOTAL_COSTO_PRODUCT0 = tablerow.SAP_ACTUAL_COST

                        End With
                        .AddSAP_INTERFASE_COSTOSRow(NewDataRow)

                    End With
                End If
            Next
        End With
        Dim result1 As Integer = (New CostosDML).insertarNuevosCostos(Costos)
        If result1 = -1 Then
            MsgBox("Registros se guardaron exitosamente", MsgBoxStyle.Information And MsgBoxStyle.OkOnly, "Costos de productos nuevos")
        End If

    End Sub
    Private Sub ActualizarItemsCero()

    End Sub
#End Region

#Region "Grid"
    Private Sub ConfigurarGridNoExisten()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridDatos.Width - 48

        Dim PRO_CLAVEColumn As New DataGridViewTextBoxColumn()
        Dim PRO_DESCRIColumn As New DataGridViewTextBoxColumn()
        Dim SAP_ACTUAL_COSTColumn As New DataGridViewTextBoxColumn()
        Dim SAP_UOM_BASEColumn As New DataGridViewTextBoxColumn()

        With PRO_CLAVEColumn
            .HeaderText = "Codigo"
            .Name = "PRO_CLAVE"
            .DataPropertyName = "PRO_CLAVE"
            .Width = CInt(TotalGridWidth * 0.15)
            .DefaultCellStyle.SelectionBackColor = Color.Red
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .ReadOnly = True
        End With
        With PRO_DESCRIColumn
            .HeaderText = "Descripcion"
            .Name = "PRO_DESCRI"
            .DataPropertyName = "PRO_DESCRI"
            .Width = CInt(TotalGridWidth * 0.5)
            '.DefaultCellStyle.SelectionBackColor = Color.Red
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .ReadOnly = True
        End With
        With SAP_ACTUAL_COSTColumn
            .HeaderText = "SAP_ACTUAL_COST"
            .Name = "SAP_ACTUAL_COST"
            .DataPropertyName = "SAP_ACTUAL_COST"
            .Width = CInt(TotalGridWidth * 0.15)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,#.0000"
            .ReadOnly = False
        End With
        With SAP_UOM_BASEColumn
            .HeaderText = "Unidad Medida"
            .Name = "SAP_UOM_BASE"
            .DataPropertyName = "SAP_UOM_BASE"
            .Width = CInt(TotalGridWidth * 0.15)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = False
        End With

        With Me.GridDatos
            .Columns.Add(PRO_CLAVEColumn)
            .Columns.Add(PRO_DESCRIColumn)
            .Columns.Add(SAP_ACTUAL_COSTColumn)
            .Columns.Add(SAP_UOM_BASEColumn)

            .AutoSizeColumnsMode = _
                DataGridViewAutoSizeColumnsMode.AllCells

            .RowHeadersWidth = CInt(TotalGridWidth * 0.09)
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .ColumnHeadersDefaultCellStyle = columnHeaderStyle
            .ReadOnly = False
            .RowsDefaultCellStyle.BackColor = Color.PowderBlue
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
        End With

    End Sub
    Private Sub ConfigurarGridCostoCero()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridDatos.Width - 48

        Dim PRO_CLAVEColumn As New DataGridViewTextBoxColumn()
        Dim PRO_DESCRIColumn As New DataGridViewTextBoxColumn()
        Dim SAP_ACTUAL_COSTColumn As New DataGridViewTextBoxColumn()


        With PRO_CLAVEColumn
            .HeaderText = "Codigo"
            .Name = "SAP_NUM_MATERIAL_LEGADO"
            .DataPropertyName = "SAP_NUM_MATERIAL_LEGADO"
            .Width = CInt(TotalGridWidth * 0.15)
            .DefaultCellStyle.SelectionBackColor = Color.Red
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .ReadOnly = True
        End With
        With PRO_DESCRIColumn
            .HeaderText = "Descripcion"
            .Name = "SAP_TEXTO_MATERIAL"
            .DataPropertyName = "SAP_TEXTO_MATERIAL"
            .Width = CInt(TotalGridWidth * 0.5)
            '.DefaultCellStyle.SelectionBackColor = Color.Red
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .ReadOnly = True
        End With
        With SAP_ACTUAL_COSTColumn
            .HeaderText = "TOTAL_COSTO_PRODUCT0"
            .Name = "TOTAL_COSTO_PRODUCT0"
            .DataPropertyName = "TOTAL_COSTO_PRODUCT0"
            .Width = CInt(TotalGridWidth * 0.15)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,#.0000"
            .ReadOnly = False
        End With

        With Me.GridItemsCostoCero
            .Columns.Add(PRO_CLAVEColumn)
            .Columns.Add(PRO_DESCRIColumn)
            .Columns.Add(SAP_ACTUAL_COSTColumn)


            .AutoSizeColumnsMode = _
                DataGridViewAutoSizeColumnsMode.AllCells

            .RowHeadersWidth = CInt(TotalGridWidth * 0.09)
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .ColumnHeadersDefaultCellStyle = columnHeaderStyle
            .ReadOnly = False
            .RowsDefaultCellStyle.BackColor = Color.PowderBlue
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
        End With

    End Sub
#End Region
#Region "eventos"
    Private Sub CargarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CargarButton.Click
        'StatusLabelDisplay("Cargando datos")
        My.Application.DoEvents()

        Dim f1 As Date = New System.DateTime(CInt(FiscalYear.Text), Me.PeriodCB.SelectedIndex + 1, 1)
        Dim UltimoDiaMes As Integer = Date.DaysInMonth(CInt(FiscalYear.Text), Me.PeriodCB.SelectedIndex)

        Dim f2 As Date = New System.DateTime(CInt(FiscalYear.Text), Me.PeriodCB.SelectedIndex + 1, UltimoDiaMes)

        'Me.Periodo = Format(Me.FechaHasta.Value.Month, "000")
        'Me.FiscalYear = Me.FechaHasta.Value.Year

        'InfoCache.PeriodoActual = Format((Me.FechaDesde.Value.Month), "000")
        'InfoCache.FiscalYear = Me.FechaDesde.Value.Year
        Me.Cursor = Cursors.WaitCursor
        Me.Costos = (New CostosDML).ItemsSinCostoSelectData(f1, f2)

        Me.GridDatos.DataSource = Costos.ItemsSinCosto.DefaultView
        Me.GridItemsCostoCero.DataSource = Costos.ITEMSSINPRECIO.DefaultView

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarButton.Click
        Me.Close()
    End Sub

    Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarButton.Click
        GuardarCambiosItemsNulos()
        'ActualizarItemsCero()
    End Sub
#End Region



End Class
