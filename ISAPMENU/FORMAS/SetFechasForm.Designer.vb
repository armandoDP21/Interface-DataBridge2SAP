<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetFechasForm
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
        Me.FechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CancelarButton = New System.Windows.Forms.Button()
        Me.AceptarButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.FechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'FechaHasta
        '
        Me.FechaHasta.Checked = False
        Me.FechaHasta.Location = New System.Drawing.Point(119, 104)
        Me.FechaHasta.MinDate = New Date(2011, 10, 1, 0, 0, 0, 0)
        Me.FechaHasta.Name = "FechaHasta"
        Me.FechaHasta.Size = New System.Drawing.Size(218, 20)
        Me.FechaHasta.TabIndex = 0
        Me.FechaHasta.Value = New Date(2011, 11, 10, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(62, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Desde:"
        '
        'CancelarButton
        '
        Me.CancelarButton.Location = New System.Drawing.Point(276, 157)
        Me.CancelarButton.Name = "CancelarButton"
        Me.CancelarButton.Size = New System.Drawing.Size(93, 34)
        Me.CancelarButton.TabIndex = 2
        Me.CancelarButton.Text = "Cancelar"
        Me.CancelarButton.UseVisualStyleBackColor = True
        '
        'AceptarButton
        '
        Me.AceptarButton.Location = New System.Drawing.Point(177, 157)
        Me.AceptarButton.Name = "AceptarButton"
        Me.AceptarButton.Size = New System.Drawing.Size(93, 34)
        Me.AceptarButton.TabIndex = 2
        Me.AceptarButton.Text = "Aceptar"
        Me.AceptarButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(66, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Hasta:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(12, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(211, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Las interfaces correran entre estas fechas"
        '
        'FechaDesde
        '
        Me.FechaDesde.Checked = False
        Me.FechaDesde.Location = New System.Drawing.Point(119, 75)
        Me.FechaDesde.MinDate = New Date(2011, 10, 1, 0, 0, 0, 0)
        Me.FechaDesde.Name = "FechaDesde"
        Me.FechaDesde.Size = New System.Drawing.Size(218, 20)
        Me.FechaDesde.TabIndex = 0
        Me.FechaDesde.Value = New Date(2011, 11, 4, 0, 0, 0, 0)
        '
        'SetFechasForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 203)
        Me.Controls.Add(Me.AceptarButton)
        Me.Controls.Add(Me.CancelarButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FechaHasta)
        Me.Controls.Add(Me.FechaDesde)
        Me.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SetFechasForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seleccione Fechas"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FechaDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents FechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CancelarButton As System.Windows.Forms.Button
    Friend WithEvents AceptarButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
