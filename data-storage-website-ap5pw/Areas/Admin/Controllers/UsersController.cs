using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Application.ViewModels;
using data_storage_website_ap5pw.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace data_storage_website_ap5pw.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class UsersController : Controller
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        private static readonly string[] AssignableRoles =
            {
                nameof(Roles.Customer),
                nameof(Roles.Manager)
            };

        [HttpGet]
        public async Task<IActionResult> Select()
        {
            var model = await _accountService.GetUsersWithRoles();
            return View("~/Areas/Admin/Views/Users/Select.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _accountService.GetUserById(id);

            if (user == null)
                return NotFound();

            var roles = await _accountService.GetUserRoles(id);

            if (roles.Contains(nameof(Roles.Admin)))
            {
                TempData["Error"] = "Administrator cannot be edited.";
                return RedirectToAction(nameof(Select));
            }

            var model = new EditUserViewModel
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault(r => AssignableRoles.Contains(r))
                       ?? nameof(Roles.Customer)
            };

            ViewBag.AssignableRoles = AssignableRoles;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _accountService.GetUserById(model.Id);

            if (user == null)
                return NotFound();

            var roles = await _accountService.GetUserRoles(model.Id);

            if (roles.Contains(nameof(Roles.Admin)))
            {
                TempData["Error"] = "Administrator cannot be edited.";
                return RedirectToAction(nameof(Select));
            }

            if (!AssignableRoles.Contains(model.Role))
            {
                ModelState.AddModelError(
                    nameof(model.Role),
                    "Invalid role selected.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.AssignableRoles = AssignableRoles;
                return View(model);
            }

            var result = await _accountService.UpdateUser(model);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                ViewBag.AssignableRoles = AssignableRoles;
                return View(model);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(Select));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accountService.DeleteUser(id);

            TempData[result.Success ? "Success" : "Error"] = result.Message;

            return RedirectToAction(nameof(Select));
        }
    }
}