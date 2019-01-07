namespace ApiEndpoint.Controllers
{
    using Models.Response;
    using DataEntity;
    using System.Collections.Generic;
    using AutoMapper;
    using Contracts;
    using DataTransferObject;
    using Microsoft.AspNetCore.Mvc;
    using Operations;
    using Provider.Contracts;
    using ViewModels.Request;
    using ViewModels.Response;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TaskController : Controller, IController<TaskRequestModel, TaskResponseModel>
    {
        private readonly ControllerOperationsDto<TaskDto, TaskEntity, TaskRequestModel, TaskResponseModel> _controllerOperationsDto;

        public TaskController(IGenericProviderDto<TaskDto, TaskEntity> provider, IMapper mapper) =>
            _controllerOperationsDto = new ControllerOperationsDto<TaskDto, TaskEntity, TaskRequestModel, TaskResponseModel>(provider, mapper);

        [HttpGet]
        public ApiResponse<List<TaskResponseModel>> GetAll() => _controllerOperationsDto.GetAll();

        [HttpGet("{id}")]
        public ApiResponse<TaskResponseModel> Get(int id) => _controllerOperationsDto.Get(id);

        [HttpPost]
        public ApiResponse<TaskResponseModel> Insert([FromBody] TaskRequestModel requestModel) =>
            _controllerOperationsDto.Insert(requestModel);

        [HttpPut]
        public ApiResponse<TaskResponseModel> Update([FromBody] TaskRequestModel requestModel) =>
            _controllerOperationsDto.Update(requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromBody] TaskRequestModel requestModel) =>
            _controllerOperationsDto.Delete(requestModel);
    }
}