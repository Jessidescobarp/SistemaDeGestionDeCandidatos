using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;

namespace SistemaDeGestionDeCandidatos.Commands.Commads
{
    public class DeleteCandidateCommandHandler
    {
        private readonly GestionCanditadosDbContext _context;

        public DeleteCandidateCommandHandler(GestionCanditadosDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCandidateCommand command)
        {
            try { 
            var candidate = await _context.Candidates.FindAsync(command.IdCandidate);
            if (candidate != null)
                {
                    _context.Candidates.Remove(candidate);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Candidato con ID {command.IdCandidate} no encontrado.");
                }

            }
            catch
            (Exception ex) {
                throw new Exception("En este momento no se puede Eliminar el Candidato intentelo más tarde o consulte al administrador");
            }
        }
    }
}
