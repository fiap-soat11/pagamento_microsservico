using System;
using System.Collections.Generic;

namespace Domain;

public partial class Preparo
{
    public int IdPreparo { get; set; }

    public int IdPedido { get; set; }

    public int IdStatus { get; set; }

    public DateTime DataStatus { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;
}
