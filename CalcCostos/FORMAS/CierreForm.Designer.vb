<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CierreForm
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.KryptonPanel2 = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.CerrarButton = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.GuardarButton = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.ButtonAbrir = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.FISCAL_YEAR = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.adpPalette = New ComponentFactory.Krypton.Toolkit.KryptonPalette(Me.components)
        Me.KryptonLabel5 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.PERIODO = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel4 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.FISCALYEAR_ANTERIOR = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel3 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.PERIODO_ANTERIOR = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel2 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonPanel1 = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.KryptonLabel1 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KryptonPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel2.SuspendLayout()
        CType(Me.KryptonPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'KryptonPanel
        '
        Me.KryptonPanel.Controls.Add(Me.PictureBox1)
        Me.KryptonPanel.Controls.Add(Me.KryptonPanel2)
        Me.KryptonPanel.Controls.Add(Me.FISCAL_YEAR)
        Me.KryptonPanel.Controls.Add(Me.KryptonLabel5)
        Me.KryptonPanel.Controls.Add(Me.PERIODO)
        Me.KryptonPanel.Controls.Add(Me.KryptonLabel4)
        Me.KryptonPanel.Controls.Add(Me.FISCALYEAR_ANTERIOR)
        Me.KryptonPanel.Controls.Add(Me.KryptonLabel3)
        Me.KryptonPanel.Controls.Add(Me.PERIODO_ANTERIOR)
        Me.KryptonPanel.Controls.Add(Me.KryptonLabel2)
        Me.KryptonPanel.Controls.Add(Me.KryptonPanel1)
        Me.KryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonPanel.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel.Name = "KryptonPanel"
        Me.KryptonPanel.Size = New System.Drawing.Size(476, 317)
        Me.KryptonPanel.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.CalcCostos.My.Resources.Resources._112_ArrowCurve_Blue_Left_48x48_72
        Me.PictureBox1.Location = New System.Drawing.Point(109, 109)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 96)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'KryptonPanel2
        '
        Me.KryptonPanel2.Controls.Add(Me.CerrarButton)
        Me.KryptonPanel2.Controls.Add(Me.GuardarButton)
        Me.KryptonPanel2.Controls.Add(Me.ButtonAbrir)
        Me.KryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.KryptonPanel2.Location = New System.Drawing.Point(0, 281)
        Me.KryptonPanel2.Name = "KryptonPanel2"
        Me.KryptonPanel2.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.TabHighProfile
        Me.KryptonPanel2.Size = New System.Drawing.Size(476, 36)
        Me.KryptonPanel2.TabIndex = 3
        '
        'CerrarButton
        '
        Me.CerrarButton.Location = New System.Drawing.Point(374, 8)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(90, 25)
        Me.CerrarButton.TabIndex = 0
        Me.CerrarButton.Values.Text = "Cancelar"
        '
        'GuardarButton
        '
        Me.GuardarButton.Location = New System.Drawing.Point(108, 6)
        Me.GuardarButton.Name = "GuardarButton"
        Me.GuardarButton.Size = New System.Drawing.Size(183, 25)
        Me.GuardarButton.TabIndex = 0
        Me.GuardarButton.Values.Text = "Guardar y cargar embarques"
        '
        'ButtonAbrir
        '
        Me.ButtonAbrir.Location = New System.Drawing.Point(12, 6)
        Me.ButtonAbrir.Name = "ButtonAbrir"
        Me.ButtonAbrir.Size = New System.Drawing.Size(90, 25)
        Me.ButtonAbrir.TabIndex = 0
        Me.ButtonAbrir.Values.Text = "Abrir periodo"
        '
        'FISCAL_YEAR
        '
        Me.FISCAL_YEAR.AutoSize = False
        Me.FISCAL_YEAR.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.Custom1
        Me.FISCAL_YEAR.Location = New System.Drawing.Point(334, 208)
        Me.FISCAL_YEAR.Name = "FISCAL_YEAR"
        Me.FISCAL_YEAR.Palette = Me.adpPalette
        Me.FISCAL_YEAR.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.FISCAL_YEAR.Size = New System.Drawing.Size(58, 19)
        Me.FISCAL_YEAR.TabIndex = 2
        Me.FISCAL_YEAR.Values.Text = ""
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
        'KryptonLabel5
        '
        Me.KryptonLabel5.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel
        Me.KryptonLabel5.Location = New System.Drawing.Point(269, 209)
        Me.KryptonLabel5.Name = "KryptonLabel5"
        Me.KryptonLabel5.Palette = Me.adpPalette
        Me.KryptonLabel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel5.Size = New System.Drawing.Size(64, 16)
        Me.KryptonLabel5.TabIndex = 2
        Me.KryptonLabel5.Values.Text = "Ano fiscal"
        '
        'PERIODO
        '
        Me.PERIODO.AutoSize = False
        Me.PERIODO.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.Custom1
        Me.PERIODO.Location = New System.Drawing.Point(179, 208)
        Me.PERIODO.Name = "PERIODO"
        Me.PERIODO.Palette = Me.adpPalette
        Me.PERIODO.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.PERIODO.Size = New System.Drawing.Size(94, 19)
        Me.PERIODO.TabIndex = 2
        Me.PERIODO.Values.Text = ""
        '
        'KryptonLabel4
        '
        Me.KryptonLabel4.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel
        Me.KryptonLabel4.Location = New System.Drawing.Point(84, 209)
        Me.KryptonLabel4.Name = "KryptonLabel4"
        Me.KryptonLabel4.Palette = Me.adpPalette
        Me.KryptonLabel4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel4.Size = New System.Drawing.Size(100, 16)
        Me.KryptonLabel4.TabIndex = 2
        Me.KryptonLabel4.Values.Text = "Proximo periodo"
        '
        'FISCALYEAR_ANTERIOR
        '
        Me.FISCALYEAR_ANTERIOR.AutoSize = False
        Me.FISCALYEAR_ANTERIOR.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.Custom1
        Me.FISCALYEAR_ANTERIOR.Location = New System.Drawing.Point(334, 90)
        Me.FISCALYEAR_ANTERIOR.Name = "FISCALYEAR_ANTERIOR"
        Me.FISCALYEAR_ANTERIOR.Palette = Me.adpPalette
        Me.FISCALYEAR_ANTERIOR.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.FISCALYEAR_ANTERIOR.Size = New System.Drawing.Size(58, 19)
        Me.FISCALYEAR_ANTERIOR.TabIndex = 2
        Me.FISCALYEAR_ANTERIOR.Values.Text = ""
        '
        'KryptonLabel3
        '
        Me.KryptonLabel3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel
        Me.KryptonLabel3.Location = New System.Drawing.Point(269, 91)
        Me.KryptonLabel3.Name = "KryptonLabel3"
        Me.KryptonLabel3.Palette = Me.adpPalette
        Me.KryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel3.Size = New System.Drawing.Size(64, 16)
        Me.KryptonLabel3.TabIndex = 2
        Me.KryptonLabel3.Values.Text = "Ano fiscal"
        '
        'PERIODO_ANTERIOR
        '
        Me.PERIODO_ANTERIOR.AutoSize = False
        Me.PERIODO_ANTERIOR.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.Custom1
        Me.PERIODO_ANTERIOR.Location = New System.Drawing.Point(179, 90)
        Me.PERIODO_ANTERIOR.Name = "PERIODO_ANTERIOR"
        Me.PERIODO_ANTERIOR.Palette = Me.adpPalette
        Me.PERIODO_ANTERIOR.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.PERIODO_ANTERIOR.Size = New System.Drawing.Size(88, 19)
        Me.PERIODO_ANTERIOR.TabIndex = 2
        Me.PERIODO_ANTERIOR.Values.Text = ""
        '
        'KryptonLabel2
        '
        Me.KryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel
        Me.KryptonLabel2.Location = New System.Drawing.Point(84, 91)
        Me.KryptonLabel2.Name = "KryptonLabel2"
        Me.KryptonLabel2.Palette = Me.adpPalette
        Me.KryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom
        Me.KryptonLabel2.Size = New System.Drawing.Size(93, 16)
        Me.KryptonLabel2.TabIndex = 2
        Me.KryptonLabel2.Values.Text = "Periodo abierto"
        '
        'KryptonPanel1
        '
        Me.KryptonPanel1.Controls.Add(Me.KryptonLabel1)
        Me.KryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.KryptonPanel1.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel1.Name = "KryptonPanel1"
        Me.KryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.InputControlCustom1
        Me.KryptonPanel1.Size = New System.Drawing.Size(476, 43)
        Me.KryptonPanel1.StateNormal.Image = Global.CalcCostos.My.Resources.Resources._109_AllAnnotations_Info_48x48_72
        Me.KryptonPanel1.StateNormal.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.CenterLeft
        Me.KryptonPanel1.TabIndex = 1
        '
        'KryptonLabel1
        '
        Me.KryptonLabel1.Location = New System.Drawing.Point(51, 12)
        Me.KryptonLabel1.Name = "KryptonLabel1"
        Me.KryptonLabel1.Size = New System.Drawing.Size(357, 19)
        Me.KryptonLabel1.TabIndex = 0
        Me.KryptonLabel1.Values.Text = "Al cerrar este periodo no podra modificar los datos de los embarques"
        '
        'CierreForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 317)
        Me.Controls.Add(Me.KryptonPanel)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CierreForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cierre de periodo"
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel.ResumeLayout(False)
        Me.KryptonPanel.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KryptonPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel2.ResumeLayout(False)
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
    Friend WithEvents KryptonLabel1 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonPanel2 As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents CerrarButton As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents ButtonAbrir As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents KryptonLabel5 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel4 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel3 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel2 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents FISCAL_YEAR As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents PERIODO As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents FISCALYEAR_ANTERIOR As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents PERIODO_ANTERIOR As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents GuardarButton As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents adpPalette As ComponentFactory.Krypton.Toolkit.KryptonPalette
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
