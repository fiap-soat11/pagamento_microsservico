using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Domain;

namespace Adapters.Controllers
{
    public class CategoriaController : ICategoriaController
    {
        private readonly ICategoriaGateway _categoriaGateway;

        public CategoriaController(ICategoriaGateway categoriaGateway)
        {
            _categoriaGateway = categoriaGateway;
        }

        public async Task<List<Categoria>> ListarTodos()
        {
            return (await _categoriaGateway.ListarTodasCategorias()).ToList();
        }
    }
}
