Imports System.Data.SqlClient

Public Class mantenimientos
    Inherits conexiones
    Event MantenimientosError(descripcion As String)

    Dim query As String

    Public Function fBuscarDatosPoliza(idPoliza As Integer)

        query = "SELECT Producto.Nombre as NombreProducto, TipoProducto.Nombre as NombreTipo, Poliza.Estado as EstadoPoliza
	                FROM Poliza
		                INNER JOIN Producto
			                ON Poliza.IdProducto = Producto.IdProducto
		                INNER JOIN TipoProducto
			                ON Poliza.IdTipoProducto = TipoProducto.IdTipoProducto
	                WHERE Poliza.IdPoliza = " & idPoliza

        Try
            fConectar()
            Dim cmd As New SqlCommand(query, conn)
            cmd.CommandType = CommandType.Text
            Dim registro As SqlDataReader = cmd.ExecuteReader()
            Dim tabla As DataTable = New DataTable()
            tabla.Load(registro)
            Return tabla
        Catch ex As Exception
            RaiseEvent MantenimientosError(ex.Message)
            Return Nothing
        Finally
            fDesconectar()
        End Try

    End Function

    Public Function fBuscarRegistros(idPoliza As Integer)
        query = "SELECT Asegurados.idAsegurado as IdAsegurado, Asegurados.Dni as dni, Asegurados.Nombre as NombreAsegurado, DATEDIFF(YEAR,Asegurados.FechaNacimiento,GETDATE())-(CASE
                    WHEN DATEADD(YY,DATEDIFF(YEAR,Asegurados.FechaNacimiento,GETDATE()),Asegurados.FechaNacimiento)>GETDATE() THEN
                        1
                    ELSE
                        0 
                    END) as Edad
	                FROM Poliza
		                INNER JOIN Asegurados
			                ON Poliza.IdPoliza = Asegurados.IdPoliza
	                WHERE Poliza.IdPoliza = " & idPoliza & " AND Asegurados.Estado = 'Activo'
	                ORDER BY Asegurados.IdPoliza"

        Try
            fConectar()
            Dim cmd As New SqlCommand(query, conn)
            cmd.CommandType = CommandType.Text
            Dim registro As SqlDataReader = cmd.ExecuteReader()
            Dim tabla As DataTable = New DataTable()
            tabla.Load(registro)
            Return tabla
        Catch ex As Exception
            RaiseEvent MantenimientosError(ex.Message)
            Return Nothing
        Finally
            fDesconectar()
        End Try
    End Function

    Public Function fEliminar(idPoliza As Integer)
        query = ""
    End Function
End Class
