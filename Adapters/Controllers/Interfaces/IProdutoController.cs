using Adapters.Presenters.Produto;
using Domain;

namespace Adapters.Controllers.Interfaces
{
    public interface IProdutoController
    {
        Task<List<Produto>> BuscarProdutosPorCategoria(int idCategoria);
        Task<Produto> BuscarProdutoPorId(int idProduto);
        Task<IEnumerable<Produto>> ListarTodos();
        Task IncluirProduto(ProdutoRequest produtoRequest);
        Task AtualizarProduto(ProdutoRequest produtoRequest);
        Task ExcluirProduto(int idProduto);
    }
}