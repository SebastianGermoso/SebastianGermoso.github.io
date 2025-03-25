using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class AudCliente
{
    public int? IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Password { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FhModi { get; set; }

    public string? Actividad { get; set; }
}
