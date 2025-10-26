using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Presenters.Cliente
{
    public class ClienteResponse
    {
        public string Cpf { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public string? Email { get; set; }
    }
}
