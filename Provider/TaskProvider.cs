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

    public class TaskProvider : IGenericProviderDto<TaskDto, TaskEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TaskEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TaskEntity>();
            _mapper = mapper;
        }

        public IEnumerable<TaskEntity> All() => _repository.GetList().Items;
        //public IEnumerable<TaskEntity> All() => _repository.GetList(e => e.UsuarioEntityId == userId, null, null, 0, disableTracking: true).Items;

        public TaskEntity GetById(int id) => _repository.Single(e => e.Id == id, disableTracking: true);

        public TaskEntity Insert(TaskDto entityDto)
        {
            var entity = _mapper.Map<TaskDto, TaskEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var isEntityExists = _repository.Single(o => o.Description == entity.Description)?.Id > 0;

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            //entity.UsuarioEntityId = userId;

            _repository.Add(entity);
            _unitOfWork.SaveChanges();
            _unitOfWork.DetachEntry(entity);

            return entity;
        }

        public TaskEntity Update(TaskDto entityDto)
        {
            var exists = GetById(entityDto.Id)?.Id > 0;

            if (!exists)
                throw new NotFoundCustomException();

            var entity = _mapper.Map<TaskDto, TaskEntity>(entityDto);
            //entity.UsuarioEntityId = userId;

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

            return GetById(entityDto.Id);
        }

        public void Delete(TaskDto entity)
        {
            _repository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }
    }
}