# Sistema de Gestión de Candidatos

## Descripción
El Sistema de Gestión de Candidatos es una aplicación web basada en arquitectura MVC con .NET Core y Entity Framework. La aplicación permite la administración de candidatos para un proceso selectivo, gestionando su información y las experiencias laborales asociadas.

## Estructura del Proyecto
La estructura del proyecto está enfocada en una arquitectura multicapa, con el objetivo de que sea escalable y fácil de mantener.
- **SistemaDeGestionDeCandidatos**: Proyecto principal que incluye la interfaz de usuario y la capa de presentación, utilizando la arquitectura MVC (Modelo-Vista-Controlador).
- **SistemaDeGestionDeCandidatos.Commands**: Responsable de la lógica relacionada con los comandos del sistema (crear, actualizar, eliminar candidatos), siguiendo el patrón CQRS (Command Query Responsibility Segregation).
- **SistemaDeGestionDeCandidatos.Data**: Proyecto encargado del acceso a la base de datos y la persistencia de datos.
- **SistemaDeGestionDeCandidatos.Queries**: Contiene la lógica de consulta (obtención de información de candidatos y experiencias) utilizando el patrón CQRS.
- **SistemaDeGestionDeCandidatos.Services**: Servicios utilizados en el sistema, que incluyen validaciones y otras operaciones relacionadas con la lógica de negocio.

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
- **Base de Datos**: SQL Server o SQL in-memory
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap
- **Versionado**: Git
- **IDE**: Visual Studio 2022

## Instalación y configuración

### Prerrequisitos
- .NET Core SDK 6.0 o superior.
- SQL Server (puedes utilizar SQL Server Express) o SQL in-memory.
- Visual Studio 2022 o Visual Studio Code.
- Git instalado.

### Pasos para la instalación
1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/Jessidescobarp/SistemaDeGestionDeCandidatos
   cd SistemaDeGestionDeCandidatos
   ```

### Configurar la base de datos

Asegúrate de tener una instancia de SQL Server en ejecución. Sigue estos pasos para configurar la conexión:

1. **Actualiza la cadena de conexión** en `appsettings.json` para que coincida con tu entorno de base de datos. La configuración debería verse así:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;User Id=USUARIO;Password=CONTRASEÑA;"
   }
   ```

2. Para crear la base de datos en el servidor SQL Server, puedes ejecutar los siguientes scripts:

   ```sql
   -- Crear la base de datos
   CREATE DATABASE GestionCanditadosDb;
   GO

   -- Usar la base de datos recién creada
   USE GestionCanditadosDb;
   GO

   -- Crear la tabla Candidates
   CREATE TABLE Candidates (
       IdCandidate INT IDENTITY(1,1) PRIMARY KEY,   -- Clave primaria con incremento automático
       Name NVARCHAR(100),                     -- Nombre del candidato
       Surname NVARCHAR(100),                  -- Apellido del candidato
       Birthdate DATETIME,                     -- Fecha de nacimiento
       Email NVARCHAR(255),                    -- Correo electrónico
       InsertDate DATETIME, -- Fecha de inserción
       ModifyDate DATETIME   -- Fecha de modificación
   );
   GO

   -- Crear la tabla CandidateExperience
   CREATE TABLE CandidateExperience (
       IdCandidateExperience INT IDENTITY(1,1) PRIMARY KEY,  -- Clave primaria con incremento automático
       IdCandidate INT NOT NULL,                             -- Clave foránea que referencia a Candidates
       Company NVARCHAR(255),                          -- Nombre de la empresa
       Job NVARCHAR(100),                              -- Título del trabajo
       Description NVARCHAR(MAX),                      -- Descripción del trabajo
       Salary DECIMAL(18, 2),                      -- Salario
       BeginDate DATE,                             -- Fecha de inicio
       EndDate DATE,                               -- Fecha de finalización
       InsertDate DATETIME,      -- Fecha de inserción
       ModifyDate DATETIME,      -- Fecha de modificación
       FOREIGN KEY (IdCandidate) REFERENCES Candidates(IdCandidate) -- Clave foránea que referencia a Candidates
   );
   GO
   ```

3. Si no deseas conectarte a una base de datos SQL Server Express, puedes utilizar SQL in-memory, descomentando en el archivo `Program.cs`:

   ```csharp
   // Configuración para SQL In-Memory
   builder.Services.AddDbContext<YourDbContext>(options =>
       options.UseInMemoryDatabase("GestionCanditadosDb"));
   ```

   Y comentando la sección para SQL Express (LocalDB):

   ```csharp
   // Configuración para SQL Express (LocalDB)
   builder.Services.AddDbContext<YourDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
   ```

### Ejecutar las migraciones para la base de datos
```bash
dotnet ef database update
```

### Compilar y ejecutar la aplicación
```bash
dotnet build
dotnet run
```

## Uso de la aplicación
1. **Registro de un nuevo candidato:**
   - Haz clic en el botón "Registrar un nuevo candidato" en la página de inicio.
   - Completa el formulario con los datos del candidato.

2. **Gestión de experiencias:**
   - Haz clic en el botón "Agregar Experiencia" dentro de la tabla de candidatos.
   - Completa la información de la experiencia laboral.

3. **Eliminación de candidatos:**
   - Si un candidato tiene experiencias asociadas, se mostrará una advertencia antes de eliminarlo.
