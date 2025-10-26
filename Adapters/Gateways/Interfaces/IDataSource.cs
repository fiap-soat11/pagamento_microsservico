using Domain;

namespace Adapters.Gateways.Interfaces
{
    public interface IDataSource
    {
        #region Cliente DataSource
        Task AtualizarCliente(Cliente cliente);
        Task<Cliente> BuscarClientePorCPF(string cpf);
        Task<Cliente> BuscarClientePorEmail(string email);
        Task<IEnumerable<Cliente>> ListarTodos();
        Task ExcluirCliente(Cliente cliente);
        Task<Cliente> IncluirCliente(Cliente cliente);

        #endregion

        #region Categoria DataSource
        Task<IEnumerable<Categoria>> ListarCategorias();

        #endregion

        #region Produto DataSource
        Task<Produto> IncluirProduto(Produto produto);
        Task AtualizarProduto(Produto produto);
        Task ExcluirProduto(Produto produto);
        Task<IEnumerable<Produto>> ListarProdutos();
        Task<List<Produto>> BuscarProdutosCategoria(int IdCategoria);
        Task<Produto> BuscarProdutoPorProdutoID(int produtoID);
        #endregion

        #region Pedido DataSource
        Task<Pedido> IniciarPedido(string cpf);
        Task AtualizarStatusPedido(int idStatusPedido, int idPedido);
        Task<Pedido> BuscarPedidoPorId(int idPedido);
        Task CancelarPedido(int idPedido);
        Task FinalizarPedido(int idPedido);
        Task<IEnumerable<Pedido>> ListarPedidoClienteStatus();
        Task<IEnumerable<Pedido>> ListarPedidoCozinha();
        Task<IEnumerable<Pedido>> ListarPedidos();
        Task<Pedido> AdicionarProduto(int idPedido, int idProduto, int quantidade, string? observacao);
        Task<Pedido> AtualizarProduto(int idPedido, int idPedidoProduto, int novaQuantidade, string? observacao);
        Task<Pedido> RemoverProduto(int idPedido, int idPedidoProduto);
        void AtualizarPedido(Pedido pedido);
        void RecalcularValorTotal(Pedido pedido);

        #endregion

        #region Status DataSource
        Task<IEnumerable<Status>> ListarTodosStatus();
        Task<Status> BuscarStatusPorNome(string nomeStatus);
        Task<Status> BuscarStatusPorId(int idStatus);

        #endregion

        #region PedidoProduto DataSource
        Task<IList<PedidoProduto>> CarregarTodosProdutosPedido(int idPedido);
        #endregion

    }

} 
