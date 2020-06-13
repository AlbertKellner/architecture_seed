using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IRepositoryAsync<FarmaciaEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public FarmaciaCore(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepositoryAsync<FarmaciaEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<FarmaciaEntity>> AllAsync() => (await _repository.GetListAsync()).Items;
        //public IEnumerable<FarmaciaEntity> GetAsync() => _repository.GetList(e => e.UsuarioEntityId == userId, null, null, 0, disableTracking: true).Items;

        public async Task<FarmaciaEntity> GetByIdAsync(int id) => await _repository.SingleAsync(e => e.Id == id, disableTracking: true);

        public async Task<FarmaciaEntity> InsertAsync(FarmaciaDto entityDto)
        {
            var entity = _mapper.Map<FarmaciaDto, FarmaciaEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var farmaciaEntity = await _repository.SingleAsync(o => o.Nome == entity.Nome);

            var isEntityExists = farmaciaEntity?.Id > 0;

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            //entity.UsuarioEntityId = userId;

            await _repository.AddAsync(entity);

            _unitOfWork.SaveChanges();
            _unitOfWork.DetachEntry(entity);

            return entity;
        }

        public async Task<FarmaciaEntity> UpdateAsync(FarmaciaDto entityDto)
        {
            var exists = (await GetByIdAsync(entityDto.Id))?.Id > 0;

            if (!exists)
                throw new NotFoundCustomException();

            var entity = _mapper.Map<FarmaciaDto, FarmaciaEntity>(entityDto);
            //entity.UsuarioEntityId = userId;

            await _repository.UpdateAsync(entity);
            _unitOfWork.SaveChanges();

            return await GetByIdAsync(entityDto.Id);
        }

        public async Task DeleteAsync(FarmaciaDto entity)
        {
            throw new NotImplementedException();
            //_repository.Delete(entity.Id);
            //_unitOfWork.SaveChanges();
        }
    }
}