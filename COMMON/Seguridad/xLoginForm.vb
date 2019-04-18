Public Class xLoginForm


    'Private identity As IIdentity
    'Private token As IToken = Nothing
    'Private authenticated As Boolean
    'Private cache As ISecurityCacheProvider
    'Private ruleProvider As IAuthorizationProvider
    'Private profile As PerfilUsuario
    Private AplicacionRole As String
    Private _loginattempts As Integer
    Private logginAttemps As Short
    Private m_showing As Boolean = True


    'Public Shared AppForm As System.Windows.Forms.Form
#Region " Evento Aceptar "


    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        If Me.Usuario.Text.Trim().Length > 0 AndAlso Me.Password.Text.Trim().Length > 0 Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

            'If Autenticar() Then
            '    If IsInAplicationRole() Then
            '        Me.DialogResult = Windows.Forms.DialogResult.OK
            '        Me.Close()
            '    Else
            '        Me.DialogResult = Windows.Forms.DialogResult.Cancel
            '        Me.Close()
            '    End If
            'Else
            '    Me.DialogResult = Windows.Forms.DialogResult.Cancel
            'End If

        Else
            logginAttemps = CShort(logginAttemps + 1)
            If logginAttemps > 3 Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            Else
                Me.Password.Text = ""
                Password.Focus()
                MessageBox.Show("Ingrese nombre y contraseña válidos.", "Error en inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            End If
        End If
    End Sub
#End Region

#Region " Eventos "
    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Version.Text = "Versión " & My.
        Me.Opacity = 0
        Me.Activate()
        Me.Refresh()
        fadeTimer.Start()
        Me.Refresh()

    End Sub
    Private Sub Servidor_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Servidor.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            Me.cmdAceptar.PerformClick()
        End If
    End Sub
    Private Sub Usuario_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Usuario.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            Me.Password.Focus()
        End If

    End Sub
    Private Sub Password_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Password.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If Servidor.Text.Length > 0 Then
                e.KeyChar.Equals(Nothing)
                SendKeys.Send("{TAB}")
                Me.cmdAceptar.PerformClick()
            Else
                Servidor.Focus()
            End If
        End If
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.Close()
    End Sub
    Private Sub fadeTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fadeTimer.Tick
        Dim d As Double

        If m_showing Then
            d = (1000 / Me.fadeTimer.Interval) / 100
            If Me.Opacity + d >= 1.0 Then
                Me.Opacity = 1.0
                Me.fadeTimer.Stop()
            Else
                Me.Opacity += d
            End If
        Else
            d = (1000.0 / Me.fadeTimer.Interval) / 100.0
            If Me.Opacity - d <= 0.0 Then
                Me.Opacity = 0.0
                Me.fadeTimer.Stop()
            Else
                Me.Opacity -= d
            End If
        End If
    End Sub
    Private Sub LoginForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        m_showing = False
        fadeTimer.Start()
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


#Region " propiedades "

    Public ReadOnly Property UusarioActual() As String
        Get
            Return Me.Usuario.Text
        End Get
    End Property
    Public ReadOnly Property PasswordActual() As String
        Get
            Return Me.Password.Text
        End Get
    End Property
    Public ReadOnly Property ServerHostActual() As String
        Get
            Return Me.Servidor.Text
        End Get
    End Property
#End Region

#End Region


End Class
