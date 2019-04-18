Imports System.IO
Imports System.ComponentModel

Public Class ISAPMainForm
#Region " Internal members "
    Private Fase1BGW As BackgroundWorker
    Private Fase2BGW As BackgroundWorker
#End Region

    Private ts As TimeSpan
    Private startDate As DateTime
    Private endDate As DateTime
    Private DatosMenu As RunIsapData
    Private ModuloActual As String = "ISAPmenu"
    Dim start, finish, totalTime As Double
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        'RestoreWindowSettings()
        InfoCache.ConnectionString = "Data Source=ODSCA;User Id=ODSCA;Password=PRO56KAL"

        Me.FechaDesde.DateTime = "2010/11/1"

        Dim duration As System.TimeSpan

        duration = New System.TimeSpan(-3I, 0, 0, 0, 0)

        Me.FechaHasta.DateTime = Date.Today.Add(duration)
        'CargarDatosMenu()

    End Sub
#Region "metodos"
    Private Sub Autenticar()
        DatosMenu = (New MenuSP).MenuSelectData(Me.GPIDText.Text)
        If DatosMenu.VSI_PROCESOS.Rows.Count = 0 Then
            MsgBox("Ha ingresado un GPID invalido", MsgBoxStyle.Exclamation And MsgBoxStyle.OkOnly, "GPID invalido")
        Else
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In DatosMenu.VSI_PROCESOS
                AsignarPaisSucursal(Tablerow)
            Next
            Me.GridDatos.DataSource = DatosMenu.VSI_PROCESOS.DefaultView
        End If
    End Sub
    Private Sub AsignarPaisSucursal(ActualRow As RunIsapData.VSI_PROCESOSRow)
        For Each Tablerow As RunIsapData.VSI_SUCURSALESRow In DatosMenu.VSI_SUCURSALES
            If Tablerow.SUCURSAL_CLAVE = ActualRow.SUC_CLAVE Then
                ActualRow.SUCURSAL_DESCRIPCION = Tablerow.SUCURSAL_DESCRIPCION
                ActualRow.PAIS_DESCRIPCION = Tablerow.PAIS_DESCRIPCION
            End If
        Next

    End Sub
    Private Sub Procesar()
        Fase1Datos()
    End Sub
#End Region
#Region " Data Fase 1"

    Private Sub Fase1Datos()
        'updateStatus("Un momento...")

        'Me.ProgressBar.Value = Me.ProgressBar.Minimum
        My.Application.DoEvents()

        Dim myWatch As Stopwatch = New Stopwatch()
        myWatch.Start()

        Me.Fase1BGW.RunWorkerAsync()

        myWatch.Stop()
        'updateStatus(myWatch.ElapsedMilliseconds.ToString() + " ms")

    End Sub
    Sub Fase1BGW_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        e.Result = Fase1DatosAsync(CInt(e.Argument), CType(sender, BackgroundWorker), e)
    End Sub
    Sub Fase1BGW_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        'updateProgress(e.ProgressPercentage)
        DisplayStatus(e.UserState.ToString)
    End Sub
    Sub Fase1BGW_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        'Me.AñoLabel.Text = "Año fiscal " & InfoCache.AñoIniciaBudget.ToString
        'If InfoCache.UserID = 0 Then
        '    MsgBox("No se pudo validar el nombre de usuario. Por favor, Intente nuevamente")
        '    Me.Close()
        'Else
        '    Fase2Datos()
        '    LoadVentanas()
        'End If
    End Sub
    Private Function Fase1DatosAsync(ByVal start As Integer, ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs) As Integer
        Dim percentComplete As Integer = start
        With DatosMenu.VSI_PROCESOS
            For Each Tablerow As RunIsapData.VSI_PROCESOSRow In .Rows
                If Tablerow.Ejecutar Then
                    percentComplete += 10
                    worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " " & FechaDesde.ToString)

                    Dim obj As New ProcesoSP
                    AddHandler obj.Mensaje, AddressOf EClass_EventHandler
                    Dim result As Integer = obj.EjecutarSP(Tablerow.PROCEDURE_NAME, Me.FechaDesde.DateTime, Me.FechaHasta.DateTime)
                    Dim Resultado As Integer = (New ProcesoSP).EjecutarSP(Tablerow.PROCEDURE_NAME, Me.FechaDesde.DateTime, Me.FechaHasta.DateTime)

                    totalTime = Microsoft.VisualBasic.DateAndTime.Timer - start

                    worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Proceso: " & Tablerow.PROCEDURE_NAME & " ")
                    worker.ReportProgress(percentComplete Mod 100, Format(Date.Now, "R") & "> Duración: " & totalTime.ToString & " segundos")
                    worker.ReportProgress(percentComplete Mod 100, "=================================================")
                End If
            Next
        End With

        'Dim BudgetDT As DataTable = (New MgBudgetsSystem).BudgetAbierto()
        'InfoCache.IdBudget = CInt(BudgetDT.Rows(0)("IdBudget"))
        'InfoCache.AñoIniciaBudget = CInt(BudgetDT.Rows(0)("AñoInicia"))
       

        Dim charSeparators() As Char = {","c}
        'Dim DatosUsuario As String = (New SacSolicitantesSystem).DatosDeUsuario()
        'Dim result() As String = DatosUsuario.Split(charSeparators, 4, StringSplitOptions.RemoveEmptyEntries)
        percentComplete += 10
        worker.ReportProgress(percentComplete Mod 100, " Creando conjunto de datos: Datos del usuario")
        'If result.Length > 0 Then
        '    InfoCache.UserID = Convert.ToInt32(result(0))
        '    InfoCache.UserGroupID = Convert.ToInt32(result(1))
        '    InfoCache.UserFullName = Convert.ToString(result(2))
        '    InfoCache.GroupName = Convert.ToString(result(3))
        'Else
        '    InfoCache.UserID = 0
        'End If
        percentComplete += 5
        worker.ReportProgress(percentComplete Mod 100, " Creando conjunto de datos: Actualizando cache")
        Return 1
    End Function

#End Region
#Region "Bitacora"
    Private Sub guardarBitacora()
        'Dim LogFileName As String = My.Settings.PathFiles & "\" & Format(Date.Today, "yyyyMMMdd") & " " & Format(Date.Now, "HHmm") & ".log"
        'Using sw As StreamWriter = New StreamWriter(LogFileName)
        '    sw.WriteLine(StatusTextBox.Text)
        'End Using
    End Sub
#End Region
#Region "Mensajes"
    Sub EClass_EventHandler(ByVal mensaje As String)
        'DisplayStatus(mensaje)
    End Sub
    Private Sub DisplayStatus(ByVal Status As String)
        StatusTextBox.Text = StatusTextBox.Text & Status & Environment.NewLine
        ScrollToBottom()
        My.Application.DoEvents()
    End Sub
    Private Sub ScrollToBottom()
        StatusTextBox.Select()
        StatusTextBox.SelectionStart = StatusTextBox.Text.Length
        StatusTextBox.ScrollToCaret()
    End Sub
#End Region
#Region "Establecer Ruta"
    'Private Sub LocalizarBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocalizarBtn.Click
    '    EstablecerRuta()
    'End Sub
    Private Sub EstablecerRuta()
        With FolderBrowserDialog1
            .ShowNewFolderButton = True
            .Description = "Directorio de bitacoras"
            .RootFolder = Environment.SpecialFolder.MyDocuments
        End With
        'If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
        '    Me.PathTxt.Text = FolderBrowserDialog1.SelectedPath
        'End If

        ''TODO: CREAR SETTING DE PATHFILE
        'If Not My.Computer.FileSystem.DirectoryExists(My.Settings.PathFiles & "\Procesados") Then
        '    My.Computer.FileSystem.CreateDirectory(My.Settings.PathFiles & "\Procesados")
        'End If

        'My.Settings.Save()
        'VerificarArchivos()
    End Sub
#End Region
#Region "Eventos"
    Private Sub AutenticarButton_Click(sender As System.Object, e As System.EventArgs) Handles AutenticarButton.Click
        Autenticar()
    End Sub

    Private Sub ProcesarButton_Click(sender As System.Object, e As System.EventArgs) Handles ProcesarButton.Click
        Procesar()
    End Sub
    Private Sub SatusChangeButton_Click(sender As System.Object, e As System.EventArgs) Handles SatusChangeButton.Click
        With New CambioStatusForm
            .ShowDialog()
        End With
    End Sub
#End Region
#Region "Otros"
    Private Sub StatusLabelDisplay(ByVal ThisText As String)
        'Me.StatusLabel.Text = ThisText
    End Sub

    Private Sub ConfigurarGrid()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridDatos.Width - 48

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

            .AutoSizeColumnsMode = _
                DataGridViewAutoSizeColumnsMode.AllCells

            '.RowHeadersWidth = CInt(TotalGridWidth * 0.09)
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
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
End Class
