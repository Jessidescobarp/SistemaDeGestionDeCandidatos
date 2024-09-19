using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Services
{
    public class CandidateValidationService : ICandidateValidationService
    {
        private readonly GestionCanditadosDbContext _context;

        public CandidateValidationService(GestionCanditadosDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateCandidateExists(string email)
        {
            return await _context.Candidates.AnyAsync(c => c.Email == email);
        }

        public bool ValidateBirthdate(DateTime birthdate)
        {            
            return birthdate <= DateTime.Now;
        }
    }
}
