Module pdcvModule
    'Public Function PopulateDataGrid(ByRef dg As DataGridView)

    '    'Create a DataTable

    '    Dim dt As DataTable = New DataTable("TablaDatos")

    '    Dim dcolumn As DataColumn

    '    Dim i As Integer

    '    Dim count As Integer

    '    Dim colname As String

    '    Dim coltype As System.Type

    '    sbox.AppendText("Current FetchSize value is " + oreader.FetchSize.ToString() + " bytes " + Environment.NewLine)

    '    sbox.Update()

    '    'Get the number of columns in this OracleDataReader

    '    count = oreader.FieldCount

    '    sbox.AppendText("Fetching data with given FetchSize.." + Environment.NewLine)

    '    sbox.Update()

    '    'Add the columns in the DataTable

    '    For i = 0 To count - 1

    '        colname = oreader.GetName(i)

    '        coltype = oreader.GetFieldType(i)

    '        dcolumn = New DataColumn(colname, coltype)

    '        dt.Columns.Add(dcolumn)

    '    Next

    '    'Make the first column the primary key column.

    '    Dim PrimaryKeyColumns(0) As DataColumn

    '    PrimaryKeyColumns(0) = dt.Columns("productname")

    '    dt.PrimaryKey = PrimaryKeyColumns

    '    'Populate the table

    '    While oreader.Read()

    '        Dim drow As DataRow = dt.NewRow()

    '        drow(oreader.GetName(0)) = oreader.GetString(0)

    '        drow(oreader.GetName(1)) = oreader.GetString(1)

    '        drow(oreader.GetName(2)) = oreader.GetString(2)

    '        drow(oreader.GetName(3)) = oreader.GetDecimal(3)

    '        dt.Rows.Add(drow)

    '    End While

    '    'Assign the DataSource 

    '    dg.DataSource = dt

    '    'Dispose and close the OracleDataReader

    '    oreader.Dispose()

    '    oreader.Close()

    'End Function


End Module
