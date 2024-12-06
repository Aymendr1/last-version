using Microsoft.AspNetCore.Mvc;

namespace Bassma.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
