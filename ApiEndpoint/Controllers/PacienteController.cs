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
        public ApiResponse<List<PacienteResponseModel>> GetAll() => _controllerOperationsDto.GetAll();

        [HttpGet("{id}")]
        public ApiResponse<PacienteResponseModel> Get(int id) => _controllerOperationsDto.Get(id);

        [HttpPost]
        public ApiResponse<PacienteResponseModel> Insert([FromBody] PacienteRequestModel requestModel) =>
            _controllerOperationsDto.Insert(requestModel);

        [HttpPut]
        public ApiResponse<PacienteResponseModel> Update([FromBody] PacienteRequestModel requestModel) =>
            _controllerOperationsDto.Update(requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromBody] PacienteRequestModel requestModel) =>
            _controllerOperationsDto.Delete(requestModel);
    }
}