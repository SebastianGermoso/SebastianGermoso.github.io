using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class DetalleVenta
{
    public int IddetalleVenta { get; set; }

    public int? Idventa { get; set; }

    public int? Idproducto { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public int? Cantidad { get; set; }

    public virtual Producto? IdproductoNavigation { get; set; }

    public virtual Venta? IdventaNavigation { get; set; }
}
