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
    [Route("api/Laboratorio")]
    public class LaboratorioFarmaciaController : Controller, IControllerParentChildren<FarmaciaRequestModel, FarmaciaResponseModel>
    {
        private readonly ControllerParentChildrenOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel> _controllerParentChildrenOperationsDto;

        public LaboratorioFarmaciaController(IParentChildrenProviderDto<FarmaciaDto, FarmaciaEntity> providerDto, IMapper mapper) =>
            _controllerParentChildrenOperationsDto =
                new ControllerParentChildrenOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel>(providerDto, mapper);

        [HttpGet("{laboratorioId}/Farmacia")]
        public ApiResponse<List<FarmaciaResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId, int laboratorioId) =>
            _controllerParentChildrenOperationsDto.GetAll(headerUserId, laboratorioId);

        [HttpGet("{laboratorioId}/Farmacia/{farmaciaId}")]
        public ApiResponse<FarmaciaResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int laboratorioId, int farmaciaId) =>
            _controllerParentChildrenOperationsDto.Get(headerUserId, laboratorioId, farmaciaId);

        [HttpPost("{laboratorioId}/Farmacia")]
        public ApiResponse<FarmaciaResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) =>
            _controllerParentChildrenOperationsDto.Insert(headerUserId, laboratorioId, requestModel);

        [HttpPut("{laboratorioId}/Farmacia")]
        public ApiResponse<FarmaciaResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) =>
            _controllerParentChildrenOperationsDto.Update(headerUserId, laboratorioId, requestModel);

        [HttpDelete("{laboratorioId}/Farmacia")]
        public ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) =>
            _controllerParentChildrenOperationsDto.Delete(headerUserId, laboratorioId, requestModel);
    }
}