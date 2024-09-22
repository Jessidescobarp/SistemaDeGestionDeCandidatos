using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Models;


namespace SistemaDeGestionDeCandidatos.Context
{
    public class GestionCanditadosDbContext:DbContext
    {
        public GestionCanditadosDbContext(DbContextOptions<GestionCanditadosDbContext> options) : base(options){}

        // Se definen DbSets que estan en la base de datos
        public DbSet<Candidates> Candidates { get; set; } = null!;
        public DbSet<CandidateExperience> Experiences { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidates>()
                .HasMany(c => c.Experiences)
                .WithOne(e => e.Candidate)
                .HasForeignKey(e => e.IdCandidate);
            base.OnModelCreating(modelBuilder);
        }
    }

}
