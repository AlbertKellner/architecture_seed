namespace Repository.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context) => Context = context ?? throw new ArgumentNullException(nameof(context));

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<TEntity>(Context);

            return (IRepository<TEntity>) _repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
                _repositories[type] = new RepositoryAsync<TEntity>(Context);

            return (IRepositoryAsync<TEntity>) _repositories[type];
        }

        public TContext Context { get; }

        public int SaveChanges() => Context.SaveChanges();

        public void Dispose() => Context?.Dispose();

        public void DetachEntry<TEntity>(TEntity entity) where TEntity : class => Context.Entry(entity).State = EntityState.Detached;

        public void DetachEntries<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities)
                Context.Entry(entity).State = EntityState.Detached;
        }

        public void DetachAllEntities()
        {
            foreach (var entity in Context.ChangeTracker.Entries().Where(e => e.State != EntityState.Detached).ToList())
                Context.Entry(entity.Entity).State = EntityState.Detached;
        }
    }
}