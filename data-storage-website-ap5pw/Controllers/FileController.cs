using data_storage_website_ap5pw.Application.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace data_storage_website_ap5pw.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileAppService _fileAppService;

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var file = _fileAppService.SelectAll()
                .FirstOrDefault(f => f.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            return View("View", file);
        }

        [HttpGet]
        public IActionResult Download(int id)
        {
            var file = _fileAppService.SelectAll()
                .FirstOrDefault(f => f.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            string content = $"{file.Topic}{Environment.NewLine}" +
                             $"{Environment.NewLine}{file.Text}";

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(content);

            string fileName = $"{file.FileName}.txt";

            return File(bytes, "text/plain; charset=utf-8", fileName);
        }
    }
}