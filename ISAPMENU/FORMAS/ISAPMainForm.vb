Imports System.IO
Imports System.ComponentModel
Public Class ISAPMainForm
#Region " Internal members "
    Private Fase1BGW As BackgroundWorker
    Private Fase2BGW As BackgroundWorker
#End Region
    Private InicioProceso As Date
    Private TareaEnProgeso As Boolean
    Private ts As TimeSpan
    Private startDate As DateTime
    Private endDate As DateTime
    Private DatosMenu As RunIsapData
    Private DatosCalendario As CalendarioData
    Private ModuloActual As String = "ISAPmenu"
    Dim start, finish, totalTime As Double
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        Me.Text = "Interfaces SAP"
        InfoCache.ConnectionString = "Data Source=ODS.PEPWDR00474;User Id=PRODODS;Password=managerjr"
       


        InfoCache.FechaDesde = New System.DateTime(My.Settings.FechaDesde.Year, My.Settings.FechaDesde.Month, My.Settings.FechaDesde.Day)
        InfoCache.FechaHasta = New System.DateTime(My.Settings.FechaHasta.Year, My.Settings.FechaHasta.Month, My.Settings.FechaHasta.Day)
        InfoCache.PeriodoActual = Format((InfoCache.FechaDesde.Month), "000")
        InfoCache.FiscalYear = InfoCache.FechaDesde.Year

        InfoCache.ESTATUS_SAP = "REV"
        'InfoCache.ESTATUS_SAP = "GT TEST"

        Me.Show()
        My.Application.DoEvents()
        If DisplayLoginForm() Then
            GetCalendarioDeFechas()
            If My.Settings.PathFiles.Length = 0 Then
                With New Bitacoras
                    .ShowDialog()
                End With
            End If
            StatusBarSetFechas()
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In DatosMenu.VSI_PROCESOS
                AsignarPaisSucursal(Tablerow)
            Next
            APSset()
            GetNombreUsuario()
            Me.InterfacesControl.InterfacesRS = DatosMenu
            Me.PolizasControl1.InterfacesRS = DatosMenu
            Me.PolizasControl1.DatosCalendarioRS = DatosCalendario

        Else
            Me.Close()
        End If
        'RestoreWindowSettings()
    End Sub
#Region "metodos"
    Private Function DisplayLoginForm() As DialogResult
        Try

            Dim lForm As New LoginForm()
            Dim result As Windows.Forms.DialogResult = lForm.ShowDialog()
            If Windows.Forms.DialogResult.OK Then
                DatosMenu = lForm.MenuDatos
            End If
            Me.Refresh()
            Return result
        Catch ex As Exception
            Return Windows.Forms.DialogResult.Abort
            'Globales.DisplayError("The email message could not be created.", ex)
        End Try
    End Function
    Private Sub AsignarPaisSucursal(ActualRow As RunIsapData.VSI_PROCESOSRow)
        If ActualRow.SUC_CLAVE = 0 Then
            ActualRow.PAIS_DESCRIPCION = NombrePais(ActualRow.PAIS_CLAVE)
        End If

        For Each Tablerow As RunIsapData.VSI_SUCURSALESRow In DatosMenu.VSI_SUCURSALES
            If Tablerow.SUCURSAL_CLAVE = ActualRow.SUC_CLAVE Then
                ActualRow.SUCURSAL_DESCRIPCION = Tablerow.SUCURSAL_DESCRIPCION
                ActualRow.PAIS_DESCRIPCION = Tablerow.PAIS_DESCRIPCION
                ActualRow.PAIS_CLAVE = Tablerow.PAIS_CLAVE

            End If
        Next

    End Sub

    Private Function NombrePais(ClavePAIS As Decimal) As String
        Dim Nombre As String = String.Empty
        For Each Tablerow As RunIsapData.VSI_SUCURSALESRow In DatosMenu.VSI_SUCURSALES
            If Tablerow.PAIS_CLAVE = ClavePAIS Then
                Nombre = Tablerow.PAIS_DESCRIPCION
                Exit For
            End If
        Next
        Return Nombre
    End Function
    Private Sub APSset()
        
        For Each Tablerow As CalendarioData.CALENDARIO_PAISRow In DatosCalendario.CALENDARIO_PAIS
            Tablerow.APS = "A" & _
            Format(Tablerow.CFE_EJERCICIO, "0000").Remove(0, 2) & _
            "P" & _
            Format(Tablerow.CFE_PERIODO, "00") & _
            "S" & _
            Format(Tablerow.CFE_SEMANA, "0")

        Next
    End Sub
    Private Sub ProramarCronometro()
        With New ProgramarTimerForm
            .ShowDialog()
            If .DialogResult = Windows.Forms.DialogResult.OK Then
                Me.Timer1.Enabled = True
                Me.Timer1.Interval = 2000
                InicioProceso = .IniciarDate
                'inhabilitar botones de ejecuciones de interface y polizas
                TareaEnProgeso = True
                MsgBox("Ha iniciado el cronómetro")
                InhabilitarBotones()
                Me.CronometroLabel.ForeColor = Color.Red
                Me.CronometroLabel.Text = "Cronómetro activado"



            Else
                TareaEnProgeso = False
                'Habilitar botones de ejecuciones de interface y polizas
                MsgBox("Ha detenido el cronómetro")
                HabilitarBotones()
                Me.CronometroLabel.ForeColor = Color.Gray
                Me.CronometroLabel.Text = "Cronómetro inactivo"
            End If
        End With
        My.Application.DoEvents()

    End Sub
    Private Sub GetNombreUsuario()
        Dim q = From p In DatosMenu.VSI_PROCESOS
        Dim Registro = q.First
        InfoCache.UId = Registro.USERNAME

        Me.PaisLabel.Text = InfoCache.UId
    End Sub
    Private Sub GetCalendarioDeFechas()
        DatosCalendario = (New MenuSP).CalendarioSelectData()
    End Sub

    'Private Sub Procesar()


    '    With DatosMenu.VSI_PROCESOS
    '        For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
    '            If Tablerow.Ejecutar Then
    '                'percentComplete += 10
    '                'worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " " & FechaDesde.ToString)

    '                Dim obj As New ProcesoSP
    '                AddHandler obj.Mensaje, AddressOf EClass_EventHandler
    '                Dim result As Integer = obj.EjecutarSP(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta)
    '                'Dim Resultado As Integer = (New ProcesoSP).EjecutarSP(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta)
    '                If result = -1 Then
    '                    Tablerow.Ejecutar = False
    '                Else

    '                End If

    '                totalTime = Microsoft.VisualBasic.DateAndTime.Timer - start

    '                'worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " ")
    '                'worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Duración: " & totalTime.ToString & " segundos")
    '                'worker.ReportProgress(percentComplete Mod 100, "=================================================")
    '            End If
    '        Next
    '    End With

    'End Sub
#End Region
    '#Region " Data Fase 1"

    '    Private Sub Fase1Datos()
    '        'updateStatus("Un momento...")

    '        'Me.ProgressBar.Value = Me.ProgressBar.Minimum
    '        My.Application.DoEvents()

    '        Dim myWatch As Stopwatch = New Stopwatch()
    '        myWatch.Start()

    '        Me.Fase1BGW.RunWorkerAsync()
    '        MsgBox(InfoCache.UpdateError)
    '        myWatch.Stop()
    '        'updateStatus(myWatch.ElapsedMilliseconds.ToString() + " ms")

    '    End Sub
    '    Sub Fase1BGW_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
    '        e.Result = Fase1DatosAsync(CInt(e.Argument), CType(sender, BackgroundWorker), e)
    '    End Sub
    '    Sub Fase1BGW_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
    '        'updateProgress(e.ProgressPercentage)
    '        DisplayStatus(e.UserState.ToString)
    '        StatusLabel.Text = e.UserState.ToString
    '    End Sub
    '    Sub Fase1BGW_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
    '        'Me.AñoLabel.Text = "Año fiscal " & InfoCache.AñoIniciaBudget.ToString
    '        'If InfoCache.UserID = 0 Then
    '        '    MsgBox("No se pudo validar el nombre de usuario. Por favor, Intente nuevamente")
    '        '    Me.Close()
    '        'Else
    '        '    Fase2Datos()
    '        '    LoadVentanas()
    '        'End If
    '    End Sub
    '    Private Function Fase1DatosAsync(ByVal start As Integer, ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs) As Integer
    '        Dim percentComplete As Integer = start
    '        With DatosMenu.VSI_PROCESOS
    '            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
    '                If Tablerow.Ejecutar Then
    '                    percentComplete += 10
    '                    'worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " " & FechaDesde.ToString)

    '                    Dim obj As New ProcesoSP
    '                    AddHandler obj.Mensaje, AddressOf EClass_EventHandler
    '                    'Dim result As Integer = obj.EjecutarSP(Tablerow.PROCEDURE_NAME, Me.FechaDesde.DateTime, Me.FechaHasta.DateTime)
    '                    Dim Resultado As Integer = (New ProcesoSP).EjecutarSP(Tablerow.PROCEDURE_NAME, InfoCache.FechaDesde, InfoCache.FechaHasta)

    '                    totalTime = Microsoft.VisualBasic.DateAndTime.Timer - start

    '                    worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " ")
    '                    worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Duración: " & totalTime.ToString & " segundos")
    '                    worker.ReportProgress(percentComplete Mod 100, "=================================================")
    '                End If
    '            Next
    '        End With

    '        'Dim BudgetDT As DataTable = (New MgBudgetsSystem).BudgetAbierto()
    '        'InfoCache.IdBudget = CInt(BudgetDT.Rows(0)("IdBudget"))
    '        'InfoCache.AñoIniciaBudget = CInt(BudgetDT.Rows(0)("AñoInicia"))


    '        Dim charSeparators() As Char = {","c}
    '        'Dim DatosUsuario As String = (New SacSolicitantesSystem).DatosDeUsuario()
    '        'Dim result() As String = DatosUsuario.Split(charSeparators, 4, StringSplitOptions.RemoveEmptyEntries)
    '        percentComplete += 10
    '        worker.ReportProgress(percentComplete Mod 100, " Creando conjunto de datos: Datos del usuario")
    '        'If result.Length > 0 Then
    '        '    InfoCache.UserID = Convert.ToInt32(result(0))
    '        '    InfoCache.UserGroupID = Convert.ToInt32(result(1))
    '        '    InfoCache.UserFullName = Convert.ToString(result(2))
    '        '    InfoCache.GroupName = Convert.ToString(result(3))
    '        'Else
    '        '    InfoCache.UserID = 0
    '        'End If
    '        percentComplete += 5
    '        worker.ReportProgress(percentComplete Mod 100, " Creando conjunto de datos: Actualizando cache")
    '        Return 1
    '    End Function

    '#End Region

#Region "Mensajes"
    'Sub EClass_EventHandler(ByVal mensaje As String)
    '    DisplayStatus(mensaje)
    'End Sub
    'Private Sub DisplayStatus(ByVal Status As String)
    '    StatusTextBox.AppendText(Status & Environment.NewLine)
    '    ScrollToBottom()
    '    My.Application.DoEvents()
    'End Sub
    'Private Sub ScrollToBottom()
    '    StatusTextBox.Select()
    '    StatusTextBox.SelectionStart = StatusTextBox.Text.Length
    '    StatusTextBox.ScrollToCaret()
    'End Sub
    Private Sub StatusLabelDisplay(ByVal ThisText As String)
        Me.StatusLabel.Text = ThisText
    End Sub
    Private Sub StatusBarSetFechas()
        Me.StatusPeriodo.Text = Format(My.Settings.FechaDesde, "ddd dd/MM/yyyy") & " - " & Format(My.Settings.FechaHasta, "ddd dd/MM/yyyy")
    End Sub
#End Region

#Region "Eventos"
    Private Sub UbicacionDeBitacorasToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UbicacionDeBitacorasToolStripMenuItem.Click
        With New Bitacoras
            .ShowDialog()
        End With
    End Sub
    Private Sub SemanaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SemanaToolStripMenuItem.Click
        With New SetFechasForm
            .ShowDialog()
            If .DialogResult = Windows.Forms.DialogResult.OK Then
                GetCalendarioDeFechas()
                APSset()
                Me.PolizasControl1.DatosCalendarioRS = DatosCalendario
            End If
        End With
        StatusBarSetFechas()
    End Sub
    Private Sub StatusButton_Click(sender As System.Object, e As System.EventArgs) Handles StatusButton.Click
        With New CambioStatusForm
            .InterfacesRS = DatosMenu
            .DatosCalendarioRS = DatosCalendario
            .ShowDialog()
        End With
    End Sub
    Private Sub TerminarToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TerminarToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub CognosButton_Click(sender As System.Object, e As System.EventArgs) Handles CognosButton.Click
        With New CognosForm
            .ShowDialog()
        End With
    End Sub
    Private Sub AcercaDeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AcercaDeToolStripMenuItem.Click
        With New AboutBox1
            .ShowDialog()
        End With
    End Sub

#End Region
    '#Region "ConfigurarGrid"


    '    Private Sub ConfigurarGrid()
    '        Dim columnHeaderStyle As New DataGridViewCellStyle()
    '        columnHeaderStyle.BackColor = Color.Beige
    '        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


    '        Dim TotalGridWidth As Integer = Me.GridDatos.Width - 48

    '        Dim PAIS_DESCRIPCIONColumn As New DataGridViewTextBoxColumn()
    '        Dim SUCURSAL_DESCRIPCIONColumn As New DataGridViewTextBoxColumn()
    '        Dim PROCEDURE_NAMEColumn As New DataGridViewTextBoxColumn()
    '        Dim EjecutarColumn As New DataGridViewCheckBoxColumn()

    '        With EjecutarColumn
    '            .HeaderText = "Ejecutar"
    '            .Name = "Ejecutar"
    '            .DataPropertyName = "Ejecutar"
    '            .Width = CInt(TotalGridWidth * 0.1)
    '            .DefaultCellStyle.SelectionBackColor = Color.Red
    '            .ReadOnly = False
    '        End With

    '        With PAIS_DESCRIPCIONColumn
    '            .HeaderText = "Pais"
    '            .Name = "PAIS_DESCRIPCION"
    '            .DataPropertyName = "PAIS_DESCRIPCION"
    '            .Width = CInt(TotalGridWidth * 0.25)
    '            .DefaultCellStyle.SelectionBackColor = Color.Red
    '            .ReadOnly = True
    '        End With
    '        With SUCURSAL_DESCRIPCIONColumn
    '            .HeaderText = "Sucursal"
    '            .Name = "SUCURSAL_DESCRIPCION"
    '            .DataPropertyName = "SUCURSAL_DESCRIPCION"
    '            .Width = CInt(TotalGridWidth * 0.25)
    '            '.DefaultCellStyle.SelectionBackColor = Color.Red
    '            .ReadOnly = True
    '        End With
    '        With PROCEDURE_NAMEColumn
    '            .HeaderText = "PROCEDURE_NAME"
    '            .Name = "PROCEDURE_NAME"
    '            .DataPropertyName = "PROCEDURE_NAME"
    '            .Width = CInt(TotalGridWidth * 0.4)
    '            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    '            .ReadOnly = True
    '        End With


    '        With Me.GridDatos
    '            .Columns.Add(EjecutarColumn)
    '            .Columns.Add(PAIS_DESCRIPCIONColumn)
    '            .Columns.Add(SUCURSAL_DESCRIPCIONColumn)
    '            .Columns.Add(PROCEDURE_NAMEColumn)

    '            .AutoSizeColumnsMode = _
    '                DataGridViewAutoSizeColumnsMode.AllCells

    '            '.RowHeadersWidth = CInt(TotalGridWidth * 0.09)
    '            .AutoGenerateColumns = False
    '            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    '            .ColumnHeadersDefaultCellStyle = columnHeaderStyle
    '            .ReadOnly = False
    '            .RowsDefaultCellStyle.BackColor = Color.PowderBlue
    '            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
    '            .AllowUserToAddRows = False
    '            .AllowUserToDeleteRows = False
    '            .SelectionMode = DataGridViewSelectionMode.CellSelect
    '        End With

    '    End Sub
    '#End Region
#Region "Bitacora"
    Private Sub guardarBitacora()
        Dim LogFileName As String = My.Settings.PathFiles & "\" & Format(Date.Today, "yyyyMMMdd") & " " & Format(Date.Now, "HHmm") & ".log"
        Using sw As StreamWriter = New StreamWriter(LogFileName)
            sw.WriteLine(StatusTextBox.Text)
        End Using
    End Sub
#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        'Me.Fase1BGW = New BackgroundWorker()
        'Me.Fase1BGW.WorkerReportsProgress = True
        'AddHandler Me.Fase1BGW.DoWork, New DoWorkEventHandler(AddressOf Fase1BGW_DoWork)
        'AddHandler Me.Fase1BGW.ProgressChanged, New ProgressChangedEventHandler(AddressOf Fase1BGW_ProgressChanged)
        'AddHandler Me.Fase1BGW.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Fase1BGW_RunWorkerCompleted)

        'Me.Fase2BGW = New BackgroundWorker()
        'Me.Fase2BGW.WorkerReportsProgress = True
        'AddHandler Me.Fase2BGW.DoWork, New DoWorkEventHandler(AddressOf Fase2BGW_DoWork)
        'AddHandler Me.Fase2BGW.ProgressChanged, New ProgressChangedEventHandler(AddressOf Fase2BGW_ProgressChanged)
        'AddHandler Me.Fase2BGW.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Fase2BGW_RunWorkerCompleted)

        'ConfigurarGrid()


    End Sub
#Region "Cronometro/programar tareas"

    Private Sub CronometroLabel_DoubleClick(sender As Object, e As System.EventArgs) Handles CronometroLabel.DoubleClick

    End Sub
    Private Sub ProgramarTareaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ProgramarTareaToolStripMenuItem.Click
        ProramarCronometro()

    End Sub


    Private Sub InhabilitarBotones()
        Me.SemanaToolStripMenuItem.Enabled = False
        'Me.ProcesarButton.Enabled = False
        'Me.GenerarPolizasButton.Enabled = False
        'Me.StatusButton.Enabled = False

    End Sub
    Private Sub HabilitarBotones()
        Me.SemanaToolStripMenuItem.Enabled = True
        'Me.ProcesarButton.Enabled = True
        'Me.GenerarPolizasButton.Enabled = True
        'Me.StatusButton.Enabled = True
    End Sub
    Private Sub iniciarProcesoProgramado()
        Me.CronometroLabel.ForeColor = Color.Gray
        Me.CronometroLabel.Text = "Cronómetro inactivo"
        Me.Timer1.Enabled = False
        TareaEnProgeso = False
        Application.DoEvents()
        Me.InterfacesControl.Procesar()


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        If Date.Now >= InicioProceso Then
            iniciarProcesoProgramado()
        End If
    End Sub


    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        guardarBitacora()

        If TareaEnProgeso = True Then
            Dim mensaje As String = "Hay una tarea programada que no se ha ejecutado." & vbCrLf & "Realmente desea terminar el programa?" & vbCrLf & "Haga clic en Si para terminar o No para cancelar"

            Dim result As DialogResult = MsgBox(mensaje, MsgBoxStyle.Critical Or MsgBoxStyle.YesNo)
            If result <> Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region






End Class