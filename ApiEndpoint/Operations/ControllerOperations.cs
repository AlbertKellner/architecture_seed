namespace ApiEndpoint.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using Controllers.Bases;
    using Provider.Contracts;
    using ViewModels.Response;

    public class ControllerOperations<TEntity, TRequestModel, TResponseModel> : IBasicOperation<TRequestModel, TResponseModel>
        where TEntity : new() where TRequestModel : new() where TResponseModel : BaseResponseModel, new()
    {
        private const string MissingHeaderUserIdError = "Missing header UserId, value must be Integer";
        private readonly IGenericProvider<TEntity> _genericProvider;
        private readonly IMapper _mapper;

        public ControllerOperations(IGenericProvider<TEntity> genericProvider, IMapper mapper)
        {
            _genericProvider = genericProvider;
            _mapper = mapper;
        }

        public ApiResponse<List<TResponseModel>> GetAll(string headerUserId)
        {
            List<TEntity> responseEntities;

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseMissingHeader((List<TResponseModel>) null, new Exception(MissingHeaderUserIdError));

            try
            {
                responseEntities = (List<TEntity>) _genericProvider.All(userId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError((List<TResponseModel>) null, exception);
            }

            var responseModel = _mapper.Map<List<TEntity>, List<TResponseModel>>(responseEntities);

            return !responseModel.Any() ? BaseResponse.ResponseNotFound((List<TResponseModel>) null) : BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse<TResponseModel> Get(string headerUserId, int id)
        {
            TEntity responseEntity;

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseMissingHeader((TResponseModel) null, new Exception(MissingHeaderUserIdError));

            try
            {
                responseEntity = _genericProvider.GetById(userId, id);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return responseModel == null || responseModel.Id == 0 ? BaseResponse.ResponseNotFound((TResponseModel) null) : BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse<TResponseModel> Insert(string headerUserId, TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);
            TEntity responseEntity;

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseMissingHeader((TResponseModel) null, new Exception(MissingHeaderUserIdError));

            try
            {
                responseEntity = _genericProvider.Insert(userId, requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseCreated(responseModel);
        }

        public ApiResponse<TResponseModel> Update(string headerUserId, TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);
            TEntity responseEntity;

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseMissingHeader((TResponseModel) null, new Exception(MissingHeaderUserIdError));

            try
            {
                responseEntity = _genericProvider.Update(userId, requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse Delete(string headerUserId, TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseEntityValidation(new Exception(MissingHeaderUserIdError));

            try
            {
                _genericProvider.Delete(userId, requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(exception);
            }

            return BaseResponse.ResponseNoContent();
        }

        public ApiResponse<TResponseModel> GetByIdentity(string identityId)
        {
            TEntity responseEntity;

            try
            {
                responseEntity = _genericProvider.GetByIdentity(identityId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return responseModel == null || responseModel.Id == 0 ? BaseResponse.ResponseNotFound((TResponseModel) null) : BaseResponse.ResponseOk(responseModel);
        }
    }
}