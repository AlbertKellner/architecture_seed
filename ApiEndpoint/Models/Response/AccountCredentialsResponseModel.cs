namespace ApiEndpoint.Models.Response
{
    public class AccountCredentialsResponseModel
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public string AuthToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}