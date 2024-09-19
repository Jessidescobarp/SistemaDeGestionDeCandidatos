using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Models;


namespace SistemaDeGestionDeCandidatos.Context
{
    public class GestionCanditadosDbContext:DbContext
    {
        public GestionCanditadosDbContext(DbContextOptions<GestionCanditadosDbContext> options) : base(options)
        {
        }

        // Se definen DbSets que estan en la base de datos
        public DbSet<Candidates> Candidates { get; set; } = null!;
        public DbSet<CandidateExperience> Experiences { get; set; } = null!;
    }

}
