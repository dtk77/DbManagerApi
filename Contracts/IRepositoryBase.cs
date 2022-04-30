using System.Linq.Expressions;

namespace Contracts;
public interface IRepositoryBase<T>
{
    IQueryable<T> GetAll(bool trackChanges);
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> exception,
        bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
  
}
