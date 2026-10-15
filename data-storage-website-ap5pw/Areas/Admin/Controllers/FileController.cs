using Microsoft.AspNetCore.Mvc;

namespace data_storage_website_ap5pw.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileController : Controller
    {
        // GET: /<controller>/
        public IActionResult Select()
        {
            return View();
        }
    }
}