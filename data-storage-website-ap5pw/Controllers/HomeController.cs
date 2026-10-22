using data_storage_website_ap5pw.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Application.ViewModels;

namespace data_storage_website_ap5pw.Controllers
{
    public class HomeController : Controller
    {
        IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = _homeService.GetIndexViewModel();
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
