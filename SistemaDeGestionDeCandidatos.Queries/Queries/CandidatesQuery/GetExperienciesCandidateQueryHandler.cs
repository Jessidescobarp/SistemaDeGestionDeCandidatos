using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;

namespace SistemaDeGestionDeCandidatos.Queries.Queries.CandidatesQuery
{
    public class GetExperienciesCandidateQueryHandler
    {
        private readonly GestionCanditadosDbContext _context;
        public GetExperienciesCandidateQueryHandler(GestionCanditadosDbContext context)
        {
            _context = context;
        }

        public async Task<List<CandidateExperience>> ListExperencies(GetExperienciesCandidateQuery query)
        {
            
            var candidateWithExperiences = await _context.Candidates
                .Where(c => c.IdCandidate == query.IdCandidate)
                .Include(c => c.Experiences) 
                .FirstOrDefaultAsync();

            if (candidateWithExperiences == null)
            {
                throw new Exception("No se encontró el candidato.");
            }
            
            return candidateWithExperiences.Experiences.ToList();
        }

    }
}
