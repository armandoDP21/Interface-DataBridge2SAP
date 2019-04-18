Imports Bis.Sap.Common
Imports System.Data.Common
Imports System.Text

'Option Strict On
'Option Explicit On


Namespace Bis.Sap.DataAccess

    Public Class MenuSP

        Private _changeStatusCxCSemana As Integer

        Public Event Mensaje(ByVal a As String)

#Region "SelectData"
        Public Function CalendarioSelectData() As CalendarioData
            Dim IsapData As New CalendarioData
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()



            Dim cmdCalendario As New OracleCommand
            Dim adCalendario As OracleDataAdapter = New OracleDataAdapter(cmdCalendario)
            With cmdCalendario
                .Connection = conn
                .CommandText = "PRODODS.VS_CALENDARIO_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.NVarchar2)
                p1.Value = Format(InfoCache.FechaDesde, "yyyy/MM/dd")
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.NVarchar2)
                p2.Value = Format(InfoCache.FechaHasta, "yyyy/MM/dd")
                .Parameters.Add(p2)

                Dim p3 As OracleParameter = New OracleParameter("p3", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p3)

                adCalendario.Fill(IsapData, "CALENDARIO_PAIS")
                .Dispose()
            End With



            conn.Close()
            conn.Dispose()
            Return IsapData
        End Function
        Public Function MenuSelectData(uGPID As String) As RunIsapData

            Dim IsapData As New RunIsapData
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()


          
            Dim cmdDatospais As New OracleCommand
            Dim adDatospais As OracleDataAdapter = New OracleDataAdapter(cmdDatospais)
            With cmdDatospais
                .Connection = conn
                .CommandText = "PRODODS.VSI_DATOSPAIS_SELECT"
                .CommandType = CommandType.StoredProcedure


                Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p1)
                Try
                    adDatospais.Fill(IsapData, "DATOSPAIS")
                Catch ex As OracleException
                    InfoCache.UpdateError = ex.ErrorCode
                End Try
                .Dispose()
            End With

            Dim cmdProcesos As New OracleCommand
            Dim adPaises As OracleDataAdapter = New OracleDataAdapter(cmdProcesos)
            With cmdProcesos
                .Connection = conn
                .CommandText = "PRODODS.VSI_PROCESOS_SELECT"
                .CommandType = CommandType.StoredProcedure


                Dim p1 As OracleParameter = New OracleParameter("InUserID", OracleDbType.NVarchar2)
                p1.Value = uGPID
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)
                Try
                    adPaises.Fill(IsapData, "VSI_PROCESOS")
                Catch ex As OracleException
                    InfoCache.UpdateError = ex.ErrorCode

                End Try

                .Dispose()
            End With

            Dim cmdSucursales As New OracleCommand
            Dim adSucursales As OracleDataAdapter = New OracleDataAdapter(cmdSucursales)
            With cmdSucursales
                .Connection = conn
                .CommandText = "PRODODS.VSI_SUCURSALES_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p1)

                Try
                    adSucursales.Fill(IsapData, "VSI_SUCURSALES")
                Catch ex As OracleException
                    InfoCache.UpdateError = ex.ErrorCode
                End Try

                .Dispose()
            End With


            conn.Close()
            conn.Dispose()
            Return IsapData
        End Function
        Public Function SelectSecuencia() As Long
            Dim returnvalue As Long

            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim cmdUserSecuencia As New OracleCommand
            Dim adUserSecuencia As OracleDataAdapter = New OracleDataAdapter(cmdUserSecuencia)
            With cmdUserSecuencia
                .Connection = conn
                .CommandText = "PRODODS.VSI_USERS_SECUENCIA_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("InUserID", OracleDbType.NVarchar2)
                p1.Value = InfoCache.SAPuser
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)

                Try
                    returnvalue = cmdUserSecuencia.ExecuteScalar()
                    'adUserSecuencia.Fill(IsapData, "USERS_SECUENCIA")
                Catch ex As OracleException
                    InfoCache.UpdateError = ex.ErrorCode
                End Try

                .Dispose()
            End With


            conn.Close()
            conn.Dispose()
            Return returnvalue
        End Function
#End Region
#Region "BorrarPolizasSemana"
        Public Function DeletePolizasCxCSemana() As Integer
            Dim SqlText As New StringBuilder
            SqlText.Append("DELETE FROM PRODODS.SAP_GL_INTERFASE ")
            SqlText.Append("WHERE ")
            SqlText.Append("(ESTATUS_SAP = '").Append(InfoCache.ESTATUS_SAP).Append("') ")
            SqlText.Append("AND ")
            SqlText.Append("(SAP_COMPANY_CODE = '").Append(InfoCache.ClaveCompania).Append("') ")
            SqlText.Append("AND ")
            SqlText.Append("(REF_DOCUMENT_NUMBER LIKE 'B").Append(InfoCache.PaisClave.ToString).Append("%') ")
            SqlText.Append("AND ")
            SqlText.Append("(DOC_HEADER_TEXT LIKE '%").Append(InfoCache.APS).Append("')")


            'RaiseEvent Mensaje(Format(Date.Now, "R") & "> Proceso: " & SPname & " de: " & FechaDesde.ToString & " a:" & FechaHasta.ToString)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> Iniciado a")

            Dim recordsBorrados As Integer


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
                recordsBorrados = cmd.ExecuteNonQuery()
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
            conn.Dispose()
            Return recordsBorrados
        End Function
        Public Function DeletePolizasCostoVentasSemana() As Integer

            'RaiseEvent Mensaje(Format(Date.Now, "R") & "> Proceso: " & SPname & " de: " & FechaDesde.ToString & " a:" & FechaHasta.ToString)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> Iniciado a")
            Dim SqlText As New StringBuilder
            SqlText.Append("DELETE FROM PRODODS.SAP_GL_INTERFASE ")
            SqlText.Append("WHERE ")
            SqlText.Append("(ESTATUS_SAP = '").Append(InfoCache.ESTATUS_SAP).Append("') ")
            SqlText.Append("AND ")
            SqlText.Append("(SAP_COMPANY_CODE = '").Append(InfoCache.ClaveCompania).Append("') ")
            SqlText.Append("AND ")
            SqlText.Append("(REF_DOCUMENT_NUMBER LIKE 'A").Append(InfoCache.PaisClave.ToString).Append("%') ")
            SqlText.Append("AND ")
            SqlText.Append("(DOC_HEADER_TEXT LIKE '%").Append(InfoCache.APS).Append("')")
            Dim recordsBorrados As Integer

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
                recordsBorrados = cmd.ExecuteNonQuery()
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

            Return recordsBorrados
        End Function
#End Region
#Region "GuardarSecuencia"
        Public Function RegistrarSecuencia() As Integer
            Dim SqlText As New StringBuilder

            SqlText.Append("UPDATE").Append(" ")
            SqlText.Append("PRODODS.VSI_USERS_SECUENCIA").Append(" ")
            SqlText.Append("SET").Append(" ")
            SqlText.Append("SECUENCIA =").Append(InfoCache.Secuencia.ToString).Append(" ")
            SqlText.Append("WHERE ").Append(" ")
            SqlText.Append("(VSI_USERS_SECUENCIA.USERID ='").Append(InfoCache.SAPuser).Append("')")

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
#End Region

#Region "Cambio Status"
        Public Function ChangeStatusCxCSemana(ClaveCompania As String, APS As String, ClavePais As Decimal) As Integer
            Dim SqlText As New StringBuilder

            SqlText.Append("UPDATE PRODODS.SAP_GL_INTERFASE ")
            SqlText.Append("SET ")
            SqlText.Append("ESTATUS_SAP = 'NEW' ")
            SqlText.Append("WHERE ")
            SqlText.Append("(SAP_GL_INTERFASE.ESTATUS_SAP = 'REV') ")
            SqlText.Append("AND ")
            SqlText.Append("(SAP_GL_INTERFASE.SAP_COMPANY_CODE ='").Append(ClaveCompania).Append("') ")
            SqlText.Append("AND ")
            SqlText.Append("SAP_GL_INTERFASE.REF_DOCUMENT_NUMBER LIKE 'B" & ClavePais & "%' ")
            SqlText.Append("AND ")
            SqlText.Append("(SAP_GL_INTERFASE.DOC_HEADER_TEXT LIKE '%").Append(APS).Append("') ")



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
        Public Function ChangeStatusCostoVentasSemana(ClaveCompania As String, APS As String, ClavePais As Decimal) As Integer
            Dim SqlText As New StringBuilder

            SqlText.Append("UPDATE PRODODS.SAP_GL_INTERFASE ")
            SqlText.Append("SET ")
            SqlText.Append("ESTATUS_SAP = 'NEW' ")
            SqlText.Append("WHERE ")
            SqlText.Append("(SAP_GL_INTERFASE.ESTATUS_SAP = 'REV') ")
            SqlText.Append("AND ")
            SqlText.Append("(SAP_GL_INTERFASE.SAP_COMPANY_CODE ='").Append(ClaveCompania).Append("') ")
            SqlText.Append("AND ")
            SqlText.Append("SAP_GL_INTERFASE.REF_DOCUMENT_NUMBER LIKE 'A" & ClavePais & "%' ")
            SqlText.Append("AND ")
            SqlText.Append("(SAP_GL_INTERFASE.DOC_HEADER_TEXT LIKE '%").Append(APS).Append("') ")



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
        Public Function ChangeStatusCentralizadaSemana(PaisClave As Decimal) As Integer
            Dim SqlText As New StringBuilder

            SqlText.Append("UPDATE").Append(" ")
            SqlText.Append("PRODODS.SAP_AR_INTERFASE").Append(" ")
            SqlText.Append("SET").Append(" ")
            SqlText.Append("SAP_STATUS = 'N'").Append(" ")
            SqlText.Append("WHERE").Append(" ")
            SqlText.Append("(SAP_STATUS = 'REV')").Append(" ")
            SqlText.Append("AND").Append(" ")

            Select Case PaisClave
                Case 2
                    SqlText.Append("(SOURCE = 'DCTOTAL-GT')").Append(" ")
                Case 4
                    SqlText.Append("(SOURCE = 'DCTOTAL-PA')").Append(" ")
            End Select
            SqlText.Append("AND").Append(" ")

            SqlText.Append("(POSTING_DATE = TO_DATE('" & Format(InfoCache.FechaHasta, "MM/dd/yyyy") & "', 'MM/DD/YYYY'))")


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
#End Region
        'Public Sub ProcesarGuatemala(ByVal Proceso As Integer)
        '    'DTS= Megabodega , clave 5000
        '    'ZACAPA = Nor-Oriente , clave 4500
        '    'CHIMAL = Chimaltenango , clave 5020

        '    Select Case Proceso
        '        Case 0 'Todas los movimientos
        '            Select Case InfoCache.SucursalClave
        '                Case 0 'todos los movimientos de todas las sucursales

        '                    EjecutarSP("dbp_cargaMP_ZACAPA")
        '                    EjecutarSP("dbp_liquidacion1_ZACA")
        '                    EjecutarSP("dbp_facturas_arsa_zac")

        '                    EjecutarSP("dbp_cargaMP_Xela")
        '                    EjecutarSP("dbp_liquidacion1_XELA")
        '                    EjecutarSP("dbp_facturas_arsa_xel")

        '                    EjecutarSP("dbp_cargaMP_JUTIAPA")
        '                    EjecutarSP("dbp_liquidacion1_JUTIA")
        '                    EjecutarSP("dbp_facturas_arsa_jut")

        '                    EjecutarSP("dbp_cargaMP_DTS")
        '                    EjecutarSP("dbp_liquidacion1_DTS")
        '                    EjecutarSP("dbp_facturas_arsa_dts")

        '                    EjecutarSP("dbp_cargaMP_CHIMALTEN")
        '                    EjecutarSP("dbp_liquidacion1_CHIMAL")
        '                    EjecutarSP("dbp_facturas_arsa_chi")

        '                    EjecutarSP("dbp_cargaMP_MAZATE")
        '                    EjecutarSP("dbp_liquidacion1_MAZA")
        '                    EjecutarSP("dbp_facturas_arsa_maz")

        '                    EjecutarSP("dbp_cargaMP_MAYOREO")
        '                    EjecutarSP("dbp_liquidacion1_MAYOR")
        '                    EjecutarSP("dbp_facturas_arsa_may")

        '                    EjecutarSP("dbp_cargaMP_ATLANTICO")
        '                    EjecutarSP("dbp_liquidacion1_ATLAN")
        '                    EjecutarSP("dbp_facturas_arsa_Atl")

        '                    EjecutarSP("dbp_cargaMP_ESCUINTLA")
        '                    EjecutarSP("dbp_liquidacion1_ESCUIN")
        '                    EjecutarSP("dbp_facturas_arsa_esc")
        '                Case 4500   'NOR-ORIENTE
        '                    EjecutarSP("dbp_cargaMP_ZACAPA")
        '                    EjecutarSP("dbp_liquidacion1_ZACA")
        '                    EjecutarSP("dbp_facturas_arsa_zac")

        '                Case 4600   'XELA
        '                    EjecutarSP("dbp_cargaMP_Xela")
        '                    EjecutarSP("dbp_liquidacion1_XELA")
        '                    EjecutarSP("dbp_facturas_arsa_xel")

        '                Case 4610   'COATEPEQUE
        '                    'no hay sp?
        '                Case 4700   'JUTIAPA
        '                    EjecutarSP("dbp_cargaMP_JUTIAPA")
        '                    EjecutarSP("dbp_liquidacion1_JUTIA")
        '                    EjecutarSP("dbp_facturas_arsa_jut")

        '                Case 5000   'MEGABODEGA
        '                    EjecutarSP("dbp_cargaMP_DTS")
        '                    EjecutarSP("dbp_liquidacion1_DTS")
        '                    EjecutarSP("dbp_facturas_arsa_dts")

        '                Case 5020   'CHIMALTENANGO
        '                    EjecutarSP("dbp_cargaMP_CHIMALTEN")
        '                    EjecutarSP("dbp_liquidacion1_CHIMAL")
        '                    EjecutarSP("dbp_facturas_arsa_chi")
        '                Case 5030   'MAZATE
        '                    EjecutarSP("dbp_cargaMP_MAZATE")
        '                    EjecutarSP("dbp_liquidacion1_MAZA")
        '                    EjecutarSP("dbp_facturas_arsa_maz")

        '                Case 5040   'MEGAMAYOREO
        '                    EjecutarSP("dbp_cargaMP_MAYOREO")
        '                    EjecutarSP("dbp_liquidacion1_MAYOR")
        '                    EjecutarSP("dbp_facturas_arsa_may")

        '                Case 5060   'ATLANTICO
        '                    EjecutarSP("dbp_cargaMP_ATLANTICO")
        '                    EjecutarSP("dbp_liquidacion1_ATLAN")
        '                    EjecutarSP("dbp_facturas_arsa_Atl")
        '                Case 5200   'ESCUINTLA
        '                    EjecutarSP("dbp_cargaMP_ESCUINTLA")
        '                    EjecutarSP("dbp_liquidacion1_ESCUIN")
        '                    EjecutarSP("dbp_facturas_arsa_esc")
        '            End Select

        '        Case 1 'Cargar Movimientos Productos
        '            Select Case InfoCache.SucursalClave
        '                Case 0
        '                    EjecutarSP("dbp_cargaMP_ZACAPA")
        '                    EjecutarSP("dbp_cargaMP_Xela")
        '                    EjecutarSP("dbp_cargaMP_JUTIAPA")
        '                    EjecutarSP("dbp_cargaMP_DTS")
        '                    EjecutarSP("dbp_cargaMP_CHIMALTEN")
        '                    EjecutarSP("dbp_cargaMP_MAZATE")
        '                    EjecutarSP("dbp_cargaMP_MAYOREO")
        '                    EjecutarSP("dbp_cargaMP_ATLANTICO")
        '                    EjecutarSP("dbp_cargaMP_ESCUINTLA")

        '                Case 4500   'NOR-ORIENTE
        '                    EjecutarSP("dbp_cargaMP_ZACAPA")
        '                Case 4600   'XELA
        '                    EjecutarSP("dbp_cargaMP_Xela")
        '                Case 4610   'COATEPEQUE
        '                    'no hay sp?
        '                Case 4700   'JUTIAPA
        '                    EjecutarSP("dbp_cargaMP_JUTIAPA")
        '                Case 5000   'MEGABODEGA
        '                    EjecutarSP("dbp_cargaMP_DTS")
        '                Case 5020   'CHIMALTENANGO
        '                    EjecutarSP("dbp_cargaMP_CHIMALTEN")
        '                Case 5030   'MAZATE
        '                    EjecutarSP("dbp_cargaMP_MAZATE")
        '                Case 5040   'MEGAMAYOREO
        '                    EjecutarSP("dbp_cargaMP_MAYOREO")
        '                Case 5060   'ATLANTICO
        '                    EjecutarSP("dbp_cargaMP_ATLANTICO")
        '                Case 5200   'ESCUINTLA
        '                    EjecutarSP("dbp_cargaMP_ESCUINTLA")
        '            End Select
        '            '
        '            '

        '        Case 2 'Cargar Movimientos Liquidaciones e Ingresos
        '            Select Case InfoCache.SucursalClave

        '                Case 0 'todas las sucursales

        '                    EjecutarSP("dbp_liquidacion1_ZACA")
        '                    EjecutarSP("dbp_liquidacion1_XELA")
        '                    EjecutarSP("dbp_liquidacion1_JUTIA")
        '                    EjecutarSP("dbp_liquidacion1_DTS")
        '                    EjecutarSP("dbp_liquidacion1_CHIMAL")
        '                    EjecutarSP("dbp_liquidacion1_MAZA")
        '                    EjecutarSP("dbp_liquidacion1_MAYOR")
        '                    EjecutarSP("dbp_liquidacion1_ATLAN")
        '                    EjecutarSP("dbp_liquidacion1_ESCUIN")

        '                Case 4500   'NOR-ORIENTE
        '                    EjecutarSP("dbp_liquidacion1_ZACA")
        '                Case 4600   'XELA
        '                    EjecutarSP("dbp_liquidacion1_XELA")
        '                Case 4610   'COATEPEQUE
        '                    'no hay sp?
        '                Case 4700   'JUTIAPA
        '                    EjecutarSP("dbp_liquidacion1_JUTIA")
        '                Case 5000   'MEGABODEGA
        '                    EjecutarSP("dbp_liquidacion1_DTS")
        '                Case 5020   'CHIMALTENANGO
        '                    EjecutarSP("dbp_liquidacion1_CHIMAL")
        '                Case 5030   'MAZATE
        '                    EjecutarSP("dbp_liquidacion1_MAZA")
        '                Case 5040   'MEGAMAYOREO
        '                    EjecutarSP("dbp_liquidacion1_MAYOR")
        '                Case 5060   'ATLANTICO
        '                    EjecutarSP("dbp_liquidacion1_ATLAN")
        '                Case 5200   'ESCUINTLA
        '                    EjecutarSP("dbp_liquidacion1_ESCUIN")

        '            End Select

        '        Case 3 'Cargar Movimientos de Facturacion

        '            Select Case InfoCache.SucursalClave
        '                Case 0
        '                    EjecutarSP("dbp_facturas_arsa_Atl")
        '                    EjecutarSP("dbp_facturas_arsa_chi")
        '                    EjecutarSP("dbp_facturas_arsa_dts")
        '                    EjecutarSP("dbp_facturas_arsa_jut")
        '                    EjecutarSP("dbp_facturas_arsa_may")
        '                    EjecutarSP("dbp_facturas_arsa_maz")
        '                    EjecutarSP("dbp_facturas_arsa_zac")
        '                    EjecutarSP("dbp_facturas_arsa_xel")
        '                    EjecutarSP("dbp_facturas_arsa_esc")

        '                Case 4500   'NOR-ORIENTE
        '                    EjecutarSP("dbp_facturas_arsa_zac")
        '                Case 4600   'XELA
        '                    EjecutarSP("dbp_facturas_arsa_xel")
        '                Case 4610   'COATEPEQUE
        '                    'no hay sp?
        '                Case 4700   'JUTIAPA
        '                    EjecutarSP("dbp_facturas_arsa_jut")
        '                Case 5000   'MEGABODEGA
        '                    EjecutarSP("dbp_facturas_arsa_dts")
        '                Case 5020   'CHIMALTENANGO
        '                    EjecutarSP("dbp_facturas_arsa_chi")
        '                Case 5030   'MAZATE
        '                    EjecutarSP("dbp_facturas_arsa_maz")
        '                Case 5040   'MEGAMAYOREO
        '                    EjecutarSP("dbp_facturas_arsa_may")
        '                Case 5060   'ATLANTICO
        '                    EjecutarSP("dbp_facturas_arsa_Atl")
        '                Case 5200   'ESCUINTLA
        '                    EjecutarSP("dbp_facturas_arsa_esc")

        '            End Select

        '    End Select

        'End Sub
        'Public Sub ProcesarHonduras()
        '    Select Case InfoCache.SucursalClave
        '        Case 0
        '        Case 3300   'SAN PEDRO SULA
        '        Case 3301   'STA. ROSA DE COPAN
        '        Case 3400   'TEGUCIGALPA
        '        Case 3401   'COMAYAGUA
        '        Case 3402   'OLANCHO
        '        Case 3404   'CHOLUTECA
        '        Case 3500   'LA CEIBA
        '    End Select
        'End Sub
        'Public Sub ProcesarPanama()
        '    Select Case InfoCache.SucursalClave
        '        Case 0
        '        Case 1  'PANAMA
        '        Case 2  'TRANSISTMICA
        '        Case 3  'CHITRE
        '        Case 4  'DAVID
        '        Case 5  'CHORRERA
        '        Case 6  'AGUADULCE

        '    End Select
        'End Sub
        'Public Sub ProcesarElSalvador()
        '    Select Case InfoCache.SucursalClave
        '        Case 0
        '        Case 5100 'SAN SALVADOR

        '    End Select
        'End Sub
        'Public Sub ProcesarCostaRica()
        '    Select Case InfoCache.SucursalClave
        '        Case 0
        '        Case 50 'COSTA RICA


        '    End Select
        'End Sub
        'Private Sub EjecutarSP_test(ByVal NombreSP As String)

        'End Sub
        'Private Sub EjecutarSP(ByVal NombreSP As String)
        '    RaiseEvent Mensaje(Now.ToShortTimeString & "> SP: " & NombreSP)
        '    Dim conn As OracleConnection = New OracleConnection


        '    conn.ConnectionString = InfoCache.ConnectionString
        '    conn.Open()
        '    Dim cmd As New OracleCommand
        '    cmd.Connection = conn

        '    'Dim MaestroAdapter As OracleDataAdapter = New OracleDataAdapter(cmd)
        '    'Dim MaestroDataSet As New OSDMaestroData


        '    cmd.CommandText = "PRODODS." & NombreSP
        '    cmd.CommandType = CommandType.StoredProcedure


        '    Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Date)
        '    Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.Date)
        '    p1.Value = InfoCache.FechaDesde
        '    p2.Value = InfoCache.FechaHasta

        '    cmd.Parameters.Add(p1)
        '    cmd.Parameters.Add(p2)

        '    Try
        '        Dim recordsInesrtados As Integer = cmd.ExecuteNonQuery()
        '    Catch ex As Exception

        '    End Try

        '    cmd.Dispose()
        '    conn.Close()
        'End Sub
    End Class
End Namespace


