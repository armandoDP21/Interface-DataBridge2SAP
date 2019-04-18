Public Class Costeo
    Private EmbarqueClave As String
    'PPFOB= DMP_CANTID* DMP_PRECIO de ODS_MOVIMIENTOPRODUCTO
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
    'PPCosto$=PPCostoU*TipoCambio
    'PPCifQ=PPCosto*TipoCambio

    'PPCostoTotal=PPCIFQ+PPFlete+PPAranceles +PPFlete +TOTALGV
    'PPCostoUnitario=PPCostoTotal /DMP_CANTID
    'PPCostoUnitario$=PPCostoUnitario / TipoDeCambio




    Public Sub New(ByVal InEmbarqueClave As String)
        EmbarqueClave = InEmbarqueClave
    End Sub
End Class
