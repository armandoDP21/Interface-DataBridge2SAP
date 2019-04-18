<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportEmbarquesForm
    Inherits ComponentFactory.Krypton.Toolkit.KryptonForm

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
        Me.KryptonPanel = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.GridDatos = New ComponentFactory.Krypton.Toolkit.KryptonDataGridView()
        Me.KryptonPanel1 = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FiscalYear = New System.Windows.Forms.TextBox()
        Me.Periodo = New System.Windows.Forms.TextBox()
        Me.CerrarButton = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.ImportarButton = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel.SuspendLayout()
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KryptonPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'KryptonPanel
        '
        Me.KryptonPanel.Controls.Add(Me.GridDatos)
        Me.KryptonPanel.Controls.Add(Me.KryptonPanel1)
        Me.KryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonPanel.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel.Name = "KryptonPanel"
        Me.KryptonPanel.Size = New System.Drawing.Size(649, 424)
        Me.KryptonPanel.TabIndex = 0
        '
        'GridDatos
        '
        Me.GridDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDatos.Location = New System.Drawing.Point(0, 33)
        Me.GridDatos.Name = "GridDatos"
        Me.GridDatos.Size = New System.Drawing.Size(649, 391)
        Me.GridDatos.TabIndex = 2
        '
        'KryptonPanel1
        '
        Me.KryptonPanel1.Controls.Add(Me.Label2)
        Me.KryptonPanel1.Controls.Add(Me.Label1)
        Me.KryptonPanel1.Controls.Add(Me.FiscalYear)
        Me.KryptonPanel1.Controls.Add(Me.Periodo)
        Me.KryptonPanel1.Controls.Add(Me.CerrarButton)
        Me.KryptonPanel1.Controls.Add(Me.ImportarButton)
        Me.KryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.KryptonPanel1.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel1.Name = "KryptonPanel1"
        Me.KryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridHeaderRowCustom1
        Me.KryptonPanel1.Size = New System.Drawing.Size(649, 33)
        Me.KryptonPanel1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(114, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Año:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Periodo:"
        '
        'FiscalYear
        '
        Me.FiscalYear.Location = New System.Drawing.Point(149, 6)
        Me.FiscalYear.Name = "FiscalYear"
        Me.FiscalYear.Size = New System.Drawing.Size(71, 21)
        Me.FiscalYear.TabIndex = 2
        '
        'Periodo
        '
        Me.Periodo.Location = New System.Drawing.Point(65, 6)
        Me.Periodo.Name = "Periodo"
        Me.Periodo.Size = New System.Drawing.Size(34, 21)
        Me.Periodo.TabIndex = 2
        '
        'CerrarButton
        '
        Me.CerrarButton.Location = New System.Drawing.Point(547, 3)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(90, 25)
        Me.CerrarButton.TabIndex = 1
        Me.CerrarButton.Values.Text = "Cerrar"
        '
        'ImportarButton
        '
        Me.ImportarButton.Location = New System.Drawing.Point(451, 3)
        Me.ImportarButton.Name = "ImportarButton"
        Me.ImportarButton.Size = New System.Drawing.Size(90, 25)
        Me.ImportarButton.TabIndex = 1
        Me.ImportarButton.Values.Text = "Importar"
        '
        'ImportEmbarquesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 424)
        Me.Controls.Add(Me.KryptonPanel)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ImportEmbarquesForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Importar Movimientos"
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel.ResumeLayout(False)
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KryptonPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel1.ResumeLayout(False)
        Me.KryptonPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents KryptonPanel As ComponentFactory.Krypton.Toolkit.KryptonPanel

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents KryptonPanel1 As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents ImportarButton As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents CerrarButton As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FiscalYear As System.Windows.Forms.TextBox
    Friend WithEvents Periodo As System.Windows.Forms.TextBox
    Friend WithEvents GridDatos As ComponentFactory.Krypton.Toolkit.KryptonDataGridView
End Class
