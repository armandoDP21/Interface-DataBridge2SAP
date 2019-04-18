Imports System.Text

'Imports Oracle.DataAccess.Client
'Imports Oracle.DataAccess.Types
'Imports System.Data.Common

Public Class ISAPcostoventas

    Public Event Mensaje(TextoMensaje As String)
    Dim start, finish, totalTime As Double

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

    Public Function ProcesarCostoVentas() As Integer
        start = Microsoft.VisualBasic.DateAndTime.Timer
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Proceso: " & "Pólizas de inventario" & " de: " & InfoCache.FechaDesde.ToString & " a:" & InfoCache.FechaHasta.ToString)
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Pais: " & InfoCache.NombrePais)






        MovimientoSegregado = New ODS_MOVIMIENTOPRODUCTO_SEGREGADO
        CargarMaestro()
        CargarNoPolizaPais()
        CargarMovimientosDeProductos()
        T1()
        T3()


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

#Region "Inicializacion"
    Private Sub CargarMaestro()
        RaiseEvent Mensaje(Format(Date.Now, "R") & ">  " & "Cargando datos maestros.")
        Maestro = (New PDCVDatos).MaestroSelectData()
    End Sub
    Private Sub CargarNoPolizaPais()

        'TODO:   agregar Me.SecuenciaInicial.Text en algun lado
        'NoPoliza = CLng(Me.SecuenciaInicial.Text)
        '------------------------------------------------------

        'TODO: habilitar el uso en proxima version

        'Dim Secuencia As OSDMaestroData
        'StatusLabelDisplay("Cargando secuencia del pais")
        'My.Application.DoEvents()
        'Secuencia = (New PDCVDatos).SecuenciaSelectData()
        'InfoCache.Secuencia = Secuencia.VS_SECUENCIAPAIS(0).SECUENCIA
    End Sub
    
#End Region
#Region "Liquidaciones y Movimientos de Productos"
    Private Sub CargarMovimientosDeProductos()
        RaiseEvent Mensaje(Format(Date.Now, "R") & ">  " & "Cargando ovimientos de productos.")
        MovimientosTotalDeProductos = (New PDCVDatos).MovimientoTotalDeProductosSelectData
    End Sub
#End Region
#Region "Procesar datos"
    Private Sub T1()


        With Maestro.ODS_CATALOGO_MOVIMIENTOSPROD
            For Each tablerow As OSDMaestroData.ODS_CATALOGO_MOVIMIENTOSPRODRow In .Rows
                If tablerow.MOV_CLAVE = 16 Then
                    Continue For
                Else
                    Select Case InfoCache.PaisClave
                        Case 2
                            Select Case tablerow.MOV_CLAVE
                                Case 16
                                    Continue For
                                Case Else
                            End Select
                        Case 3
                            Select Case tablerow.MOV_CLAVE
                                Case 16
                                    Continue For
                                Case Else
                            End Select
                        Case 4
                            Select Case tablerow.MOV_CLAVE
                                Case 11, 28, 29, 34, 63, 64, 65, 66, 67, 68, 69
                                    Continue For
                                Case Else
                            End Select
                        Case 5
                            Select Case tablerow.MOV_CLAVE
                                Case 16, 37
                                    Continue For
                                Case Else
                            End Select
                           
                        Case 6, 7
                            Select Case tablerow.MOV_CLAVE
                                Case 3, 11, 28, 29, 34, 37, 38, 79, 81, 40, 41
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


                'StatusLabelDisplay("Procesando movimientos de tipo:" & tablerow("MOV_CLAVE").ToString)
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

                'StatusLabelDisplay("Procesando movimientos de tipo:" & tablerow("MOV_CLAVE").ToString)
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
                    'With VerDatosForm
                    '    .Grid_Calendario.DataSource = MovimientoSegregado.DesgloseMOV
                    'End With


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
       
    End Sub

#End Region
#Region "Registra datos"
    Private Sub DeleteDatos()
        Dim result As Integer = (New MenuSP).DeletePolizasCostoVentasSemana
    End Sub
    Private Function PersistInDatabase() As Integer
        SapGLInterfase = New SAP_GLINTERFASEData

        With MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO
            For Each tablerow As ODS_MOVIMIENTOPRODUCTO_SEGREGADO.ODS_MOVIMIENTOPRODUCTO_AGRUPADORow In .Rows
                Dim CuentaDR As String = GetDebitoMovimiento(tablerow.MOV_CLAVE)
                Dim CuentaCR As String = GetCreditoMovimiento(tablerow.MOV_CLAVE)
                With SapGLInterfase.SAP_GL_INTERFASE
                    Dim NewDataRowDB As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
                    With NewDataRowDB

                        .ESTATUS_SAP = InfoCache.ESTATUS_SAP

                        .DOCUMENT_DATE = Date.Today
                        .DOCUMENT_TYPE = "ZI"
                        .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")

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
                        .ACCOUNTING_DATE = InfoCache.FechaHasta
                        .CURRENCY_CODE = InfoCache.CurrencyCode
                        .LEDGER_GROUP = String.Empty
                        .SAP_TAX_INDICATOR = String.Empty

                    End With
                    Dim NewDataRowCR As SAP_GLINTERFASEData.SAP_GL_INTERFASERow = .NewSAP_GL_INTERFASERow
                   

                    With NewDataRowCR
                        .ESTATUS_SAP = InfoCache.ESTATUS_SAP
                        .DOCUMENT_DATE = Date.Today
                        .DOCUMENT_TYPE = "ZI"
                        .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")
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

                        .ACCOUNTING_DATE = InfoCache.FechaHasta
                        .CURRENCY_CODE = InfoCache.CurrencyCode
                        .LEDGER_GROUP = String.Empty
                        .SAP_TAX_INDICATOR = String.Empty

                    End With
                    Try
                        .AddSAP_GL_INTERFASERow(NewDataRowDB)
                        .AddSAP_GL_INTERFASERow(NewDataRowCR)
                        .AcceptChanges()
                    Catch ex As Exception

                    End Try


                End With
                InfoCache.Secuencia += 1
            Next
        End With



        InfoCache.UpdateError = String.Empty

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

    '    With MovimientoSegregado.ODS_MOVIMIENTOPRODUCTO_AGRUPADO
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
    '                    .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")
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
    '                    .REF_DOCUMENT_NUMBER = "A" & InfoCache.PaisClave & Format(InfoCache.Secuencia, "000000")
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
    '            InfoCache.Secuencia += 1
    '        Next
    '    End With


    '    'definitiva

    '    With VerDatosForm.GRID_SAP_GL_INTERFASE
    '        .DataSource = SapGLInterfase.SAP_GL_INTERFASE
    '    End With
    '    InfoCache.SecuenciaTermina = InfoCache.Secuencia

    '    'Dim result As Boolean = (New PDCVDatos).insertarEnGL(SapGLInterfase.SAP_GL_INTERFASE)

    '    With SapGLInterfase.SAP_GL_INTERFASE
    '        For Each tablerow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow In .Rows
    '            Dim result As Boolean = (New PDCVDatos).insert(tablerow)
    '        Next
    '    End With
    '    'Me.SecuenciaInicial.Text = (InfoCache.Secuencia + 1).ToString


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
#Region "LookUp"

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





End Class
