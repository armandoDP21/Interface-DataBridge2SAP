Imports Bis.Sap.Common

Imports System.Data.Common
Imports System.Text

'Option Strict On
'Option Explicit On


Namespace Bis.Sap.DataAccess
    Public Class ISapAdminDatos
#Region "DML"
        Public Function ListaSelect() As DataTable
            Dim UsuariosTable As New DataTable
            Dim ConnectionText As New StringBuilder
            ConnectionText.Append("user id=prodods;password=managerjr;data source=")
            ConnectionText.Append("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)")
            ConnectionText.Append("(HOST=30.9.131.124)(PORT=1521))(CONNECT_DATA=")
            ConnectionText.Append("(SERVICE_NAME=ODSCA)))")
            Dim conn As New OracleConnection(ConnectionText.ToString)
            conn.Open()


            Dim cmdGetUsuarios As New OracleCommand
            Dim adUsuario As OracleDataAdapter = New OracleDataAdapter(cmdGetUsuarios)
            With cmdGetUsuarios
                .Connection = conn
                .CommandText = "PRODODS.VS_USER_SELECT"
                .CommandType = CommandType.StoredProcedure

                Dim p1 As OracleParameter = New OracleParameter("INUSUARIO", OracleDbType.NVarchar2)
                p1.Value = InfoCache.UId
                .Parameters.Add(p1)

                Dim p2 As OracleParameter = New OracleParameter("p2", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                .Parameters.Add(p2)

                Try
                    'adUsuario.Fill(UsuariosTable, "DIC_USUARIOS")
                Catch ex As Exception

                End Try

                .Dispose()
            End With

            conn.Close()

            Return UsuariosTable
        End Function
        Public Function DataSelect(ByVal Usuario_Clave As String) As UsuariosData

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


