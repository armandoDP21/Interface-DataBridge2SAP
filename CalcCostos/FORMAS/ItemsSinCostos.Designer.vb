<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemsSinCostos
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
        Me.components = New System.ComponentModel.Container()
        Me.KryptonPanel = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.KryptonSplitContainer1 = New ComponentFactory.Krypton.Toolkit.KryptonSplitContainer()
        Me.KryptonLabel3 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.adpPalette = New ComponentFactory.Krypton.Toolkit.KryptonPalette(Me.components)
        Me.GridDatos = New ComponentFactory.Krypton.Toolkit.KryptonDataGridView()
        Me.KryptonLabel4 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.GridItemsCostoCero = New ComponentFactory.Krypton.Toolkit.KryptonDataGridView()
        Me.KryptonPanel2 = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.GuardarButton = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.CerrarButton = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.KryptonPanel1 = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.KryptonLabel2 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel1 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.FiscalYear = New ComponentFactory.Krypton.Toolkit.KryptonTextBox()
        Me.PeriodCB = New ComponentFactory.Krypton.Toolkit.KryptonComboBox()
        Me.CargarButton = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel.SuspendLayout()
        CType(Me.KryptonSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonSplitContainer1.Panel1.SuspendLayout()
        Me.KryptonSplitContainer1.Panel2.SuspendLayout()
        Me.KryptonSplitContainer1.SuspendLayout()
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridItemsCostoCero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KryptonPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel2.SuspendLayout()
        CType(Me.KryptonPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel1.SuspendLayout()
        CType(Me.PeriodCB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'KryptonPanel
        '
        Me.KryptonPanel.Controls.Add(Me.KryptonSplitContainer1)
        Me.KryptonPanel.Controls.Add(Me.KryptonPanel2)
        Me.KryptonPanel.Controls.Add(Me.KryptonPanel1)
        Me.KryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonPanel.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel.Name = "KryptonPanel"
        Me.KryptonPanel.Size = New System.Drawing.Size(728, 613)
        Me.KryptonPanel.TabIndex = 0
        '
        'KryptonSplitContainer1
        '
        Me.KryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.KryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonSplitContainer1.Location = New System.Drawing.Point(0, 40)
        Me.KryptonSplitContainer1.Name = "KryptonSplitContainer1"
        Me.KryptonSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'KryptonSplitContainer1.Panel1
        '
        Me.KryptonSplitContainer1.Panel1.Controls.Add(Me.KryptonLabel3)
        Me.KryptonSplitContainer1.Panel1.Controls.Add(Me.GridDatos)
        '
        'KryptonSplitContainer1.Panel2
        '
        Me.KryptonSplitContainer1.Panel2.Controls.Add(Me.KryptonLabel4)
        Me.KryptonSplitContainer1.Panel2.Controls.Add(Me.GridItemsCostoCero)
        Me.KryptonSplitContainer1.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile
        Me.KryptonSplitContainer1.Size = New System.Drawing.Size(728, 538)
        Me.KryptonSplitContainer1.SplitterDistance = 293
        Me.KryptonSplitContainer1.TabIndex = 4
        '
        'KryptonLabel3
        '
        Me.KryptonLabel3.AutoSize = False
        Me.KryptonLabel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.KryptonLabel3.Location = New System.Drawing.Point(0, 0)
        Me.KryptonLabel3.Name = "KryptonLabel3"
        Me.KryptonLabel3.Palette = Me.adpPalette
        Me.KryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel3.Size = New System.Drawing.Size(728, 19)
        Me.KryptonLabel3.TabIndex = 3
        Me.KryptonLabel3.Values.Text = "Items que no existen"
        '
        'adpPalette
        '
        Me.adpPalette.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue
        Me.adpPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Color1 = System.Drawing.Color.White
        Me.adpPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Color2 = System.Drawing.Color.White
        Me.adpPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adpPalette.LabelStyles.LabelCustom1.StateCommon.ShortText.Color1 = System.Drawing.Color.White
        Me.adpPalette.LabelStyles.LabelCustom1.StateCommon.ShortText.Color2 = System.Drawing.Color.White
        Me.adpPalette.LabelStyles.LabelCustom1.StateCommon.ShortText.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'GridDatos
        '
        Me.GridDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDatos.Location = New System.Drawing.Point(0, 0)
        Me.GridDatos.Name = "GridDatos"
        Me.GridDatos.Size = New System.Drawing.Size(728, 293)
        Me.GridDatos.TabIndex = 2
        '
        'KryptonLabel4
        '
        Me.KryptonLabel4.AutoSize = False
        Me.KryptonLabel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.KryptonLabel4.Location = New System.Drawing.Point(0, 0)
        Me.KryptonLabel4.Name = "KryptonLabel4"
        Me.KryptonLabel4.Palette = Me.adpPalette
        Me.KryptonLabel4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel4.Size = New System.Drawing.Size(728, 19)
        Me.KryptonLabel4.TabIndex = 4
        Me.KryptonLabel4.Values.Text = "Items que tienen costo cero {0.00}"
        '
        'GridItemsCostoCero
        '
        Me.GridItemsCostoCero.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridItemsCostoCero.Location = New System.Drawing.Point(0, 0)
        Me.GridItemsCostoCero.Name = "GridItemsCostoCero"
        Me.GridItemsCostoCero.Size = New System.Drawing.Size(728, 240)
        Me.GridItemsCostoCero.TabIndex = 3
        '
        'KryptonPanel2
        '
        Me.KryptonPanel2.Controls.Add(Me.GuardarButton)
        Me.KryptonPanel2.Controls.Add(Me.CerrarButton)
        Me.KryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.KryptonPanel2.Location = New System.Drawing.Point(0, 578)
        Me.KryptonPanel2.Name = "KryptonPanel2"
        Me.KryptonPanel2.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderCalendar
        Me.KryptonPanel2.Size = New System.Drawing.Size(728, 35)
        Me.KryptonPanel2.TabIndex = 1
        '
        'GuardarButton
        '
        Me.GuardarButton.Location = New System.Drawing.Point(295, 5)
        Me.GuardarButton.Name = "GuardarButton"
        Me.GuardarButton.Size = New System.Drawing.Size(132, 27)
        Me.GuardarButton.TabIndex = 0
        Me.GuardarButton.Values.Text = "Guardar cambios"
        '
        'CerrarButton
        '
        Me.CerrarButton.Location = New System.Drawing.Point(433, 5)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(132, 27)
        Me.CerrarButton.TabIndex = 0
        Me.CerrarButton.Values.Text = "Salir"
        '
        'KryptonPanel1
        '
        Me.KryptonPanel1.Controls.Add(Me.KryptonLabel2)
        Me.KryptonPanel1.Controls.Add(Me.KryptonLabel1)
        Me.KryptonPanel1.Controls.Add(Me.FiscalYear)
        Me.KryptonPanel1.Controls.Add(Me.PeriodCB)
        Me.KryptonPanel1.Controls.Add(Me.CargarButton)
        Me.KryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.KryptonPanel1.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel1.Name = "KryptonPanel1"
        Me.KryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderCalendar
        Me.KryptonPanel1.Size = New System.Drawing.Size(728, 40)
        Me.KryptonPanel1.TabIndex = 0
        '
        'KryptonLabel2
        '
        Me.KryptonLabel2.Location = New System.Drawing.Point(21, 13)
        Me.KryptonLabel2.Name = "KryptonLabel2"
        Me.KryptonLabel2.Palette = Me.adpPalette
        Me.KryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel2.Size = New System.Drawing.Size(36, 16)
        Me.KryptonLabel2.TabIndex = 3
        Me.KryptonLabel2.Values.Text = "Año:"
        '
        'KryptonLabel1
        '
        Me.KryptonLabel1.Location = New System.Drawing.Point(122, 13)
        Me.KryptonLabel1.Name = "KryptonLabel1"
        Me.KryptonLabel1.Palette = Me.adpPalette
        Me.KryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel1.Size = New System.Drawing.Size(56, 16)
        Me.KryptonLabel1.TabIndex = 3
        Me.KryptonLabel1.Values.Text = "Periodo:"
        '
        'FiscalYear
        '
        Me.FiscalYear.Location = New System.Drawing.Point(63, 10)
        Me.FiscalYear.Name = "FiscalYear"
        Me.FiscalYear.Size = New System.Drawing.Size(53, 22)
        Me.FiscalYear.TabIndex = 2
        '
        'PeriodCB
        '
        Me.PeriodCB.DropDownWidth = 121
        Me.PeriodCB.Items.AddRange(New Object() {"001 Enero", "002 Febrero", "003 Marzo", "004 Abril", "005 Mayo", "006 Junio", "007 Julio", "008 Agosto", "009 Setiembre", "010 Octubre", "011 Noviembre", "012 Diciembre"})
        Me.PeriodCB.Location = New System.Drawing.Point(184, 10)
        Me.PeriodCB.Name = "PeriodCB"
        Me.PeriodCB.Size = New System.Drawing.Size(180, 22)
        Me.PeriodCB.TabIndex = 1
        '
        'CargarButton
        '
        Me.CargarButton.Location = New System.Drawing.Point(584, 6)
        Me.CargarButton.Name = "CargarButton"
        Me.CargarButton.Size = New System.Drawing.Size(132, 26)
        Me.CargarButton.TabIndex = 0
        Me.CargarButton.Values.Text = "CargarDatos"
        '
        'ItemsSinCostos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 613)
        Me.Controls.Add(Me.KryptonPanel)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ItemsSinCostos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Items Sin Costos"
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel.ResumeLayout(False)
        Me.KryptonSplitContainer1.Panel1.ResumeLayout(False)
        Me.KryptonSplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.KryptonSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonSplitContainer1.ResumeLayout(False)
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridItemsCostoCero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KryptonPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel2.ResumeLayout(False)
        CType(Me.KryptonPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel1.ResumeLayout(False)
        Me.KryptonPanel1.PerformLayout()
        CType(Me.PeriodCB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents KryptonPanel As ComponentFactory.Krypton.Toolkit.KryptonPanel

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ConfigurarGridNoExisten()
        ConfigurarGridCostoCero()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents KryptonPanel1 As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents KryptonPanel2 As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents GuardarButton As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents CerrarButton As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents GridDatos As ComponentFactory.Krypton.Toolkit.KryptonDataGridView
    Friend WithEvents CargarButton As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents KryptonSplitContainer1 As ComponentFactory.Krypton.Toolkit.KryptonSplitContainer
    Friend WithEvents GridItemsCostoCero As ComponentFactory.Krypton.Toolkit.KryptonDataGridView
    Friend WithEvents FiscalYear As ComponentFactory.Krypton.Toolkit.KryptonTextBox
    Friend WithEvents PeriodCB As ComponentFactory.Krypton.Toolkit.KryptonComboBox
    Friend WithEvents KryptonLabel2 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel1 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel3 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents adpPalette As ComponentFactory.Krypton.Toolkit.KryptonPalette
    Friend WithEvents KryptonLabel4 As ComponentFactory.Krypton.Toolkit.KryptonLabel
End Class
