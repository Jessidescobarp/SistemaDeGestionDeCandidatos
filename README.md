# Sistema de Gestión de Candidatos

## Descripción
El Sistema de Gestión de Candidatos es una aplicación web basada en arquitectura MVC con .NET Core y Entity Framework. La aplicación permite la administración de candidatos para un proceso selectivo, gestionando su información y las experiencias laborales asociadas.
## Estructura del Proyecto

La estructura del proyecto está enfocada en una arquitectura multicapa, con el objetivo de que sea escalable y fácil de mantener.
- **SistemaDeGestionDeCandidatos**: Proyecto principal que incluye la interfaz de usuario y la capa de presentación, utilizando la arquitectura MVC (Modelo-Vista-Controlador).
- **SistemaDeGestionDeCandidatos.Commands**: Responsable de la lógica relacionada con los comandos del sistema (crear, actualizar, eliminar candidatos), siguiendo el patrón CQRS (Command Query Responsibility Segregation).
- **SistemaDeGestionDeCandidatos.Data**: Proyecto encargado del acceso a la base de datos y la persistencia de datos.
- **SistemaDeGestionDeCandidatos.Queries**: Contiene la lógica de consulta (obtención de información de candidatos y experiencias) utilizando el patrón CQRS.
- **SistemaDeGestionDeCandidatos.Services**: ervicios utilizados en el sistema, que incluyen validaciones y otras operaciones relacionadas con la lógica de negocio.

## Características
- Crear, editar y eliminar candidatos.
- Gestionar experiencias laborales de los candidatos.
- Validaciones y restricciones en la creación y edición de candidatos.
- Listado y búsqueda de candidatos.
- Inyección de dependencias y patrones de diseño SOLID aplicados.
- Manejo de excepciones y errores.
- Arquitectura basada en principios de Clean Code y CQRS.

## Tecnologías utilizadas
- **Lenguaje**: C#
- **Framework**: .NET Core 6
- **ORM**: Entity Framework Core
- **Base de Datos**: SQL Server o  SQL in-memory
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap
- **Versionado**: Git
- **IDE**: Visual Studio 2022

## Instalación y configuración
### Prerrequisitos
- .NET Core SDK 6.0 o superior.
- SQL Server (puedes utilizar SQL Server Express) o  SQL in-memory.
- Visual Studio 2022 o Visual Studio Code.
- Git instalado.

### Pasos para la instalación
1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/usuario/repo.git
   cd SistemaDeGestionDeCandidatos

#### Configurar la base de datos

Asegúrate de tener una instancia de SQL Server en ejecución. Sigue estos pasos para configurar la conexión:

1. **Actualiza la cadena de conexión** en `appsettings.json` para que coincida con tu entorno de base de datos. La configuración debería verse así:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;User Id=USUARIO;Password=CONTRASEÑA;"
   }
  - ahora si no deseas conectarte a una BD sql server express, puedes utilizar SQL in-memoery, descopmentando  en el archivo Program.cs
    
  ```
// Configuración para SQL In-Memory
builder.Services.AddDbContext<YourDbContext>(options =>
    options.UseInMemoryDatabase("GestionCanditadosDb"));

y comentando la seccion
 ```
// Configuración para SQL Express (LocalDB)

builder.Services.AddDbContext<YourDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


#### Ejecutar las migraciones para la base de datos
dotnet ef database update
#### Compilar y ejecutar la aplicación
dotnet build
dotnet run

## Uso de la aplicación
1. Registro de un nuevo candidato:

 - Haz clic en el botón "Registrar un nuevo candidato" en la página de inicio.
 - Completa el formulario con los datos del candidato.
2. Gestión de experiencias:

- Haz clic en el botón "Agregar Experiencia" dentro de la tabla de candidatos.
- Completa la información de la experiencia laboral.
3. Eliminación de candidatos:
- Si un candidato tiene experiencias asociadas, se mostrará una advertencia antes de eliminarlo.

  
  
