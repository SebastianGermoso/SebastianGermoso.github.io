using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public int? Idcategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? Stock { get; set; }

    public int? PuntoReorden { get; set; }

    public string? Imagen { get; set; }


    public bool? Activo { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categorium? IdcategoriaNavigation { get; set; }

}
