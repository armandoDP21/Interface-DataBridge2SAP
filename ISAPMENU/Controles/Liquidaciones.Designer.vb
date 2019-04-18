<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Liquidaciones
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
        Me.LiquidacionesPanel = New ComponentFactory.Krypton.Toolkit.KryptonPanel()
        Me.KryptonLabel8 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.I1F4Button = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.KryptonButton1 = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.I1F2Button = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.I1F1Button = New ComponentFactory.Krypton.Toolkit.KryptonButton()
        Me.KryptonLabel6 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel5 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel4 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel2 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel7 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel3 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.KryptonLabel1 = New ComponentFactory.Krypton.Toolkit.KryptonLabel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.KryptonTextBox1 = New ComponentFactory.Krypton.Toolkit.KryptonTextBox()
        CType(Me.LiquidacionesPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LiquidacionesPanel.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'LiquidacionesPanel
        '
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonLabel8)
        Me.LiquidacionesPanel.Controls.Add(Me.I1F4Button)
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonButton1)
        Me.LiquidacionesPanel.Controls.Add(Me.I1F2Button)
        Me.LiquidacionesPanel.Controls.Add(Me.I1F1Button)
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonLabel6)
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonLabel5)
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonLabel4)
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonLabel2)
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonLabel7)
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonLabel3)
        Me.LiquidacionesPanel.Controls.Add(Me.KryptonLabel1)
        Me.LiquidacionesPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LiquidacionesPanel.Location = New System.Drawing.Point(3, 3)
        Me.LiquidacionesPanel.Name = "LiquidacionesPanel"
        Me.LiquidacionesPanel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.TabOneNote
        Me.LiquidacionesPanel.Size = New System.Drawing.Size(644, 365)
        Me.LiquidacionesPanel.TabIndex = 1
        '
        'KryptonLabel8
        '
        Me.KryptonLabel8.Location = New System.Drawing.Point(136, 248)
        Me.KryptonLabel8.Name = "KryptonLabel8"
        Me.KryptonLabel8.Size = New System.Drawing.Size(499, 19)
        Me.KryptonLabel8.StateCommon.ShortText.Color1 = System.Drawing.Color.Blue
        Me.KryptonLabel8.TabIndex = 0
        Me.KryptonLabel8.Values.Image = Global.ISAPMenu.My.Resources.Resources.WarningHS
        Me.KryptonLabel8.Values.Text = "Si hay errores o diferencias se deben reivsar los datos en DC TOTAL y ejecutar de" & _
            "sde la FASE 1."
        '
        'I1F4Button
        '
        Me.I1F4Button.Enabled = False
        Me.I1F4Button.Location = New System.Drawing.Point(20, 289)
        Me.I1F4Button.Name = "I1F4Button"
        Me.I1F4Button.Size = New System.Drawing.Size(110, 49)
        Me.I1F4Button.TabIndex = 1
        Me.I1F4Button.Values.ImageStates.ImageCheckedNormal = Nothing
        Me.I1F4Button.Values.ImageStates.ImageCheckedPressed = Nothing
        Me.I1F4Button.Values.ImageStates.ImageCheckedTracking = Nothing
        Me.I1F4Button.Values.ImageStates.ImageDisabled = Global.ISAPMenu.My.Resources.Resources.base_checkmark_32
        Me.I1F4Button.Values.ImageStates.ImageNormal = Global.ISAPMenu.My.Resources.Resources._112_RefreshArrow_Blue_32x42_72
        Me.I1F4Button.Values.Text = "Fase 4"
        '
        'KryptonButton1
        '
        Me.KryptonButton1.Enabled = False
        Me.KryptonButton1.Location = New System.Drawing.Point(20, 218)
        Me.KryptonButton1.Name = "KryptonButton1"
        Me.KryptonButton1.Size = New System.Drawing.Size(110, 49)
        Me.KryptonButton1.TabIndex = 1
        Me.KryptonButton1.Values.ImageStates.ImageCheckedNormal = Nothing
        Me.KryptonButton1.Values.ImageStates.ImageCheckedPressed = Nothing
        Me.KryptonButton1.Values.ImageStates.ImageCheckedTracking = Nothing
        Me.KryptonButton1.Values.ImageStates.ImageDisabled = Global.ISAPMenu.My.Resources.Resources.base_checkmark_32
        Me.KryptonButton1.Values.ImageStates.ImageNormal = Global.ISAPMenu.My.Resources.Resources.internet_explorer_logo
        Me.KryptonButton1.Values.Text = "Fase 3"
        '
        'I1F2Button
        '
        Me.I1F2Button.Enabled = False
        Me.I1F2Button.Location = New System.Drawing.Point(20, 138)
        Me.I1F2Button.Name = "I1F2Button"
        Me.I1F2Button.Size = New System.Drawing.Size(110, 49)
        Me.I1F2Button.TabIndex = 1
        Me.I1F2Button.Values.ImageStates.ImageCheckedNormal = Nothing
        Me.I1F2Button.Values.ImageStates.ImageCheckedPressed = Nothing
        Me.I1F2Button.Values.ImageStates.ImageCheckedTracking = Nothing
        Me.I1F2Button.Values.ImageStates.ImageDisabled = Global.ISAPMenu.My.Resources.Resources.base_checkmark_32
        Me.I1F2Button.Values.ImageStates.ImageNormal = Global.ISAPMenu.My.Resources.Resources._112_RefreshArrow_Blue_32x42_72
        Me.I1F2Button.Values.Text = "Fase 2"
        '
        'I1F1Button
        '
        Me.I1F1Button.Enabled = False
        Me.I1F1Button.Location = New System.Drawing.Point(20, 63)
        Me.I1F1Button.Name = "I1F1Button"
        Me.I1F1Button.Size = New System.Drawing.Size(110, 49)
        Me.I1F1Button.TabIndex = 1
        Me.I1F1Button.Values.ImageStates.ImageCheckedNormal = Nothing
        Me.I1F1Button.Values.ImageStates.ImageCheckedPressed = Nothing
        Me.I1F1Button.Values.ImageStates.ImageCheckedTracking = Nothing
        Me.I1F1Button.Values.ImageStates.ImageDisabled = Global.ISAPMenu.My.Resources.Resources.base_checkmark_32
        Me.I1F1Button.Values.ImageStates.ImageNormal = Global.ISAPMenu.My.Resources.Resources._112_RefreshArrow_Blue_32x42_72
        Me.I1F1Button.Values.Text = "Fase 1"
        '
        'KryptonLabel6
        '
        Me.KryptonLabel6.Location = New System.Drawing.Point(136, 296)
        Me.KryptonLabel6.Name = "KryptonLabel6"
        Me.KryptonLabel6.Size = New System.Drawing.Size(275, 19)
        Me.KryptonLabel6.TabIndex = 0
        Me.KryptonLabel6.Values.Text = "Cambie el Status de las polizas para que suban a SAP"
        '
        'KryptonLabel5
        '
        Me.KryptonLabel5.Location = New System.Drawing.Point(148, 192)
        Me.KryptonLabel5.Name = "KryptonLabel5"
        Me.KryptonLabel5.Size = New System.Drawing.Size(290, 50)
        Me.KryptonLabel5.TabIndex = 0
        Me.KryptonLabel5.Values.Image = Global.ISAPMenu.My.Resources.Resources.Move
        Me.KryptonLabel5.Values.Text = "Fase 3  Revise en Cognos las polizas de ventas"
        '
        'KryptonLabel4
        '
        Me.KryptonLabel4.Location = New System.Drawing.Point(136, 152)
        Me.KryptonLabel4.Name = "KryptonLabel4"
        Me.KryptonLabel4.Size = New System.Drawing.Size(200, 19)
        Me.KryptonLabel4.TabIndex = 0
        Me.KryptonLabel4.Values.Text = "Ejecuta  interfase de Polizas de Ventas"
        '
        'KryptonLabel2
        '
        Me.KryptonLabel2.Location = New System.Drawing.Point(136, 68)
        Me.KryptonLabel2.Name = "KryptonLabel2"
        Me.KryptonLabel2.Size = New System.Drawing.Size(199, 19)
        Me.KryptonLabel2.TabIndex = 0
        Me.KryptonLabel2.Values.Text = "Ejecuta interfase de DC TOTAL a ODS."
        '
        'KryptonLabel7
        '
        Me.KryptonLabel7.Location = New System.Drawing.Point(20, 360)
        Me.KryptonLabel7.Name = "KryptonLabel7"
        Me.KryptonLabel7.Size = New System.Drawing.Size(267, 34)
        Me.KryptonLabel7.TabIndex = 0
        Me.KryptonLabel7.Values.Image = Global.ISAPMenu.My.Resources.Resources.Task_Inspector_Suggestion
        Me.KryptonLabel7.Values.Text = "Puede levantar un ticket para la ejecucion de "
        '
        'KryptonLabel3
        '
        Me.KryptonLabel3.Location = New System.Drawing.Point(136, 84)
        Me.KryptonLabel3.Name = "KryptonLabel3"
        Me.KryptonLabel3.Size = New System.Drawing.Size(464, 19)
        Me.KryptonLabel3.StateCommon.ShortText.Color1 = System.Drawing.Color.Blue
        Me.KryptonLabel3.TabIndex = 0
        Me.KryptonLabel3.Values.Image = Global.ISAPMenu.My.Resources.Resources.WarningHS
        Me.KryptonLabel3.Values.Text = "Todos los servidores de las sucursales deben estar disponibles y haber cerrado pe" & _
            "riodo."
        '
        'KryptonLabel1
        '
        Me.KryptonLabel1.Location = New System.Drawing.Point(20, 20)
        Me.KryptonLabel1.Name = "KryptonLabel1"
        Me.KryptonLabel1.Size = New System.Drawing.Size(180, 31)
        Me.KryptonLabel1.StateCommon.ShortText.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonLabel1.TabIndex = 0
        Me.KryptonLabel1.Values.Text = "Liquidaciones"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(658, 397)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LiquidacionesPanel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(650, 371)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Interface"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.KryptonTextBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(650, 371)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Log de avance"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'KryptonTextBox1
        '
        Me.KryptonTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonTextBox1.Location = New System.Drawing.Point(3, 3)
        Me.KryptonTextBox1.Multiline = True
        Me.KryptonTextBox1.Name = "KryptonTextBox1"
        Me.KryptonTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.KryptonTextBox1.Size = New System.Drawing.Size(644, 365)
        Me.KryptonTextBox1.TabIndex = 0
        Me.KryptonTextBox1.Text = "KryptonTextBox1"
        '
        'Liquidaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Liquidaciones"
        Me.Size = New System.Drawing.Size(658, 397)
        CType(Me.LiquidacionesPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LiquidacionesPanel.ResumeLayout(False)
        Me.LiquidacionesPanel.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LiquidacionesPanel As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents KryptonLabel8 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents I1F4Button As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents KryptonButton1 As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents I1F2Button As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents I1F1Button As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents KryptonLabel6 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel5 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel4 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel2 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel7 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel3 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel1 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents KryptonTextBox1 As ComponentFactory.Krypton.Toolkit.KryptonTextBox

End Class
