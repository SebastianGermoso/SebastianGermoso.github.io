using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class ProductosAudit
{
    public int IdAudit { get; set; }

    public int? Idproducto { get; set; }

    public int? Idcategoria { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public string? Imagen { get; set; }

    public bool? Activo { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
