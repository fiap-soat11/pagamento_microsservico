using System;
using System.Collections.Generic;

namespace Domain;

public partial class Status
{
    public int IdStatus { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Preparo> Preparos { get; set; } = new List<Preparo>();
}
