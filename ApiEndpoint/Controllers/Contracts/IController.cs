using ApiEndpoint.Models.Response;

namespace ApiEndpoint.Controllers.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface IController<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        ApiResponse<List<TResponseModel>> GetAll();

        ApiResponse<TResponseModel> Get(int id);

        ApiResponse<TResponseModel> Insert([FromBody] TRequestModel requestModel);

        ApiResponse<TResponseModel> Update([FromBody] TRequestModel requestModel);

        ApiResponse Delete([FromBody] TRequestModel requestModel);
    }
}