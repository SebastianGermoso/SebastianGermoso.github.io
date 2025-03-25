using Prueba3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Prueba3.Logica;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Syncfusion.EJ2.Layouts;
using System.Data;
using System.Globalization;
using Prueba3.Paypal;
using Prueba3;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.DataProtection;
using System.Text;

namespace Prueba3.Controllers
{
    [IgnoreAntiforgeryToken]
    public class TiendaController : Controller
    {

        private readonly TumaeContext _context;
        public string UrlPaypal { get; } = "";
        public string ClientId { get; } = "";
        public string Secret { get;} = "";


        public static decimal pago = 20.00m;
        public static string NT = "";
        public static string UBI = "";
        public static List<Carrito> opro;
        [HttpPost]
        public IActionResult ProcesarPago(string nombreTitular, string ubicacion, decimal totalUSD, List<Carrito> productos)
        {
            try
            {
                // Aquí puedes procesar los datos recibidos, como guardarlos en la base de datos, realizar pagos, etc.
                // Ejemplo de procesamiento:
                // Guardar los datos en la base de datos, enviar un correo electrónico de confirmación, etc.

                // Por simplicidad, este controlador solo devuelve un mensaje de éxito
                Console.WriteLine(nombreTitular);
                Console.WriteLine(totalUSD);
                Console.WriteLine(ubicacion);
                foreach (var producto in productos)
                {
                    Console.WriteLine($"Producto: {producto.Idproducto}, Cantidad: {producto.Cantidad}");
                }
                string tu = totalUSD.ToString("0.00");
                Console.WriteLine(tu);
                pago = decimal.Parse(tu);        
                Console.WriteLine(pago);
                NT = nombreTitular;
                UBI = ubicacion;
                opro = productos;
                return Ok("Pago procesado exitosamente.");
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que pueda ocurrir durante el procesamiento
                Console.WriteLine("Error al procesar el pago: " + ex.Message);
                return StatusCode(500, "Ocurrió un error al procesar el pago. Por favor, inténtalo de nuevo más tarde.");
            }
        }

        public JsonResult ventainsertar()
        {
            //Console.WriteLine(NT);
            //Console.WriteLine(pago);
            //Console.WriteLine(UBI);
            //foreach (var producto in opro)
            //{
            //    Console.WriteLine($"Producto: {producto.Idproducto}, Cantidad: {producto.Cantidad}");
            //}
            CarritoLogica.Instancia.ventas(User.Identity.Name, NT, UBI, pago, opro);


            return new JsonResult("");
        }


        
        public JsonResult OnPostCreateOrder()
        {
            Console.WriteLine(pago);

            JsonObject createOrderRequest = new JsonObject();
            createOrderRequest.Add("intent", "CAPTURE");

            JsonObject amount = new JsonObject();
            amount.Add("currency_code", "USD");
            amount.Add("value", pago);

            JsonObject purchaseUnit1 = new JsonObject();
            purchaseUnit1.Add("amount", amount);

            JsonArray purchaseUnit = new JsonArray();
            purchaseUnit.Add(purchaseUnit1);

            createOrderRequest.Add("purchase_units", purchaseUnit);

            string accessToken = GetPaypalAccesToken();

            string url = "https://sandbox.paypal.com/v2/checkout/orders";

            string orderId = "";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(createOrderRequest.ToString(), null, "application/json");

                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResponse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strResponse);
                    Console.WriteLine(jsonResponse);
                    if (jsonResponse != null)
                    {
                        orderId = jsonResponse["id"]?.ToString() ?? "";
                    }
                }
            }

            var response = new
            {
                id = orderId
            };
            
            return new JsonResult(response);
        }


        public JsonResult OnPostCompleteOrder([FromBody] JsonObject data)
        {
            if (data == null || data["orderID"] == null) return new JsonResult("");

            var orderID = data["orderID"]!.ToString();

            string accessToken = GetPaypalAccesToken();

            string url = "https://sandbox.paypal.com/v2/checkout/orders/"+orderID+"/capture";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("", null, "application/json");

                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResponse = readTask.Result;

                    var jsonResponse = JsonNode.Parse(strResponse);
                    Console.WriteLine(jsonResponse);
                    if (jsonResponse != null)
                    {
                        string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
                        if(paypalOrderStatus == "COMPLETED")
                        {
                            return new JsonResult("succes");
                        }
                    }
                }
            }


            return new JsonResult("");
        }

        public JsonResult OnPostCancelOrder([FromBody] JsonObject data)
        {
            if (data == null || data["orderID"] == null) return new JsonResult("");

            var orderID = data["orderID"]!.ToString();

            return new JsonResult("");
        }

        private string GetPaypalAccesToken()
        {
            string accessToken = "";
            string url = "https://sandbox.paypal.com/v1/oauth2/token";
            
            using(var client = new HttpClient())
            {
                string credentials64 =
                    Convert.ToBase64String(Encoding.UTF8.GetBytes("ATQtoOHb2OaiDsvJoZj4sw3Ktcn5LAMNS9kH4P1k5atZecPm4OMvuO094t80gQ1xOah7HtRtSnS8tt4_:EAgyar5Z1jZRG8W94OFR7aGOLSk-gLUEoru_qOCvrtQM7d3ccf-WcjD8dpVj_m4tWPKMLv_pdgbji7ED"));
                
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials64);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("grant_type=client_credentials",null,"application/x-www-form-urlencoded");

                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strRespoonse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strRespoonse);
                    if(jsonResponse != null)
                    {
                        accessToken = jsonResponse["access_token"]?.ToString() ?? "";
                    }
                }
            }
            return accessToken;
        }

        public TiendaController(TumaeContext context)
        {
            _context = context;
        }

        public string Usuario = "";

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdcategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Idproducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        public async Task<IActionResult> Hola()
        {
            var tumaeContext = _context.Productos
                                        .Include(p => p.IdcategoriaNavigation)
                                        .Where(p => p.Activo != null && p.Activo == true);

            return View(await tumaeContext.ToListAsync());
        }

        public async Task<IActionResult> Carrito()
        {
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("Index", "Login");
            else
            {
                List<Carrito> oLista = new List<Carrito>();
                oLista = CarritoLogica.Instancia.Obtener(User.Identity.Name);

                if (oLista.Count != 0)
                {
                    oLista = (from d in oLista
                              select new Carrito()
                              {
                                  Idcarrito = d.Idcarrito,
                                  IdproductoNavigation = new Producto()
                                  {
                                      Idproducto = d.IdproductoNavigation.Idproducto,
                                      Nombre = d.IdproductoNavigation.Nombre,
                                      Precio = d.IdproductoNavigation.Precio,
                                      Imagen = d.IdproductoNavigation.Imagen,
                                  }
                              }).ToList();
                }

                return View(oLista); 
            }
        }
      

        public JsonResult InsertarCarrito(Carrito oCarrito)
        {
            oCarrito.CorreoCliente = User.Identity.Name;
            int _respuesta = 0;
            _respuesta = CarritoLogica.Instancia.Registrar(oCarrito);
            return Json(new { respuesta = _respuesta });
        }

        [HttpGet]
        public JsonResult CantidadCarrito()
        {
            int _respuesta = 0;
            _respuesta = CarritoLogica.Instancia.Cantidad(User.Identity.Name) ;
            return Json(new { respuesta = _respuesta });
        }


        [HttpGet]
        public JsonResult ObtenerCarrito()
        {
            List<Carrito> oLista = new List<Carrito>();
            oLista = CarritoLogica.Instancia.Obtener(User.Identity.Name);

            if (oLista.Count != 0)
            {
                oLista = (from d in oLista
                          select new Carrito()
                          {
                              Idcarrito = d.Idcarrito,
                              IdproductoNavigation = new Producto()
                              {
                                  Idproducto = d.IdproductoNavigation.Idproducto,
                                  Nombre = d.IdproductoNavigation.Nombre,
                                  Precio = d.IdproductoNavigation.Precio,
                                  Imagen = d.IdproductoNavigation.Imagen,
                              }
                          }).ToList();
            }


            return Json(new { lista = oLista });
        }

        [HttpPost]
        public JsonResult EliminarCarrito( string IdProducto)
        {
            bool respuesta = false;
            respuesta = CarritoLogica.Instancia.Eliminar(User.Identity.Name, IdProducto);
            return Json(new { resultado = respuesta });
        }

        public JsonResult Sumar(int IDProducto)
        {
            bool respuesta = false;
            respuesta = CarritoLogica.Instancia.SUMRES(1,IDProducto);

            Console.WriteLine("bobo");
            return Json(new { resultado = respuesta });
        }
        public JsonResult Restar(int IDProducto)
        {
            bool respuesta = false;
            respuesta = CarritoLogica.Instancia.SUMRES(0, IDProducto);
            return Json(new { resultado = respuesta });
            
        }



        //[HttpPost]
        //public JsonResult RegistrarCompra(Compra oCompra)
        //{
        //    bool respuesta = false;

        //    oCompra.IdUsuario = oUsuario.IdUsuario;
        //    respuesta = CompraLogica.Instancia.Registrar(oCompra);
        //    return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        //}

        ////
        //[HttpGet]
        //public JsonResult ObtenerCompra()
        //{
        //    List<Compra> oLista = new List<Compra>();

        //    oLista = CarritoLogica.Instancia.ObtenerCompra(oUsuario.IdUsuario);

        //    oLista = (from c in oLista
        //              select new Compra()
        //              {
        //                  Total = c.Total,
        //                  FechaTexto = c.FechaTexto,
        //                  oDetalleCompra = (from dc in c.oDetalleCompra
        //                                    select new DetalleCompra() {
        //                                        oProducto = new Producto() {
        //                                            oMarca = new Marca() {Descripcion = dc.oProducto.oMarca.Descripcion },
        //                                            Nombre = dc.oProducto.Nombre,
        //                                            RutaImagen = dc.oProducto.RutaImagen,
        //                                            base64 = utilidades.convertirBase64(Server.MapPath(dc.oProducto.RutaImagen)),
        //                                            extension = Path.GetExtension(dc.oProducto.RutaImagen).Replace(".", ""),
        //                                        },
        //                                        Total = dc.Total,
        //                                        Cantidad = dc.Cantidad
        //                                    }).ToList()
        //              }).ToList();
        //    return Json(new { lista = oLista });
        //}
    }


}