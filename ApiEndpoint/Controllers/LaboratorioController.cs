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

        public LaboratorioController(IGenericProviderDto<LaboratorioDto, LaboratorioEntity> providerDto, IMapper mapper) =>
            _controllerOperationsDto = new ControllerOperationsDto<LaboratorioDto, LaboratorioEntity, LaboratorioRequestModel, LaboratorioResponseModel>(providerDto, mapper);

        [HttpGet]
        public ApiResponse<List<LaboratorioResponseModel>> GetAll() => _controllerOperationsDto.GetAll();

        [HttpGet("{laboratorioId}")]
        public ApiResponse<LaboratorioResponseModel> Get(int laboratorioId) =>
            _controllerOperationsDto.Get(laboratorioId);

        [HttpPost]
        public ApiResponse<LaboratorioResponseModel> Insert([FromBody] LaboratorioRequestModel requestModel) =>
            _controllerOperationsDto.Insert(requestModel);

        [HttpPut]
        public ApiResponse<LaboratorioResponseModel> Update([FromBody] LaboratorioRequestModel requestModel) =>
            _controllerOperationsDto.Update(requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromBody] LaboratorioRequestModel requestModel) =>
            _controllerOperationsDto.Delete(requestModel);
    }
}