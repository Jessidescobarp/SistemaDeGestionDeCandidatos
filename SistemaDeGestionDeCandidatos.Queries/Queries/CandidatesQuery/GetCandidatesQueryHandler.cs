using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using SistemaDeGestionDeCandidatos.Queries.Queries;

namespace SistemaDeGestionDeCandidatos.Queries.Queries.CandidatesQuery
{
    public class GetCandidatesQueryHandler
    {
        private readonly GestionCanditadosDbContext _context;

        public GetCandidatesQueryHandler(GestionCanditadosDbContext context)
        {
            _context = context;
        }
        public async Task<List<SistemaDeGestionDeCandidatos.Models.Candidates>> Handle(GetCandidatesQuery query)
        {

            try
            {
                // Obtén la lista de candidatos
                var candidates = await _context.Candidates
                    .Where(c => string.IsNullOrEmpty(query.SearchTerm)
                        || c.Name.Contains(query.SearchTerm)
                        || c.Surname.Contains(query.SearchTerm)
                        || c.Email.Contains(query.SearchTerm))
                    .ToListAsync();

                return candidates;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de candidatos: " + ex.Message);
            }
        }
    }
}
