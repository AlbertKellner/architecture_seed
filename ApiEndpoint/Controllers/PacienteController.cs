namespace ApiEndpoint.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Contracts;
    using DataEntity.Model;
    using DataTransferObject;
    using Microsoft.AspNetCore.Mvc;
    using Operations;
    using Provider.Contracts;
    using ViewModels.Request;
    using ViewModels.Response;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PacienteController : Controller, IController<PacienteRequestModel, PacienteResponseModel>
    {
        private readonly ControllerOperationsDto<PacienteDto, PacienteEntity, PacienteRequestModel, PacienteResponseModel> _controllerOperationsDto;

        public PacienteController(IGenericProviderDto<PacienteDto, PacienteEntity> provider, IMapper mapper) =>
            _controllerOperationsDto = new ControllerOperationsDto<PacienteDto, PacienteEntity, PacienteRequestModel, PacienteResponseModel>(provider, mapper);

        [HttpGet]
        public ApiResponse<List<PacienteResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId) => _controllerOperationsDto.GetAll(headerUserId);

        [HttpGet("{id}")]
        public ApiResponse<PacienteResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int id) => _controllerOperationsDto.Get(headerUserId, id);

        [HttpPost]
        public ApiResponse<PacienteResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, [FromBody] PacienteRequestModel requestModel) =>
            _controllerOperationsDto.Insert(headerUserId, requestModel);

        [HttpPut]
        public ApiResponse<PacienteResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, [FromBody] PacienteRequestModel requestModel) =>
            _controllerOperationsDto.Update(headerUserId, requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, [FromBody] PacienteRequestModel requestModel) =>
            _controllerOperationsDto.Delete(headerUserId, requestModel);
    }
}