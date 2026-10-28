using System.ComponentModel.DataAnnotations;

namespace data_storage_website_ap5pw.Application.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match!")]
        public string? RepeatedPassword { get; set; }
    }
}