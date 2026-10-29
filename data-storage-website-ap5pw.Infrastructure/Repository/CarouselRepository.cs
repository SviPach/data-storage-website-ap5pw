using data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository;
using data_storage_website_ap5pw.Domain.Infrastructure.Repository;
using data_storage_website_ap5pw.Domain.Entities;
using data_storage_website_ap5pw.Infrastructure.Database;

namespace data_storage_website_ap5pw.Infrastructure.Repository
{
    public class CarouselRepository : Repository<Carousel, int>, ICarouselRepository
    {
        public CarouselRepository(DataStorageDbContext context) : base(context)
        {
        }
    }
}