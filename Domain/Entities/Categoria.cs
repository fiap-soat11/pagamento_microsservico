using System;
using System.Collections.Generic;

namespace Domain;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
