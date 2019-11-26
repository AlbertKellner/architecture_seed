using ApiEndpoint.Models.Response;
using Core.Contracts;

namespace ApiEndpoint.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using Controllers.Bases;

    public class ControllerOperations<TEntity, TRequestModel, TResponseModel> : IBasicOperation<TRequestModel, TResponseModel>
        where TEntity : new() where TRequestModel : new() where TResponseModel : BaseResponseModel, new()
    {
        private readonly IGenericProvider<TEntity> _genericProvider;
        private readonly IMapper _mapper;

        public ControllerOperations(IGenericProvider<TEntity> genericProvider, IMapper mapper)
        {
            _genericProvider = genericProvider;
            _mapper = mapper;
        }

        public ApiResponse<List<TResponseModel>> Get()
        {
            List<TEntity> responseEntities;

            try
            {
                responseEntities = (List<TEntity>) _genericProvider.Get();
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError((List<TResponseModel>) null, exception);
            }

            var responseModel = _mapper.Map<List<TEntity>, List<TResponseModel>>(responseEntities);

            return !responseModel.Any() ? BaseResponse.ResponseNotFound((List<TResponseModel>) null) : BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse<TResponseModel> Get(int id)
        {
            TEntity responseEntity;

            try
            {
                responseEntity = _genericProvider.Get(id);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return responseModel == null || responseModel.Id == 0 ? BaseResponse.ResponseNotFound((TResponseModel) null) : BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse<TResponseModel> Insert(TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);

            TEntity responseEntity;
            const int userId = 0;

            try
            {
                responseEntity = _genericProvider.Insert(requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseCreated(responseModel);
        }

        public ApiResponse<TResponseModel> Update(TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);

            TEntity responseEntity;
            const int userId = 0;

            try
            {
                responseEntity = _genericProvider.Update(requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse Delete(TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);

            const int userId = 0;

            try
            {
                _genericProvider.Delete(requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(exception);
            }

            return BaseResponse.ResponseNoContent();
        }
    }
}