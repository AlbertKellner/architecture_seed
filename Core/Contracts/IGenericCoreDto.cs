using System.Collections.Generic;

namespace Core.Contracts
{
    public interface IGenericCoreDto<in TEntityDto, out TEntity>
    {
        IEnumerable<TEntity> All();
        TEntity GetById(int id);
        TEntity Insert(TEntityDto entityDto);
        TEntity Update(TEntityDto entityDto);
        void Delete(TEntityDto entityDto);
    }
}