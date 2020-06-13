using System.Threading.Tasks;
using ApiEndpoint.Models.Response;

namespace ApiEndpoint.Operations.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface IBasicParentChildrenOperation<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        Task<ApiResponse<List<TResponseModel>>> GetAllAsync(int parentId);
        Task<ApiResponse<TResponseModel>> GetAsync(int parentId, int childId);
        Task<ApiResponse<TResponseModel>> InsertAsync(int parentId, [FromBody] TRequestModel requestModel);
        Task<ApiResponse<TResponseModel>> UpdateAsync(int parentId, [FromBody] TRequestModel requestModel);
        Task<ApiResponse> DeleteAsync(int parentId, [FromBody] TRequestModel requestModel);
    }
}