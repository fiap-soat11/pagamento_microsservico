using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Adapters.Presenters.Cliente;
using Application.Configurations;
using Application.UseCases;
using Microsoft.Extensions.Logging;
using WebAPI.Mappers;

namespace Adapters.Controllers
{
    public class ClienteController : IClienteController
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteUseCase _clienteUseCase;
        private readonly IClienteGateway _clienteGateway;

        public ClienteController(ILogger<ClienteController> logger,
                                 IClienteUseCase clienteUseCase,
                                 IClienteGateway clienteGateway)
        {
            _logger = logger;
            _clienteUseCase = clienteUseCase;
            _clienteGateway = clienteGateway;
        }

        public async Task<List<ClienteResponse>?> ListarClientes()
        {
            var listaClientes = new List<ClienteResponse>();
            var clientes = await _clienteGateway.ListarTodos();
            if (clientes == null || !clientes.Any())
                return listaClientes;                       
            listaClientes = clientes.Where(cliente => cliente.Ativo)
                                    .Select(cliente => ClienteMapper.ToDTO(cliente))
                                    .ToList();
            return listaClientes;
        }

        public async Task<ClienteResponse?> BuscarCliente(string cpf)
        {
            try
            {
                var cliente = await _clienteGateway.BuscarClientePorCPF(cpf);
                if (cliente == null || !cliente.Ativo)
                    return null;                
                var clienteResponse = ClienteMapper.ToDTO(cliente);
                return clienteResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar cliente");
                throw;
            }

        }

        public async Task IncluirCliente(ClienteRequest clienteRequest)
        {
            try
            {
                var clienteExistente = await _clienteGateway.BuscarClientePorCPF(clienteRequest.Cpf);
                if (clienteExistente != null && clienteExistente.Ativo)
                    throw new BusinessException("Cliente já cadastrado com o CPF informado");

                var cliente = ClienteMapper.ToEntity(clienteRequest);
                if(clienteExistente is null)
                {
                    var novoCliente = await _clienteUseCase.IncluirCliente(cliente);
                    await _clienteGateway.IncluirCliente(novoCliente);
                }
                else
                {
                    cliente.Ativo = true; 
                    var clienteAtualizado = await _clienteUseCase.AtualizarCliente(clienteExistente, cliente);
                    await _clienteGateway.AtualizarCliente(clienteAtualizado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir cliente");
                throw;
            }
        }

        public async Task AtualizarCliente(ClienteRequest clienteRequest)
        {
            try
            {
                var cliente = await _clienteGateway.BuscarClientePorCPF(clienteRequest.Cpf);
                if (cliente == null)
                    throw new BusinessException("Cliente não encontrado");
                var novoCliente = ClienteMapper.ToEntity(clienteRequest);
                novoCliente = await _clienteUseCase.AtualizarCliente(cliente, novoCliente);
                await _clienteGateway.AtualizarCliente(novoCliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar cliente");
                throw;
                
            }

        }

        public async Task ExcluirCliente(string cpf)
        {
            try
            {
                var cliente = await _clienteGateway.BuscarClientePorCPF(cpf);
                if (cliente == null)
                    throw new BusinessException("Cliente não encontrado");
                var clienteExcluido = await _clienteUseCase.ExcluirCliente(cliente);
                await _clienteGateway.ExcluirCliente(clienteExcluido);
            }            
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir cliente");
                throw;
            }

        }

    }
}