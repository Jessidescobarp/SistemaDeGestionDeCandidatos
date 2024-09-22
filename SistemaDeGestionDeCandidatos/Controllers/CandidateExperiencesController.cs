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

namespace SistemaDeGestionDeCandidatos.Controllers
{
    public class CandidateExperiencesController : Controller
    {
        private readonly GestionCanditadosDbContext _context;
        private readonly GetExperienciesCandidatesHandler _getExperienciesCandidate;

        public CandidateExperiencesController(
            GestionCanditadosDbContext context,
            GetExperienciesCandidatesHandler getExperienciesCandidate)
        {
            _context = context;
            _getExperienciesCandidate = getExperienciesCandidate;
        }

        // GET: CandidateExperiences
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

        // GET: CandidateExperiences/CreateForCandidate
        public IActionResult CreateForCandidate(int candidateId)
        {
            var experienceModel = new CandidateExperience
            {
                IdCandidate = candidateId // Asociar la experiencia con el candidato
            };
            return PartialView("_CreateForCandidate", experienceModel);
        }


        // GET: CandidateExperiences/Create
        public IActionResult Create()
        {
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCandidateExperience,IdCandidate,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate")] CandidateExperience candidateExperience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidateExperience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Edit/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCandidateExperience,IdCandidate,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate")] CandidateExperience candidateExperience)
        {
            if (id != candidateExperience.IdCandidateExperience)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidateExperience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExperienceExists(candidateExperience.IdCandidateExperience))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Delete/5
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

        // POST: CandidateExperiences/Delete/5
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
