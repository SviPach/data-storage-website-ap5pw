using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data_storage_website_ap5pw.Domain.Entities
{
    [Table(nameof(FileEntity))]
    public class FileEntity
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required long Size { get; set; }

        [Required]
        public required string Path { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public required int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public required User User { get; set; }
    }
}
