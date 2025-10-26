using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Presenters.Pedido
{
    public class AdicionarProdutoPedidoRequest
    {
        [Required]
        public int IdProduto { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "A quantidade deve ser no m�nimo 1.")]
        public int Quantidade { get; set; }

        public string? Observacao { get; set; }
    }
}