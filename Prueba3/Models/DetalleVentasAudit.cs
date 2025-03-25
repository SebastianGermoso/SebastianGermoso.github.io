using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class DetalleVentasAudit
{
    public int IdAudit { get; set; }

    public int? IddetalleVenta { get; set; }

    public int? Idventa { get; set; }

    public int? Idproducto { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public int? Cantidad { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
