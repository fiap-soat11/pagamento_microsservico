using DataSource.Context;
using DataSource.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataSource.Repositories
{
    public class PagamentoRepository : RepositoryBase<Pagamento, int>, IPagamentoRepository
    {
        private readonly ApplicationDbContext _db;

        public PagamentoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        // Example of eager loading when needed
        public IEnumerable<Pagamento> ListarComRelacionamentos(Expression<Func<Pagamento, bool>>? predicate = null)
        {
            var query = _db.Pagamentos
                .Include(p => p.IdFormaPagamentoNavigation)
                .Include(p => p.IdStatusPagamentoNavigation)
                .AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.AsEnumerable();
        }
    }
}
