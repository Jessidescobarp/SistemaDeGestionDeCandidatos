using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidate
{
    public class EditCandidateCommandHandler
    {
        private readonly GestionCanditadosDbContext _context;

        public EditCandidateCommandHandler(GestionCanditadosDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EditCandidateCommand command)
        {
            var candidate = await _context.Candidates.FindAsync(command.IdCandidate);
            if (candidate == null)
            {
                throw new Exception("Candidato no encontrado.");
            }

            // Actualiza los campos del candidato
            candidate.Name = command.Name;
            candidate.Surname = command.Surname;
            candidate.Birthdate = command.Birthdate;
            candidate.Email = command.Email;
            candidate.ModifyDate = DateTime.Now;
            // Actualiza otros campos necesarios

            await _context.SaveChangesAsync();
        }
    }
}
