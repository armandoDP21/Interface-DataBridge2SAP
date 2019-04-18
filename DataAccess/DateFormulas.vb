Public Class DateFormulas
    Public Function PreviousDayOfWeek(ByVal StartDate As Date, _
     ByVal DayOfWeek As DayOfWeek) As Object
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' PreviousDayOfWeek
        ' This function returns the date of the DayOfWeek prior to StartDate.
        ' Note that this function uses WSMod to use Excel's worksheet function MOD
        ' rather than VBA's Mod operator.
        ' Formula equivalent:
        '  =StartDate-MOD(WEEKDAY(StartDate)-DayOfWeek,7)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'If (DayOfWeek < vbSunday) Or (DayOfWeek > vbSaturday) Then
        '    PreviousDayOfWeek = CVErr(xlErrValue)
        '    Exit Function
        'End If
        'If (StartDate < 0) Then
        '    PreviousDayOfWeek = CVErr(xlErrValue)
        '    Exit Function
        'End If
        PreviousDayOfWeek = DateAdd("d", WSMod(Weekday(StartDate) - DayOfWeek, 7), StartDate)
        'PreviousDayOfWeek = StartDate - WSMod(Weekday(StartDate) - DayOfWeek, 7)


    End Function

    Function WSMod(ByVal Number As Double, ByVal Divisor As Double) As Double
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' WSMod
        ' The Excel worksheet function MOD and the VBA Mod operator
        ' work differently and can return different results under
        ' certain circumstances. For continuity between the worksheet
        ' formulas and the VBA code, we use this WSMod function, which
        ' produces the same result as the Excel MOD worksheet function,
        ' rather than the VBA Mod operator.
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Return Number - Divisor * Int(Number / Divisor)
    End Function


End Class
