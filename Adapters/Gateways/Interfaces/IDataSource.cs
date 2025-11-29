using Domain;

namespace Adapters.Gateways.Interfaces
{
    public interface IDataSource
    {
        // Forma de Pagamento
        IEnumerable<FormaPagamento> ListarFormasPagamento();
        FormaPagamento? BuscarFormaPagamentoPorId(int id);
        void InserirFormaPagamento(FormaPagamento forma);
        void AtualizarFormaPagamento(FormaPagamento forma);
        void ExcluirFormaPagamento(int id);

        // Status de Pagamento
        IEnumerable<StatusPagamento> ListarStatusPagamento();
        StatusPagamento? BuscarStatusPagamentoPorId(int id);
        void InserirStatusPagamento(StatusPagamento status);
        void AtualizarStatusPagamento(StatusPagamento status);
        void ExcluirStatusPagamento(int id);

        // Pagamento
        IEnumerable<Pagamento> ListarPagamentos();
        IEnumerable<Pagamento> ListarPagamentosComRelacionamentos();
        Pagamento? BuscarPagamentoPorId(int id);
        Pagamento? BuscarPagamentoPorPedido(int idPedido);
        void InserirPagamento(Pagamento pagamento);
        void AtualizarPagamento(Pagamento pagamento);
        void ExcluirPagamento(int id);

    }

} 
