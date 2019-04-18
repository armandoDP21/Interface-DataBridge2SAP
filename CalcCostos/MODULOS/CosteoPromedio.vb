Public Class CosteoPromedio
    Private EmbarqueClave As String
    Private TipoCambio As Decimal
    Private DerechosArancelarios As Decimal
    Private HonorariosGastosInternacion As Decimal
    Private RestoGastos As Decimal

    Private PPFOB As Decimal
    Private TOTALFOB As Decimal
    Private TOTALCIF As Decimal
    Private TOTALKILOS As Decimal
    Private Flete As Decimal
    Private Seguro As Decimal
    Private OtrosGastos As Decimal
    Private FleteTerMarLoc As Decimal

    Private TOTALGastos As Decimal
    Private TOTALCIFQ As Decimal

    Private TOTALPesoK As Decimal
    Private TOTALGV As Decimal

    Private PPFlete As Decimal
    Private PPSeguro As Decimal
    Private PPOtrosGastos As Decimal
    Private PPCosto As Decimal

    Private PPCostoU As Decimal
    Private Gramaje As Decimal
    Private PPPesoK As Decimal
    Private PPCostoML As Decimal
    Private PPCifMl As Decimal

    Private PPCostoTotal As Decimal
    Private PPCostoUnitario As Decimal
    Private PPCostoUnitarioML As Decimal

    'PPFOB= DMP_CANTID* DMP_PRECIO 
    'TOTALFOB=Suma del valor FOB del embarque.
    'TOTALGastos=Flete+Seguro+OtrosGastos
    'TOTALCIFQ= (TOTALFOB+TOTALGastos) * TipoDeCambio
    'TOTALFlete=FleteTterrestre+FleteMarítimo+FleteLocal
    'TOTALPesoK= que calculara esta variable , la suma de todos los Kilos?
    'TOTALGV=Suma de  Gastos Varios

    'PPFlete=(Flete*PPFOB)/TOTALFOB
    'PPSeguro=(Seguro*PPFOB)/TOTALFOB
    'PPOtrosGastos=(OtrosGastos*PPFOB)/TOTALFOB
    'PPCosto=PPFOB+PPFlete+PPSeguro+PPOtrosGastos

    'PPCostoU=PPCosto/UnidadesDeProducto
    'Gramaje=De Master File
    'PPPesoK (Kilos)=(UnidadesDeProducto*Gramaje)/1000
    'PPCostoML=PPCostoU*TipoCambio
    'PPCifMl=PPCosto*TipoCambio

    'PPCostoTotal=PPCIFQ+PPFlete+PPAranceles +PPFlete +TOTALGV
    'PPCostoUnitario=PPCostoTotal /DMP_CANTID
    'PPCostoUnitarioML=PPCostoUnitario / TipoDeCambio




    Public Sub New(ByVal InEmbarqueClave As String)
        EmbarqueClave = InEmbarqueClave
        GetFleteTerMarLoc()
        GetHonorariosRestoGastos()
        TipoCambio = GetTipoCambio()
        DerechosArancelarios = GetDerechosArancelarios()
        HonorariosGastosInternacion = GetHonorariosGastosInternación()
        TOTALFOB = (From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES Where p.EMBARQUE_CLAVE = EmbarqueClave Select p.DMP_CANTID * p.DMP_PRECIO).Sum

        Dim g = From pGastos In InfoCache.EmbarquesDS.EMBARQUESGASTOS _
        Where pGastos.EMBARQUE_CLAVE = EmbarqueClave And pGastos.TIPO = 1 _
        Select pGastos
        For Each sg In g
            Select Case sg.G_CLAVE
                Case 1 'Flete
                    Flete = sg.MONTO
                Case 2 'Seguro
                    Seguro = sg.MONTO
                Case 3 'Otros Gastos
                    OtrosGastos = sg.MONTO
            End Select
        Next


        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES _
               Where p.EMBARQUE_CLAVE = EmbarqueClave
               Select p

        For Each secuencia In q
            'secuencia.PFOB = (secuencia.DMP_CANTID * secuencia.DMP_PRECIO) / TOTALFOB
            secuencia.AVG_FLETE = (Flete * secuencia.DMP_CANTID * secuencia.DMP_PRECIO) / TOTALFOB
            secuencia.AVG_SEGUROS = (Seguro * secuencia.DMP_CANTID * secuencia.DMP_PRECIO) / TOTALFOB
            secuencia.AVG_OG = (OtrosGastos * secuencia.DMP_CANTID * secuencia.DMP_PRECIO) / TOTALFOB

            secuencia.CU = ((secuencia.DMP_CANTID * secuencia.DMP_PRECIO) + secuencia.AVG_FLETE + secuencia.AVG_SEGUROS + secuencia.AVG_OG) / secuencia.DMP_CANTID
            secuencia.ML_PU = secuencia.CU * TipoCambio
            'secuencia.ML_CU = (secuencia.CostoTotal / secuencia.DMP_CANTID) * TipoCambio
            secuencia.ML_CIF = ((secuencia.DMP_CANTID * secuencia.DMP_PRECIO) + secuencia.AVG_FLETE + secuencia.AVG_SEGUROS + secuencia.AVG_OG) * TipoCambio
        Next
        TOTALCIF = (From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES Where p.EMBARQUE_CLAVE = EmbarqueClave Select p.ML_CIF).Sum
        TOTALKILOS = (From p In InfoCache.EmbarquesDS.EMBARQUESDETALLES Where p.EMBARQUE_CLAVE = EmbarqueClave Select p.KILOS).Sum

        For Each secuencia In q
            secuencia.ML_ARANCEL = DerechosArancelarios * (secuencia.ML_CIF / TOTALCIF)

            If FleteTerMarLoc > 0 Then
                secuencia.ML_FLETE = (secuencia.KILOS * FleteTerMarLoc) / TOTALKILOS
            Else
                secuencia.ML_FLETE = 0
            End If

            secuencia.ML_HONORARIOS = (secuencia.ML_CIF / TOTALCIF) * HonorariosGastosInternacion
            secuencia.ML_VARIOS = (RestoGastos / TOTALCIF) * secuencia.ML_CIF
            secuencia.ML_TOTAL = secuencia.ML_CIF + secuencia.ML_ARANCEL + secuencia.ML_FLETE + secuencia.ML_HONORARIOS + secuencia.ML_VARIOS
            secuencia.ML_CU = secuencia.ML_TOTAL / secuencia.DMP_CANTID
            secuencia.ML_PU = secuencia.ML_CU / TipoCambio
        Next


    End Sub
    Private Function GetTipoCambio() As Decimal
        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUES _
         Where p.EMBARQUE_CLAVE = EmbarqueClave _
         Select p.TIPO_CAMBIO
        Return q(0)
    End Function
    Private Function GetDerechosArancelarios() As Decimal
        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESGASTOS _
         Where p.G_CLAVE = 7 And p.EMBARQUE_CLAVE = EmbarqueClave _
         Select p.MONTO
        Return q(0)
    End Function
    Private Sub GetFleteTerMarLoc()
        FleteTerMarLoc = (From p In InfoCache.EmbarquesDS.EMBARQUESGASTOS
                    Where (p.EMBARQUE_CLAVE = EmbarqueClave) And (p.G_CLAVE = 4 Or p.G_CLAVE = 5 Or p.G_CLAVE = 6) Select p.MONTO).Sum
    End Sub
    Private Function GetHonorariosGastosInternación() As Decimal
        Dim q = From p In InfoCache.EmbarquesDS.EMBARQUESGASTOS _
                 Where p.G_CLAVE = 8 And p.EMBARQUE_CLAVE = EmbarqueClave _
                 Select p.MONTO
        Return q(0)
    End Function

    Private Sub GetHonorariosRestoGastos()
        RestoGastos = (From p In InfoCache.EmbarquesDS.EMBARQUESGASTOS
                      Where (p.EMBARQUE_CLAVE = EmbarqueClave) And p.G_CLAVE > 8 Select p.MONTO).Sum

    End Sub
End Class
