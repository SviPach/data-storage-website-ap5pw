using data_storage_website_ap5pw.Domain.Entities;

namespace data_storage_website_ap5pw.Application.Abstraction
{
    public interface ICarouselAppService
    {
        IList<Carousel> SelectAll();
    }
}