namespace ApiEndpoint.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using Controllers.Bases;
    using DataTransferObject;
    using Provider.Contracts;
    using ViewModels.Response;

    public class ControllerParentChildrenOperationsDto<TEntityDto, TEntity, TRequestModel, TResponseModel> : IBasicParentChildrenOperation<TRequestModel, TResponseModel>
        where TEntityDto : BaseEntityDto, new() where TEntity : new() where TRequestModel : new() where TResponseModel : BaseResponseModel, new()
    {
        private const string MissingHeaderUserIdError = "Missing header UserId, value must be Integer";
        private readonly IMapper _mapper;
        private readonly IParentChildrenProviderDto<TEntityDto, TEntity> _parentChildrenProviderDto;

        public ControllerParentChildrenOperationsDto(IParentChildrenProviderDto<TEntityDto, TEntity> parentChildrenProviderDto, IMapper mapper)
        {
            _parentChildrenProviderDto = parentChildrenProviderDto;
            _mapper = mapper;
        }

        public ApiResponse<List<TResponseModel>> GetAll(string headerUserId, int parentId)
        {
            List<TEntity> responseEntities;

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseMissingHeader((List<TResponseModel>) null, new Exception(MissingHeaderUserIdError));

            try
            {
                responseEntities = (List<TEntity>) _parentChildrenProviderDto.All(userId, parentId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError((List<TResponseModel>) null, exception);
            }

            var responseModel = _mapper.Map<List<TEntity>, List<TResponseModel>>(responseEntities);

            return !responseModel.Any() ? BaseResponse.ResponseNotFound((List<TResponseModel>) null) : BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse<TResponseModel> Get(string headerUserId, int parentId, int childId)
        {
            TEntity responseEntity;

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseMissingHeader((TResponseModel) null, new Exception(MissingHeaderUserIdError));

            try
            {
                responseEntity = _parentChildrenProviderDto.GetById(userId, parentId, childId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return responseModel == null || responseModel.Id == 0 ? BaseResponse.ResponseNotFound((TResponseModel) null) : BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse<TResponseModel> Insert(string headerUserId, int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);
            TEntity responseEntity;

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseMissingHeader((TResponseModel) null, new Exception(MissingHeaderUserIdError));

            try
            {
                responseEntity = _parentChildrenProviderDto.Insert(userId, parentId, requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseCreated(responseModel);
        }

        public ApiResponse<TResponseModel> Update(string headerUserId, int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);
            TEntity responseEntity;

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseMissingHeader((TResponseModel) null, new Exception(MissingHeaderUserIdError));

            try
            {
                responseEntity = _parentChildrenProviderDto.Update(userId, parentId, requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse Delete(string headerUserId, int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);

            var hasUserId = int.TryParse(headerUserId, out var userId);

            if (!hasUserId)
                return BaseResponse.ResponseEntityValidation(new Exception(MissingHeaderUserIdError));

            try
            {
                _parentChildrenProviderDto.Delete(userId, parentId, requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(exception);
            }

            return BaseResponse.ResponseNoContent();
        }
    }
}