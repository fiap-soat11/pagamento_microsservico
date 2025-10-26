using Adapters.Gateways.Interfaces;
using Domain;

namespace Adapters.Gateways
{
    public class ProdutoGateway : IProdutoGateway
    {
        private readonly IDataSource _produtoDataSource;

        public ProdutoGateway(IDataSource produtoDataSource)
        {
            _produtoDataSource = produtoDataSource;
        }

        public async Task AtualizarProduto(Produto produto)
        {
            await _produtoDataSource.AtualizarProduto(produto);
        }

        public async Task<List<Produto>> BuscarProdutosCategoria(int IdCategoria)
        {
            return await _produtoDataSource.BuscarProdutosCategoria(IdCategoria);
        }

        public async Task ExcluirProduto(Produto produto)
        {
            await _produtoDataSource.ExcluirProduto(produto);
        }

        public async Task IncluirProduto(Produto produto)
        {
            await _produtoDataSource.IncluirProduto(produto);
        }

        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
            return await _produtoDataSource.ListarProdutos();
        }

        public async Task<Produto> BuscarProdutoPorId(int idProduto)
        {
            return await _produtoDataSource.BuscarProdutoPorProdutoID(idProduto);
        }
    }
}
