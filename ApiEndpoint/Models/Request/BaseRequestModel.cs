namespace ApiEndpoint.ViewModels.Request
{
    using AutoMapper;

    public class BaseRequestModel
    {
        public int Id;

        public T ToDto2<T>(IMapper mapper) => mapper.Map<T>(this);
    }
}