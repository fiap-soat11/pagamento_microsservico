namespace Adapters.Presenters.Pagamento
{
    public class PagamentoResponse
    {
        public int IdPagamento { get; set; }
        public int IdPedido { get; set; }
        public decimal? ValorPago { get; set; }
        public DateTime? DataPagamento { get; set; }
        public int? IdFormaPagamento { get; set; }
        public int? IdStatusPagamento { get; set; }
        public int? Tentativa { get; set; }
    }
}
