using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.DataTransferObjectMapping;
using CustomExceptions;
using DataEntity.Model;
using DataTransferObject;
using Repository.Operations;
using Repository.Tests.Structure;
using Xunit;

namespace Core.Tests
{
    public class FarmaciaCoreTests : IClassFixture<SqlLiteTestFixture>
    {
        public FarmaciaCoreTests(SqlLiteTestFixture fixture) => _fixture = fixture;

        private readonly SqlLiteTestFixture _fixture;

        [Fact]
        public void GetAll()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            const int userId = 55;
            const int otherUserId = 70;

            var repositoryEntities = new List<FarmaciaDto>
                                     {
                                         new FarmaciaDto {Nome = "Farmacia 01"},
                                         new FarmaciaDto {Nome = "Farmacia 02"},
                                         new FarmaciaDto {Nome = "Farmacia 03"}
                                     };

            foreach (var entity in repositoryEntities)
                core.Insert(entity);

            //Act
            var actual = core.All();
            
            //Assert
            Assert.Equal("Farmacia 01", actual.First().Nome);
            Assert.Equal("Farmacia 03", actual.Last().Nome);

            core.Insert(new FarmaciaDto { Nome = "Farmacia 04" });

            //Re-Act
            var actual2 = core.All();

            //Re-Assert
            Assert.Equal("Farmacia 04", actual2.Last().Nome);
        }

        [Fact]
        public void GetById()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            const int userId = 55;

            var repositoryEntities = new List<FarmaciaDto>
                                     {
                                         new FarmaciaDto {Nome = "Farmacia 01"},
                                         new FarmaciaDto {Nome = "Farmacia 02"},
                                         new FarmaciaDto {Nome = "Farmacia 03"}
                                     };

            foreach (var entity in repositoryEntities)
                core.Insert(entity);

            //Act
            var farmacia1 = core.GetById(1);
            var farmacia2 = core.GetById(2);
            var farmacia3 = core.GetById(3);

            //Assert
            Assert.Equal("Farmacia 01", farmacia1.Nome);
            Assert.Equal("Farmacia 02", farmacia2.Nome);
            Assert.Equal("Farmacia 03", farmacia3.Nome);
        }

        [Fact]
        public void GetByIdInvalidUser()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            const int userId = 55;
            const int otherUserId = 70;

            var repositoryEntities = new List<FarmaciaDto>
                                     {
                                         new FarmaciaDto {Nome = "Farmacia 01"},
                                         new FarmaciaDto {Nome = "Farmacia 02"},
                                         //new FarmaciaDto {Nome = "Farmacia 03"}
                                     };

            foreach (var entity in repositoryEntities)
                core.Insert(entity);

            //Act
            var farmacia1 = core.GetById(1);
            var farmacia2 = core.GetById(2);
            var farmacia3 = core.GetById(3);

            //Assert
            Assert.Equal("Farmacia 01", farmacia1.Nome);
            Assert.Equal("Farmacia 02", farmacia2.Nome);
            Assert.Null(farmacia3);
        }

        [Fact]
        public void Insert()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var entityDto = new FarmaciaDto { Nome = "asd" };
            //const int userId = 55;

            //Act
            var actual = core.Insert(entityDto);

            //Assert
            Assert.Equal(0, entityDto.Id);
            Assert.Equal(1, actual.Id);
            //Assert.Equal(userId, actual.UsuarioEntityId);
        }

        [Fact]
        public void InsertDuplicated()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var entityDto = new FarmaciaDto { Nome = "asd" };
            //const int userId = 55;

            //Act
            var actual = core.Insert(entityDto);

            //Assert
            Assert.Equal(0, entityDto.Id);
            Assert.Equal(1, actual.Id);
            //Assert.Equal(userId, actual.UsuarioEntityId);

            //Re-Act
            FarmaciaEntity InsertAgain() => core.Insert(entityDto);

            //Assert
            var exception = Assert.Throws<AlreadyExistsCustomException>((Func<FarmaciaEntity>) InsertAgain);

            Assert.Equal(typeof(AlreadyExistsCustomException), exception.GetType());
        }

        [Fact]
        public void Update()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var entityDto = new FarmaciaDto { Nome = "Farmacia" };
            //const int userId = 55;

            //Act
            var entity = core.Insert(entityDto);

            //Assert
            var getInsert = core.GetById(entity.Id);
            Assert.Equal(entity.Id, getInsert.Id);
            Assert.Equal(entity.Nome, getInsert.Nome);
            Assert.Equal(entity.UsuarioEntityId, getInsert.UsuarioEntityId);

            Assert.NotEqual(0, entity.Id);
            Assert.Equal(entityDto.Nome, entity.Nome);
            //Assert.Equal(userId, entity.UsuarioEntityId);

            //Re-Arrange
            entityDto.Id = entity.Id;
            entityDto.Nome = "Farmacia atualizada";

            //Re-Act
            var updatedActual = core.Update(entityDto);

            //Re-Assert
            var getUpdate = core.GetById(updatedActual.Id);
            Assert.Equal(updatedActual.Id, getUpdate.Id);
            Assert.Equal(updatedActual.Nome, getUpdate.Nome);
            Assert.Equal(updatedActual.UsuarioEntityId, getUpdate.UsuarioEntityId);

            Assert.Equal(entity.Id, updatedActual.Id);
            Assert.Equal(entityDto.Nome, updatedActual.Nome);
            //Assert.Equal(userId, updatedActual.UsuarioEntityId);
        }

        [Fact] 
        public void UpdateDoesntExist()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var updateDto = new FarmaciaDto { Id = 1, Nome = "Farmacia" };
            const int userId = 55;

            //Act
            FarmaciaEntity Update() => core.Update(updateDto);
            //Assert
            var exception = Assert.Throws<NotFoundCustomException>((Func<FarmaciaEntity>)Update);

            Assert.Equal(typeof(NotFoundCustomException), exception.GetType());
        }
    }
} 