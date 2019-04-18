Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Data.Common


Public Class MainForm
    Private ModuloActual As String = "ITT"

    Private Inventario As IITData
    Private UsuarioInfo As UsuariosData
    Private NoSecuencia As Long
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        'RestoreWindowSettings()
        InfoCache.ConnectionString = "Data Source=ODSCA;User Id=ODSCA;Password=PRO56KAL"

        If DisplayLoginForm() = Windows.Forms.DialogResult.OK Then
            My.Application.DoEvents()
            CargarDatosUsuario()

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
                    InfoCache.MonedaClave = tablerow.MONEDA_CLAVE
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

        CargarInventario()
       
        Me.Cursor = Cursors.Default
        StatusLabelDisplay("Proceso completado: " & Date.Now.ToString)
        My.Application.DoEvents()
    End Sub
    'Sub CargarDatosUsuario()
    '    StatusLabelDisplay("Cargando datos del usuario")
    '    My.Application.DoEvents()

    '    UsuarioInfo = (New Usuarios).SelectDataUsuarios()
    '    If UsuarioInfo.DIC_USUARIOS.Count > 0 Then
    '        With UsuarioInfo.DIC_USUARIOS

    '            For Each tablerow As UsuariosData.DIC_USUARIOSRow In .Rows
    '                Me.NombreUsuario.Text = tablerow.NOMBRE
    '                Me.NombrePais.Text = tablerow.PAIS_NOMBRE
    '                Me.Moneda.Text = tablerow.MONEDA
    '                Me.ClaveCia_Label.Text = tablerow.CLAVE_CIA
    '                InfoCache.PaisClave = tablerow.PAIS_CLAVE
    '                InfoCache.MonedaClave = tablerow.MONEDA_CLAVE
    '                InfoCache.CurrencyCode = tablerow.MONEDA
    '                InfoCache.ClaveCompania = tablerow.CLAVE_CIA

    '            Next

    '        End With
    '        Me.ProcesarButton.Enabled = True
    '        StatusLabelDisplay("Listo. Presione boton <Procesar> para iniciar.")
    '        My.Application.DoEvents()
    '    Else
    '        StatusLabelDisplay("Datos no encontrados")
    '        My.Application.DoEvents()
    '    End If

    'End Sub

    'Private Sub CargarInventario()
    '    NoSecuencia = 0
    '    'Dim Secuencia As OSDMaestroData
    '    'StatusLabelDisplay("Cargando secuencia del pais")
    '    'My.Application.DoEvents()
    '    'Secuencia = (New PDCVDatos).SecuenciaSelectData()
    '    'InfoCache.Secuencia = Secuencia.VS_SECUENCIAPAIS(0).SECUENCIA
    'End Sub
    Private Sub CargarInventario()
        StatusLabelDisplay("Cargando  liquidaciones")
        My.Application.DoEvents()
        Inventario = (New IITDatos).InventarioSelectData()
        Me.GridDatos.DataSource = Inventario.MaestroInventario
        Me.GridDetalles.DataSource = Inventario.DetalleInventario
        'With VerDatosForm.Grid_Liquidaciones
        '    
        'End With

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
    'Private Sub T1()

    '    With CatalogoPolizas.VS_CATALOGOPOLIZAS
    '        For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow In .Rows
    '            NoSecuencia += 1
    '            StatusLabelDisplay("Procesando " & tableRow.DESCRIPCION)
    '            My.Application.DoEvents()
    '            Select Case tableRow.POL_CLAVE
    '                'Case 1, 2
    '                Case 1 To 5, 10, 11, 14 To 18

    '                    Select Case tableRow.TIPO
    '                        Case "D"
    '                            T2Carga(tableRow)
    '                        Case "V"

    '                            T2Carga(tableRow)
    '                        Case "O"

    '                            T2Otros(tableRow)
    '                        Case "B"
    '                            T2Bancos(tableRow)

    '                    End Select
    '                Case Else

    '            End Select




    '        Next

    '    End With
    '    With VerDatosForm.Grid_PolizasC
    '        .DataSource = Procesados.PolizasC.DefaultView
    '    End With
    'End Sub
    'Private Sub T2Carga(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)


    '    With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
    '        For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
    '            If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then
    '                Dim SumaResta As Decimal = tableRow.SUMAR - tableRow.RESTAR
    '                Select Case tableRow.TMD_CLAVE
    '                    Case 10
    '                        If tableRow.NIVEL_AGRUPA = 1 Then
    '                            With MovimientosDeProductos.VS_CARGA
    '                                For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
    '                                    Dim MontoSucursal As Decimal = tRow.TOTAL - MontoDevolucion(tRow.SUC_CLAVE)
    '                                    If SumaResta <> 0 Then
    '                                        MontoSucursal = (tRow.TOTAL + SumaResta)
    '                                    End If
    '                                    MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                                    MontoSucursal = MontoSucursal / tableRow.DIVIDIR
    '                                    With Procesados.PolizasC
    '                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                        With NewDataRow
    '                                            If tableRow.CENTRO = "B" Then
    '                                                .SAP_CENTROCOSTO = GetSAPCentroBeneficio(tRow.SUC_CLAVE)
    '                                            Else
    '                                                .SAP_CENTROCOSTO = GetSAPCentroCosto(tRow.SUC_CLAVE)
    '                                            End If
    '                                            If tableRow.IsCUENTA_DRNull Then
    '                                                .CUENTADR = String.Empty
    '                                                .MONTODR = 0
    '                                                .CUENTACR = tableRow.CUENTA_CR
    '                                                .MONTOCR = MontoSucursal

    '                                            ElseIf tableRow.IsCUENTA_CRNull Then
    '                                                .CUENTADR = tableRow.CUENTA_DR
    '                                                .MONTODR = MontoSucursal
    '                                                .CUENTACR = String.Empty
    '                                                .MONTOCR = 0

    '                                            End If
    '                                            .Secuencia = NoSecuencia
    '                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                            .CENTRO = tableRow.CENTRO
    '                                            .SUC_CLAVE = tRow.SUC_CLAVE
    '                                        End With
    '                                        .AddPolizasCRow(NewDataRow)
    '                                    End With

    '                                Next
    '                            End With

    '                        Else
    '                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
    '                            GTotal = GTotal - MontoTotalDevolucion()
    '                            If SumaResta <> 0 Then
    '                                GTotal = GTotal + SumaResta
    '                            End If
    '                            GTotal = GTotal * tableRow.MULTIPLICAR
    '                            GTotal = GTotal / tableRow.DIVIDIR
    '                            If GTotal > 0 Then
    '                                With Procesados.PolizasC
    '                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                    With NewDataRow
    '                                        .SAP_CENTROCOSTO = String.Empty

    '                                        If tableRow.IsCUENTA_DRNull Then
    '                                            .CUENTADR = String.Empty
    '                                            .MONTODR = 0
    '                                            .CUENTACR = tableRow.CUENTA_CR
    '                                            .MONTOCR = GTotal

    '                                        ElseIf tableRow.IsCUENTA_CRNull Then
    '                                            .CUENTADR = tableRow.CUENTA_DR
    '                                            .MONTODR = GTotal
    '                                            .CUENTACR = String.Empty
    '                                            .MONTOCR = 0

    '                                        End If
    '                                        .Secuencia = NoSecuencia
    '                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                        .CENTRO = tableRow.CENTRO
    '                                    End With

    '                                    .AddPolizasCRow(NewDataRow)
    '                                End With
    '                            End If


    '                        End If
    '                    Case 16   'TRADEPROMOTIONS

    '                        If tableRow.NIVEL_AGRUPA = 1 Then


    '                            With MovimientosDeProductos.VS_TRADEPROMOTIONS
    '                                For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
    '                                    Dim MontoSucursal As Decimal = tRow.TOTAL
    '                                    If SumaResta <> 0 Then
    '                                        MontoSucursal = (tRow.TOTAL + SumaResta)
    '                                    End If
    '                                    MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                                    MontoSucursal = MontoSucursal / tableRow.DIVIDIR
    '                                    With Procesados.PolizasC
    '                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                        With NewDataRow
    '                                            .SAP_CENTROCOSTO = GetSAPCentroBeneficio(tRow.SUC_CLAVE)
    '                                            If tableRow.IsCUENTA_DRNull Then
    '                                                .CUENTADR = String.Empty
    '                                                .MONTODR = 0
    '                                                .CUENTACR = tableRow.CUENTA_CR
    '                                                .MONTOCR = MontoSucursal

    '                                            ElseIf tableRow.IsCUENTA_CRNull Then
    '                                                .CUENTADR = tableRow.CUENTA_DR
    '                                                .MONTODR = MontoSucursal
    '                                                .CUENTACR = String.Empty
    '                                                .MONTOCR = 0

    '                                            End If
    '                                            .Secuencia = NoSecuencia
    '                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                            .CENTRO = tableRow.CENTRO
    '                                            .SUC_CLAVE = tRow.SUC_CLAVE
    '                                        End With
    '                                        .AddPolizasCRow(NewDataRow)
    '                                    End With

    '                                Next
    '                            End With

    '                        Else
    '                            Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum
    '                            If SumaResta <> 0 Then
    '                                GTotal = GTotal + SumaResta
    '                            End If
    '                            GTotal = GTotal * tableRow.MULTIPLICAR
    '                            GTotal = GTotal / tableRow.DIVIDIR
    '                            If GTotal > 0 Then
    '                                With Procesados.PolizasC
    '                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                    With NewDataRow
    '                                        .SAP_CENTROCOSTO = String.Empty
    '                                        If tableRow.IsCUENTA_DRNull Then
    '                                            .CUENTADR = String.Empty
    '                                            .MONTODR = 0
    '                                            .CUENTACR = tableRow.CUENTA_CR
    '                                            .MONTOCR = GTotal

    '                                        ElseIf tableRow.IsCUENTA_CRNull Then
    '                                            .CUENTADR = tableRow.CUENTA_DR
    '                                            .MONTODR = GTotal
    '                                            .CUENTACR = String.Empty
    '                                            .MONTOCR = 0

    '                                        End If
    '                                        .Secuencia = NoSecuencia
    '                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                        .CENTRO = tableRow.CENTRO

    '                                    End With
    '                                    .AddPolizasCRow(NewDataRow)
    '                                End With
    '                            End If
    '                        End If
    '                End Select
    '            End If
    '        Next
    '    End With
    'End Sub
    'Private Sub T2Otros(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
    '    With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
    '        For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

    '            If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then
    '                Dim SumaResta As Decimal = tableRow.SUMAR - tableRow.RESTAR
    '                Dim Tmd As Decimal = tableRow.TMD_CLAVE

    '                If tableRow.NIVEL_AGRUPA = 1 Then


    '                    With Liquidaciones.VS_ODS_LIQUIDACIONES
    '                        For Each tRow As LiquidacionesData.VS_ODS_LIQUIDACIONESRow In .Rows
    '                            If tRow.TMD_CLAVE = Tmd Then


    '                                Dim MontoSucursal As Decimal = tRow.TOTAL
    '                                If SumaResta <> 0 Then
    '                                    MontoSucursal = (tRow.TOTAL + SumaResta)
    '                                End If
    '                                MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                                MontoSucursal = MontoSucursal / tableRow.DIVIDIR
    '                                With Procesados.PolizasC
    '                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                    With NewDataRow
    '                                        If tableRow.CENTRO = "B" Then
    '                                            .SAP_CENTROCOSTO = GetSAPCentroBeneficio(tRow.SUC_CLAVE)
    '                                        Else
    '                                            .SAP_CENTROCOSTO = GetSAPCentroCosto(tRow.SUC_CLAVE)
    '                                        End If


    '                                        If tableRow.IsCUENTA_DRNull Then
    '                                            .CUENTADR = String.Empty
    '                                            .MONTODR = 0
    '                                            .CUENTACR = tableRow.CUENTA_CR
    '                                            .MONTOCR = MontoSucursal

    '                                        ElseIf tableRow.IsCUENTA_CRNull Then
    '                                            .CUENTADR = tableRow.CUENTA_DR
    '                                            .MONTODR = MontoSucursal
    '                                            .CUENTACR = String.Empty
    '                                            .MONTOCR = 0

    '                                        End If
    '                                        .Secuencia = NoSecuencia
    '                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                        .CENTRO = tableRow.CENTRO
    '                                        .SUC_CLAVE = tRow.SUC_CLAVE
    '                                    End With
    '                                    .AddPolizasCRow(NewDataRow)
    '                                End With
    '                            End If
    '                        Next
    '                    End With

    '                ElseIf tableRow.NIVEL_AGRUPA = 0 Then
    '                    Dim GTotal As Decimal = (From p In Liquidaciones.VS_ODS_LIQUIDACIONES Where p.TMD_CLAVE = Tmd Select p.TOTAL).Sum
    '                    If SumaResta <> 0 Then
    '                        GTotal = GTotal + SumaResta
    '                    End If
    '                    GTotal = GTotal * tableRow.MULTIPLICAR
    '                    GTotal = GTotal / tableRow.DIVIDIR
    '                    If GTotal > 0 Then
    '                        With Procesados.PolizasC
    '                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                            With NewDataRow
    '                                .SAP_CENTROCOSTO = String.Empty
    '                                If tableRow.IsCUENTA_DRNull Then
    '                                    .CUENTADR = String.Empty
    '                                    .MONTODR = 0
    '                                    .CUENTACR = tableRow.CUENTA_CR
    '                                    .MONTOCR = GTotal

    '                                ElseIf tableRow.IsCUENTA_CRNull Then
    '                                    .CUENTADR = tableRow.CUENTA_DR
    '                                    .MONTODR = GTotal
    '                                    .CUENTACR = String.Empty
    '                                    .MONTOCR = 0

    '                                End If
    '                                .Secuencia = NoSecuencia
    '                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                .CENTRO = tableRow.CENTRO

    '                            End With
    '                            .AddPolizasCRow(NewDataRow)
    '                        End With
    '                    End If


    '                ElseIf tableRow.NIVEL_AGRUPA = 2 Then

    '                End If



    '            End If

    '        Next
    '    End With

    'End Sub
    'Private Sub T2Bancos(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
    '    With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
    '        For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

    '            If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then
    '                Dim SumaResta As Decimal = tableRow.SUMAR - tableRow.RESTAR
    '                Dim Tmd As Decimal = tableRow.TMD_CLAVE

    '                If tableRow.NIVEL_AGRUPA = 2 Then


    '                    With Liquidaciones.VS_LIQUIDACIONESBANCOS
    '                        For Each tRow As LiquidacionesData.VS_LIQUIDACIONESBANCOSRow In .Rows
    '                            If tRow.TMD_CLAVE = Tmd Then


    '                                Dim MontoSucursal As Decimal = tRow.TOTAL
    '                                If SumaResta <> 0 Then
    '                                    MontoSucursal = (tRow.TOTAL + SumaResta)
    '                                End If
    '                                MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                                MontoSucursal = MontoSucursal / tableRow.DIVIDIR
    '                                With Procesados.PolizasC
    '                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                    With NewDataRow
    '                                        If tableRow.CENTRO = "B" Then
    '                                            .SAP_CENTROCOSTO = GetSAPCentroBeneficio(tRow.SUC_CLAVE)
    '                                        Else
    '                                            .SAP_CENTROCOSTO = GetSAPCentroCosto(tRow.SUC_CLAVE)
    '                                        End If


    '                                        If tableRow.IsCUENTA_DRNull Then
    '                                            .CUENTADR = String.Empty
    '                                            .MONTODR = 0
    '                                            .CUENTACR = tableRow.CUENTA_CR
    '                                            .MONTOCR = MontoSucursal

    '                                        ElseIf tableRow.IsCUENTA_CRNull Then
    '                                            .CUENTADR = tableRow.CUENTA_DR
    '                                            .MONTODR = MontoSucursal
    '                                            .CUENTACR = String.Empty
    '                                            .MONTOCR = 0

    '                                        End If
    '                                        .Secuencia = NoSecuencia
    '                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                        .CENTRO = tableRow.CENTRO
    '                                        .SUC_CLAVE = tRow.SUC_CLAVE
    '                                        If Not tRow.IsMDI_NUMREMNull Then
    '                                            .MDI_NUMREM = tRow.MDI_NUMREM
    '                                        End If

    '                                    End With
    '                                    .AddPolizasCRow(NewDataRow)
    '                                End With
    '                            End If
    '                        Next
    '                    End With

    '                ElseIf tableRow.NIVEL_AGRUPA = 0 Then
    '                    Dim GTotal As Decimal = (From p In Liquidaciones.VS_LIQUIDACIONESBANCOS Where p.TMD_CLAVE = Tmd Select p.TOTAL).Sum
    '                    If SumaResta <> 0 Then
    '                        GTotal = GTotal + SumaResta
    '                    End If
    '                    GTotal = GTotal * tableRow.MULTIPLICAR
    '                    GTotal = GTotal / tableRow.DIVIDIR
    '                    If GTotal > 0 Then
    '                        With Procesados.PolizasC
    '                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                            With NewDataRow
    '                                .SAP_CENTROCOSTO = String.Empty
    '                                If tableRow.IsCUENTA_DRNull Then
    '                                    .CUENTADR = String.Empty
    '                                    .MONTODR = 0
    '                                    .CUENTACR = tableRow.CUENTA_CR
    '                                    .MONTOCR = GTotal

    '                                ElseIf tableRow.IsCUENTA_CRNull Then
    '                                    .CUENTADR = tableRow.CUENTA_DR
    '                                    .MONTODR = GTotal
    '                                    .CUENTACR = String.Empty
    '                                    .MONTOCR = 0

    '                                End If
    '                                .Secuencia = NoSecuencia
    '                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                .CENTRO = tableRow.CENTRO

    '                            End With
    '                            .AddPolizasCRow(NewDataRow)
    '                        End With
    '                    End If
    '                End If
    '            End If
    '        Next
    '    End With

    'End Sub
    'Private Sub T4()
    '    SapGLInterfase = New SAP_GLINTERFASEData
    '    Dim NoSecuencia As Long = 0
    '    With Procesados.PolizasC
    '        For Each tablerow As ProcesadasData.PolizasCRow In .Rows

    '            'definitiva()
    '            With SapGLInterfase.SAP_GL_INTERFASE

    '                ''test
    '                'With SapGLInterfase.SAP_GL_TEMP

    '                'definitiva()
    '                Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow

    '                ''test
    '                'Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_TEMPRow = .NewSAP_GL_TEMPRow

    '                With NewDataRow
    '                    .ESTATUS_SAP = "NEW"
    '                    .DOCUMENT_DATE = Date.Today
    '                    .DOCUMENT_TYPE = "ZI"
    '                    .REF_DOCUMENT_NUMBER = "B" & InfoCache.PaisClave & Format(tablerow.Secuencia, "000000")
    '                    .DOC_HEADER_TEXT = tablerow.DOC_HEADER_TEXT & GetPeriodo()
    '                    .SAP_COMPANY_CODE = InfoCache.ClaveCompania

    '                    If tablerow.CUENTADR.Length > 0 Then
    '                        .SAP_ACCOUNT = tablerow.CUENTADR
    '                    Else
    '                        .SAP_ACCOUNT = tablerow.CUENTACR
    '                    End If
    '                    If tablerow.CENTRO = "B" And tablerow.SAP_CENTROCOSTO.Length > 0 Then
    '                        .SAP_COST_CENTER = String.Empty
    '                        .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO
    '                    Else
    '                        .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO
    '                        .SAP_PROFIT_CENTER = String.Empty
    '                    End If
    '                    If Not tablerow.IsMDI_NUMREMNull Then
    '                        .SAP_SEGMENT = tablerow.MDI_NUMREM
    '                    Else
    '                        .SAP_SEGMENT = String.Empty
    '                    End If
    '                    .ENTERED_DR = tablerow.MONTODR
    '                    .ENTERED_CR = tablerow.MONTOCR
    '                    .ASSIGNMENT = String.Empty
    '                    .LINE_TEXT = String.Empty
    '                    .SAP_REF_DOC_NUMBER = String.Empty
    '                    .SAP_REF_MESSAGE = String.Empty
    '                    .ACCOUNTING_DATE = Date.Today
    '                    .CURRENCY_CODE = InfoCache.CurrencyCode
    '                    .LEDGER_GROUP = String.Empty
    '                    .SAP_TAX_INDICATOR = String.Empty

    '                End With
    '                Try
    '                    .AddSAP_GL_INTERFASERow(NewDataRow)
    '                    '.AddSAP_GL_TEMPRow(NewDataRow)

    '                    .AcceptChanges()
    '                Catch ex As Exception

    '                End Try
    '            End With
    '        Next

    '    End With

    '    ''definitiva
    '    With VerDatosForm.GRID_SAP_GL_INTERFASE
    '        .DataSource = SapGLInterfase.SAP_GL_INTERFASE
    '    End With
    '    With SapGLInterfase.SAP_GL_INTERFASE
    '        For Each tablerow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow In .Rows
    '            Dim result As Boolean = (New PDCVDatos).insert(tablerow)
    '        Next
    '    End With


    '    ''test
    '    'With VerDatosForm.GRID_SAP_GL_INTERFASE
    '    '    .DataSource = SapGLInterfase.SAP_GL_TEMP
    '    'End With
    '    'With SapGLInterfase.SAP_GL_TEMP
    '    '    For Each tablerow As SAP_GLINTERFASEData.SAP_GL_TEMPRow In .Rows
    '    '        Dim result As Boolean = (New PDCVDatos).insertInTemp(tablerow)
    '    '    Next
    '    'End With
    'End Sub
#End Region
    '#Region "LookUp"
    '    Private Function MontoDevolucion(ByVal SUC_CLAVE As Long) As Decimal
    '        Dim Devolucion As Decimal = 0
    '        With MovimientosDeProductos.VS_MOVIMIENTO33PROD
    '            For Each tableRow As MovimientosDeProductosData.VS_MOVIMIENTO33PRODRow In .Rows
    '                If tableRow.SUC_CLAVE = SUC_CLAVE Then
    '                    Devolucion = tableRow.TOTAL
    '                    Exit For
    '                End If
    '            Next
    '            Return Devolucion
    '        End With
    '    End Function
    '    Private Function MontoTotalDevolucion() As Decimal
    '        Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_MOVIMIENTO33PROD Select p.TOTAL).Sum
    '        Return GTotal
    '    End Function
    '    Private Function GetSAPCentroCosto(ByVal SUC_CLAVE As Long) As String
    '        Dim SapSucursal As String = String.Empty
    '        With Maestro.ODS_SUCURSALES
    '            For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
    '                If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
    '                    SapSucursal = tableRow.SAP_CENTROCOSTO
    '                    Exit For
    '                End If
    '            Next
    '            Return SapSucursal
    '        End With
    '    End Function
    '    Private Function GetSAPCentroBeneficio(ByVal SUC_CLAVE As Long) As String
    '        Dim SapSucursal As String = String.Empty
    '        With Maestro.ODS_SUCURSALES
    '            For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
    '                If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
    '                    SapSucursal = tableRow.SAP_CENTROBENEFICIO.Trim
    '                    Exit For
    '                End If
    '            Next
    '            Return SapSucursal
    '        End With
    '    End Function
    '    Private Function GetPeriodo() As String
    '        Dim ComposicionPeriodo As String = String.Empty
    '        Dim fechas = From micalendario In Maestro.ODS_CALENDARIO
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
    'Private Sub Save()
    '    Dim SelectString As New StringBuilder
    '    SelectString.Append("SELECT ")
    '    SelectString.Append("ESTATUS_SAP, ")
    '    SelectString.Append("DOCUMENT_DATE, ")
    '    SelectString.Append("DOCUMENT_TYPE, ")
    '    SelectString.Append("REF_DOCUMENT_NUMBER, ")
    '    SelectString.Append("DOC_HEADER_TEXT, ")
    '    SelectString.Append("SAP_COMPANY_CODE, ")
    '    SelectString.Append("SAP_ACCOUNT, ")
    '    SelectString.Append("SAP_COST_CENTER, ")
    '    SelectString.Append("SAP_PROFIT_CENTER, ")
    '    SelectString.Append("SAP_SEGMENT, ")
    '    SelectString.Append("ENTERED_DR, ")
    '    SelectString.Append("ENTERED_CR, ")
    '    SelectString.Append("ASSIGNMENT, ")
    '    SelectString.Append("LINE_TEXT, ")
    '    SelectString.Append("SAP_REF_DOC_NUMBER, ")
    '    SelectString.Append("SAP_REF_MESSAGE, ")
    '    SelectString.Append("ACCOUNTING_DATE, ")
    '    SelectString.Append("CURRENCY_CODE, ")
    '    SelectString.Append("LEDGER_GROUP, ")
    '    SelectString.Append("SAP_TAX_INDICATOR ")
    '    SelectString.Append("FROM PRODODS.SAP_GL_INTERFASE ")
    '    SelectString.Append("WHERE ")
    '    SelectString.Append("ESTATUS_SAP = 'A' ")

    '    Dim conn As OracleConnection = New OracleConnection
    '    Dim productsDataSet As DataSet
    '    conn.ConnectionString = InfoCache.ConnectionString
    '    conn.Open()
    '    Dim cmd As New OracleCommand
    '    cmd.Connection = conn

    '    Dim MaestroAdapter As OracleDataAdapter = New OracleDataAdapter(cmd)
    '    Dim MaestroDataSet As New OSDMaestroData

    '    'Try
    '    cmd.CommandText = SelectString.ToString
    '    cmd.CommandType = CommandType.Text

    '    'Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
    '    'p1.Value = InfoCache.PaisClave
    '    'cmd.Parameters.Add(p1)

    '    'Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
    '    'cmd.Parameters.Add(p2)
    '    Try
    '        MaestroAdapter.Fill(MaestroDataSet, "SAP_GL_INTERFASE")
    '        'With VerDatosForm.GRID_SAP_GL_INTERFASE
    '        '    .DataSource = SapGLInterfase.SAP_GL_INTERFASE
    '        'End With
    '        productsDataSet = SapGLInterfase.SAP_GL_INTERFASE.DataSet 'New DataSet("productsDataSet")
    '        VerDatosForm.GRID_SAP_GL_INTERFASE.DataSource = productsDataSet



    '        'productsDataSet.Merge(SapGLInterfase.SAP_GL_INTERFASE.DataSet, True)
    '        MaestroAdapter.Update(productsDataSet, "SAP_GL_INTERFASE")

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    cmd.Dispose()
    '    conn.Close()


    'End Sub

#End Region

#Region "Eventos"
    Private Sub ProcesarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcesarButton.Click
        RetrieveData()
    End Sub

    Private Sub VerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerButton.Click
        'VerDatosForm.ShowDialog()
        'With New VerDatosForm
        '    .ShowDialog()
        'End With
    End Sub
#End Region
End Class
