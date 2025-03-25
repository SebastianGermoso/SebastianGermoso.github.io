using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Venta
{
    public int Idventa { get; set; }

    public string CorreoCliente { get; set; } = null!;

    public int? IdMesa { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaVenta { get; set; }

    public string? Ubicacion { get; set; }

    public string? Tipo { get; set; }

    public string? IdTransaccion { get; set; }

    public string? Referente { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Mesa? IdMesaNavigation { get; set; }
}
