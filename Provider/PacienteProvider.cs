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

    public class PacienteProvider : IGenericProviderDto<PacienteDto, PacienteEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PacienteEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UsuarioEntity> _usuarioRepository;

        public PacienteProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<PacienteEntity>();
            _usuarioRepository = _unitOfWork.GetRepository<UsuarioEntity>();
            _mapper = mapper;
        }

        public IEnumerable<PacienteEntity> All() => _repository.GetList().Items;

        public void Delete(PacienteDto entityDto)
        {
            _repository.Delete(entityDto.Id);
            _unitOfWork.SaveChanges();
        }

        public PacienteEntity GetById(int id) => _repository.Single(e => e.Id == id);

        public PacienteEntity Insert(PacienteDto entityDto)
        {
            var entity = _mapper.Map<PacienteDto, PacienteEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var isEntityExists = _repository.Single(o => o.Nome == entity.Nome)?.Id > 0;

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            //entity.UsuarioEntityId = userId;

            _repository.Add(entity);

            return entity;
        }

        public PacienteEntity Update(PacienteDto entityDto)
        {
            var isEntityExists = GetById(entityDto.Id)?.Id > 0;

            if (!isEntityExists)
                return null;

            var entity = _mapper.Map<PacienteDto, PacienteEntity>(entityDto);
            //entity.UsuarioEntityId = userId;

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

            return entity;
        }
    }
}