Public Class Bitacoras
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(My.Settings.PathFiles, FileIO.SearchOption.SearchTopLevelOnly, "*.log")
            DirectorioList.Items.Add(My.Computer.FileSystem.GetName(foundFile))
        Next

    End Sub
#Region "Establecer Ruta"
    Private Sub LocalizarBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocalizarBtn.Click
        EstablecerRuta()
    End Sub
    Private Sub EstablecerRuta()
        With FolderBrowserDialog1
            .ShowNewFolderButton = True
            .Description = "Directorio de archivos de ISAPMENU"
            .RootFolder = Environment.SpecialFolder.MyDocuments
        End With
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Me.PathTxt.Text = FolderBrowserDialog1.SelectedPath
        End If
        My.Settings.Save()
        'VerificarArchivos()
    End Sub
#End Region
#Region "Leer archivo"
    Private Sub ReadFile(ByVal NombreFile As String)
        Dim ArchivoOrigen As String = My.Settings.PathFiles & "\" & NombreFile
        If My.Computer.FileSystem.DirectoryExists(Me.PathTxt.Text) Then
            If My.Computer.FileSystem.FileExists(ArchivoOrigen) Then
                My.Application.DoEvents()
                Using MyReader As New Microsoft.VisualBasic.
                               FileIO.TextFieldParser(
                               ArchivoOrigen)
                    MyReader.TextFieldType = FileIO.FieldType.FixedWidth
                    Dim currentRow As String
                    While Not MyReader.EndOfData
                        Try
                            currentRow = MyReader.ReadLine
                            AgregarFila(currentRow)
                        Catch ex As Microsoft.VisualBasic.
                                    FileIO.MalformedLineException
                            MsgBox("Line " & ex.Message &
                            "is not valid and will be skipped.")
                        End Try
                    End While
                End Using
            End If
        End If
    End Sub
    Private Sub AgregarFila(currentRow As String)
        BitacoraTextBox.AppendText(currentRow & Environment.NewLine)
        ScrollToBottom()
        My.Application.DoEvents()
    End Sub
    Private Sub ScrollToBottom()
        BitacoraTextBox.Select()
        BitacoraTextBox.SelectionStart = BitacoraTextBox.Text.Length
        BitacoraTextBox.ScrollToCaret()
    End Sub
#End Region
#Region "Eventos"
    Private Sub DirectorioList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles DirectorioList.SelectedIndexChanged
        BitacoraTextBox.Text = String.Empty
        ReadFile(DirectorioList.Items(DirectorioList.SelectedIndex).ToString)
    End Sub

    Private Sub CerrarButton_Click(sender As System.Object, e As System.EventArgs) Handles CerrarButton.Click
        Me.Close()
    End Sub
#End Region

    Private Sub Bitacoras_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If My.Settings.PathFiles.Length = 0 Then
            My.Settings.PathFiles = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        End If
    End Sub
End Class