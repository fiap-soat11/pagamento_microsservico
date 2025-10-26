using Domain;

namespace Adapters.Gateways.Interfaces
{
    public interface IPedidoGateway
    {
        Task<Pedido> IniciarPedido(string cpf);
        Task AtualizarStatusPedido(int idStatusPedido, int idPedido);
        Task<IEnumerable<Pedido>> ListarPedidos();        
        Task<Pedido> BuscarPedidoPorId(int idPedido);
        Task CancelarPedido(int idPedido);
        Task FinalizarPedido(int idPedido);
        Task<IEnumerable<Pedido>> ListarPedidoClienteStatus();
        Task<IEnumerable<Pedido>> ListarPedidoCozinha();        
        Task<Pedido> AdicionarProduto(int idPedido, int idProduto, int quantidade, string? observacao);
        Task<Pedido> AtualizarProduto(int idPedido, int idPedidoProduto, int novaQuantidade, string? observacao);
        Task<Pedido> RemoverProduto(int idPedido, int idPedidoProduto);
        void AtualizarPedido(Pedido pedido);
        void RecalcularValorTotal(Pedido pedido);
        Task<IList<PedidoProduto>> CarregarTodosProdutosPedido(int idPedido);

    }
}
