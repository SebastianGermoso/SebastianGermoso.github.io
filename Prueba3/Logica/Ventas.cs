﻿using Microsoft.Data.SqlClient;
using Prueba3.Models;
using System.Data;
using System.Globalization;

namespace Prueba3.Logica
{
    public class Ventas
    {
    //    public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
    //    {
    //        bool respuesta = false;
    //        Mensaje = string.Empty;
    //        try
    //        {
    //            using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
    //            {
    //                SqlCommand cmd = new SqlCommand("usp_RegistrarVenta", oconexion);
    //                cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
    //                cmd.Parameters.AddWithValue("TotalProducto", obj.TotalProducto);
    //                cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
    //                cmd.Parameters.AddWithValue("Contacto", obj.Contacto);
    //                cmd.Parameters.AddWithValue("IdProvincia", obj.IdProvincia);
    //                cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
    //                cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
    //                cmd.Parameters.AddWithValue("IdTransaccion", obj.IdTransaccion);
    //                cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
    //                cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
    //                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
    //                cmd.CommandType = CommandType.StoredProcedure;

    //                oconexion.Open();

    //                cmd.ExecuteNonQuery();

    //                respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
    //                Mensaje = cmd.Parameters["Mensaje"].Value.ToString();




    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            respuesta = false;
    //            Mensaje = ex.Message;
    //        }
    //        return respuesta;
    //    }


    //    public List<DetalleVenta> ListarCompras(int idcliente)
    //    {

    //        List<DetalleVenta> lista = new List<DetalleVenta>();

    //        try
    //        {
    //            using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
    //            {

    //                string query = "select * from fn_ListarCompra(@idcliente)";

    //                SqlCommand cmd = new SqlCommand(query, oconexion);
    //                cmd.Parameters.AddWithValue("@idcliente", idcliente);
    //                cmd.CommandType = CommandType.Text;

    //                oconexion.Open();

    //                using (SqlDataReader dr = cmd.ExecuteReader())
    //                {
    //                    while (dr.Read())
    //                    {
    //                        lista.Add(new DetalleVenta()
    //                        {
    //                            oProducto = new Producto()
    //                            {
    //                                Nombre = dr["Nombre"].ToString(),
    //                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-DO")),
    //                                RutaImagen = dr["RutaImagen"].ToString(),
    //                                NombreImagen = dr["NombreImagen"].ToString(),
    //                            },

    //                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
    //                            Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-DO")),
    //                            IdTransaccion = dr["IdTransaccion"].ToString()
    //                        });
    //                    }
    //                }
    //            }
    //        }
    //        catch
    //        {
    //            lista = new List<DetalleVenta>();

    //        }
    //        return lista;
    //    }


    }
}
