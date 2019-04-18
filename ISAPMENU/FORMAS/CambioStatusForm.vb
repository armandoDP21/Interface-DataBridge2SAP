
'option Strict on
Imports System.Text

Public Class CambioStatusForm
    Private DatosMenuInterfaces As DataView
    Private DatosMenu As RunIsapData
    Private DatosCalendario As CalendarioData
    Private UserPaises As New ArrayList

    Public WriteOnly Property InterfacesRS As RunIsapData
        Set(value As RunIsapData)
            Try
                DatosMenu = value
            Catch ex As Exception
                MsgBox(ex.Message, CType("Error Datos", MsgBoxStyle))
            End Try
        End Set
    End Property
    Public WriteOnly Property DatosCalendarioRS As CalendarioData
        Set(value As CalendarioData)
            Try
                DatosCalendario = value
            Catch ex As Exception
                MsgBox(ex.Message, CType("Error Calendario", MsgBoxStyle))
            End Try
        End Set
    End Property
#Region "Metodos"
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        Me.FechaLabel.Text = Format(InfoCache.FechaHasta, "ddd dd/MMM/yyyy")
        CargarListaTiposPolizas()
        CrearListaPaises()
    End Sub
    Private Sub CrearListaPaises()

        Dim q = From p In DatosMenu.VSI_PROCESOS _
        Where p.PARAM = "Modulo" _
        Group p By p.PAIS_CLAVE Into g = Group _
        Select (New With {g, PAIS_CLAVE})

        For Each secuencia In q
            Dim NomPais As String = GetNombrePais(secuencia.PAIS_CLAVE)
            Dim codePais As String = GetSAP_COMPANY_CODE(secuencia.PAIS_CLAVE)
            UserPaises.Add(New Paises(NomPais, codePais))
        Next
        Try
            With PaisesListBox
                .DataSource = UserPaises
                .DisplayMember = "Name"
                .ValueMember = "Clave"
            End With
        Catch ex As Exception
            MsgBox(ex.Message, CType("Lista Paises", MsgBoxStyle))
        End Try
        'PaisesListBox.SelectedIndex = 0
    End Sub
    Private Function GetNombrePais(ByVal ClavePais As Decimal) As String
        Dim NombreString As String = String.Empty
        With DatosMenu.DATOSPAIS
            For Each Tablerow As RunIsapData.DATOSPAISRow In .Rows
                If Tablerow.PAIS_CLAVE = ClavePais Then
                    NombreString = Tablerow.PAIS_NOMBRE
                    Exit For

                End If
            Next
            Return NombreString
        End With
    End Function
    Private Function GetSAP_COMPANY_CODE(ByVal ClavePais As Decimal) As String
        Dim NombreString As String = String.Empty
        With DatosMenu.DATOSPAIS
            For Each Tablerow As RunIsapData.DATOSPAISRow In .Rows
                If Tablerow.PAIS_CLAVE = ClavePais Then
                    NombreString = Tablerow.CLAVE_CIA
                    Exit For
                End If
            Next
            Return NombreString
        End With
    End Function
    Private Sub CargarListaTiposPolizas()
        Dim q = From p In DatosMenu.VSI_PROCESOS
        Where p.PARAM = "Modulo"
        Group p By p.PROCEDURE_NAME Into g = Group
        Select (New With {g, PROCEDURE_NAME})

        For Each Poliza In q
            Select Case Poliza.PROCEDURE_NAME
                Case "icc"
                    Me.CboTiposPolizas.Items.Add("Pólizas de ingresos y ventas")
                Case "pdcv"
                    Me.CboTiposPolizas.Items.Add("Pólizas de costo de ventas")
                Case "dbp_interfase_centralizada"
                    Me.CboTiposPolizas.Items.Add("Pólizas de cartera centralizada")
            End Select

        Next
        Me.CboTiposPolizas.SelectedIndex = 0

    End Sub

#End Region
#Region "Eventos"
    Private Sub CambiarStatusButton_Click_1(sender As System.Object, e As System.EventArgs) Handles CambiarStatusButton.Click
        Dim APS As String
        Dim ClavePais As Decimal
        Dim ClaveCompania As String
        Dim NombrePais As String
        Dim result As Integer
        Dim ResultadoText As New StringBuilder
        Me.Cursor = Cursors.WaitCursor
        For Each indexChecked As Integer In PaisesListBox.CheckedIndices
            PaisesListBox.SelectedIndex = indexChecked

            ClaveCompania = PaisesListBox.SelectedValue
            NombrePais = PaisesListBox.SelectedItem.name
            ClavePais = GetClavePais(NombrePais)
            APS = GetAps(ClavePais)

            Dim selectedItem As Object
            selectedItem = CboTiposPolizas.SelectedItem
            ResultadoText.Append(selectedItem.ToString).Append("-")
            Select Case selectedItem.ToString
                Case "Pólizas de ingresos y ventas"
                    result = (New MenuSP).ChangeStatusCxCSemana(ClaveCompania, APS, ClavePais)
                Case "Pólizas de costo de ventas"
                    result = (New MenuSP).ChangeStatusCostoVentasSemana(ClaveCompania, APS, ClavePais)
                Case "Pólizas de cartera centralizada"
                    result = (New MenuSP).ChangeStatusCentralizadaSemana(ClavePais)
            End Select

            ResultadoText.Append(NombrePais).Append(" - Se cambió el estatus a ").Append(result.ToString).Append(" registros").Append(vbCrLf)

            'Debug.Print(PaisesListBox.SelectedValue.clave & " - " & selectedItem.ToString() & vbCrLf)
        Next
        Me.Cursor = Cursors.Default
        My.Application.DoEvents()
        MsgBox(ResultadoText.ToString, MsgBoxStyle.Information And MsgBoxStyle.OkOnly, "Cambio de Estatus de pólizas")

    End Sub

    Private Sub CerrarButton_Click_1(sender As System.Object, e As System.EventArgs) Handles CerrarButton.Click
        Me.Close()
    End Sub

#End Region
    Private Function GetClavePais(NombrePais As String) As Decimal

        Dim ClavePais As Decimal

        With DatosMenu.DATOSPAIS
            For Each Tablerow As RunIsapData.DATOSPAISRow In .Rows
                If Tablerow.PAIS_NOMBRE = NombrePais Then
                    ClavePais = Tablerow.PAIS_CLAVE
                    Exit For
                End If
            Next

        End With

        Return ClavePais
    End Function

    Private Function GetAps(ClavePais As Decimal) As String
        Dim ReturnText As String = String.Empty

        With DatosCalendario.CALENDARIO_PAIS
            For Each Tablerow As CalendarioData.CALENDARIO_PAISRow In .Rows
                If Tablerow.PAIS_CLAVE = ClavePais Then
                    ReturnText = Tablerow.APS
                    Exit For
                End If
            Next
        End With
        Return ReturnText
    End Function


End Class