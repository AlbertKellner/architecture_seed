using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts;
using CustomExceptions;
using DataEntity.Model;
using DataTransferObject;
using MorseCode.ITask;
using Repository.Contracts;

namespace Core
{
    public class LaboratorioCore : IGenericCoreDto<LaboratorioDto, LaboratorioEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<LaboratorioEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public LaboratorioCore(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepositoryAsync<LaboratorioEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<LaboratorioEntity>> AllAsync() => (await _repository.GetListAsync()).Items;

        public async Task<LaboratorioEntity> GetByIdAsync(int id) => await _repository.SingleAsync(e => e.Id == id);

        //public IEnumerable<LaboratorioEntity> GetAsync(int userId) => _repository.GetList(
        //    include: s => s.Include(e => e.Farmacias).Include(e => e.Medicos)).Items;

        //public LaboratorioEntity GetAsync(int userId, int id) => _repository.Single(e => e.Id == id,
        //    include: s => s.Include(e => e.Farmacias).Include(e => e.Medicos));

        public async Task<LaboratorioEntity> InsertAsync(LaboratorioDto entityDto)
        {
            var entity = _mapper.Map<LaboratorioDto, LaboratorioEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var isEntityExists = (await _repository.SingleAsync(o => o.Nome == entity.Nome)).Id > 0;

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            //entity.UsuarioEntityId = userId;

            await _repository.AddAsync(entity);

            _unitOfWork.SaveChanges();

            return entity;
        }

        public async Task<LaboratorioEntity> UpdateAsync(LaboratorioDto entityDto)
        {
            var isEntityExists = (await GetByIdAsync(entityDto.Id)).Id > 0;

            if (!isEntityExists)
                return null;

            var entity = _mapper.Map<LaboratorioDto, LaboratorioEntity>(entityDto);
            //entity.UsuarioEntityId = userId;

            await _repository.UpdateAsync(entity);
            _unitOfWork.SaveChanges();

            return entity;
        }

        public Task DeleteAsync(LaboratorioDto entityDto)
        {
            return Task.CompletedTask;
            //_repository.Delete(entityDto.Id);
            //_unitOfWork.SaveChanges();
        }
    }
}