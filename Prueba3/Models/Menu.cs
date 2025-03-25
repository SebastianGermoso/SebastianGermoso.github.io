using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string NombrePlato { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public int IdCategoria { get; set; }

    public string Estatus { get; set; } = null!;

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
