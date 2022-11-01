using Microsoft.EntityFrameworkCore;
using PrivateEvents.Contracts;
using PrivateEvents.Entities;
using System.Linq.Expressions;

namespace PrivateEvents.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext {get; set;}
    public RepositoryBase(RepositoryContext context)
    {
        RepositoryContext = context;
    }
    public IQueryable<T> FindAll() => 
        RepositoryContext.Set<T>().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        RepositoryContext.Set<T>().Where(expression).AsNoTracking();
    public void Create(T entity) =>
        RepositoryContext.Set<T>().Add(entity);
    public void Update(T entity) =>
        RepositoryContext.Set<T>().Update(entity);
    public void Delete(T entity) =>
        RepositoryContext.Set<T>().Remove(entity);
}