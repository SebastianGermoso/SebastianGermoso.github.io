using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Usario
{
    public int IdUsuario { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Estatus { get; set; } = null!;

    public string Correo { get; set; } = null!;
}
