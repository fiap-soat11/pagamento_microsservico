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
        public async Task<IEnumerable<Status>> ListarTodosStatus()
        {
            return await _statusDataSource.ListarTodosStatus();
        }

        public async Task<Status> BuscarStatusPorNome(string nomeStatus)
        {
            return await _statusDataSource.BuscarStatusPorNome(nomeStatus);
        }

        public async Task<Status> BuscarStatusPorId(int idStatus)
        {
            return await _statusDataSource.BuscarStatusPorId(idStatus);
        }
    }
}
