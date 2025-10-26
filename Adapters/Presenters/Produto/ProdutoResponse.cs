using System.Collections;
using Domain;

namespace Adapters.Presenters.Categoria
{
    public class ProdutoResponse
    {
        public int IdProduto { get; set; }

        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? Imagem{ get; set; }

    }
}
