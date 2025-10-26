using DataSource.Context;
using DataSource.Repositories.Interfaces;
using Domain;

namespace DataSource.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto, int>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}