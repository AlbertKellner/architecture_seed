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
    public class LaboratorioProvider : IGenericProviderDto<LaboratorioDto, LaboratorioEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<LaboratorioEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public LaboratorioProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<LaboratorioEntity>();
            _mapper = mapper;
        }

        public IEnumerable<LaboratorioEntity> All() => _repository.GetList().Items;

        public LaboratorioEntity GetById(int id) => _repository.Single(e => e.Id == id);

        //public IEnumerable<LaboratorioEntity> Get(int userId) => _repository.GetList(
        //    include: s => s.Include(e => e.Farmacias).Include(e => e.Medicos)).Items;

        //public LaboratorioEntity Get(int userId, int id) => _repository.Single(e => e.Id == id,
        //    include: s => s.Include(e => e.Farmacias).Include(e => e.Medicos));

        public LaboratorioEntity Insert(LaboratorioDto entityDto)
        {
            var entity = _mapper.Map<LaboratorioDto, LaboratorioEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var isEntityExists = _repository.Single(o => o.Nome == entity.Nome)?.Id > 0;
                                     
            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            //entity.UsuarioEntityId = userId;

            _repository.Add(entity);
            
            _unitOfWork.SaveChanges();

            return entity;
        }

        public LaboratorioEntity Update(LaboratorioDto entityDto)
        {
            var isEntityExists = GetById(entityDto.Id)?.Id > 0;

            if (!isEntityExists)
                return null;

            var entity = _mapper.Map<LaboratorioDto, LaboratorioEntity>(entityDto);
            //entity.UsuarioEntityId = userId;

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

            return entity;
        }

        public void Delete(LaboratorioDto entityDto)
        {
            _repository.Delete(entityDto.Id);
            _unitOfWork.SaveChanges();
        }
    }
}