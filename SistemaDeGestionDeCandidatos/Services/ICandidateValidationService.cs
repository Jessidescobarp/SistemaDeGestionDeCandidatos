namespace SistemaDeGestionDeCandidatos.Services
{
    public interface ICandidateValidationService
    {
        Task<bool> ValidateCandidateExists(string email);
        bool ValidateBirthdate(DateTime birthdate);
    }
}
