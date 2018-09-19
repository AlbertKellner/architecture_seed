namespace Repository.Contracts
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;

        int SaveChanges();
        void DetachEntry<TEntity>(TEntity entity) where TEntity : class;
        void DetachAllEntities();
    }

    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}