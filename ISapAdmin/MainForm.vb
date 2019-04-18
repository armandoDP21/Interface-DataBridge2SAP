Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Data.Common


Public Class MainForm
    Private ModuloActual As String = "PDCV"
    Private UsuarioInfo As UsuariosData
    Private Maestro As OSDMaestroData
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        'RestoreWindowSettings()
        InfoCache.ConnectionString = "Data Source=ODSCA;User Id=ODSCA;Password=PRO56KAL"
        CargarListaUsuarios()
        'If DisplayLoginForm() = Windows.Forms.DialogResult.OK Then
        '    My.Application.DoEvents()
        '    CargarDatosUsuario()
        '    Me.FechaDesde.Value = "2010/11/1"
        '    Me.FechaHasta.Value = "2010/11/4"
        'Else
        '    Me.Close()
        'End If
    End Sub
#Region " Save and restoring setting "

    'Private Sub RestoreWindowSettings()
    '    Try

    '        'InfoCache.UId = My.Settings.UserName
    '        'InfoCache.ServerHost = My.Settings.ServerHost
    '        'InfoCache.QueryTimeOut = My.Settings.QueryTimeOut
    '        'InfoCache.PacketSize = My.Settings.PacketSize
    '        'InfoCache.Catalogo = My.Settings.Catalogo
    '        Dim bounds() As String = My.Settings.MainFormPlacement.Split(","c)
    '        If bounds.Length = 4 Then
    '            Dim boundsRect As Rectangle = New Rectangle( _
    '             CInt(bounds(0)), CInt(bounds(1)), _
    '             CInt(bounds(2)), CInt(bounds(3)))

    '            boundsRect = Rectangle.Intersect(Screen.PrimaryScreen.WorkingArea, boundsRect)
    '            If boundsRect.Width > 0 And boundsRect.Height > 0 Then
    '                ' set the window location and size
    '                Me.StartPosition = FormStartPosition.Manual
    '                Me.SetBounds(boundsRect.Left, boundsRect.Top, _
    '                 boundsRect.Width, boundsRect.Height)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Me.StartPosition = FormStartPosition.WindowsDefaultLocation
    '    End Try
    'End Sub
    'Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
    '    MyBase.OnClosing(e)
    '    My.Settings.MainFormPlacement = String.Format("{0},{1},{2},{3}", _
    '             Me.Bounds.X, Me.Bounds.Y, _
    '             Me.Bounds.Width, Me.Bounds.Height)

    '    My.Settings.Save()
    'End Sub



#End Region
#Region "Metodos"
    Private Sub CargarListaUsuarios()




    End Sub
#End Region
End Class


