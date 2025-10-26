using Application.Interfaces;
using Domain;
using Application.Configurations;

namespace Application.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {        
        public async Task<Pedido> IniciarPedido(string cpf)
        {
            // CPF opcional: caso nulo ou vazio, gateway tratará como pedido de convidado
            return new Pedido();
        }

        public async Task<IEnumerable<Status>> ListarStatus()
        {
            //return _statusRepository.ListarTodos();
            return new List<Status>();
        }

        /*public async Task<IEnumerable<Pedido>> ListarPedidos()
        {
            //return _pedidoRepository.ListarTodos();

            return Enumerable.Empty<Pedido>();
        }*/

        public async Task<Pedido> AdicionarProduto(int idPedido, int idProduto, int quantidade, string? observacao)
        {
            PedidoProduto produto = new PedidoProduto() { IdPedido = idPedido, IdProduto = idProduto, Observacao = observacao, Quantidade = quantidade };

            var pedido = new Pedido() { IdPedido = idPedido };
            pedido.PedidoProdutos.Add(produto);

            return pedido;
        }

        public async Task<Pedido> AtualizarProduto(int idPedido, int idPedidoProduto, int novaQuantidade, string? observacao)
        {
            return new Pedido();
        }

        public async Task<Pedido> RemoverProduto(int idPedido, int idPedidoProduto)
        {
            return new Pedido();
        }

        private void RecalcularValorTotal(Pedido pedido)
        {
            // É necessário carregar o preço de cada produto
            /*
            var idsProdutos = pedido.PedidoProdutos.Select(pp => pp.IdProduto).ToList();
            var produtosDoPedido = _produtoRepository.Buscar(p => idsProdutos.Contains(p.IdProduto));

            decimal valorTotal = 0;
            foreach (var item in pedido.PedidoProdutos)
            {
                var produto = produtosDoPedido.First(p => p.IdProduto == item.IdProduto);
                valorTotal += (item.Quantidade ?? 0) * produto.Preco;
            }

            pedido.ValorTotal = valorTotal;
            */
        }       

        public async Task<IEnumerable<Pedido>> ListarPedidos()
        {
            return new List<Pedido>();
        }

        public async Task<Pedido> AtualizarStatusPedido(int idPedido, int novoStatusId)
        {
            

            return new Pedido();
        }
    }
}
