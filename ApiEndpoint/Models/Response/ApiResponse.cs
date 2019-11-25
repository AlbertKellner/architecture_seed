//https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api
//{
//"code" : 1024,
//"message" : "Validation Failed",
//"errors" : [
//{
//    "code" : 5432,
//    "field" : "first_name",
//    "message" : "First name cannot have fancy characters"
//},
//{
//"code" : 5622,
//"field" : "password",
//"message" : "Password cannot be blank"
//}
//]
//}


using System;
using Newtonsoft.Json;

namespace ApiEndpoint.Models.Response
{
    [JsonObject]
    public class ApiResponse<TResponseModel> where TResponseModel : new()
    {
        public ApiResponse() => Data = new TResponseModel();

        [JsonProperty(PropertyName = "success", NullValueHandling = NullValueHandling.Include)]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "innerException", NullValueHandling = NullValueHandling.Ignore)]
        public Exception InnerException { get; set; }

        [JsonProperty(PropertyName = "statusCode", NullValueHandling = NullValueHandling.Include)]
        public CustomHttpStatusCode StatusCode { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public TResponseModel Data { get; set; }
    }

    [JsonObject]
    public sealed class ApiResponse : ApiResponse<object>
    {
        public ApiResponse() => Data = null;
    }
}