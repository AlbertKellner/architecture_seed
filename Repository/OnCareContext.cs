namespace Repository
{
    using DataEntity;
    using DataEntity.Model;
    using DataEntity.Model.Relations;
    using Microsoft.EntityFrameworkCore;

    public class OnCareContext : DbContext
    {
        public OnCareContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UsuarioEntity> Usuarios { get; set; }

        public DbSet<RelationLaboratorioFarmacia> RelationLaboratorioFarmacia { get; set; }

        public DbSet<LaboratorioEntity> Laboratorio { get; set; }
        public DbSet<FarmaciaEntity> Farmacia { get; set; }
        public DbSet<MedicoEntity> Medico { get; set; }
        public DbSet<PacienteEntity> Paciente { get; set; }

        public DbSet<TaskEntity> Task { get; set; }
        public DbSet<TaskListEntity> TaskList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .EnableSensitiveDataLogging();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnModelCreatingBase<UsuarioEntity>.MapBaseEntity(modelBuilder);

            MapRelationLaboratorioFarmacia(modelBuilder);

            MapRelationLaboratorioMedico(modelBuilder);

            MapRelationFarmaciaMedico(modelBuilder);

            MapRelationFarmaciaPaciente(modelBuilder);

            MapRelationMedicoPaciente(modelBuilder);
        }

        private static void MapRelationLaboratorioFarmacia(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationLaboratorioFarmacia>()
                .HasKey(e => new {e.LaboratorioId, e.FarmaciaId});

            modelBuilder.Entity<RelationLaboratorioFarmacia>()
                .HasOne(e => e.Laboratorio)
                .WithMany(e => e.Farmacias)
                .HasForeignKey(e => e.LaboratorioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelationLaboratorioFarmacia>()
                .HasOne(e => e.Farmacia)
                .WithMany(e => e.Laboratorios)
                .HasForeignKey(e => e.FarmaciaId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void MapRelationLaboratorioMedico(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationLaboratorioMedico>()
                .HasKey(e => new {e.LaboratorioId, e.MedicoId});

            modelBuilder.Entity<RelationLaboratorioMedico>()
                .HasOne(e => e.Laboratorio)
                .WithMany(e => e.Medicos)
                .HasForeignKey(e => e.LaboratorioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelationLaboratorioMedico>()
                .HasOne(e => e.Medico)
                .WithMany(e => e.Laboratorios)
                .HasForeignKey(e => e.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void MapRelationFarmaciaMedico(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationFarmaciaMedico>()
                .HasKey(e => new {e.FarmaciaId, e.MedicoId});

            modelBuilder.Entity<RelationFarmaciaMedico>()
                .HasOne(e => e.Farmacia)
                .WithMany(e => e.Medicos)
                .HasForeignKey(e => e.FarmaciaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelationFarmaciaMedico>()
                .HasOne(e => e.Medico)
                .WithMany(e => e.Farmacias)
                .HasForeignKey(e => e.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void MapRelationFarmaciaPaciente(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationFarmaciaPaciente>()
                .HasKey(e => new {e.FarmaciaId, e.PacienteId});

            modelBuilder.Entity<RelationFarmaciaPaciente>()
                .HasOne(e => e.Farmacia)
                .WithMany(e => e.Pacientes)
                .HasForeignKey(e => e.FarmaciaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelationFarmaciaPaciente>()
                .HasOne(e => e.Paciente)
                .WithMany(e => e.Farmacias)
                .HasForeignKey(e => e.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void MapRelationMedicoPaciente(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationMedicoPaciente>()
                .HasKey(e => new {e.MedicoId, e.PacienteId});

            modelBuilder.Entity<RelationMedicoPaciente>()
                .HasOne(e => e.Medico)
                .WithMany(e => e.Pacientes)
                .HasForeignKey(e => e.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelationMedicoPaciente>()
                .HasOne(e => e.Paciente)
                .WithMany(e => e.Medicos)
                .HasForeignKey(e => e.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}