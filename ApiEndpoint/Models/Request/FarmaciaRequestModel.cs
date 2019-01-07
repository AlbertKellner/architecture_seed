using System.Reflection;

namespace ApiEndpoint.ViewModels.Request
{
    using AutoMapper;
    using DataTransferObject;

    public class FarmaciaRequestModel : BaseRequestModel
    {
        public string Nome { get; set; }
        //public FarmaciaDto ToDto(IMapper mapper) => mapper.Map<FarmaciaRequestModel, FarmaciaDto>(this);
        //public T ToDto<T>(IMapper mapper) => mapper.Map<FarmaciaRequestModel, T>(this);
        //public T ToDto2<T>(IMapper mapper) => mapper.Map<T>(this);
    }
}