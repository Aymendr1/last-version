using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bassma.Data;
using Bassma.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bassma.Controllers
{
/*    [Authorize(Roles = "Livreur")]*/
    public class LivraisonsController : Controller
    {
        private readonly BakeryDbContext _context;

        public LivraisonsController(BakeryDbContext context)
        {
            _context = context;
        }

        // GET: Livraisons
        public async Task<IActionResult> Index()
        {
            var bakeryDbContext = _context.Livraisons.Include(l => l.Commande).Include(l => l.User);
            return View(await bakeryDbContext.ToListAsync());
        }

        // GET: Livraisons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livraison = await _context.Livraisons
                .Include(l => l.Commande)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livraison == null)
            {
                return NotFound();
            }

            return View(livraison);
        }

        // GET: Livraisons/Create
        public IActionResult Create()
        {
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Livraisons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CommandeId,Date,Status")] Livraison livraison)
        {
            
                _context.Add(livraison);
                await _context.SaveChangesAsync();
                
            
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", livraison.CommandeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", livraison.UserId);
            return View(livraison);
        }

        // GET: Livraisons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livraison = await _context.Livraisons.FindAsync(id);
            if (livraison == null)
            {
                return NotFound();
            }
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", livraison.CommandeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", livraison.UserId);
            return View(livraison);
        }

        // POST: Livraisons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CommandeId,Date,Status")] Livraison livraison)
        {
            if (id != livraison.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(livraison);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivraisonExists(livraison.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            
            
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", livraison.CommandeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", livraison.UserId);
            return View(livraison);
        }

        // GET: Livraisons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livraison = await _context.Livraisons
                .Include(l => l.Commande)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livraison == null)
            {
                return NotFound();
            }

            return View(livraison);
        }

        // POST: Livraisons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livraison = await _context.Livraisons.FindAsync(id);
            if (livraison != null)
            {
                _context.Livraisons.Remove(livraison);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivraisonExists(int id)
        {
            return _context.Livraisons.Any(e => e.Id == id);
        }
    }
}
