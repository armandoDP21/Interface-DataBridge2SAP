Imports System.Text

Public Class PolizasIngresosGT

    Public Event Mensaje(TextoMensaje As String)
    Dim start, finish, totalTime As Double

    Private ts As TimeSpan
    Private startDate As DateTime
    Private endDate As DateTime
    Private ModuloActual As String = "ICC"
    Private OrigenIngresosData As GTPolizasIngresosData
    Private SapGLInterfase As SAP_GLINTERFASEData
   
    Private NoPoliza As Long

    Public Function ProcesarCxC() As Integer
        start = Microsoft.VisualBasic.DateAndTime.Timer
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Proceso: " & "Pólizas de ingresos y gastos" & " de: " & InfoCache.FechaDesde.ToString & " a:" & InfoCache.FechaHasta.ToString)
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Iniciado a")

        InfoCache.UpdateError = String.Empty

        OrigenIngresosData = (New IngresosVentasDML).SelectData
        If InfoCache.UpdateError.Length > 0 Then
            MsgBox(InfoCache.UpdateError, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        End If
        SapGLInterfase = New SAP_GLINTERFASEData

        RaiseEvent Mensaje(Format(Date.Now, "R") & ">  " & "Creando pólizas.")
        T1()
        'DeleteDatos()
        RaiseEvent Mensaje(Format(Date.Now, "R") & ">  " & "Registrando pólizas.")
        'BalancearPoizas()
        'Dim recordsInesrtados As Integer = PersistInDatabase()

        'If recordsInesrtados = -1 Then
        '    totalTime = Microsoft.VisualBasic.DateAndTime.Timer - start
        '    RaiseEvent Mensaje(Format(Date.Now, "R") & ">  " & "La creación de las Pólizas de inventario" & " terminó satisfactoriamente.")
        '    RaiseEvent Mensaje(Format(Date.Now, "R") & "> Duración: " & SecondsToText(totalTime))
        'Else
        '    RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & "Pólizas de inventario" & "no se ejecutó. Debe procesarla nuevamente.")
        'End If
        RaiseEvent Mensaje("...............................................")
        Return 1
    End Function

#Region "Procesar datos"
    Private Sub T1()
        'Me.ProcesarButton.Enabled = False
        Dim MaxPolClave As Integer = GetMaxPolClave
        For registro As Integer = 1 To MaxPolClave
            InfoCache.Secuencia += 1
            ProcesarRegistro(registro)
        Next
    End Sub
   
    Private Sub ProcesarRegistro(InPolClave As Integer)
        Dim q = From p In OrigenIngresosData.POLIZASINGRESOS Where p.POL_CLAVE = InPolClave
        Dim Registro = q.First
        Select Case Registro.TIPO
            Case "A" 'mas de una entrada
                CrearRegistroA(q)

            Case "B" '2 entradas 
                CrearRegistroB(q)

            Case "C" 'c/detalle depositos
                CrearRegistroC(q)
            Case "D" 'poliza de ventas
                CrearRegistroD(q)
            Case "E" 'poliza de ventas
                CrearRegistroE(q)
        End Select
    End Sub
#Region "Tipo A"
    Private Sub CrearRegistroA(q As System.Data.EnumerableRowCollection(Of GTPolizasIngresosData.POLIZASINGRESOSRow))
        Dim IvaPorPagar As Decimal
        For Each linea In q
            Select Case linea.NIVEL_AGRUPA
                Case 0
                    Dim TotalMovimiento As Decimal
                    Select Case linea.ORIGENDEDATOS
                        Case "ODS_LIQUIDACIONES"
                            TotalMovimiento = GetTotalLiquidaciones(linea.MOVIMIENTOS)
                        Case "ODS_MOVIMIENTOPRODUCTO"
                            TotalMovimiento = GetTotalMovimientosProductos(linea.MOVIMIENTOS)
                    End Select
                    IvaPorPagar = TotalMovimiento * 0.12 / 1.12
                    Add2SAP_GL_INTERFASE(linea, TotalMovimiento)

                Case 1 'registrar por valor de sucursal

                    Select Case linea.ORIGENDEDATOS
                        Case "ODS_LIQUIDACIONES"

                            Dim Movs As String = linea.MOVIMIENTOS
                            'Dim Incluir As String() = New String() {Movs}
                            Dim Incluir As String = linea.MOVIMIENTOS
                            Dim sucursales = From p In OrigenIngresosData.ODS_LIQUIDACIONES
                                Where Incluir.Contains(p.TMD_CLAVE.ToString) _
                                Group By p.SUC_CLAVE Into g = Group _
                                Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}


                            For Each sucursal In sucursales
                                Dim monto As Decimal = sucursal.GTotal / 1.12
                                Dim sucClave As String = sucursal.g(0).SUC_CLAVE
                                IvaPorPagar += (sucursal.GTotal * 0.12 / 1.12)
                                AddSucursal2SAP_GL_INTERFASE(linea, sucClave, monto)
                            Next


                        Case "ODS_MOVIMIENTOPRODUCTO"

                            Dim Movs As String = linea.MOVIMIENTOS
                            Dim Incluir As String() = New String() {Movs}

                            Dim sucursales = From p In OrigenIngresosData.ODS_MOVIMPRODUCTOS
                                Where Incluir.Contains(p.MOV_CLAVE.ToString) _
                                Group By p.SUC_CLAVE Into g = Group _
                                Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}

                            For Each sucursal In sucursales
                                Dim monto As Decimal = sucursal.GTotal
                                Dim sucClave As String = sucursal.g(0).SUC_CLAVE

                                AddSucursal2SAP_GL_INTERFASE(linea, sucClave, monto)
                            Next

                    End Select

                Case 4 ' IVA
                    Add2SAP_GL_INTERFASE(linea, IvaPorPagar)
            End Select
        Next

    End Sub
    Private Sub AddSucursal2SAP_GL_INTERFASE(linea As GTPolizasIngresosData.POLIZASINGRESOSRow, sucClave As String, monto As Decimal)
        With SapGLInterfase.SAP_GL_INTERFASE
            Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
            With NewDataRow
                .ESTATUS_SAP = InfoCache.ESTATUS_SAP
                .DOCUMENT_DATE = Date.Today
                .DOCUMENT_TYPE = "ZI"
                .REF_DOCUMENT_NUMBER = "B" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")
                .DOC_HEADER_TEXT = linea.DOC_HEADER_TEXT & InfoCache.APS
                .SAP_COMPANY_CODE = InfoCache.ClaveCompania

                .SAP_ACCOUNT = linea.SAP_ACCOUNT
                Select Case linea.SAP_ACCOUNT.Substring(0, 1)
                    Case "1", "2", "3"
                        .SAP_PROFIT_CENTER = String.Empty
                        .SAP_COST_CENTER = String.Empty
                    Case "4", "5", "7"
                        .SAP_PROFIT_CENTER = GetSAPCentroBeneficio(sucClave)
                        .SAP_COST_CENTER = String.Empty
                    Case "6"
                        .SAP_COST_CENTER = GetSAPCentroCostos(sucClave)
                        .SAP_PROFIT_CENTER = String.Empty
                End Select

                .SAP_SEGMENT = String.Empty
                Select Case linea.DRCR
                    Case "D"
                        .ENTERED_DR = monto
                    Case "C"
                        .ENTERED_CR = monto
                End Select
                .ASSIGNMENT = String.Empty

                .LINE_TEXT = String.Empty
                .SAP_REF_DOC_NUMBER = String.Empty
                .SAP_REF_MESSAGE = String.Empty
                .ACCOUNTING_DATE = InfoCache.FechaHasta
                .CURRENCY_CODE = InfoCache.CurrencyCode
                .LEDGER_GROUP = String.Empty
                .SAP_TAX_INDICATOR = String.Empty

            End With
            .AddSAP_GL_INTERFASERow(NewDataRow)
        End With
    End Sub

#End Region
#Region "Tipo B"
    Private Sub CrearRegistroB(q As System.Data.EnumerableRowCollection(Of GTPolizasIngresosData.POLIZASINGRESOSRow))
        Dim Registro = q.First
        Dim TotalMovimiento As Decimal = GetTotalLiquidaciones(Registro.MOVIMIENTOS)

        For Each linea In q
            Add2SAP_GL_INTERFASE(linea, TotalMovimiento)
        Next

    End Sub
    Private Sub Add2SAP_GL_INTERFASE(secuencia As GTPolizasIngresosData.POLIZASINGRESOSRow, TotalMovimiento As Decimal, Optional ByVal MontoLinea As Decimal = 0)
        With SapGLInterfase.SAP_GL_INTERFASE
            Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
            With NewDataRow
                .ESTATUS_SAP = InfoCache.ESTATUS_SAP
                .DOCUMENT_DATE = Date.Today
                .DOCUMENT_TYPE = "ZI"
                .REF_DOCUMENT_NUMBER = "B" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")
                .DOC_HEADER_TEXT = secuencia.DOC_HEADER_TEXT & InfoCache.APS
                .SAP_COMPANY_CODE = InfoCache.ClaveCompania

                .SAP_ACCOUNT = secuencia.SAP_ACCOUNT
                If Not secuencia.IsCENTRO_FIJONull Then
                    .SAP_PROFIT_CENTER = secuencia.CENTRO_FIJO
                Else
                    'Select Case secuencia.SAP_ACCOUNT.Substring(0, 1)
                    '    Case "1", "2", "3"
                    '        .SAP_PROFIT_CENTER = String.Empty
                    '        .SAP_COST_CENTER = String.Empty
                    '    Case "4", "5", "7"
                    '        .SAP_PROFIT_CENTER = GetSAPCentroBeneficio(secuencia.su)
                    '        .SAP_COST_CENTER = String.Empty
                    '    Case "6"
                    '        .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO.ToString
                    '        .SAP_PROFIT_CENTER = String.Empty
                    'End Select
                End If


              
                'Select Case secuencia.DRCR
                '        Case "D"

                '            Select Case secuencia.SAP_ACCOUNT.Substring(0, 1)
                '                Case "1", "2", "3"
                '                    .SAP_PROFIT_CENTER = String.Empty
                '                    .SAP_COST_CENTER = String.Empty
                '                Case "4", "5", "7"
                '                    .SAP_PROFIT_CENTER = secuencia.CENTRO_FIJO
                '                    .SAP_COST_CENTER = String.Empty
                '                    'Case "6"
                '                    '    .SAP_COST_CENTER = secuencia.CENTRO_FIJO
                '                    '    .SAP_PROFIT_CENTER = String.Empty
                '            End Select
                '        Case "C"

                '            Select Case secuencia.SAP_ACCOUNT.Substring(0, 1)
                '                Case "1", "2", "3"
                '                    .SAP_PROFIT_CENTER = String.Empty
                '                    .SAP_COST_CENTER = String.Empty
                '                Case "4", "5", "7"
                '                    .SAP_PROFIT_CENTER = secuencia.CENTRO_FIJO
                '                    .SAP_COST_CENTER = String.Empty
                '                    'Case "6"
                '                    '    .SAP_COST_CENTER = secuencia.CENTRO_FIJO
                '                    '    .SAP_PROFIT_CENTER = String.Empty
                '            End Select

                '    End Select




                    .SAP_SEGMENT = String.Empty

                    Select Case secuencia.DRCR
                        Case "D"
                            .ENTERED_DR = TotalMovimiento
                        Case "C"
                            .ENTERED_CR = TotalMovimiento
                    End Select



                    '------------------para depositos
                    'If Not tablerow.IsASSIGNMENTNull Then
                    '    .ASSIGNMENT = tablerow.ASSIGNMENT
                    'Else
                    '    .ASSIGNMENT = String.Empty
                    'End If
                    '-------------------------------------


                    .LINE_TEXT = String.Empty
                    .SAP_REF_DOC_NUMBER = String.Empty
                    .SAP_REF_MESSAGE = String.Empty
                    '.ACCOUNTING_DATE = Date.Today
                    .ACCOUNTING_DATE = InfoCache.FechaHasta
                    .CURRENCY_CODE = InfoCache.CurrencyCode
                    .LEDGER_GROUP = String.Empty
                    .SAP_TAX_INDICATOR = String.Empty

            End With
            .AddSAP_GL_INTERFASERow(NewDataRow)
        End With

    End Sub
#End Region
#Region "Tipo C - DEPOSITOS"
    Private Sub CrearRegistroC(q As System.Data.EnumerableRowCollection(Of GTPolizasIngresosData.POLIZASINGRESOSRow))
        Dim Registro = q.First
        Dim TotalMovimiento As Decimal

        'calcula el movimiento del 1er registro de la poliza
        TotalMovimiento = GetTotalBanca(Registro.MOVIMIENTOS)
        If TotalMovimiento > 0 Then
            Dim Transacciones = From c In OrigenIngresosData.POLIZASINGRESOS _
                            Where c.POL_CLAVE = Registro.POL_CLAVE

            For Each linea In Transacciones
                Dim Mov2 As Decimal = CDec(linea.MOVIMIENTOS)
                If linea.NIVEL_AGRUPA = 0 Then

                    Add2SAP_GL_INTERFASE(linea, TotalMovimiento)
                Else
                    Dim Depositos = From p In OrigenIngresosData.ODS_BANCA
                                                Where p.TMD_CLAVE = Mov2

                    For Each Deposito In Depositos
                        AddDeposito2SAP_GL_INTERFASE(linea, Deposito)
                    Next
                End If
            Next

        End If
    End Sub
    Private Sub AddDeposito2SAP_GL_INTERFASE(linea As GTPolizasIngresosData.POLIZASINGRESOSRow, Deposito As GTPolizasIngresosData.ODS_BANCARow)
        With SapGLInterfase.SAP_GL_INTERFASE
            Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
            With NewDataRow
                .ESTATUS_SAP = InfoCache.ESTATUS_SAP
                .DOCUMENT_DATE = Date.Today
                .DOCUMENT_TYPE = "ZI"
                .REF_DOCUMENT_NUMBER = "B" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")
                .DOC_HEADER_TEXT = linea.DOC_HEADER_TEXT & InfoCache.APS
                .SAP_COMPANY_CODE = InfoCache.ClaveCompania

                .SAP_ACCOUNT = linea.SAP_ACCOUNT


                'Select Case linea.DRCR
                '    Case "D"

                '        Select Case linea.SAP_ACCOUNT.Substring(0, 1)
                '            Case "1", "2", "3"
                '                .SAP_PROFIT_CENTER = String.Empty
                '                .SAP_COST_CENTER = String.Empty
                '            Case "4", "5", "7"
                '                .SAP_PROFIT_CENTER = linea.CENTRO_FIJO
                '                .SAP_COST_CENTER = String.Empty
                '                'Case "6"
                '                '    .SAP_COST_CENTER = linea.CENTRO_FIJO
                '                '    .SAP_PROFIT_CENTER = String.Empty
                '        End Select
                '    Case "C"

                '        Select Case linea.SAP_ACCOUNT.Substring(0, 1)
                '            Case "1", "2", "3"
                '                .SAP_PROFIT_CENTER = String.Empty
                '                .SAP_COST_CENTER = String.Empty
                '            Case "4", "5", "7"
                '                .SAP_PROFIT_CENTER = linea.CENTRO_FIJO
                '                .SAP_COST_CENTER = String.Empty
                '                'Case "6"
                '                '    .SAP_COST_CENTER = linea.CENTRO_FIJO
                '                '    .SAP_PROFIT_CENTER = String.Empty
                '        End Select

                'End Select

                .SAP_SEGMENT = String.Empty
                Select Case linea.DRCR
                    Case "D"
                        .ENTERED_DR = Deposito.TOTAL
                    Case "C"
                        .ENTERED_CR = Deposito.TOTAL
                End Select

                If Not Deposito.IsMDI_NUMREMNull Then
                    If Deposito.MDI_NUMREM.Length > 12 Then
                        .ASSIGNMENT = Deposito.MDI_NUMREM.Substring(0, 12)
                    Else
                        .ASSIGNMENT = Deposito.MDI_NUMREM
                    End If
                End If

                .LINE_TEXT = String.Empty
                .SAP_REF_DOC_NUMBER = String.Empty
                .SAP_REF_MESSAGE = String.Empty
                '.ACCOUNTING_DATE = Date.Today
                .ACCOUNTING_DATE = InfoCache.FechaHasta
                .CURRENCY_CODE = InfoCache.CurrencyCode
                .LEDGER_GROUP = String.Empty
                .SAP_TAX_INDICATOR = String.Empty

            End With
            .AddSAP_GL_INTERFASERow(NewDataRow)
        End With
    End Sub

#End Region
#Region "Tipo D - VENTAS"
    Private Sub CrearRegistroD(q As System.Data.EnumerableRowCollection(Of GTPolizasIngresosData.POLIZASINGRESOSRow))
        Dim MontoVentasFAE = (From p In OrigenIngresosData.VENTASFAE Select p.BRUTO).Sum
        Dim Neto = MontoVentasFAE / 1.12
        Dim IvaFAE = MontoVentasFAE - Neto
        'Dim MontoVentasLiquidacion As Decimal = GetTotalLiquidaciones(95)
        Dim IvaLiquidaciones As Decimal

        For Each linea In q
            Select Case linea.POSICION
                Case 1
                    Add2SAP_GL_INTERFASE(linea, MontoVentasFAE)
                   
                Case 2 'registrar por valor de sucursal

                    Dim Incluir As String = linea.MOVIMIENTOS
                    Dim sucursales = From p In OrigenIngresosData.VENTASFAE
                       Group By p.DOC_SUCURSAL Into g = Group _
                        Select New With {g, .GTotal = g.Sum(Function(p) p.BRUTO)}
                    For Each sucursal In sucursales
                        Dim monto As Decimal = sucursal.GTotal / 1.12
                        Dim sucClave As String = sucursal.g(0).DOC_SUCURSAL


                        AddSucursal2SAP_GL_INTERFASE(linea, sucClave, monto)
                    Next
                Case 3
                    Add2SAP_GL_INTERFASE(linea, IvaFAE + IvaLiquidaciones)
            End Select
        Next
    End Sub
#End Region
#Region "Tipo E - dev mal estado Cliente RV"
    Private Sub CrearRegistroE(q As System.Data.EnumerableRowCollection(Of GTPolizasIngresosData.POLIZASINGRESOSRow))
        'Dim TotalMovimientoLiquidaciones As Decimal = GetTotalLiquidaciones(107)
        'Dim TotalMovimientoMovimientos As Decimal = GetTotalLiquidaciones(95)
        Dim TotalMovimiento As Decimal
        Dim IvaPorPagar As Decimal
        'Dim TotalCuentaDescuentos As Decimal = TotalMovimiento - IvaPorPagar

        For Each linea In q
            Select Case linea.POSICION
                Case 1
                    Dim Incluir As String = linea.MOVIMIENTOS
                    Dim sucursales = From p In OrigenIngresosData.ODS_LIQUIDACIONES
                        Where Incluir.Contains(p.TMD_CLAVE.ToString) _
                        Group By p.SUC_CLAVE Into g = Group _
                        Select New With {g, .GTotal = g.Sum(Function(p) p.TOTAL)}
                    For Each sucursal In sucursales
                        Dim monto As Decimal = sucursal.GTotal / 1.12
                        Dim sucClave As String = sucursal.g(0).SUC_CLAVE
                        IvaPorPagar += (sucursal.GTotal * 0.12 / 1.12)
                        TotalMovimiento += sucursal.GTotal
                        AddSucursal2SAP_GL_INTERFASE(linea, sucClave, monto)
                    Next
                Case 2
                    Add2SAP_GL_INTERFASE(linea, IvaPorPagar)
                Case 3
                    Add2SAP_GL_INTERFASE(linea, TotalMovimiento)
            End Select
        Next
    End Sub
#End Region
#Region "LookUP"
    Private Function GetMaxPolClave() As Integer
        Dim MxPl As Integer = (From p In OrigenIngresosData.POLIZASINGRESOS Select (p.POL_CLAVE)).Max()
        Return MxPl
    End Function
    Private Function GetTotalLiquidaciones(CadenaMovimientos As String) As Decimal

        Dim Monto As Decimal = (From p In OrigenIngresosData.ODS_LIQUIDACIONES Where CadenaMovimientos.Contains(p.TMD_CLAVE.ToString) Select p.TOTAL).Sum
        Debug.Print(CadenaMovimientos & " liq " & Monto.ToString)
        Return Monto

    End Function
    Private Function GetTotalMovimientosProductos(CadenaMovimientos As String) As Decimal

        'Dim Movimientos As String() = New String() {CadenaMovimientos}
        Dim Monto As Decimal = (From p In OrigenIngresosData.ODS_MOVIMPRODUCTOS Where CadenaMovimientos.Contains(p.MOV_CLAVE.ToString) Select p.TOTAL).Sum
        Debug.Print(CadenaMovimientos.ToString & " movprod " & Monto.ToString)
        Return Monto

    End Function
    Private Function GetTotalBanca(CadenaMovimientos As String) As Decimal

        Dim Monto As Decimal = (From p In OrigenIngresosData.ODS_BANCA Where CadenaMovimientos.Contains(p.TMD_CLAVE.ToString) Select p.TOTAL).Sum
        Debug.Print(CadenaMovimientos & " banca " & Monto.ToString)
        Return Monto

    End Function
    Private Function GetSAPCentroBeneficio(ByVal SUC_CLAVE As Long) As String
        Dim SapSucursal As String = (From p In OrigenIngresosData.SUCURSALES Where p.SUCURSAL_CLAVE = SUC_CLAVE Select p.SAP_CENTROBENEFICIO).First
        Return SapSucursal.Trim
    End Function
    Private Function GetSAPCentroCostos(ByVal SUC_CLAVE As Long) As String
        Dim SapSucursal As String = (From p In OrigenIngresosData.SUCURSALES Where p.SUCURSAL_CLAVE = SUC_CLAVE Select p.SAP_CENTROCOSTO).First
        Return SapSucursal.Trim
    End Function
#End Region
    'Private Sub T2VentasCAMN(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
    '    With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
    '        For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows
    '            If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

    '                Select Case tableRow.TMD_CLAVE
    '                    Case 10
    '                        If tableRow.NIVEL_AGRUPA = 1 Then
    '                            With MovimientosDeProductos.VS_CARGA
    '                                For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
    '                                    Dim TotalImpuesto As Double = 0
    '                                    Dim MontoSucursal As Decimal = tRow.TOTAL - MontoDevolucion(tRow.SUC_CLAVE)

    '                                    MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                                    MontoSucursal = MontoSucursal / tableRow.DIVIDIR
    '                                    With Procesados.PolizasC
    '                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                        With NewDataRow
    '                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

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
    '                                            .Secuencia = NoPoliza
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
    '                                        .Secuencia = NoPoliza
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

    '                                    MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                                    MontoSucursal = MontoSucursal / tableRow.DIVIDIR
    '                                    With Procesados.PolizasC
    '                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                        With NewDataRow
    '                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

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
    '                                            .Secuencia = NoPoliza
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
    '                                        .Secuencia = NoPoliza
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

    '                Dim Tmd As Decimal = tableRow.TMD_CLAVE

    '                If Tmd = 30000 Then
    '                    Dim x As New Dictionary(Of Integer, Decimal)



    '                    Dim q = From p In Liquidaciones.VS_ODS_LIQUIDACIONES
    '                        Where p.TMD_CLAVE = 25 Or p.TMD_CLAVE = 26 Or p.TMD_CLAVE = 27 _
    '                        Group p By p.SUC_CLAVE Into g = Group _
    '                        Select New With {g, SUC_CLAVE, .monto = g.Sum(Function(p) p.TOTAL)}

    '                    For Each secuencia In q

    '                        x.Add(secuencia.SUC_CLAVE, secuencia.monto)

    '                    Next
    '                    For Each secuencia In x
    '                        Dim Montosucursal As Decimal = secuencia.Value

    '                        Montosucursal = Montosucursal * tableRow.MULTIPLICAR
    '                        Montosucursal = Montosucursal / tableRow.DIVIDIR

    '                        With Procesados.PolizasC
    '                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                            With NewDataRow
    '                                .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, secuencia.Key, tableRow.CENTRO_FIJO)


    '                                If tableRow.IsCUENTA_DRNull Then
    '                                    .CUENTADR = String.Empty
    '                                    .MONTODR = 0
    '                                    .CUENTACR = tableRow.CUENTA_CR
    '                                    .MONTOCR = Montosucursal

    '                                ElseIf tableRow.IsCUENTA_CRNull Then
    '                                    .CUENTADR = tableRow.CUENTA_DR
    '                                    .MONTODR = Montosucursal
    '                                    .CUENTACR = String.Empty
    '                                    .MONTOCR = 0

    '                                End If
    '                                .Secuencia = NoPoliza
    '                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                .CENTRO = tableRow.CENTRO
    '                                .SUC_CLAVE = secuencia.Key
    '                            End With
    '                            .AddPolizasCRow(NewDataRow)
    '                        End With
    '                    Next

    '                ElseIf Tmd = 40000 Then 'SOLO GUATEMALA
    '                    Dim GTotal As Decimal = (From p In Liquidaciones.VS_ODS_LIQUIDACIONES Where p.TMD_CLAVE = 25 Or p.TMD_CLAVE = 26 Or p.TMD_CLAVE = 27 Select (p.TOTAL)).Sum

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
    '                                .Secuencia = NoPoliza
    '                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                .CENTRO = tableRow.CENTRO
    '                                .SUC_CLAVE = String.Empty
    '                            End With
    '                            .AddPolizasCRow(NewDataRow)
    '                        End With
    '                    End If
    '                ElseIf Tmd = 50000 Then
    '                    Dim GTotal As Decimal = (From p In Liquidaciones.VS_ODS_LIQUIDACIONES Where p.TMD_CLAVE = 25 Or p.TMD_CLAVE = 26 Or p.TMD_CLAVE = 27 Select (p.TOTAL)).Sum

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
    '                                .Secuencia = NoPoliza
    '                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                .CENTRO = tableRow.CENTRO
    '                                .SUC_CLAVE = String.Empty
    '                            End With
    '                            .AddPolizasCRow(NewDataRow)
    '                        End With
    '                    End If
    '                Else



    '                    If tableRow.NIVEL_AGRUPA = 1 Then


    '                        With Liquidaciones.VS_ODS_LIQUIDACIONES
    '                            For Each tRow As LiquidacionesData.VS_ODS_LIQUIDACIONESRow In .Rows
    '                                If tRow.TMD_CLAVE = Tmd Then
    '                                    '
    '                                    Dim MontoSucursal As Decimal = tRow.TOTAL
    '                                    MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                                    MontoSucursal = MontoSucursal / tableRow.DIVIDIR

    '                                    With Procesados.PolizasC
    '                                        Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                        With NewDataRow
    '                                            .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)


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
    '                                            .Secuencia = NoPoliza
    '                                            .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                            .CENTRO = tableRow.CENTRO
    '                                            .SUC_CLAVE = tRow.SUC_CLAVE
    '                                        End With
    '                                        .AddPolizasCRow(NewDataRow)
    '                                    End With
    '                                End If
    '                            Next
    '                        End With

    '                    ElseIf tableRow.NIVEL_AGRUPA = 0 Then
    '                        Dim GTotal As Decimal
    '                        'Dim IVAfactor As Decimal = tableRow.MULTIPLICAR / tableRow.DIVIDIR

    '                        GTotal = (From p In Liquidaciones.VS_ODS_LIQUIDACIONES Where p.TMD_CLAVE = Tmd Select p.TOTAL).Sum
    '                        'GTotal = GTotal * IVAfactor
    '                        GTotal = GTotal * tableRow.MULTIPLICAR
    '                        GTotal = GTotal / tableRow.DIVIDIR
    '                        If GTotal > 0 Then
    '                            With Procesados.PolizasC
    '                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                With NewDataRow
    '                                    .SAP_CENTROCOSTO = String.Empty

    '                                    If tableRow.IsCUENTA_DRNull Then
    '                                        .CUENTADR = String.Empty
    '                                        .MONTODR = 0
    '                                        .CUENTACR = tableRow.CUENTA_CR
    '                                        .MONTOCR = GTotal

    '                                    ElseIf tableRow.IsCUENTA_CRNull Then
    '                                        .CUENTADR = tableRow.CUENTA_DR
    '                                        .MONTODR = GTotal
    '                                        .CUENTACR = String.Empty
    '                                        .MONTOCR = 0

    '                                    End If
    '                                    .Secuencia = NoPoliza
    '                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                    .CENTRO = tableRow.CENTRO

    '                                End With
    '                                .AddPolizasCRow(NewDataRow)
    '                            End With
    '                        End If



    '                    End If

    '                End If

    '            End If


    '        Next
    '    End With

    'End Sub
    'Private Sub T2DVentasSumarizado(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
    '    Dim MontoSumarizado As Decimal
    '    With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
    '        For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

    '            If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

    '                Dim Tmd As Decimal = tableRow.TMD_CLAVE

    '                If tableRow.NIVEL_AGRUPA = 1 Then


    '                    With MovimientosDeProductos.VS_CARGA
    '                        For Each tRow As MovimientosDeProductosData.VS_CARGARow In .Rows
    '                            'If tRow.TMd_CLAVE = Tmd Then
    '                            '
    '                            Dim MontoSucursal As Decimal = tRow.TOTAL - MontoDevolucion(tRow.SUC_CLAVE)

    '                            MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                            MontoSucursal = MontoSucursal / tableRow.DIVIDIR
    '                            MontoSumarizado = MontoSumarizado + Math.Round(MontoSucursal, 2)

    '                            With Procesados.PolizasC
    '                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                With NewDataRow
    '                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)


    '                                    If tableRow.IsCUENTA_DRNull Then
    '                                        .CUENTADR = String.Empty
    '                                        .MONTODR = 0
    '                                        .CUENTACR = tableRow.CUENTA_CR
    '                                        .MONTOCR = Math.Round(MontoSucursal, 2)

    '                                    ElseIf tableRow.IsCUENTA_CRNull Then
    '                                        .CUENTADR = tableRow.CUENTA_DR
    '                                        .MONTODR = Math.Round(MontoSucursal, 2)
    '                                        .CUENTACR = String.Empty
    '                                        .MONTOCR = 0

    '                                    End If
    '                                    .Secuencia = NoPoliza
    '                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                    .CENTRO = tableRow.CENTRO
    '                                    .SUC_CLAVE = tRow.SUC_CLAVE
    '                                End With
    '                                .AddPolizasCRow(NewDataRow)
    '                            End With
    '                            'End If
    '                        Next
    '                    End With

    '                ElseIf tableRow.NIVEL_AGRUPA = 0 Then
    '                    Dim GTotal As Decimal
    '                    Dim IVAfactor As Decimal = tableRow.MULTIPLICAR / tableRow.DIVIDIR

    '                    GTotal = (From p In MovimientosDeProductos.VS_CARGA Select p.TOTAL).Sum
    '                    GTotal = GTotal - MontoTotalDevolucion()
    '                    GTotal = GTotal * tableRow.MULTIPLICAR
    '                    GTotal = GTotal / tableRow.DIVIDIR
    '                    MontoSumarizado = MontoSumarizado + Math.Round(GTotal, 2)

    '                    If GTotal > 0 Then
    '                        With Procesados.PolizasC
    '                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                            With NewDataRow
    '                                .SAP_CENTROCOSTO = String.Empty

    '                                If tableRow.IsCUENTA_DRNull Then
    '                                    .CUENTADR = String.Empty
    '                                    .MONTODR = 0
    '                                    .CUENTACR = tableRow.CUENTA_CR
    '                                    .MONTOCR = Math.Round(GTotal, 2)

    '                                ElseIf tableRow.IsCUENTA_CRNull Then
    '                                    .CUENTADR = tableRow.CUENTA_DR
    '                                    .MONTODR = Math.Round(GTotal, 2)
    '                                    .CUENTACR = String.Empty
    '                                    .MONTOCR = 0

    '                                End If
    '                                .Secuencia = NoPoliza
    '                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                .CENTRO = tableRow.CENTRO

    '                            End With
    '                            .AddPolizasCRow(NewDataRow)
    '                        End With
    '                    End If
    '                ElseIf tableRow.TMD_CLAVE = 1000 Then
    '                    Dim GTotal As Decimal = MontoSumarizado
    '                    If GTotal > 0 Then
    '                        With Procesados.PolizasC
    '                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                            With NewDataRow
    '                                .SAP_CENTROCOSTO = String.Empty

    '                                If tableRow.IsCUENTA_DRNull Then
    '                                    .CUENTADR = String.Empty
    '                                    .MONTODR = 0
    '                                    .CUENTACR = tableRow.CUENTA_CR
    '                                    .MONTOCR = Math.Round(GTotal, 2)

    '                                ElseIf tableRow.IsCUENTA_CRNull Then
    '                                    .CUENTADR = tableRow.CUENTA_DR
    '                                    .MONTODR = Math.Round(GTotal, 2)
    '                                    .CUENTACR = String.Empty
    '                                    .MONTOCR = 0

    '                                End If
    '                                .Secuencia = NoPoliza
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
    'Private Sub T2DescuentosSumarizado(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
    '    Dim MontoSumarizado As Decimal
    '    With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
    '        For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

    '            If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

    '                Dim Tmd As Decimal = tableRow.TMD_CLAVE

    '                If tableRow.NIVEL_AGRUPA = 1 Then


    '                    With MovimientosDeProductos.VS_TRADEPROMOTIONS
    '                        For Each tRow As MovimientosDeProductosData.VS_TRADEPROMOTIONSRow In .Rows
    '                            'If tRow.TMo_CLAVE = Tmd Then
    '                            '
    '                            Dim MontoSucursal As Decimal = tRow.TOTAL

    '                            MontoSucursal = MontoSucursal * tableRow.MULTIPLICAR
    '                            MontoSucursal = MontoSucursal / tableRow.DIVIDIR
    '                            MontoSumarizado = MontoSumarizado + Math.Round(MontoSucursal, 2)

    '                            With Procesados.PolizasC
    '                                Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                                With NewDataRow
    '                                    .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)

    '                                    If tableRow.IsCUENTA_DRNull Then
    '                                        .CUENTADR = String.Empty
    '                                        .MONTODR = 0
    '                                        .CUENTACR = tableRow.CUENTA_CR
    '                                        .MONTOCR = Math.Round(MontoSucursal, 2)

    '                                    ElseIf tableRow.IsCUENTA_CRNull Then
    '                                        .CUENTADR = tableRow.CUENTA_DR
    '                                        .MONTODR = Math.Round(MontoSucursal, 2)
    '                                        .CUENTACR = String.Empty
    '                                        .MONTOCR = 0

    '                                    End If
    '                                    .Secuencia = NoPoliza
    '                                    .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                    .CENTRO = tableRow.CENTRO
    '                                    .SUC_CLAVE = tRow.SUC_CLAVE
    '                                End With
    '                                .AddPolizasCRow(NewDataRow)
    '                            End With
    '                            'End If
    '                        Next
    '                    End With

    '                ElseIf tableRow.NIVEL_AGRUPA = 0 Then
    '                    Dim GTotal As Decimal
    '                    Dim IVAfactor As Decimal = tableRow.MULTIPLICAR / tableRow.DIVIDIR
    '                    If tableRow.SUMAR = 1 Then
    '                        GTotal = MontoSumarizado
    '                    Else
    '                        GTotal = (From p In MovimientosDeProductos.VS_TRADEPROMOTIONS Select p.TOTAL).Sum
    '                        GTotal = GTotal * tableRow.MULTIPLICAR
    '                        GTotal = GTotal / tableRow.DIVIDIR
    '                        MontoSumarizado = MontoSumarizado + Math.Round(GTotal, 2)
    '                    End If



    '                    If GTotal > 0 Then
    '                        With Procesados.PolizasC
    '                            Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow
    '                            With NewDataRow
    '                                .SAP_CENTROCOSTO = String.Empty

    '                                If tableRow.IsCUENTA_DRNull Then
    '                                    .CUENTADR = String.Empty
    '                                    .MONTODR = 0
    '                                    .CUENTACR = tableRow.CUENTA_CR
    '                                    .MONTOCR = Math.Round(GTotal, 2)

    '                                ElseIf tableRow.IsCUENTA_CRNull Then
    '                                    .CUENTADR = tableRow.CUENTA_DR
    '                                    .MONTODR = Math.Round(GTotal, 2)
    '                                    .CUENTACR = String.Empty
    '                                    .MONTOCR = 0

    '                                End If
    '                                .Secuencia = NoPoliza
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
    'Private Sub T2BancosCAMN(ByVal MasterTableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASRow)
    '    With CatalogoPolizas.VS_CATALOGOPOLIZASDETALLE
    '        For Each tableRow As VS_CATALOGOPOLIZASData.VS_CATALOGOPOLIZASDETALLERow In .Rows

    '            If tableRow.POL_CLAVE = MasterTableRow.POL_CLAVE Then

    '                Dim Tmd As Decimal = tableRow.TMD_CLAVE

    '                If tableRow.NIVEL_AGRUPA = 2 Then


    '                    With Liquidaciones.VS_LIQUIDACIONESBANCOS
    '                        For Each tRow As LiquidacionesData.VS_LIQUIDACIONESBANCOSRow In .Rows
    '                            If tRow.TMD_CLAVE = Tmd Then


    '                                Dim MontoSucursal As Decimal = tRow.TOTAL

    '                                With Procesados.PolizasC
    '                                    Dim NewDataRow As ProcesadasData.PolizasCRow = .NewPolizasCRow

    '                                    With NewDataRow
    '                                        .SAP_CENTROCOSTO = FillCentroCosto(tableRow.CENTRO, tRow.SUC_CLAVE, tableRow.CENTRO_FIJO)


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
    '                                        .Secuencia = NoPoliza
    '                                        .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                        .CENTRO = tableRow.CENTRO
    '                                        .SUC_CLAVE = tRow.SUC_CLAVE
    '                                        If Not tRow.IsMDI_NUMREMNull Then
    '                                            If tRow.MDI_NUMREM.Length > 12 Then
    '                                                .ASSIGNMENT = tRow.MDI_NUMREM.Substring(0, 12)
    '                                            Else
    '                                                .ASSIGNMENT = tRow.MDI_NUMREM
    '                                            End If

    '                                        Else
    '                                            .ASSIGNMENT = Format(MontoSucursal, "#.00") & Format(tRow.FECHA_DEPOSITO, "ddMMyy")
    '                                        End If

    '                                    End With
    '                                    .AddPolizasCRow(NewDataRow)
    '                                End With
    '                            End If
    '                        Next
    '                    End With

    '                ElseIf tableRow.NIVEL_AGRUPA = 0 Then
    '                    Dim GTotal As Decimal = (From p In Liquidaciones.VS_LIQUIDACIONESBANCOS Where p.TMD_CLAVE = Tmd Select p.TOTAL).Sum

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
    '                                .Secuencia = NoPoliza
    '                                .DOC_HEADER_TEXT = tableRow.DOC_HEADER
    '                                .CENTRO = String.Empty

    '                            End With
    '                            .AddPolizasCRow(NewDataRow)
    '                        End With
    '                    End If
    '                End If
    '            End If
    '        Next
    '    End With

    'End Sub
    'Private Sub T3()
    '    'Agrupar MovimientoSegregado by centro de costo an movclave


    '    Dim q = From p In MovimientoSegregado._ODS_MOVIMIENTOPRODUCTO_SEGREGADO _
    '      Group p By p.SAP_CENTROCOSTODR, p.SAP_CENTROCOSTOCR, p.MOV_CLAVE, p.ESBENEFICIODR, p.ESBENEFICIOCR Into g = Group _
    '      Select New With {g, .monto = g.Sum(Function(p) p.TOTAL)}

    '    For Each Secuencia In q
    '        With MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO

    '            Dim NewDataRow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.ODS_MOVIMIENTOPRODUCTO_AGRUPADORow = .NewODS_MOVIMIENTOPRODUCTO_AGRUPADORow
    '            With NewDataRow
    '                .MOV_CLAVE = Secuencia.g(0).MOV_CLAVE
    '                .SAP_CENTROCOSTODR = Secuencia.g(0).SAP_CENTROCOSTODR
    '                .SAP_CENTROCOSTOCR = Secuencia.g(0).SAP_CENTROCOSTOCR
    '                .MONTO = Secuencia.monto

    '                .ESBENEFICIODR = Secuencia.g(0).ESBENEFICIODR
    '                .ESBENEFICIOCR = Secuencia.g(0).ESBENEFICIOCR
    '            End With

    '            .AddODS_MOVIMIENTOPRODUCTO_AGRUPADORow(NewDataRow)
    '        End With
    '    Next
    '    'With VerDatosForm.Grid_Agrupado
    '    '    .DataSource = MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO.DefaultView
    '    'End With

    'End Sub
#End Region
#Region "LookUp viejo"
    'Private Function FillCentroCosto(ByVal Centro As String, ByVal Sucursal As Integer, ByVal CFijo As String) As String
    '    Dim CentroString As String = String.Empty
    '    Select Case Centro
    '        Case "B"
    '            CentroString = GetSAPCentroBeneficio(Sucursal)
    '        Case "C"
    '            CentroString = GetSAPCentroCosto(Sucursal)
    '        Case "D"
    '            CentroString = String.Empty
    '        Case "FB", "FC"
    '            CentroString = CFijo
    '        Case "G"
    '            CentroString = GetSAPCentroGastos(Sucursal)
    '    End Select
    '    Return CentroString
    'End Function
    'Private Function MontoDevolucion(ByVal SUC_CLAVE As Long) As Decimal
    '    Dim Devolucion As Decimal = 0
    '    With MovimientosDeProductos.VS_MOVIMIENTO33PROD
    '        For Each tableRow As MovimientosDeProductosData.VS_MOVIMIENTO33PRODRow In .Rows
    '            If tableRow.SUC_CLAVE = SUC_CLAVE Then
    '                If tableRow.IsTOTALNull Then
    '                    Devolucion = 0
    '                Else
    '                    Devolucion = CDec(Format(tableRow.TOTAL, "#.00"))
    '                End If

    '                Exit For
    '            End If
    '        Next
    '        Return Devolucion
    '    End With
    'End Function
    'Private Function MontoTotalDevolucion() As Decimal
    '    Dim GTotal As Decimal = (From p In MovimientosDeProductos.VS_MOVIMIENTO33PROD Select p.TOTAL).Sum
    '    Return CDec(Format(GTotal, "#.00"))
    'End Function
    'Private Function GetSAPCentroCosto(ByVal SUC_CLAVE As Long) As String
    '    Dim SapSucursal As String = String.Empty
    '    With Maestro.ODS_SUCURSALES
    '        For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
    '            If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
    '                SapSucursal = tableRow.SAP_CENTROCOSTO
    '                Exit For
    '            End If
    '        Next
    '        Return SapSucursal
    '    End With
    'End Function
    'Private Function GetSAPCentroBeneficio(ByVal SUC_CLAVE As Long) As String
    '    Dim SapSucursal As String = String.Empty
    '    With Maestro.ODS_SUCURSALES
    '        For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
    '            If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
    '                SapSucursal = tableRow.SAP_CENTROBENEFICIO.Trim
    '                Exit For
    '            End If
    '        Next
    '        Return SapSucursal
    '    End With
    'End Function
    'Private Function GetSAPCentroGastos(ByVal SUC_CLAVE As Long) As String
    '    Dim SapSucursal As String = String.Empty
    '    With Maestro.ODS_SUCURSALES
    '        For Each tableRow As OSDMaestroData.ODS_SUCURSALESRow In .Rows
    '            If tableRow.SUCURSAL_CLAVE = SUC_CLAVE Then
    '                SapSucursal = tableRow.SAP_CENTROGASTO.Trim
    '                Exit For
    '            End If
    '        Next
    '        Return SapSucursal
    '    End With
    'End Function

    '    Private Function GetImpuesto(ByVal Prod_Clave As String) As Double
    '        Dim P100Impuesto As Double = 0

    '        Dim Impuestos = From catalogo In Maestro.ODS_TMF_PRODUCTO _
    '           Where catalogo.PRO_CLAVE = Prod_Clave

    '        For Each Impuesto In Impuestos
    '            If Impuesto.PRO_IVASN.Trim = "S" Then
    '                P100Impuesto = Impuesto.PRO_IVADET
    '            Else
    '                P100Impuesto = 0
    '            End If
    '        Next
    '        Return P100Impuesto
    '    End Function
    '    Private Function CXCEmpleados() As Decimal
    '        Dim GTtotal As Decimal
    '        With MovimientosDeProductos.CXCEMPLEADOS
    '            For Each tRow As MovimientosDeProductosData.CXCEMPLEADOSRow In .Rows
    '                If Not tRow.IsTOTALNull Then
    '                    GTtotal = GTtotal + tRow.TOTAL
    '                End If
    '            Next
    '        End With
    '        Return GTtotal
    '    End Function
#End Region
#Region "Registra datos"
    Private Sub DeleteDatos()
        Dim result As Integer = (New MenuSP).DeletePolizasCxCSemana
    End Sub
    Private Function PersistInDatabase() As Integer
        Dim result As Integer
        result = (New PDCVDatos).insertarEnGL(SapGLInterfase.SAP_GL_INTERFASE)
        If result = -1 Then
        Else
            MsgBox(InfoCache.UpdateError)
        End If
        Return result
    End Function
    'Private Function PersistInDatabase() As Integer
    '   
    '    'Dim NoSecuencia As Long = 0
    '    With Procesados.PolizasC
    '        For Each tablerow As ProcesadasData.PolizasCRow In .Rows
    '            With SapGLInterfase.SAP_GL_INTERFASE
    '                Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
    '                With NewDataRow
    '                    .ESTATUS_SAP = InfoCache.ESTATUS_SAP


    '                    .DOCUMENT_DATE = Date.Today
    '                    .DOCUMENT_TYPE = "ZI"
    '                    .REF_DOCUMENT_NUMBER = "B" & InfoCache.PaisClave & Format(tablerow.Secuencia, "000000")
    '                    .DOC_HEADER_TEXT = tablerow.DOC_HEADER_TEXT & InfoCache.APS
    '                    .SAP_COMPANY_CODE = InfoCache.ClaveCompania

    '                    If tablerow.CUENTADR.Length > 0 Then
    '                        .SAP_ACCOUNT = tablerow.CUENTADR
    '                        If tablerow.SAP_CENTROCOSTO.Length > 0 Then
    '                            Select Case tablerow.CUENTADR.Substring(0, 1)
    '                                Case "1", "2", "3"
    '                                    .SAP_PROFIT_CENTER = String.Empty
    '                                    .SAP_COST_CENTER = String.Empty
    '                                Case "4", "5", "7"
    '                                    .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO.ToString
    '                                    .SAP_COST_CENTER = String.Empty
    '                                Case "6"
    '                                    .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO.ToString
    '                                    .SAP_PROFIT_CENTER = String.Empty
    '                            End Select
    '                        End If
    '                    Else
    '                        .SAP_ACCOUNT = tablerow.CUENTACR
    '                        If tablerow.SAP_CENTROCOSTO.Length > 0 Then
    '                            Select Case tablerow.CUENTACR.Substring(0, 1)
    '                                Case "1", "2", "3"
    '                                    .SAP_PROFIT_CENTER = String.Empty
    '                                    .SAP_COST_CENTER = String.Empty
    '                                Case "4", "5", "7"
    '                                    .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO.ToString
    '                                    .SAP_COST_CENTER = String.Empty
    '                                Case "6"
    '                                    .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO.ToString
    '                                    .SAP_PROFIT_CENTER = String.Empty
    '                            End Select
    '                        End If
    '                    End If

    '                    .SAP_SEGMENT = String.Empty
    '                    .ENTERED_DR = tablerow.MONTODR
    '                    .ENTERED_CR = tablerow.MONTOCR

    '                    If Not tablerow.IsASSIGNMENTNull Then
    '                        .ASSIGNMENT = tablerow.ASSIGNMENT
    '                    Else
    '                        .ASSIGNMENT = String.Empty
    '                    End If
    '                    .LINE_TEXT = String.Empty
    '                    .SAP_REF_DOC_NUMBER = String.Empty
    '                    .SAP_REF_MESSAGE = String.Empty
    '                    '.ACCOUNTING_DATE = Date.Today
    '                    .ACCOUNTING_DATE = InfoCache.FechaHasta
    '                    .CURRENCY_CODE = InfoCache.CurrencyCode
    '                    .LEDGER_GROUP = String.Empty
    '                    .SAP_TAX_INDICATOR = String.Empty

    '                End With
    '                Try
    '                    .AddSAP_GL_INTERFASERow(NewDataRow)
    '                    .AcceptChanges()
    '                Catch ex As Exception

    '                End Try
    '            End With
    '        Next

    '    End With

    '    'TODO: CAMBIAR a update masivo
    '    InfoCache.UpdateError = String.Empty

    '    Dim result As Integer
    '    result = (New PDCVDatos).insertarEnGL(SapGLInterfase.SAP_GL_INTERFASE)
    '    If result = -1 Then
    '    Else
    '        MsgBox(InfoCache.UpdateError)
    '    End If
    '    Return result

    '    'TODO: REVISAR manejo de nosecuencia
    '    'Me.SecuenciaInicial.Text = (NoSecuencia + 1).ToString


    'End Function
    'Private Sub PersistInDatabaseTEST()
    '    SapGLInterfase = New SAP_GLINTERFASEData
    '    'Dim NoSecuencia As Long = 0
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
    '                    .ESTATUS_SAP = "REV"
    '                    '.ESTATUS_SAP = "TEST"
    '                    '.ESTATUS_SAP = "NEW"
    '                    .DOCUMENT_DATE = Date.Today
    '                    .DOCUMENT_TYPE = "ZI"
    '                    .REF_DOCUMENT_NUMBER = "B" & InfoCache.PaisClave & Format(tablerow.Secuencia, "000000")
    '                    .DOC_HEADER_TEXT = tablerow.DOC_HEADER_TEXT & InfoCache.APS
    '                    .SAP_COMPANY_CODE = InfoCache.ClaveCompania

    '                    If tablerow.CUENTADR.Length > 0 Then
    '                        .SAP_ACCOUNT = tablerow.CUENTADR
    '                        If tablerow.SAP_CENTROCOSTO.Length > 0 Then


    '                            Select Case tablerow.CUENTADR.Substring(0, 1)
    '                                Case "1", "2", "3"
    '                                    .SAP_PROFIT_CENTER = String.Empty
    '                                    .SAP_COST_CENTER = String.Empty
    '                                Case "4", "5", "7"
    '                                    .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO.ToString
    '                                    .SAP_COST_CENTER = String.Empty
    '                                Case "6"
    '                                    .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO.ToString
    '                                    .SAP_PROFIT_CENTER = String.Empty
    '                            End Select
    '                        End If
    '                    Else
    '                        .SAP_ACCOUNT = tablerow.CUENTACR
    '                        If tablerow.SAP_CENTROCOSTO.Length > 0 Then


    '                            Select Case tablerow.CUENTACR.Substring(0, 1)
    '                                Case "1", "2", "3"
    '                                    .SAP_PROFIT_CENTER = String.Empty
    '                                    .SAP_COST_CENTER = String.Empty
    '                                Case "4", "5", "7"
    '                                    .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO.ToString
    '                                    .SAP_COST_CENTER = String.Empty
    '                                Case "6"
    '                                    .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO.ToString
    '                                    .SAP_PROFIT_CENTER = String.Empty
    '                            End Select
    '                        End If
    '                    End If

    '                    'If Not tablerow.IsSAP_CENTROCOSTONull Then
    '                    '    If tablerow.SAP_CENTROCOSTO.Length > 0 Then
    '                    '        Select Case tablerow.CENTRO
    '                    '            Case "B", "FB"
    '                    '                .SAP_COST_CENTER = String.Empty
    '                    '                .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTO
    '                    '            Case Else
    '                    '                .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTO
    '                    '                .SAP_PROFIT_CENTER = String.Empty
    '                    '        End Select
    '                    '    End If
    '                    'Else
    '                    '    .SAP_COST_CENTER = String.Empty
    '                    '    .SAP_PROFIT_CENTER = String.Empty
    '                    'End If



    '                    .SAP_SEGMENT = String.Empty
    '                    .ENTERED_DR = tablerow.MONTODR
    '                    .ENTERED_CR = tablerow.MONTOCR


    '                    '.ENTERED_DR = CDec(Format(tablerow.MONTODR, "#.00"))
    '                    '.ENTERED_CR = CDec(Format(tablerow.MONTOCR, "#.00"))
    '                    If Not tablerow.IsASSIGNMENTNull Then
    '                        .ASSIGNMENT = tablerow.ASSIGNMENT
    '                    Else
    '                        .ASSIGNMENT = String.Empty
    '                    End If
    '                    .LINE_TEXT = String.Empty
    '                    .SAP_REF_DOC_NUMBER = String.Empty
    '                    .SAP_REF_MESSAGE = String.Empty
    '                    '.ACCOUNTING_DATE = Date.Today
    '                    .ACCOUNTING_DATE = Me.FechaHasta.Value
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

    '    'definitiva
    '    With VerDatosForm.GRID_SAP_GL_INTERFASE
    '        .DataSource = SapGLInterfase.SAP_GL_INTERFASE
    '    End With


    '    With SapGLInterfase.SAP_GL_INTERFASE
    '        For Each tablerow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow In .Rows
    '            Dim result As Boolean = (New PDCVDatos).insert(tablerow)
    '        Next
    '    End With
    '    Me.SecuenciaInicial.Text = (NoSecuencia + 1).ToString


    '    ' ''test
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
#Region "BalancearPolizas"
    Private Sub BalancearPoizas()
        Dim q = From p In SapGLInterfase.SAP_GL_INTERFASE
        Group p By p.REF_DOCUMENT_NUMBER
        Into Debitos = Sum(p.ENTERED_DR), Creditos = Sum(p.ENTERED_CR)
        For Each Poliza In q
            Dim diferencia As Decimal = Poliza.Debitos - Poliza.Creditos
            If diferencia > 0 Then
                AddDiff2Cr(diferencia, Poliza.REF_DOCUMENT_NUMBER)
            ElseIf diferencia < 0 Then
                AddDiff2Dr(diferencia, Poliza.REF_DOCUMENT_NUMBER)
            End If
        Next

    End Sub

    Private Sub AddDiff2Dr(Ajuste As Decimal, Referencia As String)
        Dim q = From p In SapGLInterfase.SAP_GL_INTERFASE Where p.REF_DOCUMENT_NUMBER = Referencia
        Dim Registro = q.First
        Registro.ENTERED_DR += Ajuste

        'For Each Registro In q
        '    Registro.ENTERED_DR += Ajuste
        '    Exit For
        'Next
    End Sub

    Private Sub AddDiff2Cr(Ajuste As Decimal, Referencia As String)
        Dim q = From p In SapGLInterfase.SAP_GL_INTERFASE Where p.REF_DOCUMENT_NUMBER = Referencia
        Dim Registro = q.First

        Registro.ENTERED_CR += Ajuste

        'For Each Registro In q
        '    Registro.ENTERED_CR += Ajuste
        '    Exit For
        'Next
    End Sub

#End Region
    Private Sub AddSucursal2SAP_GL_INTERFASExx(linea As GTPolizasIngresosData.POLIZASINGRESOSRow, sucursal As Object)
        Throw New NotImplementedException
    End Sub
End Class
