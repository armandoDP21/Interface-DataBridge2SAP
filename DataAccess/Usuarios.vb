Imports Bis.Sap.Common

Imports System.Data.Common
Imports System.Text

'Option Strict On
'Option Explicit On


Namespace Bis.Sap.DataAccess
    Public Class Usuarios
#Region " Internal members "

        Private Const USUARIO_CLAVE_FIELD As String = "USUARIO_CLAVE"
        Private Const NOMBRE_FIELD As String = "NOMBRE"
        Private Const PAIS_CLAVE_FIELD As String = "RUC"
        Private Const SUC_CLAVE_FIELD As String = "SUC_CLAVE"
        Private Const MONEDA_CLAVE_FIELD As String = "MONEDA_CLAVE"
        Private Const PASS_WORD_FIELD As String = "PASS_WORD"
        Private Const LAST_LOGIN_FIELD As String = "LAST_LOGIN"
        Private Const FECHA_CREADO_FIELD As String = "FECHA_CREADO"

        Private Const ListaCommand As String = "PRODODS.Usuario_Lista"
        Private Const SelectSqlCommand As String = "VS_User_Select"
        Private Const DeleteSqlCommand As String = "Usuario_Delete"
        Private Const InsertSqlCommand As String = "PRODODS.Usuario_Insert"
        Private Const UpdateSqlCommand As String = "PRODODS.Usuario_Update"
        Private Const ExisteSqlCommand As String = "PRODODS.Usuario_Existe"
        Private Const DependenciasSqlCommand As String = "Usuario_Dep"
        Dim productsAdapter As New OracleDataAdapter()
        Dim productsCmd As New OracleCommand()

        ' Command object for updating data

        Dim updateProductsCmd As New OracleCommand()

        ' In-memory cache of data

        Dim productsDataset As New DataSet("ProductsDataset")


#End Region
#Region "DML"

        Public Function SelectDataUsuarios() As UsuariosData

            Dim dsUsuario As New UsuariosData
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim cmdUsuario As New OracleCommand
            Dim adUsuario As OracleDataAdapter = New OracleDataAdapter(cmdUsuario)
            With cmdUsuario
                .Connection = conn
                .CommandText = "PRODODS.VS_USER_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("INUSUARIO", OracleDbType.NVarchar2)
                p1.Value = InfoCache.UId
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)

                Try
                    adUsuario.Fill(dsUsuario, "DIC_USUARIOS")
                Catch ex As Exception

                End Try

                .Dispose()
            End With

            conn.Close()

            Return dsUsuario


        End Function
        Public Function SelectData1() As UsuariosData

            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()

            Dim productsAdapter As OracleDataAdapter = New OracleDataAdapter()
            productsAdapter.SelectCommand = New OracleCommand("SELECT NOMBRE FROM PRODODS.DIC_USUARIOS", conn)
            Dim productsDataSet As DataSet = New DataSet("usuarioDataSet")
            productsAdapter.Fill(productsDataSet, "Products")

            Return productsDataSet

        End Function

#End Region


    End Class
End Namespace


