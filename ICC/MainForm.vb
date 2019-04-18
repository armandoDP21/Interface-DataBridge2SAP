Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Data.Common


Public Class MainForm
    Private ModuloActual As String = "ICC"
    Private MovimientoSegregado As ODS_MOVIMIENTOPRODUCTO_SEGREGADO
    'Private Movimientos As MovimientosData
    Private MovimientosDeProductos As MovimientosDeProductosData
    Private Maestro As OSDMaestroData
    Private UsuarioInfo As UsuariosData
    Private ImpuestosCarga As ImpuestosProductoData
    Private CatalogoPolizas As VS_CATALOGOPOLIZASData
    Private Liquidaciones As LiquidacionesData

    Private Procesados As ProcesadasData
    Private SapGLInterfase As SAP_GLINTERFASEData
    Private NoSecuencia As Long
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        Procesados = New ProcesadasData
        'RestoreWindowSettings()
        InfoCache.ConnectionString = "Data Source=ODSCA;User Id=ODSCA;Password=PRO56KAL"

        If DisplayLoginForm() = Windows.Forms.DialogResult.OK Then
            My.Application.DoEvents()
            CargarDatosUsuario()
            Me.FechaDesde.Value = "25/11/2011"
            Me.FechaHasta.Value = "30/11/2011"
            'With New DateFormulas
            '    Me.FechaDesde.Value = .PreviousDayOfWeek(Date.Today.AddDays(-7), DayOfWeek.Tuesday)
            '    'Me.FechaHasta.Value = .PreviousDayOfWeek(Date.Today, DayOfWeek.Tuesday)

            'End With
            'Me.FechaHasta.Value = Me.FechaDesde.Value.AddDays(6)

        Else
            Me.Close()
        End If

    End Sub
#Region " Save and restoring setting "

    'Private Sub RestoreWindowSettings()
    '    Try

    '        'InfoCache.UId = My.Settings.UserName
    '        'InfoCache.ServerHost = My.Settings.ServerHost
    '        'InfoCache.QueryTimeOut = My.Settings.QueryTimeOut
    '        'InfoCache.PacketSize = My.Settings.PacketSize
    '        'InfoCache.Catalogo = My.Settings.Catalogo
    '        Dim bounds() As String = My.Settings.MainFormPlacement.Split(","c)
    '        If bounds.Length = 4 Then
    '            Dim boundsRect As Rectangle = New Rectangle( _
    '             CInt(bounds(0)), CInt(bounds(1)), _
    '             CInt(bounds(2)), CInt(bounds(3)))

    '            boundsRect = Rectangle.Intersect(Screen.PrimaryScreen.WorkingArea, boundsRect)
    '            If boundsRect.Width > 0 And boundsRect.Height > 0 Then
    '                ' set the window location and size
    '                Me.StartPosition = FormStartPosition.Manual
    '                Me.SetBounds(boundsRect.Left, boundsRect.Top, _
    '                 boundsRect.Width, boundsRect.Height)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Me.StartPosition = FormStartPosition.WindowsDefaultLocation
    '    End Try
    'End Sub
    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        'MyBase.OnClosing(e)
        'My.Settings.MainFormPlacement = String.Format("{0},{1},{2},{3}", _
        '         Me.Bounds.X, Me.Bounds.Y, _
        '         Me.Bounds.Width, Me.Bounds.Height)

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
                    InfoCache.PaisClave = tablerow.PAIS_CLAVE
                    'InfoCache.MonedaClave = tablerow.MONEDA
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

        StatusLabelDisplay("Cargando datos")
        My.Application.DoEvents()
        InfoCache.FechaDesde = New System.DateTime(Me.FechaDesde.Value.Year, Me.FechaDesde.Value.Month, Me.FechaDesde.Value.Day)
        InfoCache.FechaHasta = New System.DateTime(Me.FechaHasta.Value.Year, Me.FechaHasta.Value.Month, Me.FechaHasta.Value.Day)
        InfoCache.PeriodoActual = Format((Me.FechaDesde.Value.Month), "000")
        InfoCache.FiscalYear = Me.FechaDesde.Value.Year
        Me.Cursor = Cursors.WaitCursor
        MovimientoSegregado = New ODS_MOVIMIENTOPRODUCTO_SEGREGADO
        CargarSecuenciaPais()
        CargarMaestro()
        CargarImpuestosProducto()
        CargarCatalogoPolizas()
        CargarLiquidaciones()
        CargarMovimientosProducto()

        T1()

        T4()


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
            '.Grid_CATALOGO_MOVIMIENTOSPROD.DataSource = Maestro.ODS_CATALOGO_MOVIMIENTOSPROD
            .Grid_Sucursales.DataSource = Maestro.ODS_SUCURSALES
            '.Grid_INTERFASECOSTOS.DataSource = Maestro.SAP_INTERFASE_COSTOS

        End With

    End Sub
    Private Sub CargarSecuenciaPais()
        NoSecuencia = CLng(Me.SecuenciaInicial.Text)
        'Dim Secuencia As OSDMaestroData
        'StatusLabelDisplay("Cargando secuencia del pais")
        'My.Application.DoEvents()
        'Secuencia = (New PDCVDatos).SecuenciaSelectData()
        'InfoCache.Secuencia = Secuencia.VS_SECUENCIAPAIS(0).SECUENCIA
    End Sub
    Private Sub CargarLiquidaciones()
        StatusLabelDisplay("Cargando  liquidaciones")
        My.Application.DoEvents()
        Liquidaciones = (New PDCVDatos).LiquidacionesSelectData()

        With VerDatosForm.Grid_Liquidaciones
            .DataSource = Liquidaciones.VS_ODS_LIQUIDACIONES
        End With

    End Sub
    Private Sub CargarCatalogoPolizas()
        StatusLabelDisplay("Cargando  Catalogo de Polizas")
        My.Application.DoEvents()
        CatalogoPolizas = (New PDCVDatos).CatalogoPolizasSelectData()
        If CatalogoPolizas.VS_CATALOGOPOLIZAS.Rows.Count > 0 Then
            With VerDatosForm
                .Grid_CatalogoPolizas.DataSource = CatalogoPolizas.VS_CATALOGOPOLIZAS
                .Grid_PolizasDetalles.DataSource = CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            End With
        End If
    End Sub
    Private Sub CargarImpuestosProducto()
        StatusLabelDisplay("Cargando ImpuestosCarga")
        My.Application.DoEvents()
        ImpuestosCarga = (New PDCVDatos).TMFProductoSelectData()
        If ImpuestosCarga.VS_ODSTMFPRODUCTO.Rows.Count > 0 Then
            With VerDatosForm.Grid_TMFProducto
                .DataSource = ImpuestosCarga.VS_ODSTMFPRODUCTO
            End With
        End If
    End Sub
    Sub CargarMovimientosProducto()
        StatusLabelDisplay("Cargando  movimientos de productos")
        My.Application.DoEvents()
        MovimientosDeProductos = (New PDCVDatos).MovimientosSelectData()
        With VerDatosForm
            .Grid_MOVIMIENTOPRODUCTO.DataSource = MovimientosDeProductos.VS_CARGA
            .Grid_TRADEPROMOTIONS.DataSource = MovimientosDeProductos.VS_TRADEPROMOTIONS
            .Grid_VS_CAMSVENTAS.DataSource = MovimientosDeProductos.VS_CAMSVENTAS
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
        Me.ProcesarButton.Enabled = False

        With CatalogoPolizas.VS_CATALOGOPOLIZAS
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow In .Rows
                NoSecuencia += 1
                StatusLabelDisplay("Procesando " & tableRow.DESCRIPCION)
                My.Application.DoEvents()

                'Select tableRow.POL_CLAVE

                '    Case 70

                Select Case tableRow.TIPO
                    Case "D"
                        Select Case InfoCache.PaisClave
                            Case 3
                                T2OtrosSumarizado(tableRow)
                                'T2VentasHonduras(tableRow)
                            Case 4
                                T2VentasDevolucionesPanama(tableRow)
                            Case 6
                                T2DevolucionesCostaRica(tableRow)
                            Case 7
                                T2DevolucionesQuaker(tableRow)
                            Case Else
                                T2VentasCAMN(tableRow)
                        End Select

                    Case "V"

                        Select Case InfoCache.PaisClave
                            Case 2
                                T2DVentasSumarizado(tableRow)
                            Case 3
                                T2VentasHonduras(tableRow)
                            Case 4
                                T2VentasDevolucionesPanama(tableRow)
                            Case 6
                                T2VentasCostaRica(tableRow)
                            Case 7
                                T2VentasQuaker(tableRow)
                            Case Else
                                T2VentasCAMN(tableRow)
                        End Select

                    Case "O"
                        If InfoCache.PaisClave = 6 Or InfoCache.PaisClave = 7 Then
                            T2OtrosSabritasQuaker(tableRow)
                        Else
                            T2Otros(tableRow)
                        End If
                    Case "S"
                        If InfoCache.PaisClave = 3 And tableRow.POL_CLAVE = 21 Then
                            T2DevolucionesHonduras(tableRow)
                        ElseIf InfoCache.PaisClave = 3 And tableRow.POL_CLAVE = 27 Then
                            T2OtrosSumarizado(tableRow)
                        ElseIf InfoCache.PaisClave = 2 And tableRow.POL_CLAVE = 3 Then
                            T2DescuentosSumarizado(tableRow)
                        ElseIf InfoCache.PaisClave = 2 And tableRow.POL_CLAVE = 1 Then
                            T2DescuentosSumarizado(tableRow)

                        Else


                        End If

                    Case "B"
                        Select Case InfoCache.PaisClave
                            Case 4
                                T2BancosPanama(tableRow)
                            Case 6
                                T2BancosCostaRica(tableRow)
                            Case 7
                                T2BancosQuaker(tableRow)
                            Case Else
                                T2BancosCAMN(tableRow)
                        End Select
                    Case "N"
                        Select Case InfoCache.PaisClave
                            Case 4
                                T2NCPanama(tableRow)
                            Case 6
                                T2NCPanama(tableRow)
                        End Select

                    Case "Y"
                        Select Case InfoCache.PaisClave
                            Case 4
                                T2VendedoresDirectosPanama(tableRow)
                            Case 6
                                T2VendedoresDirectosCR(tableRow)
                        End Select
                End Select
                '    Case Else
                'End Select
            Next
        End With
        With VerDatosForm.Grid_PolizasC
            .DataSource = Procesados.PolizasC.DefaultView
        End With
    End Sub
    Private Sub T2VendedoresDirectosPanama(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim CCDistribuidores102 As Decimal
        Dim CCDistribuidores104 As Decimal
        Dim CCDistribuidores106 As Decimal
        Dim CCDistribuidores108 As Decimal
        Dim CCDistribuidores110 As Decimal
        Dim CCDistribuidores112 As Decimal
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE
                        Case 101 'VENTAS PANAMA
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VDPAVentas
                                    For Each tRow As MovimientosDeProductosData.VDPAVentasRow In .Rows
                                        CCDistribuidores102 = CCDistribuidores102 + tRow.MONTO
                                        Dim TotalImpuesto As Double = 0
                                        Dim MontoSucursal As Decimal = tRow.MONTO ' - MontoDevolucion(tRow.SUC_CLAVE)

                                        'MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        'MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    Next
                                End With

                            End If

                        Case 102 'cxc Distribuidores 

                            If CCDistribuidores102 > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = CCDistribuidores102

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = CCDistribuidores102
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If

                        Case 103 'DEVOLUCION BUEN ESTADO
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VDPADEVBE
                                    For Each tRow As MovimientosDeProductosData.VDPADEVBERow In .Rows
                                        CCDistribuidores104 = CCDistribuidores104 + tRow.DEVOLUCION
                                        Dim TotalImpuesto As Double = 0
                                        Dim MontoSucursal As Decimal = tRow.DEVOLUCION ' - MontoDevolucion(tRow.SUC_CLAVE)

                                        'MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        'MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    Next
                                End With

                            End If

                        Case 104 'CUENTAS POR COBRAR DISTRIBUIDORES

                            If CCDistribuidores104 > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = CCDistribuidores104

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = CCDistribuidores104
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If


                        Case 105 'DEVOLUCION MAL ESTADO
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VDPADEVME
                                    For Each tRow As MovimientosDeProductosData.VDPADEVMERow In .Rows
                                        CCDistribuidores106 = CCDistribuidores106 + tRow.DEVOLUCION
                                        Dim TotalImpuesto As Double = 0
                                        Dim MontoSucursal As Decimal = tRow.DEVOLUCION ' - MontoDevolucion(tRow.SUC_CLAVE)

                                        'MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        'MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    Next
                                End With

                            End If

                        Case 106 'CUENTAS POR COBRAR DISTRIBUIDORES

                            If CCDistribuidores106 > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = CCDistribuidores106

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = CCDistribuidores106
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 107   'Banco Nacional
                            With Liquidaciones.VDPADEPOSITOS

                            End With
                            Dim GTotal As Decimal = (From p In Liquidaciones.VDPADEPOSITOS Where p.TMD_CLAVE = 41 Select p.TOTAL).Sum
                            If GTotal > 0 Then
                                CCDistribuidores108 = GTotal
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 108 ' cxcDistriuidores banco nacional

                            If CCDistribuidores108 > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = CCDistribuidores108

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = CCDistribuidores108
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If


                        Case 109   'hsbc
                            Dim GTotal As Decimal = (From p In Liquidaciones.VDPADEPOSITOS Where p.TMD_CLAVE = 35 Select p.TOTAL).Sum
                            If GTotal > 0 Then
                                CCDistribuidores110 = GTotal
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 110 ' cxcDistriuidores banco nacional

                            If CCDistribuidores110 > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = CCDistribuidores110

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = CCDistribuidores110
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 111 'DESCUENTO ESPECIALES PROMO
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VDPADESCESPECIAL
                                    For Each tRow As MovimientosDeProductosData.VDPADESCESPECIALRow In .Rows
                                        CCDistribuidores112 = CCDistribuidores112 + tRow.TOTAL
                                        Dim TotalImpuesto As Double = 0
                                        Dim MontoSucursal As Decimal = tRow.TOTAL ' - MontoDevolucion(tRow.SUC_CLAVE)

                                        'MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        'MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    Next
                                End With

                            End If

                        Case 112 'CUENTAS POR COBRAR VENDEDORES DIRECTOS

                            If CCDistribuidores112 > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = CCDistribuidores112

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = CCDistribuidores112
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                    End Select

                End If
            Next
        End With
    End Sub

   

    Private Sub T2VendedoresDirectosCR(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)

    End Sub
    Private Sub T2VentasCAMN(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)


        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE
                        Case 10
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VS_CARGA
                                    For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
                                        Dim TotalImpuesto As Double = 0
                                        Dim MontoSucursal As Decimal = tRow.TOTAL - MontoDevolucion(tRow.SUC_CLAVE)

                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
                                GTotal = GTotal - MontoTotalDevolucion()

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = String.Empty
                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                        End With

                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If


                            End If
                        Case 16   'TRADEPROMOTIONS

                            If tableRow.NIVEL_AGRUPA = 1 Then


                                With MovimientosDeProductos.VS_TRADEPROMOTIONS
                                    For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
                                        Dim MontoSucursal As Decimal = tRow.TOTAL

                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = String.Empty

                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO

                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            End If

                    End Select
                End If
            Next
        End With
    End Sub
    Private Sub T2VentasDevolucionesPanama(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)



        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE
                        Case 200 'DEVOLUCION BUEN ESTADO CARTERA CENTRAL


                            If tableRow.NIVEL_AGRUPA = 1 Then
                                Dim Excluir As Long() = New Long() {11, 34}
                                Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESPANAMA _
                                Where Not Excluir.Contains(p.MOV_CLAVE) And p.CAR_CLAVE = "2200" _
                                Select p.DEVOLUCION, p.SUC_CLAVE

                                For Each secuencia In q
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.SUC_CLAVE, tableRow.CENTRO_FIJO)


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = secuencia.DEVOLUCION

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = secuencia.DEVOLUCION
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = secuencia.SUC_CLAVE
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                Next


                            Else
                                'no
                            End If
                        Case 300 ''DEVOLUCION BUEN ESTADO Descuentos  CARTERA CENTRAL


                            If tableRow.NIVEL_AGRUPA = 1 Then
                                Dim Excluir As Long() = New Long() {11, 34}
                                Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESPANAMA _
                                Where Not Excluir.Contains(p.MOV_CLAVE) And p.CAR_CLAVE = "2200" _
                                Select p.SUC_CLAVE, p.DESCUENTO

                                For Each secuencia In q
                                    If secuencia.DESCUENTO > 0 Then
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow

                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.SUC_CLAVE, tableRow.CENTRO_FIJO)
                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = secuencia.DESCUENTO

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = secuencia.DESCUENTO
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = secuencia.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If

                                Next



                                'With MovimientosDeProductos.VS_DEVOLUCIONESPANAMA
                                '    For Each tRow As MovimientosDeProductosData.VS_DEVOLUCIONESPANAMARow In .Rows
                                '        Dim Descontar As Decimal
                                '        If Not tRow.IsDESCUENTONull Then
                                '            Descontar = tRow.DESCUENTO
                                '        Else
                                '            Descontar = 0
                                '        End If

                                '        'If tRow.MOV_CLAVE = 27 And tRow.CAR_CLAVE = 2200 And Descontar > 0 Then
                                '        If (tRow.MOV_CLAVE <> 11 Or tRow.MOV_CLAVE <> 34) And tRow.CAR_CLAVE = 2200 And Descontar > 0 Then


                                '        End If
                                '    Next
                                'End With


                            Else
                                'no
                            End If
                        Case 400 ''DEVOLUCION BUEN ESTADO CUENTAS POR COBRAR  CARTERA CENTRAL

                            Dim Excluir As Long() = New Long() {11, 34}
                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESPANAMA Where (Not Excluir.Contains(p.MOV_CLAVE) And p.CAR_CLAVE = "2200") Select p.DEVOLUCION - p.DESCUENTO).Sum

                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = String.Empty
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 500 'DEVOLUCION MAL ESTADO  CARTERA CENTRAL
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VS_DEVOLUCIONESPANAMA
                                    For Each tRow As MovimientosDeProductosData.VS_DEVOLUCIONESPANAMARow In .Rows
                                        If (tRow.MOV_CLAVE = 11 Or tRow.MOV_CLAVE = 34) And tRow.CAR_CLAVE = 2200 And tRow.DEVOLUCION > 0 Then
                                            With Procesados.PolizasC
                                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                                With NewDataRow
                                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                    If tableRow.IsCUENTA_DRNull Then
                                                        .CUENTADR = String.Empty
                                                        .MONTODR = 0
                                                        .CUENTACR = tableRow.CUENTA_CR
                                                        .MONTOCR = tRow.DEVOLUCION

                                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                                        .CUENTADR = tableRow.CUENTA_DR
                                                        .MONTODR = tRow.DEVOLUCION
                                                        .CUENTACR = String.Empty
                                                        .MONTOCR = 0

                                                    End If
                                                    .Secuencia = NoSecuencia
                                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                    .CENTRO = tableRow.CENTRO
                                                    .SUC_CLAVE = tRow.SUC_CLAVE
                                                End With
                                                .AddPolizasCRow(NewDataRow)
                                            End With
                                        End If

                                    Next
                                End With


                            Else

                            End If
                        Case 600 'DEVOLUCION MAL ESTADO Descuentos  CARTERA CENTRAL

                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VS_DEVOLUCIONESPANAMA
                                    For Each tRow As MovimientosDeProductosData.VS_DEVOLUCIONESPANAMARow In .Rows
                                        Dim Descontar As Decimal
                                        If Not tRow.IsDESCUENTONull Then
                                            Descontar = tRow.DESCUENTO
                                        Else
                                            Descontar = 0
                                        End If
                                        If (tRow.MOV_CLAVE = 11 Or tRow.MOV_CLAVE = 34) And tRow.CAR_CLAVE = 2200 And Descontar > 0 Then
                                            With Procesados.PolizasC
                                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                                With NewDataRow

                                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)
                                                    If tableRow.IsCUENTA_DRNull Then
                                                        .CUENTADR = String.Empty
                                                        .MONTODR = 0
                                                        .CUENTACR = tableRow.CUENTA_CR
                                                        .MONTOCR = tRow.DESCUENTO

                                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                                        .CUENTADR = tableRow.CUENTA_DR
                                                        .MONTODR = tRow.DESCUENTO
                                                        .CUENTACR = String.Empty
                                                        .MONTOCR = 0

                                                    End If
                                                    .Secuencia = NoSecuencia
                                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                    .CENTRO = tableRow.CENTRO
                                                    .SUC_CLAVE = tRow.SUC_CLAVE
                                                End With
                                                .AddPolizasCRow(NewDataRow)
                                            End With
                                        End If

                                    Next
                                End With


                            Else

                            End If
                        Case 700 'DEVOLUCION MAL ESTADO CUENTAS POR COBRAR  CARTERA CENTRAL

                            Dim Neto As Decimal
                            With MovimientosDeProductos.VS_DEVOLUCIONESPANAMA
                                For Each tRow As MovimientosDeProductosData.VS_DEVOLUCIONESPANAMARow In .Rows
                                    Dim Descontar As Decimal
                                    If Not tRow.IsDESCUENTONull Then
                                        Descontar = tRow.DESCUENTO
                                    Else
                                        Descontar = 0
                                    End If

                                    If (tRow.MOV_CLAVE = 11 Or tRow.MOV_CLAVE = 34) And tRow.CAR_CLAVE = 2200 Then
                                        Neto = Neto + (tRow.DEVOLUCION - Descontar)
                                    End If

                                Next
                            End With
                            If Neto > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty
                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Neto

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Neto
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = String.Empty
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 201 'DEVOLUCION BUEN ESTADO CARTERA CENTRAL

                            If tableRow.NIVEL_AGRUPA = 1 Then
                                Dim Excluir As Long() = New Long() {11, 34}
                                Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESPANAMA _
                                Where Not Excluir.Contains(p.MOV_CLAVE) And p.CAR_CLAVE = "2100" _
                                Select p.DEVOLUCION, p.SUC_CLAVE

                                For Each secuencia In q
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.SUC_CLAVE, tableRow.CENTRO_FIJO)


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = secuencia.DEVOLUCION

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = secuencia.DEVOLUCION
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = secuencia.SUC_CLAVE
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                Next

                            Else

                            End If
                        Case 301 'DEVOLUCION BUEN ESTADO Descuentos  CARTERA CENTRAL


                            If tableRow.NIVEL_AGRUPA = 1 Then
                                Dim Excluir As Long() = New Long() {11, 34}
                                Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESPANAMA _
                                Where Not Excluir.Contains(p.MOV_CLAVE) And p.CAR_CLAVE = "2100" _
                                Select p.SUC_CLAVE, p.DESCUENTO

                                For Each secuencia In q
                                    If secuencia.DESCUENTO > 0 Then
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow

                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.SUC_CLAVE, tableRow.CENTRO_FIJO)
                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = secuencia.DESCUENTO

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = secuencia.DESCUENTO
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = secuencia.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If

                                Next



                            Else

                            End If
                        Case 401 'DEVOLUCION BUEN ESTADO CUENTAS POR COBRAR  CARTERA CENTRAL
                            Dim Excluir As Long() = New Long() {11, 34}
                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESPANAMA Where (Not Excluir.Contains(p.MOV_CLAVE) And p.CAR_CLAVE = "2100") Select p.DEVOLUCION - p.DESCUENTO).Sum

                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = String.Empty
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 501 'DEVOLUCION MAL ESTADO  CARTERA CENTRAL
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VS_DEVOLUCIONESPANAMA
                                    For Each tRow As MovimientosDeProductosData.VS_DEVOLUCIONESPANAMARow In .Rows
                                        If (tRow.MOV_CLAVE = 11 Or tRow.MOV_CLAVE = 34) And tRow.CAR_CLAVE = 2100 And tRow.DEVOLUCION > 0 Then
                                            With Procesados.PolizasC
                                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                                With NewDataRow
                                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                    If tableRow.IsCUENTA_DRNull Then
                                                        .CUENTADR = String.Empty
                                                        .MONTODR = 0
                                                        .CUENTACR = tableRow.CUENTA_CR
                                                        .MONTOCR = tRow.DEVOLUCION

                                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                                        .CUENTADR = tableRow.CUENTA_DR
                                                        .MONTODR = tRow.DEVOLUCION
                                                        .CUENTACR = String.Empty
                                                        .MONTOCR = 0

                                                    End If
                                                    .Secuencia = NoSecuencia
                                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                    .CENTRO = tableRow.CENTRO
                                                    .SUC_CLAVE = tRow.SUC_CLAVE
                                                End With
                                                .AddPolizasCRow(NewDataRow)
                                            End With
                                        End If

                                    Next
                                End With


                            Else

                            End If
                        Case 601 'DEVOLUCION MAL ESTADO Descuentos  CARTERA CENTRAL

                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VS_DEVOLUCIONESPANAMA
                                    For Each tRow As MovimientosDeProductosData.VS_DEVOLUCIONESPANAMARow In .Rows
                                        Dim Descontar As Decimal
                                        If Not tRow.IsDESCUENTONull Then
                                            Descontar = tRow.DESCUENTO
                                        Else
                                            Descontar = 0
                                        End If
                                        If (tRow.MOV_CLAVE = 11 Or tRow.MOV_CLAVE = 34) And tRow.CAR_CLAVE = 2100 And Descontar > 0 Then
                                            With Procesados.PolizasC
                                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                                With NewDataRow

                                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)
                                                    If tableRow.IsCUENTA_DRNull Then
                                                        .CUENTADR = String.Empty
                                                        .MONTODR = 0
                                                        .CUENTACR = tableRow.CUENTA_CR
                                                        .MONTOCR = tRow.DESCUENTO

                                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                                        .CUENTADR = tableRow.CUENTA_DR
                                                        .MONTODR = tRow.DESCUENTO
                                                        .CUENTACR = String.Empty
                                                        .MONTOCR = 0

                                                    End If
                                                    .Secuencia = NoSecuencia
                                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                    .CENTRO = tableRow.CENTRO
                                                    .SUC_CLAVE = tRow.SUC_CLAVE
                                                End With
                                                .AddPolizasCRow(NewDataRow)
                                            End With
                                        End If

                                    Next
                                End With


                            Else

                            End If
                        Case 701 'DEVOLUCION MAL ESTADO CUENTAS POR COBRAR  CARTERA CENTRAL

                            Dim Neto As Decimal
                            With MovimientosDeProductos.VS_DEVOLUCIONESPANAMA
                                For Each tRow As MovimientosDeProductosData.VS_DEVOLUCIONESPANAMARow In .Rows
                                    Dim Descontar As Decimal
                                    If Not tRow.IsDESCUENTONull Then
                                        Descontar = tRow.DESCUENTO
                                    Else
                                        Descontar = 0
                                    End If

                                    If (tRow.MOV_CLAVE = 11 Or tRow.MOV_CLAVE = 34) And tRow.CAR_CLAVE = 2100 Then
                                        Neto = Neto + (tRow.DEVOLUCION - Descontar)
                                    End If

                                Next
                            End With
                            If Neto > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty
                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Neto

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Neto
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = String.Empty
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If


                        Case 10000 'CUENTAS POR COBRAR LOCALES


                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 6205 Select p.TOTAL).Sum

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal '- CXCEmpleados()

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal '- CXCEmpleados()
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With





                        Case 11000 'Descuentos

                            Dim q = From p In MovimientosDeProductos.VS_CAMSVENTAS _
                            Where p.TMO_CLAVE = 2003 Or p.TMO_CLAVE = 2002 _
                              Group p By p.SUC_CLAVE Into g = Group _
                              Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}


                            For Each secuencia In q

                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)


                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = secuencia.GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = secuencia.GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = secuencia.g(0).SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            Next

                        Case 12000 'Diferencia en Precio VENTAS PANAMA 
                            Dim x As New Dictionary(Of Integer, Decimal)

                            x.Add(1, 0)
                            x.Add(2, 0)
                            x.Add(3, 0)
                            x.Add(4, 0)
                            x.Add(5, 0)
                            x.Add(6, 0)
                            x.Add(50, 0)

                            '

                            Dim q = From p In MovimientosDeProductos.VS_CAMSVENTAS _
                            Where p.TMO_CLAVE = 2003 Or p.TMO_CLAVE = 2002 Or p.TMO_CLAVE = 6204 Or p.TMO_CLAVE = 6205 _
                              Group p By p.SUC_CLAVE Into g = Group _
                              Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}

                            For Each secuencia In q
                                x(secuencia.g(0).SUC_CLAVE) = secuencia.GTotal

                            Next

                            'With MovimientosDeProductos.VS_CARGA
                            '    For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
                            '        Dim TotalActual As Decimal = x(tRow.SUC_CLAVE)
                            '        x(tRow.SUC_CLAVE) = TotalActual + tRow.TOTAL
                            '    Next
                            'End With

                            For Each sucursal In x
                                If sucursal.Value > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow


                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, sucursal.Key, tableRow.CENTRO_FIJO)
                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = sucursal.Value

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = sucursal.Value
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = sucursal.Key
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next


                        Case 13000  'Diferencia en Precio CUENTAS POR COBRAR EMPLEADOS
                            Dim GTtotal As Decimal
                            With MovimientosDeProductos.CXCEMPLEADOS
                                For Each tRow As MovimientosDeProductosData.CXCEMPLEADOSRow In .Rows
                                    If Not tRow.IsTOTALNull Then
                                        GTtotal = GTtotal + tRow.TOTAL
                                    End If

                                Next
                            End With
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTtotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTtotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If

                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With

                        Case 14000 'CUENTAS POR COBRAR centralizadas
                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 6204 Select p.TOTAL).Sum

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal '- CXCEmpleados()

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal ' - CXCEmpleados()
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With

                        Case 27
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VS_CAMSVENTAS
                                    For Each tRow As MovimientosDeProductosData.VS_CAMSVENTASRow In .Rows
                                        Dim TotalImpuesto As Double = 0
                                        Dim MontoSucursal As Decimal = tRow.TOTAL - MontoDevolucion(tRow.SUC_CLAVE)



                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
                                GTotal = GTotal - MontoTotalDevolucion()

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = String.Empty


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                        End With

                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If


                            End If

                        Case 27   'TRADEPROMOTIONS

                            If tableRow.NIVEL_AGRUPA = 1 Then


                                With MovimientosDeProductos.VS_TRADEPROMOTIONS
                                    For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
                                        Dim MontoSucursal As Decimal = tRow.TOTAL

                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = String.Empty


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO

                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            End If
                    End Select
                End If
            Next
        End With
    End Sub

    Private Sub T2VentasCostaRica(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim GTotalVentaConIva As Decimal
        Dim GTotalVentaSinIva As Decimal
        Dim IvaPorPagar As Decimal
        Dim GTotalDescuentos As Decimal
        Dim GTDescuentosSinIva As Decimal


        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE


                        Case 10 'Cuenta por cobrar Vendedores C.R. ;'NO

                            GTotalDescuentos = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2002 Select p.TOTAL).Sum
                            GTDescuentosSinIva = (From p In MovimientosDeProductos.DESCUENTOSSINIVACRQK Select p.DESCUENTO).Sum
                            GTotalVentaConIva = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
                            GTotalVentaSinIva = (From p In MovimientosDeProductos.VS_CARGASinIva Select p.TOTAL).Sum
                            'GTotalDeDescuentosSinIva = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVACR Select p.DESCUENTO).Sum
                            Dim DescuentoNeto As Decimal = GTotalDescuentos - GTDescuentosSinIva

                            IvaPorPagar = ((GTotalVentaConIva / 1.13) - DescuentoNeto) * 0.13
                            Dim GTotalTemporal = GTotalVentaConIva


                            'GTotalTemporal = GTotalTemporal * tableRow.MULTIPLICAR
                            'GTotalTemporal = GTotalTemporal / tableRow.DIVIDIR


                            'Dim IvaErrado As Decimal = GTotalVentaConIva - GTotalTemporal
                            'Dim VentaBruta As Decimal = GTotalVentaConIva - IvaErrado
                            'Dim VentasConDescuentoSinIva As Decimal = VentaBruta - GTotalDescuentos

                            'Dim GTotal As Decimal = (VentasConDescuentoSinIva * 1.13) '+ GTotalVentaSinIva
                            Dim GTotal As Decimal = (GTotalVentaConIva / 1.13) + GTotalVentaSinIva - GTotalDescuentos + IvaPorPagar



                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                    End With

                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If

                        Case 2002 'IVA POR PAGAR 'NO
                            'Dim IvaPorPagar As Decimal = MontoIvaPorPagarCR()

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = IvaPorPagar

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = IvaPorPagar
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    '.SUC_CLAVE = '50'
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With

                        Case 11000 'Descuento a Distribuidores 'OK
                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2002 Select p.TOTAL).Sum

                            'GTotal = GTotal * tableRow.MULTIPLICAR
                            'GTotal = GTotal / tableRow.DIVIDIR

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    '.SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tableRow.SUC_CLAVE, tableRow.CENTRO_FIJO)
                                    .SAP_CENTROCOSTO = GetSAPCentroBeneficio(50) 'hay que rehacer el query porque debe ser por sucursal

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With

                        Case 11001 'Diferencia en Precios 
                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2006 Select p.TOTAL).Sum

                            'GTotal = GTotal * tableRow.MULTIPLICAR
                            'GTotal = GTotal / tableRow.DIVIDIR

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    '.SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tableRow.SUC_CLAVE, tableRow.CENTRO_FIJO)
                                    .SAP_CENTROCOSTO = GetSAPCentroBeneficio(50) 'hay que rehacer el query porque debe ser por sucursal

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With





                        Case 14000 'Venta Bruta 'OK

                            Dim VentaBruta As Decimal = (GTotalVentaConIva / 1.13) + GTotalVentaSinIva
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow

                                    .SAP_CENTROCOSTO = GetSAPCentroBeneficio(50)

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = VentaBruta

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = VentaBruta
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    '.SUC_CLAVE = '50'
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With

                        Case 12000 'VENTAS
                            Dim x As New Dictionary(Of Integer, Decimal)

                            x.Add(1, 0)
                            x.Add(2, 0)
                            x.Add(3, 0)
                            x.Add(4, 0)
                            x.Add(5, 0)
                            x.Add(6, 0)
                            x.Add(50, 0)


                            Dim q = From p In MovimientosDeProductos.VS_CAMSVENTAS _
                            Where p.TMO_CLAVE = 2003 Or p.TMO_CLAVE = 2002 Or p.TMO_CLAVE = 6204 Or p.TMO_CLAVE = 6205 _
                              Group p By p.SUC_CLAVE Into g = Group _
                              Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}

                            For Each Secuencia In q
                                x(Secuencia.g(0).SUC_CLAVE) = Secuencia.GTotal

                            Next

                            With MovimientosDeProductos.VS_CARGA
                                For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
                                    Dim TotalActual As Decimal = x(tRow.SUC_CLAVE)
                                    x(tRow.SUC_CLAVE) = TotalActual + tRow.TOTAL
                                Next
                            End With

                            For Each sucursal In x
                                If sucursal.Value > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, sucursal.Key, tableRow.CENTRO_FIJO)


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = sucursal.Value

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = sucursal.Value
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = sucursal.Key
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next

                        Case 16   'TRADEPROMOTIONS

                            If tableRow.NIVEL_AGRUPA = 1 Then


                                With MovimientosDeProductos.VS_TRADEPROMOTIONS
                                    For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
                                        Dim GTotal As Decimal = tRow.TOTAL

                                        GTotal = GTotal * tableRow.MULTIPLICAR
                                        GTotal = GTotal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = GTotal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = GTotal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow

                                            .SAP_CENTROCOSTO = String.Empty

                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO

                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            End If
                    End Select
                End If
            Next
        End With
    End Sub
    Private Sub T2VentasQuaker(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim GTotalVentaConIva As Decimal
        Dim GTotalVentaSinIva As Decimal
        Dim IvaPorPagar As Decimal
        Dim GTotalDescuentos As Decimal
        Dim GTDescuentosSinIva As Decimal

        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE


                        Case 10 'Cuenta por cobrar Vendedores C.R.
                            GTotalDescuentos = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2002 Select p.TOTAL).Sum
                            GTDescuentosSinIva = (From p In MovimientosDeProductos.DESCUENTOSSINIVACRQK Select p.DESCUENTO).Sum
                            GTotalVentaConIva = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
                            GTotalVentaSinIva = (From p In MovimientosDeProductos.VS_CARGASinIva Select p.TOTAL).Sum
                            'GTotalDeDescuentosSinIva = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVACR Select p.DESCUENTO).Sum
                            Dim DescuentoNeto As Decimal = GTotalDescuentos - GTDescuentosSinIva

                            IvaPorPagar = ((GTotalVentaConIva / 1.13) - DescuentoNeto) * 0.13
                            Dim GTotalTemporal = GTotalVentaConIva


                            'GTotalTemporal = GTotalTemporal * tableRow.MULTIPLICAR
                            'GTotalTemporal = GTotalTemporal / tableRow.DIVIDIR


                            'Dim IvaErrado As Decimal = GTotalVentaConIva - GTotalTemporal
                            'Dim VentaBruta As Decimal = GTotalVentaConIva - IvaErrado
                            'Dim VentasConDescuentoSinIva As Decimal = VentaBruta - GTotalDescuentos

                            'Dim GTotal As Decimal = (VentasConDescuentoSinIva * 1.13) '+ GTotalVentaSinIva
                            Dim GTotal As Decimal = (GTotalVentaConIva / 1.13) + GTotalVentaSinIva - GTotalDescuentos + IvaPorPagar



                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                    End With

                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 2002 'IVA POR PAGAR

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = IvaPorPagar

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = IvaPorPagar
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    '.SUC_CLAVE = '50'
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With

                        Case 11000 'Descuento a Distribuidores
                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2002 Select p.TOTAL).Sum

                            'GTotal = GTotal * tableRow.MULTIPLICAR
                            'GTotal = GTotal / tableRow.DIVIDIR

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    '.SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tableRow.SUC_CLAVE, tableRow.CENTRO_FIJO)
                                    .SAP_CENTROCOSTO = GetSAPCentroBeneficio(51) 'hay que rehacer el query porque debe ser por sucursal

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With

                        Case 14000 'Venta Bruta
                            Dim VentaBruta As Decimal = (GTotalVentaConIva / 1.13) + GTotalVentaSinIva
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow

                                    .SAP_CENTROCOSTO = GetSAPCentroBeneficio(51)

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = VentaBruta

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = VentaBruta
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    '.SUC_CLAVE = '50'
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With


                        Case 12000 'VENTAS
                            Dim x As New Dictionary(Of Integer, Decimal)

                            x.Add(1, 0)
                            x.Add(2, 0)
                            x.Add(3, 0)
                            x.Add(4, 0)
                            x.Add(5, 0)
                            x.Add(6, 0)
                            x.Add(51, 0)


                            Dim q = From p In MovimientosDeProductos.VS_CAMSVENTAS _
                            Where p.TMO_CLAVE = 2003 Or p.TMO_CLAVE = 2002 Or p.TMO_CLAVE = 6204 Or p.TMO_CLAVE = 6205 _
                              Group p By p.SUC_CLAVE Into g = Group _
                              Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}

                            For Each Secuencia In q
                                x(Secuencia.g(0).SUC_CLAVE) = Secuencia.GTotal

                            Next

                            With MovimientosDeProductos.VS_CARGA
                                For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
                                    Dim TotalActual As Decimal = x(tRow.SUC_CLAVE)
                                    x(tRow.SUC_CLAVE) = TotalActual + tRow.TOTAL
                                Next
                            End With

                            For Each sucursal In x
                                If sucursal.Value > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, sucursal.Key, tableRow.CENTRO_FIJO)


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = sucursal.Value

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = sucursal.Value
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = sucursal.Key
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next

                        Case 16   'TRADEPROMOTIONS

                            If tableRow.NIVEL_AGRUPA = 1 Then


                                With MovimientosDeProductos.VS_TRADEPROMOTIONS
                                    For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
                                        Dim GTotal As Decimal = tRow.TOTAL

                                        GTotal = GTotal * tableRow.MULTIPLICAR
                                        GTotal = GTotal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = GTotal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = GTotal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow

                                            .SAP_CENTROCOSTO = String.Empty

                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO

                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            End If
                    End Select
                End If
            Next
        End With
    End Sub
    Private Sub T2VentasHonduras(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim MontoSumarizado As Decimal

        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE
                        Case 10000  'VENTAS SNACKS (Carga P.T. SALADO)
                            Dim x As New Dictionary(Of Integer, Decimal)

                            Dim q = From p In MovimientosDeProductos.VS_CARGAHONDURAS
                                Where p.MOV_CLAVE = 10 _
                                Group p By p.SUC_CLAVE Into g = Group _
                                Select New With {g, SUC_CLAVE, .monto = g.Sum(Function(p) p.UNIDADES * p.PRECIO_SINIVA)}

                            For Each secuencia In q

                                x.Add(secuencia.SUC_CLAVE, secuencia.monto)

                            Next

                            Dim qDev = From p In MovimientosDeProductos.VS_CARGAHONDURAS
                               Where p.MOV_CLAVE = 33 _
                               Group p By p.SUC_CLAVE Into g = Group _
                               Select New With {g, SUC_CLAVE, .monto = g.Sum(Function(p) p.UNIDADES * p.PRECIO_SINIVA * -1)}

                            For Each secuencia In qDev

                                Dim montoAnterior As Decimal = x(secuencia.SUC_CLAVE)
                                x(secuencia.SUC_CLAVE) = montoAnterior - secuencia.monto

                            Next
                            For Each secuencia In x
                                With Procesados.PolizasC
                                    MontoSumarizado = MontoSumarizado + Math.Round(secuencia.Value, 2)
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow


                                    With NewDataRow
                                        .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.Key, tableRow.CENTRO_FIJO)

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Math.Round(secuencia.Value, 2)

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Math.Round(secuencia.Value, 2)
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = secuencia.Key
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            Next




                        Case 13000   'CUENTAS POR COBRAR (VENDEDORES)

                            Dim GTotal As Decimal = MontoSumarizado
                            'Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 10 Select (p.UNIDADES * p.PRECIO_SINIVA)).Sum
                            'Dim GTotalDevoluciones As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 33 Select (p.UNIDADES * p.PRECIO_SINIVA)).Sum
                            'Dim GTotalIvaDeDevoluciones As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 33 Select (p.UNIDADES * p.IVA)).Sum
                            'Dim GTotalIva As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 10 Select (p.UNIDADES * p.IVA)).Sum

                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Math.Round(GTotal, 2)

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Math.Round(GTotal, 2)
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 12000   'RETENCION IMPUESTO SOBRE VENTAS


                            Dim GTotalConIva As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 10 Select (p.UNIDADES * p.IVA)).Sum
                            Dim GTotaIvaDevoluciones As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 33 Select (p.UNIDADES * p.IVA)).Sum
                            Dim GTotal As Decimal = GTotalConIva + GTotaIvaDevoluciones
                            MontoSumarizado = MontoSumarizado + Math.Round(GTotal, 2)
                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow

                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty
                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Math.Round(GTotal, 2)

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Math.Round(GTotal, 2)
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                    End Select
                End If
            Next
        End With
    End Sub
    Private Sub T2DevolucionesHonduras(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim MontoSumarizado As Decimal

        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE



                        Case 10000  'VENTAS SNACKS (Carga P.T. SALADO)
                            Dim x As New Dictionary(Of Integer, Decimal)



                            Dim q = From p In MovimientosDeProductos.VS_CARGAHONDURAS
                                Where p.MOV_CLAVE = 16 _
                                Group p By p.SUC_CLAVE Into g = Group _
                                Select New With {g, SUC_CLAVE, .monto = g.Sum(Function(p) p.UNIDADES * p.PRECIO_SINIVA * -1)}

                            For Each secuencia In q

                                x.Add(secuencia.SUC_CLAVE, secuencia.monto)

                            Next


                            For Each secuencia In x
                                With Procesados.PolizasC
                                    MontoSumarizado = MontoSumarizado + Math.Round(secuencia.Value, 2)
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.Key, tableRow.CENTRO_FIJO)

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Math.Round(secuencia.Value, 2)

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Math.Round(secuencia.Value, 2)
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = secuencia.Key
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            Next




                        Case 13000   'CUENTAS POR COBRAR (VENDEDORES)

                            Dim GTotal As Decimal = MontoSumarizado
                            'Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 10 Select (p.UNIDADES * p.PRECIO_SINIVA)).Sum
                            'Dim GTotalDevoluciones As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 33 Select (p.UNIDADES * p.PRECIO_SINIVA)).Sum
                            'Dim GTotalIvaDeDevoluciones As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 33 Select (p.UNIDADES * p.IVA)).Sum
                            'Dim GTotalIva As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 10 Select (p.UNIDADES * p.IVA)).Sum

                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow

                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Math.Round(GTotal, 2)

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Math.Round(GTotal, 2)
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 12000   'RETENCION IMPUESTO SOBRE VENTAS


                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 16 Select (p.UNIDADES * p.IVA * -1)).Sum

                            'Dim GTotaIvaDevoluciones As Decimal = (From p In MovimientosDeProductos.VS_CARGAHONDURAS Where p.MOV_CLAVE = 33 Select (p.UNIDADES * p.IVA)).Sum
                            'Dim GTotal As Decimal = GTotalConIva + GTotaIvaDevoluciones
                            MontoSumarizado = MontoSumarizado + Math.Round(GTotal, 2)
                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Math.Round(GTotal, 2)

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Math.Round(GTotal, 2)
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                    End Select
                End If
            Next
        End With
    End Sub
    Private Sub T2DevolucionesCostaRica(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim GTotalDescuentos As Decimal
        Dim GTDescuentosSinIva As Decimal


        Dim DevolucionNeta As Decimal

        'Dim TotalDescuentos64 As Decimal

        Dim DevBEconIVA As Decimal
        Dim DevBEsinIVA As Decimal
        Dim DescuentoBEconIVA As Decimal
        Dim DescuentoBEsinIVA As Decimal
        Dim IvaXPagarBE As Decimal
        Dim CXCBE As Decimal

        Dim MEDevoluciones As Decimal
        Dim MEDescuentos As Decimal
        Dim MEIvaXPagar As Decimal
        Dim MECXC As Decimal

        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE
                        Case 200 'DEVOLUCION BUEN ESTADO
                            GTotalDescuentos = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2002 Select p.TOTAL).Sum
                            GTDescuentosSinIva = (From p In MovimientosDeProductos.DESCUENTOSSINIVACRQK Select p.DESCUENTO).Sum
                            Dim DescuentoNeto As Decimal = GTotalDescuentos - GTDescuentosSinIva

                            DevBEconIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESCR Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 27 Select p.DEVOLUCION).Sum
                            DevBEsinIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVACR Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 27 Select p.DEVOLUCION).Sum
                            DescuentoBEconIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESCR Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 27 Select p.DESCUENTO).Sum
                            DescuentoBEsinIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVACR Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 27 Select p.DESCUENTO).Sum

                            DevolucionNeta = (DevBEconIVA / 1.13) + DevBEsinIVA
                            IvaXPagarBE = ((DevBEconIVA / 1.13) + DevBEsinIVA - DescuentoBEconIVA - DescuentoBEsinIVA) * 0.13
                            CXCBE = ((DevBEconIVA / 1.13) + DevBEsinIVA - DescuentoBEconIVA - DescuentoBEsinIVA) + IvaXPagarBE

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 50, tableRow.CENTRO_FIJO)


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = DevolucionNeta

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = DevolucionNeta
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 50
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With




                        Case 300 'Descuentos a buen estado
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow

                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 50, tableRow.CENTRO_FIJO)
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = DescuentoBEconIVA + DescuentoBEsinIVA

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = DescuentoBEconIVA + DescuentoBEsinIVA
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 50
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With


                            '----------------------------------------------------------------
                            'PRESENTAR POR SUCURSAL PARA EL FUTURO
                            'Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESCR _
                            'Where p.MOV_CLAVE = 27 Or p.MOV_CLAVE = 39 _
                            '  Group p By p.SUC_CLAVE Into g = Group _
                            '  Select New With {g, .GTotal = g.Sum(Function(p) p.DESCUENTO)}

                            'For Each secuencia In q
                            '    TotalDescuentos = TotalDescuentos + secuencia.GTotal

                            '    With Procesados.PolizasC
                            '        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                            '        With NewDataRow

                            '            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)
                            '            If tableRow.IsCUENTA_DRNull Then
                            '                .CUENTADR = String.Empty
                            '                .MONTODR = 0
                            '                .CUENTACR = tableRow.CUENTA_CR
                            '                .MONTOCR = secuencia.GTotal

                            '            ElseIf tableRow.IsCUENTA_CRNull Then
                            '                .CUENTADR = tableRow.CUENTA_DR
                            '                .MONTODR = secuencia.GTotal
                            '                .CUENTACR = String.Empty
                            '                .MONTOCR = 0

                            '            End If
                            '            .Secuencia = NoSecuencia
                            '            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                            '            .CENTRO = tableRow.CENTRO
                            '            .SUC_CLAVE = secuencia.g(0).SUC_CLAVE
                            '        End With
                            '        .AddPolizasCRow(NewDataRow)
                            '    End With
                            'Next
                            '----------------------------------------------------------------


                        Case 500 'IVA POR PAGAR buen estado

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = IvaXPagarBE

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = IvaXPagarBE
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                            'End If

                        Case 501 'Cuenta por cobrar Vendedores C.R

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = CXCBE

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = CXCBE
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                            'End If


                        Case 600 'DEVOLUCION MAL ESTADO 
                            Dim DescuentosConIva As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESCR Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DESCUENTO).Sum
                            Dim DescuentosSinIva As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVACR Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DESCUENTO).Sum
                            Dim MEDevconIVA As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESCR Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DEVOLUCION).Sum
                            Dim MEDevsinIVA As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVACR Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DEVOLUCION).Sum
                            Dim A As Decimal = MEDevconIVA / 1.13
                            Dim B As Decimal = A + DescuentosSinIva
                            Dim C As Decimal = A - (DescuentosConIva + DescuentosSinIva)

                            MEDescuentos = DescuentosConIva + DescuentosSinIva
                            MEDevoluciones = A + MEDevsinIVA
                            MEIvaXPagar = C * 0.13
                            MECXC = A + MEDevsinIVA - MEDescuentos + MEIvaXPagar


                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 50, tableRow.CENTRO_FIJO)

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MEDevoluciones

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MEDevoluciones
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 50
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With




                            'Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESCR _
                            'Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 _
                            'Group p By p.SUC_CLAVE Into g = Group _
                            'Select New With {g, .GTotal = g.Sum(Function(p) p.DEVOLUCION)}

                            'For Each secuencia In q
                            '    If secuencia.GTotal > 0 Then
                            '        With Procesados.PolizasC
                            '            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                            '            With NewDataRow
                            '                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)

                            '                If tableRow.IsCUENTA_DRNull Then
                            '                    .CUENTADR = String.Empty
                            '                    .MONTODR = 0
                            '                    .CUENTACR = tableRow.CUENTA_CR
                            '                    .MONTOCR = secuencia.GTotal / 1.13
                            '                    DevolucionNeta64 = DevolucionNeta64 + (secuencia.GTotal / 1.13)
                            '                ElseIf tableRow.IsCUENTA_CRNull Then
                            '                    .CUENTADR = tableRow.CUENTA_DR
                            '                    .MONTODR = secuencia.GTotal / 1.13
                            '                    .CUENTACR = String.Empty
                            '                    .MONTOCR = 0
                            '                    DevolucionNeta64 = DevolucionNeta64 + (secuencia.GTotal / 1.13)
                            '                End If
                            '                .Secuencia = NoSecuencia
                            '                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                            '                .CENTRO = tableRow.CENTRO
                            '                .SUC_CLAVE = secuencia.g(0).SUC_CLAVE
                            '            End With
                            '            .AddPolizasCRow(NewDataRow)
                            '        End With
                            '    End If
                            'Next
                        Case 601 'Descuento a Distribuidores (mal estado)
                            '
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow

                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 50, tableRow.CENTRO_FIJO)
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MEDescuentos

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MEDescuentos
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 50
                                End With

                                .AddPolizasCRow(NewDataRow)
                            End With

                            ' Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESCR _
                            'Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 _
                            '  Group p By p.SUC_CLAVE Into g = Group _
                            '  Select New With {g, .GTotal = g.Sum(Function(p) p.DESCUENTO)}

                            ' For Each secuencia In q

                            '     If secuencia.GTotal > 0 Then
                            '         Dim Monto As Decimal = secuencia.GTotal
                            '         Monto = Monto * tableRow.MULTIPLICAR
                            '         Monto = Monto / tableRow.DIVIDIR
                            '         With Procesados.PolizasC
                            '             Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                            '             With NewDataRow

                            '                 .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)
                            '                 If tableRow.IsCUENTA_DRNull Then
                            '                     .CUENTADR = String.Empty
                            '                     .MONTODR = 0
                            '                     .CUENTACR = tableRow.CUENTA_CR
                            '                     .MONTOCR = Monto

                            '                 ElseIf tableRow.IsCUENTA_CRNull Then
                            '                     .CUENTADR = tableRow.CUENTA_DR
                            '                     .MONTODR = Monto
                            '                     .CUENTACR = String.Empty
                            '                     .MONTOCR = 0

                            '                 End If
                            '                 .Secuencia = NoSecuencia
                            '                 .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                            '                 .CENTRO = tableRow.CENTRO
                            '                 .SUC_CLAVE = secuencia.g(0).SUC_CLAVE
                            '             End With

                            '             .AddPolizasCRow(NewDataRow)
                            '         End With
                            '         TotalDescuentos64 = TotalDescuentos64 + Monto
                            '     End If

                            ' Next

                        Case 602 'Descuento a Distribuidores (mal estado)
                            '
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow

                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 50, tableRow.CENTRO_FIJO)
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MEDescuentos

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MEDescuentos
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 50
                                End With

                                .AddPolizasCRow(NewDataRow)
                            End With


                        Case 800 'IVA POR PAGAR MERCANCIA DANADA


                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MEIvaXPagar

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MEIvaXPagar
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                            'End If




                        Case 901 'Cuenta por cobrar Vendedores C.R. (mal estado)

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MECXC

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MECXC
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                            'End If

                        Case 10000 'CUENTAS POR COBRAR CENTRALIZADA/LOCALES 


                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 6204 Or p.TMO_CLAVE = 6205 Select p.TOTAL).Sum

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal - CXCEmpleados()

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal - CXCEmpleados()
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With





                        Case 11000 'Descuentos 

                            Dim q = From p In MovimientosDeProductos.VS_CAMSVENTAS _
                            Where p.TMO_CLAVE = 2003 Or p.TMO_CLAVE = 2002 _
                              Group p By p.SUC_CLAVE Into g = Group _
                              Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}


                            For Each Secuencia In q

                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, Secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)


                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Secuencia.GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Secuencia.GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = Secuencia.g(0).SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            Next

                        Case 12000 'VENTAS CR 
                            Dim x As New Dictionary(Of Integer, Decimal)

                            x.Add(1, 0)
                            x.Add(2, 0)
                            x.Add(3, 0)
                            x.Add(4, 0)
                            x.Add(5, 0)
                            x.Add(6, 0)
                            x.Add(50, 0)


                            Dim q = From p In MovimientosDeProductos.VS_CAMSVENTAS _
                            Where p.TMO_CLAVE = 2003 Or p.TMO_CLAVE = 2002 Or p.TMO_CLAVE = 6204 Or p.TMO_CLAVE = 6205 _
                              Group p By p.SUC_CLAVE Into g = Group _
                              Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}

                            For Each Secuencia In q
                                x(Secuencia.g(0).SUC_CLAVE) = Secuencia.GTotal

                            Next

                            'With MovimientosDeProductos.VS_CARGA
                            '    For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
                            '        Dim TotalActual As Decimal = x(tRow.SUC_CLAVE)
                            '        x(tRow.SUC_CLAVE) = TotalActual + tRow.TOTAL
                            '    Next
                            'End With

                            For Each sucursal In x
                                If sucursal.Value > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow


                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, sucursal.Key, tableRow.CENTRO_FIJO)
                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = sucursal.Value

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = sucursal.Value
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = sucursal.Key
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next


                        Case 13000  'CUENTAS POR COBRAR EMPLEADOS 
                            Dim GTtotal As Decimal
                            With MovimientosDeProductos.CXCEMPLEADOS
                                For Each tRow As MovimientosDeProductosData.CXCEMPLEADOSRow In .Rows
                                    If Not tRow.IsTOTALNull Then
                                        GTtotal = GTtotal + tRow.TOTAL
                                    End If

                                Next
                            End With
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTtotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTtotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If

                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With


                        Case 27
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VS_CAMSVENTAS
                                    For Each tRow As MovimientosDeProductosData.VS_CAMSVENTASRow In .Rows
                                        Dim TotalImpuesto As Double = 0
                                        Dim MontoSucursal As Decimal = tRow.TOTAL - MontoDevolucion(tRow.SUC_CLAVE)
                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
                                GTotal = GTotal - MontoTotalDevolucion()

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = String.Empty


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                        End With

                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If


                            End If

                        Case 27   'TRADEPROMOTIONS 

                            If tableRow.NIVEL_AGRUPA = 1 Then


                                With MovimientosDeProductos.VS_TRADEPROMOTIONS
                                    For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
                                        Dim MontoSucursal As Decimal = tRow.TOTAL

                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = String.Empty


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO

                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            End If
                    End Select
                End If
            Next
        End With
    End Sub
    Private Sub T2DevolucionesQuaker(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        '
        'Case 200 'DEVOLUCION BUEN ESTADO
        'DevBEconIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 27 Select p.DEVOLUCION).Sum
        'DevBEsinIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVAQK Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 27 Select p.DEVOLUCION).Sum
        'DescuentoBEconIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 27 Select p.DESCUENTO).Sum
        'DescuentoBEsinIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVAQK Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 27 Select p.DESCUENTO).Sum
        '.MONTODR = DevolucionNeta

        'Case 300 'Descuentos a buen estado
        ' Case 600 'DEVOLUCION MAL ESTADO 
        'DevMEconIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DEVOLUCION).Sum
        'DevMEsinIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVAQK Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DEVOLUCION).Sum
        'IvaXPagarME = ((DevMEconIVA / 1.13) + DevMEsinIVA) * 0.13
        'CXCME = (DevMEconIVA / 1.13) + DevMEsinIVA + IvaXPagarME

        Dim GTotalDescuentos As Decimal
        Dim GTDescuentosSinIva As Decimal


        Dim DevolucionNeta As Decimal

        'Dim TotalDescuentos64 As Decimal

        Dim DevBEconIVA As Decimal
        Dim DevBEsinIVA As Decimal
        Dim DescuentoBEconIVA As Decimal
        Dim DescuentoBEsinIVA As Decimal
        Dim IvaXPagarBE As Decimal
        Dim CXCBE As Decimal

        Dim MEDevoluciones As Decimal
        Dim MEDescuentos As Decimal
        Dim MEIvaXPagar As Decimal
        Dim MECXC As Decimal

        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE
                        Case 200 'DEVOLUCION BUEN ESTADO
                            GTotalDescuentos = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2002 Select p.TOTAL).Sum
                            GTDescuentosSinIva = (From p In MovimientosDeProductos.DESCUENTOSSINIVACRQK Select p.DESCUENTO).Sum
                            Dim DescuentoNeto As Decimal = GTotalDescuentos - GTDescuentosSinIva

                            DevBEconIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 74 Select p.DEVOLUCION).Sum
                            DevBEsinIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVAQK Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 74 Select p.DEVOLUCION).Sum
                            DescuentoBEconIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 74 Select p.DESCUENTO).Sum
                            DescuentoBEsinIVA = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVAQK Where p.MOV_CLAVE = 39 Or p.MOV_CLAVE = 74 Select p.DESCUENTO).Sum

                            DevolucionNeta = (DevBEconIVA / 1.13) + DevBEsinIVA
                            IvaXPagarBE = ((DevBEconIVA / 1.13) + DevBEsinIVA - DescuentoBEconIVA - DescuentoBEsinIVA) * 0.13
                            CXCBE = ((DevBEconIVA / 1.13) + DevBEsinIVA - DescuentoBEconIVA - DescuentoBEsinIVA) + IvaXPagarBE

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 51, tableRow.CENTRO_FIJO)


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = DevolucionNeta

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = DevolucionNeta
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 51
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With




                        Case 300 'Descuentos a buen estado
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow

                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 51, tableRow.CENTRO_FIJO)
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = DescuentoBEconIVA + DescuentoBEsinIVA

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = DescuentoBEconIVA + DescuentoBEsinIVA
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 51
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With


                            '----------------------------------------------------------------
                            'PRESENTAR POR SUCURSAL PARA EL FUTURO
                            'Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER _
                            'Where p.MOV_CLAVE = 27 Or p.MOV_CLAVE = 39 _
                            '  Group p By p.SUC_CLAVE Into g = Group _
                            '  Select New With {g, .GTotal = g.Sum(Function(p) p.DESCUENTO)}

                            'For Each secuencia In q
                            '    TotalDescuentos = TotalDescuentos + secuencia.GTotal

                            '    With Procesados.PolizasC
                            '        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                            '        With NewDataRow

                            '            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)
                            '            If tableRow.IsCUENTA_DRNull Then
                            '                .CUENTADR = String.Empty
                            '                .MONTODR = 0
                            '                .CUENTACR = tableRow.CUENTA_CR
                            '                .MONTOCR = secuencia.GTotal

                            '            ElseIf tableRow.IsCUENTA_CRNull Then
                            '                .CUENTADR = tableRow.CUENTA_DR
                            '                .MONTODR = secuencia.GTotal
                            '                .CUENTACR = String.Empty
                            '                .MONTOCR = 0

                            '            End If
                            '            .Secuencia = NoSecuencia
                            '            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                            '            .CENTRO = tableRow.CENTRO
                            '            .SUC_CLAVE = secuencia.g(0).SUC_CLAVE
                            '        End With
                            '        .AddPolizasCRow(NewDataRow)
                            '    End With
                            'Next
                            '----------------------------------------------------------------


                        Case 500 'IVA POR PAGAR buen estado

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = IvaXPagarBE

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = IvaXPagarBE
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                            'End If

                        Case 501 'Cuenta por cobrar Vendedores C.R

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = CXCBE

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = CXCBE
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                            'End If


                        Case 600 'DEVOLUCION MAL ESTADO 
                            Dim DescuentosConIva As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DESCUENTO).Sum
                            Dim DescuentosSinIva As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVAQK Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DESCUENTO).Sum
                            Dim MEDevconIVA As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DEVOLUCION).Sum
                            Dim MEDevsinIVA As Decimal = (From p In MovimientosDeProductos.VS_DEVOLUCIONESSINIVAQK Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 Select p.DEVOLUCION).Sum
                            Dim A As Decimal = MEDevconIVA / 1.13
                            Dim B As Decimal = A + DescuentosSinIva
                            Dim C As Decimal = A - (DescuentosConIva + DescuentosSinIva)

                            MEDescuentos = DescuentosConIva + DescuentosSinIva
                            MEDevoluciones = A + MEDevsinIVA
                            MEIvaXPagar = C * 0.13
                            MECXC = A + MEDevsinIVA - MEDescuentos + MEIvaXPagar


                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 51, tableRow.CENTRO_FIJO)

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MEDevoluciones

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MEDevoluciones
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 51
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With




                            'Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER _
                            'Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 _
                            'Group p By p.SUC_CLAVE Into g = Group _
                            'Select New With {g, .GTotal = g.Sum(Function(p) p.DEVOLUCION)}

                            'For Each secuencia In q
                            '    If secuencia.GTotal > 0 Then
                            '        With Procesados.PolizasC
                            '            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                            '            With NewDataRow
                            '                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)

                            '                If tableRow.IsCUENTA_DRNull Then
                            '                    .CUENTADR = String.Empty
                            '                    .MONTODR = 0
                            '                    .CUENTACR = tableRow.CUENTA_CR
                            '                    .MONTOCR = secuencia.GTotal / 1.13
                            '                    DevolucionNeta64 = DevolucionNeta64 + (secuencia.GTotal / 1.13)
                            '                ElseIf tableRow.IsCUENTA_CRNull Then
                            '                    .CUENTADR = tableRow.CUENTA_DR
                            '                    .MONTODR = secuencia.GTotal / 1.13
                            '                    .CUENTACR = String.Empty
                            '                    .MONTOCR = 0
                            '                    DevolucionNeta64 = DevolucionNeta64 + (secuencia.GTotal / 1.13)
                            '                End If
                            '                .Secuencia = NoSecuencia
                            '                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                            '                .CENTRO = tableRow.CENTRO
                            '                .SUC_CLAVE = secuencia.g(0).SUC_CLAVE
                            '            End With
                            '            .AddPolizasCRow(NewDataRow)
                            '        End With
                            '    End If
                            'Next
                        Case 601 'Descuento a Distribuidores (mal estado)
                            '
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow

                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, 51, tableRow.CENTRO_FIJO)
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MEDescuentos

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MEDescuentos
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = 51
                                End With

                                .AddPolizasCRow(NewDataRow)
                            End With

                            ' Dim q = From p In MovimientosDeProductos.VS_DEVOLUCIONESQUAKER _
                            'Where p.MOV_CLAVE = 11 Or p.MOV_CLAVE = 37 Or p.MOV_CLAVE = 38 _
                            '  Group p By p.SUC_CLAVE Into g = Group _
                            '  Select New With {g, .GTotal = g.Sum(Function(p) p.DESCUENTO)}

                            ' For Each secuencia In q

                            '     If secuencia.GTotal > 0 Then
                            '         Dim Monto As Decimal = secuencia.GTotal
                            '         Monto = Monto * tableRow.MULTIPLICAR
                            '         Monto = Monto / tableRow.DIVIDIR
                            '         With Procesados.PolizasC
                            '             Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                            '             With NewDataRow

                            '                 .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)
                            '                 If tableRow.IsCUENTA_DRNull Then
                            '                     .CUENTADR = String.Empty
                            '                     .MONTODR = 0
                            '                     .CUENTACR = tableRow.CUENTA_CR
                            '                     .MONTOCR = Monto

                            '                 ElseIf tableRow.IsCUENTA_CRNull Then
                            '                     .CUENTADR = tableRow.CUENTA_DR
                            '                     .MONTODR = Monto
                            '                     .CUENTACR = String.Empty
                            '                     .MONTOCR = 0

                            '                 End If
                            '                 .Secuencia = NoSecuencia
                            '                 .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                            '                 .CENTRO = tableRow.CENTRO
                            '                 .SUC_CLAVE = secuencia.g(0).SUC_CLAVE
                            '             End With

                            '             .AddPolizasCRow(NewDataRow)
                            '         End With
                            '         TotalDescuentos64 = TotalDescuentos64 + Monto
                            '     End If

                            ' Next



                        Case 800 'IVA POR PAGAR MERCANCIA DANADA


                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MEIvaXPagar

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MEIvaXPagar
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                            'End If




                        Case 901 'Cuenta por cobrar Vendedores C.R. (mal estado)

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = MECXC

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = MECXC
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                            'End If

                        Case 10000 'CUENTAS POR COBRAR CENTRALIZADA/LOCALES 


                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 6204 Or p.TMO_CLAVE = 6205 Select p.TOTAL).Sum

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal - CXCEmpleados()

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal - CXCEmpleados()
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With





                        Case 11000 'Descuentos 

                            Dim q = From p In MovimientosDeProductos.VS_CAMSVENTAS _
                            Where p.TMO_CLAVE = 2003 Or p.TMO_CLAVE = 2002 _
                              Group p By p.SUC_CLAVE Into g = Group _
                              Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}


                            For Each Secuencia In q

                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, Secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)


                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Secuencia.GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Secuencia.GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = Secuencia.g(0).SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            Next

                        Case 12000 'VENTAS CR 
                            Dim x As New Dictionary(Of Integer, Decimal)

                            x.Add(1, 0)
                            x.Add(2, 0)
                            x.Add(3, 0)
                            x.Add(4, 0)
                            x.Add(5, 0)
                            x.Add(6, 0)
                            x.Add(51, 0)


                            Dim q = From p In MovimientosDeProductos.VS_CAMSVENTAS _
                            Where p.TMO_CLAVE = 2003 Or p.TMO_CLAVE = 2002 Or p.TMO_CLAVE = 6204 Or p.TMO_CLAVE = 6205 _
                              Group p By p.SUC_CLAVE Into g = Group _
                              Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}

                            For Each Secuencia In q
                                x(Secuencia.g(0).SUC_CLAVE) = Secuencia.GTotal

                            Next

                            'With MovimientosDeProductos.VS_CARGA
                            '    For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
                            '        Dim TotalActual As Decimal = x(tRow.SUC_CLAVE)
                            '        x(tRow.SUC_CLAVE) = TotalActual + tRow.TOTAL
                            '    Next
                            'End With

                            For Each sucursal In x
                                If sucursal.Value > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow


                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, sucursal.Key, tableRow.CENTRO_FIJO)
                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = sucursal.Value

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = sucursal.Value
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = sucursal.Key
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next


                        Case 13000  'CUENTAS POR COBRAR EMPLEADOS 
                            Dim GTtotal As Decimal
                            With MovimientosDeProductos.CXCEMPLEADOS
                                For Each tRow As MovimientosDeProductosData.CXCEMPLEADOSRow In .Rows
                                    If Not tRow.IsTOTALNull Then
                                        GTtotal = GTtotal + tRow.TOTAL
                                    End If

                                Next
                            End With
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTtotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTtotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If

                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With


                        Case 27
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.VS_CAMSVENTAS
                                    For Each tRow As MovimientosDeProductosData.VS_CAMSVENTASRow In .Rows
                                        Dim TotalImpuesto As Double = 0
                                        Dim MontoSucursal As Decimal = tRow.TOTAL - MontoDevolucion(tRow.SUC_CLAVE)
                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
                                GTotal = GTotal - MontoTotalDevolucion()

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = String.Empty


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                        End With

                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If


                            End If

                        Case 27   'TRADEPROMOTIONS 

                            If tableRow.NIVEL_AGRUPA = 1 Then


                                With MovimientosDeProductos.VS_TRADEPROMOTIONS
                                    For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
                                        Dim MontoSucursal As Decimal = tRow.TOTAL

                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With

                                    Next
                                End With

                            Else
                                Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum

                                GTotal = GTotal * tableRow.MULTIPLICAR
                                GTotal = GTotal / tableRow.DIVIDIR
                                If GTotal > 0 Then
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = String.Empty


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = GTotal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = GTotal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO

                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            End If
                    End Select
                End If
            Next
        End With
    End Sub

    Private Sub T2Otros(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)

        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Dim Tmd As Decimal = tableRow.TMD_CLAVE

                    If Tmd = 30000 Then
                        Dim x As New Dictionary(Of Integer, Decimal)



                        Dim q = From p In Liquidaciones.VS_ODS_LIQUIDACIONES
                            Where p.TMD_CLAVE = 25 Or p.TMD_CLAVE = 26 Or p.TMD_CLAVE = 27 _
                            Group p By p.SUC_CLAVE Into g = Group _
                            Select New With {g, SUC_CLAVE, .monto = g.Sum(Function(p) p.TOTAL)}

                        For Each secuencia In q

                            x.Add(secuencia.SUC_CLAVE, secuencia.monto)

                        Next
                        For Each secuencia In x
                            Dim Montosucursal As Decimal = secuencia.Value

                            Montosucursal = Montosucursal * tableRow.MULTIPLICAR
                            Montosucursal = Montosucursal / tableRow.DIVIDIR

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.Key, tableRow.CENTRO_FIJO)


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = Montosucursal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = Montosucursal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = secuencia.Key
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                        Next

                    ElseIf Tmd = 40000 Then 'SOLO GUATEMALA
                        Dim GTotal As Decimal = (From p In Liquidaciones.VS_ODS_LIQUIDACIONES Where p.TMD_CLAVE = 25 Or p.TMD_CLAVE = 26 Or p.TMD_CLAVE = 27 Select (p.TOTAL)).Sum

                        GTotal = GTotal * tableRow.MULTIPLICAR
                        GTotal = GTotal / tableRow.DIVIDIR

                        If GTotal > 0 Then
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty
                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                        End If
                    ElseIf Tmd = 50000 Then
                        Dim GTotal As Decimal = (From p In Liquidaciones.VS_ODS_LIQUIDACIONES Where p.TMD_CLAVE = 25 Or p.TMD_CLAVE = 26 Or p.TMD_CLAVE = 27 Select (p.TOTAL)).Sum

                        If GTotal > 0 Then
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty


                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    .SUC_CLAVE = String.Empty
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                        End If
                    Else



                        If tableRow.NIVEL_AGRUPA = 1 Then


                            With Liquidaciones.VS_ODS_LIQUIDACIONES
                                For Each tRow As LiquidacionesData.VS_ODS_LIQUIDACIONESRow In .Rows
                                    If tRow.TMD_CLAVE = Tmd Then
                                        '
                                        Dim MontoSucursal As Decimal = tRow.TOTAL
                                        MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        MontoSucursal = MontoSucursal / tableRow.DIVIDIR

                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)


                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If
                                Next
                            End With

                        ElseIf tableRow.NIVEL_AGRUPA = 0 Then
                            Dim GTotal As Decimal
                            'Dim IVAfactor As Decimal = tableRow.MULTIPLICAR / tableRow.DIVIDIR

                            GTotal = (From p In Liquidaciones.VS_ODS_LIQUIDACIONES Where p.TMD_CLAVE = Tmd Select p.TOTAL).Sum
                            'GTotal = GTotal * IVAfactor
                            GTotal = GTotal * tableRow.MULTIPLICAR
                            GTotal = GTotal / tableRow.DIVIDIR
                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If



                        End If

                    End If

                End If


            Next
        End With

    End Sub

    Private Sub T2OtrosSumarizado(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim MontoSumarizado As Decimal
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Dim Tmd As Decimal = tableRow.TMD_CLAVE

                    If tableRow.NIVEL_AGRUPA = 1 Then


                        With Liquidaciones.VS_ODS_LIQUIDACIONES
                            For Each tRow As LiquidacionesData.VS_ODS_LIQUIDACIONESRow In .Rows
                                If tRow.TMD_CLAVE = Tmd Then
                                    '
                                    Dim MontoSucursal As Decimal = tRow.TOTAL

                                    MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                    MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                    MontoSumarizado = MontoSumarizado + Math.Round(MontoSucursal, 2)

                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = Math.Round(MontoSucursal, 2)

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = Math.Round(MontoSucursal, 2)
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = tRow.SUC_CLAVE
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next
                        End With

                    ElseIf tableRow.NIVEL_AGRUPA = 0 Then
                        Dim GTotal As Decimal
                        Dim IVAfactor As Decimal = tableRow.MULTIPLICAR / tableRow.DIVIDIR
                        If tableRow.SUMAR = 1 Then
                            GTotal = MontoSumarizado
                        Else
                            GTotal = (From p In Liquidaciones.VS_ODS_LIQUIDACIONES Where p.TMD_CLAVE = Tmd Select p.TOTAL).Sum
                            GTotal = GTotal * tableRow.MULTIPLICAR
                            GTotal = GTotal / tableRow.DIVIDIR
                            MontoSumarizado = MontoSumarizado + Math.Round(GTotal, 2)
                        End If



                        If GTotal > 0 Then
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = Math.Round(GTotal, 2)

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = Math.Round(GTotal, 2)
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = String.Empty

                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                        End If
                    End If
                End If
            Next
        End With

    End Sub


    Private Sub T2OtrosSabritasQuaker(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)

        Dim CCVendedores As Decimal

        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Select Case tableRow.TMD_CLAVE
                        Case 101 'Descuentos Otros
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.GASTOSSABQUAK
                                    For Each tRow As MovimientosDeProductosData.GASTOSSABQUAKRow In .Rows
                                        If tRow.LETRA = "A" Then
                                            CCVendedores += tRow.TOTAL
                                            With Procesados.PolizasC
                                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                                With NewDataRow
                                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                    If tableRow.IsCUENTA_DRNull Then
                                                        .CUENTADR = String.Empty
                                                        .MONTODR = 0
                                                        .CUENTACR = tableRow.CUENTA_CR
                                                        .MONTOCR = tRow.TOTAL

                                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                                        .CUENTADR = tableRow.CUENTA_DR
                                                        .MONTODR = tRow.TOTAL
                                                        .CUENTACR = String.Empty
                                                        .MONTOCR = 0

                                                    End If
                                                    .Secuencia = NoSecuencia
                                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                    .CENTRO = tableRow.CENTRO
                                                    .SUC_CLAVE = tRow.SUC_CLAVE
                                                End With
                                                .AddPolizasCRow(NewDataRow)
                                            End With
                                        End If
                                    Next
                                End With
                            End If


                        Case 102 'Gastos de Promociones A & M
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.GASTOSSABQUAK
                                    For Each tRow As MovimientosDeProductosData.GASTOSSABQUAKRow In .Rows
                                        If tRow.LETRA = "B" Then
                                            CCVendedores += tRow.TOTAL
                                            With Procesados.PolizasC
                                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                                With NewDataRow
                                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                    If tableRow.IsCUENTA_DRNull Then
                                                        .CUENTADR = String.Empty
                                                        .MONTODR = 0
                                                        .CUENTACR = tableRow.CUENTA_CR
                                                        .MONTOCR = tRow.TOTAL

                                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                                        .CUENTADR = tableRow.CUENTA_DR
                                                        .MONTODR = tRow.TOTAL
                                                        .CUENTACR = String.Empty
                                                        .MONTOCR = 0

                                                    End If
                                                    .Secuencia = NoSecuencia
                                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                    .CENTRO = tableRow.CENTRO
                                                    .SUC_CLAVE = tRow.SUC_CLAVE
                                                End With
                                                .AddPolizasCRow(NewDataRow)
                                            End With
                                        End If
                                    Next
                                End With
                            End If

                        Case 103 'Gastos de Exhibidores
                            If tableRow.NIVEL_AGRUPA = 1 Then
                                With MovimientosDeProductos.GASTOSSABQUAK
                                    For Each tRow As MovimientosDeProductosData.GASTOSSABQUAKRow In .Rows
                                        If tRow.LETRA = "C" Or tRow.LETRA = "D" Then
                                            CCVendedores += tRow.TOTAL
                                            With Procesados.PolizasC
                                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                                With NewDataRow
                                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                    If tableRow.IsCUENTA_DRNull Then
                                                        .CUENTADR = String.Empty
                                                        .MONTODR = 0
                                                        .CUENTACR = tableRow.CUENTA_CR
                                                        .MONTOCR = tRow.TOTAL

                                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                                        .CUENTADR = tableRow.CUENTA_DR
                                                        .MONTODR = tRow.TOTAL
                                                        .CUENTACR = String.Empty
                                                        .MONTOCR = 0

                                                    End If
                                                    .Secuencia = NoSecuencia
                                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                    .CENTRO = tableRow.CENTRO
                                                    .SUC_CLAVE = tRow.SUC_CLAVE
                                                End With
                                                .AddPolizasCRow(NewDataRow)
                                            End With
                                        End If
                                    Next
                                End With
                            End If
                        Case 104 'Cuenta por cobrar Vendedores
                            If CCVendedores > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = CCVendedores

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = CCVendedores
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                    End Select
                End If
            Next
        End With

    End Sub

    Private Sub T2DVentasSumarizado(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim MontoSumarizado As Decimal
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Dim Tmd As Decimal = tableRow.TMD_CLAVE

                    If tableRow.NIVEL_AGRUPA = 1 Then


                        With MovimientosDeProductos.VS_CARGA
                            For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
                                'If tRow.TMd_CLAVE = Tmd Then
                                '
                                Dim MontoSucursal As Decimal = tRow.TOTAL - MontoDevolucion(tRow.SUC_CLAVE)

                                MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                MontoSumarizado = MontoSumarizado + Math.Round(MontoSucursal, 2)

                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)


                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Math.Round(MontoSucursal, 2)

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Math.Round(MontoSucursal, 2)
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = tRow.SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                                'End If
                            Next
                        End With

                    ElseIf tableRow.NIVEL_AGRUPA = 0 Then
                        Dim GTotal As Decimal
                        Dim IVAfactor As Decimal = tableRow.MULTIPLICAR / tableRow.DIVIDIR

                        GTotal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
                        GTotal = GTotal - MontoTotalDevolucion()
                        GTotal = GTotal * tableRow.MULTIPLICAR
                        GTotal = GTotal / tableRow.DIVIDIR
                        MontoSumarizado = MontoSumarizado + Math.Round(GTotal, 2)

                        If GTotal > 0 Then
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = Math.Round(GTotal, 2)

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = Math.Round(GTotal, 2)
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO

                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                        End If
                    ElseIf tableRow.TMD_CLAVE = 1000 Then
                        Dim GTotal As Decimal = MontoSumarizado
                        If GTotal > 0 Then
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = Math.Round(GTotal, 2)

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = Math.Round(GTotal, 2)
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO

                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                        End If

                    End If
                End If
            Next
        End With

    End Sub
    Private Sub T2DescuentosSumarizado(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim MontoSumarizado As Decimal
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Dim Tmd As Decimal = tableRow.TMD_CLAVE

                    If tableRow.NIVEL_AGRUPA = 1 Then


                        With MovimientosDeProductos.VS_TRADEPROMOTIONS
                            For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
                                'If tRow.TMo_CLAVE = Tmd Then
                                '
                                Dim MontoSucursal As Decimal = tRow.TOTAL

                                MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                MontoSumarizado = MontoSumarizado + Math.Round(MontoSucursal, 2)

                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = Math.Round(MontoSucursal, 2)

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = Math.Round(MontoSucursal, 2)
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        .SUC_CLAVE = tRow.SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                                'End If
                            Next
                        End With

                    ElseIf tableRow.NIVEL_AGRUPA = 0 Then
                        Dim GTotal As Decimal
                        Dim IVAfactor As Decimal = tableRow.MULTIPLICAR / tableRow.DIVIDIR
                        If tableRow.SUMAR = 1 Then
                            GTotal = MontoSumarizado
                        Else
                            GTotal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum
                            GTotal = GTotal * tableRow.MULTIPLICAR
                            GTotal = GTotal / tableRow.DIVIDIR
                            MontoSumarizado = MontoSumarizado + Math.Round(GTotal, 2)
                        End If



                        If GTotal > 0 Then
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = Math.Round(GTotal, 2)

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = Math.Round(GTotal, 2)
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO

                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                        End If
                    End If
                End If
            Next
        End With

    End Sub
    Private Sub T2BancosCAMN(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Dim Tmd As Decimal = tableRow.TMD_CLAVE

                    If tableRow.NIVEL_AGRUPA = 2 Then


                        With Liquidaciones.VS_LIQUIDACIONESBANCOS
                            For Each tRow As LiquidacionesData.VS_LIQUIDACIONESBANCOSRow In .Rows
                                If tRow.TMD_CLAVE = Tmd Then


                                    Dim MontoSucursal As Decimal = tRow.TOTAL

                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow

                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = MontoSucursal

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = MontoSucursal
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = tRow.SUC_CLAVE
                                            If Not tRow.IsMDI_NUMREMNull Then
                                                If tRow.MDI_NUMREM.Length > 12 Then
                                                    .ASSIGNMENT = tRow.MDI_NUMREM.Substring(0, 12)
                                                Else
                                                    .ASSIGNMENT = tRow.MDI_NUMREM
                                                End If

                                            Else
                                                .ASSIGNMENT = Format(MontoSucursal, "#.00") & Format(tRow.FECHA_DEPOSITO, "ddMMyy")
                                            End If

                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next
                        End With

                    ElseIf tableRow.NIVEL_AGRUPA = 0 Then
                        Dim GTotal As Decimal = (From p In Liquidaciones.VS_LIQUIDACIONESBANCOS Where p.TMD_CLAVE = Tmd Select p.TOTAL).Sum

                        If GTotal > 0 Then
                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotal

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotal
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = String.Empty

                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                        End If
                    End If
                End If
            Next
        End With

    End Sub
    Private Sub T2BancosPanama(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Dim Tmd As Decimal = tableRow.TMD_CLAVE
                    Select Case tableRow.TMD_CLAVE
                        Case 10000 'BANCO NACIONAL DE PANAMA
                            With Liquidaciones.DEPOSITOSCAMSUR
                                For Each tRow As LiquidacionesData.DEPOSITOSCAMSURRow In .Rows
                                    If tRow.TMO_CLAVE = "0017" Then

                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)


                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = tRow.TOTAL

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = tRow.TOTAL
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                                If Not tRow.IsMDI_NUMREMNull Then
                                                    If tRow.MDI_NUMREM.Length > 12 Then
                                                        .ASSIGNMENT = tRow.MDI_NUMREM.Substring(0, 12)
                                                    Else
                                                        .ASSIGNMENT = tRow.MDI_NUMREM
                                                    End If

                                                Else
                                                    .ASSIGNMENT = Format(tRow.TOTAL, "#.00") & Format(tRow.FECHA_DEPOSITO, "ddMMyy")
                                                End If


                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If
                                Next
                            End With
                        Case 11000 'HSBC 
                            With Liquidaciones.DEPOSITOSCAMSUR
                                For Each tRow As LiquidacionesData.DEPOSITOSCAMSURRow In .Rows
                                    If tRow.TMO_CLAVE = "0016" Then

                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = tRow.TOTAL

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = tRow.TOTAL
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                                If Not tRow.IsMDI_NUMREMNull Then

                                                    If tRow.MDI_NUMREM.Length > 12 Then
                                                        .ASSIGNMENT = tRow.MDI_NUMREM.Substring(0, 12)
                                                    Else
                                                        .ASSIGNMENT = tRow.MDI_NUMREM
                                                    End If
                                                Else
                                                    .ASSIGNMENT = Format(tRow.TOTAL, "#.00") & Format(tRow.FECHA_DEPOSITO, "ddMMyy")
                                                End If

                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If
                                Next
                            End With
                        Case 12000
                            Dim GTotal As Decimal = (From p In Liquidaciones.BANCOSCAMSUR Where p.TMO_CLAVE = "0016" Or p.TMO_CLAVE = "0017" Select p.TOTAL).Sum
                            If GTotal > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = String.Empty

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                    End Select

                End If
            Next
        End With

    End Sub
    Private Sub T2BancosCostaRica(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Dim Tmd As Decimal = tableRow.TMD_CLAVE
                    Select Case tableRow.TMD_CLAVE
                        Case 10000 'SCOTIABANK
                            With Liquidaciones.DEPOSITOSCAMSUR
                                For Each tRow As LiquidacionesData.DEPOSITOSCAMSURRow In .Rows
                                    If tRow.TMO_CLAVE = "0009" Then

                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = tRow.TOTAL

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = tRow.TOTAL
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                                If Not tRow.IsMDI_NUMREMNull Then
                                                    If tRow.MDI_NUMREM.Length > 12 Then
                                                        .ASSIGNMENT = tRow.MDI_NUMREM.Substring(0, 12)
                                                    Else
                                                        .ASSIGNMENT = tRow.MDI_NUMREM
                                                    End If

                                                Else
                                                    .ASSIGNMENT = Format(tRow.TOTAL, "#.00") & Format(tRow.FECHA_DEPOSITO, "ddMMyy")
                                                End If

                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If
                                Next
                            End With
                        Case 31, 35 'DEPOSITOS desde ODS_LIQUIDACIONES 31 y 35
                            With Liquidaciones.DepositosQuakerSabritas
                                For Each tRow As LiquidacionesData.DepositosQuakerSabritasRow In .Rows
                                    If tRow.TMD_CLAVE = tableRow.TMD_CLAVE Then


                                        Dim MontoSucursal As Decimal = tRow.TOTAL

                                        'MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        'MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                                If Not tRow.IsMDI_NUMREMNull Then

                                                    If tRow.MDI_NUMREM.Length > 12 Then
                                                        .ASSIGNMENT = tRow.MDI_NUMREM.Substring(0, 12)
                                                    Else
                                                        .ASSIGNMENT = tRow.MDI_NUMREM
                                                    End If
                                                Else
                                                    .ASSIGNMENT = Format(tRow.TOTAL, "#.00") & Format(tRow.FECHA_DEPOSITO, "ddMMyy")
                                                End If

                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If
                                Next
                            End With
                        Case 12000
                            Dim GTotal1 As Decimal = (From p In Liquidaciones.BANCOSCAMSUR Where p.TMO_CLAVE = "0009" Select p.TOTAL).Sum
                            Dim GTotal2 As Decimal = (From p In Liquidaciones.DepositosQuakerSabritas Where p.TMD_CLAVE = 31 Or p.TMD_CLAVE = 35 Select p.TOTAL).Sum

                            If GTotal1 + GTotal2 > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal1 + GTotal2

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal1 + GTotal2
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = String.Empty

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If




                    End Select

                End If
            Next
        End With

    End Sub
    Private Sub T2BancosQuaker(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

                    Dim Tmd As Decimal = tableRow.TMD_CLAVE
                    Select Case tableRow.TMD_CLAVE
                        Case 10000 'SCOTIABANK
                            With Liquidaciones.DEPOSITOSCAMSUR
                                For Each tRow As LiquidacionesData.DEPOSITOSCAMSURRow In .Rows
                                    If tRow.TMO_CLAVE = "0009" Then

                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = tRow.TOTAL

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = tRow.TOTAL
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                                If Not tRow.IsMDI_NUMREMNull Then
                                                    If tRow.MDI_NUMREM.Length > 12 Then
                                                        .ASSIGNMENT = tRow.MDI_NUMREM.Substring(0, 12)
                                                    Else
                                                        .ASSIGNMENT = tRow.MDI_NUMREM
                                                    End If

                                                Else
                                                    .ASSIGNMENT = Format(tRow.TOTAL, "#.00") & Format(tRow.FECHA_DEPOSITO, "ddMMyy")
                                                End If

                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If
                                Next
                            End With
                        Case 31, 35 'DEPOSITOS desde ODS_LIQUIDACIONES 31 y 35
                            With Liquidaciones.VS_LIQUIDACIONESBANCOS
                                For Each tRow As LiquidacionesData.VS_LIQUIDACIONESBANCOSRow In .Rows
                                    If tRow.TMD_CLAVE = tableRow.TMD_CLAVE Then


                                        Dim MontoSucursal As Decimal = tRow.TOTAL

                                        'MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
                                        'MontoSucursal = MontoSucursal / tableRow.DIVIDIR
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = MontoSucursal

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = MontoSucursal
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                                If Not tRow.IsMDI_NUMREMNull Then

                                                    If tRow.MDI_NUMREM.Length > 12 Then
                                                        .ASSIGNMENT = tRow.MDI_NUMREM.Substring(0, 12)
                                                    Else
                                                        .ASSIGNMENT = tRow.MDI_NUMREM
                                                    End If
                                                Else
                                                    .ASSIGNMENT = Format(tRow.TOTAL, "#.00") & Format(tRow.FECHA_DEPOSITO, "ddMMyy")
                                                End If

                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If
                                Next
                            End With
                        Case 12000
                            Dim GTotal1 As Decimal = (From p In Liquidaciones.BANCOSCAMSUR Where p.TMO_CLAVE = "0009" Select p.TOTAL).Sum
                            Dim GTotal2 As Decimal = (From p In Liquidaciones.VS_LIQUIDACIONESBANCOS Where p.TMD_CLAVE = 31 Or p.TMD_CLAVE = 35 Select p.TOTAL).Sum

                            If GTotal1 + GTotal2 > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty

                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotal1 + GTotal2

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotal1 + GTotal2
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = String.Empty

                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If




                    End Select

                End If
            Next
        End With

    End Sub
    Private Sub T2NCPanama(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim GTotalPromosCentralizadas As Decimal
        Dim GTotalPromosLocals As Decimal
        Dim GTotalDiferencias As Decimal
        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then
                    Select Case tableRow.TMD_CLAVE
                        Case 10000 'DESCUENTO ESPECIALES PROMO CENTRALIZADA
                            With MovimientosDeProductos.VS_NCPROMOSPANAMA

                                For Each tRow As MovimientosDeProductosData.VS_NCPROMOSPANAMARow In .Rows
                                    If tRow.CAR_CLAVE = "2200" Then
                                        GTotalPromosCentralizadas = GTotalPromosCentralizadas + tRow.TOTAL
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = tRow.TOTAL

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = tRow.TOTAL
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If


                                Next
                            End With

                        Case 11000 'CUENTAS POR COBRAR CENTRALIZADA

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotalPromosCentralizadas

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotalPromosCentralizadas
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    '.SUC_CLAVE = tRow.SUC_CLAVE
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With

                        Case 12000 'Diferencia en Precio
                            With MovimientosDeProductos.VS_NCDIFERENCIASPANAMA

                                For Each tRow As MovimientosDeProductosData.VS_NCDIFERENCIASPANAMARow In .Rows
                                    GTotalDiferencias = GTotalDiferencias + tRow.TOTAL
                                    If tRow.TOTAL > 0 Then
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = tRow.TOTAL

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = tRow.TOTAL
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If


                                Next
                            End With
                        Case 13000 'Diferencia en Precio
                            If GTotalDiferencias > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty
                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotalDiferencias

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotalDiferencias
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        '.SUC_CLAVE = tRow.SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 14000 'DESCUENTO ESPECIALES PROMO Local
                            With MovimientosDeProductos.VS_NCPROMOSPANAMA

                                For Each tRow As MovimientosDeProductosData.VS_NCPROMOSPANAMARow In .Rows
                                    If tRow.CAR_CLAVE = "2100" Then
                                        GTotalPromosLocals = GTotalPromosLocals + tRow.TOTAL
                                        With Procesados.PolizasC
                                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                            With NewDataRow
                                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

                                                If tableRow.IsCUENTA_DRNull Then
                                                    .CUENTADR = String.Empty
                                                    .MONTODR = 0
                                                    .CUENTACR = tableRow.CUENTA_CR
                                                    .MONTOCR = tRow.TOTAL

                                                ElseIf tableRow.IsCUENTA_CRNull Then
                                                    .CUENTADR = tableRow.CUENTA_DR
                                                    .MONTODR = tRow.TOTAL
                                                    .CUENTACR = String.Empty
                                                    .MONTOCR = 0

                                                End If
                                                .Secuencia = NoSecuencia
                                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                                .CENTRO = tableRow.CENTRO
                                                .SUC_CLAVE = tRow.SUC_CLAVE
                                            End With
                                            .AddPolizasCRow(NewDataRow)
                                        End With
                                    End If


                                Next
                            End With

                        Case 15000 'CUENTAS POR COBRAR Local

                            With Procesados.PolizasC
                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                With NewDataRow
                                    .SAP_CENTROCOSTO = String.Empty

                                    If tableRow.IsCUENTA_DRNull Then
                                        .CUENTADR = String.Empty
                                        .MONTODR = 0
                                        .CUENTACR = tableRow.CUENTA_CR
                                        .MONTOCR = GTotalPromosLocals

                                    ElseIf tableRow.IsCUENTA_CRNull Then
                                        .CUENTADR = tableRow.CUENTA_DR
                                        .MONTODR = GTotalPromosLocals
                                        .CUENTACR = String.Empty
                                        .MONTOCR = 0

                                    End If
                                    .Secuencia = NoSecuencia
                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                    .CENTRO = tableRow.CENTRO
                                    '.SUC_CLAVE = tRow.SUC_CLAVE
                                End With
                                .AddPolizasCRow(NewDataRow)
                            End With
                    End Select
                End If
            Next

        End With




    End Sub
    Private Sub T2NCCostaRica(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
        Dim GTotalDescuentos As Decimal
        Dim GTotalPromociones As Decimal
        Dim GTotalExhibidores As Decimal


        With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
            For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
                If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then
                    Select Case tableRow.TMD_CLAVE
                        Case 10000 'Descuentos Otros

                            Dim q = From p In MovimientosDeProductos.VS_NCPROMOSCR
                            Where p.TIPO = "A" _
                            Group p By p.SUC_CLAVE Into g = Group _
                            Select New With {g, .monto = g.Sum(Function(p) p.TOTAL)}

                            For Each Secuencia In q
                                If Secuencia.monto > 0 Then


                                    GTotalDescuentos = GTotalDescuentos + Secuencia.monto
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, Secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = Secuencia.monto

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = Secuencia.monto
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = Secuencia.g(0).SUC_CLAVE
                                        End With
                                        .AddPolizasCRow(NewDataRow)

                                    End With
                                End If

                            Next

                        Case 11000 'Cuenta por cobrar Vendedores C.R.
                            If GTotalDescuentos > 0 Then


                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty


                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotalDescuentos

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotalDescuentos
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        '.SUC_CLAVE = tRow.SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 12000 'Gastos de Promociones A & M
                            Dim q = From p In MovimientosDeProductos.VS_NCPROMOSCR
                            Where p.TIPO = "B" _
                            Group p By p.SUC_CLAVE Into g = Group _
                            Select New With {g, .monto = g.Sum(Function(p) p.TOTAL)}

                            For Each Secuencia In q
                                If Secuencia.monto > 0 Then
                                    GTotalPromociones = GTotalPromociones + Secuencia.monto
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, Secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = Secuencia.monto

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = Secuencia.monto
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = Secuencia.g(0).SUC_CLAVE
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next



                        Case 13000 'Cuenta por cobrar Vendedores C.R.
                            If GTotalPromociones > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty


                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotalPromociones

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotalPromociones
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        '.SUC_CLAVE = tRow.SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If
                        Case 14000 'Gastos de Exhibidores
                            Dim q = From p In MovimientosDeProductos.VS_NCPROMOSCR
                            Where p.TIPO = "D" _
                            Group p By p.SUC_CLAVE Into g = Group _
                            Select New With {g, .monto = g.Sum(Function(p) p.TOTAL)}

                            For Each Secuencia In q
                                If Secuencia.monto > 0 Then
                                    GTotalExhibidores = GTotalExhibidores + Secuencia.monto
                                    With Procesados.PolizasC
                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                        With NewDataRow
                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, Secuencia.g(0).SUC_CLAVE, tableRow.CENTRO_FIJO)


                                            If tableRow.IsCUENTA_DRNull Then
                                                .CUENTADR = String.Empty
                                                .MONTODR = 0
                                                .CUENTACR = tableRow.CUENTA_CR
                                                .MONTOCR = Secuencia.monto

                                            ElseIf tableRow.IsCUENTA_CRNull Then
                                                .CUENTADR = tableRow.CUENTA_DR
                                                .MONTODR = Secuencia.monto
                                                .CUENTACR = String.Empty
                                                .MONTOCR = 0

                                            End If
                                            .Secuencia = NoSecuencia
                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                            .CENTRO = tableRow.CENTRO
                                            .SUC_CLAVE = Secuencia.g(0).SUC_CLAVE
                                        End With
                                        .AddPolizasCRow(NewDataRow)
                                    End With
                                End If
                            Next



                        Case 15000 'Cuenta por cobrar Vendedores C.R..
                            If GTotalExhibidores > 0 Then
                                With Procesados.PolizasC
                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
                                    With NewDataRow
                                        .SAP_CENTROCOSTO = String.Empty


                                        If tableRow.IsCUENTA_DRNull Then
                                            .CUENTADR = String.Empty
                                            .MONTODR = 0
                                            .CUENTACR = tableRow.CUENTA_CR
                                            .MONTOCR = GTotalExhibidores

                                        ElseIf tableRow.IsCUENTA_CRNull Then
                                            .CUENTADR = tableRow.CUENTA_DR
                                            .MONTODR = GTotalExhibidores
                                            .CUENTACR = String.Empty
                                            .MONTOCR = 0

                                        End If
                                        .Secuencia = NoSecuencia
                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
                                        .CENTRO = tableRow.CENTRO
                                        '.SUC_CLAVE = tRow.SUC_CLAVE
                                    End With
                                    .AddPolizasCRow(NewDataRow)
                                End With
                            End If

                    End Select
                End If
            Next
        End With




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
        'Dim NoSecuencia As Long = 0
        With Procesados.PolizasC
            For Each tablerow As ProcesadasData.PolizasCRow In .Rows

                'definitiva()
                With SapGLInterfase.SAP_GL_INTERFASE

                    ''test
                    'With SapGLInterfase.SAP_GL_TEMP

                    'definitiva()
                    Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow

                    ''test
                    'Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_TEMPRow = .NewSAP_GL_TEMPRow

                    With NewDataRow
                        .ESTATUS_SAP = "REV"
                        '.ESTATUS_SAP = "TEST"
                        '.ESTATUS_SAP = "NEW"
                        .DOCUMENT_DATE = Date.Today
                        .DOCUMENT_TYPE = "ZI"
                        .REF_DOCUMENT_NUMBER = "B" & InfoCache.PaisClave & Format(tablerow.Secuencia, "000000")
                        .DOC_HEADER_TEXT = tablerow.DOC_HEADER_TEXT & GetPeriodo()
                        .SAP_COMPANY_CODE = InfoCache.ClaveCompania

                        If tablerow.CUENTADR.Length > 0 Then
                            .SAP_ACCOUNT = tablerow.CUENTADR
                            If tablerow.SAP_CENTROCOSTO.Length > 0 Then


                                Select Case tablerow.CUENTADR.Substring(0, 1)
                                    Case "1", "2", "3"
                                        .SAP_PROFIT_CENTER = String.Empty
                                        .SAP_COST_CENTER = String.Empty
                                    Case "4", "5", "7"
                                        .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO.ToString
                                        .SAP_COST_CENTER = String.Empty
                                    Case "6"
                                        .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO.ToString
                                        .SAP_PROFIT_CENTER = String.Empty
                                End Select
                            End If
                        Else
                            .SAP_ACCOUNT = tablerow.CUENTACR
                            If tablerow.SAP_CENTROCOSTO.Length > 0 Then


                                Select Case tablerow.CUENTACR.Substring(0, 1)
                                    Case "1", "2", "3"
                                        .SAP_PROFIT_CENTER = String.Empty
                                        .SAP_COST_CENTER = String.Empty
                                    Case "4", "5", "7"
                                        .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO.ToString
                                        .SAP_COST_CENTER = String.Empty
                                    Case "6"
                                        .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO.ToString
                                        .SAP_PROFIT_CENTER = String.Empty
                                End Select
                            End If
                        End If

                        'If Not tablerow.IsSAP_CENTROCOSTONull Then
                        '    If tablerow.SAP_CENTROCOSTO.Length > 0 Then
                        '        Select Case tablerow.CENTRO
                        '            Case "B", "FB"
                        '                .SAP_COST_CENTER = String.Empty
                        '                .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO
                        '            Case Else
                        '                .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO
                        '                .SAP_PROFIT_CENTER = String.Empty
                        '        End Select
                        '    End If
                        'Else
                        '    .SAP_COST_CENTER = String.Empty
                        '    .SAP_PROFIT_CENTER = String.Empty
                        'End If



                        .SAP_SEGMENT = String.Empty
                        .ENTERED_DR = tablerow.MONTODR
                        .ENTERED_CR = tablerow.MONTOCR


                        '.ENTERED_DR = CDec(Format(tablerow.MONTODR, "#.00"))
                        '.ENTERED_CR = CDec(Format(tablerow.MONTOCR, "#.00"))
                        If Not tablerow.IsASSIGNMENTNull Then
                            .ASSIGNMENT = tablerow.ASSIGNMENT
                        Else
                            .ASSIGNMENT = String.Empty
                        End If
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
                        .AddSAP_GL_INTERFASERow(NewDataRow)
                        '.AddSAP_GL_TEMPRow(NewDataRow)

                        .AcceptChanges()
                    Catch ex As Exception

                    End Try
                End With
            Next

        End With

        'definitiva
        With VerDatosForm.GRID_SAP_GL_INTERFASE
            .DataSource = SapGLInterfase.SAP_GL_INTERFASE
        End With


        With SapGLInterfase.SAP_GL_INTERFASE
            For Each tablerow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow In .Rows
                Dim result As Boolean = (New PDCVDatos).insert(tablerow)
            Next
        End With
        Me.SecuenciaInicial.Text = (NoSecuencia + 1).ToString


        ' ''test
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
    Private Function FillCentroCosto(ByVal Centro As String, ByVal Sucursal As Integer, ByVal CFijo As String) As String
        Dim CentroString As String = String.Empty
        Select Case Centro
            Case "B"
                CentroString = GetSAPCentroBeneficio(Sucursal)
            Case "C"
                CentroString = GetSAPCentroCosto(Sucursal)
            Case "D"
                CentroString = String.Empty
            Case "FB", "FC"
                CentroString = CFijo
            Case "G"
                CentroString = GetSAPCentroGastos(Sucursal)
        End Select
        Return CentroString
    End Function




    Private Function MontoIvaPorPagarCR() As Decimal
        Dim GTotalDescuentos As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2002 Select p.TOTAL).Sum
        Dim GTotalVentaConIva As Decimal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
        Dim GTotalVentaSinIva = GTotalVentaConIva / 1.13
        Dim IvaErrado As Decimal = GTotalVentaConIva - GTotalVentaSinIva
        Dim VentaBruta As Decimal = GTotalVentaConIva - IvaErrado

        Dim VentasConDescuentoSinIva As Decimal = VentaBruta - GTotalDescuentos
        Dim GTotal As Decimal = VentasConDescuentoSinIva * 1.13



        Dim IVApxp As Decimal = GTotal - VentasConDescuentoSinIva

        Return IVApxp
    End Function
    Private Function MontoVentaBrutaCR() As Decimal
        Dim GTotalDescuentos As Decimal = (From p In MovimientosDeProductos.VS_CAMSVENTAS Where p.TMO_CLAVE = 2002 Select p.TOTAL).Sum
        Dim GTotalVentaConIva As Decimal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
        Dim GTotalVentaSinIva = GTotalVentaConIva / 1.13
        Dim IvaErrado As Decimal = GTotalVentaConIva - GTotalVentaSinIva
        Dim VentaBruta As Decimal = GTotalVentaConIva - IvaErrado '- GTotalDescuentos

        'Dim VentasConDescuentoSinIva As Decimal = VentaBruta - GTotalDescuentos
        'Dim GTotal As Decimal = VentasConDescuentoSinIva * 1.13

        'Dim IVApxp As Decimal = GTotal - VentasConDescuentoSinIva
        Dim VentaB As Decimal = VentaBruta
        Return VentaB
    End Function
    Private Function MontoDevolucion(ByVal SUC_CLAVE As Long) As Decimal
        Dim Devolucion As Decimal = 0
        With MovimientosDeProductos.VS_MOVIMIENTO33PROD
            For Each tableRow As MovimientosDeProductosData.VS_MOVIMIENTO33PRODRow In .Rows
                If tableRow.SUC_CLAVE = SUC_CLAVE Then
                    If tableRow.IsTOTALNull Then
                        Devolucion = 0
                    Else
                        Devolucion = CDec(Format(tableRow.TOTAL, "#.00"))
                    End If

                    Exit For
                End If
            Next
            Return Devolucion
        End With
    End Function
    Private Function MontoTotalDevolucion() As Decimal
        Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_MOVIMIENTO33PROD Select p.TOTAL).Sum
        Return CDec(Format(GTotal, "#.00"))
    End Function
    Private Function GetSAPCentroCosto(ByVal SUC_CLAVE As Long) As String
        Dim SapSucursal As String = String.Empty
        With Maestro.ODS_SUCURSALES
            For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
                If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
                    SapSucursal = tableRow.SAP_CENTROCOSTO
                    Exit For
                End If
            Next
            Return SapSucursal
        End With
    End Function
    Private Function GetSAPCentroBeneficio(ByVal SUC_CLAVE As Long) As String
        Dim SapSucursal As String = String.Empty
        With Maestro.ODS_SUCURSALES
            For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
                If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
                    SapSucursal = tableRow.SAP_CENTROBENEFICIO.Trim
                    Exit For
                End If
            Next
            Return SapSucursal
        End With
    End Function
    Private Function GetSAPCentroGastos(ByVal SUC_CLAVE As Long) As String
        Dim SapSucursal As String = String.Empty
        With Maestro.ODS_SUCURSALES
            For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
                If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
                    SapSucursal = tableRow.SAP_CENTROGASTO.Trim
                    Exit For
                End If
            Next
            Return SapSucursal
        End With
    End Function
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
    Private Function CXCEmpleados() As Decimal
        Dim GTtotal As Decimal
        With MovimientosDeProductos.CXCEMPLEADOS
            For Each tRow As MovimientosDeProductosData.CXCEMPLEADOSRow In .Rows
                If Not tRow.IsTOTALNull Then
                    GTtotal = GTtotal + tRow.TOTAL
                End If
            Next
        End With
        Return GTtotal
    End Function
#End Region
    '#Region "Transformar"

    '    Private Sub T2(ByVal MOV_Clave As Integer, ByVal SegregaSucursal As Boolean, ByVal Beneficia As Boolean)

    '        With MovimientosProducto.Tables(0)
    '            For Each tablerow As DataRow In .Rows
    '                If CInt(tablerow("MOV_CLAVE").ToString) = MOV_Clave Then

    '                    With MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO
    '                        Dim NewDataRow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.ODS_MOVIMIENTOPRODUCTO_SEGREGADORow = .NewODS_MOVIMIENTOPRODUCTO_SEGREGADORow

    '                        With NewDataRow
    '                            .MOV_CLAVE = MOV_Clave
    '                            If SegregaSucursal Then
    '                                If Beneficia Then
    '                                    .SAP_CENTROCOSTO = GetCentroBeneficio(tablerow("SUC_CLAVE").ToString.Trim)
    '                                    .ESBENEFICIO = True
    '                                Else
    '                                    .SAP_CENTROCOSTO = GetCentroCosto(tablerow("SUC_CLAVE").ToString.Trim)
    '                                    .ESBENEFICIO = False
    '                                End If

    '                            Else
    '                                .SAP_CENTROCOSTO = Format(InfoCache.PaisClave, "000")
    '                                .ESBENEFICIO = False
    '                            End If
    '                            .TOTAL = GetCostoProducto(CLng(tablerow("PRO_CLAVE").ToString.Trim)) * CDec(tablerow("DMP_CANTID").ToString())
    '                        End With
    '                        .AddODS_MOVIMIENTOPRODUCTO_SEGREGADORow(NewDataRow)
    '                    End With
    '                End If
    '            Next
    '        End With
    '        With VerDatosForm.Grid_Segregado
    '            .DataSource = MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO.DefaultView

    '        End With
    '        VerDatosForm.TotalMovimientosLabel.Text = MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO.DefaultView.Count
    '    End Sub
    '    Private Function GetCentroCosto(ByVal SUC_CLAVE As String) As String
    '        With SucursalesPais.Tables(0)
    '            For Each tableRow As DataRow In .Rows
    '                If tableRow("SUCURSAL_CLAVE").ToString.Trim = SUC_CLAVE Then
    '                    Return tableRow("SAP_CENTROCOSTO").ToString
    '                    Exit For
    '                End If
    '            Next
    '            Return String.Empty
    '        End With
    '    End Function
    '    Private Function GetCentroBeneficio(ByVal SUC_CLAVE As String) As String
    '        With SucursalesPais.Tables(0)
    '            For Each tableRow As DataRow In .Rows
    '                If tableRow("SUCURSAL_CLAVE").ToString.Trim = SUC_CLAVE Then
    '                    Return tableRow("SAP_CENTROBENEFICIO").ToString
    '                    Exit For
    '                End If
    '            Next
    '            Return String.Empty
    '        End With
    '    End Function
    '    
    '    Private Sub T3()
    '        'Agrupar MovimientoSegregado by centro de costo an movclave


    '        Dim q = From p In MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO _
    '          Group p By p.SAP_CENTROCOSTO, p.MOV_CLAVE, p.ESBENEFICIO Into g = Group _
    '          Select New With {g, .monto = g.Sum(Function(p) p.TOTAL), .cuenta = g.Count(Function(p) p.MOV_CLAVE)}

    '        For Each Secuencia In q
    '            With MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO

    '                Dim NewDataRow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.ODS_MOVIMIENTOPRODUCTO_AGRUPADORow = .NewODS_MOVIMIENTOPRODUCTO_AGRUPADORow
    '                With NewDataRow
    '                    .MOV_CLAVE = Secuencia.g(0).MOV_CLAVE
    '                    .SAP_CENTROCOSTO = Secuencia.g(0).SAP_CENTROCOSTO
    '                    .MONTO = Secuencia.monto
    '                    .CUENTA = Secuencia.cuenta
    '                    .ESBENEFICIO = Secuencia.g(0).ESBENEFICIO
    '                End With

    '                .AddODS_MOVIMIENTOPRODUCTO_AGRUPADORow(NewDataRow)
    '            End With
    '        Next
    '        With VerDatosForm.Grid_Agrupado
    '            .DataSource = MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO.DefaultView
    '        End With

    '    End Sub

    '    Private Function GetDebitoMovimiento(ByVal MOV_CLAVE As Long) As String
    '        Dim NumeroCuenta As String = String.Empty
    '        Dim movimientos = From catalogo In CatalogoMovimientos.ODS_CATALOGO_MOVIMIENTOSPROD _
    '           Where catalogo.MOV_CLAVE = MOV_CLAVE
    '        For Each movimiento In movimientos
    '            NumeroCuenta = movimiento.CUENTA_CONTABLE_DR
    '        Next
    '        Return NumeroCuenta
    '    End Function
    '    Private Function GetCreditoMovimiento(ByVal MOV_CLAVE As Long) As String
    '        Dim NumeroCuenta As String = String.Empty
    '        Dim movimientos = From catalogo In CatalogoMovimientos.ODS_CATALOGO_MOVIMIENTOSPROD _
    '           Where catalogo.MOV_CLAVE = MOV_CLAVE
    '        For Each movimiento In movimientos
    '            NumeroCuenta = movimiento.CUENTA_CONTABLE_CR
    '        Next
    '        Return NumeroCuenta
    '    End Function
    '    Private Function GetRefDocNo(ByVal MOV_CLAVE As Long) As String
    '        Dim Cadena As New StringBuilder
    '        Cadena.Append(GetNombrePolizaCorto(MOV_CLAVE)).Append(GetPeriodo)

    '        Return Cadena.ToString

    '    End Function
    '    Private Function GetRefHeader(ByVal MOV_CLAVE As Long) As String
    '        Dim Cadena As New StringBuilder
    '        Cadena.Append(GetNombrePolizaLargo(MOV_CLAVE)).Append(GetPeriodo)

    '        Return Cadena.ToString

    '    End Function
    '    Private Function GetNombrePolizaCorto(ByVal MOV_CLAVE As Long) As String
    '        Dim nombreCortoPoliza As String = String.Empty
    '        Dim movimientos = From catalogo In CatalogoMovimientos.ODS_CATALOGO_MOVIMIENTOSPROD _
    '           Where catalogo.MOV_CLAVE = MOV_CLAVE
    '        For Each movimiento In movimientos
    '            If movimiento.IsNOMBRE_POLIZA_CORTONull Then
    '                nombreCortoPoliza = String.Empty
    '            Else
    '                nombreCortoPoliza = movimiento.NOMBRE_POLIZA_CORTO
    '            End If

    '        Next
    '        Return nombreCortoPoliza
    '    End Function
    '    Private Function GetNombrePolizaLargo(ByVal MOV_CLAVE As Long) As String
    '        Dim nombreCortoPoliza As String = String.Empty
    '        Dim movimientos = From catalogo In CatalogoMovimientos.ODS_CATALOGO_MOVIMIENTOSPROD _
    '           Where catalogo.MOV_CLAVE = MOV_CLAVE
    '        For Each movimiento In movimientos
    '            nombreCortoPoliza = movimiento.NOMBRE_POLIZA
    '        Next
    '        Return nombreCortoPoliza
    '    End Function
    '    Private Function GetPeriodo() As String
    '        Dim ComposicionPeriodo As String = String.Empty
    '        Dim fechas = From micalendario In Calendario.ODS_CALENDARIO
    '           Where micalendario.CFE_FECHA = FechaHasta.Value.Date

    '        For Each movimiento In fechas
    '            ComposicionPeriodo = "A" & _
    '                Format(movimiento.CFE_EJERCICIO, "0000").Remove(0, 2) & _
    '                "P" & _
    '                Format(movimiento.CFE_PERIODO, "00") & _
    '                "S" & _
    '                Format(movimiento.CFE_SEMANA, "0")
    '        Next
    '        Return ComposicionPeriodo


    '    End Function
    '#End Region
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
    Private Sub ProcesarButton_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcesarButton.Click
        RetrieveData()
        'Me.Close()
    End Sub

    Private Sub VerButton_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerButton.Click
        VerDatosForm.ShowDialog()

    End Sub
#End Region


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
