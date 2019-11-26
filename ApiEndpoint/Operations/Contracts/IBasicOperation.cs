using ApiEndpoint.Models.Response;

namespace ApiEndpoint.Operations.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface IBasicOperation<in TRequestModel, TResponseModel> where TRequestModel : new() where TResponseModel : new()
    {
        ApiResponse<List<TResponseModel>> Get();

        ApiResponse<TResponseModel> Get(int id);

        ApiResponse<TResponseModel> Insert([FromBody] TRequestModel requestModel);

        ApiResponse<TResponseModel> Update([FromBody] TRequestModel requestModel);

        ApiResponse Delete([FromBody] TRequestModel requestModel);
    }
}