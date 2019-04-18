<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VerDatosForm
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
        Me.TabDatos = New System.Windows.Forms.TabControl()
        Me.Calendario = New System.Windows.Forms.TabPage()
        Me.Grid_Calendario = New System.Windows.Forms.DataGridView()
        Me.INTERFASE_COSTOS = New System.Windows.Forms.TabPage()
        Me.Grid_INTERFASECOSTOS = New System.Windows.Forms.DataGridView()
        Me.CATALOGO_MOVIMIENTOSPROD = New System.Windows.Forms.TabPage()
        Me.Grid_CATALOGO_MOVIMIENTOSPROD = New System.Windows.Forms.DataGridView()
        Me.MOVIMIENTOPRODUCTO = New System.Windows.Forms.TabPage()
        Me.Grid_MOVIMIENTOPRODUCTO = New System.Windows.Forms.DataGridView()
        Me.Sucursales = New System.Windows.Forms.TabPage()
        Me.Grid_Sucursales = New System.Windows.Forms.DataGridView()
        Me.Segregado = New System.Windows.Forms.TabPage()
        Me.Grid_Segregado = New System.Windows.Forms.DataGridView()
        Me.Agrupado = New System.Windows.Forms.TabPage()
        Me.Grid_Agrupado = New System.Windows.Forms.DataGridView()
        Me.SAP_GL_INTERFASE = New System.Windows.Forms.TabPage()
        Me.GRID_SAP_GL_INTERFASE = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TotalMovimientosLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CerrarButton = New System.Windows.Forms.Button()
        Me.TabDatos.SuspendLayout()
        Me.Calendario.SuspendLayout()
        CType(Me.Grid_Calendario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INTERFASE_COSTOS.SuspendLayout()
        CType(Me.Grid_INTERFASECOSTOS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CATALOGO_MOVIMIENTOSPROD.SuspendLayout()
        CType(Me.Grid_CATALOGO_MOVIMIENTOSPROD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MOVIMIENTOPRODUCTO.SuspendLayout()
        CType(Me.Grid_MOVIMIENTOPRODUCTO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Sucursales.SuspendLayout()
        CType(Me.Grid_Sucursales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Segregado.SuspendLayout()
        CType(Me.Grid_Segregado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Agrupado.SuspendLayout()
        CType(Me.Grid_Agrupado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SAP_GL_INTERFASE.SuspendLayout()
        CType(Me.GRID_SAP_GL_INTERFASE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabDatos
        '
        Me.TabDatos.Controls.Add(Me.Calendario)
        Me.TabDatos.Controls.Add(Me.INTERFASE_COSTOS)
        Me.TabDatos.Controls.Add(Me.CATALOGO_MOVIMIENTOSPROD)
        Me.TabDatos.Controls.Add(Me.MOVIMIENTOPRODUCTO)
        Me.TabDatos.Controls.Add(Me.Sucursales)
        Me.TabDatos.Controls.Add(Me.Segregado)
        Me.TabDatos.Controls.Add(Me.Agrupado)
        Me.TabDatos.Controls.Add(Me.SAP_GL_INTERFASE)
        Me.TabDatos.Location = New System.Drawing.Point(0, 0)
        Me.TabDatos.Name = "TabDatos"
        Me.TabDatos.SelectedIndex = 0
        Me.TabDatos.Size = New System.Drawing.Size(854, 564)
        Me.TabDatos.TabIndex = 0
        '
        'Calendario
        '
        Me.Calendario.Controls.Add(Me.Grid_Calendario)
        Me.Calendario.Location = New System.Drawing.Point(4, 22)
        Me.Calendario.Name = "Calendario"
        Me.Calendario.Padding = New System.Windows.Forms.Padding(3)
        Me.Calendario.Size = New System.Drawing.Size(846, 538)
        Me.Calendario.TabIndex = 7
        Me.Calendario.Text = "Calendario"
        Me.Calendario.UseVisualStyleBackColor = True
        '
        'Grid_Calendario
        '
        Me.Grid_Calendario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_Calendario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_Calendario.Location = New System.Drawing.Point(3, 3)
        Me.Grid_Calendario.Name = "Grid_Calendario"
        Me.Grid_Calendario.Size = New System.Drawing.Size(840, 532)
        Me.Grid_Calendario.TabIndex = 3
        '
        'INTERFASE_COSTOS
        '
        Me.INTERFASE_COSTOS.Controls.Add(Me.Grid_INTERFASECOSTOS)
        Me.INTERFASE_COSTOS.Location = New System.Drawing.Point(4, 22)
        Me.INTERFASE_COSTOS.Name = "INTERFASE_COSTOS"
        Me.INTERFASE_COSTOS.Padding = New System.Windows.Forms.Padding(3)
        Me.INTERFASE_COSTOS.Size = New System.Drawing.Size(846, 538)
        Me.INTERFASE_COSTOS.TabIndex = 3
        Me.INTERFASE_COSTOS.Text = "INTERFASE_COSTOS"
        Me.INTERFASE_COSTOS.UseVisualStyleBackColor = True
        '
        'Grid_INTERFASECOSTOS
        '
        Me.Grid_INTERFASECOSTOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_INTERFASECOSTOS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_INTERFASECOSTOS.Location = New System.Drawing.Point(3, 3)
        Me.Grid_INTERFASECOSTOS.Name = "Grid_INTERFASECOSTOS"
        Me.Grid_INTERFASECOSTOS.Size = New System.Drawing.Size(840, 532)
        Me.Grid_INTERFASECOSTOS.TabIndex = 2
        '
        'CATALOGO_MOVIMIENTOSPROD
        '
        Me.CATALOGO_MOVIMIENTOSPROD.Controls.Add(Me.Grid_CATALOGO_MOVIMIENTOSPROD)
        Me.CATALOGO_MOVIMIENTOSPROD.Location = New System.Drawing.Point(4, 22)
        Me.CATALOGO_MOVIMIENTOSPROD.Name = "CATALOGO_MOVIMIENTOSPROD"
        Me.CATALOGO_MOVIMIENTOSPROD.Padding = New System.Windows.Forms.Padding(3)
        Me.CATALOGO_MOVIMIENTOSPROD.Size = New System.Drawing.Size(846, 538)
        Me.CATALOGO_MOVIMIENTOSPROD.TabIndex = 0
        Me.CATALOGO_MOVIMIENTOSPROD.Text = "CATALOGO_MOVIMIENTOSPROD"
        Me.CATALOGO_MOVIMIENTOSPROD.UseVisualStyleBackColor = True
        '
        'Grid_CATALOGO_MOVIMIENTOSPROD
        '
        Me.Grid_CATALOGO_MOVIMIENTOSPROD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_CATALOGO_MOVIMIENTOSPROD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_CATALOGO_MOVIMIENTOSPROD.Location = New System.Drawing.Point(3, 3)
        Me.Grid_CATALOGO_MOVIMIENTOSPROD.Name = "Grid_CATALOGO_MOVIMIENTOSPROD"
        Me.Grid_CATALOGO_MOVIMIENTOSPROD.Size = New System.Drawing.Size(840, 532)
        Me.Grid_CATALOGO_MOVIMIENTOSPROD.TabIndex = 0
        '
        'MOVIMIENTOPRODUCTO
        '
        Me.MOVIMIENTOPRODUCTO.Controls.Add(Me.Grid_MOVIMIENTOPRODUCTO)
        Me.MOVIMIENTOPRODUCTO.Location = New System.Drawing.Point(4, 22)
        Me.MOVIMIENTOPRODUCTO.Name = "MOVIMIENTOPRODUCTO"
        Me.MOVIMIENTOPRODUCTO.Padding = New System.Windows.Forms.Padding(3)
        Me.MOVIMIENTOPRODUCTO.Size = New System.Drawing.Size(846, 538)
        Me.MOVIMIENTOPRODUCTO.TabIndex = 1
        Me.MOVIMIENTOPRODUCTO.Text = "MOVIMIENTOPRODUCTO"
        Me.MOVIMIENTOPRODUCTO.UseVisualStyleBackColor = True
        '
        'Grid_MOVIMIENTOPRODUCTO
        '
        Me.Grid_MOVIMIENTOPRODUCTO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_MOVIMIENTOPRODUCTO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_MOVIMIENTOPRODUCTO.Location = New System.Drawing.Point(3, 3)
        Me.Grid_MOVIMIENTOPRODUCTO.Name = "Grid_MOVIMIENTOPRODUCTO"
        Me.Grid_MOVIMIENTOPRODUCTO.Size = New System.Drawing.Size(840, 532)
        Me.Grid_MOVIMIENTOPRODUCTO.TabIndex = 0
        '
        'Sucursales
        '
        Me.Sucursales.Controls.Add(Me.Grid_Sucursales)
        Me.Sucursales.Location = New System.Drawing.Point(4, 22)
        Me.Sucursales.Name = "Sucursales"
        Me.Sucursales.Padding = New System.Windows.Forms.Padding(3)
        Me.Sucursales.Size = New System.Drawing.Size(846, 538)
        Me.Sucursales.TabIndex = 2
        Me.Sucursales.Text = "Sucursales Pais"
        Me.Sucursales.UseVisualStyleBackColor = True
        '
        'Grid_Sucursales
        '
        Me.Grid_Sucursales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_Sucursales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_Sucursales.Location = New System.Drawing.Point(3, 3)
        Me.Grid_Sucursales.Name = "Grid_Sucursales"
        Me.Grid_Sucursales.Size = New System.Drawing.Size(840, 532)
        Me.Grid_Sucursales.TabIndex = 1
        '
        'Segregado
        '
        Me.Segregado.Controls.Add(Me.Grid_Segregado)
        Me.Segregado.Location = New System.Drawing.Point(4, 22)
        Me.Segregado.Name = "Segregado"
        Me.Segregado.Padding = New System.Windows.Forms.Padding(3)
        Me.Segregado.Size = New System.Drawing.Size(846, 538)
        Me.Segregado.TabIndex = 4
        Me.Segregado.Text = "Segregado"
        Me.Segregado.UseVisualStyleBackColor = True
        '
        'Grid_Segregado
        '
        Me.Grid_Segregado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_Segregado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_Segregado.Location = New System.Drawing.Point(3, 3)
        Me.Grid_Segregado.Name = "Grid_Segregado"
        Me.Grid_Segregado.Size = New System.Drawing.Size(840, 532)
        Me.Grid_Segregado.TabIndex = 3
        '
        'Agrupado
        '
        Me.Agrupado.Controls.Add(Me.Grid_Agrupado)
        Me.Agrupado.Location = New System.Drawing.Point(4, 22)
        Me.Agrupado.Name = "Agrupado"
        Me.Agrupado.Padding = New System.Windows.Forms.Padding(3)
        Me.Agrupado.Size = New System.Drawing.Size(846, 538)
        Me.Agrupado.TabIndex = 6
        Me.Agrupado.Text = "Agrupado"
        Me.Agrupado.UseVisualStyleBackColor = True
        '
        'Grid_Agrupado
        '
        Me.Grid_Agrupado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_Agrupado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_Agrupado.Location = New System.Drawing.Point(3, 3)
        Me.Grid_Agrupado.Name = "Grid_Agrupado"
        Me.Grid_Agrupado.Size = New System.Drawing.Size(840, 532)
        Me.Grid_Agrupado.TabIndex = 4
        '
        'SAP_GL_INTERFASE
        '
        Me.SAP_GL_INTERFASE.Controls.Add(Me.GRID_SAP_GL_INTERFASE)
        Me.SAP_GL_INTERFASE.Location = New System.Drawing.Point(4, 22)
        Me.SAP_GL_INTERFASE.Name = "SAP_GL_INTERFASE"
        Me.SAP_GL_INTERFASE.Padding = New System.Windows.Forms.Padding(3)
        Me.SAP_GL_INTERFASE.Size = New System.Drawing.Size(846, 538)
        Me.SAP_GL_INTERFASE.TabIndex = 5
        Me.SAP_GL_INTERFASE.Text = "SAP_GL_INTERFASE"
        Me.SAP_GL_INTERFASE.UseVisualStyleBackColor = True
        '
        'GRID_SAP_GL_INTERFASE
        '
        Me.GRID_SAP_GL_INTERFASE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRID_SAP_GL_INTERFASE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GRID_SAP_GL_INTERFASE.Location = New System.Drawing.Point(3, 3)
        Me.GRID_SAP_GL_INTERFASE.Name = "GRID_SAP_GL_INTERFASE"
        Me.GRID_SAP_GL_INTERFASE.Size = New System.Drawing.Size(840, 532)
        Me.GRID_SAP_GL_INTERFASE.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TotalMovimientosLabel)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.CerrarButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 567)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(854, 36)
        Me.Panel1.TabIndex = 2
        '
        'TotalMovimientosLabel
        '
        Me.TotalMovimientosLabel.AutoSize = True
        Me.TotalMovimientosLabel.Location = New System.Drawing.Point(127, 13)
        Me.TotalMovimientosLabel.Name = "TotalMovimientosLabel"
        Me.TotalMovimientosLabel.Size = New System.Drawing.Size(0, 13)
        Me.TotalMovimientosLabel.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "total movimientos:"
        '
        'CerrarButton
        '
        Me.CerrarButton.Location = New System.Drawing.Point(739, 3)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(103, 23)
        Me.CerrarButton.TabIndex = 0
        Me.CerrarButton.Text = "Cerrar"
        Me.CerrarButton.UseVisualStyleBackColor = True
        '
        'VerDatosForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(854, 603)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabDatos)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "VerDatosForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Datos de trabajo"
        Me.TabDatos.ResumeLayout(False)
        Me.Calendario.ResumeLayout(False)
        CType(Me.Grid_Calendario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INTERFASE_COSTOS.ResumeLayout(False)
        CType(Me.Grid_INTERFASECOSTOS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CATALOGO_MOVIMIENTOSPROD.ResumeLayout(False)
        CType(Me.Grid_CATALOGO_MOVIMIENTOSPROD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MOVIMIENTOPRODUCTO.ResumeLayout(False)
        CType(Me.Grid_MOVIMIENTOPRODUCTO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Sucursales.ResumeLayout(False)
        CType(Me.Grid_Sucursales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Segregado.ResumeLayout(False)
        CType(Me.Grid_Segregado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Agrupado.ResumeLayout(False)
        CType(Me.Grid_Agrupado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SAP_GL_INTERFASE.ResumeLayout(False)
        CType(Me.GRID_SAP_GL_INTERFASE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabDatos As System.Windows.Forms.TabControl
    Friend WithEvents CATALOGO_MOVIMIENTOSPROD As System.Windows.Forms.TabPage
    Friend WithEvents MOVIMIENTOPRODUCTO As System.Windows.Forms.TabPage
    Friend WithEvents Grid_CATALOGO_MOVIMIENTOSPROD As System.Windows.Forms.DataGridView
    Friend WithEvents Grid_MOVIMIENTOPRODUCTO As System.Windows.Forms.DataGridView
    Friend WithEvents Sucursales As System.Windows.Forms.TabPage
    Friend WithEvents Grid_Sucursales As System.Windows.Forms.DataGridView
    Friend WithEvents INTERFASE_COSTOS As System.Windows.Forms.TabPage
    Friend WithEvents Grid_INTERFASECOSTOS As System.Windows.Forms.DataGridView
    Friend WithEvents Segregado As System.Windows.Forms.TabPage
    Friend WithEvents Grid_Segregado As System.Windows.Forms.DataGridView
    Friend WithEvents SAP_GL_INTERFASE As System.Windows.Forms.TabPage
    Friend WithEvents GRID_SAP_GL_INTERFASE As System.Windows.Forms.DataGridView
    Friend WithEvents Agrupado As System.Windows.Forms.TabPage
    Friend WithEvents Grid_Agrupado As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CerrarButton As System.Windows.Forms.Button
    Friend WithEvents TotalMovimientosLabel As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Calendario As System.Windows.Forms.TabPage
    Friend WithEvents Grid_Calendario As System.Windows.Forms.DataGridView
End Class
