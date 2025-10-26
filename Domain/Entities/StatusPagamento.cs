using System;
using System.Collections.Generic;

namespace Domain;

public partial class StatusPagamento
{
    public int IdStatusPagamento { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
