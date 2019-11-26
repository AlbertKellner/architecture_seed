using System.Collections.Generic;
using Core.Contracts;
using DataEntity.Model;
using Repository.Contracts;

namespace Core
{
    public class UsuarioProvider : IGenericProvider<UsuarioEntity>
    {
        private readonly IRepository<UsuarioEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<UsuarioEntity>();
        }

        public IEnumerable<UsuarioEntity> Get() => _repository.GetList().Items;

        public UsuarioEntity Get(int id) => _repository.Single(e => e.Id == id);

        public UsuarioEntity GetByIdentity(string id) => _repository.Single(e => e.IdentityId == id);

        public UsuarioEntity Insert(UsuarioEntity entity) => new UsuarioEntity();

        public UsuarioEntity Update(UsuarioEntity entity) => new UsuarioEntity();

        public void Delete(UsuarioEntity entity)
        {
            //_repository.Delete(entity.Id);
            //_unitOfWork.SaveChanges();
        }

        //public UsuarioEntity GetByEmail(UsuarioEntity entity) => _repository.Single(e => e.UserName == entity.UserName);
    }
}