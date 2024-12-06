using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bassma.Data;
using Bassma.Models;

namespace Bassma.Controllers
{
    public class PaiementsController : Controller
    {
        private readonly BakeryDbContext _context;

        public PaiementsController(BakeryDbContext context)
        {
            _context = context;
        }

        // GET: Paiements
        public async Task<IActionResult> Index()
        {
            var bakeryDbContext = _context.Paiments.Include(p => p.Commande);
            return View(await bakeryDbContext.ToListAsync());
        }

        // GET: Paiements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.Paiments
                .Include(p => p.Commande)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paiement == null)
            {
                return NotFound();
            }

            return View(paiement);
        }

        // GET: Paiements/Create
        public IActionResult Create()
        {
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse");
            return View();
        }

        // POST: Paiements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CommandeId,Status")] Paiement paiement)
        {
            
                _context.Add(paiement);
                await _context.SaveChangesAsync();
              
            
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", paiement.CommandeId);
            return View(paiement);
        }

        // GET: Paiements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.Paiments.FindAsync(id);
            if (paiement == null)
            {
                return NotFound();
            }
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", paiement.CommandeId);
            return View(paiement);
        }

        // POST: Paiements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CommandeId,Status")] Paiement paiement)
        {
            if (id != paiement.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(paiement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaiementExists(paiement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
            
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", paiement.CommandeId);
            return View(paiement);
        }

        // GET: Paiements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.Paiments
                .Include(p => p.Commande)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paiement == null)
            {
                return NotFound();
            }

            return View(paiement);
        }

        // POST: Paiements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paiement = await _context.Paiments.FindAsync(id);
            if (paiement != null)
            {
                _context.Paiments.Remove(paiement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaiementExists(int id)
        {
            return _context.Paiments.Any(e => e.Id == id);
        }
    }
}
