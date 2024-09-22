using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using System.Threading.Tasks;


namespace SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidate
{


    public class CreateCandidateCommandHandler
    {
  
        private readonly GestionCanditadosDbContext _context;

        /// <summary>
        /// Creacion del constructor, e inyección de dependecia GestionCanditadosDbContext
        /// </summary>
        public CreateCandidateCommandHandler(GestionCanditadosDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// handler para crear un candidato
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Handle(CreateCandidateCommand command)
        {
            var candidate = new Candidates
            {
                Name = command.Name,
                Surname = command.Surname,
                Birthdate = command.Birthdate,
                Email = command.Email,
                InsertDate = DateTime.Now,
                ModifyDate = null
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }
    }

}
