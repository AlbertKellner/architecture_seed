using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IParentChildrenCoreDto<in TEntityDto, TEntity>
    {
        Task<IEnumerable<TEntity>> AllAsync(int parentId);
        Task<TEntity> GetByIdAsync(int parentId, int childId);
        Task<TEntity> InsertAsync(int parentId, TEntityDto entityDto);
        Task<TEntity> UpdateAsync(int parentId);
        Task DeleteAsync(int parentId, TEntityDto entityDto);
    }
}