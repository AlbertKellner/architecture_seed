//namespace Data.Tests
//{
//    using Model;
//    using Xunit;

//    public class PerfilJuridicoEntityTest
//    {
//        private readonly PerfilJuridicoEntity _perfilValido = new PerfilJuridicoEntity
//                                                              {
//                                                                  Cnpj = "41.987.483/0001-47",
//                                                                  NomeFantasia = "Developer",
//                                                                  RazaoSocial = "Developer S/A"
//                                                              };

//        [Fact]
//        public void IsNotValid_Perfil()
//        {
//            var perfil = _perfilValido;
//            perfil.NomeFantasia = string.Empty;
//            Assert.False(perfil.IsValid());

//            perfil = _perfilValido;
//            perfil.Cnpj = string.Empty;
//            Assert.False(perfil.IsValid());
//        }

//        [Fact]
//        public void IsNotValid_PerfilValido_CnpjInvalido()
//        {
//            var perfilValido = _perfilValido;
//            perfilValido.Cnpj = "123";

//            Assert.False(perfilValido.IsValid());
//        }

//        [Fact]
//        public void IsValid_Perfil()
//        {
//            var perfilValido = _perfilValido;

//            Assert.True(perfilValido.IsValid());
//        }

//        [Fact]
//        public void IsValid_PerfilValido_CnpjValido()
//        {
//            var perfilValido = _perfilValido;
//            perfilValido.Cnpj = "29.347.618/0001-04";

//            Assert.True(perfilValido.IsValid());
//        }
//    }
//}

