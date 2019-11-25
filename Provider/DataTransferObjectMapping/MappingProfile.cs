using AutoMapper;
using DataEntity.Model;
using DataTransferObject;

namespace Core.DataTransferObjectMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LaboratorioDto, LaboratorioEntity>(); // TODO: Fazer mapeamento manual de UserId
            CreateMap<FarmaciaDto, FarmaciaEntity>(); // TODO: Fazer mapeamento manual de UserId
        }
    }
}