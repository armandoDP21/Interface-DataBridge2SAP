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
        Me.VerButton = New System.Windows.Forms.Button()
        Me.ProcesarButton = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.FechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.FechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.NombreUsuario = New System.Windows.Forms.ToolStripStatusLabel()
        Me.NombrePais = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ClaveCia_Label = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Moneda = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GridDatos = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GridDetalles = New System.Windows.Forms.DataGridView()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.GridDetalles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VerButton
        '
        Me.VerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VerButton.Location = New System.Drawing.Point(288, 3)
        Me.VerButton.Name = "VerButton"
        Me.VerButton.Size = New System.Drawing.Size(140, 23)
        Me.VerButton.TabIndex = 13
        Me.VerButton.Text = "Guardar inventario"
        Me.VerButton.UseVisualStyleBackColor = True
        '
        'ProcesarButton
        '
        Me.ProcesarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProcesarButton.Enabled = False
        Me.ProcesarButton.Location = New System.Drawing.Point(742, 4)
        Me.ProcesarButton.Name = "ProcesarButton"
        Me.ProcesarButton.Size = New System.Drawing.Size(99, 23)
        Me.ProcesarButton.TabIndex = 14
        Me.ProcesarButton.Text = "Cargar registros"
        Me.ProcesarButton.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(184, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Hasta:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Desde:"
        '
        'FechaHasta
        '
        Me.FechaHasta.CustomFormat = "ddd dd-MMM-yyyy"
        Me.FechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaHasta.Location = New System.Drawing.Point(232, 3)
        Me.FechaHasta.Name = "FechaHasta"
        Me.FechaHasta.Size = New System.Drawing.Size(120, 21)
        Me.FechaHasta.TabIndex = 4
        '
        'FechaDesde
        '
        Me.FechaDesde.CustomFormat = "ddd dd-MMM-yyyy"
        Me.FechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaDesde.Location = New System.Drawing.Point(57, 3)
        Me.FechaDesde.Name = "FechaDesde"
        Me.FechaDesde.Size = New System.Drawing.Size(120, 21)
        Me.FechaDesde.TabIndex = 5
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NombreUsuario, Me.NombrePais, Me.ToolStripStatusLabel1, Me.StatusLabel, Me.ClaveCia_Label, Me.Moneda})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 586)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(844, 22)
        Me.StatusStrip1.TabIndex = 16
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'NombreUsuario
        '
        Me.NombreUsuario.AutoSize = False
        Me.NombreUsuario.Name = "NombreUsuario"
        Me.NombreUsuario.Size = New System.Drawing.Size(130, 17)
        '
        'NombrePais
        '
        Me.NombrePais.AutoSize = False
        Me.NombrePais.Name = "NombrePais"
        Me.NombrePais.Size = New System.Drawing.Size(110, 17)
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(0, 17)
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ClaveCia_Label
        '
        Me.ClaveCia_Label.AutoSize = False
        Me.ClaveCia_Label.Name = "ClaveCia_Label"
        Me.ClaveCia_Label.Size = New System.Drawing.Size(45, 17)
        Me.ClaveCia_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Moneda
        '
        Me.Moneda.AutoSize = False
        Me.Moneda.Name = "Moneda"
        Me.Moneda.Size = New System.Drawing.Size(120, 17)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.FechaDesde)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.FechaHasta)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.ProcesarButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(844, 33)
        Me.Panel1.TabIndex = 20
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 33)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GridDatos)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GridDetalles)
        Me.SplitContainer1.Size = New System.Drawing.Size(844, 553)
        Me.SplitContainer1.SplitterDistance = 400
        Me.SplitContainer1.TabIndex = 21
        '
        'GridDatos
        '
        Me.GridDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDatos.Location = New System.Drawing.Point(0, 0)
        Me.GridDatos.Name = "GridDatos"
        Me.GridDatos.Size = New System.Drawing.Size(400, 553)
        Me.GridDatos.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.VerButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 524)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(440, 29)
        Me.Panel2.TabIndex = 1
        '
        'GridDetalles
        '
        Me.GridDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridDetalles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDetalles.Location = New System.Drawing.Point(0, 0)
        Me.GridDetalles.Name = "GridDetalles"
        Me.GridDetalles.Size = New System.Drawing.Size(440, 553)
        Me.GridDetalles.TabIndex = 0
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 608)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IIT - Interfase de Inventario en transito"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.GridDetalles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VerButton As System.Windows.Forms.Button
    Friend WithEvents ProcesarButton As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents FechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents FechaDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ClaveCia_Label As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Moneda As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GridDatos As System.Windows.Forms.DataGridView
    Friend WithEvents GridDetalles As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents NombreUsuario As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents NombrePais As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel

End Class
