using ApiEndpoint.Controllers;
using ApiEndpoint.Models.Response;
using Core.Contracts;
using AutoMapper;
using DataEntity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApiEndpoint.Tests
{


    public class UsuarioControllerTests
    {
        private readonly Mock<IGenericProvider<UsuarioEntity>> _mockProvider = new Mock<IGenericProvider<UsuarioEntity>>();

        [Fact]
        public void GetById()
        {
            var mappingService = new Mock<IMapper>();

            //Arrange
            var usuarioEntity = new UsuarioEntity { Id = 1 };

            var usuarioResponseModel = new UsuarioResponseModel { Id = 1 };

            _mockProvider.Setup(x => x.Get(1)).Returns(usuarioEntity);

            mappingService.Setup(m => m.Map<UsuarioEntity, UsuarioResponseModel>(It.IsAny<UsuarioEntity>())).Returns(usuarioResponseModel);

            var controller = new UsuarioController(_mockProvider.Object, mappingService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            //Act
            var apiResponse = controller.Get(1);

            //Assert
            Assert.Equal(apiResponse.Data.Id, usuarioEntity.Id);
        }

        //[Fact]
        //public void TestControllerById()
        //{
        //    Mock<IMappingService> mappingService = new Mock<IMappingService>();

        //    //Arrange
        //    var farmaciaResponseModels = new FarmaciaEntity
        //    { Nome = "Farmacia 01" };

        //    var apiResponseModel = new ApiResponse<FarmaciaResponseModel>
        //    {
        //        Data = new FarmaciaResponseModel
        //        {
        //            Nome = "Farmacia 01"
        //        }
        //    };

        //    _mockProvider.Setup(x => x.Get(1, 1)).Returns(farmaciaResponseModels);

        //    mappingService.Setup(m => m.Map<FarmaciaDto, FarmaciaEntity>(It.IsAny<FarmaciaDto>()))
        //        .Returns(farmaciaResponseModels);

        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new FarmaciaController(_mockProvider.Object, mappingService.Object);
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

        //    _mockProvider.Setup(x => x.Get(1)).Returns(farmaciaResponseModels);

        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new FarmaciaController(_mockProvider.Object, mockMapper.Object);
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext();
        //    controller.ControllerContext.HttpContext.Request.Headers["UserId"] = "1";

        //    //Act            
        //    var actual = controller.Get();

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

        //    _mockProvider.Setup(x => x.Insert(entity));

        //    var controller = new PerfilFisicoController(_mockProvider.Object);

        //    //Act            
        //    controller.Insert(entity);

        //    //Assert
        //    Assert.Equal(entity, entity);
        //}
    }
}

