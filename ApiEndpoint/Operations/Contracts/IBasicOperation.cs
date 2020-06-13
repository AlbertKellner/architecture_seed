using System.Threading.Tasks;
using ApiEndpoint.Models.Response;

namespace ApiEndpoint.Operations.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface IBasicOperation<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        Task<ApiResponse<List<TResponseModel>>> GetAsync();
        Task<ApiResponse<TResponseModel>> GetAsync(int id);
        Task<ApiResponse<TResponseModel>> InsertAsync([FromBody] TRequestModel requestModel);
        Task<ApiResponse<TResponseModel>> UpdateAsync([FromBody] TRequestModel requestModel);
        Task<ApiResponse> DeleteAsync([FromBody] TRequestModel requestModel);
    }
}