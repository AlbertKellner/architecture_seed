namespace Provider.Contracts
{
    using System.Collections.Generic;

    public interface IGenericProvider<TEntity>
    {
        IEnumerable<TEntity> All(int userId);
        TEntity GetById(int userId, int id);
        TEntity GetByIdentity(string id);
        TEntity Insert(int userId, TEntity entity);
        TEntity Update(int userId, TEntity entity);
        void Delete(int userId, TEntity entity);
    }
}