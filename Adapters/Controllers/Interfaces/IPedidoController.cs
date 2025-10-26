using Domain;
using Adapters.Presenters.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Controllers.Interfaces
{
    public interface IPedidoController
    {
        Task<IEnumerable<Pedido>> ListarPedidos();
        Task<IEnumerable<Pedido>> ListarPedidoClienteStatus();
        Task<IEnumerable<Pedido>> ListarPedidoCozinha();
        Task<bool> AtualizarStatusPedido(int idStatusPedido, int idPedido);
        Task<bool> FinalizarPedido(int pedido);
        Task<Pedido> BuscarPedidoPorId(int idPedido);
        Task<bool> CancelarPedido(int pedido);        
        Task<Pedido> IniciarPedido(string cpf);
        Task<Pedido> AdicionarProduto(int idPedido, int idProduto, int quantidade, string? observacao);
        Task<Pedido> AtualizarProduto(int idPedido, int idPedidoProduto, int novaQuantidade, string? observacao);
        Task<Pedido> RemoverProduto(int idPedido, int idPedidoProduto);
        List<PedidoProduto> ListarProdutosDoPedido(int idPedido);
    }
}
