using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class AudDetallesPedido
{
    public int? IdDetallePedido { get; set; }

    public int? IdPedido { get; set; }

    public int? IdMenu { get; set; }

    public int? Cantidad { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
