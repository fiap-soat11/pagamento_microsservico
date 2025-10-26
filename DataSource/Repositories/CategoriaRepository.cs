using DataSource.Context;
using DataSource.Repositories.Interfaces;
using Domain;

namespace DataSource.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria, string>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
