namespace Provider.Contracts
{
    using System.Threading.Tasks;
    using DataEntity.Model;

    public interface IAuthenticationProvider
    {
        bool AccountRecovery(UsuarioEntity email);
        Task<bool> RegisterAccount(AppUser appUser, string password);
    }
}