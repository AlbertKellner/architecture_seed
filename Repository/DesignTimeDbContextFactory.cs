namespace Repository
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OnCareContext>
    {
        public OnCareContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OnCareContext>();

            const string connection = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=OnCare;Integrated Security=True;Min Pool Size=0;Max Pool Size=500;Connect Timeout=10;Load Balance Timeout=5;Application Name=OnCareApi";
            //const string connection = @"Data Source=vsrv300;Initial Catalog=OnCare;Persist Security Info=True;User ID=sa;Password=K@lunga!;Min Pool Size=0;Max Pool Size=500;Connect Timeout=15;Application Name=OnCare";

            builder.UseSqlServer(connection);

            return new OnCareContext(builder.Options);
        }
    }
}