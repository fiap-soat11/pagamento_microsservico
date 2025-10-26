using System.Collections.Generic;
using System.Threading.Tasks;
using Adapters.Gateways.Interfaces;
using Domain;

namespace Adapters.Gateways
{
    public class ClienteGateway : IClienteGateway
    {
        private readonly IDataSource _clienteDataSource;

        public ClienteGateway(IDataSource clienteDataSource)
        {
            _clienteDataSource = clienteDataSource;
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            await _clienteDataSource.AtualizarCliente(cliente);
        }

        public async Task<Cliente> BuscarClientePorCPF(string cpf)
        {
            return await _clienteDataSource.BuscarClientePorCPF(cpf);
        }

        public async Task<Cliente> BuscarClientePorEmail(string email)
        {
            return await _clienteDataSource.BuscarClientePorEmail(email);
        }

        public async Task ExcluirCliente(Cliente cliente)
        {
            await _clienteDataSource.ExcluirCliente(cliente);
        }

        public async Task IncluirCliente(Cliente cliente)
        {
            await _clienteDataSource.IncluirCliente(cliente);
        }

        public async Task<IEnumerable<Cliente>> ListarTodos()
        {
            return await _clienteDataSource.ListarTodos();
        }
    }
}