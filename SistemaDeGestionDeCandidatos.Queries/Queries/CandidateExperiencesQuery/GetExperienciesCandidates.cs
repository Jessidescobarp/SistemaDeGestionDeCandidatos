using SistemaDeGestionDeCandidatos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Queries.Queries.CandidateExperiencesQuery
{
    public class GetExperienciesCandidates
    {
    
        public int IdCandidateExperience { get; set; } 
        public string? CandidateName { get; set; }
        public string? Company { get; set; }
        public string? Job { get; set; }
        public string? Description { get; set; }
        public decimal Salary { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InsertDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ModifyDate { get; set; }

    }
}
