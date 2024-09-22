using SistemaDeGestionDeCandidatos.Models;

namespace SistemaDeGestionDeCandidatos.Services
{
    public interface ICandidateValidationService
    {
        Task ValidateCandidate(Candidates candidates);

    }
}
