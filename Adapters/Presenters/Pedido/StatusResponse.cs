using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Presenters.Pedido
{
    public class StatusResponse
    {
        public int IdStatus { get; set; }

        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

    }
}
