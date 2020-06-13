using System.Threading.Tasks;
using ApiEndpoint.Models.Response;

namespace ApiEndpoint.Controllers.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface IController<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        Task<ApiResponse<List<TResponseModel>>> Get();
        Task<ApiResponse<TResponseModel>> Get(int id);
        Task<ApiResponse<TResponseModel>> Insert([FromBody] TRequestModel requestModel);
        Task<ApiResponse<TResponseModel>> Update([FromBody] TRequestModel requestModel);
        Task<ApiResponse> Delete([FromBody] TRequestModel requestModel);
    }
}