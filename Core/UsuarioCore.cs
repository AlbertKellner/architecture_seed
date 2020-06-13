using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts;
using DataEntity.Model;
using Repository.Contracts;

namespace Core
{
    public class UsuarioCore : IGenericCore<UsuarioEntity>
    {
        private readonly IRepositoryAsync<UsuarioEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioCore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepositoryAsync<UsuarioEntity>();
        }

        public async Task<IEnumerable<UsuarioEntity>> GetAsync() => (await _repository.GetListAsync()).Items;

        public async Task<UsuarioEntity> GetAsync(int id) => await _repository.SingleAsync(e => e.Id == id);

        public async Task<UsuarioEntity> GetByIdentityAsync(string id) => await _repository.SingleAsync(e => e.IdentityId == id);

        public async Task<UsuarioEntity> InsertAsync(UsuarioEntity entity) => await Task.Run(() => new UsuarioEntity());

        public async Task<UsuarioEntity> UpdateAsync(UsuarioEntity entity) => await Task.Run(() => new UsuarioEntity());

        public async Task DeleteAsync(UsuarioEntity entity)
        {
            await Task.CompletedTask;
            //_repository.Delete(entity.Id);
            //_unitOfWork.SaveChanges();
        }

        //public UsuarioEntity GetByEmail(UsuarioEntity entity) => _repository.Single(e => e.UserName == entity.UserName);
    }
}