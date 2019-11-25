using System.Collections.Generic;

namespace Core.Contracts
{
    public interface IGenericProvider<TEntity>
    {
        IEnumerable<TEntity> All();
        TEntity GetById(int id);
        TEntity GetByIdentity(string id);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}