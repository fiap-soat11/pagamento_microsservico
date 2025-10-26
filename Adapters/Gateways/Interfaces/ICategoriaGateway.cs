using Domain;

namespace Adapters.Gateways.Interfaces
{
    public interface ICategoriaGateway 
    {
        Task<IEnumerable<Domain.Categoria>> ListarTodasCategorias();
        Task<Categoria> BuscarCategoriaPorId(int idCategoria);
    }
}