using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;


namespace data_storage_website_ap5pw.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class FileController : Controller
    {
        private readonly IFileAppService _fileAppService;
        private readonly IAccountService _accountService;

        public FileController(
            IFileAppService fileAppService,
            IAccountService accountService)
        {
            _fileAppService = fileAppService;
            _accountService = accountService;
        }

        // GET: /<controller>/Select
        public IActionResult Select()
        {
            IList<Domain.Entities.File> files = _fileAppService.SelectAll();
            return View(files);
        }

        private async Task LoadAuthorsAsync(int? selectedAuthorId = null)
        {
            var users = await _accountService.GetAllUsers();

            ViewBag.Authors = new SelectList(
                users,
                "Id",
                "UserName",
                selectedAuthorId);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> Edit(int id)
        {
            var file = _fileAppService.SelectAll()
                .FirstOrDefault(f => f.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            if (User.IsInRole(nameof(Roles.Admin)))
            {
                await LoadAuthorsAsync(file.AuthorId);
            }

            return View(file);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Domain.Entities.File file)
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

                // Только Admin может менять автора.
                if (User.IsInRole(nameof(Roles.Admin)))
                {
                    var author = await _accountService.GetUserById(file.AuthorId);

                    if (author == null)
                    {
                        ModelState.AddModelError(
                            nameof(file.AuthorId),
                            "Selected author does not exist.");

                        await LoadAuthorsAsync(existingFile.AuthorId);
                        return View(file);
                    }

                    existingFile.AuthorId = author.Id;
                }

                _fileAppService.Edit(existingFile);

                return RedirectToAction(nameof(FileController.Select));
            }

            if (User.IsInRole(nameof(Roles.Admin)))
            {
                await LoadAuthorsAsync(file.AuthorId);
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