namespace ApiEndpoint.Controllers.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Response;

    public interface IController<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        ApiResponse<List<TResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId);

        ApiResponse<TResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int id);

        ApiResponse<TResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TRequestModel requestModel);

        ApiResponse<TResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TRequestModel requestModel);

        ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, [FromBody] TRequestModel requestModel);
    }
}