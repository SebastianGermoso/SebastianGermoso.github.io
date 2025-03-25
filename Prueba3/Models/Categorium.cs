using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Categorium
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
