﻿using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext;

    public RepositoryBase(RepositoryContext repositoryContext)
        => RepositoryContext = repositoryContext;


    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

    public IQueryable<T> GetAll(bool trackChanges) =>
        !trackChanges ?
        RepositoryContext.Set<T>()
            .AsNoTracking() :
        RepositoryContext.Set<T>();

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> exception,
        bool trackChanges) =>
            !trackChanges ?
        RepositoryContext.Set<T>()
            .Where(exception)
            .AsNoTracking() :
        RepositoryContext.Set<T>()
            .Where(exception);
}
