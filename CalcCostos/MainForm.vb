Imports System.Text

Public Class MainForm 'COSTOS
    Private ts As TimeSpan
    Private startDate As DateTime
    Private endDate As DateTime
    Private ModuloActual As String = "COSTOS"
    Private MaestroDeCostos As CostosMasterData
    Private UsuarioInfo As UsuariosData
    Private Maestro As OSDMaestroData
    Private MovimientosDeProductos As MovimientosDeProductosData
    Private MovimientoSegregado As ODS_MOVIMIENTOPRODUCTO_SEGREGADO
    Private SapGLInterfase As SAP_GLINTERFASEData
    Private MovimientosTotalDeProductos As OSDMaestroData

    Private FiltroSucursal As String = String.Empty
    Private FiltroTipoembarque As String = String.Empty
    Private RegistrosNuevos As Integer

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        '1	Centro América
        '2	Guatemala
        '3	Honduras
        '4	Panamá
        '5	El Salvador
        '6	Costa Rica
        '7	Quaker Costa Rica


        MyBase.OnLoad(e)
        RestoreWindowSettings()
        'InfoCache.ConnectionString = "Data Source=ODSCA;User Id=prodods;Password=managerjr"
        InfoCache.ConnectionString = "Data Source=ODS.PEPWDR00474;User Id=ODSCA;Password=PRO56KAL"
        Me.TextExtra = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Revision

        If DisplayLoginForm() = Windows.Forms.DialogResult.OK Then
            My.Application.DoEvents()
            CrearDirectorio()
            CargarDatosUsuario()
            CargarMaestrocostos()
            Select Case InfoCache.PaisClave
                Case 2, 3
                    Me.TipoDeCambio.Enabled = True
                    KryptonContextMenuItem2.Enabled = True
                Case Else
                    Me.TipoDeCambio.Enabled = False
                    KryptonContextMenuItem2.Enabled = False
            End Select
            If My.Settings.FExcel.Length > 0 Then
                Select Case My.Settings.ToString
                    Case "Excel 2003 y anterior"
                        InfoCache.VersionExcel = 1
                    Case "Excel 2007 a 2010"
                        InfoCache.VersionExcel = 2
                End Select
            Else
                InfoCache.VersionExcel = -1
            End If
            RegistrosNuevos = -1
            Do While RegistrosNuevos <> 0
                CargarEmbarques()
            Loop

         
        Else
            Me.Close()
        End If
    End Sub
    Private Sub CrearDirectorio()
        If Not My.Computer.FileSystem.DirectoryExists(Constantes.FilesGlobalDirectory) Then
            My.Computer.FileSystem.CreateDirectory(Constantes.FilesGlobalDirectory)
        End If

        Dim ImageFileLocation As String = My.Application.Info.DirectoryPath & "\LCostos.bmp"
        If Not My.Computer.FileSystem.FileExists(ImageFileLocation) Then
            My.Resources.CARICAM_Oficial.Save(ImageFileLocation)

        End If

    End Sub
#Region " Save and restoring setting "

    Private Sub RestoreWindowSettings()
        Try

            'InfoCache.UId = My.Settings.UserName
            'InfoCache.ServerHost = My.Settings.ServerHost
            'InfoCache.QueryTimeOut = My.Settings.QueryTimeOut
            'InfoCache.PacketSize = My.Settings.PacketSize
            'InfoCache.Catalogo = My.Settings.Catalogo
            Dim bounds() As String = My.Settings.MainFormPlacement.Split(","c)
            If bounds.Length = 4 Then
                Dim boundsRect As Rectangle = New Rectangle( _
                 CInt(bounds(0)), CInt(bounds(1)), _
                 CInt(bounds(2)), CInt(bounds(3)))

                boundsRect = Rectangle.Intersect(Screen.PrimaryScreen.WorkingArea, boundsRect)
                If boundsRect.Width > 0 And boundsRect.Height > 0 Then
                    ' set the window location and size
                    Me.StartPosition = FormStartPosition.Manual
                    Me.SetBounds(boundsRect.Left, boundsRect.Top, _
                     boundsRect.Width, boundsRect.Height)
                End If
            End If
        Catch ex As Exception
            Me.StartPosition = FormStartPosition.WindowsDefaultLocation
        End Try
    End Sub
    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        MyBase.OnClosing(e)
        My.Settings.MainFormPlacement = String.Format("{0},{1},{2},{3}", _
                 Me.Bounds.X, Me.Bounds.Y, _
                 Me.Bounds.Width, Me.Bounds.Height)

        My.Settings.Save()
    End Sub



#End Region
#Region "Datos Maestros"
    Sub CargarDatosUsuario()
        StatusLabelDisplay("Cargando datos del usuario")
        My.Application.DoEvents()

        InfoCache.TipoDeCambio = CDec(Me.TipoDeCambio.Text)

        UsuarioInfo = (New Usuarios).SelectDataUsuarios()
        If UsuarioInfo.DIC_USUARIOS.Count > 0 Then
            With UsuarioInfo.DIC_USUARIOS

                For Each tablerow As UsuariosData.DIC_USUARIOSRow In .Rows
                    'Me.NombreUsuario.Text = tablerow.NOMBRE
                    InfoCache.NombrePais = tablerow.PAIS_NOMBRE
                    'Me.Moneda.Text = tablerow.MONEDA
                    'Me.ClaveCia_Label.Text = tablerow.CLAVE_CIA
                    InfoCache.NombreUsuario = tablerow.NOMBRE
                    InfoCache.PaisClave = tablerow.PAIS_CLAVE
                    'InfoCache.MonedaClave = tablerow.MONEDA
                    InfoCache.CurrencyCode = tablerow.MONEDA
                    InfoCache.ClaveCompania = tablerow.CLAVE_CIA

                Next

            End With
            'Me.ProcesarButton.Enabled = True
            StatusLabelDisplay("Listo. Presione boton <Procesar> para iniciar.")
            My.Application.DoEvents()
        Else
            StatusLabelDisplay("Datos no encontrados")
            My.Application.DoEvents()
        End If

    End Sub
    Private Sub CargarMaestrocostos()
        StatusLabelDisplay("Cargando maestro de costos")
        My.Application.DoEvents()
        MaestroDeCostos = (New CostosDML).MaestroCostosSelectData()
    End Sub
  

#End Region
#Region "Metodos"
    Private Sub Promediar()
        If Not ValidarTipoCambioAsignado() Then
            MsgBox("No ha asignado el tipo de cambio a esta liquidacion.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "LCostos")
            Exit Sub
        End If
        If Not ValidarPrecioUnitario() Then
            Exit Sub
        End If

        With New CosteoPromedio(Me.EMBARQUE_CLAVE.Text)

        End With
        UpdateStatus("C")


    End Sub
    Private Sub UpdateStatus(ByVal NuevoStatus As String)

        Dim q = (From c In InfoCache.EmbarquesDS.EMBARQUES _
        Where c.EMBARQUE_CLAVE = Me.EMBARQUE_CLAVE.Text _
        Select c).First()

        q.STATUS = NuevoStatus
    End Sub
    Private Sub CerrarPeriodo()
        Dim lForm As New CierreForm()
        Dim result As Windows.Forms.DialogResult = lForm.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then
            RegistrosNuevos = -1
            Do While RegistrosNuevos <> 0
                CargarEmbarques()
            Loop
        End If


    End Sub
#End Region
#Region "Embarques"
    Private Sub CargarEmbarques()
        My.Application.DoEvents()
        Dim lForm As New CargaDatosForm
        lForm.Show()

        If Not IsNothing(InfoCache.EmbarquesDS) Then
            InfoCache.EmbarquesDS.Dispose()
            InfoCache.NuevosEmbarquesDS.Dispose()
        End If
        InfoCache.NuevosEmbarquesDS = New EmbarquesData
        InfoCache.EmbarquesDS = (New CostosDML).EmbarquesSelectData()
        
        My.Application.DoEvents()
        AsignarEMBARQUE_CLAVE()
        RegistrosNuevos = 0
        RegistrarEmbarquesOrigen()
        If RegistrosNuevos > 0 Then
            Insertar()
            MsgBox("Se registraron " & RegistrosNuevos.ToString & " embarques nuevos", MsgBoxStyle.OkOnly And MsgBoxStyle.Information, "Nuevos registros")
            RegistrosNuevos = 0
        Else
            MsgBox("No se encontraron embarques nuevos", MsgBoxStyle.OkOnly And MsgBoxStyle.Information, "Nuevos registros")
        End If


        Me.EMBARQUE_CLAVE.DataBindings.Clear()
        Me.EMBARQUE_CLAVE.DataBindings.Add("Text", InfoCache.EmbarquesDS.EMBARQUES.DefaultView, "EMBARQUE_CLAVE")

        Me.GridDatos.DataSource = InfoCache.EmbarquesDS.EMBARQUES.DefaultView
        Me.EmbarqueDetalle.Bind2Control()
        Me.EmbarqueDetalle.GridDatos.DataSource = InfoCache.EmbarquesDS.EMBARQUESDETALLES.DefaultView
        My.Application.DoEvents()
        lForm.Close()
    End Sub
    Private Sub AsignarEMBARQUE_CLAVE()
        With InfoCache.EmbarquesDS.EMBARQUESORIGEN
            Dim tablerow As EmbarquesData.EMBARQUESORIGENRow
            For Each tablerow In .Rows
                Select Case InfoCache.PaisClave
                    Case 2
                        If _
                            tablerow.MOV_CLAVE = 1 Or _
                            tablerow.MOV_CLAVE = 25 Or _
                            tablerow.MOV_CLAVE = 44 Or _
                            tablerow.MOV_CLAVE = 50 Or _
                            tablerow.MOV_CLAVE = 96 Then

                            tablerow.EMBARQUE_CLAVE = _
                                               InfoCache.ClaveCompania & _
                                               "-" & _
                                               Format(tablerow.SUC_CLAVE, "0000") & _
                                               "-" & _
                                               Format(tablerow.MOV_CLAVE, "00") & _
                                               "-" & _
                                               Format(tablerow.MMP_FOLIO, "00000000") & _
                                               "-" & _
                                               Format(tablerow.MMP_FECHA, "yyyyMMdd")
                        End If
                    Case 3
                        If _
                          tablerow.MOV_CLAVE = 1 Or _
                          tablerow.MOV_CLAVE = 25 Or _
                          tablerow.MOV_CLAVE = 29 Then

                            tablerow.EMBARQUE_CLAVE = _
                                               InfoCache.ClaveCompania & _
                                               "-" & _
                                               Format(tablerow.SUC_CLAVE, "0000") & _
                                               "-" & _
                                               Format(tablerow.MOV_CLAVE, "00") & _
                                               "-" & _
                                               Format(tablerow.MMP_FOLIO, "00000000") & _
                                               "-" & _
                                               Format(tablerow.MMP_FECHA, "yyyyMMdd")
                        End If
                    Case 4
                        If tablerow.MOV_CLAVE = 26 Or _
                            tablerow.MOV_CLAVE = 52 Or _
                            tablerow.MOV_CLAVE = 53 Or _
                            tablerow.MOV_CLAVE = 54 Or _
                            tablerow.MOV_CLAVE = 55 Or _
                            tablerow.MOV_CLAVE = 56 Or _
                            tablerow.MOV_CLAVE = 57 Or _
                            tablerow.MOV_CLAVE = 58 Or _
                            tablerow.MOV_CLAVE = 78 Or _
                            tablerow.MOV_CLAVE = 81 Then

                            tablerow.EMBARQUE_CLAVE = _
                                               InfoCache.ClaveCompania & _
                                               "-" & _
                                               Format(tablerow.SUC_CLAVE, "0000") & _
                                               "-" & _
                                               Format(tablerow.MOV_CLAVE, "00") & _
                                               "-" & _
                                               Format(tablerow.MMP_FOLIO, "00000000") & _
                                               "-" & _
                                               Format(tablerow.MMP_FECHA, "yyyyMMdd")
                        End If
                    Case 5
                        If _
                            tablerow.MOV_CLAVE = 1 Or _
                            tablerow.MOV_CLAVE = 2 Or _
                            tablerow.MOV_CLAVE = 29 Or _
                            tablerow.MOV_CLAVE = 33 Then

                            tablerow.EMBARQUE_CLAVE = _
                                               InfoCache.ClaveCompania & _
                                               "-" & _
                                               Format(tablerow.SUC_CLAVE, "0000") & _
                                               "-" & _
                                               Format(tablerow.MOV_CLAVE, "00") & _
                                               "-" & _
                                               Format(tablerow.MMP_FOLIO, "00000000") & _
                                               "-" & _
                                               Format(tablerow.MMP_FECHA, "yyyyMMdd")
                        End If
                    Case 6
                        If tablerow.MOV_CLAVE = 26 Or _
                            tablerow.MOV_CLAVE = 54 Or _
                            tablerow.MOV_CLAVE = 55 Or _
                            tablerow.MOV_CLAVE = 56 Or _
                            tablerow.MOV_CLAVE = 57 Or _
                            tablerow.MOV_CLAVE = 58 Or _
                            tablerow.MOV_CLAVE = 59 Or _
                            tablerow.MOV_CLAVE = 60 Or _
                            tablerow.MOV_CLAVE = 61  Then

                            tablerow.EMBARQUE_CLAVE = _
                                               InfoCache.ClaveCompania & _
                                               "-" & _
                                               Format(tablerow.SUC_CLAVE, "0000") & _
                                               "-" & _
                                               Format(tablerow.MOV_CLAVE, "00") & _
                                               "-" & _
                                               Format(tablerow.MMP_FOLIO, "00000000") & _
                                               "-" & _
                                               Format(tablerow.MMP_FECHA, "yyyyMMdd")
                        End If
                    Case 7
                        If tablerow.MOV_CLAVE = 26 Or _
                            tablerow.MOV_CLAVE = 54 Or _
                            tablerow.MOV_CLAVE = 55 Or _
                            tablerow.MOV_CLAVE = 56 Or _
                            tablerow.MOV_CLAVE = 57 Or _
                            tablerow.MOV_CLAVE = 58 Or _
                            tablerow.MOV_CLAVE = 59 Or _
                            tablerow.MOV_CLAVE = 60 Or _
                            tablerow.MOV_CLAVE = 61  Then
                            tablerow.EMBARQUE_CLAVE = _
                                               InfoCache.ClaveCompania & _
                                               "-" & _
                                               Format(tablerow.SUC_CLAVE, "0000") & _
                                               "-" & _
                                               Format(tablerow.MOV_CLAVE, "00") & _
                                               "-" & _
                                               Format(tablerow.MMP_FOLIO, "00000000") & _
                                               "-" & _
                                               Format(tablerow.MMP_FECHA, "yyyyMMdd")
                        End If
                End Select
                'CompanyCode.Suc_Clave.Mov_Clave.MMP_FOLIO.MMP_FECHA
                'GT10-0001 - 10 - 1704062 - 20101010)
            Next
        End With
    End Sub

    Private Sub RegistrarEmbarquesOrigen()
        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESORIGEN _
        Where p.EMBARQUE_CLAVE <> "A"
        Group p By p.EMBARQUE_CLAVE Into g = Group
        Select (New With {g, EMBARQUE_CLAVE})
        For Each secuencia In q

            If Not ExisteEmbarque(secuencia.EMBARQUE_CLAVE) Then
                RegistrosNuevos += 1
                With InfoCache.NuevosEmbarquesDS.EMBARQUES
                    Dim NewDataRow As EmbarquesData.EMBARQUESRow = .NewEMBARQUESRow
                    With NewDataRow
                        .EMBARQUE_CLAVE = secuencia.EMBARQUE_CLAVE
                        .MMP_FECHA = secuencia.g(0).MMP_FECHA
                        .MMP_FOLIO = secuencia.g(0).MMP_FOLIO
                        .PAIS_CLAVE = InfoCache.PaisClave
                        .SUC_CLAVE = secuencia.g(0).SUC_CLAVE
                        .SAP_FISCAL_YEAR = InfoCache.FiscalYear
                        .SAP_PERIOD = InfoCache.PeriodoActual
                        .MOV_CLAVE = secuencia.g(0).MOV_CLAVE
                        .STATUS = "A"
                        Select Case InfoCache.PaisClave
                            Case 2, 3 'Guatemala, honduras
                                If CDec(Me.TipoDeCambio.Text) > 0 Then
                                    .TIPO_CAMBIO = CDec(Me.TipoDeCambio.Text)
                                Else
                                    .TIPO_CAMBIO = 0
                                End If
                            Case 4, 5  'Panama y El Salvador
                                .TIPO_CAMBIO = 1
                            Case 6, 7 'Costa Rica y Quaker
                                .TIPO_CAMBIO = 0
                        End Select
                        If Not secuencia.g(0).IsMMP_NUMCONNull Then
                            .CONTROL_NO = secuencia.g(0).MMP_NUMCON
                        Else
                            .CONTROL_NO = String.Empty
                        End If
                        If Not secuencia.g(0).IsMMP_CONCEPNull Then
                            .CONCEPTO = secuencia.g(0).MMP_CONCEP
                        Else
                            .CONCEPTO = String.Empty
                        End If

                    End With
                    .AddEMBARQUESRow(NewDataRow)


                End With

                RegistrarOrigenDetalles(secuencia.EMBARQUE_CLAVE)

                With MaestroDeCostos.DEFINICIONGASTOS
                    For Each tableRow As CostosMasterData.DEFINICIONGASTOSRow In .Rows
                        With InfoCache.NuevosEmbarquesDS.EMBARQUESGASTOS
                            Dim NewDataRow As EmbarquesData.EMBARQUESGASTOSRow = .NewEMBARQUESGASTOSRow
                            With NewDataRow
                                .EMBARQUE_CLAVE = secuencia.EMBARQUE_CLAVE
                                .G_CLAVE = tableRow.G_CLAVE

                                .NOMBRE = tableRow.NOMBRE
                                .NIVEL = tableRow.NIVEL
                                .TIPO = tableRow.TIPO
                                .MONTO = 0
                                .CLASE = tableRow.CLASE
                            End With
                            .AddEMBARQUESGASTOSRow(NewDataRow)
                        End With
                    Next
                End With
            End If


        Next
    End Sub

    Private Sub RegistrarOrigenDetalles(ByVal EMBARQUECLAVE As String)

        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESORIGEN _
         Where p.EMBARQUE_CLAVE = EMBARQUECLAVE _
         Select p

        For Each secuencia In q
            With InfoCache.NuevosEmbarquesDS.EMBARQUESDETALLES
                Dim NewDataRow As EmbarquesData.EMBARQUESDETALLESRow = .NewEMBARQUESDETALLESRow
                With NewDataRow

                    .EMBARQUE_CLAVE = secuencia.EMBARQUE_CLAVE
                    .DMP_CANTID = secuencia.DMP_CANTID
                    .PRO_CLAVE = secuencia.PRO_CLAVE

                    If secuencia.IsPRO_GRAMAJNull Then
                        .PRO_GRAMAJ = 0
                        .KILOS = 0
                    Else
                        .PRO_GRAMAJ = secuencia.PRO_GRAMAJ
                        .KILOS = .PRO_GRAMAJ * .DMP_CANTID * 0.001
                    End If

                    .PRO_DESCRI = secuencia.PRO_DESCRI
                    If Not secuencia.IsDMP_PRECIONull Then
                        .DMP_PRECIO = secuencia.DMP_PRECIO
                        .PFOB = .DMP_CANTID * .DMP_PRECIO
                    Else
                        .DMP_PRECIO = 0
                        .PFOB = 0
                    End If


                    .SAP_NUM_MATERIAL_LEGADO = "A" ' tableRow.SAP_NUM_MATERIAL_LEGADO

                End With
                .AddEMBARQUESDETALLESRow(NewDataRow)
            End With
        Next

        'Next
        'With InfoCache.EmbarquesDS.EMBARQUESORIGEN
        '    For Each tableRow As EmbarquesData.EMBARQUESORIGENRow In .Rows

        '    Next

        'End With
    End Sub

    Private Sub Insertar()
        startDate = DateTime.Parse(Date.Now.ToString)
        InfoCache.UpdateError = String.Empty
        Dim result1 As Integer = (New CostosDML).insertarEmbarque()
        If InfoCache.UpdateError.Length > 0 Then
            MsgBox(InfoCache.UpdateError, MsgBoxStyle.Critical And MsgBoxStyle.OkOnly, "Error al registrar embarque")
        End If


        Dim result2 As Integer = (New CostosDML).insertarEmbarqueDetalle()
        Dim result4 As Integer = (New CostosDML).insertarEmbarqueGastos()

        endDate = DateTime.Parse(Date.Now.ToString)
        ts = endDate.Subtract(startDate).Duration

        Debug.Write("Tiempo ejecucion: " & ts.ToString & vbCrLf)
        Debug.Write("Embarque: " & result1.ToString & vbCrLf)
        Debug.Write("EmbarqueDetalle: " & result2.ToString & vbCrLf)
        Debug.Write("EmbarqueGL: " & result4.ToString & vbCrLf)

    End Sub

    Private Sub UpdateDatosEmbarques()
        Dim result1 As Integer = (New CostosDML).updateDatosEmbarques()
    End Sub

    

    Private Sub UpdateGastos()
        'Dim result1 As Integer = (New CostosDML).updateGastos()
    End Sub

    Private Sub AsignarTipoCambio()
        With InfoCache.EmbarquesDS.EMBARQUES
            For Each tablerow As EmbarquesData.EMBARQUESRow In .Rows
                tablerow.TIPO_CAMBIO = CDec(Me.TipoDeCambio.Text)
            Next
        End With
    End Sub
    Private Sub ImprimirExcel01()
        If Not EmbarqueHaSidoProcesado() Then
            MsgBox("No ha costeado esta liquidacion", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Informacion incompleta")
            Exit Sub
        End If
        If DatosEmbarqueIncompletos() Then
            Exit Sub
        End If
        CrearExcel01(Me.EMBARQUE_CLAVE.Text)



    End Sub
    Private Sub ImprimirExcel02()

    End Sub
    Private Sub ImprimirExcel03()

    End Sub
    Private Sub ImprimirExcel04()

    End Sub

#End Region
#Region "Otros"
    Private Sub StatusLabelDisplay(ByVal ThisText As String)
        Me.StatusLabel.Text = ThisText
    End Sub
    Private Function DisplayLoginForm() As DialogResult
        Try

            Dim lForm As New LoginForm(ModuloActual)
            Dim result As Windows.Forms.DialogResult = lForm.ShowDialog()
            If Windows.Forms.DialogResult.OK Then

                InfoCache.UId = lForm.UusarioActual
                InfoCache.ServerHost = lForm.ServerHostActual

            End If
            Me.Refresh()
            Return result
        Catch ex As Exception
            Return Windows.Forms.DialogResult.Abort
            'Globales.DisplayError("The email message could not be created.", ex)
        End Try
    End Function
    Private Sub ConfigurarGrid()
        Dim columnHeaderStyle As New DataGridViewCellStyle()
        columnHeaderStyle.BackColor = Color.Beige
        columnHeaderStyle.Font = New Font("Arial", 8.25, FontStyle.Bold)


        Dim TotalGridWidth As Integer = Me.GridDatos.Width - 48

        Dim ClaveColumn As New DataGridViewTextBoxColumn()
        Dim FolioColumn As New DataGridViewTextBoxColumn()
        Dim FechaColumn As New DataGridViewTextBoxColumn()
        Dim StatusColumn As New DataGridViewTextBoxColumn()
        Dim TipoCambioColumn As New DataGridViewTextBoxColumn()

        Dim FACTURA_NOColumn As New DataGridViewTextBoxColumn()
        Dim FECHA_FACTURAColumn As New DataGridViewTextBoxColumn()
        Dim CONTROL_NOColumn As New DataGridViewTextBoxColumn()
        Dim CONCEPTOColumn As New DataGridViewTextBoxColumn()
        Dim FECHA_INGRESOColumn As New DataGridViewTextBoxColumn()

        Dim PROVEEDOR_CLAVEColumn As New DataGridViewTextBoxColumn()
        Dim PROVEEDOR_NOMBREColumn As New DataGridViewTextBoxColumn()

        With ClaveColumn
            .HeaderText = "Clave de embarque"
            .Name = "EMBARQUE_CLAVE"
            .DataPropertyName = "EMBARQUE_CLAVE"
            .Width = CInt(TotalGridWidth * 0.2)
            .DefaultCellStyle.SelectionBackColor = Color.Red
            .ReadOnly = True
        End With
        With FolioColumn
            .HeaderText = "Folio DCT"
            .Name = "MMP_FOLIO"
            .DataPropertyName = "MMP_FOLIO"
            .Width = CInt(TotalGridWidth * 0.075)
            '.DefaultCellStyle.SelectionBackColor = Color.Red
            .ReadOnly = True
        End With
        With FechaColumn
            .HeaderText = "Fecha"
            .Name = "MMP_FECHA"
            .DataPropertyName = "MMP_FECHA"
            .Width = CInt(TotalGridWidth * 0.11)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .DefaultCellStyle.Format = "ddd dd/MM/yyyy"
        End With
        With StatusColumn
            .HeaderText = "Satus"
            .Name = "STATUS"
            .DataPropertyName = "STATUS"
            .Width = CInt(TotalGridWidth * 0.04)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ReadOnly = True
        End With
        With TipoCambioColumn
            .HeaderText = "Tipo Cambio"
            .Name = "TIPO_CAMBIO"
            .DataPropertyName = "TIPO_CAMBIO"
            .Width = CInt(TotalGridWidth * 0.075)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        With FACTURA_NOColumn
            .HeaderText = "Factura"
            .Name = "FACTURA"
            .DataPropertyName = "FACTURA_NO"
            .Width = CInt(TotalGridWidth * 0.1)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With FECHA_FACTURAColumn
            .HeaderText = "Fecha de factura"
            .Name = "FECHA_FACTURA"
            .DataPropertyName = "FECHA_FACTURA"
            .Width = CInt(TotalGridWidth * 0.1)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With CONTROL_NOColumn
            .HeaderText = "No. Control"
            .Name = "CONTROL_NO"
            .DataPropertyName = "CONTROL_NO"
            .Width = CInt(TotalGridWidth * 0.1)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With CONCEPTOColumn
            .HeaderText = "Concepto"
            .Name = "CONCEPTO"
            .DataPropertyName = "CONCEPTO"
            .Width = CInt(TotalGridWidth * 0.1)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With

        With FECHA_INGRESOColumn
            .HeaderText = "Fecha de Ingreso"
            .Name = "FECHA_INGRESO"
            .DataPropertyName = "FECHA_INGRESO"
            .Width = CInt(TotalGridWidth * 0.1)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
       

        With PROVEEDOR_CLAVEColumn
            .HeaderText = "Clave de Proveedor"
            .Name = "PROVEEDOR_CLAVE"
            .DataPropertyName = "PROVEEDOR_CLAVE"
            .Width = CInt(TotalGridWidth * 0.1)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With PROVEEDOR_NOMBREColumn
            .HeaderText = "Proveedor"
            .Name = "PROVEEDOR_NOMBRE"
            .DataPropertyName = "PROVEEDOR_NOMBRE"
            .Width = CInt(TotalGridWidth * 0.1)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With Me.GridDatos
            .Columns.Add(ClaveColumn)
            .Columns.Add(FolioColumn)
            .Columns.Add(FechaColumn)
            .Columns.Add(StatusColumn)
            .Columns.Add(TipoCambioColumn)
            .Columns.Add(FACTURA_NOColumn)
            .Columns.Add(FECHA_FACTURAColumn)
            .Columns.Add(CONTROL_NOColumn)
            .Columns.Add(CONCEPTOColumn)
            .Columns.Add(FECHA_INGRESOColumn)
            .Columns.Add(PROVEEDOR_CLAVEColumn)
            .Columns.Add(PROVEEDOR_NOMBREColumn)

            .AutoSizeColumnsMode = _
                DataGridViewAutoSizeColumnsMode.AllCells

            .RowHeadersWidth = CInt(TotalGridWidth * 0.09)
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .ColumnHeadersDefaultCellStyle = columnHeaderStyle
            .ReadOnly = False
            .RowsDefaultCellStyle.BackColor = Color.PowderBlue
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
        End With

    End Sub
#End Region

#Region "LookUp"
    Private Function EmbarqueHaSidoProcesado() As Boolean
        Dim StatusActual As String = (From p In InfoCache.EmbarquesDS.EMBARQUES Where (p.EMBARQUE_CLAVE = Me.EMBARQUE_CLAVE.Text) Select p.STATUS).First
        If StatusActual = "C" Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function getNombreGasto(ByVal G_CLAVE As Decimal) As String
        Dim TempString As String = String.Empty
        With MaestroDeCostos.DEFINICIONGASTOS
            For Each Tablerow As CostosMasterData.DEFINICIONGASTOSRow In .Rows
                If Tablerow.G_CLAVE = G_CLAVE Then
                    TempString = Tablerow.NOMBRE
                    Exit For
                End If
            Next

        End With
        Return TempString
    End Function

    Private Function ExisteEmbarque(ByVal EMBARQUE_CLAVE As String) As Boolean
        Dim TablaDetalles = InfoCache.EmbarquesDS.EMBARQUES.AsEnumerable()
        Dim existe = TablaDetalles.Any(Function(w) w.Field(Of String)("EMBARQUE_CLAVE").Contains(EMBARQUE_CLAVE))
        Return existe
    End Function
#End Region

#Region "Eventos"
    Private Sub GridDatos_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridDatos.SelectionChanged
        'Me.EmbarqueLabel.Text = value
        Me.EmbarqueDetalle.EmbarqueClave = Me.EMBARQUE_CLAVE.Text
        Me.FichaT1.EmbarqueClave = Me.EMBARQUE_CLAVE.Text.Trim
        Me.FichaT2.EmbarqueClave = Me.EMBARQUE_CLAVE.Text.Trim
        Me.FichaT3.EmbarqueClave = Me.EMBARQUE_CLAVE.Text.Trim

    End Sub
    

    Private Sub KryptonContextMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KryptonContextMenuItem1.Click
        'update data
        'Debug.Write(KryptonContextMenuItem1.Text & " 1" & vbCrLf)
        UpdateDatosEmbarques()
    End Sub
    Private Sub KryptonContextMenuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KryptonContextMenuItem2.Click
        'TipoCambio()
        AsignarTipoCambio()

        'Debug.Write(KryptonContextMenuItem2.Text & " 2" & vbCrLf)
    End Sub
    Private Sub KryptonContextMenuItem7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KryptonContextMenuItem7.Click
        'Informe de prorratero de embarque 
        'Debug.Write(KryptonContextMenuItem7.Text & " 7" & vbCrLf)
        ImprimirExcel01()

    End Sub
    Private Sub KryptonContextMenuItem8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KryptonContextMenuItem8.Click
        'Consolidado de Prorrateos Embarques Importados
        'Debug.Write(KryptonContextMenuItem8.Text & " 8" & vbCrLf)
    End Sub
    Private Sub KryptonContextMenuItem9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KryptonContextMenuItem9.Click
        'Consolidado de Prorrateos Embarques Importados por Proveedor
        'Debug.Write(KryptonContextMenuItem9.Text & " 9" & vbCrLf)
    End Sub
    Private Sub KryptonContextMenuItem10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KryptonContextMenuItem10.Click
        'Costo promedio por producto
        'Debug.Write(KryptonContextMenuItem10.Text & " 10" & vbCrLf)
    End Sub
    Private Sub KryptonContextMenuItem11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KryptonContextMenuItem11.Click
        CerrarPeriodo()
        'Cerrar periodo
        'Debug.Write(KryptonContextMenuItem11.Text & " 11" & vbCrLf)

    End Sub
    Private Sub KryptonContextMenuItem12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KryptonContextMenuItem12.Click
        'Terminar
        Debug.Write(KryptonContextMenuItem12.Text & " 12" & vbCrLf)
        Me.Close()
    End Sub
    Private Sub PromediarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromediarButton.Click
        Promediar()
    End Sub
    Private Sub CostosCeroButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostosCeroButton.Click
        With New ItemsSinCostos
            .ShowDialog()
        End With
    End Sub
    Private Sub cboFormatoExcel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFormatoExcel.SelectedIndexChanged
        Select Case cboFormatoExcel.Text
            Case "Excel 2003 y anterior"
                InfoCache.VersionExcel = 1
            Case "Excel 2007 a 2010"
                InfoCache.VersionExcel = 2
        End Select
    End Sub
#End Region
#Region "Validaciones"
    Private Function DatosEmbarqueIncompletos() As Boolean
        Dim ErrorMsg As New StringBuilder
        Dim HayErrores As Boolean
        Dim q = (From c In InfoCache.EmbarquesDS.EMBARQUES _
        Where c.EMBARQUE_CLAVE = Me.EMBARQUE_CLAVE.Text _
        Select c).First()

        If q.IsFACTURA_NONull Then
            HayErrores = True
            ErrorMsg.Append("Sin informacion: ").Append("/").Append("No. Factura").Append(vbCrLf)
        End If
        If q.IsFECHA_FACTURANull Then
            HayErrores = True
            ErrorMsg.Append("Sin informacion: ").Append("/").Append("Fecha de Factura").Append(vbCrLf)
        End If
        If q.IsCONTROL_NONull Then
            HayErrores = True
            ErrorMsg.Append("Sin informacion: ").Append("/").Append("No. Control").Append(vbCrLf)
        End If
        If q.IsFECHA_INGRESONull Then
            HayErrores = True
            ErrorMsg.Append("Sin informacion: ").Append("/").Append("Fecha de Ingreso").Append(vbCrLf)
        End If
        If q.IsPROVEEDOR_NOMBRENull Then
            HayErrores = True
            q.PROVEEDOR_NOMBRE = String.Empty
            ErrorMsg.Append("Sin informacion: ").Append("/").Append("Nombre del proveedor").Append(vbCrLf)
        End If

        If HayErrores Then
            MsgBox(ErrorMsg.ToString, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Registre los datos antes de continuar")
            Return True
        Else
            Return False
        End If
    End Function
    Private Function ValidarTipoCambioAsignado() As Boolean
        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUES _
                  Where p.EMBARQUE_CLAVE = Me.EMBARQUE_CLAVE.Text _
                  Select p.TIPO_CAMBIO

        If q(0) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Private Function ValidarPrecioUnitario() As Boolean
        Dim ErrorMsg As New StringBuilder
        Dim HayErrores As Boolean
        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES _
                  Where p.EMBARQUE_CLAVE = Me.EMBARQUE_CLAVE.Text _
                  Select p

        For Each secuencia In q
            If secuencia.DMP_PRECIO = 0 Then
                HayErrores = True
                ErrorMsg.Append(secuencia.PRO_CLAVE).Append("/").Append(secuencia.PRO_DESCRI).Append(vbCrLf)
            End If
        Next
        If HayErrores Then
            MsgBox(ErrorMsg.ToString, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Registrar precios de factura")
            Return False
        Else
            Return True
        End If
    End Function
#End Region
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ConfigurarGrid()

    End Sub



    Private Sub CostosContextMenu_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles CostosContextMenu.Opening

    End Sub

    Private Sub AccionesContextMenu_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles AccionesContextMenu.Opening

    End Sub
End Class 'COSTOS
