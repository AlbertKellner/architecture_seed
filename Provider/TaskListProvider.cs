namespace Provider
{
    using DataEntity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using CustomExceptions;
    using DataTransferObject;
    using Repository.Contracts;

    public class TaskListProvider : IGenericProviderDto<TaskListDto, TaskListEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TaskListEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskListProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TaskListEntity>();
            _mapper = mapper;
        }

        public IEnumerable<TaskListEntity> All(int userId) => _repository.GetList(e => e.UsuarioEntityId == userId, null, null, 0, disableTracking: true).Items;

        public TaskListEntity GetById(int userId, int id) => _repository.Single(e => e.Id == id && e.UsuarioEntityId == userId, disableTracking: true);

        public TaskListEntity Insert(int userId, TaskListDto entityDto)
        {
            var entity = _mapper.Map<TaskListDto, TaskListEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var isEntityExists = _repository.Single(o => o.Name == entity.Name)?.Id > 0;

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            entity.UsuarioEntityId = userId;

            _repository.Add(entity);
            _unitOfWork.SaveChanges();
            _unitOfWork.DetachEntry(entity);

            return entity;
        }

        public TaskListEntity Update(int userId, TaskListDto entityDto)
        {
            var exists = GetById(userId, entityDto.Id)?.Id > 0;

            if (!exists)
                throw new NotFoundCustomException();

            var entity = _mapper.Map<TaskListDto, TaskListEntity>(entityDto);
            entity.UsuarioEntityId = userId;

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

            return GetById(userId, entityDto.Id);
        }

        public void Delete(int userId, TaskListDto entity)
        {
            _repository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }
    }
}