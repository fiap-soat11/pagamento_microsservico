using System;
using System.Collections.Generic;
    
namespace Domain;

public partial class ProdutoIngrediente
{
    public int IdProdutoIngrediente { get; set; }

    public int IdIngrediente { get; set; }

    public int IdProduto { get; set; }

    public decimal Quantidade { get; set; }

    public virtual Ingrediente IdIngredienteNavigation { get; set; } = null!;

    public virtual Produto IdProdutoNavigation { get; set; } = null!;
}
