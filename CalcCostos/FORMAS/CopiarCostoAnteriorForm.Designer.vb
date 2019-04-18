<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CopiarCostoAnteriorForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.YearOrigenText = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CopiarButton = New System.Windows.Forms.Button()
        Me.YearDestinoText = New System.Windows.Forms.TextBox()
        Me.cboMesOrigen = New System.Windows.Forms.ComboBox()
        Me.cboMesDestino = New System.Windows.Forms.ComboBox()
        Me.CerrarButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(56, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Año de origen:"
        '
        'YearOrigenText
        '
        Me.YearOrigenText.Location = New System.Drawing.Point(145, 68)
        Me.YearOrigenText.Name = "YearOrigenText"
        Me.YearOrigenText.Size = New System.Drawing.Size(100, 21)
        Me.YearOrigenText.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(56, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Mes de origen:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(66, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Año destino:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(66, 198)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Mes destino:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(27, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(229, 24)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Destino de los costos"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(27, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(219, 24)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Origen de los costos"
        '
        'CopiarButton
        '
        Me.CopiarButton.Location = New System.Drawing.Point(12, 12)
        Me.CopiarButton.Name = "CopiarButton"
        Me.CopiarButton.Size = New System.Drawing.Size(75, 23)
        Me.CopiarButton.TabIndex = 2
        Me.CopiarButton.Text = "Copiar"
        Me.CopiarButton.UseVisualStyleBackColor = True
        '
        'YearDestinoText
        '
        Me.YearDestinoText.Location = New System.Drawing.Point(145, 168)
        Me.YearDestinoText.Name = "YearDestinoText"
        Me.YearDestinoText.Size = New System.Drawing.Size(100, 21)
        Me.YearDestinoText.TabIndex = 1
        '
        'cboMesOrigen
        '
        Me.cboMesOrigen.FormattingEnabled = True
        Me.cboMesOrigen.Items.AddRange(New Object() {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre"})
        Me.cboMesOrigen.Location = New System.Drawing.Point(145, 100)
        Me.cboMesOrigen.Name = "cboMesOrigen"
        Me.cboMesOrigen.Size = New System.Drawing.Size(185, 21)
        Me.cboMesOrigen.TabIndex = 3
        '
        'cboMesDestino
        '
        Me.cboMesDestino.FormattingEnabled = True
        Me.cboMesDestino.Items.AddRange(New Object() {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre"})
        Me.cboMesDestino.Location = New System.Drawing.Point(145, 195)
        Me.cboMesDestino.Name = "cboMesDestino"
        Me.cboMesDestino.Size = New System.Drawing.Size(185, 21)
        Me.cboMesDestino.TabIndex = 3
        '
        'CerrarButton
        '
        Me.CerrarButton.Location = New System.Drawing.Point(359, 12)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(75, 23)
        Me.CerrarButton.TabIndex = 4
        Me.CerrarButton.Text = "Cerrar"
        Me.CerrarButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.CerrarButton)
        Me.Panel1.Controls.Add(Me.CopiarButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 324)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(457, 38)
        Me.Panel1.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Copiar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(359, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'CopiarCostoAnteriorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 362)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cboMesDestino)
        Me.Controls.Add(Me.cboMesOrigen)
        Me.Controls.Add(Me.YearDestinoText)
        Me.Controls.Add(Me.YearOrigenText)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CopiarCostoAnteriorForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copiar Costos "
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents YearOrigenText As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CopiarButton As System.Windows.Forms.Button
    Friend WithEvents YearDestinoText As System.Windows.Forms.TextBox
    Friend WithEvents cboMesOrigen As System.Windows.Forms.ComboBox
    Friend WithEvents cboMesDestino As System.Windows.Forms.ComboBox
    Friend WithEvents CerrarButton As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
