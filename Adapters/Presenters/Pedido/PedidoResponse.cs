using Domain;

namespace Adapters.Presenters.Pedido
{
    public class PedidoResponse
    {
        public int IdPedido { get; set; }

        public string? Cpf { get; set; }

        public int? IdStatusAtual { get; set; }

        public decimal? ValorTotal { get; set; }

        public DateOnly? DataPedido { get; set; }

        //public virtual Cliente? CpfNavigation { get; set; } = null;

        public virtual Status? IdStatusAtualNavigation { get; set; }

        public virtual ICollection<PedidoProdutoResponse> Produtos { get; set; } = new List<PedidoProdutoResponse>();

        public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();

        public virtual ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();

        public virtual ICollection<Preparo> Preparos { get; set; } = new List<Preparo>();
        public string? QRCode { get; set; }
    }
}
