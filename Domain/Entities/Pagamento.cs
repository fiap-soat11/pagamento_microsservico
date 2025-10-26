using System;
using System.Collections.Generic;

namespace Domain;

public partial class Pagamento
{
    public int IdPagamento { get; set; }

    public int IdPedido { get; set; }

    public DateTime? DataPagamento { get; set; }

    public int? IdFormaPagamento { get; set; }

    public decimal? ValorPago { get; set; }

    public int? IdStatusPagamento { get; set; }
    public int? Tentativa { get; set; }

    public virtual FormaPagamento? IdFormaPagamentoNavigation { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual StatusPagamento? IdStatusPagamentoNavigation { get; set; }
}
