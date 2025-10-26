using Application.Configurations;
using DataSource.Repositories.Interfaces;
using Domain;
using System.Linq;

namespace DataSource
{
    public class DataSource : Adapters.Gateways.Interfaces.IDataSource
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IPedidoProdutoRepository _pedidoProdutoRepository;

        public DataSource(IClienteRepository clienteRepository, IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, ICategoriaRepository categoriaRepository, IStatusRepository statusRepository, IPedidoProdutoRepository pedidoProdutoRepository)
        {
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _categoriaRepository = categoriaRepository;
            _statusRepository = statusRepository;
            _pedidoProdutoRepository = pedidoProdutoRepository;
        }

        #region Cliente Datasource
        public async Task AtualizarCliente(Cliente cliente)
        {
            _clienteRepository.Atualizar(cliente);
        }

        public async Task<Cliente> BuscarClientePorCPF(string cpf)
        {
            return _clienteRepository.BuscarPorId(cpf);
        }

        public async Task<Cliente> BuscarClientePorEmail(string email)
        {
            return _clienteRepository.Buscar(x => x.Email.Equals(email)).FirstOrDefault();
        }

        public async Task<IEnumerable<Cliente>> ListarTodos()
        {
            return _clienteRepository.ListarTodos();
        }

        public async Task ExcluirCliente(Cliente cliente)
        {
            if (!_clienteRepository.Existe(x => x.Cpf == cliente.Cpf))
                throw new BusinessException("CPF não cadastrado");

            _clienteRepository.Atualizar(cliente);
        }

        public async Task<Cliente> IncluirCliente(Cliente cliente)
        {
            if (_clienteRepository.Existe(x => x.Cpf == cliente.Cpf))
                throw new BusinessException("Cliente já existe");

            _clienteRepository.Inserir(cliente);
            return cliente;
        }
        #endregion

        #region Categoria DataSource

        public async Task<IEnumerable<Categoria>> ListarCategorias()
        {
            return _categoriaRepository.ListarTodos();
        }



        #endregion

        #region Produto DataSource
        public async Task<Produto> IncluirProduto(Produto produto)
        {
            if (_produtoRepository.Existe(x => x.IdProduto == produto.IdProduto))
                throw new BusinessException("Produto ja existe");

            _produtoRepository.Inserir(produto);
            return produto;
        }

        public async Task AtualizarProduto(Produto produto)
        {
            _produtoRepository.Atualizar(produto);
        }

        public async Task ExcluirProduto(Produto produto)
        {
            if (!_produtoRepository.Existe(x => x.IdProduto == produto.IdProduto))
                throw new BusinessException("Produto não cadastrado");

            _produtoRepository.Excluir(produto.IdProduto);
        }

        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
            return _produtoRepository.ListarTodos();
        }

        public async Task<List<Produto>> BuscarProdutosCategoria(int IdCategoria)
        {
            return _produtoRepository.Buscar(x => x.IdCategoria == IdCategoria).ToList();
        }

        public async Task<Produto> BuscarProdutoPorProdutoID(int produtoID)
        {
            return _produtoRepository.BuscarPorId(produtoID);
        }

        #endregion

        #region Pedido DataSource

        public async Task<Pedido> IniciarPedido(string cpf)
        {
            Cliente cliente = null;

            if (!string.IsNullOrWhiteSpace(cpf))
            {
                cliente = await BuscarClientePorCPF(cpf);
                if (cliente == null)
                    throw new BusinessException("Cliente não encontrado.");
            }

            var pedido = new Pedido
            {
                Cpf = cliente?.Cpf,
                IdStatusAtual = 1,
                ValorTotal = 0
            };

            _pedidoRepository.Inserir(pedido);

            return pedido;
        }

        public async Task AtualizarStatusPedido(int novoStatusId, int idPedido)
        {
            var pedido = _pedidoRepository.BuscarPorId(idPedido) ?? throw new BusinessException("Pedido não encontrado.");
            var status = BuscarStatusPorId(novoStatusId) ?? throw new BusinessException("Status não encontrado.");

            if (pedido.IdStatusAtual == 5 || pedido.IdStatusAtual == 6)
            {
                throw new BusinessException("Não é possível Atualizar um pedido que já está finalizado ou cancelado.");
            }

            pedido.IdStatusAtual = novoStatusId;
            pedido.IdStatusAtualNavigation = status.Result;
            AtualizarPedido(pedido);           
        }
        public async Task<Pedido> BuscarPedidoPorId(int idPedido)
        {
            var pedido = _pedidoRepository.BuscarPorId(idPedido);
            pedido.PedidoProdutos = CarregarTodosProdutosPedido(idPedido).Result;

            return pedido;
        }
        public async Task CancelarPedido(int idPedido)
        {
            var pedido = await BuscarPedidoPorId(idPedido)
                         ?? throw new BusinessException("Pedido não encontrado.");

            if (pedido.IdStatusAtual == 5 || pedido.IdStatusAtual == 6)
            {
                throw new BusinessException("Não é possível Cancelar um pedido que já está finalizado ou cancelado.");
            }

            pedido.IdStatusAtual = 6;
            pedido.IdStatusAtualNavigation = await BuscarStatusPorId(5);

            AtualizarPedido(pedido);
        }
        public async Task FinalizarPedido(int idPedido)
        {
            var pedido = await BuscarPedidoPorId(idPedido)
                         ?? throw new BusinessException("Pedido não encontrado.");

            if (pedido.IdStatusAtual == 5 || pedido.IdStatusAtual == 6)
            {
                throw new BusinessException("Não é possível finalizar um pedido que já está finalizado ou cancelado.");
            }

            pedido.IdStatusAtual = 5;
            pedido.IdStatusAtualNavigation = await BuscarStatusPorId(5);

            AtualizarPedido(pedido);
        }

        public async Task<IEnumerable<Pedido>> ListarPedidoClienteStatus()
        {
            var filtrados = _pedidoRepository
                .ListarTodos()
                .Where(p => p.IdStatusAtual != 5 && p.IdStatusAtual != 6);

            var ordenados = filtrados
                .OrderBy(p =>
                    p.IdStatusAtual == 4 ? 0 :   // Pronto
                    p.IdStatusAtual == 3 ? 1 :   // Em Preparação
                    p.IdStatusAtual == 2 ? 2 :   // Recebido
                    3                             // Qualquer outro (Aguardando, Cancelado…)
                )
                .ThenBy(p => p.DataPedido ?? DateOnly.MinValue)
                .ToList();

            return await Task.FromResult(ordenados);
        }
        
        public async Task<IEnumerable<Pedido>> ListarPedidoCozinha() 
        {
            return _pedidoRepository.Buscar(x => x.IdStatusAtual.Equals(2)).ToList();
        }

        public async Task<IEnumerable<Pedido>> ListarPedidos()
        {
            var filtrados = _pedidoRepository
                .ListarTodos()
                .Where(p => p.IdStatusAtual != 5 && p.IdStatusAtual != 6);

            var ordenados = filtrados
                .OrderBy(p =>
                    p.IdStatusAtual == 4 ? 0 :   // Pronto
                    p.IdStatusAtual == 3 ? 1 :   // Em Preparação
                    p.IdStatusAtual == 2 ? 2 :   // Recebido
                    3                             // Qualquer outro (Aguardando, Cancelado…)
                )
                .ThenBy(p => p.DataPedido ?? DateOnly.MinValue)
                .ToList();

            ordenados.ForEach(d=> d.PedidoProdutos = CarregarTodosProdutosPedido(d.IdPedido).Result);

            return await Task.FromResult(ordenados);
        }      

        public async Task<Pedido> AdicionarProduto(int idPedido, int idProduto, int quantidade, string? observacao) 
        {
            var produto = _produtoRepository.BuscarPorId(idProduto) ?? throw new BusinessException("Produto não encontrado.");
            if (quantidade <= 0)
                throw new BusinessException("Quantidade deve ser maior que zero.");

            var pedido = await BuscarPedidoPorId(idPedido) ?? throw new BusinessException("Pedido não encontrado.");
            var itemExistente = pedido.PedidoProdutos.FirstOrDefault(pp => pp.IdProduto == idProduto);

            if (itemExistente == null)
            {
                var novoItem = new PedidoProduto
                {
                    IdPedido = idPedido,
                    IdProduto = idProduto,
                    Quantidade = quantidade,
                    Observacao = observacao
                };
                pedido.PedidoProdutos.Add(novoItem);
            }
            else
            {
                itemExistente.Quantidade += quantidade;
                itemExistente.Observacao = observacao;
            }

            AtualizarPedido(pedido);

            RecalcularValorTotal(pedido);
            return pedido;
        }

        public async Task<Pedido> AtualizarProduto(int idPedido, int idPedidoProduto, int novaQuantidade, string? observacao) 
        {
            var pedido = await BuscarPedidoPorId(idPedido) ?? throw new BusinessException("Pedido não encontrado.");
            var produto = _produtoRepository.BuscarPorId(idPedidoProduto) ?? throw new BusinessException("Produto não encontrado.");

            var itemPedido = pedido.PedidoProdutos.FirstOrDefault(pp => pp.IdPedidoProduto == idPedidoProduto);
            if (itemPedido == null)
                throw new BusinessException("Item do pedido não encontrado.");

            itemPedido.Quantidade = novaQuantidade;
            itemPedido.Observacao = observacao;

            AtualizarPedido(pedido);

            RecalcularValorTotal(pedido);
            AtualizarPedido(pedido);

            return pedido;
        }
        public async Task<Pedido> RemoverProduto(int idPedido, int idPedidoProduto)
        {
            var pedido = await BuscarPedidoPorId(idPedido) ?? throw new BusinessException("Pedido não encontrado.");
            var itemPedido = pedido.PedidoProdutos.FirstOrDefault(pp => pp.IdPedidoProduto == idPedidoProduto);
            if (itemPedido == null)
                throw new BusinessException("Item do pedido não encontrado.");

            // Remove o item diretamente do contexto
            _pedidoProdutoRepository.Excluir(itemPedido.IdPedidoProduto);

            RecalcularValorTotal(pedido);
            AtualizarPedido(pedido);

            return pedido;
        }
        public void AtualizarPedido(Pedido pedido)
        {
            _pedidoRepository.Atualizar(pedido);
        }
        public void RecalcularValorTotal(Pedido pedido)
        {
            if (pedido == null)
                throw new BusinessException("Pedido não informado para recalcular valor.");

            // Carrega todos os itens do pedido (deve vir populado via navegação ou carregado antes)
            var itens = pedido.PedidoProdutos;
            if (itens == null || !itens.Any())
            {
                pedido.ValorTotal = 0m;
            }
            else
            {
                var idsProdutos = itens
                    .Where(pp => pp.IdProduto.HasValue)
                    .Select(pp => pp.IdProduto.Value)
                    .Distinct()
                    .ToList();

                var produtosDoPedido = _produtoRepository
                    .Buscar(p => idsProdutos.Contains(p.IdProduto))
                    .ToDictionary(p => p.IdProduto, p => p.Preco);

                decimal total = 0m;
                foreach (var item in itens)
                {
                    var produtoId = item.IdProduto
                                    ?? throw new BusinessException("Item do pedido sem produto definido");
                    if (!produtosDoPedido.TryGetValue(produtoId, out var preco))
                        throw new BusinessException($"Produto {item.IdProduto} não encontrado.");

                    total += (item.Quantidade.GetValueOrDefault() * preco);
                }

                pedido.ValorTotal = total;
            }

            AtualizarPedido(pedido);
        }

        #endregion

        #region PedidoProduto DataSource
        public async Task<IList<PedidoProduto>> CarregarTodosProdutosPedido(int idPedido)
        {
            return _pedidoProdutoRepository.Buscar(d => d.IdPedido == idPedido).ToList();
        }
        #endregion

        #region Status
        public async Task<IEnumerable<Status>> ListarTodosStatus()
        {
            return _statusRepository.ListarTodos();
        }

        public async Task<Status> BuscarStatusPorNome(string nomeStatus)
        {            
            return _statusRepository.Buscar(x => x.Nome == nomeStatus).First();            
        }

        public async Task<Status> BuscarStatusPorId(int idStatus)
        {
            return _statusRepository.BuscarPorId(idStatus);
        }
        #endregion

    }
}