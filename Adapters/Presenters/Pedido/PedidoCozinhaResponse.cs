using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Presenters.Pedido
{
    public class PedidoCozinhaResponse
    {
        public int IdPedido { get; set; }

        public int? IdStatusAtual { get; set; }

        public virtual ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();

        public virtual ICollection<Preparo> Preparos { get; set; } = new List<Preparo>();
    }
}
