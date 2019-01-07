namespace ApiEndpoint.ViewModels.Request
{
    using AutoMapper;

    public class BaseRequestModel
    {
        public int Id;

        public TDestiny MapTo<TDestiny>(IMapper mapper) => mapper.Map<TDestiny>(this);
    }
}