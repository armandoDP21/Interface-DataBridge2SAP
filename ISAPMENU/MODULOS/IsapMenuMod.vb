Module IsapMenuMod
    Public Function SecondsToText(Seconds As Double) As String
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
End Module
