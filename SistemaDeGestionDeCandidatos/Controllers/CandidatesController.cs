using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionDeCandidatos.Context;
using SistemaDeGestionDeCandidatos.Models;
using SistemaDeGestionDeCandidatos.Services;

namespace SistemaDeGestionDeCandidatos.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly GestionCanditadosDbContext _context;
        private readonly ICandidateValidationService _validationService;

        public CandidatesController(ICandidateValidationService validationService, GestionCanditadosDbContext context)
        {
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
            _context = context;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
              return _context.Candidates != null ? 
                          View(await _context.Candidates.ToListAsync()) :
                          Problem("Entity set 'GestionCanditadosDbContext.Candidates'  is null.");

        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Candidates == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidates
                .FirstOrDefaultAsync(m => m.IdCandidate == id);
            if (candidates == null)
            {
                return NotFound();
            }

            return View(candidates);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCandidate,Name,Surname,Birthdate,Email,InsertDate,ModifyDate")] Candidates candidates)
        {
            if (ModelState.IsValid)
            {
                // Validar que el candidato no exista
                if (await _validationService.ValidateCandidateExists(candidates.Email))
                {
                    ModelState.AddModelError("IdCandidate", "El candidato ya existe.");
                    return View(candidates);
                }

                candidates.InsertDate = DateTime.Now;                
                _context.Add(candidates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidates);
        }

        // GET: Candidates/Edit/5
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

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCandidate,Name,Surname,Birthdate,Email,InsertDate,ModifyDate")] Candidates candidates)
        {
            if (id != candidates.IdCandidate)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatesExists(candidates.IdCandidate))
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
            return View(candidates);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Candidates == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidates
                .FirstOrDefaultAsync(m => m.IdCandidate == id);
            if (candidates == null)
            {
                return NotFound();
            }

            return View(candidates);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Candidates == null)
            {
                return Problem("Entity set 'GestionCanditadosDbContext.Candidates'  is null.");
            }
            var candidates = await _context.Candidates.FindAsync(id);
            if (candidates != null)
            {
                _context.Candidates.Remove(candidates);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatesExists(int id)
        {
          return (_context.Candidates?.Any(e => e.IdCandidate == id)).GetValueOrDefault();
        }
    }
}
