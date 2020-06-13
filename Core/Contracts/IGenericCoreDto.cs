using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IGenericCoreDto<in TEntityDto, TEntity>
    {
        Task<IEnumerable<TEntity>> AllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> InsertAsync(TEntityDto entityDto);
        Task<TEntity> UpdateAsync(TEntityDto entityDto);
        Task DeleteAsync(TEntityDto entityDto);
    }
}