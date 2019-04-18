
Imports Bis.Sap.Common


Imports System.Data.Common
Imports System.Text

'Option Strict On
'Option Explicit On


Namespace Bis.Sap.DataAccess

    Public Class CostosDML
#Region "SELECT"
        Public Function MaestroCostosSelectData() As CostosMasterData
            Dim dsMaestro As New CostosMasterData

            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim cmdCatalogoEmbarques As New OracleCommand
            Dim adCatalogoEmbarques As OracleDataAdapter = New OracleDataAdapter(cmdCatalogoEmbarques)
            With cmdCatalogoEmbarques
                .Connection = conn
                .CommandText = "PRODODS.VSC_CATALOGOEMBARQUES_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
                p1.Value = InfoCache.PaisClave
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)

                adCatalogoEmbarques.Fill(dsMaestro, "CATALOGOEMBARQUES")
                .Dispose()
            End With

           

            Dim cmdGastos As New OracleCommand
            Dim adGastosVarios As OracleDataAdapter = New OracleDataAdapter(cmdGastos)
            With cmdGastos
                .Connection = conn
                .CommandText = "PRODODS.VSC_DEFINICIONGASTOS_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p1)
                adGastosVarios.Fill(dsMaestro, "DEFINICIONGASTOS")
                .Dispose()
            End With

            Dim cmdSucursales As New OracleCommand
            Dim adSucursales As OracleDataAdapter = New OracleDataAdapter(cmdSucursales)
            With cmdSucursales
                .Connection = conn
                .CommandText = "PRODODS.VS_SUCURSALES_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
                p1.Value = InfoCache.PaisClave
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)
                Try
                    adSucursales.Fill(dsMaestro, "SUCURSALES")
                Catch ex As Exception
                    InfoCache.UpdateError = ex.Message
                End Try
                .Dispose()
            End With
           
            Return dsMaestro
        End Function
        Public Function EmbarquesSelectData() As EmbarquesData

            Dim dsMovimientos As New EmbarquesData
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()


            Dim cmdPeriodoAactual As New OracleCommand
            Dim adPeriodoAactual As OracleDataAdapter = New OracleDataAdapter(cmdPeriodoAactual)
            With cmdPeriodoAactual
                .Connection = conn
                .CommandText = "PRODODS.CostosPack.VSC_PERIODOACTUAL_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Int32)
                p1.Value = InfoCache.PaisClave
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)
                adPeriodoAactual.Fill(dsMovimientos, "PERIODOACTUAL")
                .Dispose()
            End With
            InfoCache.FiscalYear = dsMovimientos.PERIODOACTUAL(0).FISCAL_YEAR
            InfoCache.PeriodoActual = dsMovimientos.PERIODOACTUAL(0).PERIODO
            Dim daysInPeriod As Integer = System.DateTime.DaysInMonth(InfoCache.FiscalYear, CInt(InfoCache.PeriodoActual))

            InfoCache.FechaDesde = New System.DateTime(InfoCache.FiscalYear, CInt(InfoCache.PeriodoActual), 1)
            InfoCache.FechaHasta = New System.DateTime(InfoCache.FiscalYear, CInt(InfoCache.PeriodoActual), daysInPeriod)

            Dim cmdEmbarques As New OracleCommand
            Dim adEmbarques As OracleDataAdapter = New OracleDataAdapter(cmdEmbarques)
            With cmdEmbarques
                .Connection = conn
                .CommandText = "PRODODS.CostosPack.VSC_EMBARQUES_SELECT"
                .CommandType = CommandType.StoredProcedure


                 Dim p1 As OracleParameter = New OracleParameter("INFISCALYEAR", OracleDbType.Int32)
                p1.Value = InfoCache.FiscalYear
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("INPERIOD", OracleDbType.NVarchar2)
                p2.Value = InfoCache.PeriodoActual
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Int32)
                p3.Value = InfoCache.PaisClave
                .Parameters.Add(p3)


                Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p4)
                adEmbarques.Fill(dsMovimientos, "EMBARQUES")
                .Dispose()
            End With

            Dim cmdEmbarquesDetalles As New OracleCommand
            Dim adEmbarquesDetalles As OracleDataAdapter = New OracleDataAdapter(cmdEmbarquesDetalles)
            With cmdEmbarquesDetalles
                .Connection = conn
                .CommandText = "PRODODS.CostosPack.VSC_EMBARQUESDETALLES_select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("INFISCALYEAR", OracleDbType.Int32)
                p1.Value = InfoCache.FiscalYear
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("INPERIOD", OracleDbType.NVarchar2)
                p2.Value = InfoCache.PeriodoActual
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Int32)
                p3.Value = InfoCache.PaisClave
                .Parameters.Add(p3)

                Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p4)
                adEmbarquesDetalles.Fill(dsMovimientos, "EMBARQUESDETALLES")
                .Dispose()
            End With

            Dim cmdEmbarquesOrigen As New OracleCommand
            Dim adEmbarquesOrigen As OracleDataAdapter = New OracleDataAdapter(cmdEmbarquesOrigen)
            With cmdEmbarquesOrigen
                .Connection = conn
                .CommandText = "PRODODS.VSC_EMBARQUESORIGEN_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
                p1.Value = InfoCache.PaisClave
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
                p2.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
                p3.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
                .Parameters.Add(p3)

                Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p4)
                adEmbarquesOrigen.Fill(dsMovimientos, "EMBARQUESORIGEN")
                .Dispose()
            End With

            Dim cmdGastos As New OracleCommand
            Dim adGL As OracleDataAdapter = New OracleDataAdapter(cmdGastos)
            With cmdGastos
                .Connection = conn
                .CommandText = "PRODODS.CostosPack.VSC_EMBARQUESGASTOS_SELECT"
                .CommandType = CommandType.StoredProcedure


                Dim p1 As OracleParameter = New OracleParameter("INFISCALYEAR", OracleDbType.Int32)
                p1.Value = InfoCache.FiscalYear
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("INPERIOD", OracleDbType.NVarchar2)
                p2.Value = InfoCache.PeriodoActual
                .Parameters.Add(p2)


                Dim p3 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Int32)
                p3.Value = InfoCache.PaisClave
                .Parameters.Add(p3)

                Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p4)
                adGL.Fill(dsMovimientos, "EMBARQUESGASTOS")
                .Dispose()
            End With
            Return dsMovimientos
        End Function


        Public Function ItemsSinCostoSelectData(ByVal FechaDesde As Date, ByVal FechaHasta As Date) As InterfaseCostosData
            Dim dsInterfaseCostos As New InterfaseCostosData
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim cmdItemsSinCosto As New OracleCommand
            Dim adItemsSinCosto As OracleDataAdapter = New OracleDataAdapter(cmdItemsSinCosto)
            With cmdItemsSinCosto
                .Connection = conn
                .CommandText = "PRODODS.VSC_ItemsSinCosto_Select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Long)
                p1.Value = InfoCache.PaisClave
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
                p2.Value = Format(FechaDesde, "yyyy/MM/dd")
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
                p3.Value = Format(FechaHasta, "yyyy/MM/dd")
                .Parameters.Add(p3)

                Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p4)

                adItemsSinCosto.Fill(dsInterfaseCostos, "ItemsSinCosto")
                .Dispose()
            End With

            Dim cmdItemsCostoCero As New OracleCommand
            Dim adItemsCostoCero As OracleDataAdapter = New OracleDataAdapter(cmdItemsCostoCero)
            With cmdItemsCostoCero
                .Connection = conn
                .CommandText = "PRODODS.VSC_ItemsSinPrecio_Select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("INFISCALYEAR", OracleDbType.Int32)
                p1.Value = InfoCache.FiscalYear
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("INPERIOD", OracleDbType.NVarchar2)
                p2.Value = InfoCache.PeriodoActual
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("InNoCia", OracleDbType.Char)
                p3.Value = InfoCache.ClaveCompania
                .Parameters.Add(p3)

                Dim p4 As OracleParameter = New OracleParameter("p4", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p4)

                adItemsCostoCero.Fill(dsInterfaseCostos, "ItemsCostoCero")
                .Dispose()
            End With
            Return dsInterfaseCostos
        End Function

#End Region


#Region "INSERT"
        Public Function insertarEmbarque() As Integer

            Dim TotalRegistros As Int32 = InfoCache.NuevosEmbarquesDS.EMBARQUES.Rows.Count
            Dim arrayEMBARQUE_CLAVE(TotalRegistros) As String
            Dim arrayMMP_FECHA(TotalRegistros) As Date
            Dim arrayMMP_FOLIO(TotalRegistros) As Int32
            Dim arrayPAIS_CLAVE(TotalRegistros) As Int32
            Dim arraySUC_CLAVE(TotalRegistros) As Int32
            Dim arraySAP_FISCAL_YEAR(TotalRegistros) As Int32
            Dim arraySAP_PERIOD(TotalRegistros) As String
            Dim arrayMOV_CLAVE(TotalRegistros) As Int32
            Dim arraySTATUS(TotalRegistros) As String
            Dim arrayTIPO_CAMBIO(TotalRegistros) As Decimal
            Dim arrayCONTROL_NO(TotalRegistros) As String


            With InfoCache.NuevosEmbarquesDS.EMBARQUES
                Dim i As Integer = 0
                For Each tableRow As EmbarquesData.EMBARQUESRow In .Rows
                    arrayEMBARQUE_CLAVE(i) = tableRow.EMBARQUE_CLAVE
                    arrayMMP_FECHA(i) = tableRow.MMP_FECHA
                    arrayMMP_FOLIO(i) = tableRow.MMP_FOLIO
                    arrayPAIS_CLAVE(i) = tableRow.PAIS_CLAVE
                    arraySUC_CLAVE(i) = tableRow.SUC_CLAVE
                    arraySAP_FISCAL_YEAR(i) = tableRow.SAP_FISCAL_YEAR
                    arraySAP_PERIOD(i) = tableRow.SAP_PERIOD
                    arrayMOV_CLAVE(i) = tableRow.MOV_CLAVE
                    arraySTATUS(i) = tableRow.STATUS
                    arrayTIPO_CAMBIO(i) = tableRow.TIPO_CAMBIO
                    arrayCONTROL_NO(i) = tableRow.CONTROL_NO
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


            cmd.CommandText = "PRODODS.VSC_Embarque_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True
            cmd.ArrayBindCount = TotalRegistros

            Dim p1 As OracleParameter = New OracleParameter("IEMBARQUE_CLAVE", OracleDbType.NVarchar2)
            Dim p2 As OracleParameter = New OracleParameter("IMMP_FECHA", OracleDbType.Date)
            Dim p3 As OracleParameter = New OracleParameter("IMMP_FOLIO", OracleDbType.Int32)
            Dim p4 As OracleParameter = New OracleParameter("IPAIS_CLAVE", OracleDbType.Int32)
            Dim p5 As OracleParameter = New OracleParameter("ISUC_CLAVE", OracleDbType.Int32)
            Dim p6 As OracleParameter = New OracleParameter("ISAP_FISCAL_YEAR", OracleDbType.Int32)
            Dim p7 As OracleParameter = New OracleParameter("ISAP_PERIOD", OracleDbType.NVarchar2)
            Dim p8 As OracleParameter = New OracleParameter("IMOV_CLAVE", OracleDbType.Int32)
            Dim p9 As OracleParameter = New OracleParameter("ISTATUS", OracleDbType.NVarchar2)
            Dim p10 As OracleParameter = New OracleParameter("ITIPO_CAMBIO", OracleDbType.Decimal)
            Dim p11 As OracleParameter = New OracleParameter("CONTROL_NO", OracleDbType.NVarchar2)

            p1.Value = arrayEMBARQUE_CLAVE 'RowOrigen.EMBARQUE_CLAVE
            p2.Value = arrayMMP_FECHA 'RowOrigen.MMP_FECHA
            p3.Value = arrayMMP_FOLIO 'RowOrigen.MMP_FOLIO
            p4.Value = arrayPAIS_CLAVE 'RowOrigen.PAIS_CLAVE
            p5.Value = arraySUC_CLAVE 'RowOrigen.SUC_CLAVE
            p6.Value = arraySAP_FISCAL_YEAR 'RowOrigen.SAP_FISCAL_YEAR
            p7.Value = arraySAP_PERIOD 'RowOrigen.SAP_PERIOD
            p8.Value = arrayMOV_CLAVE 'RowOrigen.MOV_CLAVE
            p9.Value = arraySTATUS 'RowOrigen.STATUS
            p10.Value = arrayTIPO_CAMBIO
            p11.Value = arrayCONTROL_NO

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

            Dim recordsInesrtados As Integer
            Try
                recordsInesrtados = cmd.ExecuteNonQuery()
            Catch ex As Exception
                InfoCache.UpdateError = ex.Message
            End Try

            cmd.Dispose()
            conn.Close()

            Return recordsInesrtados
        End Function
        Public Function insertarEmbarqueDetalle() As Integer

            'IEMBARQUE_CLAVE in nVARCHAR2,
            'IPRO_CLAVE in nVARCHAR2,
            'IPRO_GRAMAJ in number ,
            'IPRO_DESCRI in nVARCHAR2,
            'IDMP_CANTID in number,
            'IDMP_PRECIO in number,
            'ISAP_NUM_MATERIAL_LEGADO in nVARCHAR2,
            'IKILOS in Number,

            'ICAJAS  in number,
            'IUNIDAD_CAJA in number,
            'IPUC in number


            Dim TotalRegistros As Int32 = InfoCache.NuevosEmbarquesDS.EMBARQUESDETALLES.Rows.Count
            Dim arrayEMBARQUE_CLAVE(TotalRegistros) As String
            Dim arrayPRO_CLAVE(TotalRegistros) As String
            Dim arrayPRO_GRAMAJ(TotalRegistros) As Double
            Dim arrayPRO_DESCRI(TotalRegistros) As String
            Dim arrayDMP_CANTID(TotalRegistros) As Decimal
            Dim arrayDMP_PRECIO(TotalRegistros) As Double
            Dim arraySAP_NUM_MATERIAL_LEGADO(TotalRegistros) As String
            Dim arrayKILOS(TotalRegistros) As Decimal

            With InfoCache.NuevosEmbarquesDS.EMBARQUESDETALLES
                Dim i As Integer = 0
                For Each tableRow As EmbarquesData.EMBARQUESDETALLESRow In .Rows
                    arrayEMBARQUE_CLAVE(i) = tableRow.EMBARQUE_CLAVE
                    arrayPRO_CLAVE(i) = tableRow.PRO_CLAVE
                    arrayPRO_GRAMAJ(i) = tableRow.PRO_GRAMAJ
                    arrayPRO_DESCRI(i) = tableRow.PRO_DESCRI
                    arrayDMP_CANTID(i) = tableRow.DMP_CANTID
                    arrayDMP_PRECIO(i) = tableRow.DMP_PRECIO
                    arraySAP_NUM_MATERIAL_LEGADO(i) = tableRow.SAP_NUM_MATERIAL_LEGADO
                    arrayKILOS(i) = tableRow.KILOS
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


            cmd.CommandText = "PRODODS.VSC_EmbarqueDetalle_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True
            cmd.ArrayBindCount = TotalRegistros

            Dim p1 As OracleParameter = New OracleParameter("IEMBARQUE_CLAVE", OracleDbType.NVarchar2)
            Dim p2 As OracleParameter = New OracleParameter("IPRO_CLAVE", OracleDbType.NVarchar2)
            Dim p3 As OracleParameter = New OracleParameter("IPRO_GRAMAJ", OracleDbType.Double)
            Dim p4 As OracleParameter = New OracleParameter("IPRO_DESCRI", OracleDbType.NVarchar2)
            Dim p5 As OracleParameter = New OracleParameter("IDMP_CANTID", OracleDbType.Decimal)
            Dim p6 As OracleParameter = New OracleParameter("IDMP_PRECIO", OracleDbType.Double)
            Dim p7 As OracleParameter = New OracleParameter("ISAP_NUM_MATERIAL_LEGADO", OracleDbType.NVarchar2)
            Dim p8 As OracleParameter = New OracleParameter("IKILOS", OracleDbType.Decimal)


            p1.Value = arrayEMBARQUE_CLAVE 'RowOrigen.EMBARQUE_CLAVE
            p2.Value = arrayPRO_CLAVE 'RowOrigen.PRO_CLAVE
            p3.Value = arrayPRO_GRAMAJ 'RowOrigen.PRO_GRAMAJ
            p4.Value = arrayPRO_DESCRI 'RowOrigen.PRO_DESCRI
            p5.Value = arrayDMP_CANTID 'RowOrigen.DMP_CANTID
            p6.Value = arrayDMP_PRECIO 'RowOrigen.DMP_PRECIO
            p7.Value = arraySAP_NUM_MATERIAL_LEGADO 'RowOrigen.SAP_NUM_MATERIAL_LEGADO
            p8.Value = arrayKILOS

            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)
            cmd.Parameters.Add(p4)
            cmd.Parameters.Add(p5)
            cmd.Parameters.Add(p6)
            cmd.Parameters.Add(p7)
            cmd.Parameters.Add(p8)

            Dim recordsInesrtados As Integer
            Try
                recordsInesrtados = cmd.ExecuteNonQuery()
            Catch ex As Exception
                InfoCache.UpdateError = ex.Message
            End Try

            cmd.Dispose()
            conn.Close()

            Return recordsInesrtados

        End Function
        Public Function insertarEmbarqueGastos() As Integer
            Dim TotalRegistros As Int32 = InfoCache.NuevosEmbarquesDS.EMBARQUESGASTOS.Rows.Count
            Dim arrayEMBARQUE_CLAVE(TotalRegistros) As String
            Dim arrayG_CLAVE(TotalRegistros) As String
            Dim arrayMONTO(TotalRegistros) As Double


            With InfoCache.NuevosEmbarquesDS.EMBARQUESGASTOS
                Dim i As Integer = 0
                For Each tableRow As EmbarquesData.EMBARQUESGASTOSRow In .Rows
                    arrayEMBARQUE_CLAVE(i) = tableRow.EMBARQUE_CLAVE
                    arrayG_CLAVE(i) = tableRow.G_CLAVE
                    arrayMONTO(i) = tableRow.MONTO

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


            cmd.CommandText = "PRODODS.VSC_EMBARQUESGASTOS_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True
            cmd.ArrayBindCount = TotalRegistros

            Dim p1 As OracleParameter = New OracleParameter("IEMBARQUE_CLAVE", OracleDbType.NVarchar2)
            Dim p2 As OracleParameter = New OracleParameter("IG_CLAVE", OracleDbType.NVarchar2)
            Dim p3 As OracleParameter = New OracleParameter("IMONTO", OracleDbType.Double)



            p1.Value = arrayEMBARQUE_CLAVE 'RowOrigen.EMBARQUE_CLAVE
            p2.Value = arrayG_CLAVE 'RowOrigen.G_CLAVE
            p3.Value = arrayMONTO 'RowOrigen.MONTO



            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)


            Dim recordsInesrtados As Integer
            Try
                recordsInesrtados = cmd.ExecuteNonQuery()
            Catch ex As Exception
                InfoCache.UpdateError = ex.Message
            End Try

            cmd.Dispose()
            conn.Close()

            Return recordsInesrtados
        End Function
        Public Function insertarNuevosCostos(ByVal CostosNuevos As InterfaseCostosData) As Integer
            'SAP_PERIOD,
            'SAP_FISCAL_YEAR,
            'SAP_ACTUAL_COST,
            'SAP_PREVIOUS_COST,
            'SAP_COST_DIFFERENCES,
            'SAP_ID_MATERIAL,
            'SAP_TEXTO_MATERIAL,
            'SAP_UOM_BASE,
            'SAP_CURRENCY,
            'SAP_ID_COMPANIA,
            'SAP_PLANTA,
            'TIPO_DE_PRODUCTO,
            'COSTO_FLETE_POR_UNIDAD,
            'COSTO_ADUANAL_POR_UNIDAD,
            'TOTAL_COSTO_PRODUCT0,
            'SAP_NUM_MATERIAL_LEGADO,
            'SAP_ID_SISTEMA_LEGADO

            Dim TotalRegistros As Int32 = CostosNuevos.SAP_INTERFASE_COSTOS.Rows.Count

            Dim arraySAP_PERIOD(TotalRegistros) As String
            Dim arraySAP_FISCAL_YEAR(TotalRegistros) As Integer
            Dim arraySAP_ACTUAL_COST(TotalRegistros) As Decimal
            Dim arraySAP_PREVIOUS_COST(TotalRegistros) As Decimal
            Dim arraySAP_COST_DIFFERENCES(TotalRegistros) As Decimal
            Dim arraySAP_ID_MATERIAL(TotalRegistros) As String
            Dim arraySAP_TEXTO_MATERIAL(TotalRegistros) As String
            Dim arraySAP_UOM_BASE(TotalRegistros) As String
            Dim arraySAP_CURRENCY(TotalRegistros) As String
            Dim arraySAP_ID_COMPANIA(TotalRegistros) As String
            Dim arraySAP_PLANTA(TotalRegistros) As String
            Dim arrayTIPO_DE_PRODUCTO(TotalRegistros) As String
            Dim arrayCOSTO_FLETE_POR_UNIDAD(TotalRegistros) As Decimal
            Dim arrayCOSTO_ADUANAL_POR_UNIDAD(TotalRegistros) As Decimal
            Dim arrayTOTAL_COSTO_PRODUCT0(TotalRegistros) As Decimal
            Dim arraySAP_NUM_MATERIAL_LEGADO(TotalRegistros) As String
            Dim arraySAP_ID_SISTEMA_LEGADO(TotalRegistros) As String

            With CostosNuevos.SAP_INTERFASE_COSTOS
                Dim i As Integer = 0
                For Each tableRow As InterfaseCostosData.SAP_INTERFASE_COSTOSRow In .Rows
                    arraySAP_PERIOD(i) = tableRow.SAP_PERIOD
                    arraySAP_FISCAL_YEAR(i) = tableRow.SAP_FISCAL_YEAR
                    arraySAP_ACTUAL_COST(i) = tableRow.SAP_ACTUAL_COST
                    arraySAP_PREVIOUS_COST(i) = tableRow.SAP_PREVIOUS_COST
                    arraySAP_COST_DIFFERENCES(i) = tableRow.SAP_COST_DIFFERENCES
                    arraySAP_ID_MATERIAL(i) = tableRow.SAP_ID_MATERIAL
                    arraySAP_TEXTO_MATERIAL(i) = tableRow.SAP_TEXTO_MATERIAL
                    arraySAP_UOM_BASE(i) = tableRow.SAP_UOM_BASE
                    arraySAP_CURRENCY(i) = tableRow.SAP_CURRENCY
                    arraySAP_ID_COMPANIA(i) = tableRow.SAP_ID_COMPANIA
                    arraySAP_PLANTA(i) = tableRow.SAP_PLANTA
                    arrayTIPO_DE_PRODUCTO(i) = tableRow.TIPO_DE_PRODUCTO
                    arrayCOSTO_FLETE_POR_UNIDAD(i) = tableRow.COSTO_FLETE_POR_UNIDAD
                    arrayCOSTO_ADUANAL_POR_UNIDAD(i) = tableRow.COSTO_ADUANAL_POR_UNIDAD
                    arrayTOTAL_COSTO_PRODUCT0(i) = tableRow.TOTAL_COSTO_PRODUCT0
                    arraySAP_NUM_MATERIAL_LEGADO(i) = tableRow.SAP_NUM_MATERIAL_LEGADO
                    arraySAP_ID_SISTEMA_LEGADO(i) = tableRow.SAP_ID_SISTEMA_LEGADO
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


            cmd.CommandText = "PRODODS.VSC_SAPINTERFASECOSTOS_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True
            cmd.ArrayBindCount = TotalRegistros

            Dim p1 As OracleParameter = New OracleParameter("ISAP_PERIOD", OracleDbType.NVarchar2)
            Dim p2 As OracleParameter = New OracleParameter("ISAP_FISCAL_YEAR", OracleDbType.Int32)
            Dim p3 As OracleParameter = New OracleParameter("ISAP_ACTUAL_COST", OracleDbType.Decimal)
            Dim p4 As OracleParameter = New OracleParameter("ISAP_PREVIOUS_COST", OracleDbType.Decimal)
            Dim p5 As OracleParameter = New OracleParameter("ISAP_COST_DIFFERENCES", OracleDbType.Decimal)
            Dim p6 As OracleParameter = New OracleParameter("ISAP_ID_MATERIAL", OracleDbType.NVarchar2)
            Dim p7 As OracleParameter = New OracleParameter("ISAP_TEXTO_MATERIAL", OracleDbType.NVarchar2)
            Dim p8 As OracleParameter = New OracleParameter("ISAP_UOM_BASE", OracleDbType.NVarchar2)
            Dim p9 As OracleParameter = New OracleParameter("ISAP_CURRENCY", OracleDbType.NVarchar2)
            Dim p10 As OracleParameter = New OracleParameter("ISAP_ID_COMPANIA", OracleDbType.NVarchar2)
            Dim p11 As OracleParameter = New OracleParameter("ISAP_PLANTA", OracleDbType.NVarchar2)
            Dim p12 As OracleParameter = New OracleParameter("ITIPO_DE_PRODUCTO", OracleDbType.NVarchar2)
            Dim p13 As OracleParameter = New OracleParameter("ICOSTO_FLETE_POR_UNIDAD", OracleDbType.Decimal)
            Dim p14 As OracleParameter = New OracleParameter("ICOSTO_ADUANAL_POR_UNIDAD", OracleDbType.Decimal)
            Dim p15 As OracleParameter = New OracleParameter("ITOTAL_COSTO_PRODUCT0", OracleDbType.Decimal)
            Dim p16 As OracleParameter = New OracleParameter("ISAP_NUM_MATERIAL_LEGADO", OracleDbType.NVarchar2)
            Dim p17 As OracleParameter = New OracleParameter("ISAP_ID_SISTEMA_LEGADO", OracleDbType.NVarchar2)



            p1.Value = arraySAP_PERIOD 'RowOrigen.SAP_PERIOD
            p2.Value = arraySAP_FISCAL_YEAR 'RowOrigen.SAP_FISCAL_YEAR
            p3.Value = arraySAP_ACTUAL_COST 'RowOrigen.SAP_ACTUAL_COST
            p4.Value = arraySAP_PREVIOUS_COST 'RowOrigen.SAP_PREVIOUS_COST
            p5.Value = arraySAP_COST_DIFFERENCES 'RowOrigen.SAP_COST_DIFFERENCES
            p6.Value = arraySAP_ID_MATERIAL 'RowOrigen.SAP_ID_MATERIAL
            p7.Value = arraySAP_TEXTO_MATERIAL 'RowOrigen.SAP_PERIOD
            p8.Value = arraySAP_UOM_BASE 'RowOrigen.SAP_UOM_BASE
            p9.Value = arraySAP_CURRENCY 'RowOrigen.SAP_CURRENCY
            p10.Value = arraySAP_ID_COMPANIA
            p11.Value = arraySAP_PLANTA
            p12.Value = arrayTIPO_DE_PRODUCTO
            p13.Value = arrayCOSTO_FLETE_POR_UNIDAD
            p14.Value = arrayCOSTO_ADUANAL_POR_UNIDAD
            p15.Value = arrayTOTAL_COSTO_PRODUCT0
            p16.Value = arraySAP_NUM_MATERIAL_LEGADO
            p17.Value = arraySAP_ID_SISTEMA_LEGADO

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


            Dim recordsInesrtados As Integer
            Try
                recordsInesrtados = cmd.ExecuteNonQuery()
            Catch ex As Exception
                InfoCache.UpdateError = ex.Message
            End Try

            cmd.Dispose()
            conn.Close()

            Return recordsInesrtados
        End Function
#End Region
#Region "UPDATE"
        'Sub UpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateButton.Click
        '    Dim conn As New OracleConnection()
        '    ' To fill Data Set from datasource
        '    Dim productsAdapter As New OracleDataAdapter()
        '    'Command object for fetching data
        '    Dim productsCmd As New OracleCommand()
        '    ' Command object for updating data
        '    Dim updateProductsCmd As New OracleCommand()
        '    ' In-memory cache of data
        '    Dim productsDataset As New DataSet("ProductsDataset")


        '    Try
        '        'Dim UDevelopmentDataGridItem As DataGridItem
        '        'Dim Cbx As CheckBox = New CheckBox()
        '        Dim myStr As String
        '        Dim i As Int32 = 0
        '        Dim prodid As String
        '        Dim rowsWanted As DataRow()
        '        '  Loop through each row in UDevelopmentDataGrid
        '        'For Each UDevelopmentDataGridItem In UDevelopmentDataGrid.Items
        '        '    ' Get checkbox control instance
        '        '    Cbx = CType(UDevelopmentDataGridItem.Cells(1).FindControl("cbx"), CheckBox)
        '        '    ' If checkbox is selected
        '        '    If Cbx.Checked = True Then
        '        '        i += 1
        '        '        ' Get value of the first column "Product ID"
        '        '        myStr = UDevelopmentDataGridItem.Cells(0).Text

        '        '        ' Get the Data Row for selected product

        '        rowsWanted = productsDataset.Tables("Products1").Select("Product_id = " + myStr + " ")

        '        '        ' Modify Product Status to orderable

        '        '        rowsWanted(0)("product_status") = "orderable"
        '        '    End If
        '        'Next


        '        ' Call 'updateStatus' stored procedure of 'ODPNet' database package
        '        ' for actual updation
        '        updateProductsCmd.CommandText = "ODPNet.updateStatus"

        '        ' Use existing database connection 
        '        updateProductsCmd.Connection = conn

        '        ' Set command type to stored procedure

        '        updateProductsCmd.CommandType = CommandType.StoredProcedure
        '        If i >= 1 Then
        '            ' Parameter for binding Product ID 
        '            Dim productIDParam As OracleParameter = New OracleParameter("productID", OracleDbType.Int32)
        '            productIDParam.Direction = ParameterDirection.Input

        '            ' Set Data Row version to orignal to use the existing product ids value
        '            productIDParam.SourceVersion = DataRowVersion.Original
        '            productIDParam.SourceColumn = "product_id"
        '            updateProductsCmd.Parameters.Add(productIDParam)

        '            ' Parameter for binding Product Status
        '            Dim productStatusParam As OracleParameter = New OracleParameter("productStatus", OracleDbType.Varchar2, 32)
        '            productStatusParam.Direction = ParameterDirection.Input
        '            productStatusParam.SourceColumn = "product_status"
        '            ' Set Data Row version to current to use the changed Product Status
        '            productStatusParam.SourceVersion = DataRowVersion.Current
        '            updateProductsCmd.Parameters.Add(productStatusParam)

        '            ' Setup the update command on adapter
        '            productsAdapter.UpdateCommand = updateProductsCmd

        '            ' Update the changes made to productsDataset
        '            productsAdapter.Update(productsDataset, "Products1")
        '            productsDataset.Clear()

        '            ' Refresh the Data Grids
        '            'PopulateProducts()
        '        Else

        '            ' If none of the checkbox(s) is selected, display message
        '            'Response.Write("<b>Atleast one product should be selected before changing product status!</b>")
        '        End If



        '        ' Disable Update Button is no products with Under Development exists. 

        '        'If (UDevelopmentDataGrid.Items.Count = 0) Then

        '        '    UpdateButton.Enabled = False

        '        'Else

        '        '    UpdateButton.Enabled = True

        '        'End If

        '    Catch ex As Exception

        '        ' If error occurs redirect to an error page

        '        'Response.Redirect("Error.aspx?error=" + ex.Message + ex.StackTrace)



        '    End Try

        'End Sub



        'End Function
        Public Function updateDatosEmbarques() As Integer
            Dim TotalRegistros As Integer = updateEmbarques()
            Dim TotalRegistros1 As Integer = updateEmbarqueDetalles()
            Dim TotalRegistros2 As Integer = updateEmbarqueGastos()
            EmbarquesSelectData()

            Return 1
        End Function
        Private Function updateEmbarques() As Integer
            Dim TotalRegistros As Int32 = InfoCache.EmbarquesDS.EMBARQUES.Rows.Count


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


            cmd.CommandText = "PRODODS.CostosPack.VSC_Embarque_Update"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True
            'cmd.ArrayBindCount = TotalRegistros + 1

            Dim p1 As OracleParameter = New OracleParameter("InEMBARQUE_CLAVE", OracleDbType.NVarchar2, 35)
            p1.Direction = ParameterDirection.Input
            p1.SourceVersion = DataRowVersion.Original
            p1.SourceColumn = "EMBARQUE_CLAVE"

            Dim p2 As OracleParameter = New OracleParameter("InSTATUS", OracleDbType.NVarchar2)
            p2.Direction = ParameterDirection.Input
            p2.SourceVersion = DataRowVersion.Current
            p2.SourceColumn = "STATUS"

            Dim p3 As OracleParameter = New OracleParameter("InTIPO_CAMBIO", OracleDbType.Decimal)
            p3.Direction = ParameterDirection.Input
            p3.SourceVersion = DataRowVersion.Current
            p3.SourceColumn = "TIPO_CAMBIO"

            Dim p4 As OracleParameter = New OracleParameter("InFACTURA_NO", OracleDbType.NVarchar2)
            p4.Direction = ParameterDirection.Input
            p4.SourceVersion = DataRowVersion.Current
            p4.SourceColumn = "FACTURA_NO"

            Dim p5 As OracleParameter = New OracleParameter("InFECHA_FACTURA", OracleDbType.Date)
            p5.Direction = ParameterDirection.Input
            p5.SourceVersion = DataRowVersion.Current
            p5.SourceColumn = "FECHA_FACTURA"

            Dim p6 As OracleParameter = New OracleParameter("InCONTROL_NO", OracleDbType.NVarchar2)
            p6.Direction = ParameterDirection.Input
            p6.SourceVersion = DataRowVersion.Current
            p6.SourceColumn = "CONTROL_NO"

            Dim p7 As OracleParameter = New OracleParameter("InFECHA_INGRESO", OracleDbType.Date)
            p7.Direction = ParameterDirection.Input
            p7.SourceVersion = DataRowVersion.Current
            p7.SourceColumn = "FECHA_INGRESO"

            Dim p8 As OracleParameter = New OracleParameter("InPROVEEDOR_NOMBRE", OracleDbType.NVarchar2)
            p8.Direction = ParameterDirection.Input
            p8.SourceVersion = DataRowVersion.Current
            p8.SourceColumn = "PROVEEDOR_NOMBRE"



            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)
            cmd.Parameters.Add(p4)
            cmd.Parameters.Add(p5)
            cmd.Parameters.Add(p6)
            cmd.Parameters.Add(p7)
            cmd.Parameters.Add(p8)

            MaestroAdapter.UpdateCommand = cmd
            MaestroAdapter.Update(InfoCache.EmbarquesDS, "EMBARQUES")

            cmd.Dispose()
            conn.Close()
            Return TotalRegistros

        End Function
        Private Function updateEmbarqueDetalles() As Integer
            Dim TotalRegistros As Int32 = InfoCache.EmbarquesDS.EMBARQUESDETALLES.Rows.Count


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


            cmd.CommandText = "PRODODS.CostosPack.VSC_EmbarqueDetalle_Update"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True
            'cmd.ArrayBindCount = TotalRegistros + 1

            Dim p1 As OracleParameter = New OracleParameter("InEMBARQUE_CLAVE", OracleDbType.NVarchar2, 35)
            p1.Direction = ParameterDirection.Input
            p1.SourceVersion = DataRowVersion.Original
            p1.SourceColumn = "EMBARQUE_CLAVE"

            Dim p2 As OracleParameter = New OracleParameter("InPRO_CLAVE", OracleDbType.Varchar2)
            p2.Direction = ParameterDirection.Input
            p2.SourceVersion = DataRowVersion.Original
            p2.SourceColumn = "PRO_CLAVE"

            Dim p3 As OracleParameter = New OracleParameter("InPRO_GRAMAJ", OracleDbType.Decimal)
            p3.Direction = ParameterDirection.Input
            p3.SourceVersion = DataRowVersion.Current
            p3.SourceColumn = "PRO_GRAMAJ"

            Dim p4 As OracleParameter = New OracleParameter("InDMP_PRECIO", OracleDbType.Decimal)
            p4.Direction = ParameterDirection.Input
            p4.SourceVersion = DataRowVersion.Current
            p4.SourceColumn = "DMP_PRECIO"

            Dim p5 As OracleParameter = New OracleParameter("InPFOB", OracleDbType.Decimal)
            p5.Direction = ParameterDirection.Input
            p5.SourceVersion = DataRowVersion.Current
            p5.SourceColumn = "PFOB"

            Dim p6 As OracleParameter = New OracleParameter("InAVG_FLETE", OracleDbType.Decimal)
            p6.Direction = ParameterDirection.Input
            p6.SourceVersion = DataRowVersion.Current
            p6.SourceColumn = "AVG_FLETE"

            Dim p7 As OracleParameter = New OracleParameter("InAVG_SEGUROS", OracleDbType.Decimal)
            p7.Direction = ParameterDirection.Input
            p7.SourceVersion = DataRowVersion.Current
            p7.SourceColumn = "AVG_SEGUROS"

            Dim p8 As OracleParameter = New OracleParameter("InAVG_OG", OracleDbType.Decimal)
            p8.Direction = ParameterDirection.Input
            p8.SourceVersion = DataRowVersion.Current
            p8.SourceColumn = "AVG_OG"

            Dim p9 As OracleParameter = New OracleParameter("InKILOS ", OracleDbType.Decimal)
            p9.Direction = ParameterDirection.Input
            p9.SourceVersion = DataRowVersion.Current
            p9.SourceColumn = "KILOS"

            Dim p10 As OracleParameter = New OracleParameter("InML_FPU", OracleDbType.Decimal)
            p10.Direction = ParameterDirection.Input
            p10.SourceVersion = DataRowVersion.Current
            p10.SourceColumn = "ML_FPU"

            Dim p11 As OracleParameter = New OracleParameter("InML_CIF", OracleDbType.Decimal)
            p11.Direction = ParameterDirection.Input
            p11.SourceVersion = DataRowVersion.Current
            p11.SourceColumn = "ML_CIF"

            Dim p12 As OracleParameter = New OracleParameter("InML_ARANCEL", OracleDbType.Decimal)
            p12.Direction = ParameterDirection.Input
            p12.SourceVersion = DataRowVersion.Current
            p12.SourceColumn = "ML_ARANCEL"

            Dim p13 As OracleParameter = New OracleParameter("InML_FLETE", OracleDbType.Decimal)
            p13.Direction = ParameterDirection.Input
            p13.SourceVersion = DataRowVersion.Current
            p13.SourceColumn = "ML_FLETE"

            Dim p14 As OracleParameter = New OracleParameter("InML_HONORARIOS", OracleDbType.Decimal)
            p14.Direction = ParameterDirection.Input
            p14.SourceVersion = DataRowVersion.Current
            p14.SourceColumn = "ML_HONORARIOS"

            Dim p15 As OracleParameter = New OracleParameter("InML_VARIOS", OracleDbType.Decimal)
            p15.Direction = ParameterDirection.Input
            p15.SourceVersion = DataRowVersion.Current
            p15.SourceColumn = "ML_VARIOS"

            Dim p16 As OracleParameter = New OracleParameter("InML_TOTAL", OracleDbType.Decimal)
            p16.Direction = ParameterDirection.Input
            p16.SourceVersion = DataRowVersion.Current
            p16.SourceColumn = "ML_TOTAL"

            Dim p17 As OracleParameter = New OracleParameter("InML_PU", OracleDbType.Decimal)
            p17.Direction = ParameterDirection.Input
            p17.SourceVersion = DataRowVersion.Current
            p17.SourceColumn = "ML_PU"

            Dim p18 As OracleParameter = New OracleParameter("InML_CU", OracleDbType.Decimal)
            p18.Direction = ParameterDirection.Input
            p18.SourceVersion = DataRowVersion.Current
            p18.SourceColumn = "ML_CU"


            Dim p19 As OracleParameter = New OracleParameter("InCU", OracleDbType.Decimal)
            p19.Direction = ParameterDirection.Input
            p19.SourceVersion = DataRowVersion.Current
            p19.SourceColumn = "CU"




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

            MaestroAdapter.UpdateCommand = cmd
            Try
                MaestroAdapter.Update(InfoCache.EmbarquesDS, "EMBARQUESDETALLES")
            Catch ex As Exception
                InfoCache.UpdateError = ex.Message
            End Try


            cmd.Dispose()
            conn.Close()
            Return TotalRegistros

        End Function
        Private Function updateEmbarqueGastos() As Integer
            Dim TotalRegistros As Int32 = InfoCache.EmbarquesDS.EMBARQUESGASTOS.Rows.Count


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


            cmd.CommandText = "PRODODS.CostosPack.VSC_EMBARQUESGASTOS_UPDATE"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.BindByName = True
            'cmd.ArrayBindCount = TotalRegistros + 1

            Dim p1 As OracleParameter = New OracleParameter("InEMBARQUE_CLAVE", OracleDbType.NVarchar2, 35)
            p1.Direction = ParameterDirection.Input
            p1.SourceVersion = DataRowVersion.Original
            p1.SourceColumn = "EMBARQUE_CLAVE"

            Dim p2 As OracleParameter = New OracleParameter("InG_CLAVE", OracleDbType.Int32)
            p2.Direction = ParameterDirection.Input
            p2.SourceVersion = DataRowVersion.Original
            p2.SourceColumn = "G_CLAVE"

            Dim p3 As OracleParameter = New OracleParameter("InMONTO", OracleDbType.Decimal)
            p3.Direction = ParameterDirection.Input
            p3.SourceVersion = DataRowVersion.Current
            p3.SourceColumn = "MONTO"

            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)

            MaestroAdapter.UpdateCommand = cmd
            Try
                MaestroAdapter.Update(InfoCache.EmbarquesDS, "EMBARQUESGASTOS")
            Catch ex As Exception
                InfoCache.UpdateError = ex.Message
            End Try


            cmd.Dispose()
            conn.Close()
            Return TotalRegistros

        End Function
        Public Function updateEmbarquePeriodoActual() As Integer
            Dim TotalRegistros As Int32 = InfoCache.EmbarquesDS.PERIODOACTUAL.Rows.Count

            InfoCache.EmbarquesDS.PERIODOACTUAL(0).FECHA_CIERRE = Date.Today

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


            cmd.CommandText = "PRODODS.CostosPack.VSC_PERIODOACTUAL_UPDATE"
            cmd.CommandType = CommandType.StoredProcedure

            Dim p1 As OracleParameter = New OracleParameter("InPAIS_CLAVE", OracleDbType.Int32)
            p1.Direction = ParameterDirection.Input
            p1.SourceVersion = DataRowVersion.Original
            p1.SourceColumn = "PAIS_CLAVE"

            Dim p2 As OracleParameter = New OracleParameter("InPERIODO_ANTERIOR", OracleDbType.NVarchar2)
            p2.Direction = ParameterDirection.Input
            p2.SourceVersion = DataRowVersion.Current
            p2.SourceColumn = "PERIODO_ANTERIOR"

            Dim p3 As OracleParameter = New OracleParameter("InFISCALYEAR_ANTERIOR", OracleDbType.Int32)
            p3.Direction = ParameterDirection.Input
            p3.SourceVersion = DataRowVersion.Current
            p3.SourceColumn = "FISCALYEAR_ANTERIOR"

            Dim p4 As OracleParameter = New OracleParameter("InFECHA_CIERRE", OracleDbType.Date)
            p4.Direction = ParameterDirection.Input
            p4.SourceVersion = DataRowVersion.Current
            p4.SourceColumn = "FECHA_CIERRE"

            Dim p5 As OracleParameter = New OracleParameter("InPERIODO", OracleDbType.NVarchar2)
            p5.Direction = ParameterDirection.Input
            p5.SourceVersion = DataRowVersion.Current
            p5.SourceColumn = "PERIODO"

            Dim p6 As OracleParameter = New OracleParameter("InFISCAL_YEAR", OracleDbType.Int32)
            p6.Direction = ParameterDirection.Input
            p6.SourceVersion = DataRowVersion.Current
            p6.SourceColumn = "FISCAL_YEAR"

           


            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p3)
            cmd.Parameters.Add(p4)
            cmd.Parameters.Add(p5)
            cmd.Parameters.Add(p6)

            MaestroAdapter.UpdateCommand = cmd
            Try
                MaestroAdapter.Update(InfoCache.EmbarquesDS, "PERIODOACTUAL")
            Catch ex As Exception
                InfoCache.UpdateError = ex.Message
            End Try


            cmd.Dispose()
            conn.Close()
            Return TotalRegistros

        End Function
#End Region
#Region "Copiar costos mes anterior"
        Public Function CopiarCostos( _
                                    PeriodoOrigen As String, _
                                    YearOrigen As Integer, _
                                    PeriodoDestino As String, _
                                    YearDestino As Integer) As Integer

            Dim SqlText As New StringBuilder

            SqlText.Append("INSERT INTO SAP_INTERFASE_COSTOS (").Append(" ")
            SqlText.Append("SAP_PERIOD,").Append(" ")
            SqlText.Append("SAP_FISCAL_YEAR,").Append(" ")
            SqlText.Append("SAP_ACTUAL_COST,").Append(" ")
            SqlText.Append("SAP_PREVIOUS_COST,").Append(" ")
            SqlText.Append("SAP_COST_DIFFERENCES,").Append(" ")
            SqlText.Append("SAP_ID_MATERIAL,").Append(" ")
            SqlText.Append("SAP_TEXTO_MATERIAL,").Append(" ")
            SqlText.Append("SAP_UOM_BASE,").Append(" ")
            SqlText.Append("SAP_CURRENCY,").Append(" ")
            SqlText.Append("SAP_ID_COMPANIA,").Append(" ")
            SqlText.Append("SAP_PLANTA,").Append(" ")
            SqlText.Append("TIPO_DE_PRODUCTO,").Append(" ")
            SqlText.Append("COSTO_FLETE_POR_UNIDAD,").Append(" ")
            SqlText.Append("COSTO_ADUANAL_POR_UNIDAD,").Append(" ")
            SqlText.Append("TOTAL_COSTO_PRODUCT0,").Append(" ")
            SqlText.Append("SAP_NUM_MATERIAL_LEGADO,").Append(" ")
            SqlText.Append("SAP_ID_SISTEMA_LEGADO )").Append(" ")
            SqlText.Append("SELECT").Append(" ")
            SqlText.Append("'").Append(PeriodoDestino).Append("',").Append(" ")
            SqlText.Append("'").Append(YearDestino.ToString).Append("',").Append(" ")
            SqlText.Append("SAP_ACTUAL_COST,").Append(" ")
            SqlText.Append("SAP_PREVIOUS_COST, ").Append(" ")
            SqlText.Append("SAP_COST_DIFFERENCES,").Append(" ")
            SqlText.Append("SAP_ID_MATERIAL,").Append(" ")
            SqlText.Append("SAP_TEXTO_MATERIAL,").Append(" ")
            SqlText.Append("SAP_UOM_BASE,").Append(" ")
            SqlText.Append("SAP_CURRENCY,").Append(" ")
            SqlText.Append("SAP_ID_COMPANIA,").Append(" ")
            SqlText.Append("SAP_PLANTA,").Append(" ")
            SqlText.Append("TIPO_DE_PRODUCTO,").Append(" ")
            SqlText.Append("COSTO_FLETE_POR_UNIDAD,").Append(" ")
            SqlText.Append("COSTO_ADUANAL_POR_UNIDAD,").Append(" ")
            SqlText.Append("TOTAL_COSTO_PRODUCT0,").Append(" ")
            SqlText.Append("SAP_NUM_MATERIAL_LEGADO,").Append(" ")
            SqlText.Append("'ODSCA'").Append(" ")
            SqlText.Append("FROM  SAP_INTERFASE_COSTOS").Append(" ")
            SqlText.Append("WHERE").Append(" ")
            SqlText.Append("(SAP_ID_COMPANIA='").Append(InfoCache.ClaveCompania).Append(")'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("SAP_PERIOD='").Append(PeriodoOrigen).Append("'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("sap_fiscal_year='").Append(YearOrigen.ToString).Append("'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("TIPO_DE_PRODUCTO='PI'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("SAP_ID_SISTEMA_LEGADO='ODSCA'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("TRIM(SAP_NUM_MATERIAL_LEGADO) NOT").Append(" ")
            SqlText.Append("IN ( ").Append(" ")
            SqlText.Append("SELECT trim(SAP_NUM_MATERIAL_LEGADO)").Append(" ")
            SqlText.Append("from SAP_interfase_costos").Append(" ")
            SqlText.Append("WHERE").Append(" ")
            SqlText.Append("(SAP_ID_COMPANIA='").Append(InfoCache.ClaveCompania).Append(")'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("'").Append(PeriodoDestino).Append("'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("sap_fiscal_year='").Append(YearDestino.ToString).Append("'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("TIPO_DE_PRODUCTO='PL'").Append(" ")
            SqlText.Append("AND").Append(" ")
            SqlText.Append("SAP_ID_SISTEMA_LEGADO='ODSCA') ")



            Dim recordsActualizados As Integer
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim cmd As New OracleCommand()
            cmd.Connection = conn
            cmd.CommandText = SqlText.ToString
            cmd.CommandType = CommandType.Text

            Try
                recordsActualizados = cmd.ExecuteNonQuery()
            Catch e As OracleException
                'RaiseEvent Mensaje(Format(Date.Now, "R") & "> NO SE EJECUTÓ el proceso: " & SPname & ". Solicite información a Soporte de Aplicaciones")
                'RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Message)

                'Dim errorMessage As String = "Code: " & e.Source & vbCrLf & _
                '                             "Message: " & e.Message

                'Dim log As System.Diagnostics.EventLog = New System.Diagnostics.EventLog()
                'log.Source = "My Application"
                'log.WriteEntry(errorMessage)

                InfoCache.UpdateError = e.Message
            End Try

            cmd.Dispose()
            conn.Close()

            Return recordsActualizados
        End Function
        'Public Function ActualizarCostosNulosyCeros() As Integer
        '    Dim SqlText As New StringBuilder
        '    SqlText.Append("UPDATE(SAP_INTERFASE_COSTOS)").Append(" ")
        '    SqlText.Append(" TOTAL_COSTO_PRODUCT0 = SAP_INTERFASE_COSTOS.SAP_ACTUAL_COST").Append(" ")
        '    SqlText.Append(" TOTAL_COSTO_PRODUCT0 = SAP_INTERFASE_COSTOS.SAP_ACTUAL_COST").Append(" ")


        '    '    '            
        '    '    '           
        '    '    'WHERE        (SAP_INTERFASE_COSTOS.TOTAL_COSTO_PRODUCT0 IS NULL) AND (NOT (SAP_INTERFASE_COSTOS.SAP_ACTUAL_COST IS NULL))
        'End Function
#End Region
    End Class

End Namespace

