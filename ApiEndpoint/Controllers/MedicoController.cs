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
        public ApiResponse<List<MedicoResponseModel>> GetAll() => _controllerOperationsDto.GetAll();

        [HttpGet("{id}")]
        public ApiResponse<MedicoResponseModel> Get(int id) => _controllerOperationsDto.Get(id);

        [HttpPost]
        public ApiResponse<MedicoResponseModel> Insert([FromBody] MedicoRequestModel requestModel) =>
            _controllerOperationsDto.Insert(requestModel);

        [HttpPut]
        public ApiResponse<MedicoResponseModel> Update([FromBody] MedicoRequestModel requestModel) =>
            _controllerOperationsDto.Update(requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromBody] MedicoRequestModel requestModel) =>
            _controllerOperationsDto.Delete(requestModel);
    }
}