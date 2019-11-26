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

        public FarmaciaController(IGenericProviderDto<FarmaciaDto, FarmaciaEntity> provider, IMapper mapper) =>
            _controllerOperationsDto =
                new ControllerOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel>(
                    provider, mapper);

        [HttpGet]
        public ApiResponse<List<FarmaciaResponseModel>> Get() => _controllerOperationsDto.Get();

        [HttpGet("{id}")]
        public ApiResponse<FarmaciaResponseModel> Get(int id) => _controllerOperationsDto.Get(id);

        [HttpPost]
        public ApiResponse<FarmaciaResponseModel> Insert([FromBody] FarmaciaRequestModel requestModel) =>
            _controllerOperationsDto.Insert(requestModel);

        [HttpPut]
        public ApiResponse<FarmaciaResponseModel> Update([FromBody] FarmaciaRequestModel requestModel) =>
            _controllerOperationsDto.Update(requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromBody] FarmaciaRequestModel requestModel) =>
            _controllerOperationsDto.Delete(requestModel);
    }
}