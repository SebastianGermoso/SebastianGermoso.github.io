using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class InventarioAudit
{
    public int IdAudit { get; set; }

    public int? IdInventario { get; set; }

    public string? NombreProducto { get; set; }

    public short? Cantidad { get; set; }

    public string? UnidadMedida { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public short? PuntoReorden { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public decimal? Costo { get; set; }

    public bool? Activo { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
