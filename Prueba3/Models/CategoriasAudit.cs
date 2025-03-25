using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class CategoriasAudit
{
    public int IdAudit { get; set; }

    public int? Idcategoria { get; set; }

    public string? Nombre { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
