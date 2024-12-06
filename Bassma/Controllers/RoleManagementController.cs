using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Bassma.Models;
using System.Linq;

namespace Bassma.Controllers
{
    [Authorize] // Allows any authenticated user temporarily
    public class RoleManagementController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagementController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Display list of users and their roles
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList(); // Get all users
            return View(users);
        }

        // Update user role
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            // Update the user's role
            user.Role = role;

            // Save changes
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to assign role.");
                return View("Index", _userManager.Users); // Reload page with users
            }

            // Redirect back to the role management page
            return RedirectToAction(nameof(Index));
        }

    }
}
