Imports System
Imports System.Globalization
Imports Microsoft.VisualBasic

Public Class ProgramarTimerForm


    Private Sub ProgramarTimerForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.FechaEjecutar.MinDate = Date.Now
        Me.FechaEjecutar.Value = Date.Now
    End Sub
    Public ReadOnly Property IniciarDate As Date
        Get
            Return Me.FechaEjecutar.Value
        End Get
    End Property
#Region "Eventos"
    Private Sub IniciarButton_Click(sender As System.Object, e As System.EventArgs) Handles IniciarButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub DetenerButton_Click(sender As System.Object, e As System.EventArgs) Handles DetenerButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Abort
    End Sub
#End Region


#Region "SERNOMINA2SAP - para convertir fechas"
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim myDTFI As DateTimeFormatInfo = New CultureInfo("en-US", False).DateTimeFormat
        Dim myDT As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day)

        Me.Label2.Text = myDT.Date.ToString("g", myDTFI)
        Me.Label3.Text = myDT.Date
    End Sub
#End Region
End Class