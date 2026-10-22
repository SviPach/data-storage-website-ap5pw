using data_storage_website_ap5pw.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using data_storage_website_ap5pw.Domain.Validations;

namespace data_storage_website_ap5pw.Domain.Entities
{
    public class File : Entity<int>
    {
        [Required]
        [StringLength(70)]
        [FirstLetterUppercase]
        public string FileName { get; set; } = string.Empty;

        public long Size { get; set; }

        public string StoragePath { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}

