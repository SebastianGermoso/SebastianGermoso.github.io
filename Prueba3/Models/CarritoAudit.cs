using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class CarritoAudit
{
    public int IdAudit { get; set; }

    public int? Idcarrito { get; set; }

    public int? Idproducto { get; set; }

    public string? CorreoCliente { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
