namespace Provider.Contracts
{
    using System.Collections.Generic;

    public interface IParentChildrenProviderDto<in TEntityDto, out TEntity>
    {
        IEnumerable<TEntity> All(int userId, int parentId);
        TEntity GetById(int userId, int parentId, int childId);
        TEntity Insert(int userId, int parentId, TEntityDto entityDto);
        TEntity Update(int userId, int parentId, TEntityDto entityDto);
        void Delete(int userId, int parentId, TEntityDto entityDto);
    }
}