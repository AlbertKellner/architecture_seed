namespace Provider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using CustomExceptions;
    using DataEntity.Model;
    using DataTransferObject;
    using Repository.Contracts;

    public class MedicoProvider : IGenericProviderDto<MedicoDto, TodoListEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TodoListEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UsuarioEntity> _usuarioRepository;

        public MedicoProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TodoListEntity>();
            _usuarioRepository = _unitOfWork.GetRepository<UsuarioEntity>();
            _mapper = mapper;
        }

        public IEnumerable<TodoListEntity> All(int userId) => _repository.GetList().Items;

        public void Delete(int userId, MedicoDto entity)
        {
            _repository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }

        public TodoListEntity GetById(int userId, int id) => _repository.Single(e => e.Id == id);

        public TodoListEntity Insert(int userId, MedicoDto entityDto)
        {
            var entity = _mapper.Map<MedicoDto, TodoListEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var isEntityExists = _repository.Single(o => o.Nome == entity.Nome && o.UsuarioEntityId == userId)?.Id > 0;

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            entity.UsuarioEntityId = userId;

            _repository.Add(entity);
            
            return entity;
        }

        public TodoListEntity Update(int userId, MedicoDto entityDto)
        {
            var isEntityExists = GetById(userId, entityDto.Id)?.Id > 0;

            if (!isEntityExists)
                return null;

            var entity = _mapper.Map<MedicoDto, TodoListEntity>(entityDto);
            entity.UsuarioEntityId = userId;

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

            return entity;
        }
    }
}