namespace Provider.DataTransferObjectMapping
{
    using AutoMapper;
    using DataEntity;
    using DataEntity.Model;
    using DataTransferObject;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LaboratorioDto, LaboratorioEntity>(); // TODO: Fazer mapeamento manual de UserId
            CreateMap<FarmaciaDto, FarmaciaEntity>(); // TODO: Fazer mapeamento manual de UserId
            CreateMap<MedicoDto, MedicoEntity>(); // TODO: Fazer mapeamento manual de UserId
            CreateMap<PacienteDto, PacienteEntity>(); // TODO: Fazer mapeamento manual de UserId

            CreateMap<TaskDto, TaskEntity>(); // TODO: Fazer mapeamento manual de UserId
            CreateMap<TaskListDto, TaskListEntity>(); // TODO: Fazer mapeamento manual de UserId
        }
    }
}