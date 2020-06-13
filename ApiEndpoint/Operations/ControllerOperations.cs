using System.Threading.Tasks;
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
        private readonly IGenericCore<TEntity> _genericCore;
        private readonly IMapper _mapper;

        public ControllerOperations(IGenericCore<TEntity> genericCore, IMapper mapper)
        {
            _genericCore = genericCore;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<TResponseModel>>> GetAsync()
        {
            List<TEntity> responseEntities;

            try
            {
                responseEntities =  await _genericCore.GetAsync() as List<TEntity>;
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError((List<TResponseModel>) null, exception);
            }

            var responseModel = _mapper.Map<List<TEntity>, List<TResponseModel>>(responseEntities);

            return !responseModel.Any() ? BaseResponse.ResponseNotFound((List<TResponseModel>) null) : BaseResponse.ResponseOk(responseModel);
        }

        public async Task<ApiResponse<TResponseModel>> GetAsync(int id)
        {
            TEntity responseEntity;

            try
            {
                responseEntity = await _genericCore.GetAsync(id);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return responseModel == null || responseModel.Id == 0 ? BaseResponse.ResponseNotFound((TResponseModel) null) : BaseResponse.ResponseOk(responseModel);
        }

        public async Task<ApiResponse<TResponseModel>> InsertAsync(TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);

            TEntity responseEntity;

            try
            {
                responseEntity = await _genericCore.InsertAsync(requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseCreated(responseModel);
        }

        public async Task<ApiResponse<TResponseModel>> UpdateAsync(TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);

            TEntity responseEntity;

            try
            {
                responseEntity = await _genericCore.UpdateAsync(requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseOk(responseModel);
        }

        public async Task<ApiResponse> DeleteAsync(TRequestModel requestModel)
        {
            var requestEntity = _mapper.Map<TRequestModel, TEntity>(requestModel);

            try
            {
                await _genericCore.DeleteAsync(requestEntity);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(exception);
            }

            return BaseResponse.ResponseNoContent();
        }
    }
}