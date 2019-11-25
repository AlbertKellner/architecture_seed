namespace ApiEndpoint.Models.Request
{
    //[Validator(typeof(CredentialsViewModelValidator))]
    public class AccountCredentialsRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}