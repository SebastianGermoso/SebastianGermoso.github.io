using Prueba3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prueba3.Logica
{
    public class CarritoLogica
    {
        private static CarritoLogica _instancia = null;

        public CarritoLogica()
        {

        }

        public static CarritoLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CarritoLogica();
                }

                return _instancia;
            }
        }

        public int Registrar(Carrito oCarrito)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {

                    oConexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarCarrito", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CorreoCLiente", oCarrito.CorreoCliente);
                    cmd.Parameters.AddWithValue("@IDProducto", oCarrito.Idproducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                    oConexion.Close();

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    Console.WriteLine(ex.Message);
                }
            }
            return respuesta;

        }


        public int Cantidad(string usuario)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select count(*) from carrito where CorreoCliente = @CorreoCliente", oConexion);
                    cmd.Parameters.AddWithValue("@CorreoCliente", usuario);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }

        public List<Carrito> Obtener(string _idusuario)
        {
            List<Carrito> lst = new List<Carrito>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerCarrito", oConexion);
                    cmd.Parameters.AddWithValue("CorreoCliente", _idusuario);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new Carrito() {

                                Idcarrito = Convert.ToInt32(dr["IDCarrito"].ToString()),
                                IdproductoNavigation = new Producto() {
                                    Idproducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                                    Nombre = dr["Nombre"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"].ToString(), new CultureInfo("es-PE")),
                                    Imagen = dr["Imagen"].ToString()
                                },
                                
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lst = new List<Carrito>();
                    Console.WriteLine(ex.Message);
                }
            }
            return lst;
        }

        public bool SUMRES (int funcion,int idProducto)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SUM_RES_PRO", oConexion);
                    cmd.Parameters.AddWithValue("@Funcion", funcion);
                    cmd.Parameters.AddWithValue("@IDProducto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return true;
        }

        public bool Eliminar(string IdCarrito, string IdProducto)
        {

            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from Carrito where CorreoCLiente = @idcarrito and IDProducto = @idproducto");
                    query.AppendLine("update Productos set Stock = Stock + 1 where IDProducto = @idproducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@idcarrito", IdCarrito);
                    cmd.Parameters.AddWithValue("@idproducto", IdProducto);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ventas(string usuario,string nombreTitular, string ubicacion, decimal totalUSD, List<Carrito> productos)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Insert into Ventas(CorreoCliente,Total,FechaVenta,Ubicacion,Tipo,IdTransaccion,Referente,estado) values (@CC,@total,GETDATE(),@UBI,'Online','nada',@REFE,'Pago')", oConexion);
                    cmd.Parameters.AddWithValue("@CC", usuario);
                    cmd.Parameters.AddWithValue("@UBI", ubicacion);
                    cmd.Parameters.AddWithValue("@REFE", nombreTitular);
                    cmd.Parameters.AddWithValue("@total", totalUSD);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    oConexion.Close();


                    foreach(var producto in productos)
                    {
                        SqlCommand jo = new SqlCommand("Insert into DetalleVentas(IDVenta,IDProducto,PrecioUnitario,Cantidad) values ((select max(IDVenta) from Ventas where CorreoCliente = @CC),@idproducto,(select Precio * @cantidad from Productos where IDProducto = @idproducto),@cantidad)", oConexion);
                        jo.Parameters.AddWithValue("@CC", usuario);
                        jo.Parameters.AddWithValue("@idproducto", producto.Idproducto);
                        jo.Parameters.AddWithValue("@cantidad", producto.Cantidad);

                        oConexion.Open();
                        jo.ExecuteNonQuery();
                        oConexion.Close();
                    }

                    SqlCommand borra = new SqlCommand("Delete from carrito where CorreoCliente = @CC", oConexion);
                    borra.Parameters.AddWithValue("@CC", usuario);
                    oConexion.Open();
                    borra.ExecuteNonQuery();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return respuesta;
        }

        //public List<Compra> ObtenerCompra(int IdUsuario)
        //{
        //    List<Compra> rptDetalleCompra = new List<Compra>();
        //    using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_ObtenerCompra", oConexion);
        //        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
        //        cmd.CommandType = CommandType.StoredProcedure;


        //        try
        //        {
        //            oConexion.Open();
        //            using (XmlReader dr = cmd.ExecuteXmlReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    XDocument doc = XDocument.Load(dr);
        //                    if (doc.Element("DATA") != null)
        //                    {
        //                        rptDetalleCompra = (from c in doc.Element("DATA").Elements("COMPRA")
        //                                            select new Compra()
        //                                            {
        //                                                Total = Convert.ToDecimal(c.Element("Total").Value,new CultureInfo("es-PE")),
        //                                                FechaTexto = c.Element("Fecha").Value,
        //                                                oDetalleCompra = (from d in c.Element("DETALLE_PRODUCTO").Elements("PRODUCTO")
        //                                                                  select new DetalleCompra() {
        //                                                                      oProducto = new Producto() {
        //                                                                          oMarca = new Marca() { Descripcion = d.Element("Descripcion").Value  },
        //                                                                          Nombre = d.Element("Nombre").Value,
        //                                                                          RutaImagen = d.Element("RutaImagen").Value
        //                                                                      },
        //                                                                      Total = Convert.ToDecimal(d.Element("Total").Value,new CultureInfo("es-PE")),
        //                                                                      Cantidad = Convert.ToInt32(d.Element("Cantidad").Value)
        //                                                                  }).ToList()
        //                                            }).ToList();
        //                    }
        //                    else
        //                    {
        //                        rptDetalleCompra = new List<Compra>();
        //                    }
        //                }

        //                dr.Close();

        //            }

        //            return rptDetalleCompra;
        //        }
        //        catch (Exception ex)
        //        {
        //            rptDetalleCompra = null;
        //            return rptDetalleCompra;
        //        }
        //    }
        //}



    }
}