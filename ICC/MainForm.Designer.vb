<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ClaveCia_Label = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VerButton = New System.Windows.Forms.Button()
        Me.ProcesarButton = New System.Windows.Forms.Button()
        Me.Moneda = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NombrePais = New System.Windows.Forms.Label()
        Me.NombreUsuario = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.FechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SecuenciaInicial = New System.Windows.Forms.TextBox()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 264)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Compa;ia:"
        '
        'ClaveCia_Label
        '
        Me.ClaveCia_Label.AutoSize = True
        Me.ClaveCia_Label.Location = New System.Drawing.Point(93, 264)
        Me.ClaveCia_Label.Name = "ClaveCia_Label"
        Me.ClaveCia_Label.Size = New System.Drawing.Size(0, 13)
        Me.ClaveCia_Label.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 232)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Moneda:"
        '
        'VerButton
        '
        Me.VerButton.Location = New System.Drawing.Point(306, 276)
        Me.VerButton.Name = "VerButton"
        Me.VerButton.Size = New System.Drawing.Size(164, 29)
        Me.VerButton.TabIndex = 16
        Me.VerButton.Text = "Ver datos"
        Me.VerButton.UseVisualStyleBackColor = True
        '
        'ProcesarButton
        '
        Me.ProcesarButton.Enabled = False
        Me.ProcesarButton.Location = New System.Drawing.Point(306, 241)
        Me.ProcesarButton.Name = "ProcesarButton"
        Me.ProcesarButton.Size = New System.Drawing.Size(164, 29)
        Me.ProcesarButton.TabIndex = 17
        Me.ProcesarButton.Text = "Procesar"
        Me.ProcesarButton.UseVisualStyleBackColor = True
        '
        'Moneda
        '
        Me.Moneda.AutoSize = True
        Me.Moneda.Location = New System.Drawing.Point(92, 232)
        Me.Moneda.Name = "Moneda"
        Me.Moneda.Size = New System.Drawing.Size(0, 13)
        Me.Moneda.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(43, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Pais:"
        '
        'NombrePais
        '
        Me.NombrePais.AutoSize = True
        Me.NombrePais.Location = New System.Drawing.Point(92, 206)
        Me.NombrePais.Name = "NombrePais"
        Me.NombrePais.Size = New System.Drawing.Size(0, 13)
        Me.NombrePais.TabIndex = 10
        '
        'NombreUsuario
        '
        Me.NombreUsuario.AutoSize = True
        Me.NombreUsuario.Location = New System.Drawing.Point(92, 180)
        Me.NombreUsuario.Name = "NombreUsuario"
        Me.NombreUsuario.Size = New System.Drawing.Size(0, 13)
        Me.NombreUsuario.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(274, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Hasta:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(273, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Desde:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 180)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Usuario:"
        '
        'FechaHasta
        '
        Me.FechaHasta.CustomFormat = "ddd dd-MMM-yyyy"
        Me.FechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaHasta.Location = New System.Drawing.Point(322, 55)
        Me.FechaHasta.Name = "FechaHasta"
        Me.FechaHasta.Size = New System.Drawing.Size(164, 21)
        Me.FechaHasta.TabIndex = 7
        '
        'FechaDesde
        '
        Me.FechaDesde.CustomFormat = "ddd dd-MMM-yyyy"
        Me.FechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaDesde.Location = New System.Drawing.Point(322, 26)
        Me.FechaDesde.Name = "FechaDesde"
        Me.FechaDesde.Size = New System.Drawing.Size(164, 21)
        Me.FechaDesde.TabIndex = 8
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 332)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(526, 22)
        Me.StatusStrip1.TabIndex = 21
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(511, 17)
        Me.StatusLabel.Spring = True
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ICC.My.Resources.Resources.CARICAM_Oficial_calado
        Me.PictureBox1.Location = New System.Drawing.Point(12, 26)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(188, 124)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(274, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "No secuencia:"
        '
        'SecuenciaInicial
        '
        Me.SecuenciaInicial.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.ICC.My.MySettings.Default, "SecuenciaInicial", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SecuenciaInicial.Location = New System.Drawing.Point(353, 82)
        Me.SecuenciaInicial.Name = "SecuenciaInicial"
        Me.SecuenciaInicial.Size = New System.Drawing.Size(133, 21)
        Me.SecuenciaInicial.TabIndex = 23
        Me.SecuenciaInicial.Text = Global.ICC.My.MySettings.Default.SecuenciaInicial
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 354)
        Me.Controls.Add(Me.SecuenciaInicial)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ClaveCia_Label)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.VerButton)
        Me.Controls.Add(Me.ProcesarButton)
        Me.Controls.Add(Me.Moneda)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NombrePais)
        Me.Controls.Add(Me.NombreUsuario)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FechaHasta)
        Me.Controls.Add(Me.FechaDesde)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ICC - Interfase de Cuentas por cobrar"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ClaveCia_Label As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents VerButton As System.Windows.Forms.Button
    Friend WithEvents ProcesarButton As System.Windows.Forms.Button
    Friend WithEvents Moneda As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NombrePais As System.Windows.Forms.Label
    Friend WithEvents NombreUsuario As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents FechaDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents SecuenciaInicial As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label

End Class
