using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Domain.Entities;
using data_storage_website_ap5pw.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace data_storage_website_ap5pw.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
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

        [HttpPost]
        public IActionResult Create(Domain.Entities.File file)
        {
            // Get the ID of the currently logged-in user
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
                return Challenge();

            // Assign the author on the server
            file.AuthorId = userId;

            if (ModelState.IsValid)
            {
                _fileAppService.Create(file);

                return RedirectToAction(nameof(FileController.Select));
            }

            return View(file);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var file = _fileAppService.SelectAll()
                .FirstOrDefault(f => f.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Domain.Entities.File file)
        {
            var existingFile = _fileAppService.SelectAll()
                .FirstOrDefault(f => f.Id == id);

            if (existingFile == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingFile.FileName = file.FileName;
                existingFile.Topic = file.Topic;
                existingFile.Text = file.Text;

                _fileAppService.Edit(existingFile);

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