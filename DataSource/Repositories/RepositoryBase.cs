using DataSource.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataSource.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IDisposable, IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly ApplicationDbContext _entities;


        private DbSet<TEntity> Dbset
        {
            get { return _entities.Set<TEntity>(); }
        }

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _entities = dbContext;
        }

        public IEnumerable<TEntity> ListarTodos()
        {
            return Dbset.AsEnumerable();
        }

        public TEntity BuscarPorId(TKey id)
        {
            return Dbset.Find(id);
        }

        public void Inserir(TEntity entity)
        {
            Dbset.Add(entity);
            _entities.SaveChanges();
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return Dbset.Where(predicate);
        }



        public void Atualizar(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            Dbset.Attach(entity);
            _entities.Entry(entity).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Excluir(TKey id)
        {
            var entity = BuscarPorId(id);
            if (entity == null)
                throw new Exception("Entidade não existe");
            Dbset.Remove(entity);
            _entities.SaveChanges();
        }

        public void Excluir(TEntity entity)
        {
            Dbset.Remove(entity);
            _entities.SaveChanges();
        }

        public void Excluir(Expression<Func<TEntity, bool>> @where)
        {
            IEnumerable<TEntity> objects = Dbset.Where(where).AsEnumerable();
            foreach (TEntity obj in objects)
                Dbset.Remove(obj);
            _entities.SaveChanges();
        }

        public bool Existe(Expression<Func<TEntity, bool>> predicate)
        {
            return Dbset.Any(predicate);
        }


        public void Dispose()
        {
            if (_entities != null)
                _entities.Dispose();
        }


    }
}
