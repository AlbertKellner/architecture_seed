using ApiEndpoint.Models.Request;
using ApiEndpoint.Models.Response;
using Core.Contracts;

namespace ApiEndpoint.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Contracts;
    using DataEntity.Model;
    using DataTransferObject;
    using Microsoft.AspNetCore.Mvc;
    using Operations;

    [Produces("application/json")]
    [Route("api/Laboratorio")]
    public class LaboratorioFarmaciaController : Controller, IControllerParentChildren<FarmaciaRequestModel, FarmaciaResponseModel>
    {
        private readonly ControllerParentChildrenOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel> _controllerParentChildrenOperationsDto;

        public LaboratorioFarmaciaController(IParentChildrenCoreDto<FarmaciaDto, FarmaciaEntity> coreDto, IMapper mapper) => _controllerParentChildrenOperationsDto =
                new ControllerParentChildrenOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel>(coreDto, mapper);

        [HttpGet("{laboratorioId}/Farmacia")]
        public ApiResponse<List<FarmaciaResponseModel>> GetAll(int laboratorioId) => _controllerParentChildrenOperationsDto.GetAll(laboratorioId);

        [HttpGet("{laboratorioId}/Farmacia/{farmaciaId}")]
        public ApiResponse<FarmaciaResponseModel> Get(int laboratorioId, int farmaciaId) => _controllerParentChildrenOperationsDto.Get(laboratorioId, farmaciaId);

        [HttpPost("{laboratorioId}/Farmacia")]
        public ApiResponse<FarmaciaResponseModel> Insert(int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) =>
            _controllerParentChildrenOperationsDto.Insert(laboratorioId, requestModel);

        [HttpPut("{laboratorioId}/Farmacia")]
        public ApiResponse<FarmaciaResponseModel> Update(int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) =>
            _controllerParentChildrenOperationsDto.Update(laboratorioId, requestModel);

        [HttpDelete("{laboratorioId}/Farmacia")]
        public ApiResponse Delete(int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) => _controllerParentChildrenOperationsDto.Delete(laboratorioId, requestModel);
    }
}