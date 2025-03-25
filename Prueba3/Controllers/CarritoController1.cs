using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba3.Models;

namespace Prueba3.Controllers
{
    public class CarritoController1 : Controller
    {
        private readonly TumaeContext _context;

        public CarritoController1(TumaeContext context)
        {
            _context = context;
        }

        // Método para agregar un producto al carrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarAlCarrito(int idProducto, Carrito carrito)
        {
            // Obtener el correo del cliente actual (asumiendo que se está utilizando la autenticación de ASP.NET Identity)
            string correoCliente = User.Identity.Name;

            // Crear un nuevo objeto Carrito con los datos necesarios

            carrito.Idproducto = idProducto; // Asigna el ID del producto recibido como parámetro
                carrito.CorreoCliente = correoCliente; // Asigna el correo del cliente actual
                                              // Puedes establecer los valores de Cantidad y Total según sea necesario
            

                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirecciona a donde sea apropiado
           
        }
    


    // GET: CarritoController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: CarritoController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarritoController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarritoController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarritoController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarritoController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
