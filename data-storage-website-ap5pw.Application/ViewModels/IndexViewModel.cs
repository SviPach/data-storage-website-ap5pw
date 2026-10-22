using data_storage_website_ap5pw.Domain.Entities;

namespace data_storage_website_ap5pw.Application.ViewModels
{
    public class IndexViewModel
    {
        public IList<Carousel>? Carousels { get; set; }
        public IList<File>? Files { get; set; }
    }
}