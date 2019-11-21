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

    public class
        ControllerOperationsDto<TEntityDto, TEntity, TRequestModel, TResponseModel> : IBasicOperation<TRequestModel,
            TResponseModel>
        where TEntityDto : BaseEntityDto, new()
        where TEntity : new()
        where TRequestModel : new()
        where TResponseModel : BaseResponseModel, new()
    {
        private readonly IGenericProviderDto<TEntityDto, TEntity> _genericProviderDto;
        private readonly IMapper _mapper;

        public ControllerOperationsDto(IGenericProviderDto<TEntityDto, TEntity> genericProviderDto, IMapper mapper)
        {
            _genericProviderDto = genericProviderDto;
            _mapper = mapper;
        }

        public ApiResponse<List<TResponseModel>> GetAll()
        {
            List<TEntity> responseEntities;
            const int userId = 0;

            try
            {
                responseEntities = (List<TEntity>) _genericProviderDto.All(userId);
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

        public ApiResponse<TResponseModel> Get(int id)
        {
            TEntity responseEntity;
            const int userId = 0;

            try
            {
                responseEntity = _genericProviderDto.GetById(id);
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

        public ApiResponse<TResponseModel> Insert(TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);

            TEntity responseEntity;
            const int userId = 0;

            try
            {
                responseEntity = _genericProviderDto.Insert(userId, requestEntityDto);
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
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);

            TEntity responseEntity;
            const int userId = 0;

            try
            {
                responseEntity = _genericProviderDto.Update(userId, requestEntityDto);
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
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);
            const int userId = 0;

            try
            {
                _genericProviderDto.Delete(userId, requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(exception);
            }

            return BaseResponse.ResponseNoContent();
        }
    }
}