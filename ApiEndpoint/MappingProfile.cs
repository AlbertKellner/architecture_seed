namespace ApiEndpoint
{
    using AutoMapper;
    using AutoMapper.EquivalencyExpression;
    using DataEntity;
    using DataEntity.Model;
    using DataTransferObject;
    using ViewModels.Request;
    using ViewModels.Response;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountRecoveryRequestModel, UsuarioEntity>();
            CreateMap<AccountRegisterRequestModel, AppUser>();
            CreateMap<AccountCredentialsRequestModel, UsuarioEntity>();

            //CreateMap<UsuarioEntity, UsuarioRequestModel>().EqualityComparison((origin, destiny) => origin.Id == destiny.Id);
            CreateMap<UsuarioEntity, UsuarioResponseModel>().EqualityComparison((origin, destiny) => origin.Id == destiny.Id);

            CreateMap<LaboratorioRequestModel, LaboratorioDto>();
            CreateMap<FarmaciaRequestModel, FarmaciaDto>();
            CreateMap<MedicoRequestModel, MedicoDto>();
            CreateMap<PacienteRequestModel, PacienteDto>();

            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskListEntity, TaskListDto>();
            
            CreateMap<LaboratorioEntity, LaboratorioResponseModel>();
            CreateMap<FarmaciaEntity, FarmaciaResponseModel>();
        }
    }
}