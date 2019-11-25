using ApiEndpoint.Models.Response;

namespace ApiEndpoint.Controllers.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface IControllerParentChildren<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        ApiResponse<List<TResponseModel>> GetAll(int parentId);

        ApiResponse<TResponseModel> Get(int parentId, int childId);

        ApiResponse<TResponseModel> Insert(int parentId, [FromBody] TRequestModel requestModel);

        ApiResponse<TResponseModel> Update(int parentId, [FromBody] TRequestModel requestModel);

        ApiResponse Delete(int parentId, [FromBody] TRequestModel requestModel);
    }
}