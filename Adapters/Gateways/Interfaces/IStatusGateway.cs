using Domain;

namespace Adapters.Gateways.Interfaces
{
    public interface IStatusGateway
    {      
        Task<IEnumerable<Status>> ListarTodosStatus();
        Task<Status> BuscarStatusPorNome(string nomeStatus);
        Task<Status> BuscarStatusPorId(int idStatus);
    }
}