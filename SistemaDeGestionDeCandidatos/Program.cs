using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using SistemaDeGestionDeCandidatos.Services; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuraci�n para SQL Express (LocalDB)
builder.Services.AddDbContext<GestionCanditadosDbContext>(options =>
     options.UseInMemoryDatabase("RecruitmentDB"));


// Configura la inyecci�n de dependencias para el servicio de validaci�n
builder.Services.AddScoped<ICandidateValidationService, CandidateValidationService>();

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
