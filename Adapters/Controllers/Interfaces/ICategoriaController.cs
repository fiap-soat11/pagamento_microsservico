using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Controllers.Interfaces
{
    public interface ICategoriaController
    {
        Task<List<Categoria>> ListarTodos();
    }
}
