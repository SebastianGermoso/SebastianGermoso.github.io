using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class VentasAudit
{
    public int IdAudit { get; set; }

    public int? Idventa { get; set; }

    public string? CorreoCliente { get; set; }

    public int? IdMesa { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaVenta { get; set; }

    public string? Ubicacion { get; set; }

    public string? Estado { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
