namespace ApiEndpoint.Tests
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Controllers;
    using Controllers.Contracts;
    using DataEntity.Model;
    using DataTransferObject;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Moq;
    using Provider.Contracts;
    using ViewModels.Request;
    using ViewModels.Response;
    using Xunit;

    public class PerfilFisicoControllerTests
    {
        //private readonly Mock<IGenericProviderDto<FarmaciaDto, FarmaciaEntity>> _mockRepository = new Mock<IGenericProviderDto<FarmaciaDto, FarmaciaEntity>>();

        //[Fact]
        //public void TestControllerById()
        //{
        //    Mock<IMappingService> mappingService = new Mock<IMappingService>();

        //    //Arrange
        //    var farmaciaResponseModels = new FarmaciaEntity
        //        { Nome = "Farmacia 01" };

        //    var apiResponseModel = new ApiResponse<FarmaciaResponseModel>
        //    {
        //        Data = new FarmaciaResponseModel
        //        {
        //            Nome = "Farmacia 01"
        //        }
        //    };

        //    _mockRepository.Setup(x => x.GetById(1, 1)).Returns(farmaciaResponseModels);

        //    mappingService.Setup(m => m.Map<FarmaciaDto, FarmaciaEntity>(It.IsAny<FarmaciaDto>()))
        //        .Returns(farmaciaResponseModels);

        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new FarmaciaController(_mockRepository.Object, mappingService.Object);
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext();
        //    controller.ControllerContext.HttpContext.Request.Headers["UserId"] = "1";

        //    //Act            
        //    var actual = controller.Get(1);

        //    //Assert
        //    Assert.Equal(apiResponseModel.Data.Nome, actual.Data.Nome);
        //    Assert.Equal(apiResponseModel, actual);
        //}

        //[Fact]
        //public void TestController()
        //{
        //    //Arrange
        //    var farmaciaResponseModels = new List<FarmaciaEntity>
        //    {
        //               new FarmaciaEntity { Nome = "Farmacia 01" },
        //               new FarmaciaEntity { Nome = "Farmacia 02" }
        //    };

        //    var apiResponseModel = new ApiResponse<List<FarmaciaResponseModel>>
        //    {
        //        Data = new List<FarmaciaResponseModel>
        //        {
        //            new FarmaciaResponseModel {Nome = "Farmacia 01"},
        //            new FarmaciaResponseModel {Nome = "Farmacia 02"}
        //        }
        //    };

        //    _mockRepository.Setup(x => x.All(1)).Returns(farmaciaResponseModels);

        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new FarmaciaController(_mockRepository.Object, mockMapper.Object);
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext();
        //    controller.ControllerContext.HttpContext.Request.Headers["UserId"] = "1";

        //    //Act            
        //    var actual = controller.GetAll();

        //    //Assert
        //    Assert.Equal(apiResponseModel, actual);
        //}

        //[Fact]
        //public void TestController2()
        //{
        //    //Arrange
        //    var entity = new PerfilFisicoEntity
        //    {
        //        Id = 1
        //    };

        //    _mockRepository.Setup(x => x.Insert(entity));

        //    var controller = new PerfilFisicoController(_mockRepository.Object);

        //    //Act            
        //    controller.Insert(entity);

        //    //Assert
        //    Assert.Equal(entity, entity);
        //}
    }
}

