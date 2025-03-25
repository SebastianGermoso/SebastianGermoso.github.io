using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class MesasAudit
{
    public int IdAudit { get; set; }

    public int? IdMesa { get; set; }

    public int? Capacidad { get; set; }

    public bool? Activo { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
