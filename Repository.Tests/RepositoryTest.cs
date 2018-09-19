namespace Repository.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;
    using Operations;
    using Structure;
    using Xunit;

    public class RepositoryTest : IClassFixture<SqlLiteTestFixture>
    {
        public RepositoryTest(SqlLiteTestFixture fixture) => _fixture = fixture;

        private readonly SqlLiteTestFixture _fixture;

        [Fact]
        public void DeleteById()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entities = new List<PokemonTestEntity>
                           {
                               new PokemonTestEntity {Id = 1, Name = "test"},
                               new PokemonTestEntity {Id = 2, Name = "test 2"},
                               new PokemonTestEntity {Id = 3, Name = "test 3"}
                           };

            repository.Add(entities);
            unitOfWork.SaveChanges();
            unitOfWork.DetachEntries(entities);

            // Act
            repository.Delete(3);
            unitOfWork.SaveChanges();

            //Assert
            var getEntities = repository.GetList();
            Assert.Equal(2, getEntities.Count);
            Assert.Equal("test", getEntities.Items.First().Name);
            Assert.Equal("test 2", getEntities.Items.Last().Name);
        }
        
        [Fact]
        public void DeleteByEntity()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entities = new List<PokemonTestEntity>
                           {
                               new PokemonTestEntity {Id = 1, Name = "test"},
                               new PokemonTestEntity {Id = 2, Name = "test 2"},
                               new PokemonTestEntity {Id = 3, Name = "test 3"}
                           };

            repository.Add(entities);
            unitOfWork.SaveChanges();
            unitOfWork.DetachEntries(entities);

            // Act
            var entityToDelete = new PokemonTestEntity { Id = 3 };
            repository.Delete(entityToDelete);
            unitOfWork.SaveChanges();

            //Assert
            var getEntities = repository.GetList();
            Assert.Equal(2, getEntities.Count);
            Assert.Equal("test", getEntities.Items.First().Name);
            Assert.Equal("test 2", getEntities.Items.Last().Name);
        }

        [Fact]
        public void DeleteManyByEntity()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entities = new List<PokemonTestEntity>
                           {
                               new PokemonTestEntity {Id = 1, Name = "test"},
                               new PokemonTestEntity {Id = 2, Name = "test 2"},
                               new PokemonTestEntity {Id = 3, Name = "test 3"}
                           };

            repository.Add(entities);
            unitOfWork.SaveChanges();
            unitOfWork.DetachEntries(entities);

            // Act
            var entitiesToDelete = new List<PokemonTestEntity>
                                 {
                                     new PokemonTestEntity {Id = 2, Name = "test 2"},
                                     new PokemonTestEntity {Id = 3, Name = "test 3"}
                                 };

            repository.Delete(entitiesToDelete);
            unitOfWork.SaveChanges();

            //Assert
            var getEntities = repository.GetList();
            Assert.Equal(1, getEntities.Count);
            Assert.Equal("test", getEntities.Items.First().Name);
            Assert.Equal("test", getEntities.Items.Last().Name);
        }

        [Fact]
        public void GetAll()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entity = new List<PokemonTestEntity>
                         {
                             new PokemonTestEntity {Id = 1, Name = "test"},
                             new PokemonTestEntity {Id = 2, Name = "test 2"},
                             new PokemonTestEntity {Id = 3, Name = "test 3"}
                         };

            repository.Add(entity);
            unitOfWork.SaveChanges();

            // Act
            var entities = repository.GetList();

            //Assert
            Assert.Equal(3, entities.Count);
            Assert.Equal("test", entities.Items.First().Name);
            Assert.Equal("test 3", entities.Items.Last().Name);
        }

        [Fact]
        public void Insert()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entity = new PokemonTestEntity {Name = "test"};

            // Act
            repository.Add(entity);
            unitOfWork.SaveChanges();

            //Assert
            Assert.Equal(1, entity.Id);
        }

        [Fact]
        public void Single()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entity = new List<PokemonTestEntity>
                         {
                             new PokemonTestEntity {Id = 1, Name = "test"},
                             new PokemonTestEntity {Id = 2, Name = "test 2"},
                             new PokemonTestEntity {Id = 3, Name = "test 3"}
                         };

            repository.Add(entity);
            unitOfWork.SaveChanges();

            // Act
            var single = repository.Single(o => o.Id == 2);

            //Assert
            Assert.Equal("test 2", single.Name);
        }

        [Fact]
        public void Update()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entity = new PokemonTestEntity {Name = "test"};

            // Act
            repository.Add(entity);
            unitOfWork.SaveChanges();
            unitOfWork.DetachEntry(entity);

            var savedEntity = repository.Single(o => o.Name == entity.Name);

            //Assert
            Assert.Equal(1, savedEntity.Id);

            // Arrange 
            savedEntity.Name = "test update";

            // Act
            repository.Update(savedEntity);
            unitOfWork.SaveChanges();

            //Assert
            Assert.Equal(1, entity.Id);
        }

        [Fact]
        public void UpdateManyDetachEntries()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entities = new List<PokemonTestEntity>
                           {
                               new PokemonTestEntity {Id = 1, Name = "test"},
                               new PokemonTestEntity {Id = 2, Name = "test 2"},
                               new PokemonTestEntity {Id = 3, Name = "test 3"}
                           };

            // Act
            repository.Add(entities);
            unitOfWork.SaveChanges();
            unitOfWork.DetachEntries(entities);

            var assertAddedEntities = repository.GetList();
            
            //Assert
            Assert.Equal(3, assertAddedEntities.Items.Count);
            Assert.Equal("test", assertAddedEntities.Items.First().Name);
            Assert.Equal("test 3", assertAddedEntities.Items.Last().Name);

            var updatedEntities = new List<PokemonTestEntity>
                           {
                               new PokemonTestEntity {Id = 2, Name = "test 2 - updated"},
                               new PokemonTestEntity {Id = 3, Name = "test 3 - updated"}
                           };

            // Act
            repository.Update(updatedEntities);
            unitOfWork.SaveChanges();

            var assertUpdatedEntities = repository.GetList();

            //Assert
            Assert.Equal(3, assertUpdatedEntities.Items.Count);
            Assert.Equal("test", assertUpdatedEntities.Items.First().Name);
            Assert.Equal("test 3 - updated", assertUpdatedEntities.Items.Last().Name);
        }

        [Fact]
        public void UpdateManyDetachAllEntities()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repository = unitOfWork.GetRepository<PokemonTestEntity>();

            var entities = new List<PokemonTestEntity>
                           {
                               new PokemonTestEntity {Id = 1, Name = "test"},
                               new PokemonTestEntity {Id = 2, Name = "test 2"},
                               new PokemonTestEntity {Id = 3, Name = "test 3"}
                           };

            // Act
            repository.Add(entities);
            unitOfWork.SaveChanges();
            unitOfWork.DetachAllEntities();

            var assertAddedEntities = repository.GetList();

            //Assert
            Assert.Equal(3, assertAddedEntities.Items.Count);
            Assert.Equal("test", assertAddedEntities.Items.First().Name);
            Assert.Equal("test 3", assertAddedEntities.Items.Last().Name);

            var updatedEntities = new List<PokemonTestEntity>
                                  {
                                      new PokemonTestEntity {Id = 2, Name = "test 2 - updated"},
                                      new PokemonTestEntity {Id = 3, Name = "test 3 - updated"}
                                  };

            // Act
            repository.Update(updatedEntities);
            unitOfWork.SaveChanges();

            var assertUpdatedEntities = repository.GetList();

            //Assert
            Assert.Equal(3, assertUpdatedEntities.Items.Count);
            Assert.Equal("test", assertUpdatedEntities.Items.First().Name);
            Assert.Equal("test 3 - updated", assertUpdatedEntities.Items.Last().Name);
        }
    }
}