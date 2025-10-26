using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public interface ICategoriaUseCase
    {
        Task<IEnumerable<Categoria>> ListarTodos();
        Task FinalizarCategoria(Categoria categoria);
        Task<Categoria> BuscarCategoriaPorId(int IdCategoria);
        Task CancelarCategoria(Categoria categoria);

    }
}
