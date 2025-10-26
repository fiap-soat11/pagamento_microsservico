using System.Collections;
using Domain;

namespace Adapters.Presenters.Categoria
{
    public class CategoriaResponse
    {
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Domain.Produto> Produtos { get; set; } = new List<Domain.Produto>();

    }
}
