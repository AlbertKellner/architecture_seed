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
    public class LaboratorioController : Controller, IController<LaboratorioRequestModel, LaboratorioResponseModel>
    {
        private readonly ControllerOperationsDto<LaboratorioDto, LaboratorioEntity, LaboratorioRequestModel, LaboratorioResponseModel> _controllerOperationsDto;

        public LaboratorioController(IGenericProviderDto<LaboratorioDto, LaboratorioEntity> providerDto, IMapper mapper) =>
            _controllerOperationsDto = new ControllerOperationsDto<LaboratorioDto, LaboratorioEntity, LaboratorioRequestModel, LaboratorioResponseModel>(providerDto, mapper);

        [HttpGet]
        public ApiResponse<List<LaboratorioResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId) => _controllerOperationsDto.GetAll(headerUserId);

        [HttpGet("{laboratorioId}")]
        public ApiResponse<LaboratorioResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int laboratorioId) =>
            _controllerOperationsDto.Get(headerUserId, laboratorioId);

        [HttpPost]
        public ApiResponse<LaboratorioResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, [FromBody] LaboratorioRequestModel requestModel) =>
            _controllerOperationsDto.Insert(headerUserId, requestModel);

        [HttpPut]
        public ApiResponse<LaboratorioResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, [FromBody] LaboratorioRequestModel requestModel) =>
            _controllerOperationsDto.Update(headerUserId, requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, [FromBody] LaboratorioRequestModel requestModel) =>
            _controllerOperationsDto.Delete(headerUserId, requestModel);
    }
}