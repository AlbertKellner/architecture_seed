namespace Provider.Contracts
{
    using System.Collections.Generic;

    public interface IGenericProviderDto<in TEntityDto, out TEntity>
    {
        IEnumerable<TEntity> All();
        TEntity GetById(int id);
        TEntity Insert(TEntityDto entityDto);
        TEntity Update(TEntityDto entityDto);
        void Delete(TEntityDto entityDto);
    }
}