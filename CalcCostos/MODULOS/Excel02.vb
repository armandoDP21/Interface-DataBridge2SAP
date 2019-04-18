Imports System
Imports System.Drawing
Imports System.Data

Imports GemBox.Spreadsheet
Module Excel02
    Private HojaExel As ExcelWorksheet
    Private fileName As String
    Private TipoDeCambioLocal As Decimal
#Region "Configuracion y Encabezado"
    Public Sub CrearExcel02()
        Dim excelFile As ExcelFile = New ExcelFile
        Dim worksheets As ExcelWorksheetCollection = excelFile.Worksheets
        HojaExel = worksheets.Add("Impresion1")

        worksheets.ActiveWorksheet = worksheets("Impresion1")

        Select Case InfoCache.VersionExcel
            Case 1
                'fileName = Constantes.FilesGlobalDirectory & EmbarqueClaveLocal & "-R01.xls"
                excelFile.SaveXls(fileName)
            Case 2
                'fileName = Constantes.FilesGlobalDirectory & EmbarqueClaveLocal & "-R01.xlsx"
                excelFile.SaveXlsx(fileName)
        End Select
        TryToDisplayGeneratedFile(fileName)
    End Sub
    Private Sub EstablecerAnchoColumnas(ByVal ws As ExcelWorksheet)
        ' Column width of 8, 30, 16, 9, 9, 9, 9, 4 and 5 characters.
        ws.Columns(0).Width = 4 * 256
        ws.Columns(ColumnaDetalles.ClaveProducto).Width = 17 * 256
        ws.Columns(ColumnaDetalles.Descripcion).Width = 38 * 256
        ws.Columns(ColumnaDetalles.Cantidad).Width = 11 * 256
        ws.Columns(ColumnaDetalles.DmpPrecioUnitario).Width = 11 * 256
        ws.Columns(ColumnaDetalles.Fob).Width = 11 * 256
        ws.Columns(ColumnaDetalles.Flete).Width = 11 * 256
        ws.Columns(ColumnaDetalles.Seguros).Width = 11 * 256
        ws.Columns(ColumnaDetalles.OG).Width = 11 * 256
        ws.Columns(ColumnaDetalles.CostoTotal).Width = 11 * 256
        ws.Columns(ColumnaDetalles.CostoUnitario).Width = 11 * 256
        ws.Columns(ColumnaDetalles.Gramos).Width = 9 * 256
        ws.Columns(ColumnaDetalles.Kilos).Width = 9 * 256

    End Sub

    Private Sub Encabezado(ByVal ws As ExcelWorksheet)
        'ws.Cells(0, 1).Value = "FLI-" & InfoCache.PaisClave

        'ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Font.Weight = ExcelFont.BoldWeight
        'ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        'ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.VerticalAlignment = HorizontalAlignmentStyle.Left
        'ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        'ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)


        ws.Cells(0, 1).Value = "Consolidado de Prorrateos Embarques Importados"
        ws.Cells(0, 1).Style.Font.Size = 10 * 20
        ws.Cells(0, 1).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(0, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(0, 1).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(0, 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.LightBlue)
        ws.Cells(0, 1).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(0, 2).Value = "MES DE: " & MonthName(CInt(InfoCache.PeriodoActual), True) & " " & InfoCache.FiscalYear
        ws.Cells(0, 2).Style.Font.Size = 7 * 20
        ws.Cells(0, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(0, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(0, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(0, 2).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.LightBlue)
        ws.Cells(0, 2).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(1, 10).Value = "TIPO DE CAMBIO: " & Format(TipoDeCambioLocal, "#,#.00")
        ws.Cells(1, 10).Style.Font.Size = 7 * 20
        ws.Cells(1, 10).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(1, 10).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(1, 10).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(1, 10).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.LightBlue)
        ws.Cells(1, 10).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        

        'ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Name = "Calibri"
        If InfoCache.VersionExcel = 2 Then
            ws.Pictures.Add(My.Application.Info.DirectoryPath & "\LCostos.bmp", New Rectangle(50, 50, 48, 48))
        End If

    End Sub
    Private Sub TryToDisplayGeneratedFile(ByVal fileName As String)
        Try
            System.Diagnostics.Process.Start(fileName)
        Catch
            Console.WriteLine(fileName + " created in application folder.")
        End Try
    End Sub
#End Region


End Module
