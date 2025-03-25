using System;
using System.Collections.Generic;

namespace Prueba3.Models;

public partial class DetallesPedido
{
    public int IdDetallePedido { get; set; }

    public int IdPedido { get; set; }

    public int IdMenu { get; set; }

    public int Cantidad { get; set; }

    public virtual Menu IdMenuNavigation { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
