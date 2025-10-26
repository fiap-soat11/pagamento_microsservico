using Domain;

namespace Adapters.Presenters.Produto
{
    public class ProdutoRequest
    {
        public int IdProduto { get; set; }

        public string Nome { get; set; } = null!;

        public int IdCategoria { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public string? Imagens { get; set; }

    }
}
