Imports System.Data.SqlClient

Public Class frmPrincipal
    Dim WithEvents objMantenimientos As New mantenimientos

    Private Sub cmdBuscar_Click(sender As Object, e As EventArgs) Handles cmdBuscar.Click
        Dim idPoliza As Integer

        Try
            idPoliza = Val(txtIdPoliza.Text)
        Catch ex As Exception
            MsgBox("Debe ingresar un numero")
        End Try

        Dim tablaDatos As New DataTable
        tablaDatos = objMantenimientos.fBuscarDatosPoliza(Val(txtIdPoliza.Text))

        For Each row As DataRow In tablaDatos.Rows
            lblProducto.Text = "Producto:" & CStr(row("NombreProducto"))
            lblTipoProducto.Text = "Tipo producto:" & CStr(row("NombreTipo"))
            lblEstado.Text = "Estado:" & CStr(row("EstadoPoliza"))
        Next


        Dim tablaRegistros As New DataTable
        tablaRegistros = objMantenimientos.fBuscarRegistros(Val(txtIdPoliza.Text))

        dgvDetalle.DataSource = tablaRegistros

    End Sub
End Class
