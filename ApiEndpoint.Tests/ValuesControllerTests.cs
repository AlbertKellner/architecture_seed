using System.Threading.Tasks;

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

    public class ValuesControllerTests
    {
        [Fact]
        public async Task TestController2()
        {
            //Arrange
            var controller = new ValuesController();

            //Act            
            var x = await controller.Get(1);

            //Assert
            Assert.Equal("", x);
        }
    }
}

