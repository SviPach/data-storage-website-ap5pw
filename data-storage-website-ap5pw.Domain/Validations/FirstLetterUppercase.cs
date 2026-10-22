using System.ComponentModel.DataAnnotations;

namespace data_storage_website_ap5pw.Domain.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class FirstLetterUppercaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else if (value is string text)
            {
                if (text == String.Empty)
                    return ValidationResult.Success;

                if (char.IsUpper(text.First()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"The {validationContext.MemberName} field must start with an uppercase letter.");
                }
            }
            else
            {
                throw new NotImplementedException($"The {nameof(FirstLetterUppercaseAttribute)} is not implemented for the type: {value.GetType()}");
            }
        }
    }
}