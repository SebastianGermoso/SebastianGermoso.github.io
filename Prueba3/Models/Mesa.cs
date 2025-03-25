using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Mesa
{
    public int IdMesa { get; set; }

    public int Capacidad { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
