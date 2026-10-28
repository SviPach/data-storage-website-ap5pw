using data_storage_website_ap5pw.Application.ViewModels;
using data_storage_website_ap5pw.Domain.Enums;
using data_storage_website_ap5pw.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace data_storage_website_ap5pw.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
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
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return View(model);
        }
    }
}