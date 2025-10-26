using Adapters.Presenters.Cliente;

namespace Adapters.Controllers.Interfaces
{
    public interface IClienteController
    {
        Task<List<ClienteResponse>?> ListarClientes();

        Task<ClienteResponse?> BuscarCliente(string cpf);

        Task IncluirCliente(ClienteRequest cliente);

        Task AtualizarCliente(ClienteRequest cliente);

        Task ExcluirCliente(string cpf);
    }
}
