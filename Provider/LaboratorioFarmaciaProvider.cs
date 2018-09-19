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
    using DataEntity.Model.Relations;
    using DataTransferObject;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Repository.Contracts;

    public class LaboratorioFarmaciaProvider : IParentChildrenProviderDto<FarmaciaDto, FarmaciaEntity>
    {
        private readonly IRepository<LaboratorioEntity> _laboratorioRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UsuarioEntity> _usuarioRepository;

        public LaboratorioFarmaciaProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _laboratorioRepository = _unitOfWork.GetRepository<LaboratorioEntity>();
            _usuarioRepository = _unitOfWork.GetRepository<UsuarioEntity>();
            _mapper = mapper;
        }

        public IEnumerable<FarmaciaEntity> All(int userId, int parentId)
        {
            var relationLaboratorioFarmacias = _laboratorioRepository.Single(e => e.UsuarioEntityId == userId && e.Id == parentId,
                include: s => s.Include(e => e.Farmacias).ThenInclude(r => r.Farmacia)).Farmacias;

            var farmaciaEntities = new List<FarmaciaEntity>();
            foreach (var laboratorioFarmacia in relationLaboratorioFarmacias)
                farmaciaEntities.Append(laboratorioFarmacia.Farmacia);

            return farmaciaEntities;
        }

        public FarmaciaEntity GetById(int userId, int parentId, int childId)
        {
            var entity = _laboratorioRepository.Single(e => e.UsuarioEntityId == userId && e.Id == parentId,
                include: s => s.Include(e => e.Farmacias).ThenInclude(e => e.Farmacia));

            if (!entity.Farmacias.Any())
                return null;

            var farmacia = entity.Farmacias.Where(e => e.Farmacia.Id == childId).Select(e => e.Farmacia).ToList().First();

            return farmacia;
        }

        public FarmaciaEntity Insert(int userId, int parentId, FarmaciaDto entityDto)
        {
            var entity = _mapper.Map<FarmaciaDto, FarmaciaEntity>(entityDto);

            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErrors.First());

            var laboratorio = _laboratorioRepository.Single(e => e.UsuarioEntityId == userId && e.Id == parentId,
                include: s => s.Include(e => e.Farmacias).ThenInclude(r => r.Farmacia));

            var isEntityExists = laboratorio.Farmacias.Any(f => f.Farmacia.Nome == entity.Nome);

            if (isEntityExists)
                throw new AlreadyExistsCustomException();

            laboratorio.Farmacias.Add(new RelationLaboratorioFarmacia
            {
                Farmacia = entity
            });

            _unitOfWork.GetRepository<LaboratorioEntity>().Update(laboratorio);

            _unitOfWork.SaveChanges();

            return entity;
        }

        public FarmaciaEntity Update(int userId, int parentId, FarmaciaDto entityDto) => throw new NotImplementedException();

        public void Delete(int userId, int parentId, FarmaciaDto entityDto) => throw new NotImplementedException();

        //public IEnumerable<LaboratorioEntity> All(int userId) => _laboratorioRepository.GetList(
        //    include: s => s.Include(e => e.Farmacias).Include(e => e.Medicos)).Items;

        //public LaboratorioEntity GetById(int userId, int id) => _laboratorioRepository.Single(e => e.Id == id,
        //    include: s => s.Include(e => e.Farmacias).Include(e => e.Medicos));

        //public LaboratorioEntity Insert(int userId, LaboratorioDto entityDto)
        //{
        //    var entity = _mapper.Map<LaboratorioDto, LaboratorioEntity>(entityDto);

        //    if (!entity.IsValid())
        //        throw new ValidationException(entity.ValidationErrors.First());

        //    var isEntityExists = _usuarioRepository.Single(usuario =>
        //                             usuario.Id == userId
        //                             && usuario.Laboratorios.Any(laboratorio => laboratorio.Nome == entity.Nome)
        //                         )?.Id > 0;


        //    if (isEntityExists)
        //        throw new Exception("Ja cadastrado");

        //    var user = _usuarioRepository.Single(e => e.Id == userId);

        //    user.Laboratorios.Add(entity);

        //    _unitOfWork.GetRepository<UsuarioEntity>().Update(user);

        //    _unitOfWork.SaveChanges();

        //    return entity;
        //}

        //public LaboratorioEntity InsertFarmacia(int userId, LaboratorioDto entityDto)
        //{
        //    var entity = _mapper.Map<LaboratorioDto, LaboratorioEntity>(entityDto);

        //    if (!entity.IsValid())
        //        throw new ValidationException(entity.ValidationErrors.First());

        //    var isEntityExists = _usuarioRepository.Single(usuario =>
        //                             usuario.Id == userId
        //                             && usuario.Laboratorios.Any(laboratorio => laboratorio.Nome == entity.Nome)
        //                         )?.Id > 0;

        //    if (isEntityExists)
        //        throw new Exception("Ja cadastrado");

        //    var user = _usuarioRepository.Single(e => e.Id == userId);

        //    user.Laboratorios.Add(entity);

        //    _unitOfWork.GetRepository<UsuarioEntity>().Update(user);

        //    _unitOfWork.SaveChanges();

        //    return entity;
        //}

        //public LaboratorioEntity Update(int userId, LaboratorioDto entityDto)
        //{
        //    var isEntityExists = GetById(userId, entityDto.Id)?.Id > 0;

        //    if (!isEntityExists)
        //        return null;

        //    var entity = _mapper.Map<LaboratorioDto, LaboratorioEntity>(entityDto);
        //    entity.UsuarioEntityId = userId;

        //    _laboratorioRepository.Update(entity);
        //    _unitOfWork.SaveChanges();

        //    return entity;
        //}

        //public void Delete(int userId, LaboratorioDto entityDto)
        //{
        //    _laboratorioRepository.Delete(entityDto.Id);
        //    _unitOfWork.SaveChanges();
        //}
    }
}