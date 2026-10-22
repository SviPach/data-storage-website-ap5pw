using System.ComponentModel.DataAnnotations.Schema;

namespace data_storage_website_ap5pw.Domain.Entities
{
    [Table(nameof(Carousel))]
    public class Carousel : Entity<int>
    {
        public string ImageSrc { get; set; }
        public string ImageAlt { get; set; }
    }
}
