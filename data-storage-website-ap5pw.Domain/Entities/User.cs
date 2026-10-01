using System.ComponentModel.DataAnnotations;

namespace data_storage_website_ap5pw.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        public ICollection<FileEntity> Files { get; set; }
    }
}
