using Domain;
using DataSource.Context;
using DataSource.Repositories.Interfaces;

namespace DataSource.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente, string>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
