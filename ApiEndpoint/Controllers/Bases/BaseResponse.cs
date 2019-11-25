using ApiEndpoint.Models.Response;

namespace ApiEndpoint.Controllers.Bases
{
    using System;

    public static class BaseResponse
    {
        public static ApiResponse<T> ResponseOk<T>(T data) where T : new() => new ApiResponse<T> {Data = data, Success = true, StatusCode = CustomHttpStatusCode.Ok};

        public static ApiResponse<T> ResponseCreated<T>(T data) where T : new() => new ApiResponse<T> {Data = data, Success = true, StatusCode = CustomHttpStatusCode.Created};

        public static ApiResponse<T> ResponseNotFound<T>(T data) where T : new() => new ApiResponse<T> {Success = true, StatusCode = CustomHttpStatusCode.NotFound};

        public static ApiResponse<T> ResponseNoContent<T>(T data) where T : new() => new ApiResponse<T> {Success = true, StatusCode = CustomHttpStatusCode.NoContent};

        public static ApiResponse ResponseNoContent() => new ApiResponse {Success = true, StatusCode = CustomHttpStatusCode.NoContent};


        public static ApiResponse<T> ResponseInternalServerError<T>(T data, Exception exception) where T : new() =>
            new ApiResponse<T>
            {
                Data = default(T),
                Success = false,
                Message = exception.Message,
                InnerException = exception.InnerException,
                StatusCode = CustomHttpStatusCode.InternalServerError
            };


        public static ApiResponse ResponseInternalServerError(Exception exception) =>
            new ApiResponse {Success = false, Message = exception.Message, StatusCode = CustomHttpStatusCode.InternalServerError};

        public static ApiResponse<T> ResponseEntityValidation<T>(T data, Exception exception) where T : new() =>
            new ApiResponse<T> {Data = default(T), Success = false, Message = exception.Message, StatusCode = CustomHttpStatusCode.EntityValidation};


        public static ApiResponse ResponseEntityValidation(Exception exception) =>
            new ApiResponse {Success = false, Message = exception.Message, StatusCode = CustomHttpStatusCode.EntityValidation};

        public static ApiResponse<T> ResponseAlreadyExists<T>(T data, Exception exception) where T : new() =>
            new ApiResponse<T> {Data = default(T), Success = false, Message = exception.Message, StatusCode = CustomHttpStatusCode.AlreadyExists};


        public static ApiResponse ResponseAlreadyExists(Exception exception) =>
            new ApiResponse {Success = false, Message = exception.Message, StatusCode = CustomHttpStatusCode.AlreadyExists};

        public static ApiResponse<T> ResponseMissingHeader<T>(T data, Exception exception) where T : new() =>
            new ApiResponse<T> {Data = default(T), Success = false, Message = exception.Message, StatusCode = CustomHttpStatusCode.MissingHeader};


        public static ApiResponse ResponseMissingHeader(Exception exception) =>
            new ApiResponse {Success = false, Message = exception.Message, StatusCode = CustomHttpStatusCode.MissingHeader};
    }
}