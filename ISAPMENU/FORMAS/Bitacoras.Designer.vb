<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Bitacoras
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
        Me.PathTxt = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LocalizarBtn = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CerrarButton = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DirectorioList = New System.Windows.Forms.ListBox()
        Me.BitacoraTextBox = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PathTxt
        '
        Me.PathTxt.BackColor = System.Drawing.Color.White
        Me.PathTxt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.ISAPMenu.My.MySettings.Default, "PathFiles", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.PathTxt.Location = New System.Drawing.Point(161, 11)
        Me.PathTxt.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.PathTxt.Name = "PathTxt"
        Me.PathTxt.Size = New System.Drawing.Size(374, 19)
        Me.PathTxt.TabIndex = 11
        Me.PathTxt.Text = Global.ISAPMenu.My.MySettings.Default.PathFiles
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(11, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Ubicación de los archivos"
        '
        'LocalizarBtn
        '
        Me.LocalizarBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LocalizarBtn.Location = New System.Drawing.Point(561, 11)
        Me.LocalizarBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.LocalizarBtn.Name = "LocalizarBtn"
        Me.LocalizarBtn.Size = New System.Drawing.Size(84, 24)
        Me.LocalizarBtn.TabIndex = 9
        Me.LocalizarBtn.Text = "Localizar"
        Me.LocalizarBtn.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.LocalizarBtn)
        Me.Panel1.Controls.Add(Me.PathTxt)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(656, 44)
        Me.Panel1.TabIndex = 12
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel2.Controls.Add(Me.CerrarButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 551)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(656, 35)
        Me.Panel2.TabIndex = 13
        '
        'CerrarButton
        '
        Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CerrarButton.Location = New System.Drawing.Point(561, 4)
        Me.CerrarButton.Margin = New System.Windows.Forms.Padding(2)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(84, 24)
        Me.CerrarButton.TabIndex = 0
        Me.CerrarButton.Text = "Cerrar"
        Me.CerrarButton.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 44)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DirectorioList)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BitacoraTextBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(656, 507)
        Me.SplitContainer1.SplitterDistance = 273
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 14
        '
        'DirectorioList
        '
        Me.DirectorioList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DirectorioList.FormattingEnabled = True
        Me.DirectorioList.Location = New System.Drawing.Point(0, 0)
        Me.DirectorioList.Margin = New System.Windows.Forms.Padding(2)
        Me.DirectorioList.Name = "DirectorioList"
        Me.DirectorioList.Size = New System.Drawing.Size(273, 507)
        Me.DirectorioList.TabIndex = 0
        '
        'BitacoraTextBox
        '
        Me.BitacoraTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BitacoraTextBox.Location = New System.Drawing.Point(0, 0)
        Me.BitacoraTextBox.Margin = New System.Windows.Forms.Padding(2)
        Me.BitacoraTextBox.Multiline = True
        Me.BitacoraTextBox.Name = "BitacoraTextBox"
        Me.BitacoraTextBox.ReadOnly = True
        Me.BitacoraTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.BitacoraTextBox.Size = New System.Drawing.Size(380, 507)
        Me.BitacoraTextBox.TabIndex = 0
        '
        'Bitacoras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(656, 586)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Bitacoras"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bitacoras"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PathTxt As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LocalizarBtn As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CerrarButton As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DirectorioList As System.Windows.Forms.ListBox
    Friend WithEvents BitacoraTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
End Class
