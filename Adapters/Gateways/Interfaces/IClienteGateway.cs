using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Gateways.Interfaces
{
    public interface IClienteGateway
    {
        Task IncluirCliente(Cliente cliente);
        Task AtualizarCliente(Cliente cliente);
        Task ExcluirCliente(Cliente cliente);
        Task<Cliente> BuscarClientePorCPF(string cpf);
        Task<IEnumerable<Cliente>> ListarTodos();
        Task<Cliente> BuscarClientePorEmail(string email);
    }
}
