namespace Repository.Tests.Structure
{
    using Microsoft.EntityFrameworkCore;
    using Model;

    public class TestDbContext : OnCareContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PokemonTestEntity> PokemonTestEntity { get; set; }
    }
}