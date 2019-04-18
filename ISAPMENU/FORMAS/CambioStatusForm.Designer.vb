<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CambioStatusForm
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CerrarButton = New System.Windows.Forms.Button()
        Me.CambiarStatusButton = New System.Windows.Forms.Button()
        Me.FechaLabel = New System.Windows.Forms.Label()
        Me.PaisesListBox = New System.Windows.Forms.CheckedListBox()
        Me.CboTiposPolizas = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.CerrarButton)
        Me.Panel1.Controls.Add(Me.CambiarStatusButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 325)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(576, 41)
        Me.Panel1.TabIndex = 10
        '
        'CerrarButton
        '
        Me.CerrarButton.Location = New System.Drawing.Point(489, 6)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(75, 23)
        Me.CerrarButton.TabIndex = 11
        Me.CerrarButton.Text = "Cerrar"
        Me.CerrarButton.UseVisualStyleBackColor = True
        '
        'CambiarStatusButton
        '
        Me.CambiarStatusButton.Location = New System.Drawing.Point(408, 6)
        Me.CambiarStatusButton.Name = "CambiarStatusButton"
        Me.CambiarStatusButton.Size = New System.Drawing.Size(75, 23)
        Me.CambiarStatusButton.TabIndex = 10
        Me.CambiarStatusButton.Text = "Cambiar Status"
        Me.CambiarStatusButton.UseVisualStyleBackColor = True
        '
        'FechaLabel
        '
        Me.FechaLabel.AutoSize = True
        Me.FechaLabel.Location = New System.Drawing.Point(210, 71)
        Me.FechaLabel.Name = "FechaLabel"
        Me.FechaLabel.Size = New System.Drawing.Size(0, 13)
        Me.FechaLabel.TabIndex = 11
        '
        'PaisesListBox
        '
        Me.PaisesListBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PaisesListBox.FormattingEnabled = True
        Me.PaisesListBox.Location = New System.Drawing.Point(210, 137)
        Me.PaisesListBox.Name = "PaisesListBox"
        Me.PaisesListBox.Size = New System.Drawing.Size(354, 164)
        Me.PaisesListBox.TabIndex = 12
        '
        'CboTiposPolizas
        '
        Me.CboTiposPolizas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CboTiposPolizas.FormattingEnabled = True
        Me.CboTiposPolizas.Location = New System.Drawing.Point(210, 100)
        Me.CboTiposPolizas.Name = "CboTiposPolizas"
        Me.CboTiposPolizas.Size = New System.Drawing.Size(354, 21)
        Me.CboTiposPolizas.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(30, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(431, 26)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Al ejecutar este proceso cambiará el Status de las pólizas de la semana escogida " & _
    "a NEW." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Al cambiar el Status podrá subirlas a SAP."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(66, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "La semana TERMINA el:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(72, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Cambiar el Estatus de:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(93, 137)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Seleccione paises:"
        '
        'CambioStatusForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 366)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CboTiposPolizas)
        Me.Controls.Add(Me.PaisesListBox)
        Me.Controls.Add(Me.FechaLabel)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CambioStatusForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cambio de Status"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FechaLabel As System.Windows.Forms.Label
    Friend WithEvents PaisesListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents CboTiposPolizas As System.Windows.Forms.ComboBox
    Friend WithEvents CerrarButton As System.Windows.Forms.Button
    Friend WithEvents CambiarStatusButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
