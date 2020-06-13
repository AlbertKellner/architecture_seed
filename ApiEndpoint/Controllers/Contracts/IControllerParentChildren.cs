using System.Threading.Tasks;
using ApiEndpoint.Models.Response;

namespace ApiEndpoint.Controllers.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface IControllerParentChildren<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        Task<ApiResponse<List<TResponseModel>>> GetAll(int parentId);
        Task<ApiResponse<TResponseModel>> Get(int parentId, int childId);
        Task<ApiResponse<TResponseModel>> Insert(int parentId, [FromBody] TRequestModel requestModel);
        Task<ApiResponse<TResponseModel>> Update(int parentId, [FromBody] TRequestModel requestModel);
        Task<ApiResponse> Delete(int parentId, [FromBody] TRequestModel requestModel);
    }
}