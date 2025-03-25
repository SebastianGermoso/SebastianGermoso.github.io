using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Carrito
{
    public int Idcarrito { get; set; }

    public int? Idproducto { get; set; }

    public string? CorreoCliente { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public virtual Producto? IdproductoNavigation { get; set; }
}
