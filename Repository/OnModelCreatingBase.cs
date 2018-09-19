namespace Repository
{
    using DataEntity;
    using Microsoft.EntityFrameworkCore;

    public class OnModelCreatingBase<T> where T : BaseEntity
    {
        public static void MapBaseEntity(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<T>().HasKey("Id");

            //modelBuilder.Entity<T>().Property(b => b.AddedDate)
            //    .IsRequired()
            //    .HasColumnName("AddedDate")
            //    .HasColumnType("datetime2(7)");

            //modelBuilder.Entity<T>().Property(b => b.ModifiedDate)
            //    .IsRequired()
            //    .HasColumnName("ModifiedDate")
            //    .HasColumnType("datetime2(7)");
        }
    }
}