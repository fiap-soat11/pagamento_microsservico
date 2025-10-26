using Adapters.Presenters.Categoria;
using Adapters.Presenters.Pedido;
using Domain;

namespace WebAPI.Mappers
{
    public class CategoriaMapper
    {
        public static CategoriaResponse CategoriaClienteToDTO(Categoria categoria)
        {
            return new CategoriaResponse
            {
                IdCategoria = categoria.IdCategoria
            };
        }

        public static CategoriaResponse ToDTO(Categoria categoria)
        {
            return new CategoriaResponse
            { 
                IdCategoria = categoria.IdCategoria,
                Nome = categoria.Nome//,
               // Produtos = categoria.Produtos
            };
        }
    }
}
