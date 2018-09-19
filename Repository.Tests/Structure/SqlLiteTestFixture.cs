namespace Repository.Tests.Structure
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class SqlLiteTestFixture : IDisposable
    {
        public TestDbContext Context => SqlLiteInMemoryContext();

        public void Dispose() => Context?.Dispose();

        private static TestDbContext SqlLiteInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>().UseSqlite("DataSource=:memory:").Options;

            var context = new TestDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }
    }
}