Imports Bis.Sap.Common


Imports System.Data.Common
Imports System.Text
Namespace Bis.Sap.DataAccess
    Public Class CostosVentasDML
        Public Function SelectData() As GTPolizasCostosData

            Dim FormatoDesde As String = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
            Dim FormatoHasta As String = Format(InfoCache.FechaHasta, "yyyy/MM/dd")

            Dim dsCostos As New GTPolizasCostosData
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
                p1.Value = 2
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
                    adMovimientosDeProductos.Fill(dsCostos, "ODS_MOVIMPRODUCTOS")
                Catch ex As Exception

                End Try

                .Dispose()
            End With

            Dim cmdCatalogoMovimientoProductos As New OracleCommand
            Dim adCatalogoMovimientoProductos As OracleDataAdapter = New OracleDataAdapter(cmdCatalogoMovimientoProductos)
            With cmdCatalogoMovimientoProductos
                .Connection = conn
                .CommandText = "PRODODS.VSGT_POLIZASCOSTOS_Select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p1)
                Try
                    adCatalogoMovimientoProductos.Fill(dsCostos, "POLIZASCOSTOS")
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
                    adSucursalesPais.Fill(dsCostos, "ODS_SUCURSALES")
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
                    adInterfaseCostos.Fill(dsCostos, "SAP_INTERFASE_COSTOS")
                Catch ex As Exception

                End Try

                .Dispose()
            End With

            conn.Close()
            Return dsCostos
        End Function
    End Class
End Namespace

