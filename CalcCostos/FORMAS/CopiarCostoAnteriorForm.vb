Public Class CopiarCostoAnteriorForm
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        Me.YearDestinoText.Text = Now.Year.ToString
        Me.YearOrigenText.Text = GetYearOrigen().ToString
        Me.cboMesDestino.SelectedIndex = Now.Month - 1
        Me.cboMesOrigen.SelectedIndex = GetMonthOrigen() - 1

    End Sub
#Region "metodos"
    Private Sub EjecutarCopiado()
        Dim MesOrigen As String = Format(SelectedMesOrigen, "000")
        Dim MesDestino As String = Format(SelectedMesDestino, "000")
        Dim YearDestino As Integer = CInt(Me.YearDestinoText.Text)
        Dim YearOrigen As Integer = CInt(Me.YearOrigenText.Text)

        With New CostosDML
            Dim result As Integer = .CopiarCostos(MesOrigen, YearOrigen, MesDestino, YearDestino)
        End With
    End Sub
    Private Function GetYearOrigen() As Integer
        Dim returnInteger As Integer
        If Now.Month = 1 Then
            returnInteger = Now.Year - 1
        Else
            returnInteger = Now.Year
        End If
        Return returnInteger
    End Function
    Private Function GetMonthOrigen() As Integer
        Dim returnInteger As Integer
        If Now.Month = 1 Then
            returnInteger = 12
        Else
            returnInteger = Now.Month - 1
        End If
        Return returnInteger
    End Function
    Private Function SelectedMesOrigen() As Object
        Dim indice As Integer = Me.cboMesOrigen.SelectedIndex
        Return indice + 1
    End Function
    Private Function SelectedMesDestino() As Object
        Dim indice As Integer = Me.cboMesDestino.SelectedIndex
        Return indice + 1
    End Function
#End Region
#Region "Eventos"
    Private Sub CopiarButton_Click(sender As System.Object, e As System.EventArgs) Handles CopiarButton.Click, Button1.Click
        EjecutarCopiado()
    End Sub

    Private Sub CerrarButton_Click(sender As System.Object, e As System.EventArgs) Handles CerrarButton.Click, Button2.Click
        Me.Close()
    End Sub
#End Region

End Class