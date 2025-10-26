using Domain;

namespace Adapters.Gateways.Interfaces
{
    public interface IProdutoGateway
    {
        Task IncluirProduto(Produto produto);
        Task AtualizarProduto(Produto produto);
        Task ExcluirProduto(Produto produto);
        Task<IEnumerable<Produto>> ListarProdutos();
        Task<List<Produto>> BuscarProdutosCategoria(int IdCategoria);
        Task<Produto> BuscarProdutoPorId(int idProduto);
    }
}