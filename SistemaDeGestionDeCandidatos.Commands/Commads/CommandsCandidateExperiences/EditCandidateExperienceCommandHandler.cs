using SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidate;
using SistemaDeGestionDeCandidatos.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidateExperiences
{
    public class EditCandidateExperienceCommandHandler
    {
        private readonly GestionCanditadosDbContext _context;

        public EditCandidateExperienceCommandHandler(GestionCanditadosDbContext context)
        {
            _context = context;
        }
        public async Task EditExperience(EditCandidateExperienceCommand command) 
        {
            var candidateExperiences = await _context.CandidateExperience.FindAsync(command.IdCandidateExperience);


            if (candidateExperiences == null) {
                throw new Exception("No se pudo editar la experiencia");
            }
            candidateExperiences.Company = command.Company;
            candidateExperiences.Job = command.job;
            candidateExperiences.Description = command.Description;
            candidateExperiences.BeginDate = command.BeginDate;
            candidateExperiences.EndDate = command.EndDate;
            candidateExperiences.ModifyDate = DateTime.Now;           

            await _context.SaveChangesAsync();
        }


    }
}
