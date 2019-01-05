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
    public class MedicoController : Controller, IController<MedicoRequestModel, MedicoResponseModel>
    {
        private readonly ControllerOperationsDto<MedicoDto, MedicoEntity, MedicoRequestModel, MedicoResponseModel> _controllerOperationsDto;

        public MedicoController(IGenericProviderDto<MedicoDto, MedicoEntity> provider, IMapper mapper) =>
            _controllerOperationsDto = new ControllerOperationsDto<MedicoDto, MedicoEntity, MedicoRequestModel, MedicoResponseModel>(provider, mapper);

        [HttpGet]
        public ApiResponse<List<MedicoResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId) => _controllerOperationsDto.GetAll(headerUserId);

        [HttpGet("{id}")]
        public ApiResponse<MedicoResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int id) => _controllerOperationsDto.Get(headerUserId, id);

        [HttpPost]
        public ApiResponse<MedicoResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, [FromBody] MedicoRequestModel requestModel) =>
            _controllerOperationsDto.Insert(headerUserId, requestModel);

        [HttpPut]
        public ApiResponse<MedicoResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, [FromBody] MedicoRequestModel requestModel) =>
            _controllerOperationsDto.Update(headerUserId, requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, [FromBody] MedicoRequestModel requestModel) =>
            _controllerOperationsDto.Delete(headerUserId, requestModel);
    }
}