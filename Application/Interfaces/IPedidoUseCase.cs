using Domain;

namespace Application.Interfaces
{
    public interface IPedidoUseCase
    {
        Task<IEnumerable<Status>> ListarStatus();
        Task<Pedido> IniciarPedido(string cpf);
        Task<Pedido> AdicionarProduto(int idPedido, int idProduto, int quantidade, string? observacao);
        Task<Pedido> AtualizarProduto(int idPedido, int idPedidoProduto, int novaQuantidade, string? observacao);
        Task<Pedido> RemoverProduto(int idPedido, int idPedidoProduto);
        Task<IEnumerable<Pedido>> ListarPedidos();
        Task<Pedido> AtualizarStatusPedido(int idPedido, int novoStatusId);       

    }
}
