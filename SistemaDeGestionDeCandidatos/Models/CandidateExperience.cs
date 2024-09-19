using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeGestionDeCandidatos.Models
{
    public class CandidateExperience
    {
        [Key]
        public int IdCandidateExperience { get; set; }
        public int IdCandidate { get; set; }

        [ForeignKey("IdCandidate")]
        public Candidates? Candidate { get; set; }
        public string? Company { get; set; }
        public string? Job { get; set; }
        public string? Description { get; set; }
        public decimal Salary { get; set; }
        public DateOnly BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }

}
