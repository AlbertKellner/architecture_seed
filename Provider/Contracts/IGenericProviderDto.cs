namespace Provider.Contracts
{
    using System.Collections.Generic;

    public interface IGenericProviderDto<in TEntityDto, out TEntity>
    {
        IEnumerable<TEntity> All(int userId);
        TEntity GetById(int userId, int id);
        TEntity Insert(int userId, TEntityDto entityDto);
        TEntity Update(int userId, TEntityDto entityDto);
        void Delete(int userId, TEntityDto entityDto);
    }
}