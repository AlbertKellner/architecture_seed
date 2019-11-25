﻿using ApiEndpoint.Models.Response;
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
        private readonly IParentChildrenProviderDto<TEntityDto, TEntity> _parentChildrenProviderDto;

        public ControllerParentChildrenOperationsDto(IParentChildrenProviderDto<TEntityDto, TEntity> parentChildrenProviderDto, IMapper mapper)
        {
            _parentChildrenProviderDto = parentChildrenProviderDto;
            _mapper = mapper;
        }

        public ApiResponse<List<TResponseModel>> GetAll(int parentId)
        {
            List<TEntity> responseEntities;

            try
            {
                responseEntities = (List<TEntity>) _parentChildrenProviderDto.All(parentId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError((List<TResponseModel>) null, exception);
            }

            var responseModel = _mapper.Map<List<TEntity>, List<TResponseModel>>(responseEntities);

            return !responseModel.Any() ? BaseResponse.ResponseNotFound((List<TResponseModel>) null) : BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse<TResponseModel> Get(int parentId, int childId)
        {
            TEntity responseEntity;

            try
            {
                responseEntity = _parentChildrenProviderDto.GetById(parentId, childId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return responseModel == null || responseModel.Id == 0 ? BaseResponse.ResponseNotFound((TResponseModel) null) : BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse<TResponseModel> Insert(int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);
            TEntity responseEntity;

            try
            {
                responseEntity = _parentChildrenProviderDto.Insert(parentId, requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseCreated(responseModel);
        }

        public ApiResponse<TResponseModel> Update(int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);
            TEntity responseEntity;

            try
            {
                responseEntity = _parentChildrenProviderDto.Update(parentId);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(default(TResponseModel), exception);
            }

            var responseModel = _mapper.Map<TEntity, TResponseModel>(responseEntity);

            return BaseResponse.ResponseOk(responseModel);
        }

        public ApiResponse Delete(int parentId, TRequestModel requestModel)
        {
            var requestEntityDto = _mapper.Map<TRequestModel, TEntityDto>(requestModel);

            try
            {
                _parentChildrenProviderDto.Delete(parentId, requestEntityDto);
            }
            catch (Exception exception)
            {
                return BaseResponse.ResponseInternalServerError(exception);
            }

            return BaseResponse.ResponseNoContent();
        }
    }
}