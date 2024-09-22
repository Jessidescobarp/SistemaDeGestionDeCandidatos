using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using SistemaDeGestionDeCandidatos.Queries.Queries.CandidatesQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Queries.Queries.CandidateExperiencesQuery
{
    public class GetExperienciesCandidatesHandler
    {
        private readonly GestionCanditadosDbContext _context;

        public GetExperienciesCandidatesHandler(GestionCanditadosDbContext context)
        {
            _context = context;

        }

        public async Task<List<GetExperienciesCandidates>> Handle()
        {

            try
            {               
                var candidateExperiences = await _context.CandidateExperience
                    .Include(c => c.Candidate)
                    .Select(experience=>new GetExperienciesCandidates
                    {
                        IdCandidateExperience = experience.IdCandidate,
                        CandidateName = experience.Candidate.Name,
                        Company = experience.Company,
                        Job = experience.Job,
                        Description = experience.Description,
                        Salary = experience.Salary,
                        BeginDate = experience.BeginDate,
                        EndDate= experience.EndDate
                    })
                    .ToListAsync();            



                return candidateExperiences;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de experiencias de los candidatos: " + ex.Message);
            }
        }
    }
}
