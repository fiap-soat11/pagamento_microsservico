using Adapters.Presenters.Pedido;
using Domain;

namespace WebAPI.Mappers
{
    public class StatusMapper
    {

        public static StatusResponse CodigoNomeStatusToDTO(Status status)
        {
            return new StatusResponse
            {
                IdStatus = status.IdStatus,
                Descricao = status.Descricao,
                Nome = status.Nome
            };
        }
    }
}
