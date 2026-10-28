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

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(256)]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}