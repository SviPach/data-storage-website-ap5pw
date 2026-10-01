using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data_storage_website_ap5pw.Domain.Entities
{
    [Table(nameof(User))]
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
