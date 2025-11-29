using Domain;

namespace Adapters.Gateways.Interfaces
{
    public interface IStatusGateway
    {
        IEnumerable<StatusPagamento> ListarTodos();
        StatusPagamento? BuscarPorId(int id);
        void Inserir(StatusPagamento status);
        void Atualizar(StatusPagamento status);
        void Excluir(int id);
    }
}