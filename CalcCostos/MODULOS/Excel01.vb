Imports System
Imports System.Drawing
Imports System.Data

Imports GemBox.Spreadsheet

Enum ColumnasGastos
    Descripcion = 2
    MontoDolares
    MontoML
End Enum
Enum ColumnaDetalles
    ClaveProducto = 1
    Descripcion
    Cantidad
    DmpPrecioUnitario
    Fob
    Flete
    Seguros
    OG
    CostoTotal
    CostoUnitario
    Gramos
    Kilos
End Enum
Enum ColumnaDetallesML
    ClaveProducto = 1
    Descripcion
    Cantidad
    DmpPrecioUnitario
    Cif
    DerechosAranceles
    Flete
    Honorarios
    Varios
    CostoTotalML
    CostoUnitarioML
    CostoUnitario
End Enum
Module Excel01
    Private Const PrimeraFilaGastos As Integer = 4
    Private Const PrimeraFilaDetalles As Integer = 49
    Private Const FormatoStringCantidad As String = "#,##0"
    Private Const FormatoStringMoneda As String = "#,##0.00"
    Private Const FormatoStringOtros As String = "#,##0.0000"
    Private TOTALFOB As Decimal
    Private FilaActual As Integer

    Private EmbarqueClaveLocal As String
    Private TipoDeCambioLocal As Decimal
    Private FACTURA_NOLocal As String
    Private FECHA_FACTURALocal As String
    Private FECHA_INGRESOLocal As String
    Private CONTROL_NOLocal As String
    Private PROVEEDOR_NOMBRELocal As String
    Private MMP_FOLIOLocal As String

    Private HojaExel As ExcelWorksheet
    Private fileName As String
#Region "Configuracion y Encabezado"
    Public Sub CrearExcel01(ByVal InEmbarqueClave As String)


        RetrieveDatosLocales()
        EmbarqueClaveLocal = InEmbarqueClave
        Dim excelFile As ExcelFile = New ExcelFile
        Dim worksheets As ExcelWorksheetCollection = excelFile.Worksheets
        HojaExel = worksheets.Add("Impresion1")
        ' Each static method modifies provided worksheet with one category of features.

        'ValuesSample(worksheets.Add("Values"))
        'StylesSample(worksheets.Add("Styles"))
        EstablecerAnchoColumnas(HojaExel)
        Encabezado(HojaExel)
        'PresentacionGastos(HojaExel)

        PresentacionGastosN1(HojaExel)
        PresentacionGastosN2Mas(HojaExel)

        PresentacionDetallesEncabezado(HojaExel)
        PresentacionDetalle(HojaExel)
        PresentacionDetallesMLEncabezado(HojaExel)
        PresentacionDetalleML(HojaExel)

        'ReferencingAndGroupsSample(worksheets.Add("ReferencingAndGroups"))


        worksheets.ActiveWorksheet = worksheets("Impresion1")

        Select Case InfoCache.VersionExcel
            Case 1
                fileName = Constantes.FilesGlobalDirectory & "\" & EmbarqueClaveLocal & "-R01.xls"
                excelFile.SaveXls(fileName)
            Case 2
                fileName = Constantes.FilesGlobalDirectory & "\" & EmbarqueClaveLocal & "-R01.xlsx"
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


        ws.Cells(0, 1).Value = "LIQUIDACION IMPORTACION  DE PRODUCTO TERMINADO"
        ws.Cells(0, 1).Style.Font.Size = 10 * 20
        ws.Cells(0, 1).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(0, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(0, 1).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ws.Cells(1, 1).Value = "MES : " & MonthName(CInt(InfoCache.PeriodoActual), True) & " " & InfoCache.FiscalYear
        ws.Cells(1, 1).Style.Font.Size = 7 * 20
        ws.Cells(1, 1).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(1, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(1, 1).Style.VerticalAlignment = VerticalAlignmentStyle.Center


        ws.Cells(1, 3).Value = "PROVEEDOR: " & PROVEEDOR_NOMBRELocal
        ws.Cells(1, 3).Style.Font.Size = 7 * 20
        ws.Cells(1, 3).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(1, 3).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(1, 3).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        For i As Integer = 0 To 1
            For j As Integer = 1 To 5
                ws.Cells(i, j).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.LightBlue)
                ws.Cells(i, j).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)
            Next

        Next



        ws.Cells(1, 7).Value = "FACTURA: " & FACTURA_NOLocal
        ws.Cells(1, 7).Style.Font.Size = 7 * 20
        ws.Cells(1, 7).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(1, 7).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(1, 7).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ws.Cells(2, 7).Value = "FECHA FACTURA: " & FECHA_FACTURALocal
        ws.Cells(2, 7).Style.Font.Size = 7 * 20
        ws.Cells(2, 7).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(2, 7).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(2, 7).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ws.Cells(3, 7).Value = "NO. CONTROL: " & CONTROL_NOLocal
        ws.Cells(3, 7).Style.Font.Size = 7 * 20
        ws.Cells(3, 7).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(3, 7).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(3, 7).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ws.Cells(4, 7).Value = "FECHA INGRESO: " & FECHA_INGRESOLocal
        ws.Cells(4, 7).Style.Font.Size = 7 * 20
        ws.Cells(4, 7).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(4, 7).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(4, 7).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ws.Cells(5, 7).Value = "TIPO DE CAMBIO: " & Format(TipoDeCambioLocal, "#,#.00")
        ws.Cells(5, 7).Style.Font.Size = 7 * 20
        ws.Cells(5, 7).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(5, 7).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(5, 7).Style.VerticalAlignment = VerticalAlignmentStyle.Center
       

        ws.Cells(6, 7).Value = EmbarqueClaveLocal
        ws.Cells(6, 7).Style.Font.Size = 8 * 20
        ws.Cells(6, 7).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(6, 7).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(6, 7).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        For i As Integer = 1 To 6
            For j = 7 To 9
                ws.Cells(i, j).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.LightBlue)
                ws.Cells(i, j).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)
            Next

        Next
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
#Region "Gastos"

    Private Sub PresentacionGastosN1(ByVal ws As ExcelWorksheet)
        FilaActual = PrimeraFilaGastos


        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Value = "$"
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.LightBlue)
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)


        ws.Cells(FilaActual, ColumnasGastos.MontoML).Value = "ML"
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.LightBlue)
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        FilaActual += 1

        Dim NameMontodDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnasGastos.MontoDolares) & Format(FilaActual, "##")
        Dim NameMontodMLDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnasGastos.MontoML) & Format(FilaActual, "##")

        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Value = "VALOR     F.O.B."
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Name = "Calibri"

        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Value = TOTALFOB
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.NumberFormat = FormatoStringMoneda

        ws.Cells(FilaActual, ColumnasGastos.MontoML).Value = TOTALFOB * TipoDeCambioLocal
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.NumberFormat = FormatoStringMoneda

        FilaActual += 1


        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESGASTOS _
               Where p.EMBARQUE_CLAVE = EmbarqueClaveLocal And p.NIVEL = 1
               Order By p.NOMBRE
               Select p



        For Each secuencia In q

            ws.Cells(FilaActual, ColumnasGastos.Descripcion).Value = secuencia.NOMBRE
            ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Name = "Calibri"

            ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Value = secuencia.MONTO
            ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnasGastos.MontoML).Value = secuencia.MONTO * TipoDeCambioLocal
            ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.NumberFormat = FormatoStringMoneda

            FilaActual += 1

        Next

        Dim NameMontodHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnasGastos.MontoDolares) & Format(FilaActual, "##")
        Dim NameMontodMLHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnasGastos.MontoML) & Format(FilaActual, "##")

        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Value = "Total factura #" & FACTURA_NOLocal
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.NumberFormat = FormatoStringMoneda

        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Formula = "=Sum(" & NameMontodDesde & ":" & NameMontodHasta & ")"
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.NumberFormat = FormatoStringMoneda

        ws.Cells(FilaActual, ColumnasGastos.MontoML).Formula = "=Sum(" & NameMontodMLDesde & ":" & NameMontodMLHasta & ")"
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.NumberFormat = FormatoStringMoneda

        FilaActual += 2
    End Sub
    Private Sub PresentacionGastosN2Mas(ByVal ws As ExcelWorksheet)


        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESGASTOS _
               Where p.EMBARQUE_CLAVE = EmbarqueClaveLocal And p.CLASE = 0
               Order By p.TIPO
               Select p

        'Dim NameMontodDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnasGastos.MontoDolares) & Format(FilaActual, "##")
        Dim NameMontodMLDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnasGastos.MontoML) & Format(FilaActual, "##")

        For Each secuencia In q
            If secuencia.MONTO > 0 Then
                ws.Cells(FilaActual, ColumnasGastos.Descripcion).Value = secuencia.NOMBRE
                ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Name = "Calibri"

                'ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Value = secuencia.MONTO
                'ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Name = "Calibri"
                'ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.NumberFormat = FormatoStringMoneda


                ws.Cells(FilaActual, ColumnasGastos.MontoML).Value = secuencia.MONTO
                ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Name = "Calibri"
                ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.NumberFormat = FormatoStringMoneda
                FilaActual += 1
            End If

        Next

        'Dim NameMontodHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnasGastos.MontoDolares) & Format(FilaActual, "##")
        Dim NameMontodMLHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnasGastos.MontoML) & Format(FilaActual, "##")

        'ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Formula = "=Sum(" & NameMontodDesde & ":" & NameMontodHasta & ")"
        'ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Name = "Calibri"
        'ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.Font.Weight = ExcelFont.BoldWeight
        'ws.Cells(FilaActual, ColumnasGastos.MontoDolares).Style.NumberFormat = FormatoStringMoneda

        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Value = "TOTAL GASTOS LOCALES"
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnasGastos.MontoML).Formula = "=Sum(" & NameMontodMLDesde & ":" & NameMontodMLHasta & ")"
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.NumberFormat = FormatoStringMoneda

        Dim TotalN1 As Decimal = (From p In InfoCache.EmbarquesDS.EMBARQUESGASTOS Where p.EMBARQUE_CLAVE = EmbarqueClaveLocal And p.NIVEL = 1 Select p.MONTO).Sum
        Dim TotalC0 As Decimal = (From p In InfoCache.EmbarquesDS.EMBARQUESGASTOS Where p.EMBARQUE_CLAVE = EmbarqueClaveLocal And p.CLASE = 0 Select p.MONTO).Sum


        Dim TotalGastosyFOB As Decimal = (TOTALFOB + TotalN1 + TotalC0) '* TipoDeCambioLocal

        FilaActual += 1
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Value = "TOTAL GASTOS"
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.Descripcion).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnasGastos.MontoML).Value = TotalGastosyFOB
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnasGastos.MontoML).Style.NumberFormat = FormatoStringMoneda


    End Sub
#End Region
#Region "Detalle"
    Private Sub PresentacionDetallesEncabezado(ByVal ws As ExcelWorksheet)
        FilaActual += 2
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Value = "PRODUCTO"
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)


        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Value = "NOMBRE"
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)


        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Value = "UNIDADES"
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Value = "PRECIO UNIT FACT $"
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Fob).Value = "VALOR FOB $"
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Flete).Value = "FLETE "
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Value = "SEGUROS"
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.OG).Value = "OTROS GASTOS $"
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Value = "COSTO TOTAL $"
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Value = "COSTO UNITARIO $"
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Value = "GRAMOS"
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Value = "PESO KILOS"
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)


        FilaActual += 1
    End Sub
    Private Sub PresentacionDetalle(ByVal ws As ExcelWorksheet)


        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES _
               Where p.EMBARQUE_CLAVE = EmbarqueClaveLocal
               Order By p.PRO_CLAVE
               Select p



        Dim NameCeldaCantidadDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Cantidad) & Format(FilaActual, "##")
        Dim NameCeldaFobDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Fob) & Format(FilaActual, "##")
        Dim NameCeldaFleteDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Flete) & Format(FilaActual, "##")
        Dim NameCeldaSegurosDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Seguros) & Format(FilaActual, "##")
        Dim NameCeldaOGDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.OG) & Format(FilaActual, "##")
        Dim NameCeldaCostoTotalDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.CostoTotal) & Format(FilaActual, "##")
        Dim NameCeldaKilosDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Kilos) & Format(FilaActual, "##")


        For Each secuencia In q
            ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Value = secuencia.PRO_CLAVE
            ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Font.Name = "Calibri"

            ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Value = secuencia.PRO_DESCRI
            ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.Font.Name = "Calibri"

            ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Value = secuencia.DMP_CANTID
            ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.NumberFormat = FormatoStringCantidad

            ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Value = secuencia.DMP_PRECIO
            ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetalles.Fob).Value = secuencia.DMP_CANTID * secuencia.DMP_PRECIO
            ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetalles.Flete).Value = secuencia.AVG_FLETE
            ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetalles.Seguros).Value = secuencia.AVG_SEGUROS
            ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetalles.OG).Value = secuencia.AVG_OG
            ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.OG).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Value = secuencia.PFOB + secuencia.AVG_FLETE + secuencia.AVG_SEGUROS + secuencia.AVG_OG
            ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Value = secuencia.CU
            ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetalles.Gramos).Value = secuencia.PRO_GRAMAJ
            ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.NumberFormat = FormatoStringOtros

            ws.Cells(FilaActual, ColumnaDetalles.Kilos).Value = secuencia.KILOS
            ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.NumberFormat = FormatoStringOtros

            'ws.Cells(FilaActual, 2).Style.NumberFormat=
            FilaActual += 1

        Next
        Dim NameCeldaCantidadHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Cantidad) & Format(FilaActual, "##")
        Dim NameCeldaFobHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Fob) & Format(FilaActual, "##")
        Dim NameCeldaFleteHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Flete) & Format(FilaActual, "##")
        Dim NameCeldaSegurosHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Seguros) & Format(FilaActual, "##")
        Dim NameCeldaOGHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.OG) & Format(FilaActual, "##")
        Dim NameCeldaCostoTotalHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.CostoTotal) & Format(FilaActual, "##")
        Dim NameCeldaKilosHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetalles.Kilos) & Format(FilaActual, "##")

        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Formula = "=Sum(" & NameCeldaCantidadDesde & ":" & NameCeldaCantidadHasta & ")"
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.NumberFormat = FormatoStringCantidad
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Font.Weight = ExcelFont.BoldWeight


        ws.Cells(FilaActual, ColumnaDetalles.Fob).Formula = "=Sum(" & NameCeldaFobDesde & ":" & NameCeldaFobHasta & ")"
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetalles.Flete).Formula = "=Sum(" & NameCeldaFleteDesde & ":" & NameCeldaFleteHasta & ")"
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Formula = "=Sum(" & NameCeldaSegurosDesde & ":" & NameCeldaSegurosHasta & ")"
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetalles.OG).Formula = "=Sum(" & NameCeldaOGDesde & ":" & NameCeldaOGHasta & ")"
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Formula = "=Sum(" & NameCeldaCostoTotalDesde & ":" & NameCeldaCostoTotalHasta & ")"
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Formula = "=Sum(" & NameCeldaKilosDesde & ":" & NameCeldaKilosHasta & ")"
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.NumberFormat = FormatoStringOtros
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Font.Weight = ExcelFont.BoldWeight


    End Sub
#End Region
#Region "Detalle ML"
    Private Sub PresentacionDetallesMLEncabezado(ByVal ws As ExcelWorksheet)
        FilaActual += 2
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Value = "PRODUCTO"
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.ClaveProducto).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)


        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Value = "NOMBRE"
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Descripcion).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)


        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Value = "UNIDADES"
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Cantidad).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Value = "PRECIO UNIT FACT ML"
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.DmpPrecioUnitario).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Fob).Value = "VALOR CIF ML"
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.Fob).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Flete).Value = "DERECHOS ARANCELARIOS ML"
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.Flete).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Value = "FLETE"
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Seguros).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.OG).Value = "HONORARIOS ML"
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.OG).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Value = "VARIOS ML"
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.CostoTotal).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Value = "COSTO TOTAL ML"
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.CostoUnitario).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Value = "COSTO UNITARIO ML"
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.Gramos).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Value = "COSTO UNITARIO $"
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Font.Size = 8 * 20
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Font.Weight = ExcelFont.BoldWeight
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.VerticalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Color.PowderBlue, Color.PowderBlue)
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.WrapText = True
        ws.Cells(FilaActual, ColumnaDetalles.Kilos).Style.Borders.SetBorders(MultipleBorders.Outside, Color.FromArgb(252, 1, 1), LineStyle.Thin)


        FilaActual += 1
    End Sub
    Private Sub PresentacionDetalleML(ByVal ws As ExcelWorksheet)
        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES _
             Where p.EMBARQUE_CLAVE = EmbarqueClaveLocal
             Order By p.PRO_CLAVE
             Select p



        Dim NameCeldaCantidadDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Cantidad) & Format(FilaActual, "##")
        Dim NameCeldaCIFDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Cif) & Format(FilaActual, "##")
        Dim NameCeldaArancelesDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.DerechosAranceles) & Format(FilaActual, "##")
        Dim NameCeldaFleteDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Flete) & Format(FilaActual, "##")
        Dim NameCeldaHonorariosDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Honorarios) & Format(FilaActual, "##")
        Dim NameCeldaVariosDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Varios) & Format(FilaActual, "##")
        Dim NameCeldaCostoTotalMLDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.CostoTotalML) & Format(FilaActual, "##")
        'Dim NameCeldaArancelesDesde As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Aranceles) & Format(FilaActual, "##")


        For Each secuencia In q
            ws.Cells(FilaActual, ColumnaDetallesML.ClaveProducto).Value = secuencia.PRO_CLAVE
            ws.Cells(FilaActual, ColumnaDetallesML.ClaveProducto).Style.Font.Name = "Calibri"

            ws.Cells(FilaActual, ColumnaDetallesML.Descripcion).Value = secuencia.PRO_DESCRI
            ws.Cells(FilaActual, ColumnaDetallesML.Descripcion).Style.Font.Name = "Calibri"

            ws.Cells(FilaActual, ColumnaDetallesML.Cantidad).Value = secuencia.DMP_CANTID
            ws.Cells(FilaActual, ColumnaDetallesML.Cantidad).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.Cantidad).Style.NumberFormat = FormatoStringCantidad

            ws.Cells(FilaActual, ColumnaDetallesML.DmpPrecioUnitario).Value = secuencia.CU * TipoDeCambioLocal
            ws.Cells(FilaActual, ColumnaDetallesML.DmpPrecioUnitario).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.DmpPrecioUnitario).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetallesML.Cif).Value = secuencia.ML_CIF
            ws.Cells(FilaActual, ColumnaDetallesML.Cif).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.Cif).Style.NumberFormat = FormatoStringMoneda


            ws.Cells(FilaActual, ColumnaDetallesML.DerechosAranceles).Value = secuencia.ML_ARANCEL
            ws.Cells(FilaActual, ColumnaDetallesML.DerechosAranceles).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.DerechosAranceles).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetallesML.Flete).Value = secuencia.ML_FLETE
            ws.Cells(FilaActual, ColumnaDetallesML.Flete).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.Flete).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetallesML.Honorarios).Value = secuencia.ML_HONORARIOS
            ws.Cells(FilaActual, ColumnaDetallesML.Honorarios).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.Honorarios).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetallesML.Varios).Value = secuencia.ML_VARIOS
            ws.Cells(FilaActual, ColumnaDetallesML.Varios).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.Varios).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetallesML.CostoTotalML).Value = secuencia.ML_TOTAL
            ws.Cells(FilaActual, ColumnaDetallesML.CostoTotalML).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.CostoTotalML).Style.NumberFormat = FormatoStringMoneda

            ws.Cells(FilaActual, ColumnaDetallesML.CostoUnitarioML).Value = secuencia.ML_CU
            ws.Cells(FilaActual, ColumnaDetallesML.CostoUnitarioML).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.CostoUnitarioML).Style.NumberFormat = FormatoStringOtros

            ws.Cells(FilaActual, ColumnaDetallesML.CostoUnitario).Value = secuencia.CU
            ws.Cells(FilaActual, ColumnaDetallesML.CostoUnitario).Style.Font.Name = "Calibri"
            ws.Cells(FilaActual, ColumnaDetallesML.CostoUnitario).Style.NumberFormat = FormatoStringOtros

            'ws.Cells(FilaActual, 2).Style.NumberFormat=
            FilaActual += 1

        Next
        Dim NameCeldaCantidadHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Cantidad) & Format(FilaActual, "##")
        Dim NameCeldaCIFHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Cif) & Format(FilaActual, "##")
        Dim NameCeldaArancelesHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.DerechosAranceles) & Format(FilaActual, "##")
        Dim NameCeldaFleteHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Flete) & Format(FilaActual, "##")
        Dim NameCeldaHonorariosHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Honorarios) & Format(FilaActual, "##")
        Dim NameCeldaVariosHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.Varios) & Format(FilaActual, "##")
        Dim NameCeldaCostoTotalMLHasta As String = ExcelColumnCollection.ColumnIndexToName(ColumnaDetallesML.CostoTotalML) & Format(FilaActual, "##")


        ws.Cells(FilaActual, ColumnaDetallesML.Cantidad).Formula = "=Sum(" & NameCeldaCantidadDesde & ":" & NameCeldaCantidadHasta & ")"
        ws.Cells(FilaActual, ColumnaDetallesML.Cantidad).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetallesML.Cantidad).Style.NumberFormat = FormatoStringCantidad
        ws.Cells(FilaActual, ColumnaDetallesML.Cantidad).Style.Font.Weight = ExcelFont.BoldWeight


        ws.Cells(FilaActual, ColumnaDetallesML.Cif).Formula = "=Sum(" & NameCeldaCIFDesde & ":" & NameCeldaCIFHasta & ")"
        ws.Cells(FilaActual, ColumnaDetallesML.Cif).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetallesML.Cif).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetallesML.Cif).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetallesML.DerechosAranceles).Formula = "=Sum(" & NameCeldaArancelesDesde & ":" & NameCeldaArancelesHasta & ")"
        ws.Cells(FilaActual, ColumnaDetallesML.DerechosAranceles).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetallesML.DerechosAranceles).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetallesML.DerechosAranceles).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetallesML.Flete).Formula = "=Sum(" & NameCeldaFleteDesde & ":" & NameCeldaFleteHasta & ")"
        ws.Cells(FilaActual, ColumnaDetallesML.Flete).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetallesML.Flete).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetallesML.Flete).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetallesML.Honorarios).Formula = "=Sum(" & NameCeldaHonorariosDesde & ":" & NameCeldaHonorariosHasta & ")"
        ws.Cells(FilaActual, ColumnaDetallesML.Honorarios).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetallesML.Honorarios).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetallesML.Honorarios).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetallesML.Varios).Formula = "=Sum(" & NameCeldaVariosDesde & ":" & NameCeldaVariosHasta & ")"
        ws.Cells(FilaActual, ColumnaDetallesML.Varios).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetallesML.Varios).Style.NumberFormat = FormatoStringOtros
        ws.Cells(FilaActual, ColumnaDetallesML.Varios).Style.Font.Weight = ExcelFont.BoldWeight

        ws.Cells(FilaActual, ColumnaDetallesML.CostoTotalML).Formula = "=Sum(" & NameCeldaCostoTotalMLDesde & ":" & NameCeldaCostoTotalMLHasta & ")"
        ws.Cells(FilaActual, ColumnaDetallesML.CostoTotalML).Style.Font.Name = "Calibri"
        ws.Cells(FilaActual, ColumnaDetallesML.CostoTotalML).Style.NumberFormat = FormatoStringMoneda
        ws.Cells(FilaActual, ColumnaDetallesML.CostoTotalML).Style.Font.Weight = ExcelFont.BoldWeight






    End Sub
#End Region
    Private Sub RetrieveDatosLocales()

        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUES _
        Where p.EMBARQUE_CLAVE = EmbarqueClaveLocal _
        Select p
        For Each secuencia In q
            TipoDeCambioLocal = q(0).TIPO_CAMBIO
            FACTURA_NOLocal = q(0).FACTURA_NO
            FECHA_FACTURALocal = q(0).FECHA_FACTURA
            CONTROL_NOLocal = q(0).CONTROL_NO
            PROVEEDOR_NOMBRELocal = q(0).PROVEEDOR_NOMBRE
            MMP_FOLIOLocal = q(0).MMP_FOLIO
            FECHA_INGRESOLocal = q(0).FECHA_INGRESO
        Next

        TOTALFOB = (From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES Where p.EMBARQUE_CLAVE = EmbarqueClaveLocal Select p.DMP_CANTID * p.DMP_PRECIO).Sum

    End Sub
    'Sub ValuesSample(ByVal ws As ExcelWorksheet)
    '    ws.Cells(0, 0).Value = "Cell value examples:"

    '    ' Column width of 25 and 40 characters.
    '    ws.Columns(0).Width = 25 * 256
    '    ws.Columns(1).Width = 40 * 256

    '    Dim row As Integer = 1

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "Type"
    '    ws.Cells(row, 1).Value = "Value"

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.DBNull:"
    '    ws.Cells(row, 1).Value = System.DBNull.Value

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Byte:"
    '    ws.Cells(row, 1).Value = Byte.MaxValue

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Int16:"
    '    ws.Cells(row, 1).Value = Short.MinValue

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Int64:"
    '    ws.Cells(row, 1).Value = Long.MinValue

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Int32:"
    '    ws.Cells(row, 1).Value = CType(-5678, Integer)

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Single:"
    '    ws.Cells(row, 1).Value = Single.MaxValue

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Double:"
    '    ws.Cells(row, 1).Value = Double.MaxValue

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Boolean:"
    '    ws.Cells(row, 1).Value = True

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Char:"
    '    ws.Cells(row, 1).Value = "a"c

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Text.StringBuilder:"
    '    ws.Cells(row, 1).Value = New System.Text.StringBuilder("StringBuilder text.")

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.Decimal:"
    '    ws.Cells(row, 1).Value = New Decimal(50000)

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.DateTime:"
    '    ws.Cells(row, 1).Value = DateTime.Now

    '    row = row + 1
    '    ws.Cells(row, 0).Value = "System.String:"
    '    ws.Cells(row, 1).Value = "Microsoft Excel is a spreadsheet program written and distributed by Microsoft for computers using the Microsoft Windows operating system and Apple Macintosh computers. It is overwhelmingly the dominant spreadsheet application available for these platforms and has been so since version 5 1993 and its bundling as part of Microsoft Office." + vbLf _
    '        + "Microsoft originally marketed a spreadsheet program called Multiplan in 1982, which was very popular on CP/M systems, but on MS-DOS systems it lost popularity to Lotus 1-2-3. This promoted development of a new spreadsheet called Excel which started with the intention to, in the words of Doug Klunder, 'do everything 1-2-3 does and do it better' . The first version of Excel was released for the Mac in 1985 and the first Windows version (numbered 2.0 to line-up with the Mac and bundled with a run-time Windows environment) was released in November 1987. Lotus was slow to bring 1-2-3 to Windows and by 1988 Excel had started to outsell 1-2-3 and helped Microsoft achieve the position of leading PC software developer. This accomplishment, dethroning the king of the software world, solidified Microsoft as a valid competitor and showed its future of developing graphical software. Microsoft pushed its advantage with regular new releases, every two years or so. The current version is Excel 11, also called Microsoft Office Excel 2003." + vbLf _
    '        + "Early in its life Excel became the target of a trademark lawsuit by another company already selling a software package named 'Excel.' As the result of the dispute Microsoft was required to refer to the program as 'Microsoft Excel' in all of its formal press releases and legal documents. However, over time this practice has slipped." + vbLf _
    '        + "Excel offers a large number of user interface tweaks, however the essence of UI remains the same as in the original spreadsheet, VisiCalc: the cells are organized in rows and columns, and contain data or formulas with relative or absolute references to other cells." + vbLf _
    '        + "Excel was the first spreadsheet that allowed the user to define the appearance of spreadsheets (fonts, character attributes and cell appearance). It also introduced intelligent cell recomputation, where only cells dependent on the cell being modified are updated, while previously spreadsheets recomputed everything all the time or waited for a specific user command. Excel has extensive graphing capabilities." + vbLf _
    '        + "When first bundled into Microsoft Office in 1993, Microsoft Word and Microsoft PowerPoint had their GUIs redesigned for consistency with Excel, the killer app on the PC at the time." + vbLf _
    '        + "Since 1993 Excel includes support for Visual Basic for Applications (VBA) as a scripting language. VBA is a powerful tool that makes Excel a complete programming environment. VBA and macro recording allow automating routines that otherwise take several manual steps. VBA allows creating forms to handle user input. Automation functionality of VBA exposed Excel as a target for macro viruses." + vbLf _
    '        + "Excel versions from 5.0 to 9.0 contain various Easter eggs." + vbLf + vbLf + "For more information see: http://en.wikipedia.org/wiki/Microsoft_Excel"

    '    row = row + 2
    '    ws.Cells(row, 0).Value = "DataTable insert example:"
    '    'ws.InsertDataTable(FeatureSamplesVB.CreateData(), row, 2, True)

    'End Sub


    'Function CreateData() As DataTable
    '    Dim dt As DataTable = New DataTable

    '    dt.Columns.Add("ID", Type.GetType("System.Int32"))
    '    dt.Columns.Add("FirstName", Type.GetType("System.String"))
    '    dt.Columns.Add("LastName", Type.GetType("System.String"))

    '    dt.Rows.Add(New Object() {100, "John", "Doe"})
    '    dt.Rows.Add(New Object() {101, "Fred", "Nurk"})
    '    dt.Rows.Add(New Object() {103, "Hans", "Meier"})
    '    dt.Rows.Add(New Object() {104, "Ivan", "Horvat"})
    '    dt.Rows.Add(New Object() {105, "Jean", "Dupont"})
    '    dt.Rows.Add(New Object() {106, "Mario", "Rossi"})

    '    Return dt
    'End Function
    Sub StylesSample(ByVal ws As ExcelWorksheet)
        ws.Cells(0, 0).Value = "Cell style examples:"

        Dim row As Integer = 0

        ' Column width of 4, 30 and 35 characters.
        ws.Columns(0).Width = 4 * 256
        ws.Columns(1).Width = 30 * 256
        ws.Columns(2).Width = 35 * 256

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ws.Cells(row, 2).Value = "Color.Blue"
        ws.Cells(row, 2).Style.Font.Color = Color.Blue

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ws.Cells(row, 2).Value = "true"
        ws.Cells(row, 2).Style.Font.Italic = True

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ws.Cells(row, 2).Value = "Comic Sans MS"
        ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ws.Cells(row, 2).Value = "18 * 20"
        ws.Cells(row, 2).Style.Font.Size = 18 * 20

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ws.Cells(row, 2).Value = "true"
        ws.Cells(row, 2).Style.Font.Strikeout = True

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Indent"
        ws.Cells(row, 2).Value = "five"
        ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ws.Cells(row, 2).Style.Indent = 5

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ws.Cells(row, 2).Value = "true"
        ' Set row height to 50 points.
        ws.Rows(row).Height = 50 * 20
        ws.Cells(row, 2).Style.IsTextVertical = True

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ws.Cells(row, 2).Value = 1234
        ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.Rotation"
        ws.Cells(row, 2).Value = "35 degrees up"
        ws.Cells(row, 2).Style.Rotation = 35

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ws.Cells(row, 2).Style.ShrinkToFit = True

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ' Set row height to 30 points.
        ws.Rows(row).Height = 30 * 20
        ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top

        row = row + 2
        ws.Cells(row, 1).Value = ".Style.WrapText"
        ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ws.Cells(row, 2).Style.WrapText = True
    End Sub
    'Private skyscrapers(,) As Object = New Object(20, 6) _
    '{ _
    '    {"Rank", "Building", "City", "Metric", "Imperial", "Floors", "Built (Year)"}, _
    '    {1, "Taipei 101", "Taipei", 509, 1671, 101, 2004}, _
    '    {2, "Petronas Tower 1", "Kuala Lumpur", 452, 1483, 88, 1998}, _
    '    {3, "Petronas Tower 2", "Kuala Lumpur", 452, 1483, 88, 1998}, _
    '    {4, "Sears Tower", "Chicago", 442, 1450, 108, 1974}, _
    '    {5, "Jin Mao Tower", "Shanghai", 421, 1380, 88, 1998}, _
    '    {6, "2 International Finance Centre", "Hong Kong", 415, 1362, 88, 2003}, _
    '    {7, "CITIC Plaza", "Guangzhou", 391, 1283, 80, 1997}, _
    '    {8, "Shun Hing Square", "Shenzhen", 384, 1260, 69, 1996}, _
    '    {9, "Empire State Building", "New York City", 381, 1250, 102, 1931}, _
    '    {10, "Central Plaza", "Hong Kong", 374, 1227, 78, 1992}, _
    '    {11, "Bank of China Tower", "Hong Kong", 367, 1205, 72, 1990}, _
    '    {12, "Emirates Office Tower", "Dubai", 355, 1163, 54, 2000}, _
    '    {13, "Tuntex Sky Tower", "Kaohsiung", 348, 1140, 85, 1997}, _
    '    {14, "Aon Center", "Chicago", 346, 1136, 83, 1973}, _
    '    {15, "The Center", "Hong Kong", 346, 1135, 73, 1998}, _
    '    {16, "John Hancock Center", "Chicago", 344, 1127, 100, 1969}, _
    '    {17, "Ryugyong Hotel", "Pyongyang", 330, 1083, 105, 1992}, _
    '    {18, "Burj Al Arab", "Dubai", 321, 1053, 60, 1999}, _
    '    {19, "Chrysler Building", "New York City", 319, 1046, 77, 1930}, _
    '    {20, "Bank of America Plaza", "Atlanta", 312, 1023, 55, 1992} _
    '}


    Sub TypicalTableSample(ByVal ws As ExcelWorksheet)



        'Dim i As Integer, j As Integer

        'For j = 0 To 7 - 1 Step j + 1
        '    ws.Cells(3, j).Value = skyscrapers(0, j)
        'Next

        ws.Cells.GetSubrangeAbsolute(2, 0, 3, 0).Merged = True
        ws.Cells.GetSubrangeAbsolute(2, 1, 3, 1).Merged = True
        ws.Cells.GetSubrangeAbsolute(2, 2, 3, 2).Merged = True
        ws.Cells.GetSubrangeAbsolute(2, 3, 2, 4).Merged = True
        ws.Cells(2, 3).Value = "Height"
        ws.Cells.GetSubrangeAbsolute(2, 5, 3, 5).Merged = True
        ws.Cells.GetSubrangeAbsolute(2, 6, 3, 6).Merged = True

        Dim tmpStyle As CellStyle = New CellStyle

        tmpStyle.HorizontalAlignment = HorizontalAlignmentStyle.Center
        tmpStyle.VerticalAlignment = VerticalAlignmentStyle.Center
        tmpStyle.FillPattern.SetSolid(Color.Chocolate)
        tmpStyle.Font.Weight = ExcelFont.BoldWeight
        tmpStyle.Font.Color = Color.White
        tmpStyle.WrapText = True
        tmpStyle.Borders.SetBorders(MultipleBorders.Right Or MultipleBorders.Top, Color.Black, LineStyle.Thin)

        ws.Cells.GetSubrangeAbsolute(2, 0, 3, 6).Style = tmpStyle

        tmpStyle = New CellStyle

        tmpStyle.HorizontalAlignment = HorizontalAlignmentStyle.Center
        tmpStyle.VerticalAlignment = VerticalAlignmentStyle.Center
        tmpStyle.Font.Weight = ExcelFont.BoldWeight

        Dim mergedRange As CellRange = ws.Cells.GetSubrangeAbsolute(4, 7, 13, 7)
        mergedRange.Merged = True
        mergedRange.Value = "T o p   1 0"
        tmpStyle.Rotation = -90
        tmpStyle.FillPattern.SetSolid(Color.Lime)
        mergedRange.Style = tmpStyle

        mergedRange = ws.Cells.GetSubrangeAbsolute(4, 8, 23, 8)
        mergedRange.Merged = True
        mergedRange.Value = "T o p   2 0"
        tmpStyle.IsTextVertical = True
        tmpStyle.FillPattern.SetSolid(Color.Gold)
        mergedRange.Style = tmpStyle

        mergedRange = ws.Cells.GetSubrangeAbsolute(14, 7, 23, 7)
        mergedRange.Merged = True
        mergedRange.Style = tmpStyle

        'For i = 0 To 19
        '    For j = 0 To 6
        '        Dim cell As ExcelCell = ws.Cells(i + 4, j)

        '        cell.Value = skyscrapers(i + 1, j)

        '        If i Mod 2 = 0 Then
        '            cell.Style.FillPattern.SetSolid(Color.LightSkyBlue)
        '        Else
        '            cell.Style.FillPattern.SetSolid(Color.FromArgb(210, 210, 230))
        '        End If

        '        If j = 3 Then
        '            cell.Style.NumberFormat = "#" + ControlChars.Quote + " m" + ControlChars.Quote
        '        End If

        '        If j = 4 Then
        '            cell.Style.NumberFormat = "#" + ControlChars.Quote + " ft" + ControlChars.Quote
        '        End If

        '        If j > 2 Then
        '            cell.Style.Font.Name = "Courier New"
        '        End If

        '        cell.Style.Borders(IndividualBorder.Right).LineStyle = LineStyle.Thin
        '    Next j
        'Next i

        ws.Cells.GetSubrange("A5", "I24").SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Double)
        ws.Cells.GetSubrange("A3", "G4").SetBorders(MultipleBorders.Vertical Or MultipleBorders.Top, Color.Black, LineStyle.Double)
        ws.Cells.GetSubrange("A5", "H14").SetBorders(MultipleBorders.Bottom Or MultipleBorders.Right, Color.Black, LineStyle.Double)

        ws.Cells("A27").Value = "Notes:"
        ws.Cells("A28").Value = "a) 'Metric' and 'Imperial' columns use custom number formatting."
        ws.Cells("A29").Value = "b) All number columns use 'Courier New' font for improved number readability."
        ws.Cells("A30").Value = "c) Multiple merged ranges were used for table header and categories header."
    End Sub

    Sub ReferencingAndGroupsSample(ByVal ws As ExcelWorksheet)
        ws.Cells(0).Value = "Cell referencing and grouping examples:"

        ws.Cells("B2").Value = "Cell B2."
        ws.Cells(6, 0).Value = "Cell in row 7 and column A."

        ws.Rows(2).Cells(0).Value = "Cell in row 3 and column A."
        ws.Rows("4").Cells("B").Value = "Cell in row 4 and column B."

        ws.Columns(2).Cells(4).Value = "Cell in column C and row 5."
        ws.Columns("AA").Cells("6").Value = "Cell in AA column and row 6."

        Dim cr As CellRange = ws.Rows(7).Cells

        cr(0).Value = cr.IndexingMode
        cr(3).Value = "D8"
        cr("B").Value = "B8"

        cr = ws.Columns(7).Cells

        cr(0).Value = cr.IndexingMode
        cr(2).Value = "H3"
        cr("5").Value = "H5"

        cr = ws.Cells.GetSubrange("I2", "L8")
        cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)

        cr("J7").Value = cr.IndexingMode
        cr(0, 0).Value = "I2"
        cr("J3").Value = "J3"
        cr(4).Value = "I3" ' Cell range width is 4 (I J K L).

        ' Vertical grouping.
        ws.Cells(12, 0).Value = "GroupA Start"
        ws.Rows(12).OutlineLevel = 1
        ws.Cells(13, 0).Value = "A"
        ws.Rows(13).OutlineLevel = 1
        ws.Cells(14, 1).Value = "GroupB Start"
        ws.Rows(14).OutlineLevel = 2
        ws.Cells(15, 1).Value = "B"
        ws.Rows(15).OutlineLevel = 2
        ws.Cells(16, 1).Value = "GroupB End"
        ws.Rows(16).OutlineLevel = 2
        ws.Cells(17, 0).Value = "GroupA End"
        ws.Rows(17).OutlineLevel = 1
        ' Put outline row buttons above groups.
        ws.ViewOptions.OutlineRowButtonsBelow = False

        ' Horizontal grouping (collapsed).
        ws.Cells("E12").Value = "Gr.C Start"
        ws.Columns("E").OutlineLevel = 1
        ws.Columns("E").Collapsed = True
        ws.Cells("F12").Value = "C"
        ws.Columns("F").OutlineLevel = 1
        ws.Columns("F").Collapsed = True
        ws.Cells("G12").Value = "Gr.C End"
        ws.Columns("G").OutlineLevel = 1
        ws.Columns("G").Collapsed = True
    End Sub



End Module
