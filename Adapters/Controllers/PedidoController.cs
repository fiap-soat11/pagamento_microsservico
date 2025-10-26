using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Application.Configurations;
using Domain;
using Microsoft.Extensions.Logging;

namespace Adapters.Controllers
{
    public class PedidoController : IPedidoController
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IProdutoGateway _produtoGateway;
        private readonly IClienteGateway _clienteGateway;

        public PedidoController(ILogger<PedidoController> logger, IPedidoGateway pedidoGateway, IProdutoGateway produtoGateway, IClienteGateway clienteGateway)
        {
            _logger = logger;
            _pedidoGateway = pedidoGateway;
            _produtoGateway = produtoGateway;
            _clienteGateway = clienteGateway;
        }

        public async Task<Pedido> AdicionarProduto(int idPedido, int idProduto, int quantidade, string? observacao)
        {
            try
            {
                var pedido = await _pedidoGateway.BuscarPedidoPorId(idPedido);
                if (pedido == null)
                    throw new BusinessException("Pedido não encontrado.");

                var produto = await _produtoGateway.BuscarProdutoPorId(idProduto);
                if (produto == null)
                    throw new BusinessException("Produto não encontrado.");

                await _pedidoGateway.AdicionarProduto(idPedido, idProduto, quantidade, observacao);
                return await _pedidoGateway.BuscarPedidoPorId(idPedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar produto ao pedido");
                throw;
            }
        }

        public async Task<Pedido> AtualizarProduto(int idPedido, int idPedidoProduto, int novaQuantidade, string? observacao)
        {
            try
            {
                var pedido = await _pedidoGateway.BuscarPedidoPorId(idPedido);
                if (pedido == null)
                    throw new BusinessException("Pedido não encontrado.");

                await _pedidoGateway.AtualizarProduto(idPedido, idPedidoProduto, novaQuantidade, observacao);
                return await _pedidoGateway.BuscarPedidoPorId(idPedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar produto do pedido");
                throw;
            }
        }

        public async Task<Pedido> IniciarPedido(string? cpf)
        {
            try
            {
                Cliente? cliente = null;

                if (!string.IsNullOrWhiteSpace(cpf))
                {
                    cliente = await _clienteGateway.BuscarClientePorCPF(cpf);

                    if (cliente == null)
                        throw new BusinessException("Cliente não encontrado.");
                }

                var pedido = await _pedidoGateway.IniciarPedido(cliente?.Cpf);

                return pedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao iniciar pedido");
                throw;
            }
        }

        public List<PedidoProduto> ListarProdutosDoPedido(int idPedido)
        {
            throw new NotImplementedException();
        }       

        public async Task<Pedido> RemoverProduto(int idPedido, int idPedidoProduto)
        {
            try
            {
                var pedido = await _pedidoGateway.BuscarPedidoPorId(idPedido);
                if (pedido == null)
                    throw new BusinessException("Pedido não encontrado.");

                await _pedidoGateway.RemoverProduto(idPedido, idPedidoProduto);
                return await _pedidoGateway.BuscarPedidoPorId(idPedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover produto do pedido");
                throw;
            }
        }

        public async Task<Pedido> BuscarPedidoPorId(int idPedido)
        {
            try
            {
                var pedido = await _pedidoGateway.BuscarPedidoPorId(idPedido);
                return pedido ?? throw new BusinessException("Pedido não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao encontrar pedido");
                throw;
            }
        }

        public async Task<bool> AtualizarStatusPedido(int idStatusPedido, int idPedido)
        {
            try
            {
                var pedidoRetorno = await BuscarPedidoPorId(idPedido);
                if (pedidoRetorno == null)
                    return false;
                else
                    await _pedidoGateway.AtualizarStatusPedido(idStatusPedido, idPedido);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pedido");
                throw;
            }
        }

        public async Task<bool> CancelarPedido(int idPedido)
        {
            try
            {
                var pedidoRetorno = await BuscarPedidoPorId(idPedido);
                if (pedidoRetorno == null)
                    return false;
                else
                    await _pedidoGateway.CancelarPedido(idPedido);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cancelar pedido");
                throw;
            }
        }

        public async Task<bool> FinalizarPedido(int idPedido)
        {
            try
            {
                var pedidoRetorno = await BuscarPedidoPorId(idPedido);
                if (pedidoRetorno == null)
                    return false;
                else
                    await _pedidoGateway.FinalizarPedido(idPedido);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao finalizar pedido");
                throw;
            }
        }

        public Task<IEnumerable<Pedido>> ListarPedidoClienteStatus()
        {
            try
            {
                return _pedidoGateway.ListarPedidoClienteStatus();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar pedidos cliente");
                throw;
            }
        }

        public Task<IEnumerable<Pedido>> ListarPedidoCozinha()
        {
            try
            {
                return _pedidoGateway.ListarPedidoCozinha();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar pedidos cozinha");
                throw;
            }
        }

        public Task<IEnumerable<Pedido>> ListarPedidos()
        {
            try
            {
                return _pedidoGateway.ListarPedidos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar pedidos");
                throw;
            }
        }
    }
}
