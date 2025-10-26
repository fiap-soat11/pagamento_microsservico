using Adapters.Presenters.Categoria;
using Adapters.Presenters.Produto;
using Domain;

namespace WebAPI.Mappers
{
    public class ProdutoMapper
    {
        public static ProdutoResponse ProdutoToDTO(Produto produto)
        {
            return new ProdutoResponse
            {
                IdProduto = produto.IdProduto,
            };
        }

        public static ProdutoResponse ToDTO(Produto produto)
        {
            return new ProdutoResponse
            { 
                IdProduto = produto.IdProduto,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                IdCategoria = produto.IdCategoria,
                Preco = produto.Preco,
                Imagem = produto.Imagens
            };
        }
        public static Produto ToEntity(ProdutoRequest dto)
        {
            return new Produto
            {
                IdProduto = dto.IdProduto,
                Nome = dto.Nome,
                IdCategoria= dto.IdCategoria,
                Descricao= dto.Descricao,
                Preco  = dto.Preco,
                Imagens = dto.Imagens
            };
        }
    }
}
