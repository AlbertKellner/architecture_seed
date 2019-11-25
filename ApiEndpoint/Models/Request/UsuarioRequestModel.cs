namespace ApiEndpoint.Models.Request
{
    public class UsuarioRequestModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}