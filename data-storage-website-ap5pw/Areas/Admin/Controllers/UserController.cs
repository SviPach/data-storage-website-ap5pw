using data_storage_website_ap5pw.Application.ViewModels;
using data_storage_website_ap5pw.Domain.Enums;
using data_storage_website_ap5pw.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using data_storage_website_ap5pw.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace data_storage_website_ap5pw.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly DataStorageDbContext _context;

        public UsersController(
            UserManager<User> userManager,
            DataStorageDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Select()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                model.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.UserName;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            return RedirectToAction(nameof(Select));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            // Cannot delete admin
            if (await _userManager.IsInRoleAsync(user, nameof(Roles.Admin)))
            {
                TempData["Error"] = "Administrator cannot be deleted.";
                return RedirectToAction(nameof(Select));
            }

            // Finding admin for re-linking
            var admin = (await _userManager.GetUsersInRoleAsync(nameof(Roles.Admin))).FirstOrDefault();

            if (admin == null)
            {
                TempData["Error"] = "No administrator exists.";
                return RedirectToAction(nameof(Select));
            }

            // Transaction
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // Re-linking files to admin
                var files = await _context.Files
                    .Where(f => f.AuthorId == user.Id)
                    .ToListAsync();

                foreach (var file in files)
                {
                    file.AuthorId = admin.Id;
                }

                await _context.SaveChangesAsync();

                // Deleting user after file re-linking
                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();

                    TempData["Error"] = string.Join(
                        " ",
                        result.Errors.Select(error => error.Description));

                    return RedirectToAction(nameof(Select));
                }

                await transaction.CommitAsync();

                TempData["Success"] =
                    $"User deleted successfully. {files.Count} file(s) reassigned to administrator {admin.UserName}.";

                return RedirectToAction(nameof(Select));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                TempData["Error"] =
                    "An error occurred while deleting the user. No changes were saved.";

                return RedirectToAction(nameof(Select));
            }
        }
    }
}