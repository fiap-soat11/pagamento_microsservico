using System;
using System.Collections.Generic;

namespace Domain;

public partial class Produto
{
    public int IdProduto { get; set; }

    public string Nome { get; set; } = null!;

    public int IdCategoria { get; set; }

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public string? Imagens { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();

    public virtual ICollection<ProdutoIngrediente> ProdutoIngredientes { get; set; } = new List<ProdutoIngrediente>();
}
