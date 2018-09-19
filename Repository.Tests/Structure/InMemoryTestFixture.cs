namespace Repository.Tests.Structure
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class InMemoryTestFixture : IDisposable
    {
        public TestDbContext Context => InMemoryContext();

        public void Dispose() => Context?.Dispose();

        private TestDbContext InMemoryContext() =>
            new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options);
    }
}