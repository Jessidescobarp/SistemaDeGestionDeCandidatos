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

        /// <summary>
        /// Handler para editar un candidato
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Handle(EditCandidateCommand command)
        {
            var candidate = await _context.Candidates.FindAsync(command.IdCandidate);
            if (candidate == null)
            {
                throw new Exception("Candidato no encontrado.");
            }            
            candidate.Name = command.Name;
            candidate.Surname = command.Surname;
            candidate.Birthdate = command.Birthdate;
            candidate.Email = command.Email;
            candidate.ModifyDate = DateTime.Now;         

            await _context.SaveChangesAsync();
        }
    }
}
