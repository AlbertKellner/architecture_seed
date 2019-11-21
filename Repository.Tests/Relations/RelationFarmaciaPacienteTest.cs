//namespace Repository.Tests.Relations
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using DataEntity.Model;
//    using DataEntity.Model.Relations;
//    using Microsoft.EntityFrameworkCore;
//    using Operations;
//    using Structure;
//    using Xunit;

//    public class RelationFarmaciaPacienteTest : IClassFixture<SqlLiteTestFixture>
//    {
//        public RelationFarmaciaPacienteTest(SqlLiteTestFixture fixture) => _fixture = fixture;

//        private readonly SqlLiteTestFixture _fixture;

//        [Fact]
//        public void ShouldAddOneNewRelation()
//        {
//            // Arrange 
//            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
//            var farmaciaRepository = unitOfWork.GetRepository<FarmaciaEntity>();
//            var usuarioRepository = unitOfWork.GetRepository<UsuarioEntity>();

//            var usuarioEntity = new UsuarioEntity
//            {
//                IdentityId = new Guid().ToString()
//            };
//            usuarioRepository.Add(usuarioEntity);
//            unitOfWork.SaveChanges();

//            var newEntity = new FarmaciaEntity
//                            {
//                                UsuarioEntityId=usuarioEntity.Id,
//                                Nome = "Farmacia",
//                                Pacientes = new List<RelationFarmaciaPaciente>
//                                          {
//                                              new RelationFarmaciaPaciente
//                                              {
//                                                  Paciente = new PacienteEntity
//                                                  {
//                                                      UsuarioEntityId=usuarioEntity.Id,
//                                                      Nome = "Paciente"
//                                                  }
//                                              }
//                                          }
//                            };

//            // Act
//            farmaciaRepository.Add(newEntity);
//            unitOfWork.SaveChanges();

//            var laboratorio = farmaciaRepository.Single(e => e.Nome == "Farmacia",
//                include: s => s.Include(e => e.Pacientes).ThenInclude(r => r.Paciente));

//            //Assert
//            Assert.Equal("Farmacia", laboratorio.Nome);
//            Assert.Single(laboratorio.Pacientes);
//            Assert.Equal("Paciente", laboratorio.Pacientes.First().Paciente.Nome);
//        }

//        [Fact]
//        public void ShouldAddTwoNewRelations()
//        {
//            // Arrange 
//            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
//            var farmaciaRepository = unitOfWork.GetRepository<FarmaciaEntity>();
//            var usuarioRepository = unitOfWork.GetRepository<UsuarioEntity>();

//            var usuarioEntity = new UsuarioEntity
//            {
//                IdentityId = new Guid().ToString()
//            };
//            usuarioRepository.Add(usuarioEntity);
//            unitOfWork.SaveChanges();

//            var newEntity = new FarmaciaEntity
//                            {
//                                UsuarioEntityId = usuarioEntity.Id,
//                                Nome = "Farmacia",
//                                Pacientes = new List<RelationFarmaciaPaciente>
//                                            {
//                                                new RelationFarmaciaPaciente
//                                                {
//                                                    Paciente = new PacienteEntity
//                                                    {
//                                                        UsuarioEntityId = usuarioEntity.Id,
//                                                        Nome = "Paciente"
//                                                    }
//                                                }
//                                            }
//                            };

//            // Act
//            farmaciaRepository.Add(newEntity);
//            unitOfWork.SaveChanges();

//            var farmacia = farmaciaRepository.Single(e => e.Nome == "Farmacia",
//                include: s => s.Include(e => e.Pacientes).ThenInclude(r => r.Paciente));

//            var newItem = new RelationFarmaciaPaciente
//                          {
//                              Paciente = new PacienteEntity
//                                         {Nome = "Paciente 02"}
//                          };

//            unitOfWork.Context.Entry(farmacia).Entity.Pacientes.Add(newItem);
//            unitOfWork.SaveChanges();

//            //Assert
//            Assert.Equal("Farmacia", farmacia.Nome);
//            Assert.Equal(2, farmacia.Pacientes.Count);
//            Assert.Equal("Paciente", farmacia.Pacientes.First().Paciente.Nome);
//            Assert.Equal("Paciente 02", farmacia.Pacientes.Last().Paciente.Nome);
//        }
//    }
//}