//namespace Service.Tests
//{
//    using System;
//    using Data.Model;
//    using Moq;
//    using Provider;
//    using Repository.Contracts;
//    using Xunit;

//    public class PerfilFisicoProviderTests
//    {
//        private readonly Mock<IUnitOfWork> _repository = new Mock<IUnitOfWork>();

//        private readonly PerfilFisicoEntity _validEntity = new PerfilFisicoEntity
//                                                           {
//                                                               Id = 0,
//                                                               NomeCompleto = "asdasdasd",
//                                                               Cpf = "30172808880",
//                                                               DataNascimento = new DateTime(1985, 04, 26)
//                                                           };

//        [Fact]
//        public void IsNotValidModel()
//        {
//            //Arrange
//            var entity = _validEntity;
//            entity.NomeCompleto = string.Empty;

//            _repository.Setup(x => x.GetRepository<PerfilFisicoEntity>().Add(entity));

//            var core = new PerfilFisicoProvider(_repository.Object);

//            //Act
//            var actual = core.Insert(entity);

//            //Assert
//            Assert.False(entity.IsValid());
//            Assert.Equal(0, actual);
//        }

//        [Fact]
//        public void IsValidModel()
//        {
//            //Arrange
//            _repository.Setup(x => x.GetRepository<PerfilFisicoEntity>().Add(_validEntity));

//            var core = new PerfilFisicoProvider(_repository.Object);

//            //Act
//            var actual = core.Insert(_validEntity);

//            //Assert
//            Assert.True(_validEntity.IsValid());
//            //Assert.NotEqual(0, actual);
//        }
//    }
//}

