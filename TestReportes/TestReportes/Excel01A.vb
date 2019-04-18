
Imports System.Drawing
Imports System.IO
Imports System.Data
Imports System.Text
Imports GemBox.Spreadsheet
Imports System.Collections.Generic

Module Excel01A

    Public Sub TestExcel01A()

        ' If using Professional version, put your serial key below. Otherwise, keep following
        ' line commented out as Free version doesn't have SetLicense method.
        ' SpreadsheetInfo.SetLicense("YOUR-SERIAL-KEY-HERE")

        Dim ef As ExcelFile = New ExcelFile
        Dim ws As GemBox.Spreadsheet.ExcelWorksheet = ef.Worksheets.Add("Formula Generator")

        ' Fill first column with values.
        Dim i As Int32
        For i = 0 To 9 Step 1
            ws.Cells(i, 0).Value = i + 1
        Next

        ' Cell B1 has formula '=A1*2', B2 '=A2*2', etc.
        For i = 0 To 9 Step 1
            ws.Cells(i, 1).Formula = String.Format("={0}*2", CellRange.RowColumnToPosition(i, 0))
        Next

        ' Cell C1 has formula '=SUM(A1:B1)', C2 '=SUM(A2:B2)', etc.
        For i = 0 To 9 Step 1
            ws.Cells(i, 2).Formula = String.Format("=SUM(A{0}:B{0})", ExcelRowCollection.RowIndexToName(i))
        Next

        ' Cell A12 contains sum of all values from the first row.
        Dim Fila1Name As String = ExcelColumnCollection.ColumnIndexToName(0)
        ws.Cells("A12").Formula = String.Format("=SUM(A1:{0}1)", ExcelColumnCollection.ColumnIndexToName(ws.Rows(0).AllocatedCells.Count - 1))
        Dim outPath As String = "C:\Users\armando\Desktop\Formula Generator.xlsx"

        ef.SaveXlsx(outPath)


    End Sub
End Module
