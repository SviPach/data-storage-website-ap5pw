using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Application.ViewModels;


namespace data_storage_website_ap5pw.Application.Implementation
{
    public class HomeService : IHomeService
    {
        IFileAppService _fileAppService;
        ICarouselAppService _carouselAppService;

        public HomeService(IFileAppService fileAppService,
                           ICarouselAppService carouselAppService)
        {
            _fileAppService = fileAppService;
            _carouselAppService = carouselAppService;
        }

        public IndexViewModel GetIndexViewModel()
        {
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.Files = _fileAppService.SelectAllCards();
            viewModel.Carousels = _carouselAppService.SelectAll();
            return viewModel;
        }
    }
}