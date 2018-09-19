namespace Repository.Tests
{
    using System;
    using DataEntity.Model;
    using Operations;
    using Structure;
    using Xunit;

    public class RepositoryAddTest : IClassFixture<InMemoryTestFixture>
    {
        public RepositoryAddTest(InMemoryTestFixture fixture) => _fixture = fixture;

        private readonly InMemoryTestFixture _fixture;

        [Fact]
        public void ShouldAddNewUsuario()
        {
            var identityId = new Guid().ToString();

            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<UsuarioEntity>();
            var entity = new UsuarioEntity
                         {
                             IdentityId = identityId
                         };

            // Act
            repository.Add(entity);
            unitOfWork.SaveChanges();

            //Assert
            // Assert.Equal(1, entity.Id);
            Assert.Equal(identityId, entity.IdentityId);
            Assert.NotEqual(0, entity.Id);
        }

        [Fact]
        public async void ShouldAddNewUsuarioAsync()
        {
            var identityId = new Guid().ToString();

            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repositoryAsync = unitOfWork.GetRepositoryAsync<UsuarioEntity>();
            var entity = new UsuarioEntity
            {
                IdentityId = identityId
            };

            // Act
            await repositoryAsync.AddAsync(entity);
            unitOfWork.SaveChanges();

            //Assert
            //Assert.Equal(1, entity.Id);
            Assert.Equal(identityId, entity.IdentityId);
            Assert.NotEqual(0, entity.Id);
        }
    }
}