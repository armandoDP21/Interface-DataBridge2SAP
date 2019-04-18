Public Class PolizasControl
    Private DatosMenuInterfaces As DataView
    Private DatosMenu As RunIsapData
    Private DatosCalendario As CalendarioData
    Public WriteOnly Property InterfacesRS As RunIsapData
        Set(value As RunIsapData)
            DatosMenu = value
            DatosMenuInterfaces = New DataView(DatosMenu.VSI_PROCESOS)
            DatosMenuInterfaces.RowFilter = "PARAM='Modulo'"
            Me.GridDatos.DataSource = Me.DatosMenuInterfaces
            AsignarTipoPoliza()
        End Set
    End Property
    Public WriteOnly Property DatosCalendarioRS As CalendarioData
        Set(value As CalendarioData)
            DatosCalendario = value
        End Set
    End Property

#Region "Metodos"

    Private Sub AsignarTipoPoliza()
        With DatosMenuInterfaces.Table
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
                If Tablerow.Ejecutar And Tablerow.PARAM = "Modulo" Then
                    InfoCache.TipoPolizas = Tablerow.PROCEDURE_NAME
                End If
            Next
        End With

    End Sub
    Private Sub Procesar()
        Me.Cursor = Cursors.WaitCursor
        GetSecuencia()
        With DatosMenuInterfaces.Table
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
                'percentComplete += 10
                'worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " " & FechaDesde.ToString)

                If Tablerow.Ejecutar And Tablerow.PARAM = "Modulo" Then


                    InfoCache.PaisClave = Tablerow.PAIS_CLAVE
                    SetDatosPais()
                    GerAPS()


                    Select Case Tablerow.PROCEDURE_NAME
                        Case "icc"
                            Select Case InfoCache.PaisClave

                                Case 1 'cambiar a 2 cuando se implemente
                                    Dim obj As New PolizasIngresosGT
                                    AddHandler obj.Mensaje, AddressOf EClass_EventHandler
                                    Dim result As Integer = obj.ProcesarCxC
                                Case Else
                                    Dim obj As New ISAPcxc
                                    AddHandler obj.Mensaje, AddressOf EClass_EventHandler
                                    Dim result As Integer = obj.ProcesarCxC
                            End Select

                        Case "pdcv"
                            Select Case InfoCache.PaisClave
                                Case 1 'cambiar a 2 cuando se implemente
                                    Dim obj As New PolizasCostoVentasGT
                                    AddHandler obj.Mensaje, AddressOf EClass_EventHandler
                                    Dim result As Integer = obj.ProcesarCostoVentas
                                Case Else
                                    Dim obj As New ISAPcostoventas
                                    AddHandler obj.Mensaje, AddressOf EClass_EventHandler
                                    Dim result As Integer = obj.ProcesarCostoVentas
                            End Select


                        Case "dbp_interfase_centralizada"

                            Dim obj As New ProcesoSP
                            AddHandler obj.Mensaje, AddressOf EClass_EventHandler
                            Dim result As Integer = obj.EjecutarSPC(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta, Tablerow.PAIS_CLAVE)
                            Tablerow.Ejecutar = False
                    End Select

                    'Dim Resultado As Integer = (New ProcesoSP).EjecutarSP(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta)
                    Tablerow.Ejecutar = False
                End If
            Next
        End With
        'With DatosMenu.VSI_PROCESOS
        '    For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
        '        If Tablerow.Ejecutar And Tablerow.PARAM = "{FD}{FH}" Then
        '            'percentComplete += 10
        '            'worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " " & FechaDesde.ToString)

        '            Dim obj As New ProcesoSP
        '            AddHandler obj.Mensaje, AddressOf EClass_EventHandler
        '            Dim result As Integer = obj.EjecutarSP(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta)
        '            'Dim Resultado As Integer = (New ProcesoSP).EjecutarSP(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta)
        '            Tablerow.Ejecutar = False
        '        End If
        '    Next
        'End With
        SaveSecuencia()
        Me.Cursor = Cursors.Default
        My.Application.DoEvents()
    End Sub
    Private Sub SetDatosPais()
        With DatosMenu.DATOSPAIS
            For Each Tablerow As RunIsapData.DATOSPAISRow In .Rows
                If Tablerow.PAIS_CLAVE = InfoCache.PaisClave Then
                    InfoCache.MonedaClave = Tablerow.MONEDA
                    InfoCache.CurrencyCode = Tablerow.MONEDA
                    InfoCache.ClaveCompania = Tablerow.CLAVE_CIA
                    InfoCache.NombrePais = Tablerow.PAIS_NOMBRE
                End If
            Next

        End With
        'TODO: PASAR NOMBREPAIS A STATUSLABEL

        'Me.NombrePais.Text = tablerow.PAIS_NOMBRE


        'TODO: usal linq
        'Me.Moneda.Text = tablerow.MONEDA
        'Me.ClaveCia_Label.Text = tablerow.CLAVE_CIA
        'InfoCache.PaisClave = tablerow.PAIS_CLAVE



    End Sub
    Private Sub GerAPS()
        With DatosCalendario.CALENDARIO_PAIS
            For Each Tablerow As CalendarioData.CALENDARIO_PAISRow In .Rows
                If Tablerow.PAIS_CLAVE = InfoCache.PaisClave Then
                    InfoCache.APS = Tablerow.APS
                    Exit For
                End If
            Next
        End With
    End Sub
    Private Sub GetSecuencia()
        InfoCache.Secuencia = (New MenuSP).SelectSecuencia


        'With DatosMenu.USERS_SECUENCIA
        '    For Each tablerow As RunIsapData.USERS_SECUENCIARow In .Rows

        '    Next
        'End With

    End Sub
    Private Sub SaveSecuencia()
        Dim result = (New MenuSP).RegistrarSecuencia
    End Sub
#End Region

#Region "Mensajes"
    Sub EClass_EventHandler(ByVal mensaje As String)
        DisplayStatus(mensaje)
    End Sub
    Private Sub DisplayStatus(ByVal Status As String)
        ISAPMainForm.StatusTextBox.AppendText(Status & Environment.NewLine)
        ScrollToBottom()
        My.Application.DoEvents()
    End Sub
    Private Sub ScrollToBottom()
        ISAPMainForm.StatusTextBox.Select()
        ISAPMainForm.StatusTextBox.SelectionStart = ISAPMainForm.StatusTextBox.Text.Length
        ISAPMainForm.StatusTextBox.ScrollToCaret()
    End Sub
    'Private Sub StatusLabelDisplay(ByVal ThisText As String)
    '    ISAPMainForm..StatusLabel.Text = ThisText
    'End Sub
    'Private Sub StatusBarFechas()
    '    ISAPMainForm..StatusPeriodo.Text = Format(My.Settings.FechaDesde, "ddd dd/MM/yyyy") & " - " & Format(My.Settings.FechaHasta, "ddd dd/MM/yyyy")
    'End Sub
#End Region
#Region "Eventos"

    Private Sub EjecutarButton_Click_1(sender As System.Object, e As System.EventArgs) Handles EjecutarButton.Click
        Procesar()
    End Sub

    Private Sub NoneButton_Click_1(sender As System.Object, e As System.EventArgs) Handles NoneButton.Click
        With DatosMenu.VSI_PROCESOS
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
                If Tablerow.PARAM = "Modulo" Then
                    Tablerow.Ejecutar = False
                End If
            Next
        End With
    End Sub

    Private Sub AllButton_Click_1(sender As System.Object, e As System.EventArgs) Handles AllButton.Click
        With DatosMenu.VSI_PROCESOS
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
                If Tablerow.PARAM = "Modulo" Then
                    Tablerow.Ejecutar = True
                End If
            Next
        End With
    End Sub
#End Region
#Region "ConfigurarGrid"


    Private Sub ConfigurarGrid()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridDatos.Width

        Dim PAIS_DESCRIPCIONColumn As New DataGridViewTextBoxColumn()
        Dim SUCURSAL_DESCRIPCIONColumn As New DataGridViewTextBoxColumn()
        Dim PROCEDURE_NAMEColumn As New DataGridViewTextBoxColumn()
        Dim EjecutarColumn As New DataGridViewCheckBoxColumn()

        With EjecutarColumn
            .HeaderText = "Ejecutar"
            .Name = "Ejecutar"
            .DataPropertyName = "Ejecutar"
            .Width = CInt(TotalGridWidth * 0.1)
            .DefaultCellStyle.SelectionBackColor = Color.Red
            .ReadOnly = False
        End With

        With PAIS_DESCRIPCIONColumn
            .HeaderText = "Pais"
            .Name = "PAIS_DESCRIPCION"
            .DataPropertyName = "PAIS_DESCRIPCION"
            .Width = CInt(TotalGridWidth * 0.25)
            .DefaultCellStyle.SelectionBackColor = Color.Red
            .ReadOnly = True
        End With
        With SUCURSAL_DESCRIPCIONColumn
            .HeaderText = "Sucursal"
            .Name = "SUCURSAL_DESCRIPCION"
            .DataPropertyName = "SUCURSAL_DESCRIPCION"
            .Width = CInt(TotalGridWidth * 0.25)
            '.DefaultCellStyle.SelectionBackColor = Color.Red
            .ReadOnly = True
        End With
        With PROCEDURE_NAMEColumn
            .HeaderText = "PROCEDURE_NAME"
            .Name = "PROCEDURE_NAME"
            .DataPropertyName = "PROCEDURE_NAME"
            .Width = CInt(TotalGridWidth * 0.4)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .ReadOnly = True
        End With


        With Me.GridDatos
            .Columns.Add(EjecutarColumn)
            .Columns.Add(PAIS_DESCRIPCIONColumn)
            .Columns.Add(SUCURSAL_DESCRIPCIONColumn)
            .Columns.Add(PROCEDURE_NAMEColumn)



            '.RowHeadersWidth = CInt(TotalGridWidth * 0.09)
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .ColumnHeadersDefaultCellStyle = columnHeaderStyle
            .ReadOnly = False
            .RowsDefaultCellStyle.BackColor = Color.PowderBlue
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
        End With

    End Sub
#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ConfigurarGrid()

    End Sub










End Class
