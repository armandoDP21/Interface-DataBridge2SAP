<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmbarqueDetalle
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.PanelAviso = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.KILOSTotal = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.adpPalette = New ComponentFactory.Krypton.Toolkit.KryptonPalette(Me.components)
        Me.KryptonLabel3 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.FOBTotal = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.AvisoLabel = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel1 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonPanel2 = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.GridDatos = New ComponentFactory.Krypton.Toolkit.KryptonDataGridView()
        CType(Me.PanelAviso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelAviso.SuspendLayout()
        CType(Me.KryptonPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel2.SuspendLayout()
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelAviso
        '
        Me.PanelAviso.Controls.Add(Me.KILOSTotal)
        Me.PanelAviso.Controls.Add(Me.KryptonLabel3)
        Me.PanelAviso.Controls.Add(Me.FOBTotal)
        Me.PanelAviso.Controls.Add(Me.AvisoLabel)
        Me.PanelAviso.Controls.Add(Me.KryptonLabel1)
        Me.PanelAviso.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelAviso.Location = New System.Drawing.Point(0, 0)
        Me.PanelAviso.Name = "PanelAviso"
        Me.PanelAviso.Size = New System.Drawing.Size(683, 27)
        Me.PanelAviso.TabIndex = 1
        '
        'KILOSTotal
        '
        Me.KILOSTotal.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.Custom1
        Me.KILOSTotal.Location = New System.Drawing.Point(641, 5)
        Me.KILOSTotal.Name = "KILOSTotal"
        Me.KILOSTotal.Palette = Me.adpPalette
        Me.KILOSTotal.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KILOSTotal.Size = New System.Drawing.Size(16, 17)
        Me.KILOSTotal.TabIndex = 0
        Me.KILOSTotal.Values.Text = "0"
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
        'KryptonLabel3
        '
        Me.KryptonLabel3.Location = New System.Drawing.Point(563, 5)
        Me.KryptonLabel3.Name = "KryptonLabel3"
        Me.KryptonLabel3.Palette = Me.adpPalette
        Me.KryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel3.Size = New System.Drawing.Size(72, 16)
        Me.KryptonLabel3.TabIndex = 0
        Me.KryptonLabel3.Values.Text = "Total Kilos:"
        '
        'FOBTotal
        '
        Me.FOBTotal.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.Custom1
        Me.FOBTotal.Location = New System.Drawing.Point(490, 5)
        Me.FOBTotal.Name = "FOBTotal"
        Me.FOBTotal.Palette = Me.adpPalette
        Me.FOBTotal.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.FOBTotal.Size = New System.Drawing.Size(16, 17)
        Me.FOBTotal.TabIndex = 0
        Me.FOBTotal.Values.Text = "0"
        '
        'AvisoLabel
        '
        Me.AvisoLabel.Location = New System.Drawing.Point(3, 5)
        Me.AvisoLabel.Name = "AvisoLabel"
        Me.AvisoLabel.Size = New System.Drawing.Size(22, 18)
        Me.AvisoLabel.TabIndex = 0
        Me.AvisoLabel.Values.Image = Global.CalcCostos.My.Resources.Resources.RecordHS
        Me.AvisoLabel.Values.Text = ""
        '
        'KryptonLabel1
        '
        Me.KryptonLabel1.Location = New System.Drawing.Point(416, 5)
        Me.KryptonLabel1.Name = "KryptonLabel1"
        Me.KryptonLabel1.Palette = Me.adpPalette
        Me.KryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel1.Size = New System.Drawing.Size(68, 16)
        Me.KryptonLabel1.TabIndex = 0
        Me.KryptonLabel1.Values.Text = "Total FOB:"
        '
        'KryptonPanel2
        '
        Me.KryptonPanel2.Controls.Add(Me.GridDatos)
        Me.KryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonPanel2.Location = New System.Drawing.Point(0, 27)
        Me.KryptonPanel2.Name = "KryptonPanel2"
        Me.KryptonPanel2.Size = New System.Drawing.Size(683, 389)
        Me.KryptonPanel2.TabIndex = 3
        '
        'GridDatos
        '
        Me.GridDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDatos.Location = New System.Drawing.Point(0, 0)
        Me.GridDatos.Name = "GridDatos"
        Me.GridDatos.Size = New System.Drawing.Size(683, 389)
        Me.GridDatos.TabIndex = 0
        '
        'EmbarqueDetalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.KryptonPanel2)
        Me.Controls.Add(Me.PanelAviso)
        Me.Name = "EmbarqueDetalle"
        Me.Size = New System.Drawing.Size(683, 416)
        CType(Me.PanelAviso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelAviso.ResumeLayout(False)
        Me.PanelAviso.PerformLayout()
        CType(Me.KryptonPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel2.ResumeLayout(False)
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelAviso As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents KryptonPanel2 As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents GridDatos As ComponentFactory.Krypton.Toolkit.KryptonDataGridView
    Friend WithEvents KILOSTotal As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel3 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents FOBTotal As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel1 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents AvisoLabel As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents adpPalette As ComponentFactory.Krypton.Toolkit.KryptonPalette

End Class
