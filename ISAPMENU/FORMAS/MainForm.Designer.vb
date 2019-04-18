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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.FechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.PaisLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SucursalLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProcesarButton = New System.Windows.Forms.Button()
        Me.FechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RBMovimientos = New System.Windows.Forms.RadioButton()
        Me.RBLiquidaciones = New System.Windows.Forms.RadioButton()
        Me.RBFacturacion = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkTodos = New System.Windows.Forms.CheckBox()
        Me.cboSucursales = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.StatusTextBox = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(201, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 14)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Hasta:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 14)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Desde:"
        '
        'FechaDesde
        '
        Me.FechaDesde.CustomFormat = "ddd dd-MMM-yyyy"
        Me.FechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaDesde.Location = New System.Drawing.Point(55, 45)
        Me.FechaDesde.Name = "FechaDesde"
        Me.FechaDesde.Size = New System.Drawing.Size(127, 21)
        Me.FechaDesde.TabIndex = 10
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = False
        Me.StatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.StatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.StatusLabel.Size = New System.Drawing.Size(300, 19)
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.PaisLabel, Me.SucursalLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 484)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(735, 24)
        Me.StatusStrip1.TabIndex = 21
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'PaisLabel
        '
        Me.PaisLabel.AutoSize = False
        Me.PaisLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.PaisLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.PaisLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.PaisLabel.Name = "PaisLabel"
        Me.PaisLabel.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.PaisLabel.Size = New System.Drawing.Size(110, 19)
        Me.PaisLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SucursalLabel
        '
        Me.SucursalLabel.AutoSize = False
        Me.SucursalLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.SucursalLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.SucursalLabel.Name = "SucursalLabel"
        Me.SucursalLabel.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.SucursalLabel.Size = New System.Drawing.Size(310, 19)
        Me.SucursalLabel.Spring = True
        Me.SucursalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProcesarButton
        '
        Me.ProcesarButton.Location = New System.Drawing.Point(523, 346)
        Me.ProcesarButton.Name = "ProcesarButton"
        Me.ProcesarButton.Size = New System.Drawing.Size(164, 29)
        Me.ProcesarButton.TabIndex = 18
        Me.ProcesarButton.Text = "Iniciar carga"
        Me.ProcesarButton.UseVisualStyleBackColor = True
        '
        'FechaHasta
        '
        Me.FechaHasta.CustomFormat = "ddd dd-MMM-yyyy"
        Me.FechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaHasta.Location = New System.Drawing.Point(249, 45)
        Me.FechaHasta.Name = "FechaHasta"
        Me.FechaHasta.Size = New System.Drawing.Size(127, 21)
        Me.FechaHasta.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(12, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(364, 19)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Carga de información de movimientos de productos"
        '
        'RBMovimientos
        '
        Me.RBMovimientos.AutoSize = True
        Me.RBMovimientos.Enabled = False
        Me.RBMovimientos.Location = New System.Drawing.Point(34, 111)
        Me.RBMovimientos.Name = "RBMovimientos"
        Me.RBMovimientos.Size = New System.Drawing.Size(186, 17)
        Me.RBMovimientos.TabIndex = 30
        Me.RBMovimientos.Text = "Cargar Movimientos de Productos"
        Me.RBMovimientos.UseVisualStyleBackColor = True
        '
        'RBLiquidaciones
        '
        Me.RBLiquidaciones.AutoSize = True
        Me.RBLiquidaciones.Enabled = False
        Me.RBLiquidaciones.Location = New System.Drawing.Point(34, 128)
        Me.RBLiquidaciones.Name = "RBLiquidaciones"
        Me.RBLiquidaciones.Size = New System.Drawing.Size(255, 17)
        Me.RBLiquidaciones.TabIndex = 30
        Me.RBLiquidaciones.Text = "Cargar Movimientos de Liquidaciones e Ingresos"
        Me.RBLiquidaciones.UseVisualStyleBackColor = True
        '
        'RBFacturacion
        '
        Me.RBFacturacion.AutoSize = True
        Me.RBFacturacion.Enabled = False
        Me.RBFacturacion.Location = New System.Drawing.Point(34, 145)
        Me.RBFacturacion.Name = "RBFacturacion"
        Me.RBFacturacion.Size = New System.Drawing.Size(194, 17)
        Me.RBFacturacion.TabIndex = 30
        Me.RBFacturacion.Text = "Cargar Movimientos de Facturacion"
        Me.RBFacturacion.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 14)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Sucursal:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(21, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(259, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Puede elegir una sucursal o <Todas> las sucursales."
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.chkTodos)
        Me.Panel1.Controls.Add(Me.RBFacturacion)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.RBMovimientos)
        Me.Panel1.Controls.Add(Me.cboSucursales)
        Me.Panel1.Controls.Add(Me.RBLiquidaciones)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(9, 79)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(382, 193)
        Me.Panel1.TabIndex = 31
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(21, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(125, 15)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Carga de sucursales"
        '
        'chkTodos
        '
        Me.chkTodos.AutoSize = True
        Me.chkTodos.Checked = True
        Me.chkTodos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTodos.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTodos.ForeColor = System.Drawing.Color.Blue
        Me.chkTodos.Location = New System.Drawing.Point(20, 88)
        Me.chkTodos.Name = "chkTodos"
        Me.chkTodos.Size = New System.Drawing.Size(161, 17)
        Me.chkTodos.TabIndex = 31
        Me.chkTodos.Text = "Cargar todos los movimientos"
        Me.chkTodos.UseVisualStyleBackColor = True
        '
        'cboSucursales
        '
        Me.cboSucursales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSucursales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSucursales.DataBindings.Add(New System.Windows.Forms.Binding("Name", Global.ISAPMenu.My.MySettings.Default, "SucursalClave", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.cboSucursales.FormattingEnabled = True
        Me.cboSucursales.Location = New System.Drawing.Point(20, 62)
        Me.cboSucursales.Name = Global.ISAPMenu.My.MySettings.Default.SucursalClave
        Me.cboSucursales.Size = New System.Drawing.Size(181, 21)
        Me.cboSucursales.Sorted = True
        Me.cboSucursales.TabIndex = 28
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Controls.Add(Me.RadioButton3)
        Me.Panel2.Controls.Add(Me.RadioButton1)
        Me.Panel2.Controls.Add(Me.RadioButton2)
        Me.Panel2.Location = New System.Drawing.Point(9, 278)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(382, 122)
        Me.Panel2.TabIndex = 32
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(17, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(95, 15)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Cargas de pais:"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.ForeColor = System.Drawing.Color.Blue
        Me.CheckBox1.Location = New System.Drawing.Point(24, 32)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(161, 17)
        Me.CheckBox1.TabIndex = 31
        Me.CheckBox1.Text = "Cargar todos los movimientos"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Enabled = False
        Me.RadioButton3.Location = New System.Drawing.Point(34, 89)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(283, 17)
        Me.RadioButton3.TabIndex = 30
        Me.RadioButton3.Text = "Cargar Movimientos de Facturas Cartera Centralizada"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Enabled = False
        Me.RadioButton1.Location = New System.Drawing.Point(34, 72)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(223, 17)
        Me.RadioButton1.TabIndex = 30
        Me.RadioButton1.Text = "Cargar Movimientos de Costo de la venta"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Enabled = False
        Me.RadioButton2.Location = New System.Drawing.Point(34, 55)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(229, 17)
        Me.RadioButton2.TabIndex = 30
        Me.RadioButton2.Text = "Cargar Movimientos de cuentas por cobrar"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'StatusTextBox
        '
        Me.StatusTextBox.Location = New System.Drawing.Point(397, 185)
        Me.StatusTextBox.Multiline = True
        Me.StatusTextBox.Name = "StatusTextBox"
        Me.StatusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.StatusTextBox.Size = New System.Drawing.Size(290, 155)
        Me.StatusTextBox.TabIndex = 33
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.ISAPMenu.My.Resources.Resources.log_bis
        Me.PictureBox2.Location = New System.Drawing.Point(406, 381)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(281, 88)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 34
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ISAPMenu.My.Resources.Resources.CARICAM_Oficial_calado
        Me.PictureBox1.Location = New System.Drawing.Point(397, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(297, 166)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 24
        Me.PictureBox1.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 508)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.StatusTextBox)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.FechaDesde)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ProcesarButton)
        Me.Controls.Add(Me.FechaHasta)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Movimientos de productos"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents FechaDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ProcesarButton As System.Windows.Forms.Button
    Friend WithEvents FechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents RBMovimientos As System.Windows.Forms.RadioButton
    Friend WithEvents RBLiquidaciones As System.Windows.Forms.RadioButton
    Friend WithEvents RBFacturacion As System.Windows.Forms.RadioButton
    Friend WithEvents cboSucursales As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkTodos As System.Windows.Forms.CheckBox
    Friend WithEvents StatusTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PaisLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SucursalLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox

End Class
