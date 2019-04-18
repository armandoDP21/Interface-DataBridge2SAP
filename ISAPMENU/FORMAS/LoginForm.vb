Public Class LoginForm
    Private AplicacionRole As String
    Private _loginattempts As Integer
    Private logginAttemps As Short
    Private m_showing As Boolean = True
    Private DatosMenu As RunIsapData
#Region "Metodos"
    Private Sub Autenticar()

        _loginattempts += 1
        ISAPMainForm.StatusTextBox.AppendText("Autenticando: intentos " & _loginattempts.ToString & Environment.NewLine)
        My.Application.DoEvents()
        InfoCache.UpdateError = String.Empty
        DatosMenu = (New MenuSP).MenuSelectData(Me.GPIDText.Text.Trim)
        If InfoCache.UpdateError.Length > 0 Then
            MsgBox(InfoCache.UpdateError, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "No puede ingresar a la aplicacion.")
        Else
            If DatosMenu.VSI_PROCESOS.Rows.Count = 0 Then

                MsgBox("Ha ingresado un GPID invalido", MsgBoxStyle.Exclamation And MsgBoxStyle.OkOnly, "GPID invalido")
                ISAPMainForm.StatusTextBox.AppendText("Autenticacion fallo" & Environment.NewLine)
            Else
                InfoCache.SAPuser = Me.GPIDText.Text.Trim
                My.Settings.Save()
                Me.DialogResult = Windows.Forms.DialogResult.OK
                ISAPMainForm.StatusTextBox.AppendText("Usuario Autenticado como " & Me.GPIDText.Text & Environment.NewLine)
            End If
        End If

    End Sub
    Public ReadOnly Property MenuDatos As RunIsapData
        Get
            Return DatosMenu
        End Get
    End Property
#End Region

#Region "Eventos"
    Private Sub AceptarButton_Click_1(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
        Autenticar()
    End Sub
    Private Sub CancelarButton_Click_1(sender As System.Object, e As System.EventArgs) Handles CancelarButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Abort
    End Sub
    Private Sub GPIDText_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If GPIDText.Text.Length > 0 Then
                e.KeyChar.Equals(Nothing)
                SendKeys.Send("{TAB}")
                Me.AceptarButton.PerformClick()
            Else
                AceptarButton.Focus()
            End If
        End If
    End Sub
#End Region





End Class