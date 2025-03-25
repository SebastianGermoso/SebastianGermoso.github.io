using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class AudMesa
{
    public int? IdMesa { get; set; }

    public int? Capacidad { get; set; }

    public string? Estado { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
