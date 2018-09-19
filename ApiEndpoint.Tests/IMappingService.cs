namespace ApiEndpoint.Tests
{
    public interface IMappingService
    {
        TDest Map<TSrc, TDest>(TSrc source) where TDest : class;
    }
}
