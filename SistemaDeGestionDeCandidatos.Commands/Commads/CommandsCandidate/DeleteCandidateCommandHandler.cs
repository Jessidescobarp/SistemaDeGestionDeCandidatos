using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;

namespace SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidate
{
    public class DeleteCandidateCommandHandler
    {
        private readonly GestionCanditadosDbContext _context;

        public DeleteCandidateCommandHandler(GestionCanditadosDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handler para eliminar un candidato se valida la existencia de experiencias si las hay se espera confirmacion de eliminación
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Handle(DeleteCandidateCommand command)
        {
            // Obtener el candidato y sus experiencias
            var candidate = await _context.Candidates
                .Include(c => c.Experiences)
                .FirstOrDefaultAsync(c => c.IdCandidate == command.IdCandidate);

            if (candidate == null)
            {
                throw new Exception("No se encontró el candidato.");
            }

            // Si el comando indica que se deben eliminar también las experiencias
            if (command.DeleteExperiences)
            {
                _context.CandidateExperience.RemoveRange(candidate.Experiences);
            }

            // Eliminar el candidato
            _context.Candidates.Remove(candidate);

            await _context.SaveChangesAsync();
        }
    }
}
