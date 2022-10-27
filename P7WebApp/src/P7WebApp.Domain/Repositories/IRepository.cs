using System.Linq.Expressions;

namespace P7WebApp.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
