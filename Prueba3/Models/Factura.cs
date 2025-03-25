using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int Idventa { get; set; }

    public decimal Total { get; set; }

    public decimal TotalPago { get; set; }

    public DateTime? FechaHora { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual Venta IdventaNavigation { get; set; } = null!;
}
