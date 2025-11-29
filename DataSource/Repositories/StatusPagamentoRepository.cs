using DataSource.Context;
using DataSource.Repositories.Interfaces;
using Domain;

namespace DataSource.Repositories
{
    public class StatusPagamentoRepository : RepositoryBase<StatusPagamento, int>, IStatusPagamentoRepository
    {
        public StatusPagamentoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
