namespace Provider
{
    using System.Collections.Generic;
    using Contracts;
    using DataEntity.Model;
    using Microsoft.EntityFrameworkCore;
    using Repository.Contracts;

    public class UsuarioProvider : IGenericProvider<UsuarioEntity>
    {
        private readonly IRepository<UsuarioEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<UsuarioEntity>();
        }

        public IEnumerable<UsuarioEntity> All(int userId) => _repository.GetList().Items;

        public UsuarioEntity GetById(int id) => _repository.Single(e => e.Id == id);

        public UsuarioEntity GetByIdentity(string id) => _repository.Single(e => e.IdentityId == id);

        public UsuarioEntity Insert(int userId, UsuarioEntity entity) => new UsuarioEntity();

        public UsuarioEntity Update(int userId, UsuarioEntity entity) => new UsuarioEntity();

        public void Delete(int userId, UsuarioEntity entity)
        {
            //_repository.Delete(entity.Id);
            //_unitOfWork.SaveChanges();
        }

        //public UsuarioEntity GetByEmail(UsuarioEntity entity) => _repository.Single(e => e.UserName == entity.UserName);
    }
}