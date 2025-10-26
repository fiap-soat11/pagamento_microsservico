using System;
using System.Collections.Generic;

namespace Domain;

public partial class Ingrediente
{
    public int IdIngrediente { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string UnidadeMedida { get; set; } = null!;

    public decimal? PrecoUnitario { get; set; }

    public decimal? QuantidadeEmEstoque { get; set; }

    public decimal? EstoqueMinimo { get; set; }

    public virtual ICollection<ProdutoIngrediente> ProdutoIngredientes { get; set; } = new List<ProdutoIngrediente>();
}
