using ApiEndpoint.Models.Request;
using ApiEndpoint.Models.Response;
using Core.Contracts;

namespace ApiEndpoint.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Contracts;
    using DataEntity.Model;
    using Microsoft.AspNetCore.Mvc;
    using Operations;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuarioController : Controller, IController<UsuarioRequestModel, UsuarioResponseModel>
    {
        private readonly ControllerOperations<UsuarioEntity, UsuarioRequestModel, UsuarioResponseModel> _controllerOperations;

        public UsuarioController(IGenericProvider<UsuarioEntity> genericProvider, IMapper mapper) =>
            _controllerOperations = new ControllerOperations<UsuarioEntity, UsuarioRequestModel, UsuarioResponseModel>(genericProvider, mapper);

        public ApiResponse<List<UsuarioResponseModel>> Get() => _controllerOperations.Get();

        [HttpGet("{id}")]
        public ApiResponse<UsuarioResponseModel> Get(int id) => _controllerOperations.Get(id);

        [HttpPost]
        public ApiResponse<UsuarioResponseModel> Insert([FromBody] UsuarioRequestModel requestModel) => _controllerOperations.Insert(requestModel);

        [HttpPut]
        public ApiResponse<UsuarioResponseModel> Update([FromBody] UsuarioRequestModel requestModel) => _controllerOperations.Update(requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromBody] UsuarioRequestModel requestModel) => _controllerOperations.Delete(requestModel);
    }
}