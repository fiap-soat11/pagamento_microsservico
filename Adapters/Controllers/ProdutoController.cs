using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Adapters.Presenters.Produto;
using Application.Configurations;
using Domain;
using Microsoft.Extensions.Logging;
using WebAPI.Mappers;

namespace Adapters.Controllers
{
    public class ProdutoController : IProdutoController
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoGateway _produtoGateway;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoGateway produtoGateway)
        {
            _logger = logger;
            _produtoGateway = produtoGateway;
        }

        public Task<Produto> RemoverProduto(int idProduto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Produto>> BuscarProdutosPorCategoria(int idCategoria)
        {
            try
            {
                var produtos = await _produtoGateway.BuscarProdutosCategoria(idCategoria);
                return produtos ?? throw new BusinessException("Produto não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao encontrar produto");
                throw;
            }
        }

        public async Task<Produto> BuscarProdutoPorId(int idProduto)
        {
            try
            {
                var produto = await _produtoGateway.BuscarProdutoPorId(idProduto);
                return produto ?? throw new BusinessException("Produto não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao encontrar produto");
                throw;
            }
        }

        public Task<IEnumerable<Produto>> ListarTodos()
        {
            try
            {
                return _produtoGateway.ListarProdutos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar produto");
                throw;
            }
        }

        public async Task IncluirProduto(ProdutoRequest produtoRequest)
        {
            try
            {
               await _produtoGateway.IncluirProduto(ProdutoMapper.ToEntity(produtoRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar produto");
                throw;
            }
        }
        public async Task AtualizarProduto(ProdutoRequest produtoRequest)
        {
            try
            {
                await _produtoGateway.AtualizarProduto(ProdutoMapper.ToEntity(produtoRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar produto");
                throw;
            }

        }
        public async Task ExcluirProduto(int idProduto)
        {
            try
            {
                var produto = await BuscarProdutoPorId(idProduto);

                if (produto is null)
                    throw new Exception("Produto não existe");

                await _produtoGateway.ExcluirProduto(produto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar produto");
                throw;
            }

        }

    }
}
