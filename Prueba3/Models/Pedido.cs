using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int IdCliente { get; set; }

    public int? IdMesa { get; set; }

    public DateTime FechaHora { get; set; }

    public string TipoPedido { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Mesa? IdMesaNavigation { get; set; }
}
