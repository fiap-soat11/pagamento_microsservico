using Adapters.Gateways.Interfaces;
using Domain;

namespace Adapters.Gateways
{
    public class StatusGateway : IStatusGateway
    {
        private readonly IDataSource _statusDataSource;

        public StatusGateway(IDataSource statusDataSource)
        {
            _statusDataSource = statusDataSource;
        }

        public IEnumerable<StatusPagamento> ListarTodos()
        {
            return _statusDataSource.ListarStatusPagamento();
        }

        public StatusPagamento? BuscarPorId(int id)
        {
            return _statusDataSource.BuscarStatusPagamentoPorId(id);
        }

        public void Inserir(StatusPagamento status)
        {
            _statusDataSource.InserirStatusPagamento(status);
        }

        public void Atualizar(StatusPagamento status)
        {
            _statusDataSource.AtualizarStatusPagamento(status);
        }

        public void Excluir(int id)
        {
            _statusDataSource.ExcluirStatusPagamento(id);
        }
    }
}
