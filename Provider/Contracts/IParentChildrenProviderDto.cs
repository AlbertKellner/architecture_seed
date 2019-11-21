namespace Provider.Contracts
{
    using System.Collections.Generic;

    public interface IParentChildrenProviderDto<in TEntityDto, out TEntity>
    {
        IEnumerable<TEntity> All(int parentId);
        TEntity GetById(int parentId, int childId);
        TEntity Insert(int parentId, TEntityDto entityDto);
        TEntity Update(int parentId);
        void Delete(int parentId, TEntityDto entityDto);
    }
}