<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISAPMainForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISAPMainForm))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.SemanaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UbicacionDeBitacorasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ProgramarTareaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TerminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CognosButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.StatusButton = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusPeriodo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.CronometroLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PaisLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.InterfacesControl = New ISAPMenu.InterfacesControl()
        Me.PolizasControl1 = New ISAPMenu.PolizasControl()
        Me.StatusTextBox = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.AcercaDeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.ToolStripSeparator3, Me.CognosButton, Me.ToolStripSeparator6, Me.StatusButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1055, 39)
        Me.ToolStrip1.TabIndex = 1
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SemanaToolStripMenuItem, Me.UbicacionDeBitacorasToolStripMenuItem, Me.ToolStripSeparator5, Me.ProgramarTareaToolStripMenuItem, Me.ToolStripSeparator1, Me.TerminarToolStripMenuItem, Me.ToolStripSeparator2, Me.AcercaDeToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(45, 36)
        Me.ToolStripDropDownButton1.Text = "ToolStripDropDownButton1"
        '
        'SemanaToolStripMenuItem
        '
        Me.SemanaToolStripMenuItem.Name = "SemanaToolStripMenuItem"
        Me.SemanaToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.SemanaToolStripMenuItem.Text = "Semana"
        '
        'UbicacionDeBitacorasToolStripMenuItem
        '
        Me.UbicacionDeBitacorasToolStripMenuItem.Name = "UbicacionDeBitacorasToolStripMenuItem"
        Me.UbicacionDeBitacorasToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.UbicacionDeBitacorasToolStripMenuItem.Text = "Bitacoras"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(156, 6)
        '
        'ProgramarTareaToolStripMenuItem
        '
        Me.ProgramarTareaToolStripMenuItem.Name = "ProgramarTareaToolStripMenuItem"
        Me.ProgramarTareaToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.ProgramarTareaToolStripMenuItem.Text = "Programar tarea"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(156, 6)
        '
        'TerminarToolStripMenuItem
        '
        Me.TerminarToolStripMenuItem.Name = "TerminarToolStripMenuItem"
        Me.TerminarToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.TerminarToolStripMenuItem.Text = "Terminar"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'CognosButton
        '
        Me.CognosButton.Image = CType(resources.GetObject("CognosButton.Image"), System.Drawing.Image)
        Me.CognosButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CognosButton.Name = "CognosButton"
        Me.CognosButton.Size = New System.Drawing.Size(186, 36)
        Me.CognosButton.Text = "Revisar Pre-SAP en Cognos"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 39)
        '
        'StatusButton
        '
        Me.StatusButton.Image = CType(resources.GetObject("StatusButton.Image"), System.Drawing.Image)
        Me.StatusButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.StatusButton.Name = "StatusButton"
        Me.StatusButton.Size = New System.Drawing.Size(167, 36)
        Me.StatusButton.Text = "Cambiar estatus polizas"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.StatusPeriodo, Me.CronometroLabel, Me.PaisLabel, Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 740)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1055, 25)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = False
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(290, 20)
        Me.StatusLabel.Text = "Listo."
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusPeriodo
        '
        Me.StatusPeriodo.AutoSize = False
        Me.StatusPeriodo.Image = CType(resources.GetObject("StatusPeriodo.Image"), System.Drawing.Image)
        Me.StatusPeriodo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StatusPeriodo.Name = "StatusPeriodo"
        Me.StatusPeriodo.Size = New System.Drawing.Size(275, 20)
        '
        'CronometroLabel
        '
        Me.CronometroLabel.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter
        Me.CronometroLabel.ForeColor = System.Drawing.Color.Gray
        Me.CronometroLabel.Image = CType(resources.GetObject("CronometroLabel.Image"), System.Drawing.Image)
        Me.CronometroLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CronometroLabel.Name = "CronometroLabel"
        Me.CronometroLabel.Size = New System.Drawing.Size(133, 20)
        Me.CronometroLabel.Text = "Cronómetro inactivo"
        '
        'PaisLabel
        '
        Me.PaisLabel.Name = "PaisLabel"
        Me.PaisLabel.Size = New System.Drawing.Size(140, 20)
        Me.PaisLabel.Spring = True
        Me.PaisLabel.Text = "Pais"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(200, 19)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 39)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusTextBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(1055, 701)
        Me.SplitContainer1.SplitterDistance = 689
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.InterfacesControl)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.PolizasControl1)
        Me.SplitContainer2.Size = New System.Drawing.Size(689, 701)
        Me.SplitContainer2.SplitterDistance = 534
        Me.SplitContainer2.SplitterWidth = 6
        Me.SplitContainer2.TabIndex = 1
        '
        'InterfacesControl
        '
        Me.InterfacesControl.BackColor = System.Drawing.Color.Gainsboro
        Me.InterfacesControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InterfacesControl.Location = New System.Drawing.Point(0, 0)
        Me.InterfacesControl.Name = "InterfacesControl"
        Me.InterfacesControl.Size = New System.Drawing.Size(689, 534)
        Me.InterfacesControl.TabIndex = 2
        '
        'PolizasControl1
        '
        Me.PolizasControl1.BackColor = System.Drawing.Color.Gainsboro
        Me.PolizasControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PolizasControl1.Location = New System.Drawing.Point(0, 0)
        Me.PolizasControl1.Name = "PolizasControl1"
        Me.PolizasControl1.Size = New System.Drawing.Size(689, 161)
        Me.PolizasControl1.TabIndex = 0
        '
        'StatusTextBox
        '
        Me.StatusTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StatusTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusTextBox.Location = New System.Drawing.Point(0, 0)
        Me.StatusTextBox.Multiline = True
        Me.StatusTextBox.Name = "StatusTextBox"
        Me.StatusTextBox.ReadOnly = True
        Me.StatusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.StatusTextBox.Size = New System.Drawing.Size(360, 701)
        Me.StatusTextBox.TabIndex = 0
        '
        'Timer1
        '
        '
        'AcercaDeToolStripMenuItem
        '
        Me.AcercaDeToolStripMenuItem.Name = "AcercaDeToolStripMenuItem"
        Me.AcercaDeToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.AcercaDeToolStripMenuItem.Text = "Acerca de..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(156, 6)
        '
        'ISAPMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1055, 765)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ISAPMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Interfaces SAP"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents SemanaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UbicacionDeBitacorasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TerminarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StatusButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusPeriodo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PaisLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents StatusTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ProgramarTareaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InterfacesControl As ISAPMenu.InterfacesControl
    Friend WithEvents CronometroLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PolizasControl1 As ISAPMenu.PolizasControl
    Friend WithEvents CognosButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AcercaDeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
