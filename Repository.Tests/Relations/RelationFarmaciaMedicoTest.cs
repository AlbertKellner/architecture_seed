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

//    public class RelationFarmaciaMedicoTest : IClassFixture<SqlLiteTestFixture>
//    {
//        public RelationFarmaciaMedicoTest(SqlLiteTestFixture fixture) => _fixture = fixture;

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
//                                UsuarioEntityId = usuarioEntity.Id,
//                                Nome = "Farmacia",
//                                Medicos = new List<RelationFarmaciaMedico>
//                                          {
//                                              new RelationFarmaciaMedico
//                                              {
//                                                  Medico = new MedicoEntity
//                                                  {
//                                                      UsuarioEntityId = usuarioEntity.Id,
//                                                      Nome = "Medico"
//                                                  }
//                                              }
//                                          }
//                            };

//            // Act
//            farmaciaRepository.Add(newEntity);
//            unitOfWork.SaveChanges();

//            var laboratorio = farmaciaRepository.Single(e => e.Nome == "Farmacia",
//                include: s => s.Include(e => e.Medicos).ThenInclude(r => r.Medico));

//            //Assert
//            Assert.Equal("Farmacia", laboratorio.Nome);
//            Assert.Single(laboratorio.Medicos);
//            Assert.Equal("Medico", laboratorio.Medicos.First().Medico.Nome);
//        }

//        //[Fact]
//        //public void ShouldAlterOneRelation()
//        //{
//        //    // Arrange 
//        //    var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
//        //    var repository = unitOfWork.GetRepository<FarmaciaEntity>();

//        //    var newEntity = new FarmaciaEntity
//        //                    {
//        //                        Nome = "Farmacia",
//        //                        Medicos = new List<RelationFarmaciaMedico>
//        //                                  {
//        //                                      new RelationFarmaciaMedico
//        //                                      {
//        //                                          Medico = new MedicoEntity
//        //                                                   {Nome = "Medico"}
//        //                                      },
//        //                                      new RelationFarmaciaMedico
//        //                                      {
//        //                                          Medico = new MedicoEntity
//        //                                                   {Nome = "Medico 02"}
//        //                                      }
//        //                                  }
//        //                    };

//        //    // Act
//        //    repository.Add(newEntity);
//        //    unitOfWork.SaveChanges();

//        //    var medico = unitOfWork.GetRepository<MedicoEntity>().Single(e => e.Id == 2);
//        //    Assert.Equal("Medico 02", medico.Nome);
//        //    medico.Nome = "Medico 03";

//        //    var laboratorio = repository.Single(e => e.Nome == "Farmacia",
//        //        include: s => s.Include(e => e.Medicos).ThenInclude(r => r.Medico));

//        //    Assert.Equal("Medico 03", medico.Nome);
//        //    unitOfWork.Context.Entry(medico).CurrentValues.SetValues(medico);
//        //    //unitOfWork.Context.Entry(medico).State = EntityState.Modified;
//        //    unitOfWork.SaveChanges();

//        //    //Assert
//        //    Assert.Equal("Farmacia", laboratorio.Nome);
//        //    Assert.Equal(2, laboratorio.Medicos.Count);
//        //    Assert.Equal("Medico 03", laboratorio.Medicos.Last().Medico.Nome);
//        //}

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
//                                UsuarioEntityId=usuarioEntity.Id,
//                                Nome = "Farmacia",
//                                Medicos = new List<RelationFarmaciaMedico>
//                                            {
//                                                new RelationFarmaciaMedico
//                                                {
//                                                    Medico = new MedicoEntity
//                                                    {
//                                                        UsuarioEntityId=usuarioEntity.Id,
//                                                        Nome = "Medico"
//                                                    }
//                                                }
//                                            }
//                            };

//            // Act
//            farmaciaRepository.Add(newEntity);
//            unitOfWork.SaveChanges();

//            var farmacia = farmaciaRepository.Single(e => e.Nome == "Farmacia",
//                include: s => s.Include(e => e.Medicos).ThenInclude(r => r.Medico));

//            var newItem = new RelationFarmaciaMedico
//                          {
//                              Medico = new MedicoEntity
//                                         {Nome = "Medico 02"}
//                          };

//            unitOfWork.Context.Entry(farmacia).Entity.Medicos.Add(newItem);
//            unitOfWork.SaveChanges();

//            //Assert
//            Assert.Equal("Farmacia", farmacia.Nome);
//            Assert.Equal(2, farmacia.Medicos.Count);
//            Assert.Equal("Medico", farmacia.Medicos.First().Medico.Nome);
//            Assert.Equal("Medico 02", farmacia.Medicos.Last().Medico.Nome);
//        }
//    }
//}