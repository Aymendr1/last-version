using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bassma.Data;
using Bassma.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Text.Json; // Pour la désérialisation JSON
using System.Text.Json.Nodes;

namespace Bassma.Controllers
{
    public class CommandesController : Controller
    {
        private readonly BakeryDbContext _context;

        public CommandesController(BakeryDbContext context)
        {
            _context = context;
        }

        // GET: Commandes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Commandes.ToListAsync());
        }

        // GET: Commandes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // GET: Commandes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commandes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Commande commande)
        {
            // Commande 
            string totalValue = Request.Form["total"];
            var nouvelleCommande = new Commande
            {
                // id de l'utilisateur connecté
                UserId = 1,
                Total = decimal.Parse(totalValue),
                ///Request.Form["adresse"];
                Adresse = "En attente",
            };

            _context.Commandes.Add(nouvelleCommande);
            _context.SaveChanges();

            //ProduitCommande
            string cartJson = Request.Form["cart"];
            var cartItems = JsonNode.Parse(cartJson)?.AsArray();

            if (cartItems == null)
            {
                Console.WriteLine("Erreur : le JSON du panier est vide ou mal formaté.");
                return Content("erreur");
            }

            foreach (var item in cartItems)
            {
                if (item is JsonObject jsonObject)
                {
                    var produitCommande = new ProduitCommande
                    {
                        ProduitId = jsonObject["id"] != null ? Convert.ToInt32(jsonObject["id"].ToString()) : 0,
                        CommandeId = nouvelleCommande.Id,
                        Quantite = jsonObject["quantite"] != null ? Convert.ToInt32(jsonObject["quantite"].ToString()) : 0,
                    };
                    _context.ProduitCommandes.Add(produitCommande);
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Erreur : l'élément du panier n'est pas un objet JSON valide.");
                    return Content("erreur");
                }
            }




            return Content("ok");

            //ayman dir hna return Redirect( hna ktab lview)  


        }






        // GET: Commandes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }
            return View(commande);
        }

        // POST: Commandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Total,UserId,Adresse")] Commande commande)
        {
            if (id != commande.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(commande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeExists(commande.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            
            return View(commande);
        }

        // GET: Commandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);
            if (commande != null)
            {
                _context.Commandes.Remove(commande);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeExists(int id)
        {
            return _context.Commandes.Any(e => e.Id == id);
        }
    }
}
