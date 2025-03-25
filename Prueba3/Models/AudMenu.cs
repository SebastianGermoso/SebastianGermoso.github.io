using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class AudMenu
{
    public int? IdMenu { get; set; }

    public string? NombrePlato { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? IdCategoria { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
