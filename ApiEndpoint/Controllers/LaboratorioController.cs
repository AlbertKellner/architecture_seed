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
    [Route("api/[controller]")]
    public class LaboratorioController : Controller, IController<LaboratorioRequestModel, LaboratorioResponseModel>
    {
        private readonly ControllerOperationsDto<LaboratorioDto, LaboratorioEntity, LaboratorioRequestModel, LaboratorioResponseModel> _controllerOperationsDto;

        public LaboratorioController(IGenericCoreDto<LaboratorioDto, LaboratorioEntity> coreDto, IMapper mapper) =>
            _controllerOperationsDto = new ControllerOperationsDto<LaboratorioDto, LaboratorioEntity, LaboratorioRequestModel, LaboratorioResponseModel>(coreDto, mapper);

        [HttpGet]
        public async Task<ApiResponse<List<LaboratorioResponseModel>>> Get() => await _controllerOperationsDto.GetAsync();

        [HttpGet("{laboratorioId}")]
        public async Task<ApiResponse<LaboratorioResponseModel>> Get(int laboratorioId) =>
            await _controllerOperationsDto.GetAsync(laboratorioId);

        [HttpPost]
        public async Task<ApiResponse<LaboratorioResponseModel>> Insert([FromBody] LaboratorioRequestModel requestModel) =>
            await _controllerOperationsDto.InsertAsync(requestModel);

        [HttpPut]
        public async Task<ApiResponse<LaboratorioResponseModel>> Update([FromBody] LaboratorioRequestModel requestModel) =>
            await _controllerOperationsDto.UpdateAsync(requestModel);

        [HttpDelete]
        public async Task<ApiResponse> Delete([FromBody] LaboratorioRequestModel requestModel) =>
            await _controllerOperationsDto.DeleteAsync(requestModel);
    }
}