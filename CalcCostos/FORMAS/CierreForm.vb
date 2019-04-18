Public Class CierreForm
    Private Const DataMember As String = "PERIODOACTUAL."
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        Me.PERIODO_ANTERIOR.DataBindings.Clear()
        Me.PERIODO.DataBindings.Clear()
        Me.FISCALYEAR_ANTERIOR.DataBindings.Clear()
        Me.FISCAL_YEAR.DataBindings.Clear()


        Me.PERIODO_ANTERIOR.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PERIODO_ANTERIOR")
        Me.PERIODO.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "PERIODO")
        Me.FISCALYEAR_ANTERIOR.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "FISCALYEAR_ANTERIOR")
        Me.FISCAL_YEAR.DataBindings.Add("Text", InfoCache.EmbarquesDS, DataMember & "FISCAL_YEAR")

    End Sub
    Private Sub cambiarPeriodo()
        Dim MesActual As Integer = CInt(Me.PERIODO.Text)
        Dim FiscalYearActual As Integer = CInt(Me.FISCAL_YEAR.Text)

        Me.PERIODO_ANTERIOR.Text = Me.PERIODO.Text
        Me.FISCALYEAR_ANTERIOR.Text = Me.FISCAL_YEAR.Text

        If MesActual = 12 Then
            Me.PERIODO.Text = Format(1, "000")
            Me.FISCAL_YEAR.Text = (FiscalYearActual + 1).ToString

        Else
            Me.FISCAL_YEAR.Text = (FiscalYearActual).ToString
            Me.PERIODO.Text = Format((MesActual + 1), "000")
        End If

    End Sub
    Private Sub ButtonAbrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAbrir.Click
        cambiarPeriodo()
    End Sub

    Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarButton.Click
        Me.DialogResult = DialogResult.Cancel

    End Sub

    Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarButton.Click
        InfoCache.UpdateError = String.Empty
        BindingContext(InfoCache.EmbarquesDS, "PERIODOACTUAL").EndCurrentEdit()
        Dim result1 As Integer = (New CostosDML).updateEmbarquePeriodoActual()
        If InfoCache.UpdateError.Length > 0 Then
            MsgBox(InfoCache.UpdateError, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Error entrada de datos")
        End If
        Me.DialogResult = DialogResult.OK
    End Sub
End Class
