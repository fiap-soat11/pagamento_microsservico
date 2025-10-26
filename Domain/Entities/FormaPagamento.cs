using System;
using System.Collections.Generic;

namespace Domain;

public partial class FormaPagamento
{
    public int IdFormaPagamento { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public bool? Ativo { get; set; }

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
