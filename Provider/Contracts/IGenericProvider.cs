namespace Provider.Contracts
{
    using System.Collections.Generic;

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