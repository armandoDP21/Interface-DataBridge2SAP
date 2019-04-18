
Imports Bis.Sap.Common

Imports System.Data.Common
Imports System.Text

Public Class ProcesoSP
    Public Event Mensaje(TextoMensaje As String)
    Dim start, finish, totalTime As Double

    Public Function EjecutarSP(SPname As String, FechaDesde As Date, FechaHasta As Date)
        start = Microsoft.VisualBasic.DateAndTime.Timer
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Proceso: " & SPname & " de: " & Format(FechaDesde, "dd/MM/yy") & " a:" & Format(FechaHasta, "dd/MM/yy"))
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Iniciado a")
        Dim recordsInesrtados As Integer


        Dim ConnectionText As New StringBuilder
        ConnectionText.Append("user id=prodods;password=managerjr;data source=")
        ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
        ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
        ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
        Dim conn As New OracleConnection(ConnectionText.ToString)


        Try
            conn.Open()

        Catch e As OracleException
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> NO SE EJECUTÓ el proceso: " & SPname & ". Solicite información a Soporte de Aplicaciones")
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Message)

            'Dim errorMessage As String = "Code: " & e.Source & vbCrLf & _
            '                             "Message: " & e.Message

            'Dim log As System.Diagnostics.EventLog = New System.Diagnostics.EventLog()
            'log.Source = "My Application"
            'log.WriteEntry(errorMessage)

            InfoCache.UpdateError = e.Message
        End Try

        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.CommandText = "PRODODS." & SPname
        cmd.CommandType = CommandType.StoredProcedure


        Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Date)
        Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.Date)
        p1.Value = FechaDesde
        p2.Value = FechaHasta

        cmd.Parameters.Add(p1)
        cmd.Parameters.Add(p2)

        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Ejecución de proceso iniciado...")

        Try
            recordsInesrtados = cmd.ExecuteNonQuery()
        Catch e As OracleException
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> NO SE EJECUTÓ el proceso: " & SPname)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> Solicite información a Soporte de Aplicaciones.")
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.ErrorCode)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Source)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Message)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Procedure)


            InfoCache.UpdateError = e.Message
        End Try

        If recordsInesrtados = -1 Then
            totalTime = Microsoft.VisualBasic.DateAndTime.Timer - start
            RaiseEvent Mensaje(Format(Date.Now, "R") & ">  " & SPname & " terminó satisfactoriamente.")
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> Duración: " & SecondsToText(totalTime))
        Else

            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & SPname & " no se ejecutó. Debe procesarla nuevamente.")
        End If
        RaiseEvent Mensaje("...............................................")
        cmd.Dispose()
        conn.Close()
        conn.Dispose()
        Return recordsInesrtados
    End Function
    Public Function EjecutarSPC(SPname As String, FechaDesde As Date, FechaHasta As Date, PaisClave As Decimal)
        start = Microsoft.VisualBasic.DateAndTime.Timer
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Proceso: " & SPname & " de: " & Format(FechaDesde, "dd/MM/yy") & " a:" & Format(FechaHasta, "dd/MM/yy"))
        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Iniciado a")
        Dim recordsInesrtados As Integer
        'Dim conn As OracleConnection = New OracleConnection


        Dim ConnectionText As New StringBuilder
        ConnectionText.Append("user id=prodods;password=managerjr;data source=")
        ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
        ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
        ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
        Dim conn As New OracleConnection(ConnectionText.ToString)


        Try
            conn.Open()

        Catch e As OracleException
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> NO SE EJECUTÓ el proceso: " & SPname & ". Solicite información a Soporte de Aplicaciones")
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Message)

            'Dim errorMessage As String = "Code: " & e.Source & vbCrLf & _
            '                             "Message: " & e.Message

            'Dim log As System.Diagnostics.EventLog = New System.Diagnostics.EventLog()
            'log.Source = "My Application"
            'log.WriteEntry(errorMessage)

            InfoCache.UpdateError = e.Message
        End Try

        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.CommandText = "PRODODS." & SPname
        cmd.CommandType = CommandType.StoredProcedure


        Dim p1 As OracleParameter = New OracleParameter("p1", OracleDbType.Date)
        Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.Date)
        Dim p3 As OracleParameter = New OracleParameter("p2", OracleDbType.Decimal)
        p1.Value = FechaDesde
        p2.Value = FechaHasta
        p3.Value = PaisClave
        cmd.Parameters.Add(p1)
        cmd.Parameters.Add(p2)
        cmd.Parameters.Add(p3)

        RaiseEvent Mensaje(Format(Date.Now, "R") & "> Ejecución de proceso iniciado...")

        Try
            recordsInesrtados = cmd.ExecuteNonQuery()
        Catch e As OracleException
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> NO SE EJECUTÓ el proceso: " & SPname)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> Solicite información a Soporte de Aplicaciones.")
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.ErrorCode)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Source)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Message)
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & e.Procedure)

            'Dim errorMessage As String = "Code: " & e.Source & vbCrLf & _
            '                             "Message: " & e.Messager

            'Dim log As System.Diagnostics.EventLog = New System.Diagnostics.EventLog()
            'log.Source = "My Application"
            'log.WriteEntry(errorMessage)

            InfoCache.UpdateError = e.Message
        End Try

        If recordsInesrtados = -1 Then
            totalTime = Microsoft.VisualBasic.DateAndTime.Timer - start
            RaiseEvent Mensaje(Format(Date.Now, "R") & ">  " & SPname & " terminó satisfactoriamente.")
            RaiseEvent Mensaje(Format(Date.Now, "R") & "> Duración: " & SecondsToText(totalTime))
        Else

            RaiseEvent Mensaje(Format(Date.Now, "R") & "> " & SPname & " no se ejecutó. Debe procesarla nuevamente.")
        End If
        RaiseEvent Mensaje("...............................................")
        cmd.Dispose()
        conn.Close()
        conn.Dispose()
        Return recordsInesrtados
    End Function

    Function SecondsToText(Seconds As Double) As String
        Dim days As Integer
        Dim hours As Double
        Dim minutes As Double

        Dim bAddComma As Boolean
        Dim Result As String
        Dim sTemp As String

        If Seconds <= 0 Or Not IsNumeric(Seconds) Then
            SecondsToText = "0 segundos"
            Exit Function
        End If
        Seconds = Fix(Seconds)
        If Seconds >= 86400 Then
            days = Fix(Seconds / 86400)
        Else
            days = 0

        End If

        If Seconds - (days * 86400) >= 3600 Then
            hours = Fix((Seconds - (days * 86400)) / 3600)
        Else
            hours = 0
        End If
        If Seconds - (hours * 3600) - (days * 86400) >= 60 Then
            minutes = Fix((Seconds - (hours * 3600) - (days * 86400)) / 60)
        Else
            minutes = 0
        End If


        Seconds = Seconds - (minutes * 60) - (hours * 3600) - (days * 86400)


        Result = Seconds & " segundo" & AutoS(Seconds)
        If minutes > 0 Then
            bAddComma = Result <> ""
            sTemp = minutes & " minuto" & AutoS(minutes)
            If bAddComma Then
                sTemp = sTemp & ", "
                Result = sTemp & Result
            End If
            If hours > 0 Then
                bAddComma = Result <> ""
                sTemp = hours & " hora" & AutoS(hours)
                If bAddComma Then sTemp = sTemp & ", "
                Result = sTemp & Result

            End If
            If days > 0 Then
                bAddComma = Result <> ""
                sTemp = days & " dia" & AutoS(days)
                If bAddComma Then sTemp = sTemp & ", "
                Result = sTemp & Result
            End If

        End If

        Return Result
    End Function
    Private Function AutoS(Number)

        If Number = 1 Then
            AutoS = ""
        Else
            AutoS = "s"
        End If



    End Function
End Class
