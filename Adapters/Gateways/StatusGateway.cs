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
    }
}
