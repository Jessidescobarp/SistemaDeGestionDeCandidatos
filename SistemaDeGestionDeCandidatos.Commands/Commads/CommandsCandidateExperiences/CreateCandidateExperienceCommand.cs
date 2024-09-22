using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidateExperiences
{
    public class CreateCandidateExperienceCommand
    {
        public int idCandidate {  get; set; }
        public string Company {  get; set; }
        public string job { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
