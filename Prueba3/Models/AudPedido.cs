using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class AudPedido
{
    public int? IdPedido { get; set; }

    public int? IdCliente { get; set; }

    public int? IdMesa { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? TipoPedido { get; set; }

    public string? Estado { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
