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
    public class TaskListController : Controller, IController<TaskListRequestModel, TaskListResponseModel>
    {
        private readonly ControllerOperationsDto<TaskListDto, TaskListEntity, TaskListRequestModel, TaskListResponseModel> _controllerOperationsDto;

        public TaskListController(IGenericProviderDto<TaskListDto, TaskListEntity> provider, IMapper mapper) =>
            _controllerOperationsDto = new ControllerOperationsDto<TaskListDto, TaskListEntity, TaskListRequestModel, TaskListResponseModel>(provider, mapper);

        [HttpGet]
        public ApiResponse<List<TaskListResponseModel>> GetAll() => _controllerOperationsDto.GetAll();

        [HttpGet("{id}")]
        public ApiResponse<TaskListResponseModel> Get(int id) => _controllerOperationsDto.Get(id);

        [HttpPost]
        public ApiResponse<TaskListResponseModel> Insert([FromBody] TaskListRequestModel requestModel) =>
            _controllerOperationsDto.Insert(requestModel);

        [HttpPut]
        public ApiResponse<TaskListResponseModel> Update([FromBody] TaskListRequestModel requestModel) =>
            _controllerOperationsDto.Update(requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromBody] TaskListRequestModel requestModel) =>
            _controllerOperationsDto.Delete(requestModel);
    }
}