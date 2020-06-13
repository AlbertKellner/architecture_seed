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
        public async Task<ApiResponse<List<FarmaciaResponseModel>>> GetAll(int laboratorioId) => await _controllerParentChildrenOperationsDto.GetAllAsync(laboratorioId);

        [HttpGet("{laboratorioId}/Farmacia/{farmaciaId}")]
        public async Task<ApiResponse<FarmaciaResponseModel>> Get(int laboratorioId, int farmaciaId) => await _controllerParentChildrenOperationsDto.GetAsync(laboratorioId, farmaciaId);

        [HttpPost("{laboratorioId}/Farmacia")]
        public async Task<ApiResponse<FarmaciaResponseModel>> Insert(int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) =>
            await _controllerParentChildrenOperationsDto.InsertAsync(laboratorioId, requestModel);

        [HttpPut("{laboratorioId}/Farmacia")]
        public async Task<ApiResponse<FarmaciaResponseModel>> Update(int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) =>
            await _controllerParentChildrenOperationsDto.UpdateAsync(laboratorioId, requestModel);

        [HttpDelete("{laboratorioId}/Farmacia")]
        public async Task<ApiResponse> Delete(int laboratorioId, [FromBody] FarmaciaRequestModel requestModel) => await _controllerParentChildrenOperationsDto.DeleteAsync(laboratorioId, requestModel);
    }
}