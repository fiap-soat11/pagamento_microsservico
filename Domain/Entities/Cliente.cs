using System;
using System.Collections.Generic;

namespace Domain;

public partial class Cliente
{
    public string Cpf { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? Email { get; set; }

    public bool Ativo { get; set; } = true;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
