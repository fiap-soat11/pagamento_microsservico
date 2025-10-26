using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        void Inserir(TEntity entity);
        void Atualizar(TEntity entity);
        void Excluir(TKey id);
        TEntity BuscarPorId(TKey id);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> ListarTodos();

        bool Existe(Expression<Func<TEntity, bool>> predicate);

    }
}
