namespace ApiEndpoint
{
    using System.Collections.Generic;
    using AutoMapper;

    public static class ApiModelsExtensions
    {
        public static TDestiny MapTo<TOrigin, TDestiny>(this List<TOrigin> origin, IMapper mapper) => mapper.Map<TDestiny>(origin);
    }

}
