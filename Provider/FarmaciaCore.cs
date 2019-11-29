using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Core.Contracts;
using CustomExceptions;
using DataEntity.Model;
using DataTransferObject;
using Repository.Contracts;

namespace Core
{
    public class FarmaciaCore : IGenericCoreDto<FarmaciaDto, FarmaciaEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<FarmaciaEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public FarmaciaCore(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<FarmaciaEntity>();
            _mapper = mapper;
        }

        public IEnumerable<FarmaciaEntity> All() => _repository.GetList().Items;
        //public IEnumerable<FarmaciaEntity> Get() => _repository.GetList(e => e.UsuarioEntityId == userId, null, null, 0, disableTracking: true).Items;

        public FarmaciaEntity GetById(int id) => _repository.Single(e => e.Id == id, disableTracking: true);

        public FarmaciaEntity Insert(FarmaciaDto entityDto)
        {
            var entity = _mapper.Map<FarmaciaDto, FarmaciaEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var isEntityExists = _repository.Single(o => o.Nome == entity.Nome)?.Id > 0;

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            //entity.UsuarioEntityId = userId;

            _repository.Add(entity);
            _unitOfWork.SaveChanges();
            _unitOfWork.DetachEntry(entity);

            return entity;
        }

        public FarmaciaEntity Update(FarmaciaDto entityDto)
        {
            var exists = GetById(entityDto.Id)?.Id > 0;

            if (!exists)
                throw new NotFoundCustomException();

            var entity = _mapper.Map<FarmaciaDto, FarmaciaEntity>(entityDto);
            //entity.UsuarioEntityId = userId;

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

            return GetById(entityDto.Id);
        }

        public void Delete(FarmaciaDto entity)
        {
            _repository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }
    }
}