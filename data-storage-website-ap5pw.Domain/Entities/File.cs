using data_storage_website_ap5pw.Domain.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data_storage_website_ap5pw.Domain.Entities
{
    public class File : Entity<int>
    {
        [Required]
        [StringLength(30)]
        [ValidFileName]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(70)]
        [FirstLetterUppercase]
        public string Topic{ get; set;  } = string.Empty;

        [Required]
        [Column(TypeName = "longtext")]
        public string Text { get; set; } = string.Empty;
        
        [Required]
        public int AuthorId { get; set; }
    }
}

