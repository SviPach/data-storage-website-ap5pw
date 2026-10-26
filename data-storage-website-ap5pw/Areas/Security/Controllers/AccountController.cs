using Microsoft.AspNetCore.Mvc;

namespace data_storage_website_ap5pw.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}