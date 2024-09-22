using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidate
{
    public class DeleteCandidateCommand
    {
        public int IdCandidate { get; set; }
        public bool DeleteExperiences { get; set; }
    }
}
