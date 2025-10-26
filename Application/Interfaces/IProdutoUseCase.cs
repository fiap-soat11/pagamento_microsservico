using Domain;

namespace Application.UseCases
{
    public interface IProdutoUseCase
    {
        Task<Produto> AtualizarProduto(Produto produto);
        Task<IEnumerable<Produto>> ListarTodos();
        Task<Produto> BuscarProdutoPorId(int IdProduto);        
        Task<List<Produto>> BuscarProdutoPorCategoria(int idCategoria);
       Task<Produto> RemoverProduto(int idProduto);
    }
}