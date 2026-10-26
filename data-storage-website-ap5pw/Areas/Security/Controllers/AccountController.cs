using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Application.ViewModels;
using data_storage_website_ap5pw.Domain.Enums;
using data_storage_website_ap5pw.Controllers;

namespace data_storage_website_ap5pw.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }




        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                string[] errors = await _accountService.Register(registerVM, Roles.Customer);

                if (errors == null)
                {
                    //login the user after registration
                    LoginViewModel loginVM = new LoginViewModel()
                    {
                        Username = registerVM.Username,
                        Password = registerVM.Password
                    };

                    bool isLogged = await _accountService.Login(loginVM);
                    if (isLogged)
                        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), String.Empty), new { area = String.Empty });
                    else
                        return RedirectToAction(nameof(Login));
                }
                else
                {
                    //log errors (if you add logging)
                }

            }

            return View(registerVM);
        }





        public IActionResult Login()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                bool isLogged = await _accountService.Login(loginVM);
                if (isLogged)
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), String.Empty), new { area = String.Empty });

                loginVM.LoginFailed = true;
            }

            return View(loginVM);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction(nameof(Login));
        }
    }
}