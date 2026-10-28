using System.ComponentModel.DataAnnotations;

namespace data_storage_website_ap5pw.Application.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [MinLength(5)]
        public string? NewPassword { get; set; }
    }
}