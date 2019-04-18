Imports Bis.Sap.Common


Imports System.Data.Common
Imports System.Text

Namespace Bis.Sap.DataAccess
    Public Class IngresosVentasDML
        Public Function SelectData() As GTPolizasIngresosData

            Dim dsIngresos As New GTPolizasIngresosData
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()



            Dim cmdPolizasIngresos As New OracleCommand
            Dim adPolizasIngresos As OracleDataAdapter = New OracleDataAdapter(cmdPolizasIngresos)
            With cmdPolizasIngresos
                .Connection = conn
                .CommandText = "PRODODS.VSGT_POLIZASINGRESOS_Select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p1)
                Try
                    adPolizasIngresos.Fill(dsIngresos, "POLIZASINGRESOS")
                Catch ex As Exception
                    InfoCache.UpdateError = ex.Message

                End Try

                .Dispose()
            End With


            Dim fechaDesdeFAE As String = Format(InfoCache.FechaDesde, "yyyyMMdd")
            Dim fechaHastaFAE As String = Format(InfoCache.FechaHasta, "yyyyMMdd")

            Dim cmdVentasFae As New OracleCommand
            Dim adVentasFae As OracleDataAdapter = New OracleDataAdapter(cmdVentasFae)
            With cmdVentasFae
                .Connection = conn
                .CommandText = "PRODODS.VSGT_VENTASFAE_Select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
                p1.Value = fechaDesdeFAE
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
                p2.Value = fechaHastaFAE
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p3)

                Try
                    adVentasFae.Fill(dsIngresos, "VENTASFAE")
                Catch ex As Exception
                    InfoCache.UpdateError = ex.Message
                End Try

                .Dispose()
            End With

            Dim cmdMovimProductos As New OracleCommand
            Dim adMovimProductos As OracleDataAdapter = New OracleDataAdapter(cmdMovimProductos)
            With cmdMovimProductos
                .Connection = conn
                .CommandText = "PRODODS.VSGT_ODS_MOVIMPRODUCTOS_Select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
                p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
                p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p3)

                Try
                    adMovimProductos.Fill(dsIngresos, "ODS_MOVIMPRODUCTOS")
                Catch ex As Exception
                    InfoCache.UpdateError = ex.Message
                End Try

                .Dispose()
            End With


            Dim cmdLiquidaciones As New OracleCommand
            Dim adLiquidaciones As OracleDataAdapter = New OracleDataAdapter(cmdLiquidaciones)
            With cmdLiquidaciones
                .Connection = conn
                .CommandText = "PRODODS.VSGT_ODS_LIQUIDACIONES_Select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
                p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
                p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p3)

                Try
                    adLiquidaciones.Fill(dsIngresos, "ODS_LIQUIDACIONES")
                Catch ex As Exception
                    InfoCache.UpdateError = ex.Message
                End Try

                .Dispose()
            End With

            Dim cmdBanca As New OracleCommand
            Dim adBanca As OracleDataAdapter = New OracleDataAdapter(cmdBanca)
            With cmdBanca
                .Connection = conn
                .CommandText = "PRODODS.VSGT_ODS_BANCA_Select"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("inFechaD", OracleDbType.NVarchar2)
                p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("inFechaH", OracleDbType.NVarchar2)
                p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p3)

                Try
                    adBanca.Fill(dsIngresos, "ODS_BANCA")
                Catch ex As Exception
                    InfoCache.UpdateError = ex.Message
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
                    adSucursalesPais.Fill(dsIngresos, "SUCURSALES")
                Catch ex As Exception
                    InfoCache.UpdateError = ex.Message
                End Try

                .Dispose()
            End With


            conn.Close()
            Return dsIngresos
        End Function
    End Class
End Namespace



