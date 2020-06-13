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
    public class FarmaciaController : Controller, IController<FarmaciaRequestModel, FarmaciaResponseModel>
    {
        private readonly ControllerOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel> _controllerOperationsDto;

        public FarmaciaController(IGenericCoreDto<FarmaciaDto, FarmaciaEntity> core, IMapper mapper) =>
            _controllerOperationsDto =
                new ControllerOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel>(
                    core, mapper);

        [HttpGet]
        public async Task<ApiResponse<List<FarmaciaResponseModel>>> Get() => await _controllerOperationsDto.GetAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<FarmaciaResponseModel>> Get(int id) => await _controllerOperationsDto.GetAsync(id);

        [HttpPost]
        public async Task<ApiResponse<FarmaciaResponseModel>> Insert([FromBody] FarmaciaRequestModel requestModel) =>
            await _controllerOperationsDto.InsertAsync(requestModel);

        [HttpPut]
        public async Task<ApiResponse<FarmaciaResponseModel>> Update([FromBody] FarmaciaRequestModel requestModel) =>
            await _controllerOperationsDto.UpdateAsync(requestModel);

        [HttpDelete]
        public async Task<ApiResponse> Delete([FromBody] FarmaciaRequestModel requestModel) =>
            await _controllerOperationsDto.DeleteAsync(requestModel);
    }
}