using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Domain.Entities;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository;

namespace data_storage_website_ap5pw.Application.Implementation
{
    public class CarouselAppService : ICarouselAppService
    {
        ICarouselRepository _carouselRepository;

        public CarouselAppService(ICarouselRepository carouselRepository)
        {
            _carouselRepository = carouselRepository;
        }

        public IList<Carousel> SelectAll()
        {
            return _carouselRepository.GetAll().ToList();
        }
    }
}