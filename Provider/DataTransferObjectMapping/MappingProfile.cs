namespace Provider.DataTransferObjectMapping
{
    using AutoMapper;
    using DataEntity.Model;
    using DataTransferObject;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LaboratorioDto, LaboratorioEntity>(); // TODO: Fazer mapeamento manual de UserId
            CreateMap<FarmaciaDto, FarmaciaEntity>(); // TODO: Fazer mapeamento manual de UserId
            CreateMap<MedicoDto, TodoListEntity>(); // TODO: Fazer mapeamento manual de UserId
            CreateMap<PacienteDto, PacienteEntity>(); // TODO: Fazer mapeamento manual de UserId
        }
    }
}