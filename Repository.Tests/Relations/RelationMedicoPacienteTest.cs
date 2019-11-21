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

//    public class RelationMedicoPacienteTest : IClassFixture<SqlLiteTestFixture>
//    {
//        public RelationMedicoPacienteTest(SqlLiteTestFixture fixture) => _fixture = fixture;

//        private readonly SqlLiteTestFixture _fixture;

//        [Fact]
//        public void ShouldAddOneNewRelation()
//        {
//            // Arrange 
//            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
//            var medicoRepository = unitOfWork.GetRepository<MedicoEntity>();
//            var usuarioRepository = unitOfWork.GetRepository<UsuarioEntity>();

//            var usuarioEntity = new UsuarioEntity
//            {
//                IdentityId = new Guid().ToString()
//            };
//            usuarioRepository.Add(usuarioEntity);
//            unitOfWork.SaveChanges();

//            var newEntity = new MedicoEntity
//                            {
//                                UsuarioEntityId=usuarioEntity.Id,
//                                Nome = "Medico",
//                                Pacientes = new List<RelationMedicoPaciente>
//                                          {
//                                              new RelationMedicoPaciente
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
//            medicoRepository.Add(newEntity);
//            unitOfWork.SaveChanges();

//            var laboratorio = medicoRepository.Single(e => e.Nome == "Medico",
//                include: s => s.Include(e => e.Pacientes).ThenInclude(r => r.Paciente));

//            //Assert
//            Assert.Equal("Medico", laboratorio.Nome);
//            Assert.Single(laboratorio.Pacientes);
//            Assert.Equal("Paciente", laboratorio.Pacientes.First().Paciente.Nome);
//        }

//        [Fact]
//        public void ShouldAddTwoNewRelations()
//        {
//            // Arrange 
//            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
//            var medicoRepository = unitOfWork.GetRepository<MedicoEntity>();
//            var usuarioRepository = unitOfWork.GetRepository<UsuarioEntity>();

//            var usuarioEntity = new UsuarioEntity
//            {
//                IdentityId = new Guid().ToString()
//            };
//            usuarioRepository.Add(usuarioEntity);
//            unitOfWork.SaveChanges();

//            var newEntity = new MedicoEntity
//                            {
//                                UsuarioEntityId=usuarioEntity.Id,
//                                Nome = "Medico",
//                                Pacientes = new List<RelationMedicoPaciente>
//                                            {
//                                                new RelationMedicoPaciente
//                                                {
//                                                    Paciente = new PacienteEntity
//                                                    {
//                                                        UsuarioEntityId=usuarioEntity.Id,
//                                                        Nome = "Paciente"
//                                                    }
//                                                }
//                                            }
//                            };

//            // Act
//            medicoRepository.Add(newEntity);
//            unitOfWork.SaveChanges();

//            var medico = medicoRepository.Single(e => e.Nome == "Medico",
//                include: s => s.Include(e => e.Pacientes).ThenInclude(r => r.Paciente));

//            var newItem = new RelationMedicoPaciente
//                          {
//                              Paciente = new PacienteEntity
//                                         {Nome = "Paciente 02"}
//                          };

//            unitOfWork.Context.Entry(medico).Entity.Pacientes.Add(newItem);
//            unitOfWork.SaveChanges();

//            //Assert
//            Assert.Equal("Medico", medico.Nome);
//            Assert.Equal(2, medico.Pacientes.Count);
//            Assert.Equal("Paciente", medico.Pacientes.First().Paciente.Nome);
//            Assert.Equal("Paciente 02", medico.Pacientes.Last().Paciente.Nome);
//        }
//    }
//}