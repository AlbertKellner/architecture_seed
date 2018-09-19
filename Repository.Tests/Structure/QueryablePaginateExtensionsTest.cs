namespace Repository.Tests.Structure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Paging;
    using DataEntity.Model;
    using FizzWare.NBuilder;
    using Operations;
    using Xunit;

    public class QueryablePaginateExtensionsTest : IClassFixture<InMemoryTestFixture>
    {
        public QueryablePaginateExtensionsTest(InMemoryTestFixture fixture) => _fixture = fixture;

        private readonly InMemoryTestFixture _fixture;

        private static IEnumerable<UsuarioEntity> BuildUsuario() =>
            Builder<UsuarioEntity>.CreateListOfSize(35).Build().ToList();

        [Fact]
        public async Task ToPaginateAsyncTest()
        {
            var unitOfWork = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repositoryAsync = unitOfWork.GetRepositoryAsync<UsuarioEntity>();

            await repositoryAsync.AddAsync(BuildUsuario());
            unitOfWork.SaveChanges();

            var paginate = await repositoryAsync.GetListAsync();

            var page = paginate.Items.ToPaginate(1, 2);

            Assert.NotNull(page);
            Assert.Equal(20, page.Count);
            Assert.Equal(10, page.Pages);
            Assert.Equal(2, page.Size);
        }
    }
}