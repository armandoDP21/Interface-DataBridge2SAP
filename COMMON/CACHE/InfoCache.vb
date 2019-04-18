Option Strict On
Option Explicit On

Namespace Bis.Sap.Common
    Public Enum Paises
        Guatemala = 2
        Honduras = 3
        Panama = 4
        ElSalvador = 5
        CostaRica = 6
        Quaker = 7

    End Enum
    Public Class Constantes
        Public Const MargenGrid As Integer = 28
        Public Const FormatoMoneda As String = "#,0.00;(#,0.00)"
        Public Const FormatoMoneda4Dec As String = "#,0.0000;(#,0.0000)"
        Public Const FormatoFecha As String = "dd MMM yyyy"
        Public Const FormatoP100UnDecimal As String = "0.0"
        Public Const FilesGlobalDirectory As String = "C:\LCostos"

    End Class

    Public Class InfoCache

        Private Shared ExcelFormato As Integer

        Public Shared Property VersionExcel() As Integer
            Get
                Return ExcelFormato
            End Get
            Set(ByVal Value As Integer)
                ExcelFormato = Value
            End Set
        End Property

        Private Shared EmbarquesDataSet As EmbarquesData
        Public Shared Property EmbarquesDS() As EmbarquesData
            Get
                Return EmbarquesDataSet
            End Get
            Set(ByVal Value As EmbarquesData)
                EmbarquesDataSet = Value
            End Set
        End Property

        Private Shared NuevosEmbarquesDataSet As EmbarquesData
        Public Shared Property NuevosEmbarquesDS() As EmbarquesData
            Get
                Return NuevosEmbarquesDataSet
            End Get
            Set(ByVal Value As EmbarquesData)
                NuevosEmbarquesDataSet = Value
            End Set
        End Property

#Region " ConnectionString "
        Private Shared fieldConnectionString As String = String.Empty
        Public Shared Property ConnectionString() As String
            Get
                Return fieldConnectionString
            End Get
            Set(ByVal Value As String)
                fieldConnectionString = Value
            End Set
        End Property
        Private Shared HostServer As String = String.Empty
        Public Shared Property ServerHost() As String
            Get
                Return HostServer
            End Get
            Set(ByVal Value As String)
                HostServer = Value
            End Set
        End Property
        Private Shared CatalogoName As String = String.Empty
        Public Shared Property Catalogo() As String
            Get
                Return CatalogoName
            End Get
            Set(ByVal Value As String)
                CatalogoName = Value
            End Set
        End Property
        Private Shared IdU As String
        Public Shared Property UId() As String
            Get
                Return IdU
            End Get
            Set(ByVal value As String)
                IdU = value
            End Set
        End Property
        Private Shared SAPuserActual As String
        Public Shared Property SAPuser() As String
            Get
                Return SAPuserActual
            End Get
            Set(ByVal value As String)
                SAPuserActual = value
            End Set
        End Property

#End Region
        Private Shared TipoDeCambioActual As Decimal
        Public Shared Property TipoDeCambio() As Decimal
            Get
                Return TipoDeCambioActual
            End Get
            Set(ByVal value As Decimal)
                TipoDeCambioActual = value
            End Set
        End Property
        Private Shared SecuenciaIniciaActual As Long
        Public Shared Property SecuenciaInicia() As Long
            Get
                Return SecuenciaIniciaActual
            End Get
            Set(ByVal value As Long)
                SecuenciaIniciaActual = value
            End Set
        End Property
        Private Shared SecuenciaTerminaActual As Long
        Public Shared Property SecuenciaTermina() As Long
            Get
                Return SecuenciaTerminaActual
            End Get
            Set(ByVal value As Long)
                SecuenciaTerminaActual = value
            End Set
        End Property

        Private Shared SecuenciaActual As Long
        Public Shared Property Secuencia() As Long
            Get
                Return SecuenciaActual
            End Get
            Set(ByVal value As Long)
                SecuenciaActual = value
            End Set
        End Property


        Private Shared SemanaActual As Long
        Public Shared Property Semana() As Long
            Get
                Return SemanaActual
            End Get
            Set(ByVal value As Long)
                SemanaActual = value
            End Set
        End Property
        Private Shared PaisClaveActual As Integer
        Public Shared Property PaisClave() As Integer
            Get
                Return PaisClaveActual
            End Get
            Set(ByVal value As Integer)
                PaisClaveActual = value
            End Set
        End Property
        Private Shared SucursalClaveActual As Integer
        Public Shared Property SucursalClave() As Integer
            Get
                Return SucursalClaveActual
            End Get
            Set(ByVal value As Integer)
                SucursalClaveActual = value
            End Set
        End Property
        Private Shared MonedaClaveActual As String
        Public Shared Property MonedaClave() As String
            Get
                Return MonedaClaveActual
            End Get
            Set(ByVal value As String)
                MonedaClaveActual = value
            End Set
        End Property
        Private Shared PeriodoContableActual As String
        Public Shared Property PeriodoActual() As String
            Get
                Return PeriodoContableActual
            End Get
            Set(ByVal value As String)
                PeriodoContableActual = value
            End Set
        End Property
        Private Shared FiscalYearActual As Integer
        Public Shared Property FiscalYear() As Integer
            Get
                Return FiscalYearActual
            End Get
            Set(ByVal value As Integer)
                FiscalYearActual = value
            End Set
        End Property
        Private Shared CurrencyCodeActual As String
        Public Shared Property CurrencyCode() As String
            Get
                Return CurrencyCodeActual
            End Get
            Set(ByVal value As String)
                CurrencyCodeActual = value
            End Set
        End Property
        Private Shared ClaveCompaniaActual As String
        Public Shared Property ClaveCompania() As String
            Get
                Return ClaveCompaniaActual
            End Get
            Set(ByVal value As String)
                ClaveCompaniaActual = value
            End Set
        End Property
        Private Shared NombreUsuarioActual As String
        Public Shared Property NombreUsuario() As String
            Get
                Return NombreUsuarioActual
            End Get
            Set(ByVal value As String)
                NombreUsuarioActual = value
            End Set
        End Property
        Private Shared NombrePaisActual As String
        Public Shared Property NombrePais() As String
            Get
                Return NombrePaisActual
            End Get
            Set(ByVal value As String)
                NombrePaisActual = value
            End Set
        End Property
        Private Shared APSActual As String
        Public Shared Property APS() As String
            Get
                Return APSActual
            End Get
            Set(ByVal value As String)
                APSActual = value
            End Set
        End Property
        Private Shared ESTATUS_SAPActual As String
        Public Shared Property ESTATUS_SAP() As String
            Get
                Return ESTATUS_SAPActual
            End Get
            Set(ByVal value As String)
                ESTATUS_SAPActual = value
            End Set
        End Property
        Private Shared TipoPolizasActual As String
        Public Shared Property TipoPolizas() As String
            Get
                Return TipoPolizasActual
            End Get
            Set(ByVal value As String)
                TipoPolizasActual = value
            End Set
        End Property
#Region " Errores DDL "
        Private Shared UpdateErrorMessage As String
        Private Shared ValoresDuplicados As String
        Public Shared Property UpdateError() As String
            Get
                Return UpdateErrorMessage

            End Get
            Set(ByVal value As String)
                UpdateErrorMessage = value
            End Set
        End Property
        Public Shared Property Duplicados() As String
            Get
                Return ValoresDuplicados
            End Get
            Set(ByVal value As String)
                ValoresDuplicados = value
            End Set
        End Property
#End Region
#Region " Variables reportes "
        Private Shared DesdeFecha As Date
        Public Shared Property FechaDesde() As Date
            Get
                Return DesdeFecha
            End Get
            Set(ByVal value As Date)
                DesdeFecha = value
            End Set
        End Property
        Private Shared HastaFecha As Date
        Public Shared Property FechaHasta() As Date
            Get
                Return HastaFecha
            End Get
            Set(ByVal value As Date)
                HastaFecha = value
            End Set
        End Property
        Private Shared Id01 As Integer
        Public Shared Property Pk01() As Integer
            Get
                Return Id01
            End Get
            Set(ByVal value As Integer)
                Id01 = value
            End Set
        End Property
        Private Shared VerPreliminar As Boolean
        Public Shared Property VistaPreliminar() As Boolean
            Get
                Return VerPreliminar
            End Get
            Set(ByVal value As Boolean)
                VerPreliminar = value
            End Set
        End Property
#End Region
#Region " Reportes "
        Private Shared ReportesDT As DataTable
        Public Shared Property ReportesTable() As DataTable
            Get
                Return ReportesDT
            End Get
            Set(ByVal Value As DataTable)
                ReportesDT = Value
            End Set
        End Property
        Private Shared ReporteId As Integer
        Public Shared Property IdReporte() As Integer
            Get
                Return ReporteId
            End Get
            Set(ByVal value As Integer)
                ReporteId = value
            End Set
        End Property
#End Region
    End Class



End Namespace

