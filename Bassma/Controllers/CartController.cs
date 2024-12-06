using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Bassma.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Bassma.Data;

namespace Bassma.Controllers
{
    [Authorize] // Allows any authenticated user temporarily
    public class CartController : Controller
    {
        private readonly BakeryDbContext _context; // Injected DbContext

        // Constructor
        public CartController(BakeryDbContext context)
        {
            _context = context;
        }
        // Hardcoded products (to simplify without a database)
        private readonly List<Produit> Products = new List<Produit>
        {
            new Produit { Id = 1, Nom = "Pistachio Rose Cake", Prix = 28 },
            new Produit { Id = 2, Nom = "Raspberry Swirl Croissant", Prix = 16 },
            new Produit { Id = 3, Nom = "Classic Portuguese Egg Tarts", Prix = 20 },
            new Produit { Id = 4, Nom = "Almond Blossom Cheesecake", Prix = 115 },
            new Produit { Id = 5, Nom = "Peach Blossom Delight", Prix = 26 },
            new Produit { Id = 6, Nom = "Matcha Cherry Slice", Prix = 24 },
            new Produit { Id = 7, Nom = "Cookies & Cream Cupcake", Prix = 18 },
            new Produit { Id = 8, Nom = "Classic Tiramisu Slice", Prix = 32 },
            new Produit { Id = 9, Nom = "Strawberry Mousse Cake", Prix = 64 },
            new Produit { Id = 10, Nom = "Raspberry Velvet Slice", Prix = 24 },
            new Produit { Id = 11, Nom = "Honey Glazed Brioche", Prix = 12 },
            new Produit { Id = 12, Nom = "Cinnamon Swirl Roll", Prix = 14 },
            new Produit { Id = 13, Nom = "Strawberry Swiss Roll", Prix = 18 },
            new Produit { Id = 14, Nom = "Pistachio Dome Delight", Prix = 25 },
            new Produit { Id = 15, Nom = "Chocolate Dipped Croissant", Prix = 15 },
            new Produit { Id = 16, Nom = "Tiramisu Muffin", Prix = 22 }
        };

        public IActionResult Index()
        {
            var produits = _context.Produits.ToList();
            foreach (var produit in produits)
            {
                Console.WriteLine(produit.ImagePath); // Log ImagePath values
            }
            return View(produits);
        }

    }
}
