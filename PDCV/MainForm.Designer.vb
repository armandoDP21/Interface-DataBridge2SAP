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
        Me.FechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.FechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NombreUsuario = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Moneda = New System.Windows.Forms.Label()
        Me.ProcesarButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NombrePais = New System.Windows.Forms.Label()
        Me.VerButton = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ClaveCia_Label = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Tiempo = New System.Windows.Forms.Label()
        Me.Seconds = New System.Windows.Forms.Label()
        Me.SecuenciaInicial = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FechaDesde
        '
        Me.FechaDesde.CustomFormat = "ddd dd-MMM-yyyy"
        Me.FechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaDesde.Location = New System.Drawing.Point(322, 54)
        Me.FechaDesde.Name = "FechaDesde"
        Me.FechaDesde.Size = New System.Drawing.Size(164, 23)
        Me.FechaDesde.TabIndex = 0
        '
        'FechaHasta
        '
        Me.FechaHasta.CustomFormat = "ddd dd-MMM-yyyy"
        Me.FechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaHasta.Location = New System.Drawing.Point(322, 83)
        Me.FechaHasta.Name = "FechaHasta"
        Me.FechaHasta.Size = New System.Drawing.Size(164, 23)
        Me.FechaHasta.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Usuario:"
        '
        'NombreUsuario
        '
        Me.NombreUsuario.AutoSize = True
        Me.NombreUsuario.Location = New System.Drawing.Point(108, 171)
        Me.NombreUsuario.Name = "NombreUsuario"
        Me.NombreUsuario.Size = New System.Drawing.Size(0, 15)
        Me.NombreUsuario.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(59, 197)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Pais:"
        '
        'Moneda
        '
        Me.Moneda.AutoSize = True
        Me.Moneda.Location = New System.Drawing.Point(108, 223)
        Me.Moneda.Name = "Moneda"
        Me.Moneda.Size = New System.Drawing.Size(0, 15)
        Me.Moneda.TabIndex = 1
        '
        'ProcesarButton
        '
        Me.ProcesarButton.Enabled = False
        Me.ProcesarButton.Location = New System.Drawing.Point(322, 232)
        Me.ProcesarButton.Name = "ProcesarButton"
        Me.ProcesarButton.Size = New System.Drawing.Size(164, 29)
        Me.ProcesarButton.TabIndex = 2
        Me.ProcesarButton.Text = "Procesar"
        Me.ProcesarButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 223)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Moneda:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 330)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(524, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(509, 17)
        Me.StatusLabel.Spring = True
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(273, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 15)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Desde:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(274, 87)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 15)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Hasta:"
        '
        'NombrePais
        '
        Me.NombrePais.AutoSize = True
        Me.NombrePais.Location = New System.Drawing.Point(108, 197)
        Me.NombrePais.Name = "NombrePais"
        Me.NombrePais.Size = New System.Drawing.Size(0, 15)
        Me.NombrePais.TabIndex = 1
        '
        'VerButton
        '
        Me.VerButton.Location = New System.Drawing.Point(322, 267)
        Me.VerButton.Name = "VerButton"
        Me.VerButton.Size = New System.Drawing.Size(164, 29)
        Me.VerButton.TabIndex = 2
        Me.VerButton.Text = "Ver datos"
        Me.VerButton.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(39, 255)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Compa;ia:"
        '
        'ClaveCia_Label
        '
        Me.ClaveCia_Label.AutoSize = True
        Me.ClaveCia_Label.Location = New System.Drawing.Point(109, 255)
        Me.ClaveCia_Label.Name = "ClaveCia_Label"
        Me.ClaveCia_Label.Size = New System.Drawing.Size(0, 15)
        Me.ClaveCia_Label.TabIndex = 5
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.My.Resources.Resources.CARICAM_Oficial_calado
        Me.PictureBox1.Location = New System.Drawing.Point(26, 22)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(188, 124)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'Tiempo
        '
        Me.Tiempo.AutoSize = True
        Me.Tiempo.Location = New System.Drawing.Point(39, 294)
        Me.Tiempo.Name = "Tiempo"
        Me.Tiempo.Size = New System.Drawing.Size(47, 15)
        Me.Tiempo.TabIndex = 8
        Me.Tiempo.Text = "Tiempo"
        '
        'Seconds
        '
        Me.Seconds.AutoSize = True
        Me.Seconds.Location = New System.Drawing.Point(108, 294)
        Me.Seconds.Name = "Seconds"
        Me.Seconds.Size = New System.Drawing.Size(43, 15)
        Me.Seconds.TabIndex = 8
        Me.Seconds.Text = "Label7"
        '
        'SecuenciaInicial
        '
        Me.SecuenciaInicial.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.My.MySettings.Default, "SecuenciaInicial", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SecuenciaInicial.Location = New System.Drawing.Point(374, 112)
        Me.SecuenciaInicial.Name = "SecuenciaInicial"
        Me.SecuenciaInicial.Size = New System.Drawing.Size(112, 23)
        Me.SecuenciaInicial.TabIndex = 25
        Me.SecuenciaInicial.Text = Global.My.MySettings.Default.SecuenciaInicial
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(274, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 15)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "No secuencia:"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(524, 352)
        Me.Controls.Add(Me.SecuenciaInicial)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Seconds)
        Me.Controls.Add(Me.Tiempo)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ClaveCia_Label)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.VerButton)
        Me.Controls.Add(Me.ProcesarButton)
        Me.Controls.Add(Me.Moneda)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NombrePais)
        Me.Controls.Add(Me.NombreUsuario)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FechaHasta)
        Me.Controls.Add(Me.FechaDesde)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PDCV - Interfase de costo de ventas"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FechaDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents FechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NombreUsuario As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Moneda As System.Windows.Forms.Label
    Friend WithEvents ProcesarButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents NombrePais As System.Windows.Forms.Label
    Friend WithEvents VerButton As System.Windows.Forms.Button
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ClaveCia_Label As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Tiempo As System.Windows.Forms.Label
    Friend WithEvents Seconds As System.Windows.Forms.Label
    Friend WithEvents SecuenciaInicial As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
