namespace ApiEndpoint.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Auth;
    using AutoMapper;
    using Bases;
    using CustomExceptions;
    using DataEntity.Model;
    using Helpers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Provider.Contracts;
    using ViewModels.Request;
    using ViewModels.Response;

    [Produces("application/json")]
    [Route("api/Authentication/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGenericProvider<UsuarioEntity> _usuarioProvider;

        public AuthenticationController(IAuthenticationProvider authenticationProvider, IMapper mapper, UserManager<AppUser> userManager, IJwtFactory jwtFactory,
                                        IOptions<JwtIssuerOptions> jwtOptions, IGenericProvider<UsuarioEntity> usuarioProvider)
        {
            _authenticationProvider = authenticationProvider;
            _mapper = mapper;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _usuarioProvider = usuarioProvider;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<ApiResponse<AccountCredentialsResponseModel>> Login([FromBody] AccountCredentialsRequestModel credentials)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult(BaseResponse.ResponseNoContent((AccountCredentialsResponseModel) null));

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);

            if (identity == null)
                return await Task.FromResult(BaseResponse.ResponseNotFound((AccountCredentialsResponseModel) null));

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions);

            jwt.UserId = _usuarioProvider.GetByIdentity(jwt.Id).Id;

            return await Task.FromResult(BaseResponse.ResponseOk(jwt));
        }

        [HttpPost]
        public ApiResponse<bool> AccountRecovery([FromBody] AccountRecoveryRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<AccountRecoveryRequestModel, UsuarioEntity>(requestModel);
            var isRecovered = false;

            try
            {
                isRecovered = _authenticationProvider.AccountRecovery(requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(isRecovered, exception);
            }

            return BaseResponse.ResponseOk(isRecovered);
        }

        [HttpPost]
        public async Task<ApiResponse<bool>> RegisterAccount([FromBody] AccountRegisterRequestModel requestModel)
        {
            var userIdentity = _mapper.Map<AccountRegisterRequestModel, AppUser>(requestModel);

            bool responseEntity;

            try
            {
                responseEntity = await _authenticationProvider.RegisterAccount(userIdentity, requestModel.Password);
            }
            catch (EntityValidationCustomException exception)
            {
                return BaseResponse.ResponseEntityValidation(false, exception);
            }
            catch (AlreadyExistsCustomException exception)
            {
                return BaseResponse.ResponseAlreadyExists(false, exception);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(false, exception);
            }

            //var responseModel = _mapper.Map<UsuarioEntity, UsuarioResponseModel>(responseEntity);

            return BaseResponse.ResponseCreated(responseEntity);
        }


        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null)
                return await Task.FromResult<ClaimsIdentity>(null);

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}