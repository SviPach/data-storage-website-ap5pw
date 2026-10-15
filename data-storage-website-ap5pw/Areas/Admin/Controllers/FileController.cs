using Microsoft.AspNetCore.Mvc;

namespace data_storage_website_ap5pw.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        // GET: /<controller>/
        public IActionResult Select()
        {
            return View();
        }
    }
}