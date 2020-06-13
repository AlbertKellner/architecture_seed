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
    using DataTransferObject;

    public class ControllerOperationsDto<TEntityDto, TEntity, TRequestModel, TResponseModel> : IBasicOperation<TRequestModel, TResponseModel>
        where TEntityDto : BaseEntityDto, new()
        where TEntity : new()
        where TRequestModel : new()
        where TResponseModel : BaseResponseModel, new()
    {
        private readonly IGenericCoreDto<TEntityDto, TEntity> _genericCoreDto;
        private readonly IMapper _mapper;

        public ControllerOperationsDto(IGenericCoreDto<TEntityDto, TEntity> genericCoreDto, IMapper mapper)
        {
            _genericCoreDto = genericCoreDto;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<TResponseModel>>> GetAsync()
        {
            List<TEntity> responseEntities;
            const int userId = 0;

            try
            {
                responseEntities = await _genericCoreDto.AllAsync() as List<TEntity>;
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError((List<TResponseModel>) null, exception);
            }

            var responseModel = _mapper.Map<List<TEntity>, List<TResponseModel>>(responseEntities);

            return !responseModel.Any()
                ? BaseResponse.ResponseNotFound((List<TResponseModel>) null)
                : BaseResponse.ResponseOk(responseModel);
        }

        public async Task<ApiResponse<TResponseModel>> GetAsync(int id)
        {
            TEntity responseEntity;

            try
            {
                responseEntity = await _genericCoreDto.GetByIdAsync(id);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);
            
            return responseModel == null || responseModel.Id == 0
                ? BaseResponse.ResponseNotFound((TResponseModel) null)
                : BaseResponse.ResponseOk(responseModel);
        }

        public async Task<ApiResponse<TResponseModel>> InsertAsync(TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);

            TEntity responseEntity;

            try
            {
                responseEntity = await _genericCoreDto.InsertAsync(requestEntityDto);
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
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);

            TEntity responseEntity;

            try
            {
                responseEntity = await _genericCoreDto.UpdateAsync(requestEntityDto);
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
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);

            try
            {
                await _genericCoreDto.DeleteAsync(requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(exception);
            }

            return BaseResponse.ResponseNoContent();
        }
    }
}