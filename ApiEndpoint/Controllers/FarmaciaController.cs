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
    public class FarmaciaController : Controller, IController<FarmaciaRequestModel, FarmaciaResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly ControllerOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel> _controllerOperationsDto;

        public FarmaciaController(IGenericProviderDto<FarmaciaDto, FarmaciaEntity> provider, IMapper mapper)
        {
            _mapper = mapper;
            _controllerOperationsDto =
                new ControllerOperationsDto<FarmaciaDto, FarmaciaEntity, FarmaciaRequestModel, FarmaciaResponseModel>(
                    provider, mapper);
        }

        [HttpGet]
        public ApiResponse<List<FarmaciaResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId)
        {
            var test = new FarmaciaRequestModel()
            {
                Id = 1,
                Nome = "teste"
            };

            //var test2 = test.ToDto(_mapper);
            //var test3 = test.ToDto<FarmaciaDto>(_mapper);
            var test4 = test.ToDto2<FarmaciaDto>(_mapper);

            return _controllerOperationsDto.GetAll(headerUserId);
        }

        [HttpGet("{id}")]
        public ApiResponse<FarmaciaResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int id) => _controllerOperationsDto.Get(headerUserId, id);

        [HttpPost]
        public ApiResponse<FarmaciaResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, [FromBody] FarmaciaRequestModel requestModel) =>
            _controllerOperationsDto.Insert(headerUserId, requestModel);

        [HttpPut]
        public ApiResponse<FarmaciaResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, [FromBody] FarmaciaRequestModel requestModel) =>
            _controllerOperationsDto.Update(headerUserId, requestModel);

        [HttpDelete]
        public ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, [FromBody] FarmaciaRequestModel requestModel) =>
            _controllerOperationsDto.Delete(headerUserId, requestModel);
    }
}