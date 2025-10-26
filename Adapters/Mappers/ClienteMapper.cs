using Domain;
using Adapters.Presenters.Cliente;

namespace WebAPI.Mappers
{
    public class ClienteMapper
    {
        public static Cliente ToEntity(ClienteRequest dto)
        {
            return new Cliente
            {
                Cpf = dto.Cpf,
                Nome = dto.Nome,
                Email = dto.Email
            };
        }

        public static ClienteResponse ToDTO(Cliente cliente)
        {
            return new ClienteResponse
            {
                Cpf = cliente.Cpf,
                Nome = cliente.Nome,
                Email = cliente.Email

            };
        }
    }
}
