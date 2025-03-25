using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class AudFactura
{
    public int? IdFactura { get; set; }

    public int? IdPedido { get; set; }

    public decimal? Total { get; set; }

    public decimal? TotalPago { get; set; }

    public string? IdTransaccion { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Tipo { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
