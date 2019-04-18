Namespace Bis.Sap.Common

    Partial Class EmbarquesData

        Partial Class EMBARQUESGASTOSDataTable
            Public Event TotalT1(ByVal Total As Decimal)
            Public Event TotalT2(ByVal Total As Decimal)
            Public Event TotalT3(ByVal Total As Decimal)
            Private Sub EMBARQUESGASTOSDataTable_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanged
                If e.Column.ColumnName = Me.MONTOColumn.ColumnName Then
                    Totalizar(CInt(e.Row.Item(Me.TIPOColumn)))
                End If

            End Sub
            Private Sub Totalizar(ByVal inTipo As Integer)
                Dim Total As Decimal
                Dim row As EMBARQUESGASTOSRow
                For Each row In Me.Rows
                    If row.RowState = DataRowState.Deleted Then
                        Continue For
                    End If
                    If row.TIPO = inTipo Then
                        Total += row.MONTO
                    End If
                Next
                Select Case inTipo
                    Case 1
                        RaiseEvent TotalT1(Total)
                    Case 2
                        RaiseEvent TotalT2(Total)
                    Case 3
                        RaiseEvent TotalT3(Total)
                End Select

            End Sub

            Private Sub TotalizarT3()

            End Sub
            Private Sub EMBARQUESGASTOSDataTable_EMBARQUESGASTOSRowChanging(ByVal sender As System.Object, ByVal e As EMBARQUESGASTOSRowChangeEvent) Handles Me.EMBARQUESGASTOSRowChanging


            End Sub

        End Class

        Partial Class EMBARQUESDETALLESDataTable
            Public Event Totales()

            Private Sub EMBARQUESDETALLESDataTable_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanged
                Select Case e.Column.ColumnName
                    Case Me.DMP_PRECIOColumn.ColumnName
                        If CDbl(e.Row.Item(Me.DMP_PRECIOColumn)) > 0 Then
                            e.Row.Item(Me.PFOBColumn) = CDec(e.Row.Item(Me.DMP_CANTIDColumn)) * CDec(e.Row.Item(Me.DMP_PRECIOColumn))
                        End If
                        Totalizar()
                    Case Me.PRO_GRAMAJColumn.ColumnName
                        If CDbl(e.Row.Item(Me.DMP_CANTIDColumn)) > 0 Then
                            e.Row.Item(Me.KILOSColumn) = CDec(e.Row.Item(Me.DMP_CANTIDColumn)) * CDec(e.Row.Item(Me.PRO_GRAMAJColumn)) * 0.001
                        End If

                End Select
            End Sub

            Private Sub EMBARQUESDETALLESDataTable_ColumnChanging(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
                Select Case e.Column.ColumnName
                    Case Me.DMP_PRECIOColumn.ColumnName
                        Totalizar()
                    Case Me.PRO_GRAMAJColumn.ColumnName
                        'Totalizar()
                        'Case Me.FleteColumn.ColumnName
                        '    Totalizar()
                        'Case Me.OtrosAjustesColumn.ColumnName
                        Totalizar()
                End Select
            End Sub

            Private Sub Totalizar()

                RaiseEvent Totales()
            End Sub

        End Class


    End Class

End Namespace
Namespace Bis.Sap.Common.EmbarquesDataTableAdapters
    
    Partial Public Class EMBARQUESORIGENTableAdapter
    End Class
End Namespace
