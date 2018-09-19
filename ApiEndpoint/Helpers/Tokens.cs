namespace ApiEndpoint.Helpers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Auth;
    using Models;
    using ViewModels.Response;

    public class Tokens
    {
        public static async Task<AccountCredentialsResponseModel> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions) =>
            new AccountCredentialsResponseModel
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtFactory.GenerateEncodedToken(userName, identity),
                ExpiresIn = (int) jwtOptions.ValidFor.TotalSeconds
            };
    }
}