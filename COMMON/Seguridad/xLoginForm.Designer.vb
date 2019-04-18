

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class xLoginForm
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(xLoginForm))
        Me.cmdAceptar = New System.Windows.Forms.Button()
        Me.cmdCancelar = New System.Windows.Forms.Button()
        Me.Version = New System.Windows.Forms.Label()
        Me.fadeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Usuario = New adps.ControlesXP.XPTextBox()
        Me.Password = New adps.ControlesXP.XPTextBox()
        Me.Servidor = New adps.ControlesXP.XPTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdAceptar
        '
        Me.cmdAceptar.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdAceptar.ForeColor = System.Drawing.Color.Black
        Me.cmdAceptar.Location = New System.Drawing.Point(206, 210)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(80, 30)
        Me.cmdAceptar.TabIndex = 3
        Me.cmdAceptar.TabStop = False
        Me.cmdAceptar.Text = "Aceptar"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdCancelar.ForeColor = System.Drawing.Color.Black
        Me.cmdCancelar.Location = New System.Drawing.Point(296, 210)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(80, 30)
        Me.cmdCancelar.TabIndex = 8
        Me.cmdCancelar.TabStop = False
        Me.cmdCancelar.Text = "Cancelar"
        '
        'Version
        '
        Me.Version.AccessibleDescription = "Version label"
        Me.Version.AccessibleName = "Version label"
        Me.Version.ForeColor = System.Drawing.Color.Gray
        Me.Version.Location = New System.Drawing.Point(12, 71)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(123, 23)
        Me.Version.TabIndex = 12
        Me.Version.Text = "<versión>"
        '
        'fadeTimer
        '
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(388, 64)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'Usuario
        '
        Me.Usuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Usuario.Location = New System.Drawing.Point(163, 102)
        Me.Usuario.Name = "Usuario"
        Me.Usuario.PasswortChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Usuario.Size = New System.Drawing.Size(152, 24)
        Me.Usuario.TabIndex = 0
        '
        'Password
        '
        Me.Password.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Password.Location = New System.Drawing.Point(163, 134)
        Me.Password.Name = "Password"
        Me.Password.PasswortChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.Password.Size = New System.Drawing.Size(152, 24)
        Me.Password.TabIndex = 1
        '
        'Servidor
        '
        Me.Servidor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Servidor.Location = New System.Drawing.Point(163, 166)
        Me.Servidor.Name = "Servidor"
        Me.Servidor.PasswortChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Servidor.Size = New System.Drawing.Size(152, 24)
        Me.Servidor.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(93, 145)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Contraseña:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(80, 177)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Base de Datos:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(74, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Nombre usuario:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 252)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Servidor)
        Me.Controls.Add(Me.Password)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.Usuario)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Conectar"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents Version As System.Windows.Forms.Label

    Public Sub New(ByVal roleAplicacion As String)
        InitializeComponent()
        AplicacionRole = RoleAplicacion

        Me.Servidor.Text = My.Settings.Servidor
        Me.Usuario.Text = My.Settings.User
        If Me.Usuario.Text.Length > 0 Then
            Me.Password.Focus()
        End If

    End Sub
    Friend WithEvents fadeTimer As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Usuario As Adps.ControlesXP.XPTextBox
    Friend WithEvents Password As Adps.ControlesXP.XPTextBox
    Friend WithEvents Servidor As Adps.ControlesXP.XPTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class

