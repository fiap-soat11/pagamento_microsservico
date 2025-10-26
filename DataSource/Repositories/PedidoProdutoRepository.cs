using Domain;
using DataSource.Context;
using DataSource.Repositories.Interfaces;

namespace DataSource.Repositories
{
    public class PedidoProdutoRepository : RepositoryBase<PedidoProduto, int>, IPedidoProdutoRepository
    {
        public PedidoProdutoRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}