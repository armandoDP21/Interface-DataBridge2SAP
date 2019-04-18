Public Class ImportEmbarquesForm
    'Private CurrentInfo As EmbarquesData

#Region "Metodos"


    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Me.Periodo.Text = Date.Now.Month.ToString - 1
        Me.FiscalYear.Text = Date.Now.Year.ToString
    End Sub
    Private Sub ProcesarImportacion()

        'Me.GridDatos.DataSource =  InfoCache.EmbarquesDS.EMBARQUESORIGEN.DefaultView
        Debug.Write("Origen " & InfoCache.EmbarquesDS.EMBARQUESORIGEN.Rows.Count.ToString)


        Debug.Write("Agrupadas " & InfoCache.EmbarquesDS.EMBARQUES.Rows.Count.ToString)




        Debug.Write("Detalles " & InfoCache.EmbarquesDS.EMBARQUES_DETALLE.Rows.Count.ToString)

    End Sub
#End Region

  
#Region "Eventos"
    Private Sub ImportarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportarButton.Click
        ProcesarImportacion()
    End Sub

    Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarButton.Click
        Me.Close()
    End Sub
#End Region

End Class
