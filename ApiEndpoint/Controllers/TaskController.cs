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
        public ApiResponse<List<TaskResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId) => _controllerOperationsDto.GetAll(headerUserId);

        [HttpGet("{id}")]
        public ApiResponse<TaskResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int id) => _controllerOperationsDto.Get(headerUserId, id);

        [HttpPost]
        public ApiResponse<TaskResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TaskRequestModel requestModel) =>
            _controllerOperationsDto.Insert(headerUserId, requestModel);

        [HttpPut]
        public ApiResponse<TaskResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TaskRequestModel requestModel) =>
            _controllerOperationsDto.Update(headerUserId, requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TaskRequestModel requestModel) =>
            _controllerOperationsDto.Delete(headerUserId, requestModel);
    }
}