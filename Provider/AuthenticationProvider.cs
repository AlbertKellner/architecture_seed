namespace Service
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using CustomExceptions;
    using DataEntity.Model;
    using Microsoft.AspNetCore.Identity;
    using Provider.Contracts;
    using Repository.Contracts;

    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationProvider(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Obsolete]
        public bool AccountRecovery(UsuarioEntity entity)
        {
            Thread.Sleep(2000);
            return true;
            //_unitOfWork.GetRepository<UsuarioEntity>().Single(e => e.UserName == entity.UserName) != null; // TODO: Implementar disparo de email
        }


        public async Task<bool> RegisterAccount(AppUser requestIdentity, string password)
        {
            var identityResult = await _userManager.CreateAsync(requestIdentity, password);

            if (!identityResult.Succeeded)
            {
                var erros = new StringBuilder();

                foreach (var identityResultError in identityResult.Errors)
                    erros.Append(identityResultError.Description + " - ");

                throw new Exception(erros.ToString());
            }

            var usuarioEntity = new UsuarioEntity {IdentityId = requestIdentity.Id};

            if (!usuarioEntity.IsValid())
                throw new EntityValidationCustomException();

            var repo = _unitOfWork.GetRepository<UsuarioEntity>();

            repo.Add(usuarioEntity);
            _unitOfWork.SaveChanges();

            return true;
        }
    }
}