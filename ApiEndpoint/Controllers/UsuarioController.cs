using System.Threading.Tasks;
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

        public UsuarioController(IGenericCore<UsuarioEntity> genericCore, IMapper mapper) =>
            _controllerOperations = new ControllerOperations<UsuarioEntity, UsuarioRequestModel, UsuarioResponseModel>(genericCore, mapper);

        public async Task<ApiResponse<List<UsuarioResponseModel>>> Get() => await _controllerOperations.GetAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<UsuarioResponseModel>> Get(int id) => await _controllerOperations.GetAsync(id);

        [HttpPost]
        public async Task<ApiResponse<UsuarioResponseModel>> Insert([FromBody] UsuarioRequestModel requestModel) => await _controllerOperations.InsertAsync(requestModel);

        [HttpPut]
        public async Task<ApiResponse<UsuarioResponseModel>> Update([FromBody] UsuarioRequestModel requestModel) => await _controllerOperations.UpdateAsync(requestModel);

        [HttpDelete]
        public async Task<ApiResponse> Delete([FromBody] UsuarioRequestModel requestModel) => await _controllerOperations.DeleteAsync(requestModel);
    }
}