namespace Repository
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();

            const string connection = @"Data Source=NOTEBOOKALBERT\SQLDEVELOPER;Initial Catalog=asd;Integrated Security=True;Connect Timeout=10;Application Name=DotnetSeed";

            builder.UseSqlServer(connection);

            return new DatabaseContext(builder.Options);
        }
    }
}