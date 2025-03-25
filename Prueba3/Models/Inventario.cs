using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public string NombreProducto { get; set; } = null!;

    public short Cantidad { get; set; }

    public string UnidadMedida { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public short? PuntoReorden { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public decimal Costo { get; set; }

    public bool? Activo { get; set; }
}
