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

    public class RelationLaboratorioMedicoTest : IClassFixture<SqlLiteTestFixture>
    {
        public RelationLaboratorioMedicoTest(SqlLiteTestFixture fixture) => _fixture = fixture;

        private readonly SqlLiteTestFixture _fixture;

        [Fact]
        public void ShouldAddOneNewRelation()
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
                                Medicos = new List<RelationLaboratorioMedico>
                                          {
                                              new RelationLaboratorioMedico
                                              {
                                                  Medico = new MedicoEntity
                                                  {
                                                      UsuarioEntityId=usuarioEntity.Id,
                                                      Nome = "Medico"
                                                  }
                                              }
                                          }
                            };

            // Act
            laboratorioRepository.Add(newEntity);
            unitOfWork.SaveChanges();

            var laboratorio = laboratorioRepository.Single(e => e.Nome == "Laboratorio",
                include: s => s.Include(e => e.Medicos).ThenInclude(r => r.Medico));

            //Assert
            Assert.Equal("Laboratorio", laboratorio.Nome);
            Assert.Single(laboratorio.Medicos);
            Assert.Equal("Medico", laboratorio.Medicos.First().Medico.Nome);
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
                                UsuarioEntityId=usuarioEntity.Id,
                                Nome = "Laboratorio",
                                Medicos = new List<RelationLaboratorioMedico>
                                          {
                                              new RelationLaboratorioMedico
                                              {
                                                  Medico = new MedicoEntity
                                                  {
                                                      UsuarioEntityId=usuarioEntity.Id,
                                                      Nome = "Medico"
                                                  }
                                              }
                                          }
                            };

            // Act
            laboratorioRepository.Add(newEntity);
            unitOfWork.SaveChanges();

            var laboratorio = laboratorioRepository.Single(e => e.Nome == "Laboratorio",
                include: s => s.Include(e => e.Medicos).ThenInclude(r => r.Medico));

            var newItem = new RelationLaboratorioMedico
                          {
                              Medico = new MedicoEntity
                                       {Nome = "Medico 02"}
                          };

            unitOfWork.Context.Entry(laboratorio).Entity.Medicos.Add(newItem);
            unitOfWork.SaveChanges();

            //Assert
            Assert.Equal("Laboratorio", laboratorio.Nome);
            Assert.Equal(2, laboratorio.Medicos.Count);
            Assert.Equal("Medico", laboratorio.Medicos.First().Medico.Nome);
            Assert.Equal("Medico 02", laboratorio.Medicos.Last().Medico.Nome);
        }
    }
}