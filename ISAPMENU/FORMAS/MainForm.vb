Public Class MainForm

    Private ts As TimeSpan
    Private startDate As DateTime
    Private endDate As DateTime
    Private DatosMenu As MenuData
    Private ModuloActual As String = "ISAPmenu"
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        'RestoreWindowSettings()
        InfoCache.ConnectionString = "Data Source=ODSCA;User Id=ODSCA;Password=PRO56KAL"
        Me.FechaDesde.Value = "2010/11/1"
        Me.FechaHasta.Value = "2010/11/4"
        CargarDatosMenu()

    End Sub

#Region "Metodos"
    Private Sub CargarDatosMenu()
        Me.Cursor = Cursors.WaitCursor
        StatusLabelDisplay("Cargando elementos de menu")
        My.Application.DoEvents()
        'DatosMenu = (New MenuSP).MenuSelectData()
        AgregarTodos()

        StatusLabelDisplay("Cargando elementos de menu")
        'With Me.cboPaises
        '    .ValueMember = DatosMenu.MENUPAISES.PAIS_CLAVEColumn.ColumnName
        '    .DisplayMember = DatosMenu.MENUPAISES.PAIS_DESCRIPCIONColumn.ColumnName
        '    .DataSource = DatosMenu.MENUPAISES.DefaultView
        'End With

        StatusLabelDisplay("Listo")
        My.Application.DoEvents()
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub AgregarTodos()
        With DatosMenu.MENUSUCURSALES
            Dim NewDataRow As MenuData.MENUSUCURSALESRow = .NewMENUSUCURSALESRow
            With NewDataRow
                .PAIS_CLAVE = 0
                .SUCURSAL_DESCRIPCION = "<Todas las sucursales>"
                .SUCURSAL_CLAVE = 0

            End With
            .AddMENUSUCURSALESRow(NewDataRow)
        End With
    End Sub
    Private Sub StatusLabelDisplay(ByVal ThisText As String)
        Me.StatusLabel.Text = ThisText
    End Sub
    Private Sub AsignarSucursales()




        Dim q = From p In DatosMenu.MENUSUCURSALES _
                            Where p.PAIS_CLAVE = InfoCache.PaisClave Or p.PAIS_CLAVE = 0 _
        Select New With {p.SUCURSAL_CLAVE, p.SUCURSAL_DESCRIPCION}


        For Each secuencia In q
           

        Next

        With Me.cboSucursales
            .DataSource = Nothing
            .Items.Clear()
            .DataSource = q.ToArray
            .ValueMember = "SUCURSAL_CLAVE"
            .DisplayMember = "SUCURSAL_DESCRIPCION"
            .SelectedValue = 0
        End With


    End Sub
    Private Sub ProcesarSP()
        StatusTextBox.Clear()
        StatusTextBox.AppendText("Ejecucion iniciada " & Date.Today.ToShortDateString & Environment.NewLine & Environment.NewLine)
        Dim proceso As Integer
        If chkTodos.CheckState = CheckState.Checked Then
            proceso = 0
        Else
            Select Case True
                Case RBFacturacion.Checked
                    proceso = 3
                Case RBLiquidaciones.Checked
                    proceso = 2
                Case RBMovimientos.Checked
                    proceso = 1

            End Select
        End If
        Select Case InfoCache.PaisClave
            Case 2
                With New MenuSP

                    AddHandler .Mensaje, AddressOf PresentarMensajes
                    .ProcesarGuatemala(proceso)

                End With
            Case 3
            Case 4
            Case 5
            Case 6

        End Select
        StatusTextBox.AppendText("Ejecucion terminada " & Date.Today.ToShortDateString & Environment.NewLine & Environment.NewLine)
    End Sub
    Private Sub PresentarMensajes(ByVal a As String)
        StatusTextBox.AppendText(a & Environment.NewLine & Environment.NewLine)
        ScrollToBottom()
    End Sub
    Private Sub ScrollToBottom()
        StatusTextBox.Select()
        StatusTextBox.SelectionStart = StatusTextBox.Text.Length
        StatusTextBox.ScrollToCaret()
    End Sub
#End Region
#Region "Eventos"
    Private Sub cboPaises_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'InfoCache.PaisClave = cboPaises.SelectedValue
        'Me.PaisLabel.Text = cboPaises.Text
        AsignarSucursales()
    End Sub
    Private Sub cboSucursales_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSucursales.SelectedIndexChanged
        If cboSucursales.Items.Count > 0 Then
            InfoCache.SucursalClave = GetIndex(cboSucursales.Text)
        End If
    End Sub
    Private Function GetIndex(ByVal NombreSucursal As String) As Integer
        Dim i As Integer
        Dim q = From p In DatosMenu.MENUSUCURSALES _
                           Where p.SUCURSAL_DESCRIPCION = NombreSucursal _
       Select New With {p.SUCURSAL_CLAVE}
        For Each secuencia In q
            i = secuencia.SUCURSAL_CLAVE
        Next
        Return i
    End Function
    Private Sub chkTodos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTodos.CheckedChanged, CheckBox1.CheckedChanged
        If chkTodos.CheckState = CheckState.Checked Then
            Me.RBFacturacion.Checked = False
            Me.RBLiquidaciones.Checked = False
            Me.RBMovimientos.Checked = False

            Me.RBFacturacion.Enabled = False
            Me.RBLiquidaciones.Enabled = False
            Me.RBMovimientos.Enabled = False
        ElseIf chkTodos.CheckState = CheckState.Unchecked Then
            Me.RBFacturacion.Enabled = True
            Me.RBLiquidaciones.Enabled = True
            Me.RBMovimientos.Enabled = True
            Me.RBMovimientos.Checked = True
        End If

    End Sub
    Private Sub ProcesarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcesarButton.Click
        ProcesarSP()
    End Sub
#End Region


    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub
End Class
