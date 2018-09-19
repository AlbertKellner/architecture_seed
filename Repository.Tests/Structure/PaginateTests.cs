namespace Repository.Tests.Structure
{
    using System.Collections.Generic;
    using System.Linq;
    using DataEntity.Model;
    using DataEntity.Model.Relations;
    using FizzWare.NBuilder;
    using Microsoft.EntityFrameworkCore;
    using Operations;
    using Xunit;

    public class PaginateTests : IClassFixture<InMemoryTestFixture>
    {
        public PaginateTests(InMemoryTestFixture fixture) => _fixture = fixture;

        private readonly InMemoryTestFixture _fixture;

        private static IEnumerable<FarmaciaEntity> BuildFarmacia() =>
            Builder<FarmaciaEntity>.CreateListOfSize(35)
                .TheFirst(1)
                .With(x => x.Nome = "Farmacia")
                .With(x => x.Pacientes = new List<RelationFarmaciaPaciente>()
                                            {
                                                new RelationFarmaciaPaciente()
                                                {
                                                    Paciente = new PacienteEntity
                                                               {
                                                                   Id = 3939,
                                                                   Nome = "Paciente"
                                                               }
                                                }
                                            })
                .Build()
                .ToList();

        [Fact]
        public void GetPaginate()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repositoryUsuario = unitOfWork.GetRepository<FarmaciaEntity>();

            repositoryUsuario.Add(BuildFarmacia());
            unitOfWork.SaveChanges();

            var page = repositoryUsuario.GetList(predicate: t => t.Nome == "Farmacia", size: 1);

            Assert.Equal(1, page.Items.Count);
        }
    }
}