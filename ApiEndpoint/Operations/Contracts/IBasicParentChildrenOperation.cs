namespace ApiEndpoint.Operations.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Response;

    public interface IBasicParentChildrenOperation<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        ApiResponse<List<TResponseModel>> GetAll([FromHeader(Name = "UserId")] string headerUserId, int parentId);

        ApiResponse<TResponseModel> Get([FromHeader(Name = "UserId")] string headerUserId, int parentId, int childId);

        ApiResponse<TResponseModel> Insert([FromHeader(Name = "UserId")] string headerUserId, int parentId, [FromBody] TRequestModel requestModel);

        ApiResponse<TResponseModel> Update([FromHeader(Name = "UserId")] string headerUserId, int parentId, [FromBody] TRequestModel requestModel);

        ApiResponse Delete([FromHeader(Name = "UserId")] string headerUserId, int parentId, [FromBody] TRequestModel requestModel);
    }
}