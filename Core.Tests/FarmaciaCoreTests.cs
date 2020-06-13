using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task GetAll()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var repositoryEntities = new List<FarmaciaDto>
                                     {
                                         new FarmaciaDto {Nome = "Farmacia 01"},
                                         new FarmaciaDto {Nome = "Farmacia 02"},
                                         new FarmaciaDto {Nome = "Farmacia 03"}
                                     };

            foreach (var entity in repositoryEntities)
                await core.InsertAsync(entity);

            //Act
            var actual = await core.AllAsync();
            
            //Assert
            Assert.Equal("Farmacia 01", actual.First().Nome);
            Assert.Equal("Farmacia 03", actual.Last().Nome);

            await core.InsertAsync(new FarmaciaDto { Nome = "Farmacia 04" });

            //Re-Act
            var actual2 = await core.AllAsync();

            //Re-Assert
            Assert.Equal("Farmacia 04", actual2.Last().Nome);
        }

        [Fact]
        public async Task GetById()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var repositoryEntities = new List<FarmaciaDto>
                                     {
                                         new FarmaciaDto {Nome = "Farmacia 01"},
                                         new FarmaciaDto {Nome = "Farmacia 02"},
                                         new FarmaciaDto {Nome = "Farmacia 03"}
                                     };

            foreach (var entity in repositoryEntities)
                await core.InsertAsync(entity);

            //Act
            var farmacia1 = await core.GetByIdAsync(1);
            var farmacia2 = await core.GetByIdAsync(2);
            var farmacia3 = await core.GetByIdAsync(3);

            //Assert
            Assert.Equal("Farmacia 01", farmacia1.Nome);
            Assert.Equal("Farmacia 02", farmacia2.Nome);
            Assert.Equal("Farmacia 03", farmacia3.Nome);
        }

        [Fact]
        public async Task GetByIdInvalidUser()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var repositoryEntities = new List<FarmaciaDto>
                                     {
                                         new FarmaciaDto {Nome = "Farmacia 01"},
                                         new FarmaciaDto {Nome = "Farmacia 02"},
                                         //new FarmaciaDto {Nome = "Farmacia 03"}
                                     };

            foreach (var entity in repositoryEntities)
                await core.InsertAsync(entity);

            //Act
            var farmacia1 = await core.GetByIdAsync(1);
            var farmacia2 = await core.GetByIdAsync(2);
            var farmacia3 = await core.GetByIdAsync(3);

            //Assert
            Assert.Equal("Farmacia 01", farmacia1.Nome);
            Assert.Equal("Farmacia 02", farmacia2.Nome);
            Assert.Null(farmacia3);
        }

        [Fact]
        public async Task Insert()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var entityDto = new FarmaciaDto { Nome = "asd" };
            //const int userId = 55;

            //Act
            var actual = await core.InsertAsync(entityDto);

            //Assert
            Assert.Equal(0, entityDto.Id);
            Assert.Equal(1, actual.Id);
            //Assert.Equal(userId, actual.UsuarioEntityId);
        }

        [Fact]
        public async Task InsertDuplicated()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var entityDto = new FarmaciaDto { Nome = "asd" };
            //const int userId = 55;

            //Act
            var actual = await core.InsertAsync(entityDto);

            //Assert
            Assert.Equal(0, entityDto.Id);
            Assert.Equal(1, actual.Id);
            //Assert.Equal(userId, actual.UsuarioEntityId);

            //Re-Act
            async Task<FarmaciaEntity> InsertAgain() => await core.InsertAsync(entityDto);

            //Assert
            var exception = await Assert.ThrowsAsync<AlreadyExistsCustomException>(InsertAgain);

            Assert.Equal(typeof(AlreadyExistsCustomException), exception.GetType());
        }

        [Fact]
        public async Task Update()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var entityDto = new FarmaciaDto { Nome = "Farmacia" };
            //const int userId = 55;

            //Act
            var entity = await core.InsertAsync(entityDto);

            //Assert
            var getInsert = await core.GetByIdAsync(entity.Id);
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
            var updatedActual = await core.UpdateAsync(entityDto);

            //Re-Assert
            var getUpdate = await core.GetByIdAsync(updatedActual.Id);
            Assert.Equal(updatedActual.Id, getUpdate.Id);
            Assert.Equal(updatedActual.Nome, getUpdate.Nome);
            Assert.Equal(updatedActual.UsuarioEntityId, getUpdate.UsuarioEntityId);

            Assert.Equal(entity.Id, updatedActual.Id);
            Assert.Equal(entityDto.Nome, updatedActual.Nome);
            //Assert.Equal(userId, updatedActual.UsuarioEntityId);
        }
        
        [Fact] 
        public async Task UpdateDoesntExist()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();
            var core = new FarmaciaCore(unitOfWork, mapper);

            //Arrange
            var updateDto = new FarmaciaDto { Id = 1, Nome = "Farmacia" };

            //Act
            //var actual = await core.UpdateAsync(updateDto);
            async Task<FarmaciaEntity> Update() => await core.UpdateAsync(updateDto);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundCustomException>(Update);

            Assert.Equal(typeof(NotFoundCustomException), exception.GetType());
        }
    }
} 