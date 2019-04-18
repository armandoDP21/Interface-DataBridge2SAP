Public Class InterfacesControl
    Private DatosMenuInterfaces As DataView
    Private DatosMenu As RunIsapData
    Dim start, finish, totalTime As Double
    Public WriteOnly Property InterfacesRS As RunIsapData
        Set(value As RunIsapData)
            DatosMenu = value
            DatosMenuInterfaces = New DataView(DatosMenu.VSI_PROCESOS)
            DatosMenuInterfaces.RowFilter = "PARAM='{FD}{FH}'"
            Me.GridDatos.DataSource = Me.DatosMenuInterfaces
        End Set
    End Property

    Public Sub Procesar()
        Me.Cursor = Cursors.WaitCursor
        start = Microsoft.VisualBasic.DateAndTime.Timer

        Dim Cadena As String = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=OracleSID))); uid=09064257;pwd=*******;"

        DisplayStatus(Cadena & Format(Date.Now, "R") & ". Por " & InfoCache.UId)

        DisplayStatus("Proceso iniciado a " & Format(Date.Now, "R") & ". Por " & InfoCache.UId)
        With DatosMenuInterfaces.Table
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
                If Tablerow.Ejecutar And Tablerow.PARAM = "{FD}{FH}" Then
                    'percentComplete += 10
                    'worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " " & FechaDesde.ToString)

                    Dim obj As New ProcesoSP
                    AddHandler obj.Mensaje, AddressOf EClass_EventHandler
                    Dim result As Integer = obj.EjecutarSP(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta)
                    'Dim Resultado As Integer = (New ProcesoSP).EjecutarSP(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta)
                    Tablerow.Ejecutar = False

                End If
            Next
        End With
        DisplayStatus("Todos los proceso terminaro a  " & Format(Date.Now, "R") & ". Por " & InfoCache.UId)
        totalTime = Microsoft.VisualBasic.DateAndTime.Timer - start
        DisplayStatus("Duracion global del proceso:  " & SecondsToText(totalTime))

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
        Me.Cursor = Cursors.Default
        My.Application.DoEvents()
    End Sub

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
    Private Sub AllButton_Click(sender As System.Object, e As System.EventArgs) Handles AllButton.Click
        With DatosMenu.VSI_PROCESOS
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
                If Tablerow.PARAM = "{FD}{FH}" Then
                    Tablerow.Ejecutar = True
                End If
            Next
        End With
    End Sub
    Private Sub NoneButton_Click(sender As System.Object, e As System.EventArgs) Handles NoneButton.Click
        With DatosMenu.VSI_PROCESOS
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
                If Tablerow.PARAM = "{FD}{FH}" Then
                    Tablerow.Ejecutar = False
                End If
            Next
        End With
    End Sub
    Private Sub EjecutarButton_Click(sender As System.Object, e As System.EventArgs) Handles EjecutarButton.Click
        Procesar()
    End Sub
#End Region
#Region "ConfigurarGrid"


    Private Sub ConfigurarGrid()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridDatos.Width '- 24

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
        InitializeComponent()
        ConfigurarGrid()
    End Sub
End Class
