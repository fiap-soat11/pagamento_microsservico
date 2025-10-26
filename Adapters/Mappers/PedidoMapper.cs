using Adapters.Presenters.Pedido;
using Adapters.Presenters.Cliente;
using Adapters.Presenters.Produto;
using Domain;

namespace Adapters.Mappers
{
    public class PedidoMapper
    {
        public static PedidoClienteResponse PedidoClienteToDTO(Pedido pedido)
        {
            return new PedidoClienteResponse
            {
                IdPedido = pedido.IdPedido,
                IdStatusAtual = pedido.IdStatusAtual
            };
        }

        /*public static PedidoResponse ToDTO(Pedido pedido)
        {
            return new PedidoResponse
            { 
                IdPedido = pedido.IdPedido,
                Cpf = pedido.Cpf,
                IdStatusAtual = pedido.IdStatusAtual,
                ValorTotal = pedido.ValorTotal,
                DataPedido = pedido.DataPedido,
                //CpfNavigation = pedido.CpfNavigation,
                //IdStatusAtualNavigation = pedido.IdStatusAtualNavigation,
                //Pagamentos = pedido.Pagamentos,
                //PedidoProdutos = pedido.PedidoProdutos,
                //Preparos = pedido.Preparos                
            
            };
        }*/
        /*public static PedidoResponse ToResponse(this Pedido pedido)
        {
            return new PedidoResponse
            {
                IdPedido = pedido.IdPedido,
                Cpf = pedido.Cpf,
                IdStatusAtual = pedido.IdStatusAtual,
                DataPedido = pedido.DataPedido ?? DateOnly.MinValue,
                ValorTotal = pedido.ValorTotal ?? 0,
                Produtos = pedido.PedidoProdutos.Select(pp => new PedidoProdutoResponse
                {
                    IdPedidoProduto = pp.IdPedidoProduto,
                    IdProduto = pp.IdProduto ?? 0,
                    Quantidade = pp.Quantidade ?? 0,
                    Observacao = pp.Observacao,
                    Produto = new ProdutoResponse
                    {
                        IdProduto = pp.IdProdutoNavigation.IdProduto,
                        Descricao = pp.IdProdutoNavigation.Descricao,
                        Preco = pp.IdProdutoNavigation.Preco
                    }
                }).ToList(),
            };
        }*/

        public static PedidoResponse ToResponse(Pedido pedido)
        {
            return new PedidoResponse
            {
                IdPedido = pedido.IdPedido,
                Cpf = pedido.Cpf,
                DataPedido = pedido.DataPedido ?? DateOnly.MinValue,
                IdStatusAtual = pedido.IdStatusAtual,
                ValorTotal = pedido.ValorTotal ?? 0m,
                PedidoProdutos = pedido.PedidoProdutos,
                QRCode = pedido.QRCode,
            };
        }

        public static PedidoCozinhaResponse PedidoCozinhaToDTO(Pedido pedido)
        {
            return new PedidoCozinhaResponse
            {
                IdPedido = pedido.IdPedido,
                IdStatusAtual = pedido.IdStatusAtual,
                PedidoProdutos = pedido.PedidoProdutos,
                Preparos = pedido.Preparos
            };
        }

        /*public static PedidoProdutoResponse PedidoProdutoToDTO(Pedido p)
        {
            return new PedidoProdutoResponse
            {
                IdPedido = p.IdPedido,
                Cpf = p.Cpf,
                DataPedido = p.DataPedido,
                IdStatusAtual = p.IdStatusAtual,
                ValorTotal = p.ValorTotal,
                Produtos = p.PedidoProdutos.Select(pp => new PedidoProdutoResponse
                {
                    IdPedidoProduto = pp.IdPedidoProduto,
                    IdProduto = pp.IdProduto,
                    Quantidade = pp.Quantidade,
                    Observacao = pp.Observacao,
                    Produto = new ProdutoResponse
                    {
                        IdProduto = pp.Produto.IdProduto,
                        Descricao = pp.Produto.Descricao,
                        Preco = pp.Produto.Preco
                    }
                }).ToList()
            };
        }*/
    }
}
