using Moq;
using System.Threading.Tasks;
using Xunit;
using SistemaDeGestionDeCandidatos.Commands;
using SistemaDeGestionDeCandidatos.Context;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidate;


public class CreateCandidateCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateCandidate_WhenCommandIsValid()
    {
        // Arrange: Configuramos la base de datos en memoria
        var options = new DbContextOptionsBuilder<GestionCanditadosDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateCandidateTestDb")
            .Options;

        var context = new GestionCanditadosDbContext(options);

        var command = new CreateCandidateCommand
        {
            Name = "Test Name",
            Surname = "Test Surname",
            Email = "test@example.com"
        };

        var handler = new CreateCandidateCommandHandler(context);

        // Act: Llamamos al método Handle
        await handler.Handle(command);

        // Assert: Verificamos que se ha agregado el candidato correctamente
        var candidate = await context.Candidates.FirstOrDefaultAsync(c => c.Email == "test@example.com");
        Assert.NotNull(candidate);
        Assert.Equal("Test Name", candidate.Name);
    }
}
