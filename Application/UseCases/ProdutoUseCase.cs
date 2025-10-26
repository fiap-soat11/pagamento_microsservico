using Application.Configurations;
using Domain;

namespace Application.UseCases
{
    public class ProdutoUseCase : IProdutoUseCase
    {
        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            return produto;
        }

        public async Task<Produto> BuscarProdutoPorId(int IdProduto)
        {           
           return new Produto { IdProduto = IdProduto };
        }       

        public async Task<List<Produto>> BuscarProdutoPorCategoria(int idCategoria)
        {
            //return _produtoRepository.Buscar(x => x.IdCategoria == idCategoria).ToList();
            return new List<Produto>();
        }

        public async Task<IEnumerable<Produto>> ListarTodos()
        {
            //return _produtoRepository.ListarTodos();
            return new List<Produto>();
        }

        public async Task<Produto> RemoverProduto(int idProduto)
        {
            var produto = await BuscarProdutoPorId(idProduto) ?? throw new BusinessException("Produto não encontrado.");

            /*
            if (!_produtoRepository.Existe(x => x.IdProduto == idProduto))
                throw new BusinessException("Produto Id não cadastrado");

            _produtoRepository.Excluir(idProduto);

            */
            return produto;
        }
    }
}