using Bassma.Data;
using Bassma.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bassma.Controllers
{
    public class ContactController : Controller
    {
        private readonly BakeryDbContext _context;

        public ContactController(BakeryDbContext context)
        {
            _context = context;
        }

        // Index Action to Display All Messages
        public IActionResult Index()
        {
            var messages = _context.Contacts.ToList();
            return View(messages);
        }

        // Submit Action for Submitting New Messages
        [HttpPost]
        public IActionResult Submit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "There was an error submitting your message. Please try again.";
            return RedirectToAction("Index", "Home");
        }
    }
}