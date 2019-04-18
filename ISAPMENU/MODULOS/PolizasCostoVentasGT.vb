Imports System.Text

'Imports Oracle.DataAccess.Client
'Imports Oracle.DataAccess.Types
'Imports System.Data.Common
Public Class PolizasCostoVentasGT

    Public Event Mensaje(TextoMensaje As String)
    Dim start, finish, totalTime As Double

    Private ts As TimeSpan
    Private startDate As DateTime
    Private endDate As DateTime

    Private ModuloActual As String = "PDCV"
    Private OrigenCostosData As GTPolizasCostosData
    Private SapGLInterfase As SAP_GLINTERFASEData

    Public Function ProcesarCostoVentas() As Integer
        start = Microsoft.VisualBasic.DateAndTime.Timer
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Proceso: " & "Pólizas de inventario" & " de: " & InfoCache.FechaDesde.ToString & " a:" & InfoCache.FechaHasta.ToString)
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Iniciado a")
        OrigenCostosData = (New CostosVentasDML).SelectData
        SapGLInterfase = New SAP_GLINTERFASEData
        ActualizarCostos()

        T1()

        DeleteDatos()
        Dim recordsInesrtados As Integer = PersistInDatabase()

        If recordsInesrtados = -1 Then
            totalTime = Microsoft.VisualBasic.DateAndTime.Timer - start
            RaiseEvent Mensaje(Format(Date.Now, "R") & ">  " & "La creación de las Pólizas de inventario" & " terminó satisfactoriamente.")
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> Duración: " & SecondsToText(totalTime))
        Else
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & "Pólizas de inventario" & "no se ejecutó. Debe procesarla nuevamente.")
        End If
        RaiseEvent Mensaje("...............................................")
        Return 1

    End Function
    Private Sub ActualizarCostos()
        With OrigenCostosData.ODS_MOVIMPRODUCTOS
            For Each tablerow As GTPolizasCostosData.ODS_MOVIMPRODUCTOSRow In .Rows
                tablerow.CostoActual = GetCostoProducto(tablerow.PRO_CLAVE)
            Next
        End With
    End Sub

#Region "Procesar datos"
    Private Sub T1()
        With OrigenCostosData.POLIZASCOSTOS
            For Each tablerow As GTPolizasCostosData.POLIZASCOSTOSRow In .Rows
                ProcesarMovimientoDR(tablerow)
                ProcesarMovimientoCR(tablerow)
                InfoCache.Secuencia += 1
            Next
        End With
    End Sub
    Private Sub ProcesarMovimientoDR(tablerow As GTPolizasCostosData.POLIZASCOSTOSRow)
        If tablerow.IsCENTRO_COSTO_DRNull OrElse (tablerow.CENTRO_COSTO_DR.Trim.Length > 1) Then
            Dim Total As Decimal = (From p In OrigenCostosData.ODS_MOVIMPRODUCTOS Where p.MOV_CLAVE = tablerow.MOV_CLAVE Select p.CostoActual).Sum
            Add2SAP_GL_INTERFASE(tablerow, Total, 0)

        ElseIf tablerow.CENTRO_COSTO_DR = "B" Or tablerow.CENTRO_COSTO_DR = "C" Then
            Dim sucursales = From p In OrigenCostosData.ODS_MOVIMPRODUCTOS Where p.MOV_CLAVE = tablerow.MOV_CLAVE
            Group p By p.SUC_CLAVE Into g = Group _
            Select New With {g, SUC_CLAVE, .Total = g.Sum(Function(p) p.CostoActual)}

            For Each sucursal In sucursales
                AddSucursal2SAP_GL_INTERFASE(tablerow, sucursal.SUC_CLAVE, sucursal.Total, 0)
            Next

        End If
    End Sub
    Private Sub ProcesarMovimientoCR(tablerow As GTPolizasCostosData.POLIZASCOSTOSRow)
        If tablerow.IsCENTRO_COSTO_CRNull OrElse (tablerow.CENTRO_COSTO_CR.Trim.Length) > 1 Then
            Dim Total As Decimal = (From p In OrigenCostosData.ODS_MOVIMPRODUCTOS Where p.MOV_CLAVE = tablerow.MOV_CLAVE Select p.CostoActual).Sum
            Add2SAP_GL_INTERFASE(tablerow, Total, 1)

        ElseIf tablerow.CENTRO_COSTO_CR = "B" Or tablerow.CENTRO_COSTO_CR = "C" Then
            Dim sucursales = From p In OrigenCostosData.ODS_MOVIMPRODUCTOS Where p.MOV_CLAVE = tablerow.MOV_CLAVE
            Group p By p.SUC_CLAVE Into g = Group _
            Select New With {g, SUC_CLAVE, .Total = g.Sum(Function(p) p.CostoActual)}

            For Each sucursal In sucursales
                AddSucursal2SAP_GL_INTERFASE(tablerow, sucursal.SUC_CLAVE, sucursal.Total, 1)
            Next

        End If
    End Sub
    Private Sub AddSucursal2SAP_GL_INTERFASE(ByVal tablerow As GTPolizasCostosData.POLIZASCOSTOSRow, ByVal sucClave As Long, ByVal monto As Decimal, ByVal DrCr As Integer)
        With SapGLInterfase.SAP_GL_INTERFASE
            Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
            With NewDataRow
                .ESTATUS_SAP = InfoCache.ESTATUS_SAP
                .DOCUMENT_DATE = Date.Today
                .DOCUMENT_TYPE = "ZI"
                .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")
                .DOC_HEADER_TEXT = tablerow.NOMBRE_POLIZA & InfoCache.APS
                .SAP_COMPANY_CODE = InfoCache.ClaveCompania

                If DrCr = 0 Then

                    .SAP_ACCOUNT = tablerow.CUENTA_CONTABLE_DR
                    .ENTERED_DR = monto

                    Select Case tablerow.CUENTA_CONTABLE_DR.Substring(0, 1)
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
                Else
                    .SAP_ACCOUNT = tablerow.CUENTA_CONTABLE_CR
                    .ENTERED_CR = monto
                    Select Case tablerow.CUENTA_CONTABLE_CR.Substring(0, 1)
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
                End If

                .SAP_SEGMENT = String.Empty

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
    Private Sub Add2SAP_GL_INTERFASE(ByVal tablerow As GTPolizasCostosData.POLIZASCOSTOSRow, ByVal monto As Decimal, ByVal DrCr As Integer)
        With SapGLInterfase.SAP_GL_INTERFASE
            Dim NewDataRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
            With NewDataRow
                .ESTATUS_SAP = InfoCache.ESTATUS_SAP
                .DOCUMENT_DATE = Date.Today
                .DOCUMENT_TYPE = "ZI"
                .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")
                .DOC_HEADER_TEXT = tablerow.NOMBRE_POLIZA & InfoCache.APS
                .SAP_COMPANY_CODE = InfoCache.ClaveCompania
                If DrCr = 0 Then

                    .SAP_ACCOUNT = tablerow.CUENTA_CONTABLE_DR
                    .ENTERED_DR = monto

                    If Not tablerow.IsCENTRO_COSTO_DRNull Then
                        .SAP_PROFIT_CENTER = tablerow.CENTRO_COSTO_DR
                    End If

                Else
                    .SAP_ACCOUNT = tablerow.CUENTA_CONTABLE_CR
                    .ENTERED_CR = monto
                    If Not tablerow.IsCENTRO_COSTO_CRNull Then
                        .SAP_PROFIT_CENTER = tablerow.CENTRO_COSTO_CR
                    End If
                End If
                .SAP_SEGMENT = String.Empty

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
#Region "Registra datos"
    Private Sub DeleteDatos()
        Dim result As Integer = (New MenuSP).DeletePolizasCostoVentasSemana
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

    'Private Sub PersistInDatabaseTEST()
    '    SapGLInterfase = New SAP_GLINTERFASEData

    '    With SapGLInterfase.ODS_MOVIMIENTOPRODUCTO_AGRUPADO
    '        For Each tablerow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.ODS_MOVIMIENTOPRODUCTO_AGRUPADORow In .Rows
    '            Dim CuentaDR As String = GetDebitoMovimiento(tablerow.MOV_CLAVE)
    '            Dim CuentaCR As String = GetCreditoMovimiento(tablerow.MOV_CLAVE)
    '            'Dim MontoString As String = Decimal.Round(tablerow.MONTO, 2)

    '            'definitiva()
    '            With SapGLInterfase.SAP_GL_INTERFASE

    '                ' ''test
    '                'With SapGLInterfase.SAP_GL_TEMP

    '                Dim NewDataRowDB As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow

    '                ''test
    '                'Dim NewDataRowDB As SAP_GLINTERFASEData.SAP_GL_TEMPRow = .NewSAP_GL_TEMPRow

    '                With NewDataRowDB
    '                    '.ESTATUS_SAP = "NEW"
    '                    .ESTATUS_SAP = "REV"
    '                    .DOCUMENT_DATE = Date.Today
    '                    .DOCUMENT_TYPE = "ZI"
    '                    .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(NoPoliza, "000000")
    '                    'GetRefDocNo(tablerow.MOV_CLAVE)
    '                    .DOC_HEADER_TEXT = GetRefHeader(tablerow.MOV_CLAVE)
    '                    .SAP_COMPANY_CODE = InfoCache.ClaveCompania
    '                    .SAP_ACCOUNT = CuentaDR
    '                    Select Case CuentaDR.Substring(0, 1)
    '                        Case "1", "2", "3"
    '                            .SAP_PROFIT_CENTER = String.Empty
    '                            .SAP_COST_CENTER = String.Empty
    '                        Case "4", "5", "7"
    '                            .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTODR.ToString
    '                            .SAP_COST_CENTER = String.Empty
    '                        Case "6"
    '                            .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTODR.ToString
    '                            .SAP_PROFIT_CENTER = String.Empty
    '                    End Select

    '                    .SAP_SEGMENT = String.Empty
    '                    .ENTERED_DR = tablerow.MONTO
    '                    .ENTERED_CR = 0
    '                    .ASSIGNMENT = String.Empty
    '                    .LINE_TEXT = String.Empty
    '                    .SAP_REF_DOC_NUMBER = String.Empty
    '                    .SAP_REF_MESSAGE = String.Empty
    '                    '.ACCOUNTING_DATE = Date.Today
    '                    .ACCOUNTING_DATE = InfoCache.FechaHasta
    '                    .CURRENCY_CODE = InfoCache.CurrencyCode
    '                    .LEDGER_GROUP = String.Empty
    '                    .SAP_TAX_INDICATOR = String.Empty

    '                End With
    '                Dim NewDataRowCR As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
    '                ' ''test
    '                'Dim NewDataRowCR As SAP_GLINTERFASEData.SAP_GL_TEMPRow = .NewSAP_GL_TEMPRow

    '                With NewDataRowCR
    '                    '.ESTATUS_SAP = "NEW"
    '                    .ESTATUS_SAP = "REV"
    '                    .DOCUMENT_DATE = Date.Today
    '                    .DOCUMENT_TYPE = "ZI"
    '                    .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(NoPoliza, "000000")
    '                    .DOC_HEADER_TEXT = GetRefHeader(tablerow.MOV_CLAVE)
    '                    .SAP_COMPANY_CODE = InfoCache.ClaveCompania
    '                    .SAP_ACCOUNT = CuentaCR
    '                    Select Case CuentaCR.Substring(0, 1)
    '                        Case "1", "2", "3"
    '                            .SAP_PROFIT_CENTER = String.Empty
    '                            .SAP_COST_CENTER = String.Empty
    '                        Case "4", "5", "7"
    '                            .SAP_PROFIT_CENTER = tablerow.SAP_CENTROCOSTOCR.ToString
    '                            .SAP_COST_CENTER = String.Empty
    '                        Case "6"
    '                            .SAP_COST_CENTER = tablerow.SAP_CENTROCOSTOCR.ToString
    '                            .SAP_PROFIT_CENTER = String.Empty
    '                    End Select

    '                    .SAP_SEGMENT = String.Empty
    '                    .ENTERED_DR = 0
    '                    .ENTERED_CR = tablerow.MONTO
    '                    .ASSIGNMENT = String.Empty
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
    '                    .AddSAP_GL_INTERFASERow(NewDataRowDB)
    '                    .AddSAP_GL_INTERFASERow(NewDataRowCR)

    '                    '.AddSAP_GL_TEMPRow(NewDataRowDB)
    '                    '.AddSAP_GL_TEMPRow(NewDataRowCR)

    '                    .AcceptChanges()
    '                Catch ex As Exception

    '                End Try


    '            End With
    '            NoPoliza += 1
    '        Next
    '    End With


    '    'definitiva

    '    With VerDatosForm.GRID_SAP_GL_INTERFASE
    '        .DataSource = SapGLInterfase.SAP_GL_INTERFASE
    '    End With
    '    InfoCache.SecuenciaTermina = NoPoliza

    '    'Dim result As Boolean = (New ISAPCostoVentasDML).insertarEnGL(SapGLInterfase.SAP_GL_INTERFASE)

    '    With SapGLInterfase.SAP_GL_INTERFASE
    '        For Each tablerow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow In .Rows
    '            Dim result As Boolean = (New ISAPCostoVentasDML).insert(tablerow)
    '        Next
    '    End With
    '    'Me.SecuenciaInicial.Text = (NoPoliza + 1).ToString


    '    ''test
    '    'With VerDatosForm.GRID_SAP_GL_INTERFASE
    '    '    .DataSource = SapGLInterfase.SAP_GL_TEMP
    '    'End With
    '    'With SapGLInterfase.SAP_GL_TEMP
    '    '    For Each tablerow As SAP_GLINTERFASEData.SAP_GL_TEMPRow In .Rows
    '    '        Dim result As Boolean = (New ISAPCostoVentasDML).insertInTemp(tablerow)
    '    '    Next
    '    'End With
    'End Sub
#End Region
#Region "LookUp"

    Private Function GetCostoProducto(ByVal PRO_CLAVE As String) As Decimal
        Dim Costo As Decimal
        Dim q = (From p In OrigenCostosData.SAP_INTERFASE_COSTOS Where p.SAP_NUM_MATERIAL_LEGADO.Trim = PRO_CLAVE).FirstOrDefault

        If Not IsNothing(q) Then
            Costo = q.TOTAL_COSTO_PRODUCT0
        Else
            Costo = 0
        End If
        Return Costo
    End Function
    Private Function GetSAPCentroBeneficio(ByVal SUC_CLAVE As Long) As String
        Dim Temporal As String = String.Empty
        Dim q = (From p In OrigenCostosData.ODS_SUCURSALES Where p.SUCURSAL_CLAVE = SUC_CLAVE).FirstOrDefault
        If Not IsNothing(q) Then
            Temporal = q.SAP_CENTROBENEFICIO
        End If
        Return Temporal
    End Function
    Private Function GetSAPCentroCostos(ByVal SUC_CLAVE As Long) As String
        Dim Temporal As String = String.Empty
        Dim q = (From p In OrigenCostosData.ODS_SUCURSALES Where p.SUCURSAL_CLAVE = SUC_CLAVE).FirstOrDefault
        If Not IsNothing(q) Then
            Temporal = q.SAP_CENTROCOSTO
        End If

        Return Temporal
    End Function
    Private Function GetDebitoMovimiento(ByVal MOV_CLAVE As Long) As String

        Dim NumeroCuenta As String = String.Empty
        Dim movimientos = From catalogo In OrigenCostosData.POLIZASCOSTOS _
           Where catalogo.MOV_CLAVE = MOV_CLAVE
        For Each movimiento In movimientos
            NumeroCuenta = movimiento.CUENTA_CONTABLE_DR
        Next
        Return NumeroCuenta
    End Function
   
    Private Function GetCreditoMovimiento(ByVal MOV_CLAVE As Long) As String
        Dim NumeroCuenta As String = String.Empty
        Dim movimientos = From catalogo In OrigenCostosData.POLIZASCOSTOS _
           Where catalogo.MOV_CLAVE = MOV_CLAVE
        For Each movimiento In movimientos
            NumeroCuenta = movimiento.CUENTA_CONTABLE_CR
        Next
        Return NumeroCuenta
    End Function
    Private Function GetRefDocNo(ByVal MOV_CLAVE As Long) As String
        Dim Cadena As New StringBuilder
        Cadena.Append(GetNombrePolizaCorto(MOV_CLAVE)).Append(InfoCache.APS)

        Return Cadena.ToString

    End Function
    Private Function GetRefHeader(ByVal MOV_CLAVE As Long) As String
        Dim Cadena As New StringBuilder
        Cadena.Append(GetNombrePolizaLargo(MOV_CLAVE)).Append(InfoCache.APS)

        Return Cadena.ToString

    End Function
    Private Function GetNombrePolizaCorto(ByVal MOV_CLAVE As Long) As String
        Dim nombreCortoPoliza As String = String.Empty
        Dim movimientos = From catalogo In OrigenCostosData.POLIZASCOSTOS _
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
        Dim movimientos = From catalogo In OrigenCostosData.POLIZASCOSTOS _
           Where catalogo.MOV_CLAVE = MOV_CLAVE
        For Each movimiento In movimientos
            nombreCortoPoliza = movimiento.NOMBRE_POLIZA
        Next
        Return nombreCortoPoliza
    End Function

#End Region

End Class
