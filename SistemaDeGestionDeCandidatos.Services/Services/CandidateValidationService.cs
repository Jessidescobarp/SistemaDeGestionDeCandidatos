using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SistemaDeGestionDeCandidatos.Services { 
    public class CandidateValidationService : ICandidateValidationService
    {
        private readonly GestionCanditadosDbContext _context;

        public CandidateValidationService(GestionCanditadosDbContext context)
        {
            _context = context;
        }

        public async Task ValidateCandidate(Candidates candidates)
        {
            if (await _context.Candidates.AnyAsync(c => c.Email == candidates.Email && c.IdCandidate != candidates.IdCandidate))
            {
                throw new Exception("ya existe un candidato registrado con el Email " + candidates.Email);
            }
            if (await _context.Candidates.AnyAsync(c => c.Email == candidates.Email && candidates.IdCandidate==0))
            {
                throw new Exception("ya existe un candidato registrado con el Email " + candidates.Email);
            }

            if (candidates.Birthdate.Date > DateTime.Now.Date)
            {
                throw new Exception("La fecha de cumpleaños no puede ser mayor a la fecha actual");
            }
            var minimumBirthdate = DateTime.Now.AddYears(-18).Date;
            if (candidates.Birthdate.Date > minimumBirthdate)
            {
                throw new Exception("El candidato debe tener al menos 18 años para poderlo registrar.");
            }
            if (string.IsNullOrWhiteSpace(candidates.Email) || !Regex.IsMatch(candidates.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("El email proporcionado no es válido.");
            }
     

           
        }

    }
}
