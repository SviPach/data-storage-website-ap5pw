using System.ComponentModel.DataAnnotations;

namespace data_storage_website_ap5pw.Application.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Please enter your current password.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a new password.")]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "The password must be at least 5 characters long.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your new password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword),
            ErrorMessage = "The new passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}