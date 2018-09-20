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
        public ApiResponse<List<TaskListResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId) => _controllerOperationsDto.GetAll(headerUserId);

        [HttpGet("{id}")]
        public ApiResponse<TaskListResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int id) => _controllerOperationsDto.Get(headerUserId, id);

        [HttpPost]
        public ApiResponse<TaskListResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TaskListRequestModel requestModel) =>
            _controllerOperationsDto.Insert(headerUserId, requestModel);

        [HttpPut]
        public ApiResponse<TaskListResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TaskListRequestModel requestModel) =>
            _controllerOperationsDto.Update(headerUserId, requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TaskListRequestModel requestModel) =>
            _controllerOperationsDto.Delete(headerUserId, requestModel);
    }
}