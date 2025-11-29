using Application.Configurations;
using Domain;
using System.Linq;

namespace DataSource
{
    public class DataSource : Adapters.Gateways.Interfaces.IDataSource
    {
        private readonly Repositories.Interfaces.IFormaPagamentoRepository _formaPagamentoRepo;
        private readonly Repositories.Interfaces.IPagamentoRepository _pagamentoRepo;
        private readonly Repositories.Interfaces.IStatusPagamentoRepository _statusPagamentoRepo;

        public DataSource(
            Repositories.Interfaces.IFormaPagamentoRepository formaPagamentoRepo,
            Repositories.Interfaces.IPagamentoRepository pagamentoRepo,
            Repositories.Interfaces.IStatusPagamentoRepository statusPagamentoRepo)
        {
            _formaPagamentoRepo = formaPagamentoRepo;
            _pagamentoRepo = pagamentoRepo;
            _statusPagamentoRepo = statusPagamentoRepo;
        }

        // Forma de Pagamento
        public IEnumerable<FormaPagamento> ListarFormasPagamento() => _formaPagamentoRepo.ListarTodos();
        public FormaPagamento? BuscarFormaPagamentoPorId(int id) => _formaPagamentoRepo.BuscarPorId(id);
        public void InserirFormaPagamento(FormaPagamento forma) => _formaPagamentoRepo.Inserir(forma);
        public void AtualizarFormaPagamento(FormaPagamento forma) => _formaPagamentoRepo.Atualizar(forma);
        public void ExcluirFormaPagamento(int id) => _formaPagamentoRepo.Excluir(id);

        // Status de Pagamento
        public IEnumerable<StatusPagamento> ListarStatusPagamento() => _statusPagamentoRepo.ListarTodos();
        public StatusPagamento? BuscarStatusPagamentoPorId(int id) => _statusPagamentoRepo.BuscarPorId(id);
        public void InserirStatusPagamento(StatusPagamento status) => _statusPagamentoRepo.Inserir(status);
        public void AtualizarStatusPagamento(StatusPagamento status) => _statusPagamentoRepo.Atualizar(status);
        public void ExcluirStatusPagamento(int id) => _statusPagamentoRepo.Excluir(id);

        // Pagamento
        public IEnumerable<Pagamento> ListarPagamentos() => _pagamentoRepo.ListarTodos();
        public IEnumerable<Pagamento> ListarPagamentosComRelacionamentos()
        {
            if (_pagamentoRepo is Repositories.PagamentoRepository pr)
            {
                return pr.ListarComRelacionamentos();
            }
            return _pagamentoRepo.ListarTodos();
        }
        public Pagamento? BuscarPagamentoPorId(int id) => _pagamentoRepo.BuscarPorId(id);
        public Pagamento? BuscarPagamentoPorPedido(int idPedido)
        {
            var result = _pagamentoRepo.Buscar(p => p.IdPedido == idPedido).FirstOrDefault();
            return result;
        }
        public void InserirPagamento(Pagamento pagamento) => _pagamentoRepo.Inserir(pagamento);
        public void AtualizarPagamento(Pagamento pagamento) => _pagamentoRepo.Atualizar(pagamento);
        public void ExcluirPagamento(int id) => _pagamentoRepo.Excluir(id);
    }
}