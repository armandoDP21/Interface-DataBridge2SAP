Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Data.Common


Public Class MainForm 'PDCV
    Private ts As TimeSpan
    Private startDate As DateTime
    Private endDate As DateTime

    Private ModuloActual As String = "PDCV"
    Private UsuarioInfo As UsuariosData
    Private Maestro As OSDMaestroData
    Private MovimientosDeProductos As MovimientosDeProductosData
    Private MovimientoSegregado As ODS_MOVIMIENTOPRODUCTO_SEGREGADO
    Private SapGLInterfase As SAP_GLINTERFASEData
    Private MovimientosTotalDeProductos As OSDMaestroData

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        'RestoreWindowSettings()
        InfoCache.ConnectionString = "Data Source=ODSCA;User Id=ODSCA;Password=PRO56KAL"

        If DisplayLoginForm() = Windows.Forms.DialogResult.OK Then
            My.Application.DoEvents()
            CargarDatosUsuario()
            Me.FechaDesde.Value = "25/11/2011"
            Me.FechaHasta.Value = "30/11/2011"

            'With New DateFormulas
            '    Me.FechaDesde.Value = .PreviousDayOfWeek(Date.Today.AddDays(-7), DayOfWeek.Tuesday)
            'End With
            'Me.FechaHasta.Value = Me.FechaDesde.Value.AddDays(6)
        Else
            Me.Close()
        End If
    End Sub
#Region " Save and restoring setting "

    Private Sub RestoreWindowSettings()
        Try

            'InfoCache.UId = My.Settings.UserName
            'InfoCache.ServerHost = My.Settings.ServerHost
            'InfoCache.QueryTimeOut = My.Settings.QueryTimeOut
            'InfoCache.PacketSize = My.Settings.PacketSize
            'InfoCache.Catalogo = My.Settings.Catalogo
            Dim bounds() As String = My.Settings.MainFormPlacement.Split(","c)
            If bounds.Length = 4 Then
                Dim boundsRect As Rectangle = New Rectangle( _
                 CInt(bounds(0)), CInt(bounds(1)), _
                 CInt(bounds(2)), CInt(bounds(3)))

                boundsRect = Rectangle.Intersect(Screen.PrimaryScreen.WorkingArea, boundsRect)
                If boundsRect.Width > 0 And boundsRect.Height > 0 Then
                    ' set the window location and size
                    Me.StartPosition = FormStartPosition.Manual
                    Me.SetBounds(boundsRect.Left, boundsRect.Top, _
                     boundsRect.Width, boundsRect.Height)
                End If
            End If
        Catch ex As Exception
            Me.StartPosition = FormStartPosition.WindowsDefaultLocation
        End Try
    End Sub
    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        MyBase.OnClosing(e)
        My.Settings.MainFormPlacement = String.Format("{0},{1},{2},{3}", _
                 Me.Bounds.X, Me.Bounds.Y, _
                 Me.Bounds.Width, Me.Bounds.Height)

        My.Settings.Save()
    End Sub



#End Region
#Region "Metodos"
    Sub CargarDatosUsuario()
        StatusLabelDisplay("Cargando datos del usuario")
        My.Application.DoEvents()

        UsuarioInfo = (New Usuarios).SelectDataUsuarios()
        If UsuarioInfo.DIC_USUARIOS.Count > 0 Then
            With UsuarioInfo.DIC_USUARIOS

                For Each tablerow As UsuariosData.DIC_USUARIOSRow In .Rows
                    Me.NombreUsuario.Text = tablerow.NOMBRE
                    Me.NombrePais.Text = tablerow.PAIS_NOMBRE
                    Me.Moneda.Text = tablerow.MONEDA
                    Me.ClaveCia_Label.Text = tablerow.CLAVE_CIA
                    InfoCache.NombreUsuario = tablerow.NOMBRE
                    InfoCache.PaisClave = tablerow.PAIS_CLAVE
                    'InfoCache.MonedaClave = tablerow.MONEDA_CLAVE
                    InfoCache.CurrencyCode = tablerow.MONEDA
                    InfoCache.ClaveCompania = tablerow.CLAVE_CIA

                Next

            End With
            Me.ProcesarButton.Enabled = True
            StatusLabelDisplay("Listo. Presione boton <Procesar> para iniciar.")
            My.Application.DoEvents()
        Else
            StatusLabelDisplay("Datos no encontrados")
            My.Application.DoEvents()
        End If

    End Sub
    Private Sub RetrieveData()
        InfoCache.SecuenciaInicia = CLng(Me.SecuenciaInicial.Text)
        startDate = DateTime.Parse(Date.Now.ToString)
        StatusLabelDisplay("Cargando datos")
        My.Application.DoEvents()
        InfoCache.FechaDesde = New System.DateTime(Me.FechaDesde.Value.Year, Me.FechaDesde.Value.Month, Me.FechaDesde.Value.Day)
        InfoCache.FechaHasta = New System.DateTime(Me.FechaHasta.Value.Year, Me.FechaHasta.Value.Month, Me.FechaHasta.Value.Day)
        InfoCache.PeriodoActual = Format((Me.FechaDesde.Value.Month), "000")
        InfoCache.FiscalYear = Me.FechaDesde.Value.Year
        Me.Cursor = Cursors.WaitCursor

        MovimientoSegregado = New ODS_MOVIMIENTOPRODUCTO_SEGREGADO
        CargarMaestro()
        CargarMovimientosDeProductos()

        T1()
        T3()
        T4()

        endDate = DateTime.Parse(Date.Now.ToString)
        ts = endDate.Subtract(startDate).Duration
        Seconds.Text = ts.Seconds.ToString

        'Dim result As Boolean = (New PDCVDatos).insertarBitacora("Costo de la venta", "A", SapGLInterfase.SAP_GL_INTERFASE.Rows.Count, ts.Seconds)
        'Dim result As Boolean = (New PDCVDatos).insertarBitacora("Costo de la venta", "A", 2000, ts.Seconds)

        Me.Cursor = Cursors.Default
        StatusLabelDisplay("Proceso completado: " & Date.Now.ToString)
        My.Application.DoEvents()
    End Sub
    Private Sub CargarMaestro()
        StatusLabelDisplay("Cargando calendario")
        My.Application.DoEvents()
        Maestro = (New PDCVDatos).MaestroSelectData()
        With VerDatosForm
            .Grid_Calendario.DataSource = Maestro.ODS_CALENDARIO
            .Grid_CATALOGO_MOVIMIENTOSPROD.DataSource = Maestro.ODS_CATALOGO_MOVIMIENTOSPROD
            .Grid_Sucursales.DataSource = Maestro.ODS_SUCURSALES
            .Grid_INTERFASECOSTOS.DataSource = Maestro.SAP_INTERFASE_COSTOS

        End With

    End Sub
    Private Sub CargarMovimientosDeProductos()
        StatusLabelDisplay("Cargando movimientos de productos")
        My.Application.DoEvents()
        MovimientosTotalDeProductos = (New PDCVDatos).MovimientoTotalDeProductosSelectData
        With VerDatosForm
            .Grid_MOVIMIENTOPRODUCTO.DataSource = MovimientosTotalDeProductos.VS_MOVIMIENTOSPROD
        End With
    End Sub


    Private Sub StatusLabelDisplay(ByVal ThisText As String)
        Me.StatusLabel.Text = ThisText
    End Sub
    Private Function DisplayLoginForm() As DialogResult
        Try

            Dim lForm As New LoginForm(ModuloActual)
            Dim result As Windows.Forms.DialogResult = lForm.ShowDialog()
            If Windows.Forms.DialogResult.OK Then

                InfoCache.UId = lForm.UusarioActual
                InfoCache.ServerHost = lForm.ServerHostActual

            End If
            Me.Refresh()
            Return result
        Catch ex As Exception
            Return Windows.Forms.DialogResult.Abort
            'Globales.DisplayError("The email message could not be created.", ex)
        End Try
    End Function
#End Region
#Region "Transformar"
    Private Sub T1()


        With Maestro.ODS_CATALOGO_MOVIMIENTOSPROD
            For Each tablerow As OSDMaestroData.ODS_CATALOGO_MOVIMIENTOSPRODRow In .Rows
                If tablerow.MOV_CLAVE = 16 Then
                    Continue For
                Else
                    Select Case InfoCache.PaisClave
                        Case 2
                            Select Case tablerow.MOV_CLAVE

                            End Select
                        Case 3
                        Case 4
                            Select Case tablerow.MOV_CLAVE
                                Case 11, 28, 29, 34, 69
                                    Continue For
                                Case Else
                            End Select
                        Case 5

                        Case 6, 7
                            Select Case tablerow.MOV_CLAVE
                                Case 11, 28, 29, 34, 37, 38, 81
                                    Continue For
                                Case Else
                            End Select

                    End Select
                End If

                'Select Case tablerow.MOV_CLAVE
                '    Case 2
                '        Select Case InfoCache.PaisClave
                '            Case 2, 5
                '            Case Else
                '                Continue For
                '        End Select
                '    Case 11
                '        If InfoCache.PaisClave <> 2 Then
                '            Continue For
                '        End If
                '    Case 16
                '        Continue For
                '    Case 11, 28, 29, 34, 69
                '        If InfoCache.PaisClave = 4 Then
                '            Continue For
                '        End If
                '    Case 11, 28, 29, 34, 37, 38, 69, 81
                '        If InfoCache.PaisClave = 6 Then
                '            Continue For
                '        End If
                '    Case 11, 28, 29, 34, 37, 38, 69, 81
                '        If InfoCache.PaisClave = 7 Then
                '            Continue For
                '        End If
                'End Select


                StatusLabelDisplay("Procesando movimientos de tipo:" & tablerow("MOV_CLAVE").ToString)
                My.Application.DoEvents()

                'If tablerow.MOV_CLAVE = 11 Or tablerow.MOV_CLAVE = 16 Then

                'Else
                'End If

                'If (tablerow.MOV_CLAVE = 2) And InfoCache.PaisClave <> 5 Then
                '    Continue For
                'End If
                'If (tablerow.MOV_CLAVE = 34) And InfoCache.PaisClave = 4 Then
                '    Continue For
                'End If

                StatusLabelDisplay("Procesando movimientos de tipo:" & tablerow("MOV_CLAVE").ToString)
                My.Application.DoEvents()

                Dim SegregarSucursalDR As Boolean = False
                Dim SegregarSucursalCR As Boolean = False

                Dim EsBeneficioDR As Boolean = False
                Dim EsBeneficioCR As Boolean = False

                Dim EsCentroFijoDR As Boolean = False
                Dim EsCentroFijoCR As Boolean = False
                Dim CentroFijoDR As String = String.Empty
                Dim CentroFijoCR As String = String.Empty

                Dim CCDR As String = String.Empty
                Dim CCCR As String = String.Empty

                If Not tablerow.IsCENTRO_COSTO_DRNull Then
                    CCDR = tablerow.CENTRO_COSTO_DR.Trim
                End If

                If Not tablerow.IsCENTRO_COSTO_CRNull Then
                    CCCR = tablerow.CENTRO_COSTO_CR.Trim
                End If

                If CCDR = String.Empty Then
                    SegregarSucursalDR = False
                    EsBeneficioDR = False
                    EsCentroFijoDR = False
                Else
                    Select Case CCDR
                        Case "B"
                            EsBeneficioDR = True
                            SegregarSucursalDR = True
                            EsCentroFijoDR = False
                        Case "C"
                            EsBeneficioDR = False
                            SegregarSucursalDR = True
                            EsCentroFijoDR = False
                        Case Else
                            EsBeneficioDR = False
                            EsCentroFijoDR = True
                            CentroFijoDR = tablerow.CENTRO_COSTO_DR
                            SegregarSucursalDR = False
                    End Select

                End If
                tablerow.SegregarSucursalDR = SegregarSucursalDR

                If CCCR = String.Empty Then
                    SegregarSucursalCR = False
                    EsBeneficioCR = False
                    EsCentroFijoCR = False
                Else
                    Select Case CCCR
                        Case "C"
                            EsBeneficioCR = False
                            SegregarSucursalCR = True
                            EsCentroFijoCR = False
                        Case "B"
                            EsBeneficioCR = True
                            SegregarSucursalCR = True
                            EsCentroFijoCR = False
                        Case Else
                            EsBeneficioCR = False
                            EsCentroFijoCR = True
                            CentroFijoCR = tablerow.CENTRO_COSTO_CR
                            SegregarSucursalCR = False
                    End Select
                End If
                tablerow.SegregarSucursalCR = SegregarSucursalCR

                T2( _
                        tablerow.MOV_CLAVE, _
                        SegregarSucursalDR, _
                        SegregarSucursalCR, _
                        EsBeneficioDR, _
                        EsBeneficioCR, _
                        EsCentroFijoDR, _
                        EsCentroFijoCR, _
                        CentroFijoDR, _
                        CentroFijoCR _
                   )

            Next
        End With
    End Sub
    Private Sub T2( _
                    ByVal MOV_Clave As Integer, _
                    ByVal SegregarSucursalDR As Boolean, _
                    ByVal SegregarSucursalCR As Boolean, _
                    ByVal BeneficiaDR As Boolean, _
                    ByVal BeneficiaCR As Boolean, _
                    ByVal FijarCentroDR As Boolean,
                    ByVal FijarCentroCR As Boolean, _
                    ByVal CentroFijoDR As String, _
                    ByVal CentroFijoCR As String _
                  )

        With MovimientosTotalDeProductos.VS_MOVIMIENTOSPROD
            For Each tablerow As OSDMaestroData.VS_MOVIMIENTOSPRODRow In .Rows
                If tablerow.MOV_CLAVE = MOV_Clave Then
                    With MovimientoSegregado.DesgloseMOV
                        Dim NewDataRow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.DesgloseMOVRow = .NewDesgloseMOVRow


                        With NewDataRow
                            .SUC_CLAVE = tablerow.SUC_CLAVE
                            .DMP_CANTID = tablerow.DMP_CANTID
                            .MOV_CLAVE = MOV_Clave
                            .PRO_CLAVE = tablerow.PRO_CLAVE
                            .TOTAL_COSTO_PRODUCT0 = GetCostoProducto(tablerow.PRO_CLAVE.Trim)
                        End With
                        .AddDesgloseMOVRow(NewDataRow)
                    End With
                    With VerDatosForm
                        .Grid_Calendario.DataSource = MovimientoSegregado.DesgloseMOV
                    End With


                    With MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO
                        Dim NewDataRow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.ODS_MOVIMIENTOPRODUCTO_SEGREGADORow = .NewODS_MOVIMIENTOPRODUCTO_SEGREGADORow

                        With NewDataRow
                            .MOV_CLAVE = MOV_Clave

                            If SegregarSucursalDR Then
                                .SAP_CENTROCOSTODR = GetCentroCosto(tablerow.SUC_CLAVE)
                            Else
                                .SAP_CENTROCOSTODR = Format(InfoCache.PaisClave, "000")
                            End If

                            If SegregarSucursalCR Then
                                .SAP_CENTROCOSTOCR = GetCentroCosto(tablerow.SUC_CLAVE)
                            Else
                                .SAP_CENTROCOSTOCR = Format(InfoCache.PaisClave, "000")
                            End If

                            If BeneficiaDR Then
                                .SAP_CENTROCOSTODR = GetCentroBeneficio(tablerow.SUC_CLAVE)
                                .ESBENEFICIODR = True
                            Else
                                .ESBENEFICIODR = False
                            End If

                            If BeneficiaCR Then
                                .SAP_CENTROCOSTOCR = GetCentroBeneficio(tablerow.SUC_CLAVE)
                                .ESBENEFICIOCR = True
                            Else
                                .ESBENEFICIOCR = False
                            End If

                            If FijarCentroDR Then
                                .SAP_CENTROCOSTODR = CentroFijoDR
                            End If
                            If FijarCentroCR Then
                                .SAP_CENTROCOSTOCR = CentroFijoCR
                            End If

                            'If SegregaSucursal Then
                            '    If Beneficia Then
                            '        .SAP_CENTROCOSTO = GetCentroBeneficio(tablerow.SUC_CLAVE)
                            '        .ESBENEFICIO = True
                            '    Else
                            '        .SAP_CENTROCOSTO = GetCentroCosto(tablerow.SUC_CLAVE)
                            '        .ESBENEFICIO = False
                            '    End If

                            'Else
                            '    .SAP_CENTROCOSTO = Format(InfoCache.PaisClave, "000")
                            '    .ESBENEFICIO = False
                            'End If
                            .TOTAL = GetCostoProducto(tablerow.PRO_CLAVE.Trim) * tablerow.DMP_CANTID

                        End With
                        .AddODS_MOVIMIENTOPRODUCTO_SEGREGADORow(NewDataRow)
                    End With
                End If
            Next
        End With
        With VerDatosForm.Grid_Segregado
            .DataSource = MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO.DefaultView

        End With
        VerDatosForm.TotalMovimientosLabel.Text = MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO.DefaultView.Count
    End Sub
    Private Sub T3()
        'Agrupar MovimientoSegregado by centro de costo an movclave


        Dim q = From p In MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO _
          Group p By p.SAP_CENTROCOSTODR, p.SAP_CENTROCOSTOCR, p.MOV_CLAVE, p.ESBENEFICIODR, p.ESBENEFICIOCR Into g = Group _
          Select New With {g, .monto = g.Sum(Function(p) p.TOTAL)}

        For Each Secuencia In q
            With MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO

                Dim NewDataRow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.ODS_MOVIMIENTOPRODUCTO_AGRUPADORow = .NewODS_MOVIMIENTOPRODUCTO_AGRUPADORow
                With NewDataRow
                    .MOV_CLAVE = Secuencia.g(0).MOV_CLAVE
                    .SAP_CENTROCOSTODR = Secuencia.g(0).SAP_CENTROCOSTODR
                    .SAP_CENTROCOSTOCR = Secuencia.g(0).SAP_CENTROCOSTOCR
                    .MONTO = Secuencia.monto

                    .ESBENEFICIODR = Secuencia.g(0).ESBENEFICIODR
                    .ESBENEFICIOCR = Secuencia.g(0).ESBENEFICIOCR
                End With

                .AddODS_MOVIMIENTOPRODUCTO_AGRUPADORow(NewDataRow)
            End With
        Next
        With VerDatosForm.Grid_Agrupado
            .DataSource = MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO.DefaultView
        End With

    End Sub
    Private Sub T4()
        SapGLInterfase = New SAP_GLINTERFASEData
        Dim NoSecuencia As Long = InfoCache.SecuenciaInicia
        With MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO
            For Each tablerow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.ODS_MOVIMIENTOPRODUCTO_AGRUPADORow In .Rows
                Dim CuentaDR As String = GetDebitoMovimiento(tablerow.MOV_CLAVE)
                Dim CuentaCR As String = GetCreditoMovimiento(tablerow.MOV_CLAVE)
                'Dim MontoString As String = Decimal.Round(tablerow.MONTO, 2)

                'definitiva()
                With SapGLInterfase.SAP_GL_INTERFASE

                    ' ''test
                    'With SapGLInterfase.SAP_GL_TEMP

                    Dim NewDataRowDB As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow

                    ''test
                    'Dim NewDataRowDB As SAP_GLINTERFASEData.SAP_GL_TEMPRow = .NewSAP_GL_TEMPRow

                    With NewDataRowDB
                        '.ESTATUS_SAP = "NEW"
                        .ESTATUS_SAP = "REV"
                        .DOCUMENT_DATE = Date.Today
                        .DOCUMENT_TYPE = "ZI"
                        .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(NoSecuencia, "000000")
                        'GetRefDocNo(tablerow.MOV_CLAVE)
                        .DOC_HEADER_TEXT = GetRefHeader(tablerow.MOV_CLAVE)
                        .SAP_COMPANY_CODE = InfoCache.ClaveCompania
                        .SAP_ACCOUNT = CuentaDR
                        Select Case CuentaDR.Substring(0, 1)
                            Case "1", "2", "3"
                                .SAP_PROFIT_CENTER = String.Empty
                                .SAP_COST_CENTER = String.Empty
                            Case "4", "5", "7"
                                .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTODR.ToString
                                .SAP_COST_CENTER = String.Empty
                            Case "6"
                                .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTODR.ToString
                                .SAP_PROFIT_CENTER = String.Empty
                        End Select

                        .SAP_SEGMENT = String.Empty
                        .ENTERED_DR = tablerow.MONTO
                        .ENTERED_CR = 0
                        .ASSIGNMENT = String.Empty
                        .LINE_TEXT = String.Empty
                        .SAP_REF_DOC_NUMBER = String.Empty
                        .SAP_REF_MESSAGE = String.Empty
                        '.ACCOUNTING_DATE = Date.Today
                        .ACCOUNTING_DATE = Me.FechaHasta.Value
                        .CURRENCY_CODE = InfoCache.CurrencyCode
                        .LEDGER_GROUP = String.Empty
                        .SAP_TAX_INDICATOR = String.Empty

                    End With
                    Dim NewDataRowCR As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
                    ' ''test
                    'Dim NewDataRowCR As SAP_GLINTERFASEData.SAP_GL_TEMPRow = .NewSAP_GL_TEMPRow

                    With NewDataRowCR
                        '.ESTATUS_SAP = "NEW"
                        .ESTATUS_SAP = "REV"
                        .DOCUMENT_DATE = Date.Today
                        .DOCUMENT_TYPE = "ZI"
                        .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(NoSecuencia, "000000")
                        .DOC_HEADER_TEXT = GetRefHeader(tablerow.MOV_CLAVE)
                        .SAP_COMPANY_CODE = InfoCache.ClaveCompania
                        .SAP_ACCOUNT = CuentaCR
                        Select Case CuentaCR.Substring(0, 1)
                            Case "1", "2", "3"
                                .SAP_PROFIT_CENTER = String.Empty
                                .SAP_COST_CENTER = String.Empty
                            Case "4", "5", "7"
                                .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTOCR.ToString
                                .SAP_COST_CENTER = String.Empty
                            Case "6"
                                .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTOCR.ToString
                                .SAP_PROFIT_CENTER = String.Empty
                        End Select

                        .SAP_SEGMENT = String.Empty
                        .ENTERED_DR = 0
                        .ENTERED_CR = tablerow.MONTO
                        .ASSIGNMENT = String.Empty
                        .LINE_TEXT = String.Empty
                        .SAP_REF_DOC_NUMBER = String.Empty
                        .SAP_REF_MESSAGE = String.Empty
                        '.ACCOUNTING_DATE = Date.Today
                        .ACCOUNTING_DATE = Me.FechaHasta.Value
                        .CURRENCY_CODE = InfoCache.CurrencyCode
                        .LEDGER_GROUP = String.Empty
                        .SAP_TAX_INDICATOR = String.Empty

                    End With
                    Try
                        .AddSAP_GL_INTERFASERow(NewDataRowDB)
                        .AddSAP_GL_INTERFASERow(NewDataRowCR)

                        '.AddSAP_GL_TEMPRow(NewDataRowDB)
                        '.AddSAP_GL_TEMPRow(NewDataRowCR)

                        .AcceptChanges()
                    Catch ex As Exception

                    End Try


                End With
                NoSecuencia += 1
            Next
        End With


        'definitiva

        With VerDatosForm.GRID_SAP_GL_INTERFASE
            .DataSource = SapGLInterfase.SAP_GL_INTERFASE
        End With
        InfoCache.SecuenciaTermina = NoSecuencia

        'Dim result As Boolean = (New PDCVDatos).insertarEnGL(SapGLInterfase.SAP_GL_INTERFASE)

        With SapGLInterfase.SAP_GL_INTERFASE
            For Each tablerow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow In .Rows
                Dim result As Boolean = (New PDCVDatos).insert(tablerow)
            Next
        End With
        Me.SecuenciaInicial.Text = (NoSecuencia + 1).ToString


        ''test
        'With VerDatosForm.GRID_SAP_GL_INTERFASE
        '    .DataSource = SapGLInterfase.SAP_GL_TEMP
        'End With
        'With SapGLInterfase.SAP_GL_TEMP
        '    For Each tablerow As SAP_GLINTERFASEData.SAP_GL_TEMPRow In .Rows
        '        Dim result As Boolean = (New PDCVDatos).insertInTemp(tablerow)
        '    Next
        'End With
    End Sub
#End Region
#Region "LookUp"
    Private Function GetPeriodo() As String
        Dim ComposicionPeriodo As String = String.Empty
        Dim fechas = From micalendario In Maestro.ODS_CALENDARIO
           Where micalendario.CFE_FECHA = FechaHasta.Value.Date

        For Each movimiento In fechas
            ComposicionPeriodo = "A" & _
                Format(movimiento.CFE_EJERCICIO, "0000").Remove(0, 2) & _
                "P" & _
                Format(movimiento.CFE_PERIODO, "00") & _
                "S" & _
                Format(movimiento.CFE_SEMANA, "0")
        Next
        Return ComposicionPeriodo


    End Function
    Private Function GetCostoProducto(ByVal PRO_CLAVE As String) As Decimal
        Dim TotalImpuesto As Double = 0
        Dim Costo As Decimal
        With Maestro.SAP_INTERFASE_COSTOS
            For Each tableRow As OSDMaestroData.SAP_INTERFASE_COSTOSRow In .Rows
                If tableRow.SAP_NUM_MATERIAL_LEGADO.Trim = PRO_CLAVE Then
                    'If Not tableRow.IsTOTAL_COSTO_PRODUCT0Null Then
                    Costo = tableRow.TOTAL_COSTO_PRODUCT0
                    'End If
                    Exit For
                End If
            Next
        End With
        If InfoCache.PaisClave = 3 Then
            TotalImpuesto = GetImpuesto(PRO_CLAVE)
        End If
        Dim CostoPrevio As Decimal = Costo * (1 + TotalImpuesto)


        Return CostoPrevio
    End Function
    Private Function GetCentroBeneficio(ByVal SUC_CLAVE As Long) As String
        Dim Temporal As String = String.Empty
        With Maestro.ODS_SUCURSALES
            For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
                If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
                    Temporal = tableRow.SAP_CENTROBENEFICIO
                    Exit For
                End If
            Next

        End With
        Return Temporal
    End Function
    Private Function GetCentroCosto(ByVal SUC_CLAVE As Long) As String
        Dim Temporal As String = String.Empty
        With Maestro.ODS_SUCURSALES
            For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
                If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
                    Temporal = tableRow.SAP_CENTROCOSTO
                    Exit For
                End If
            Next

        End With
        Return Temporal
    End Function
    Private Function GetDebitoMovimiento(ByVal MOV_CLAVE As Long) As String
        Dim NumeroCuenta As String = String.Empty
        Dim movimientos = From catalogo In Maestro.ODS_CATALOGO_MOVIMIENTOSPROD _
           Where catalogo.MOV_CLAVE = MOV_CLAVE
        For Each movimiento In movimientos
            NumeroCuenta = movimiento.CUENTA_CONTABLE_DR
        Next
        Return NumeroCuenta
    End Function
    Private Function GetImpuesto(ByVal Prod_Clave As String) As Double
        Dim P100Impuesto As Double = 0

        Dim Impuestos = From catalogo In Maestro.ODS_TMF_PRODUCTO _
           Where catalogo.PRO_CLAVE = Prod_Clave

        For Each Impuesto In Impuestos
            If Impuesto.PRO_IVASN.Trim = "S" Then
                P100Impuesto = Impuesto.PRO_IVADET
            Else
                P100Impuesto = 0
            End If
        Next
        Return P100Impuesto
    End Function
    Private Function GetCreditoMovimiento(ByVal MOV_CLAVE As Long) As String
        Dim NumeroCuenta As String = String.Empty
        Dim movimientos = From catalogo In Maestro.ODS_CATALOGO_MOVIMIENTOSPROD _
           Where catalogo.MOV_CLAVE = MOV_CLAVE
        For Each movimiento In movimientos
            NumeroCuenta = movimiento.CUENTA_CONTABLE_CR
        Next
        Return NumeroCuenta
    End Function
    Private Function GetRefDocNo(ByVal MOV_CLAVE As Long) As String
        Dim Cadena As New StringBuilder
        Cadena.Append(GetNombrePolizaCorto(MOV_CLAVE)).Append(GetPeriodo)

        Return Cadena.ToString

    End Function
    Private Function GetRefHeader(ByVal MOV_CLAVE As Long) As String
        Dim Cadena As New StringBuilder
        Cadena.Append(GetNombrePolizaLargo(MOV_CLAVE)).Append(GetPeriodo)

        Return Cadena.ToString

    End Function
    Private Function GetNombrePolizaCorto(ByVal MOV_CLAVE As Long) As String
        Dim nombreCortoPoliza As String = String.Empty
        Dim movimientos = From catalogo In Maestro.ODS_CATALOGO_MOVIMIENTOSPROD _
           Where catalogo.MOV_CLAVE = MOV_CLAVE
        For Each movimiento In movimientos
            If movimiento.IsNOMBRE_POLIZA_CORTONull Then
                nombreCortoPoliza = String.Empty
            Else
                nombreCortoPoliza = movimiento.NOMBRE_POLIZA_CORTO
            End If

        Next
        Return nombreCortoPoliza
    End Function
    Private Function GetNombrePolizaLargo(ByVal MOV_CLAVE As Long) As String
        Dim nombreCortoPoliza As String = String.Empty
        Dim movimientos = From catalogo In Maestro.ODS_CATALOGO_MOVIMIENTOSPROD _
           Where catalogo.MOV_CLAVE = MOV_CLAVE
        For Each movimiento In movimientos
            nombreCortoPoliza = movimiento.NOMBRE_POLIZA
        Next
        Return nombreCortoPoliza
    End Function

#End Region
#Region "Save"
    Private Sub Save()
        Dim SelectString As New StringBuilder
        SelectString.Append("SELECT ")
        SelectString.Append("ESTATUS_SAP, ")
        SelectString.Append("DOCUMENT_DATE, ")
        SelectString.Append("DOCUMENT_TYPE, ")
        SelectString.Append("REF_DOCUMENT_NUMBER, ")
        SelectString.Append("DOC_HEADER_TEXT, ")
        SelectString.Append("SAP_COMPANY_CODE, ")
        SelectString.Append("SAP_ACCOUNT, ")
        SelectString.Append("SAP_COST_CENTER, ")
        SelectString.Append("SAP_PROFIT_CENTER, ")
        SelectString.Append("SAP_SEGMENT, ")
        SelectString.Append("ENTERED_DR, ")
        SelectString.Append("ENTERED_CR, ")
        SelectString.Append("ASSIGNMENT, ")
        SelectString.Append("LINE_TEXT, ")
        SelectString.Append("SAP_REF_DOC_NUMBER, ")
        SelectString.Append("SAP_REF_MESSAGE, ")
        SelectString.Append("ACCOUNTING_DATE, ")
        SelectString.Append("CURRENCY_CODE, ")
        SelectString.Append("LEDGER_GROUP, ")
        SelectString.Append("SAP_TAX_INDICATOR ")
        SelectString.Append("FROM PRODODS.SAP_GL_INTERFASE ")
        SelectString.Append("WHERE ")
        SelectString.Append("ESTATUS_SAP = 'A' ")

        Dim conn As OracleConnection = New OracleConnection
        Dim productsDataSet As DataSet
        conn.ConnectionString = InfoCache.ConnectionString
        conn.Open()
        Dim cmd As New OracleCommand
        cmd.Connection = conn

        Dim MaestroAdapter As OracleDataAdapter = New OracleDataAdapter(cmd)
        Dim MaestroDataSet As New OSDMaestroData

        'Try
        cmd.CommandText = SelectString.ToString
        cmd.CommandType = CommandType.Text

        'Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        'p1.Value = InfoCache.PaisClave
        'cmd.Parameters.Add(p1)

        'Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        'cmd.Parameters.Add(p2)
        Try
            MaestroAdapter.Fill(MaestroDataSet, "SAP_GL_INTERFASE")
            'With VerDatosForm.GRID_SAP_GL_INTERFASE
            '    .DataSource = SapGLInterfase.SAP_GL_INTERFASE
            'End With
            productsDataSet = SapGLInterfase.SAP_GL_INTERFASE.DataSet 'New DataSet("productsDataSet")
            VerDatosForm.GRID_SAP_GL_INTERFASE.DataSource = productsDataSet



            'productsDataSet.Merge(SapGLInterfase.SAP_GL_INTERFASE.DataSet, True)
            MaestroAdapter.Update(productsDataSet, "SAP_GL_INTERFASE")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        cmd.Dispose()
        conn.Close()


    End Sub

#End Region

#Region "Eventos"
    Private Sub ProcesarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcesarButton.Click
        RetrieveData()
        'Me.Close()
    End Sub

    Private Sub VerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerButton.Click
        VerDatosForm.ShowDialog()
        'With New VerDatosForm
        '    .ShowDialog()
        'End With
    End Sub
#End Region


End Class 'PDCV