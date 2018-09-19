//namespace Data.Tests
//{
//    using System;
//    using Model;
//    using Xunit;

//    public class PerfilFisicoEntityTest
//    {
//        private readonly PerfilFisicoEntity _perfilValido = new PerfilFisicoEntity
//                                                            {
//                                                                Cpf = "818.697.573-00",
//                                                                NomeCompleto = "Developer",
//                                                                DataNascimento = new DateTime(1985, 04, 26)
//                                                            };

//        [Fact]
//        public void IsNotValid_Perfil()
//        {
//            var perfil = _perfilValido;
//            perfil.NomeCompleto = string.Empty;
//            Assert.False(perfil.IsValid());

//            perfil = _perfilValido;
//            perfil.Cpf = string.Empty;
//            Assert.False(perfil.IsValid());

//            perfil = _perfilValido;
//            perfil.DataNascimento = new DateTime();
//            Assert.False(perfil.IsValid());
//        }

//        [Fact]
//        public void IsNotValid_PerfilValido_CpfInvalido()
//        {
//            var perfilValido = _perfilValido;
//            perfilValido.Cpf = "123";

//            Assert.False(perfilValido.IsValid());
//        }

//        [Fact]
//        public void IsValid_Perfil()
//        {
//            var perfilValido = _perfilValido;

//            Assert.True(perfilValido.IsValid());
//        }

//        [Fact]
//        public void IsValid_PerfilValido_CpfValido()
//        {
//            var perfilValido = _perfilValido;
//            perfilValido.Cpf = "275.333.427-71";

//            Assert.True(perfilValido.IsValid());
//        }
//    }
//}

