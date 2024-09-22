
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidate;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using SistemaDeGestionDeCandidatos.Queries.Queries.CandidatesQuery;
using SistemaDeGestionDeCandidatos.Services;


namespace SistemaDeGestionDeCandidatos.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ICandidateValidationService _validationService;
        private readonly GestionCanditadosDbContext _context;
        private readonly CreateCandidateCommandHandler _createCandidate;
        private readonly GetCandidatesQueryHandler _getCandidates;
        private readonly DeleteCandidateCommandHandler _deleteCandidate;
        private readonly EditCandidateCommandHandler _editCandidate;
        private readonly GetExperienciesCandidateQueryHandler _getCandidateExperiencies;


        public CandidatesController(ICandidateValidationService validationService, 
            GestionCanditadosDbContext context,
            CreateCandidateCommandHandler createCandidate,
            GetCandidatesQueryHandler getCandidates,
            DeleteCandidateCommandHandler DeleteCandidate,
            EditCandidateCommandHandler EditCandidate,
            GetExperienciesCandidateQueryHandler getCandidateExperiencies)
        {
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
            _context = context;
            _createCandidate = createCandidate;
            _getCandidates = getCandidates;
            _deleteCandidate = DeleteCandidate;
            _editCandidate = EditCandidate;
            _getCandidateExperiencies = getCandidateExperiencies;
            
        }

        /// <summary>
        /// Controlador para obtener todos los canditados también se pude filtrar
   
        /// </summary>
        /// <param name="searchTerm">Nombre,aperllido, email</param>
        /// <returns> a la vista index con todos los candidatos o el filtro</returns>
        public async Task<IActionResult> Index(string searchTerm)
        {
            try 
            {
                var query = new GetCandidatesQuery { SearchTerm = searchTerm };
                var candidates = await _getCandidates.Handle(query);
                return View(candidates);

            }
            catch (DbUpdateException dbEx)
            {
                ModelState.AddModelError("", "No se pudo listar los candidatos intente más tarde. Intente nuevamente.");
                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }

       /// <summary>
       /// Controlador para mostrar crear un candidato
       /// </summary>
       /// <returns>vista</returns>
        public IActionResult Create()
        {
            return View();
        }

         /// <summary>
         /// Controlador para crear un nuevo candidato
         /// </summary>
         /// <param name="candidates"></param>
         /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCandidateCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var candidates = new Candidates
                    {
                        Name = command.Name,
                        Surname = command.Surname,
                        Birthdate = command.Birthdate,
                        Email = command.Email
                    };
                    await _validationService.ValidateCandidate(candidates);

                    await _createCandidate.Handle(command);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dbEx)
            {                
                ModelState.AddModelError("", "No se pudo guardar los cambios en la base de datos. Intente nuevamente.");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);   
            }

            // Si llegamos aquí, algo salió mal, volvemos a mostrar la vista con los datos del candidato
            return View();
        }


        /// <summary>
        /// Controlador mostra el canditado a editar
        /// </summary>
        /// <param name="id"> id del candidato</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Candidates == null)
            {
                return NotFound();
            }
            
            var candidates = await _context.Candidates.FindAsync(id);


            if (candidates == null)
            {
                return NotFound();
            }
            return View(candidates);
        }

        /// <summary>
        /// Controlador para editar un candidato
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCandidateCommand command)
        {

            var candidate = new Candidates
            {
                IdCandidate = command.IdCandidate,
                Name = command.Name,
                Surname = command.Surname,
                Email = command.Email,
                Birthdate = command.Birthdate,
                ModifyDate = DateTime.Now
            };

            try
            {
                if (ModelState.IsValid)
                {               
                    await _validationService.ValidateCandidate(candidate);             
                    await _editCandidate.Handle(command);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dbEx)
            {
                ModelState.AddModelError("", "No se pudo guardar los cambios en la base de datos. Intente nuevamente.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(candidate);
        }
           
        
        

         /// <summary>
         /// Controlador para eliminar un candidato, tiene validadción si el candidato tiene experiencias
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var query = new GetExperienciesCandidateQuery { IdCandidate = id };
                var experiences = await _getCandidateExperiencies.ListExperencies(query);
                if ( experiences != null && experiences.Count!=0) {
                    TempData["HasExperiences"] = true;
                    TempData["CandidateId"] = id;
                    TempData["ExperiencesCount"] = experiences.Count;
                    return RedirectToAction(nameof(ConfirmDelete));
                }

                var command = new DeleteCandidateCommand { IdCandidate = id }; 
                await _deleteCandidate.Handle(command);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", await _getCandidates.Handle(new GetCandidatesQuery())); 
            }

        }
        /// <summary>
        /// Controlador para validar confirmación eliminar un usuario
        /// </summary>
        /// <returns></returns>
        public IActionResult ConfirmDelete()
        {
            var candidateId = TempData["CandidateId"];
            var experiencesCount = TempData["ExperiencesCount"];

            ViewBag.CandidateId = candidateId;
            ViewBag.ExperiencesCount = experiencesCount;
            return View();
        }

        /// <summary>
        /// Controlador para confirmar si un candidato tiene experiencias
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedWithExperiences(int id)
        {
            try
            {
                // Ejecutar el comando para eliminar al candidato y sus experiencias
                var command = new DeleteCandidateCommand { IdCandidate = id, DeleteExperiences = true };
                await _deleteCandidate.Handle(command);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al intentar eliminar el candidato: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        private bool CandidatesExists(int id)
        {
          return (_context.Candidates?.Any(e => e.IdCandidate == id)).GetValueOrDefault();
        }
    }
}
