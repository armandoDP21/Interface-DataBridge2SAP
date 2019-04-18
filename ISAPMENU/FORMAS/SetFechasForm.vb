Public Class SetFechasForm
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        Me.FechaDesde.Value = My.Settings.FechaDesde
        Me.FechaHasta.Value = My.Settings.FechaHasta
    End Sub
#Region "Eventos"
    Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
        My.Settings.FechaDesde = Me.FechaDesde.Value
        My.Settings.FechaHasta = Me.FechaHasta.Value

        InfoCache.FechaDesde = New System.DateTime(Me.FechaDesde.Value.Year, Me.FechaDesde.Value.Month, Me.FechaDesde.Value.Day)
        InfoCache.FechaHasta = New System.DateTime(Me.FechaHasta.Value.Year, Me.FechaHasta.Value.Month, Me.FechaHasta.Value.Day)
        InfoCache.PeriodoActual = Format((InfoCache.FechaDesde.Month), "000")
        InfoCache.FiscalYear = InfoCache.FechaDesde.Year

        My.Settings.Save()
        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub
    Private Sub CancelarButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelarButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
    Private Sub FechaDesde_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FechaDesde.ValueChanged
        If Me.FechaDesde.Value.DayOfWeek = DayOfWeek.Thursday Then
            Me.FechaHasta.Value = Me.FechaDesde.Value
        Else
            Me.FechaHasta.Value = SetFechaHasta(FechaDesde.Value)
        End If

    End Sub
#End Region
#Region "metodos"
    Private Function SetFechaHasta(ByVal DFecha As Date) As Date
        Dim DHasta As DateTime
        Dim NuevaFecha As DateTime
        Dim UltimoDia As Integer = DateTime.DaysInMonth(DFecha.Year, DFecha.Month)
        Dim DiferenciaDias As Integer = UltimoDia - DFecha.Day
        If DiferenciaDias < 6 Then
            DHasta = DFecha.AddDays(DiferenciaDias)
        Else
            DHasta = DFecha.AddDays(1)
            NuevaFecha = DHasta

            For i As Integer = 1 To 6
                If DHasta.DayOfWeek = DayOfWeek.Thursday Then
                    Exit For
                Else
                    NuevaFecha = DHasta
                End If
                DHasta = NuevaFecha.AddDays(1)
            Next
        End If
        Return DHasta
    End Function
#End Region
End Class