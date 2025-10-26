using Adapters.Gateways.Interfaces;
using Domain;

namespace Adapters.Gateways
{
    public class CategoriaGateway : ICategoriaGateway
    {
        private readonly IDataSource _clienteDataSource;

        public CategoriaGateway(IDataSource clienteDataSource)
        {
            _clienteDataSource = clienteDataSource;
        }

        public async Task<IEnumerable<Categoria>> ListarTodasCategorias()
        {
            return await _clienteDataSource.ListarCategorias();
        }
        public async Task<Categoria> BuscarCategoriaPorId(int idCategoria)
        {
             var categorias = _clienteDataSource.ListarCategorias();

            return categorias.Result.FirstOrDefault(d=> d.IdCategoria == idCategoria);
        }
    }
}
