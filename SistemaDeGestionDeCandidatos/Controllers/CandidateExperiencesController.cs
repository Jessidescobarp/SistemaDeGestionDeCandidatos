using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using SistemaDeGestionDeCandidatos.Queries.Queries.CandidateExperiencesQuery;
using SistemaDeGestionDeCandidatos.Commands.Commads.CommandsCandidateExperiences;

namespace SistemaDeGestionDeCandidatos.Controllers
{
    public class CandidateExperiencesController : Controller
    {
        private readonly GestionCanditadosDbContext _context;
        private readonly GetExperienciesCandidatesHandler _getExperienciesCandidate;
        private readonly EditCandidateExperienceCommandHandler _editExperienciesCandidates;
        private readonly CreateCandidateExperienceCommandHandler _createExperienciesCandidates;

        public CandidateExperiencesController(
            GestionCanditadosDbContext context,
            GetExperienciesCandidatesHandler getExperienciesCandidate,
            EditCandidateExperienceCommandHandler editCandidateExperience,
            CreateCandidateExperienceCommandHandler createExperienciesCandidates
            )
        {
            _context = context;
            _getExperienciesCandidate = getExperienciesCandidate;
            _editExperienciesCandidates = editCandidateExperience;
            _createExperienciesCandidates = createExperienciesCandidates;
        }

        /// <summary>
        /// Controlador para obtener todas las experiencias
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                await _getExperienciesCandidate.Handle();
                var gestionCanditadosDbContext = _context.CandidateExperience.Include(c => c.Candidate);
                return View(await gestionCanditadosDbContext.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Controlador para crear la experiencia de candidato
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public IActionResult CreateForCandidate(int candidateId)
        {
            var experienceModel = new CandidateExperience
            {
                IdCandidate = candidateId // Asociar la experiencia con el candidato
            };
            return PartialView("_CreateForCandidate", experienceModel);
        }


        /// <summary>
        /// Controlador para retoranan modal de crear un candidato en la visata Candidates
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate");
            return View();
        }

        /// <summary>
        /// Controlador para crear una Experiencia de canditado desde el modal de creación de experiencias de visat candidates
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCandidateExperienceCommand command)
        {
            try { 
                if (ModelState.IsValid)
                {

                    var candidateExperience = new CandidateExperience
                    {
                        IdCandidate= command.idCandidate,
                        Company = command.Company,
                        Job = command.job,
                        Description = command.Description,
                        BeginDate = command.BeginDate,
                        EndDate = command.EndDate                        

                    };             

                    await _createExperienciesCandidates.CreateExperience(command);

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

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Controlador para mostrar datos del canditado a editar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CandidateExperience == null)
            {
                return NotFound();
            }

            var candidateExperience = await _context.CandidateExperience.FindAsync(id);
            if (candidateExperience == null)
            {
                return NotFound();
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        /// <summary>
        /// controladort para enviar datos a editar
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCandidateExperienceCommand command)
        {
            var candidateExperience = new CandidateExperience
            {
                IdCandidateExperience = command.IdCandidateExperience,
                Company = command.Company,
                Job = command.job,
                Description= command.Description,
                BeginDate = command.BeginDate,
                EndDate = command.EndDate,
                ModifyDate = DateTime.Now
            };

                try
                {          
                    await _editExperienciesCandidates.EditExperience(command);
                    return RedirectToAction(nameof(Index));
                }
            
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError("", "No se pudo guardar los cambios en la base de datos. Intente nuevamente.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                return View(candidateExperience);
         }

        /// <summary>
        /// Controlador para mostra datos de eliminar una experiencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CandidateExperience == null)
            {
                return NotFound();
            }

            var candidateExperience = await _context.CandidateExperience
                .Include(c => c.Candidate)
                .FirstOrDefaultAsync(m => m.IdCandidateExperience == id);
            if (candidateExperience == null)
            {
                return NotFound();
            }

            return View(candidateExperience);
        }

        /// <summary>
        /// controlador eliminar una experiencia despues de confirmación del usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CandidateExperience == null)
            {
                return Problem("Entity set 'GestionCanditadosDbContext.Experiences'  is null.");
            }
            var candidateExperience = await _context.CandidateExperience.FindAsync(id);
            if (candidateExperience != null)
            {
                _context.CandidateExperience.Remove(candidateExperience);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExperienceExists(int id)
        {
          return (_context.CandidateExperience?.Any(e => e.IdCandidateExperience == id)).GetValueOrDefault();
        }
    }
}
