using System.Collections.Generic;

namespace Core.Contracts
{
    public interface IParentChildrenCoreDto<in TEntityDto, out TEntity>
    {
        IEnumerable<TEntity> All(int parentId);
        TEntity GetById(int parentId, int childId);
        TEntity Insert(int parentId, TEntityDto entityDto);
        TEntity Update(int parentId);
        void Delete(int parentId, TEntityDto entityDto);
    }
}