Imports System.ComponentModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISAPMainForm
    Inherits System.Windows.Forms.Form

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
        Me.KryptonManager = New ComponentFactory.Krypton.Toolkit.KryptonManager(Me.components)
        Me.FechaDesde = New DevExpress.XtraEditors.DateEdit()
        Me.FechaHasta = New DevExpress.XtraEditors.DateEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.SatusChangeButton = New DevExpress.XtraEditors.SimpleButton()
        Me.ProcesarButton = New DevExpress.XtraEditors.SimpleButton()
        Me.AutenticarButton = New DevExpress.XtraEditors.SimpleButton()
        Me.GPIDText = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.GridDatos = New System.Windows.Forms.DataGridView()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.StatusTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        CType(Me.FechaDesde.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FechaDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FechaHasta.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FechaHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GPIDText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.StatusTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'KryptonManager
        '
        Me.KryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.SparklePurple
        '
        'FechaDesde
        '
        Me.FechaDesde.EditValue = Nothing
        Me.FechaDesde.Location = New System.Drawing.Point(373, 11)
        Me.FechaDesde.Name = "FechaDesde"
        Me.FechaDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FechaDesde.Properties.DisplayFormat.FormatString = "ddd d/MM/yyyy"
        Me.FechaDesde.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FechaDesde.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FechaDesde.Size = New System.Drawing.Size(134, 20)
        Me.FechaDesde.TabIndex = 2
        '
        'FechaHasta
        '
        Me.FechaHasta.EditValue = Nothing
        Me.FechaHasta.Location = New System.Drawing.Point(373, 37)
        Me.FechaHasta.Name = "FechaHasta"
        Me.FechaHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FechaHasta.Properties.DisplayFormat.FormatString = "ddd d/MM/yyyy"
        Me.FechaHasta.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FechaHasta.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FechaHasta.Size = New System.Drawing.Size(134, 20)
        Me.FechaHasta.TabIndex = 2
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.SatusChangeButton)
        Me.PanelControl1.Controls.Add(Me.ProcesarButton)
        Me.PanelControl1.Controls.Add(Me.AutenticarButton)
        Me.PanelControl1.Controls.Add(Me.GPIDText)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.FechaHasta)
        Me.PanelControl1.Controls.Add(Me.FechaDesde)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(803, 74)
        Me.PanelControl1.TabIndex = 7
        '
        'SatusChangeButton
        '
        Me.SatusChangeButton.Location = New System.Drawing.Point(627, 35)
        Me.SatusChangeButton.Name = "SatusChangeButton"
        Me.SatusChangeButton.Size = New System.Drawing.Size(125, 22)
        Me.SatusChangeButton.TabIndex = 6
        Me.SatusChangeButton.Text = "Cambiar Status Pólizas"
        '
        'ProcesarButton
        '
        Me.ProcesarButton.Location = New System.Drawing.Point(546, 34)
        Me.ProcesarButton.Name = "ProcesarButton"
        Me.ProcesarButton.Size = New System.Drawing.Size(75, 23)
        Me.ProcesarButton.TabIndex = 5
        Me.ProcesarButton.Text = "Procesar"
        '
        'AutenticarButton
        '
        Me.AutenticarButton.Location = New System.Drawing.Point(204, 34)
        Me.AutenticarButton.Name = "AutenticarButton"
        Me.AutenticarButton.Size = New System.Drawing.Size(75, 23)
        Me.AutenticarButton.TabIndex = 5
        Me.AutenticarButton.Text = "Autenticar"
        '
        'GPIDText
        '
        Me.GPIDText.DataBindings.Add(New System.Windows.Forms.Binding("Name", Global.ISAPMenu.My.MySettings.Default, "gpid", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.GPIDText.Location = New System.Drawing.Point(56, 14)
        Me.GPIDText.Name = Global.ISAPMenu.My.MySettings.Default.gpid
        Me.GPIDText.Size = New System.Drawing.Size(138, 20)
        Me.GPIDText.TabIndex = 4
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(22, 44)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(50, 13)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Password:"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(326, 44)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Hasta:"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(22, 18)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl3.TabIndex = 3
        Me.LabelControl3.Text = "GPID:"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(324, 17)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Desde:"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 74)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(803, 397)
        Me.XtraTabControl1.TabIndex = 9
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GridDatos)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(797, 371)
        Me.XtraTabPage1.Text = "Interfaces"
        '
        'GridDatos
        '
        Me.GridDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDatos.Location = New System.Drawing.Point(0, 0)
        Me.GridDatos.Name = "GridDatos"
        Me.GridDatos.Size = New System.Drawing.Size(797, 371)
        Me.GridDatos.TabIndex = 0
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.StatusTextBox)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(797, 371)
        Me.XtraTabPage2.Text = "Bitacora"
        '
        'StatusTextBox
        '
        Me.StatusTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StatusTextBox.Location = New System.Drawing.Point(0, 0)
        Me.StatusTextBox.Name = "StatusTextBox"
        Me.StatusTextBox.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.StatusTextBox.Properties.Appearance.Options.UseBackColor = True
        Me.StatusTextBox.Size = New System.Drawing.Size(797, 371)
        Me.StatusTextBox.TabIndex = 1
        '
        'ISAPMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(803, 531)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IsMdiContainer = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ISAPMainForm"
        Me.Text = "Interfases SAP"
        CType(Me.FechaDesde.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FechaDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FechaHasta.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FechaHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.GPIDText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.StatusTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents KryptonManager As ComponentFactory.Krypton.Toolkit.KryptonManager

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Fase1BGW = New BackgroundWorker()
        Me.Fase1BGW.WorkerReportsProgress = True
        AddHandler Me.Fase1BGW.DoWork, New DoWorkEventHandler(AddressOf Fase1BGW_DoWork)
        AddHandler Me.Fase1BGW.ProgressChanged, New ProgressChangedEventHandler(AddressOf Fase1BGW_ProgressChanged)
        AddHandler Me.Fase1BGW.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Fase1BGW_RunWorkerCompleted)

        'Me.Fase2BGW = New BackgroundWorker()
        'Me.Fase2BGW.WorkerReportsProgress = True
        'AddHandler Me.Fase2BGW.DoWork, New DoWorkEventHandler(AddressOf Fase2BGW_DoWork)
        'AddHandler Me.Fase2BGW.ProgressChanged, New ProgressChangedEventHandler(AddressOf Fase2BGW_ProgressChanged)
        'AddHandler Me.Fase2BGW.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Fase2BGW_RunWorkerCompleted)

        ConfigurarGrid()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents FechaDesde As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FechaHasta As DevExpress.XtraEditors.DateEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GPIDText As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridDatos As System.Windows.Forms.DataGridView
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ProcesarButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents AutenticarButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents StatusTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents SatusChangeButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
End Class
