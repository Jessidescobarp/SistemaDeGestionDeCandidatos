using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidate;
using SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidateExperiences;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using SistemaDeGestionDeCandidatos.Queries.Queries.CandidateExperiencesQuery;
using SistemaDeGestionDeCandidatos.Queries.Queries.CandidatesQuery;
using SistemaDeGestionDeCandidatos.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuraci�n para SQL Express (LocalDB)
builder.Services.AddDbContext<GestionCanditadosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//// Configuraci�n para SQL in-memory 
//builder.Services.AddDbContext<GestionCanditadosDbContext>(options =>
//     options.UseInMemoryDatabase("GestionCanditadosDb"));


// Configura la inyecci�n de dependencias para el servicio de validaci�n
builder.Services.AddScoped<ICandidateValidationService, CandidateValidationService>();
builder.Services.AddScoped<GetCandidatesQueryHandler>();
builder.Services.AddScoped<CreateCandidateCommandHandler>();
builder.Services.AddScoped<DeleteCandidateCommandHandler>();
builder.Services.AddScoped<EditCandidateCommandHandler>();
builder.Services.AddScoped<GetExperienciesCandidateQueryHandler>();
builder.Services.AddScoped<GetExperienciesCandidatesHandler>();
builder.Services.AddScoped<EditCandidateExperienceCommandHandler>();
builder.Services.AddScoped <CreateCandidateExperienceCommandHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // P�gina de error para producci�n
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Candidates}/{action=Index}/{id?}");

app.Run();
