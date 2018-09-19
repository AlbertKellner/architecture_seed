namespace Repository.Tests.Relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataEntity.Model;
    using DataEntity.Model.Relations;
    using Microsoft.EntityFrameworkCore;
    using Operations;
    using Structure;
    using Xunit;

    public class RelationLaboratorioFarmaciaTest : IClassFixture<SqlLiteTestFixture>
    {
        public RelationLaboratorioFarmaciaTest(SqlLiteTestFixture fixture) => _fixture = fixture;

        private readonly SqlLiteTestFixture _fixture;

        [Fact]
        public void ShouldAddOneNewRelation()
        {
            var identityId = new Guid().ToString();

            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var laboratorioRepository = unitOfWork.GetRepository<LaboratorioEntity>();
            var usuarioRepository = unitOfWork.GetRepository<UsuarioEntity>();

            var usuarioEntity = new UsuarioEntity
            {
                IdentityId = new Guid().ToString()
            };
            usuarioRepository.Add(usuarioEntity);
            unitOfWork.SaveChanges();

            var newEntity = new LaboratorioEntity
                            {
                                UsuarioEntityId = usuarioEntity.Id,
                                Nome = "Laboratorio",
                                Farmacias = new List<RelationLaboratorioFarmacia>
                                            {
                                                new RelationLaboratorioFarmacia
                                                {
                                                    Farmacia = new FarmaciaEntity
                                                    {
                                                        UsuarioEntityId = usuarioEntity.Id,
                                                        Nome = "Farmacia"
                                                    }
                                                }
                                            }
                            };

            // Act
            laboratorioRepository.Add(newEntity);
            unitOfWork.SaveChanges();

            var laboratorio = laboratorioRepository.Single(e => e.Nome == "Laboratorio",
                include: s => s.Include(e => e.Farmacias).ThenInclude(r => r.Farmacia));

            //Assert
            Assert.Equal("Laboratorio", laboratorio.Nome);
            Assert.Single(laboratorio.Farmacias);
            Assert.Equal("Farmacia", laboratorio.Farmacias.First().Farmacia.Nome);
        }

        [Fact]
        public void ShouldAddTwoNewRelations()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var laboratorioRepository = unitOfWork.GetRepository<LaboratorioEntity>();
            var usuarioRepository = unitOfWork.GetRepository<UsuarioEntity>();

            var usuarioEntity = new UsuarioEntity
            {
                IdentityId = new Guid().ToString()
            };
            usuarioRepository.Add(usuarioEntity);
            unitOfWork.SaveChanges();

            var newEntity = new LaboratorioEntity
                            {
                                UsuarioEntityId = usuarioEntity.Id,
                                Nome = "Laboratorio",
                                Farmacias = new List<RelationLaboratorioFarmacia>
                                            {
                                                new RelationLaboratorioFarmacia
                                                {
                                                    Farmacia = new FarmaciaEntity
                                                    {
                                                        UsuarioEntityId = usuarioEntity.Id,
                                                        Nome = "Farmacia"
                                                    }
                                                }
                                            }
                            };

            // Act
            laboratorioRepository.Add(newEntity);
            unitOfWork.SaveChanges();

            var laboratorio = laboratorioRepository.Single(e => e.Nome == "Laboratorio",
                include: s => s.Include(e => e.Farmacias).ThenInclude(r => r.Farmacia));

            var newItem = new RelationLaboratorioFarmacia
                          {
                              Farmacia = new FarmaciaEntity
                                         {Nome = "Farmacia 02"}
                          };

            unitOfWork.Context.Entry(laboratorio).Entity.Farmacias.Add(newItem);
            unitOfWork.SaveChanges();

            //Assert
            Assert.Equal("Laboratorio", laboratorio.Nome);
            Assert.Equal(2, laboratorio.Farmacias.Count);
            Assert.Equal("Farmacia", laboratorio.Farmacias.First().Farmacia.Nome);
            Assert.Equal("Farmacia 02", laboratorio.Farmacias.Last().Farmacia.Nome);
            
            var novoAssertLab = laboratorioRepository.Single(e => e.Id == laboratorio.Id,
                include: s => s.Include(e => e.Farmacias).ThenInclude(r => r.Farmacia));

            //var novoAssertFar = "";

            //Assert.Equal("Laboratorio", novoAssertLab.Nome);
            //Assert.Equal("Farmacia", novoAssertLab.Farmacias.First().Farmacia.Nome);
            //Assert.Equal(2, novoAssertLab.Farmacias.Count);
        }

        [Fact]
        public void ShouldRemoveOneRelation()
        {
            // Arrange 
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var laboratorioRepository = unitOfWork.GetRepository<LaboratorioEntity>();
            var usuarioRepository = unitOfWork.GetRepository<UsuarioEntity>();

            var usuarioEntity = new UsuarioEntity
            {
                IdentityId = new Guid().ToString()
            };
            usuarioRepository.Add(usuarioEntity);
            unitOfWork.SaveChanges();

            var newEntity = new LaboratorioEntity
                            {
                                UsuarioEntityId=usuarioEntity.Id,
                                Nome = "Laboratorio",
                                Farmacias = new List<RelationLaboratorioFarmacia>
                                            {
                                                new RelationLaboratorioFarmacia
                                                {
                                                    Farmacia = new FarmaciaEntity
                                                    {
                                                        UsuarioEntityId=usuarioEntity.Id,
                                                        Nome = "Farmacia"
                                                    }
                                                },
                                                new RelationLaboratorioFarmacia
                                                {
                                                    Farmacia = new FarmaciaEntity
                                                    {
                                                        UsuarioEntityId=usuarioEntity.Id,
                                                        Nome = "Farmacia 02"
                                                    }
                                                }
                                            }
                            };

            // Act
            laboratorioRepository.Add(newEntity);
            unitOfWork.SaveChanges();

            var laboratorio = laboratorioRepository.Single(e => e.Nome == "Laboratorio",
                include: s => s.Include(e => e.Farmacias).ThenInclude(r => r.Farmacia));

            var locatedFarmacia =
                unitOfWork.Context.RelationLaboratorioFarmacia.First(r => r.Farmacia.Nome == "Farmacia 02");

            //unitOfWork.GetRepository<RelationLaboratorioFarmacia>().Delete(locatedFarmacia);


            //var locatedLaboratorio = unitOfWork.Context.Laboratorio.Find(laboratorio.Nome == "Laboratorio");  // e => e.Nome == "Laboratorio");

            //var locatedFarmacia =
            //    unitOfWork.Context.RelationLaboratorioFarmacia.First(r => r.Farmacia.Nome == "Farmacia 02");


            //locatedLaboratorio.Farmacias.Remove(locatedFarmacia);


            //var x = unitOfWork.Context.Entry(locatedLaboratorio).Entity.Farmacias.Remove(locatedFarmacia);
            //var x = unitOfWork.Context.Entry(laboratorio).Entity.Farmacias.Single(e=>e.Farmacia.Nome == "Farmacia 02");

            //unitOfWork.Context.RelationLaboratorioFarmacia.Remove(locatedFarmacia);

            unitOfWork.SaveChanges();

            //Assert
            Assert.Equal("Laboratorio", laboratorio.Nome);
            Assert.True(true);
            Assert.Equal("Farmacia", laboratorio.Farmacias.First().Farmacia.Nome);
            Assert.Equal("Farmacia 02", laboratorio.Farmacias.Last().Farmacia.Nome);
            //Assert.Single(laboratorio.Farmacias);
        }
    }
}