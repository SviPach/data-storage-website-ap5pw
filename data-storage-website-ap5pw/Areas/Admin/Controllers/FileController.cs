using Microsoft.AspNetCore.Mvc;
using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Domain.Entities;

namespace data_storage_website_ap5pw.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileController : Controller
    {
        IFileAppService _fileAppService;

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        // GET: /<controller>/Select
        public IActionResult Select()
        {
            IList<Domain.Entities.File> files = _fileAppService.SelectAll();
            return View(files);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Domain.Entities.File file)
        {
            if (ModelState.IsValid)
            {
                _fileAppService.Create(file);

                return RedirectToAction(nameof(FileController.Select));
            }

            return View(file);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool deleted = _fileAppService.Delete(id);

            if (deleted)
            {
                return RedirectToAction(nameof(FileController.Select));
            }
            else
                return NotFound();
        }
    }
}