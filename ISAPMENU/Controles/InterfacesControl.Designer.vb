<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InterfacesControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InterfacesControl))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.EjecutarButton = New System.Windows.Forms.Button()
        Me.NoneButton = New System.Windows.Forms.Button()
        Me.AllButton = New System.Windows.Forms.Button()
        Me.GridDatos = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Green
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(572, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Interfaces"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.EjecutarButton)
        Me.Panel1.Controls.Add(Me.NoneButton)
        Me.Panel1.Controls.Add(Me.AllButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(572, 31)
        Me.Panel1.TabIndex = 4
        '
        'EjecutarButton
        '
        Me.EjecutarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EjecutarButton.Image = CType(resources.GetObject("EjecutarButton.Image"), System.Drawing.Image)
        Me.EjecutarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EjecutarButton.Location = New System.Drawing.Point(410, 3)
        Me.EjecutarButton.Name = "EjecutarButton"
        Me.EjecutarButton.Size = New System.Drawing.Size(149, 23)
        Me.EjecutarButton.TabIndex = 0
        Me.EjecutarButton.Text = "Ejecutar"
        Me.EjecutarButton.UseVisualStyleBackColor = True
        '
        'NoneButton
        '
        Me.NoneButton.Location = New System.Drawing.Point(168, 3)
        Me.NoneButton.Name = "NoneButton"
        Me.NoneButton.Size = New System.Drawing.Size(149, 23)
        Me.NoneButton.TabIndex = 0
        Me.NoneButton.Text = "Quitar selección a todos"
        Me.NoneButton.UseVisualStyleBackColor = True
        '
        'AllButton
        '
        Me.AllButton.Image = CType(resources.GetObject("AllButton.Image"), System.Drawing.Image)
        Me.AllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AllButton.Location = New System.Drawing.Point(13, 3)
        Me.AllButton.Name = "AllButton"
        Me.AllButton.Size = New System.Drawing.Size(149, 23)
        Me.AllButton.TabIndex = 0
        Me.AllButton.Text = "Seleccionar todos"
        Me.AllButton.UseVisualStyleBackColor = True
        '
        'GridDatos
        '
        Me.GridDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDatos.Location = New System.Drawing.Point(0, 51)
        Me.GridDatos.Name = "GridDatos"
        Me.GridDatos.Size = New System.Drawing.Size(572, 399)
        Me.GridDatos.TabIndex = 6
        '
        'InterfacesControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridDatos)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "InterfacesControl"
        Me.Size = New System.Drawing.Size(572, 450)
        Me.Panel1.ResumeLayout(False)
        CType(Me.GridDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents NoneButton As System.Windows.Forms.Button
    Friend WithEvents AllButton As System.Windows.Forms.Button
    Friend WithEvents EjecutarButton As System.Windows.Forms.Button
    Friend WithEvents GridDatos As System.Windows.Forms.DataGridView

End Class
