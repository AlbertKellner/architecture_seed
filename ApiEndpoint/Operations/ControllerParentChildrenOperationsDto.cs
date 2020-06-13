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

    public class ControllerParentChildrenOperationsDto<TEntityDto, TEntity, TRequestModel, TResponseModel> : IBasicParentChildrenOperation<TRequestModel, TResponseModel>
        where TEntityDto : BaseEntityDto, new() where TEntity : new() where TRequestModel : new() where TResponseModel : BaseResponseModel, new()
    {
        private const string MissingHeaderUserIdError = "Missing header UserId, value must be Integer";
        private readonly IMapper _mapper;
        private readonly IParentChildrenCoreDto<TEntityDto, TEntity> _parentChildrenCoreDto;

        public ControllerParentChildrenOperationsDto(IParentChildrenCoreDto<TEntityDto, TEntity> parentChildrenCoreDto, IMapper mapper)
        {
            _parentChildrenCoreDto = parentChildrenCoreDto;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<TResponseModel>>> GetAllAsync(int parentId)
        {
            List<TEntity> responseEntities;

            try
            {
                responseEntities = await _parentChildrenCoreDto.AllAsync(parentId) as List<TEntity>;
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError((List<TResponseModel>) null, exception);
            }

            var responseModel = _mapper.Map<List<TEntity>, List<TResponseModel>>(responseEntities);

            return !responseModel.Any() ? BaseResponse.ResponseNotFound((List<TResponseModel>) null) : BaseResponse.ResponseOk(responseModel);
        }

        public async Task<ApiResponse<TResponseModel>> GetAsync(int parentId, int childId)
        {
            TEntity responseEntity;

            try
            {
                responseEntity = await _parentChildrenCoreDto.GetByIdAsync(parentId, childId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return responseModel == null || responseModel.Id == 0 ? BaseResponse.ResponseNotFound((TResponseModel) null) : BaseResponse.ResponseOk(responseModel);
        }

        public async Task<ApiResponse<TResponseModel>> InsertAsync(int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);
            TEntity responseEntity;

            try
            {
                responseEntity = await _parentChildrenCoreDto.InsertAsync(parentId, requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseCreated(responseModel);
        }

        public async Task<ApiResponse<TResponseModel>> UpdateAsync(int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);
            TEntity responseEntity;

            try
            {
                responseEntity = await _parentChildrenCoreDto.UpdateAsync(parentId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseOk(responseModel);
        }

        public async Task<ApiResponse> DeleteAsync(int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);

            try
            {
                await _parentChildrenCoreDto.DeleteAsync(parentId, requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(exception);
            }

            return BaseResponse.ResponseNoContent();
        }
    }
}