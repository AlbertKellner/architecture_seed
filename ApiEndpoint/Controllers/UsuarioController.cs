namespace ApiEndpoint.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Contracts;
    using DataEntity.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Operations;
    using Provider.Contracts;
    using ViewModels.Request;
    using ViewModels.Response;

    //[Authorize(Policy = "ApiUser")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuarioController : Controller, IController<UsuarioRequestModel, UsuarioResponseModel>
    {
        private readonly ControllerOperations<UsuarioEntity, UsuarioRequestModel, UsuarioResponseModel> _controllerOperations;

        public UsuarioController(IGenericProvider<UsuarioEntity> genericProvider, IMapper mapper, IHttpContextAccessor httpContextAccessor) =>
            _controllerOperations = new ControllerOperations<UsuarioEntity, UsuarioRequestModel, UsuarioResponseModel>(genericProvider, mapper);

        public ApiResponse<List<UsuarioResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId) => _controllerOperations.GetAll(headerUserId);

        [HttpGet("GetById/{id}")]
        public ApiResponse<UsuarioResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int id) => _controllerOperations.Get(headerUserId, id);

        [HttpPost]
        public ApiResponse<UsuarioResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, [FromBody] UsuarioRequestModel requestModel) =>
            _controllerOperations.Insert(headerUserId, requestModel);

        [HttpPut]
        public ApiResponse<UsuarioResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, [FromBody] UsuarioRequestModel requestModel) =>
            _controllerOperations.Update(headerUserId, requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, [FromBody] UsuarioRequestModel requestModel) =>
            _controllerOperations.Delete(headerUserId, requestModel);

        [HttpGet("GetByIdentity/{identityId}")]
        public ApiResponse<UsuarioResponseModel> GetByIdentity(string identityId) => _controllerOperations.GetByIdentity(identityId);
    }
}