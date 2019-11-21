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

    public class MedicoProvider : IGenericProviderDto<MedicoDto, MedicoEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MedicoEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UsuarioEntity> _usuarioRepository;

        public MedicoProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<MedicoEntity>();
            _usuarioRepository = _unitOfWork.GetRepository<UsuarioEntity>();
            _mapper = mapper;
        }

        public IEnumerable<MedicoEntity> All() => _repository.GetList().Items;

        public void Delete(MedicoDto entity)
        {
            _repository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }

        public MedicoEntity GetById(int id) => _repository.Single(e => e.Id == id);

        public MedicoEntity Insert(MedicoDto entityDto)
        {
            var entity = _mapper.Map<MedicoDto, MedicoEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var isEntityExists = _repository.Single(o => o.Nome == entity.Nome)?.Id > 0;

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            //entity.UsuarioEntityId = userId;

            _repository.Add(entity);
            
            return entity;
        }

        public MedicoEntity Update(MedicoDto entityDto)
        {
            var isEntityExists = GetById(entityDto.Id)?.Id > 0;

            if (!isEntityExists)
                return null;

            var entity = _mapper.Map<MedicoDto, MedicoEntity>(entityDto);
            //entity.UsuarioEntityId = userId;

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

            return entity;
        }
    }
}