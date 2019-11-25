using ApiEndpoint.Models.Request;
using ApiEndpoint.Models.Response;

namespace ApiEndpoint
{
    using AutoMapper;
    using AutoMapper.EquivalencyExpression;
    using DataEntity;
    using DataEntity.Model;
    using DataTransferObject;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountRecoveryRequestModel, UsuarioEntity>();
            CreateMap<AccountCredentialsRequestModel, UsuarioEntity>();

            CreateMap<UsuarioEntity, UsuarioResponseModel>().EqualityComparison((origin, destiny) => origin.Id == destiny.Id);

            CreateMap<FarmaciaDto, UsuarioResponseModel>().EqualityComparison((origin, destiny) => origin.Id == destiny.Id);

            CreateMap<LaboratorioRequestModel, LaboratorioDto>();
            CreateMap<FarmaciaRequestModel, FarmaciaDto>();
            
            CreateMap<LaboratorioEntity, LaboratorioResponseModel>();
            CreateMap<FarmaciaEntity, FarmaciaResponseModel>();
        }
    }
}