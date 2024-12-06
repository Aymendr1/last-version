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
    public class ProduitCommandesController : Controller
    {
        private readonly BakeryDbContext _context;

        public ProduitCommandesController(BakeryDbContext context)
        {
            _context = context;
        }

        // GET: ProduitCommandes
        public async Task<IActionResult> Index()
        {
            var bakeryDbContext = _context.ProduitCommandes.Include(p => p.Commande).Include(p => p.Produit);
            return View(await bakeryDbContext.ToListAsync());
        }

        // GET: ProduitCommandes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produitCommande = await _context.ProduitCommandes
                .Include(p => p.Commande)
                .Include(p => p.Produit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produitCommande == null)
            {
                return NotFound();
            }

            return View(produitCommande);
        }

        // GET: ProduitCommandes/Create
        public IActionResult Create()
        {
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse");
            ViewData["ProduitId"] = new SelectList(_context.Produits, "Id", "Nom");
            return View();
        }

        // POST: ProduitCommandes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CommandeId,ProduitId,Quantite")] ProduitCommande produitCommande)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produitCommande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", produitCommande.CommandeId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "Id", "Nom", produitCommande.ProduitId);
            return View(produitCommande);
        }

        // GET: ProduitCommandes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produitCommande = await _context.ProduitCommandes.FindAsync(id);
            if (produitCommande == null)
            {
                return NotFound();
            }
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", produitCommande.CommandeId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "Id", "Nom", produitCommande.ProduitId);
            return View(produitCommande);
        }

        // POST: ProduitCommandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CommandeId,ProduitId,Quantite")] ProduitCommande produitCommande)
        {
            if (id != produitCommande.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produitCommande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitCommandeExists(produitCommande.Id))
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
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "Id", "Adresse", produitCommande.CommandeId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "Id", "Nom", produitCommande.ProduitId);
            return View(produitCommande);
        }

        // GET: ProduitCommandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produitCommande = await _context.ProduitCommandes
                .Include(p => p.Commande)
                .Include(p => p.Produit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produitCommande == null)
            {
                return NotFound();
            }

            return View(produitCommande);
        }

        // POST: ProduitCommandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produitCommande = await _context.ProduitCommandes.FindAsync(id);
            if (produitCommande != null)
            {
                _context.ProduitCommandes.Remove(produitCommande);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitCommandeExists(int id)
        {
            return _context.ProduitCommandes.Any(e => e.Id == id);
        }
    }
}
