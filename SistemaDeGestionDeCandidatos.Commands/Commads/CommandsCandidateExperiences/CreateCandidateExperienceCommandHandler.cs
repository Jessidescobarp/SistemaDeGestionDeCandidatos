using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidateExperiences
{
    public class CreateCandidateExperienceCommandHandler
    {
        private readonly GestionCanditadosDbContext _context;

        public CreateCandidateExperienceCommandHandler(GestionCanditadosDbContext context)
        {
            _context = context;
        }

        public async Task CreateExperience(CreateCandidateExperienceCommand command)
        {

            var candidateExperiences = new CandidateExperience
            {
                IdCandidate = command.idCandidate,
                Company = command.Company,
                Job = command.job,
                Description = command.Description,
                BeginDate = command.BeginDate,
                EndDate = command.EndDate,
                InsertDate = DateTime.Now,
                ModifyDate = null
            };

            if (candidateExperiences == null)
            {
                throw new Exception("No se pudo editar la experiencia");
            }
            _context.CandidateExperience.Add(candidateExperiences);
            await _context.SaveChangesAsync();
        }
    }
}
