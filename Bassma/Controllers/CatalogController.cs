using Microsoft.AspNetCore.Mvc;
using Bassma.Models;
using System.Linq;
using Bassma.Data;

namespace Bassma.Controllers
{
    public class CatalogController : Controller
    {
        private readonly BakeryDbContext _context;

        public CatalogController(BakeryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchTerm)
        {
            var products = string.IsNullOrEmpty(searchTerm)
                ? _context.Produits.ToList()
                : _context.Produits
                    .Where(p => p.Nom.StartsWith(searchTerm))
                    .ToList();

            return View(products); // Retourne les produits filtrés à la vue
        }
    }
}
