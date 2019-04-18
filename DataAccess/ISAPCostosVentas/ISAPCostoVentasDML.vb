Imports Bis.Sap.Common

Imports System.Data.Common
Imports System.Text

Namespace Bis.Sap.DataAccess
    Public Class ISAPCostoVentasDML
#Region "SelectData"
        Public Function MaestroSelectData() As OSDMaestroData

            Dim dsMaestro As New OSDMaestroData
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()



            Dim cmdCatalogoMovimientoProductos As New OracleCommand
            Dim adCatalogoMovimientoProductos As OracleDataAdapter = New OracleDataAdapter(cmdCatalogoMovimientoProductos)
            With cmdCatalogoMovimientoProductos
                .Connection = conn
                .CommandText = "PRODODS.VS_CATALOGO_MOV_PROD_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("INPAIS_CLAVE", OracleDbType.Long)
                p1.Value = InfoCache.PaisClave
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)
                Try
                    adCatalogoMovimientoProductos.Fill(dsMaestro, "ODS_CATALOGO_MOVIMIENTOSPROD")
                Catch ex As Exception

                End Try

                .Dispose()
            End With

            Dim cmdSucursalesPais As New OracleCommand
            Dim adSucursalesPais As OracleDataAdapter = New OracleDataAdapter(cmdSucursalesPais)
            With cmdSucursalesPais
                .Connection = conn
                .CommandText = "PRODODS.VS_SUCURSALES_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("INPAIS_CLAVE", OracleDbType.Long)
                p1.Value = InfoCache.PaisClave
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)
                Try
                    adSucursalesPais.Fill(dsMaestro, "ODS_SUCURSALES")
                Catch ex As Exception

                End Try

                .Dispose()
            End With

            Dim cmdInterfaseCostos As New OracleCommand
            Dim adInterfaseCostos As OracleDataAdapter = New OracleDataAdapter(cmdInterfaseCostos)
            With cmdInterfaseCostos
                .Connection = conn
                .CommandText = "PRODODS.VS_INTERFASE_COSTOS_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("InNoCia", OracleDbType.Char)
                p1.Value = InfoCache.ClaveCompania
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("INFISCALYEAR", OracleDbType.Long)
                p2.Value = InfoCache.FiscalYear
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("INPERIODO", OracleDbType.Char)
                p3.Value = InfoCache.PeriodoActual
                .Parameters.Add(p3)

                Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p4)

                Try
                    adInterfaseCostos.Fill(dsMaestro, "SAP_INTERFASE_COSTOS")
                Catch ex As Exception

                End Try

                .Dispose()
            End With

            conn.Close()
            Return dsMaestro
        End Function
        Public Function MovimientoTotalDeProductosSelectData() As OSDMaestroData
            Dim dsMaestro As New OSDMaestroData
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()


            Dim cmdMovimientosDeProductos As New OracleCommand
            Dim adMovimientosDeProductos As OracleDataAdapter = New OracleDataAdapter(cmdMovimientosDeProductos)
            With cmdMovimientosDeProductos
                .Connection = conn
                .CommandText = "PRODODS.VS_MOVIMIENTOSPROD_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
                p1.Value = InfoCache.PaisClave
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
                p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.NVarchar2)
                p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
                .Parameters.Add(p3)

                Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p4)

                Try
                    adMovimientosDeProductos.Fill(dsMaestro, "VS_MOVIMIENTOSPROD")
                Catch ex As Exception

                End Try

                .Dispose()
            End With

            conn.Close()

            Return dsMaestro
        End Function

        'Public Function LiquidacionesSelectData() As LiquidacionesData
        '    Dim dsLiquidaciones As New LiquidacionesData
        '    Dim conn As OracleConnection = New OracleConnection
        '    conn.ConnectionString = InfoCache.ConnectionString
        '    conn.Open()

        '    Dim cmdLiquidaciones As New OracleCommand
        '    Dim adLiquidaciones As OracleDataAdapter = New OracleDataAdapter(cmdLiquidaciones)
        '    With cmdLiquidaciones
        '        .Connection = conn
        '        .CommandText = "PRODODS.VS_ODS_LIQUIDACIONES_SELECT"
        '        .CommandType = CommandType.StoredProcedure

        '        Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        '        p1.Value = InfoCache.PaisClave
        '        .Parameters.Add(p1)

        '        Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
        '        p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '        .Parameters.Add(p2)

        '        Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.NVarchar2)
        '        p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '        .Parameters.Add(p3)

        '        Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '        .Parameters.Add(p4)

        '        Try
        '            adLiquidaciones.Fill(dsLiquidaciones, "VS_ODS_LIQUIDACIONES")
        '        Catch ex As Exception

        '        End Try

        '        .Dispose()
        '    End With
        '    If InfoCache.PaisClave = 4 Or InfoCache.PaisClave = 6 Or InfoCache.PaisClave = 7 Then
        '        Dim cmdBancaCAMSUR As New OracleCommand
        '        Dim adBancaCAMSUR As OracleDataAdapter = New OracleDataAdapter(cmdBancaCAMSUR)
        '        With cmdBancaCAMSUR
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_BANCOSCAMSUR_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)

        '            Try
        '                adBancaCAMSUR.Fill(dsLiquidaciones, "BANCOSCAMSUR")
        '            Catch ex As Exception

        '            End Try

        '            .Dispose()
        '        End With
        '    End If

        '    If InfoCache.PaisClave = 4 Or InfoCache.PaisClave = 6 Or InfoCache.PaisClave = 7 Then
        '        Dim cmdDepositosCAMSUR As New OracleCommand
        '        Dim adDepositosCAMSUR As OracleDataAdapter = New OracleDataAdapter(cmdDepositosCAMSUR)
        '        With cmdDepositosCAMSUR
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_DEPOSITOSCAMSUR_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)

        '            Try
        '                adDepositosCAMSUR.Fill(dsLiquidaciones, "DEPOSITOSCAMSUR")
        '            Catch ex As Exception

        '            End Try

        '            .Dispose()
        '        End With
        '    End If
        '    If InfoCache.PaisClave = 6 Or InfoCache.PaisClave = 7 Then
        '        Dim cmdDepositosQuakerSabritas As New OracleCommand
        '        Dim adDepositosQuakerSabritas As OracleDataAdapter = New OracleDataAdapter(cmdDepositosQuakerSabritas)
        '        With cmdDepositosQuakerSabritas
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_DEPQKERSABRTS_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)

        '            Try
        '                adDepositosQuakerSabritas.Fill(dsLiquidaciones, "DepositosQuakerSabritas")
        '            Catch ex As Exception

        '            End Try

        '            .Dispose()
        '        End With


        '    End If
        '    If InfoCache.PaisClave = 2 Or InfoCache.PaisClave = 3 Or InfoCache.PaisClave = 5 Then

        '        Dim cmdLiquidacionesBancos As New OracleCommand
        '        Dim adLiquidacionesBancos As OracleDataAdapter = New OracleDataAdapter(cmdLiquidacionesBancos)
        '        With cmdLiquidacionesBancos
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_LIQUIDACIONESBANCOS_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)

        '            Try
        '                adLiquidacionesBancos.Fill(dsLiquidaciones, "VS_LIQUIDACIONESBANCOS")
        '            Catch ex As Exception

        '            End Try

        '            .Dispose()
        '        End With

        '    End If
        '    If InfoCache.PaisClave = 4 Then
        '        Dim cmdDepositosVDPA As New OracleCommand
        '        Dim adDepositosVDPA As OracleDataAdapter = New OracleDataAdapter(cmdDepositosVDPA)
        '        With cmdDepositosVDPA
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_VDPADepositos_Select"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            Try
        '                adDepositosVDPA.Fill(dsLiquidaciones, "VDPADEPOSITOS")
        '            Catch ex As Exception

        '            End Try

        '            .Dispose()
        '        End With
        '    End If
        '    conn.Close()

        '    Return dsLiquidaciones
        'End Function
        'Public Function TMFProductoSelectData() As ImpuestosProductoData
        '    Dim dsMaestro As New ImpuestosProductoData
        '    Dim conn As OracleConnection = New OracleConnection
        '    conn.ConnectionString = InfoCache.ConnectionString
        '    conn.Open()

        '    Dim cmdTMFProducto As New OracleCommand
        '    Dim adTMFProducto As OracleDataAdapter = New OracleDataAdapter(cmdTMFProducto)
        '    With cmdTMFProducto
        '        .Connection = conn
        '        .CommandText = "PRODODS.VS_ODSTMFPRODUCTO_SELECT"
        '        .CommandType = CommandType.StoredProcedure

        '        Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        '        p1.Value = InfoCache.PaisClave
        '        .Parameters.Add(p1)

        '        Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '        .Parameters.Add(p2)

        '        adTMFProducto.Fill(dsMaestro, "VS_ODSTMFPRODUCTO")
        '        .Dispose()
        '    End With

        '    conn.Close()

        '    Return dsMaestro


        'End Function
        'Public Function CatalogoPolizasSelectData() As VS_CATALOGOPOLIZASData
        '    Dim PolizasDataSet As New VS_CATALOGOPOLIZASData

        '    Dim conn As OracleConnection = New OracleConnection

        '    conn.ConnectionString = InfoCache.ConnectionString
        '    conn.Open()
        '    Dim cmdMaster As New OracleCommand
        '    Dim cmdDetails As New OracleCommand
        '    cmdMaster.Connection = conn
        '    cmdDetails.Connection = conn


        '    Dim adMaster As OracleDataAdapter = New OracleDataAdapter(cmdMaster)
        '    Dim adDetails As OracleDataAdapter = New OracleDataAdapter(cmdDetails)


        '    'Try
        '    With cmdMaster
        '        .CommandText = "PRODODS.VS_CATALOGOPOLIZAS_SELECT"
        '        .CommandType = CommandType.StoredProcedure
        '        Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '        p1.Value = InfoCache.PaisClave
        '        .Parameters.Add(p1)
        '        Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '        .Parameters.Add(p2)
        '        adMaster.Fill(PolizasDataSet, "VS_CATALOGOPOLIZAS")
        '        .Dispose()
        '    End With




        '    With cmdDetails
        '        .CommandText = "PRODODS.VS_CATPOLIZASDETALLES_SELECT"
        '        .CommandType = CommandType.StoredProcedure
        '        Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '        p1.Value = InfoCache.PaisClave
        '        .Parameters.Add(p1)
        '        Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '        .Parameters.Add(p2)
        '        adDetails.Fill(PolizasDataSet, "VS_CATALOGOPOLIZASDETALLE")
        '        .Dispose()

        '    End With

        '    conn.Close()

        '    Return PolizasDataSet


        'End Function
        'Public Function MovimientosSelectData() As MovimientosDeProductosData
        '    Dim dsMovimientos As New MovimientosDeProductosData
        '    Dim conn As OracleConnection = New OracleConnection
        '    conn.ConnectionString = InfoCache.ConnectionString
        '    conn.Open()

        '    If InfoCache.PaisClave = 6 Or InfoCache.PaisClave = 7 Then

        '        Dim cmdCarga As New OracleCommand
        '        Dim adCarga As OracleDataAdapter = New OracleDataAdapter(cmdCarga)
        '        With cmdCarga
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_CARGADEMOVSSABQUA_SELECT"
        '            .CommandType = CommandType.StoredProcedure
        '            Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)
        '            Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)
        '            adCarga.Fill(dsMovimientos, "VS_CARGA")
        '            .Dispose()
        '        End With

        '        Dim cmdCargaSinIVA As New OracleCommand
        '        Dim adCargaSinIVA As OracleDataAdapter = New OracleDataAdapter(cmdCargaSinIVA)
        '        With cmdCargaSinIVA
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_CARGADEMOVSSABQUANO_SELECT"
        '            .CommandType = CommandType.StoredProcedure
        '            Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)
        '            Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)
        '            adCargaSinIVA.Fill(dsMovimientos, "VS_CARGASinIva")
        '            .Dispose()
        '        End With

        '        Dim cmdDescuentosSinIVA As New OracleCommand
        '        Dim adDescuentosSinIVA As OracleDataAdapter = New OracleDataAdapter(cmdDescuentosSinIVA)
        '        With cmdDescuentosSinIVA
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_DescuentosSINIvaCRQK_Select"
        '            .CommandType = CommandType.StoredProcedure
        '            Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)
        '            Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)
        '            adDescuentosSinIVA.Fill(dsMovimientos, "DESCUENTOSSINIVACRQK")
        '            .Dispose()
        '        End With
        '        Dim cmdGastosSabQuak As New OracleCommand
        '        Dim adGastosSabQuak As OracleDataAdapter = New OracleDataAdapter(cmdGastosSabQuak)
        '        With cmdGastosSabQuak
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_GASTOSSABQUAK_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)

        '            Try
        '                adGastosSabQuak.Fill(dsMovimientos, "GastosSabQuak")
        '            Catch ex As Exception

        '            End Try

        '            .Dispose()
        '        End With
        '    Else
        '        Dim cmdCarga As New OracleCommand
        '        Dim adCarga As OracleDataAdapter = New OracleDataAdapter(cmdCarga)
        '        With cmdCarga
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_CARGADEMOVIMIENTOS_SELECT"
        '            .CommandType = CommandType.StoredProcedure
        '            Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '            p1.Value = InfoCache.PaisClave
        '            .Parameters.Add(p1)
        '            Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)
        '            adCarga.Fill(dsMovimientos, "VS_CARGA")
        '            .Dispose()
        '        End With
        '    End If



        '    Dim cmdTradePromotions As New OracleCommand
        '    Dim adTradePromotions As OracleDataAdapter = New OracleDataAdapter(cmdTradePromotions)
        '    With cmdTradePromotions
        '        .Connection = conn
        '        .CommandText = "PRODODS.VS_TRADEPROMOTIONS_SELECT"
        '        .CommandType = CommandType.StoredProcedure
        '        Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '        p1.Value = InfoCache.PaisClave
        '        .Parameters.Add(p1)
        '        Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '        p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '        .Parameters.Add(p2)

        '        Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '        p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '        .Parameters.Add(p3)

        '        Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '        .Parameters.Add(p4)
        '        adTradePromotions.Fill(dsMovimientos, "VS_TRADEPROMOTIONS")
        '        .Dispose()

        '    End With

        '    If InfoCache.PaisClave = 3 Then
        '        Dim cmdHonduras As New OracleCommand
        '        Dim adHonduras As OracleDataAdapter = New OracleDataAdapter(cmdHonduras)
        '        With cmdHonduras
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_CARGAMOVIMIENTOSHON_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            'Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '            'p1.Value = InfoCache.PaisClave
        '            '.Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)
        '            adHonduras.Fill(dsMovimientos, "VS_CARGAHONDURAS")
        '            .Dispose()

        '        End With
        '    End If

        '    If InfoCache.PaisClave = 4 Or InfoCache.PaisClave = 6 Or InfoCache.PaisClave = 7 Then
        '        Dim cmdCAMSventas As New OracleCommand
        '        Dim adCAMSventas As OracleDataAdapter = New OracleDataAdapter(cmdCAMSventas)
        '        With cmdCAMSventas
        '            .Connection = conn
        '            Select Case InfoCache.PaisClave
        '                Case 4
        '                    .CommandText = "PRODODS.VS_LIQUIDACIONESPANAMA_SELECT"
        '                Case 6
        '                    .CommandText = "PRODODS.VS_LIQUIDACIONESCR_SELECT"
        '                Case 7
        '                    .CommandText = "PRODODS.VS_LIQUIDACIONESQK_SELECT"
        '            End Select

        '            .CommandType = CommandType.StoredProcedure


        '            Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p3)

        '            Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p4)
        '            adCAMSventas.Fill(dsMovimientos, "VS_CAMSVENTAS")
        '            .Dispose()

        '        End With
        '    End If
        '    If InfoCache.PaisClave = 6 Then
        '        Dim cmdDevolucionesCR As New OracleCommand
        '        Dim adDevolucionesCR As OracleDataAdapter = New OracleDataAdapter(cmdDevolucionesCR)
        '        With cmdDevolucionesCR
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_DEVOLUCIONESCR_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adDevolucionesCR.Fill(dsMovimientos, "VS_DEVOLUCIONESCR")
        '            .Dispose()

        '        End With

        '        Dim cmdDevolucionesSinIvaCR As New OracleCommand
        '        Dim adDevolucionesSinIvaCR As OracleDataAdapter = New OracleDataAdapter(cmdDevolucionesSinIvaCR)
        '        With cmdDevolucionesSinIvaCR
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_DEVOLUCIONESSINIVACR_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adDevolucionesSinIvaCR.Fill(dsMovimientos, "VS_DEVOLUCIONESSINIVACR")
        '            .Dispose()

        '        End With

        '    End If
        '    If InfoCache.PaisClave = 7 Then
        '        Dim cmdDevolucionesCR As New OracleCommand
        '        Dim adDevolucionesCR As OracleDataAdapter = New OracleDataAdapter(cmdDevolucionesCR)
        '        With cmdDevolucionesCR
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_DEVOLUCIONESQUAKER_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adDevolucionesCR.Fill(dsMovimientos, "VS_DEVOLUCIONESQUAKER")
        '            .Dispose()

        '        End With
        '        Dim cmdDevolucionesSinIvaQK As New OracleCommand
        '        Dim adDevolucionesSinIvaQK As OracleDataAdapter = New OracleDataAdapter(cmdDevolucionesSinIvaQK)
        '        With cmdDevolucionesSinIvaQK
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_DEVOLUCIONESSINIVAQK_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adDevolucionesSinIvaQK.Fill(dsMovimientos, "VS_DEVOLUCIONESSINIVAQK")
        '            .Dispose()

        '        End With

        '    End If
        '    If InfoCache.PaisClave = 4 Then
        '        Dim cmdDevolucionesPanama As New OracleCommand
        '        Dim adDevolucionesPanama As OracleDataAdapter = New OracleDataAdapter(cmdDevolucionesPanama)
        '        With cmdDevolucionesPanama
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_DEVOLUCIONESPANAMA_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adDevolucionesPanama.Fill(dsMovimientos, "VS_DEVOLUCIONESPANAMA")
        '            .Dispose()

        '        End With

        '        Dim cmdNCPromosPanama As New OracleCommand
        '        Dim adNCPromosPanama As OracleDataAdapter = New OracleDataAdapter(cmdNCPromosPanama)
        '        With cmdNCPromosPanama
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_NCPROMOSPANAMA_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adNCPromosPanama.Fill(dsMovimientos, "VS_NCPROMOSPANAMA")
        '            .Dispose()
        '        End With

        '        Dim cmdNCDiferenciasPanama As New OracleCommand
        '        Dim adNCDiferenciasPanama As OracleDataAdapter = New OracleDataAdapter(cmdNCDiferenciasPanama)
        '        With cmdNCDiferenciasPanama
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_NCDIFERENCIASPANAMA_SELECT"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adNCDiferenciasPanama.Fill(dsMovimientos, "VS_NCDIFERENCIASPANAMA")
        '            .Dispose()
        '        End With

        '        Dim cmdVentasVDPA As New OracleCommand
        '        Dim adVentasVDPA As OracleDataAdapter = New OracleDataAdapter(cmdVentasVDPA)
        '        With cmdVentasVDPA
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_VDPAVentas_Select"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adVentasVDPA.Fill(dsMovimientos, "VDPAVentas")
        '            .Dispose()

        '        End With
        '        Dim cmdDevBEVDPA As New OracleCommand
        '        Dim adDevBEVDPA As OracleDataAdapter = New OracleDataAdapter(cmdDevBEVDPA)
        '        With cmdDevBEVDPA
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_VDPADevBE_Select"

        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adDevBEVDPA.Fill(dsMovimientos, "VDPADEVBE")
        '            .Dispose()

        '        End With

        '        Dim cmdDevMEVDPA As New OracleCommand
        '        Dim adDevMEVDPA As OracleDataAdapter = New OracleDataAdapter(cmdDevMEVDPA)
        '        With cmdDevMEVDPA
        '            .Connection = conn

        '            .CommandText = "PRODODS.VS_VDPADevME_Select"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adDevMEVDPA.Fill(dsMovimientos, "VDPADEVME")
        '            .Dispose()

        '        End With

        '        Dim cmdDescEspecialVDPA As New OracleCommand
        '        Dim adDescEspecialVDPA As OracleDataAdapter = New OracleDataAdapter(cmdDescEspecialVDPA)
        '        With cmdDescEspecialVDPA
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_VDPADescEspecial_Select"
        '            .CommandType = CommandType.StoredProcedure

        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)

        '            adDescEspecialVDPA.Fill(dsMovimientos, "VDPADESCESPECIAL")
        '            .Dispose()

        '        End With

        '    End If
        '    If InfoCache.PaisClave = 4 Then
        '        Dim cmdCxCEmpleadosPanama As New OracleCommand
        '        Dim adCxCEmpleadosPanama As OracleDataAdapter = New OracleDataAdapter(cmdCxCEmpleadosPanama)
        '        With cmdCxCEmpleadosPanama
        '            .Connection = conn
        '            .CommandText = "PRODODS.VS_CXCEMPLEADOS_SELECT"
        '            .CommandType = CommandType.StoredProcedure


        '            Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '            p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '            .Parameters.Add(p1)

        '            Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '            p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '            .Parameters.Add(p2)

        '            Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '            .Parameters.Add(p3)
        '            adCxCEmpleadosPanama.Fill(dsMovimientos, "CXCEMPLEADOS")
        '            .Dispose()

        '        End With

        '    End If


        '    Dim cmdMov33 As New OracleCommand
        '    Dim adMov33 As OracleDataAdapter = New OracleDataAdapter(cmdMov33)
        '    With cmdMov33
        '        .Connection = conn
        '        .CommandText = "PRODODS.VS_MOVIMIENTO33PROD_SELECT"
        '        .CommandType = CommandType.StoredProcedure


        '        Dim p1 As OracleParameter = New OracleParameter("INMOV_CLAVE", OracleDbType.Long)
        '        Select Case InfoCache.PaisClave
        '            Case 2, 3
        '                p1.Value = 33
        '            Case 5
        '                p1.Value = 28
        '        End Select

        '        .Parameters.Add(p1)

        '        Dim p2 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
        '        p2.Value = InfoCache.PaisClave
        '        .Parameters.Add(p2)

        '        Dim p3 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
        '        p3.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
        '        .Parameters.Add(p3)

        '        Dim p4 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
        '        p4.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
        '        .Parameters.Add(p4)

        '        Dim p5 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '        .Parameters.Add(p5)
        '        adMov33.Fill(dsMovimientos, "VS_MOVIMIENTO33PROD")
        '        .Dispose()
        '    End With
        '    conn.Close()

        '    Return dsMovimientos



        'End Function
        'Public Function SecuenciaSelectData() As OSDMaestroData
        '    Dim conn As OracleConnection = New OracleConnection

        '    conn.ConnectionString = InfoCache.ConnectionString
        '    conn.Open()
        '    Dim cmd As New OracleCommand
        '    cmd.Connection = conn

        '    Dim MaestroAdapter As OracleDataAdapter = New OracleDataAdapter(cmd)
        '    Dim MaestroDataSet As New OSDMaestroData

        '    'Try
        '    cmd.CommandText = "PRODODS.VS_SECUENCIAPAIS_SELECT"
        '    cmd.CommandType = CommandType.StoredProcedure
        '    Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Long)
        '    p1.Value = InfoCache.PaisClave
        '    cmd.Parameters.Add(p1)

        '    Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
        '    cmd.Parameters.Add(p2)

        '    MaestroAdapter.Fill(MaestroDataSet, "VS_SECUENCIAPAIS")

        '    cmd.Dispose()
        '    conn.Close()

        '    Return MaestroDataSet


        'End Function
#End Region
#Region "Registrarinterfase"
        Public Function SelectSapGLInterfase() As OracleDataAdapter

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
            SelectString.Append("FROM SAP_GL_INTERFASE ")
            SelectString.Append("WHERE ")
            SelectString.Append("ESTATUS_SAP = 'A' ")

            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
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

            MaestroAdapter.Fill(MaestroDataSet, "ODS_CATALOGO_MOVIMIENTOSPROD")

            cmd.Dispose()
            conn.Close()

            Return MaestroAdapter
        End Function
        Public Function insertarEnGL(ByVal TablaDS As SAP_GLINTERFASEData.SAP_GL_INTERFASEDataTable) As Integer

            Dim TotalRegistros As Int32 = TablaDS.Rows.Count

            Dim arrayESTATUS_SAP(TotalRegistros) As String
            Dim arrayDOCUMENT_DATE(TotalRegistros) As Date
            Dim arrayDOCUMENT_TYPE(TotalRegistros) As String
            Dim arrayREF_DOCUMENT_NUMBER(TotalRegistros) As String
            Dim arrayDOC_HEADER_TEXT(TotalRegistros) As String
            Dim arraySAP_COMPANY_CODE(TotalRegistros) As String
            Dim arraySAP_ACCOUNT(TotalRegistros) As String
            Dim arraySAP_COST_CENTER(TotalRegistros) As String
            Dim arraySAP_PROFIT_CENTER(TotalRegistros) As String
            Dim arraySAP_SEGMENT(TotalRegistros) As String
            Dim arrayENTERED_DR(TotalRegistros) As Double
            Dim arrayENTERED_CR(TotalRegistros) As Double
            Dim arrayASSIGNMENT(TotalRegistros) As String
            Dim arrayLINE_TEXT(TotalRegistros) As String
            Dim arraySAP_REF_DOC_NUMBER(TotalRegistros) As String
            Dim arraySAP_REF_MESSAGE(TotalRegistros) As String
            Dim arrayACCOUNTING_DATE(TotalRegistros) As Date
            Dim arrayCURRENCY_CODE(TotalRegistros) As String
            Dim arrayLEDGER_GROUP(TotalRegistros) As String


            With TablaDS
                Dim i As Integer = 0
                For Each tableRow As SAP_GLINTERFASEData.SAP_GL_INTERFASERow In .Rows
                    arrayESTATUS_SAP(i) = tableRow.ESTATUS_SAP
                    arrayDOCUMENT_DATE(i) = tableRow.DOCUMENT_DATE
                    arrayDOCUMENT_TYPE(i) = tableRow.DOCUMENT_TYPE
                    arrayREF_DOCUMENT_NUMBER(i) = tableRow.REF_DOCUMENT_NUMBER
                    arrayDOC_HEADER_TEXT(i) = tableRow.DOC_HEADER_TEXT
                    arraySAP_COMPANY_CODE(i) = tableRow.SAP_COMPANY_CODE
                    arraySAP_ACCOUNT(i) = tableRow.SAP_ACCOUNT
                    arraySAP_COST_CENTER(i) = tableRow.SAP_COST_CENTER
                    arraySAP_PROFIT_CENTER(i) = tableRow.SAP_PROFIT_CENTER
                    arraySAP_SEGMENT(i) = tableRow.SAP_SEGMENT
                    arrayENTERED_DR(i) = tableRow.ENTERED_DR
                    arrayENTERED_CR(i) = tableRow.ENTERED_CR
                    arrayASSIGNMENT(i) = tableRow.ASSIGNMENT
                    arrayLINE_TEXT(i) = tableRow.LINE_TEXT
                    arraySAP_REF_DOC_NUMBER(i) = tableRow.SAP_REF_DOC_NUMBER
                    arraySAP_REF_MESSAGE(i) = tableRow.SAP_REF_MESSAGE
                    arrayACCOUNTING_DATE(i) = tableRow.ACCOUNTING_DATE
                    arrayCURRENCY_CODE(i) = tableRow.CURRENCY_CODE
                    arrayLEDGER_GROUP(i) = tableRow.LEDGER_GROUP
                    i += 1
                Next
            End With
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim cmd As New OracleCommand
            cmd.Connection = conn

            Dim MaestroAdapter As OracleDataAdapter = New OracleDataAdapter(cmd)
            Dim MaestroDataSet As New OSDMaestroData


            cmd.CommandText = "PRODODS.VS_SAPGLINTERFASE_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True
            cmd.ArrayBindCount = TotalRegistros

            Dim p1 As OracleParameter = New OracleParameter("Iestatus_sap", OracleDbType.Varchar2)
            Dim p2 As OracleParameter = New OracleParameter("IDOCUMENT_DATE", OracleDbType.Date)
            Dim p3 As OracleParameter = New OracleParameter("IDOCUMENT_TYPE", OracleDbType.Varchar2)
            Dim p4 As OracleParameter = New OracleParameter("IREF_DOCUMENT_NUMBER", OracleDbType.Varchar2)
            Dim p5 As OracleParameter = New OracleParameter("IDOC_HEADER_TEXT", OracleDbType.Varchar2)
            Dim p6 As OracleParameter = New OracleParameter("ISAP_COMPANY_CODE", OracleDbType.Varchar2)
            Dim p7 As OracleParameter = New OracleParameter("ISAP_ACCOUNT", OracleDbType.Varchar2)
            Dim p8 As OracleParameter = New OracleParameter("ISAP_COST_CENTER", OracleDbType.Varchar2)
            Dim p9 As OracleParameter = New OracleParameter("ISAP_PROFIT_CENTER", OracleDbType.Varchar2)
            Dim p10 As OracleParameter = New OracleParameter("ISAP_SEGMENT", OracleDbType.Varchar2)
            Dim p11 As OracleParameter = New OracleParameter("IENTERED_DR", OracleDbType.Double)
            Dim p12 As OracleParameter = New OracleParameter("IENTERED_CR", OracleDbType.Double)
            Dim p13 As OracleParameter = New OracleParameter("IASSIGNMENT", OracleDbType.Varchar2)
            Dim p14 As OracleParameter = New OracleParameter("ILINE_TEXT", OracleDbType.Varchar2)
            Dim p15 As OracleParameter = New OracleParameter("ISAP_REF_DOC_NUMBER", OracleDbType.Varchar2)
            Dim p16 As OracleParameter = New OracleParameter("ISAP_REF_MESSAGE", OracleDbType.Varchar2)
            Dim p17 As OracleParameter = New OracleParameter("IACCOUNTING_DATE", OracleDbType.Date)
            Dim p18 As OracleParameter = New OracleParameter("ICURRENCY_CODE", OracleDbType.Varchar2)
            Dim p19 As OracleParameter = New OracleParameter("ILEDGER_GROUP", OracleDbType.Varchar2)

            p1.Value = arrayESTATUS_SAP
            p2.Value = arrayDOCUMENT_DATE
            p3.Value = arrayDOCUMENT_TYPE
            p4.Value = arrayREF_DOCUMENT_NUMBER
            p5.Value = arrayDOC_HEADER_TEXT
            p6.Value = arraySAP_COMPANY_CODE
            p7.Value = arraySAP_ACCOUNT
            p8.Value = arraySAP_COST_CENTER
            p9.Value = arraySAP_PROFIT_CENTER
            p10.Value = arraySAP_SEGMENT
            p11.Value = arrayENTERED_DR
            p12.Value = arrayENTERED_CR
            p13.Value = arrayASSIGNMENT
            p14.Value = arrayLINE_TEXT
            p15.Value = arraySAP_REF_DOC_NUMBER
            p16.Value = arraySAP_REF_MESSAGE
            p17.Value = arrayACCOUNTING_DATE
            p18.Value = arrayCURRENCY_CODE
            p19.Value = arrayLEDGER_GROUP

            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)
            cmd.Parameters.Add(p4)
            cmd.Parameters.Add(p5)
            cmd.Parameters.Add(p6)
            cmd.Parameters.Add(p7)
            cmd.Parameters.Add(p8)
            cmd.Parameters.Add(p9)
            cmd.Parameters.Add(p10)
            cmd.Parameters.Add(p11)
            cmd.Parameters.Add(p12)
            cmd.Parameters.Add(p13)
            cmd.Parameters.Add(p14)
            cmd.Parameters.Add(p15)
            cmd.Parameters.Add(p16)
            cmd.Parameters.Add(p17)
            cmd.Parameters.Add(p18)
            cmd.Parameters.Add(p19)

            Dim recordsInesrtados As Integer
            Try
                recordsInesrtados = cmd.ExecuteNonQuery()
            Catch e As OracleException
                InfoCache.UpdateError = e.Message
            End Try

            cmd.Dispose()
            conn.Close()

            Return recordsInesrtados
        End Function
        Public Function insert(ByVal RowOrigen As SAP_GLINTERFASEData.SAP_GL_INTERFASERow) As Boolean
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim cmd As New OracleCommand
            cmd.Connection = conn

            Dim MaestroAdapter As OracleDataAdapter = New OracleDataAdapter(cmd)
            Dim MaestroDataSet As New OSDMaestroData


            cmd.CommandText = "PRODODS.VS_SAPGLINTERFASE_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True

            Dim p1 As OracleParameter = New OracleParameter("Iestatus_sap", OracleDbType.Varchar2)
            Dim p2 As OracleParameter = New OracleParameter("IDOCUMENT_DATE", OracleDbType.Date)
            Dim p3 As OracleParameter = New OracleParameter("IDOCUMENT_TYPE", OracleDbType.Varchar2)
            Dim p4 As OracleParameter = New OracleParameter("IREF_DOCUMENT_NUMBER", OracleDbType.Varchar2)
            Dim p5 As OracleParameter = New OracleParameter("IDOC_HEADER_TEXT", OracleDbType.Varchar2)
            Dim p6 As OracleParameter = New OracleParameter("ISAP_COMPANY_CODE", OracleDbType.Varchar2)
            Dim p7 As OracleParameter = New OracleParameter("ISAP_ACCOUNT", OracleDbType.Varchar2)
            Dim p8 As OracleParameter = New OracleParameter("ISAP_COST_CENTER", OracleDbType.Varchar2)
            Dim p9 As OracleParameter = New OracleParameter("ISAP_PROFIT_CENTER", OracleDbType.Varchar2)
            Dim p10 As OracleParameter = New OracleParameter("ISAP_SEGMENT", OracleDbType.Varchar2)
            Dim p11 As OracleParameter = New OracleParameter("IENTERED_DR", OracleDbType.Double)
            Dim p12 As OracleParameter = New OracleParameter("IENTERED_CR", OracleDbType.Double)
            Dim p13 As OracleParameter = New OracleParameter("IASSIGNMENT", OracleDbType.Varchar2)
            Dim p14 As OracleParameter = New OracleParameter("ILINE_TEXT", OracleDbType.Varchar2)
            Dim p15 As OracleParameter = New OracleParameter("ISAP_REF_DOC_NUMBER", OracleDbType.Varchar2)
            Dim p16 As OracleParameter = New OracleParameter("ISAP_REF_MESSAGE", OracleDbType.Varchar2)
            Dim p17 As OracleParameter = New OracleParameter("IACCOUNTING_DATE", OracleDbType.Date)
            Dim p18 As OracleParameter = New OracleParameter("ICURRENCY_CODE", OracleDbType.Varchar2)
            Dim p19 As OracleParameter = New OracleParameter("ILEDGER_GROUP", OracleDbType.Varchar2)

            p1.Value = RowOrigen.ESTATUS_SAP
            p2.Value = RowOrigen.DOCUMENT_DATE
            p3.Value = RowOrigen.DOCUMENT_TYPE
            p4.Value = RowOrigen.REF_DOCUMENT_NUMBER
            p5.Value = RowOrigen.DOC_HEADER_TEXT
            p6.Value = RowOrigen.SAP_COMPANY_CODE
            p7.Value = RowOrigen.SAP_ACCOUNT
            p8.Value = RowOrigen.SAP_COST_CENTER
            p9.Value = RowOrigen.SAP_PROFIT_CENTER
            p10.Value = RowOrigen.SAP_SEGMENT
            p11.Value = RowOrigen.ENTERED_DR
            p12.Value = RowOrigen.ENTERED_CR
            p13.Value = RowOrigen.ASSIGNMENT
            p14.Value = RowOrigen.LINE_TEXT
            p15.Value = RowOrigen.SAP_REF_DOC_NUMBER
            p16.Value = RowOrigen.SAP_REF_MESSAGE
            p17.Value = RowOrigen.ACCOUNTING_DATE
            p18.Value = RowOrigen.CURRENCY_CODE
            p19.Value = RowOrigen.LEDGER_GROUP

            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)
            cmd.Parameters.Add(p4)
            cmd.Parameters.Add(p5)
            cmd.Parameters.Add(p6)
            cmd.Parameters.Add(p7)
            cmd.Parameters.Add(p8)
            cmd.Parameters.Add(p9)
            cmd.Parameters.Add(p10)
            cmd.Parameters.Add(p11)
            cmd.Parameters.Add(p12)
            cmd.Parameters.Add(p13)
            cmd.Parameters.Add(p14)
            cmd.Parameters.Add(p15)
            cmd.Parameters.Add(p16)
            cmd.Parameters.Add(p17)
            cmd.Parameters.Add(p18)
            cmd.Parameters.Add(p19)



            Try
                Dim recordsInesrtados As Integer = cmd.ExecuteNonQuery()
            Catch ex As Exception

            End Try



            cmd.Dispose()
            conn.Close()

            Return True
        End Function
        Public Function insertInTemp(ByVal RowOrigen As SAP_GLINTERFASEData.SAP_GL_TEMPRow) As Boolean
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()


            Dim cmd As New OracleCommand
            cmd.Connection = conn

            Dim MaestroAdapter As OracleDataAdapter = New OracleDataAdapter(cmd)
            Dim MaestroDataSet As New OSDMaestroData


            cmd.CommandText = "PRODODS.VS_SAP_GL_TEMP_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True

            Dim p1 As OracleParameter = New OracleParameter("Iestatus_sap", OracleDbType.Varchar2)
            Dim p2 As OracleParameter = New OracleParameter("IDOCUMENT_DATE", OracleDbType.Date)
            Dim p3 As OracleParameter = New OracleParameter("IDOCUMENT_TYPE", OracleDbType.Varchar2)
            Dim p4 As OracleParameter = New OracleParameter("IREF_DOCUMENT_NUMBER", OracleDbType.Varchar2)
            Dim p5 As OracleParameter = New OracleParameter("IDOC_HEADER_TEXT", OracleDbType.Varchar2)
            Dim p6 As OracleParameter = New OracleParameter("ISAP_COMPANY_CODE", OracleDbType.Varchar2)
            Dim p7 As OracleParameter = New OracleParameter("ISAP_ACCOUNT", OracleDbType.Varchar2)
            Dim p8 As OracleParameter = New OracleParameter("ISAP_COST_CENTER", OracleDbType.Varchar2)
            Dim p9 As OracleParameter = New OracleParameter("ISAP_PROFIT_CENTER", OracleDbType.Varchar2)
            Dim p10 As OracleParameter = New OracleParameter("ISAP_SEGMENT", OracleDbType.Varchar2)
            Dim p11 As OracleParameter = New OracleParameter("IENTERED_DR", OracleDbType.Double)
            Dim p12 As OracleParameter = New OracleParameter("IENTERED_CR", OracleDbType.Double)
            Dim p13 As OracleParameter = New OracleParameter("IASSIGNMENT", OracleDbType.Varchar2)
            Dim p14 As OracleParameter = New OracleParameter("ILINE_TEXT", OracleDbType.Varchar2)
            Dim p15 As OracleParameter = New OracleParameter("ISAP_REF_DOC_NUMBER", OracleDbType.Varchar2)
            Dim p16 As OracleParameter = New OracleParameter("ISAP_REF_MESSAGE", OracleDbType.Varchar2)
            Dim p17 As OracleParameter = New OracleParameter("IACCOUNTING_DATE", OracleDbType.Date)
            Dim p18 As OracleParameter = New OracleParameter("ICURRENCY_CODE", OracleDbType.Varchar2)
            Dim p19 As OracleParameter = New OracleParameter("ILEDGER_GROUP", OracleDbType.Varchar2)

            p1.Value = RowOrigen.ESTATUS_SAP
            p2.Value = RowOrigen.DOCUMENT_DATE
            p3.Value = RowOrigen.DOCUMENT_TYPE
            p4.Value = RowOrigen.REF_DOCUMENT_NUMBER
            p5.Value = RowOrigen.DOC_HEADER_TEXT
            p6.Value = RowOrigen.SAP_COMPANY_CODE
            p7.Value = RowOrigen.SAP_ACCOUNT
            p8.Value = RowOrigen.SAP_COST_CENTER
            p9.Value = RowOrigen.SAP_PROFIT_CENTER
            p10.Value = RowOrigen.SAP_SEGMENT
            p11.Value = RowOrigen.ENTERED_DR
            p12.Value = RowOrigen.ENTERED_CR
            p13.Value = RowOrigen.ASSIGNMENT
            p14.Value = RowOrigen.LINE_TEXT
            p15.Value = RowOrigen.SAP_REF_DOC_NUMBER
            p16.Value = RowOrigen.SAP_REF_MESSAGE
            p17.Value = RowOrigen.ACCOUNTING_DATE
            p18.Value = RowOrigen.CURRENCY_CODE
            p19.Value = RowOrigen.LEDGER_GROUP

            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)
            cmd.Parameters.Add(p4)
            cmd.Parameters.Add(p5)
            cmd.Parameters.Add(p6)
            cmd.Parameters.Add(p7)
            cmd.Parameters.Add(p8)
            cmd.Parameters.Add(p9)
            cmd.Parameters.Add(p10)
            cmd.Parameters.Add(p11)
            cmd.Parameters.Add(p12)
            cmd.Parameters.Add(p13)
            cmd.Parameters.Add(p14)
            cmd.Parameters.Add(p15)
            cmd.Parameters.Add(p16)
            cmd.Parameters.Add(p17)
            cmd.Parameters.Add(p18)
            cmd.Parameters.Add(p19)



            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception

            End Try



            cmd.Dispose()
            conn.Close()

            Return True
        End Function
        Public Function insertarBitacora( _
                ByVal NombreInterfase As String, _
                ByVal PrefijoModulo As String,
                ByVal RegistrosEnGL As Integer,
                ByVal TiempoEjecucion As Long) As Boolean



            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim cmd As New OracleCommand
            cmd.Connection = conn

            Dim MaestroAdapter As OracleDataAdapter = New OracleDataAdapter(cmd)
            Dim MaestroDataSet As New OSDMaestroData


            cmd.CommandText = "PRODODS.VS_BITACORACORRIDAS_INSERT"
            cmd.CommandType = CommandType.StoredProcedure

            'ANO_FISCAL	NUMBER	
            'FECHA	DATE	
            'FECHA_CARGA	DATE	
            'HORA	VARCHAR2	
            'MACHINE_NAME	NVARCHAR2	
            'NOMBRE_INTERFASE	NVARCHAR2	
            'OS_VERSION	NVARCHAR2	
            'PAIS_CLAVE	NUMBER	
            'PERIODO	NVARCHAR2	
            'PREFIJO_MODULO	NVARCHAR2	
            'REGISTROS	NUMBER	
            'SECUENCIA_INICIA	NUMBER	
            'SECUENCIA_TERMINA	NUMBER	
            'SEMANA	NUMBER	
            'SO	NVARCHAR2	
            'STATUS_CORRIDA	NVARCHAR2	
            'TIEMPO_EJECUCION	NUMBER	
            'USUARIO_CLAVE	NUMBER	
            'USUARIO_NOMBRE	NVARCHAR2	

            Dim p1 As OracleParameter = New OracleParameter("iANO_FISCAL", OracleDbType.Int32)
            Dim p2 As OracleParameter = New OracleParameter("iFECHA", OracleDbType.Date)
            Dim p3 As OracleParameter = New OracleParameter("iFECHA_CARGA", OracleDbType.Date)
            Dim p4 As OracleParameter = New OracleParameter("iHORA", OracleDbType.Varchar2)
            Dim p5 As OracleParameter = New OracleParameter("iMACHINE_NAME", OracleDbType.NVarchar2)
            Dim p6 As OracleParameter = New OracleParameter("iNOMBRE_INTERFASE", OracleDbType.NVarchar2)
            Dim p7 As OracleParameter = New OracleParameter("iOS_VERSION", OracleDbType.NVarchar2)
            Dim p8 As OracleParameter = New OracleParameter("iPAIS_CLAVE", OracleDbType.Int32)
            Dim p9 As OracleParameter = New OracleParameter("iPERIODO", OracleDbType.NVarchar2)
            Dim p10 As OracleParameter = New OracleParameter("iPREFIJO_MODULO", OracleDbType.NVarchar2)
            Dim p11 As OracleParameter = New OracleParameter("iREGISTROS", OracleDbType.Int32)
            Dim p12 As OracleParameter = New OracleParameter("iSECUENCIA_INICIA", OracleDbType.Int64)
            Dim p13 As OracleParameter = New OracleParameter("iSECUENCIA_TERMINA", OracleDbType.Int64)
            Dim p14 As OracleParameter = New OracleParameter("iSEMANA", OracleDbType.Int32)
            Dim p15 As OracleParameter = New OracleParameter("iSO", OracleDbType.NVarchar2)
            Dim p16 As OracleParameter = New OracleParameter("iSTATUS_CORRIDA", OracleDbType.NVarchar2)
            Dim p17 As OracleParameter = New OracleParameter("iTIEMPO_EJECUCION", OracleDbType.Double)
            Dim p18 As OracleParameter = New OracleParameter("iUSUARIO_CLAVE", OracleDbType.Int32)
            Dim p19 As OracleParameter = New OracleParameter("iUSUARIO_NOMBRE", OracleDbType.NVarchar2)

            p1.Value = InfoCache.FiscalYear
            p2.Value = Date.Today
            p3.Value = InfoCache.FechaHasta
            p4.Value = Format(Now, "hh:mm tt")
            p5.Value = My.Computer.Name
            p6.Value = NombreInterfase
            p7.Value = My.Computer.Info.OSVersion
            p8.Value = InfoCache.PaisClave
            p9.Value = InfoCache.PeriodoActual
            p10.Value = PrefijoModulo
            p11.Value = RegistrosEnGL
            p12.Value = InfoCache.SecuenciaInicia
            p13.Value = InfoCache.SecuenciaTermina
            p14.Value = InfoCache.Semana
            p15.Value = My.Computer.Info.OSFullName
            p16.Value = "1"
            p17.Value = TiempoEjecucion
            p18.Value = 1 ' InfoCache.UId
            p19.Value = InfoCache.NombreUsuario


            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)
            cmd.Parameters.Add(p4)
            cmd.Parameters.Add(p5)
            cmd.Parameters.Add(p6)
            cmd.Parameters.Add(p7)
            cmd.Parameters.Add(p8)
            cmd.Parameters.Add(p9)
            cmd.Parameters.Add(p10)
            cmd.Parameters.Add(p11)
            cmd.Parameters.Add(p12)
            cmd.Parameters.Add(p13)
            cmd.Parameters.Add(p14)
            cmd.Parameters.Add(p15)
            cmd.Parameters.Add(p16)
            cmd.Parameters.Add(p17)
            cmd.Parameters.Add(p18)
            cmd.Parameters.Add(p19)



            Try
                Dim recordsInesrtados As Integer = cmd.ExecuteNonQuery()
            Catch ex As Exception

            End Try



            cmd.Dispose()
            conn.Close()

            Return True
        End Function
#End Region
    End Class
End Namespace

