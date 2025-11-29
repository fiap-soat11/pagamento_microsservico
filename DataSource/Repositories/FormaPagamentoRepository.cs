using DataSource.Context;
using DataSource.Repositories.Interfaces;
using Domain;

namespace DataSource.Repositories
{
    public class FormaPagamentoRepository : RepositoryBase<FormaPagamento, int>, IFormaPagamentoRepository
    {
        public FormaPagamentoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
