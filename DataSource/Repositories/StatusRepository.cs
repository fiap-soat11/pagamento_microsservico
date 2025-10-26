using Domain;
using DataSource.Context;
using DataSource.Repositories.Interfaces;

namespace DataSource.Repositories
{
    public class StatusRepository : RepositoryBase<Status, int>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
