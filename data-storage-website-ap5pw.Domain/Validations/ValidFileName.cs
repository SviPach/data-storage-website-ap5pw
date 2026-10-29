using System.ComponentModel.DataAnnotations;

namespace data_storage_website_ap5pw.Domain.Validations
{
    public class ValidFileName : ValidationAttribute
    {
        private static readonly char[] InvalidCharacters =
            { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };

        public ValidFileName()
        {
            ErrorMessage = "File name contains invalid characters.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            if (value is not string fileName)
                return false;

            if (string.IsNullOrWhiteSpace(fileName))
                return true;

            if (fileName == "." || fileName == "..")
                return false;

            if (fileName.EndsWith('.') || fileName.EndsWith(' '))
                return false;

            return fileName.IndexOfAny(InvalidCharacters) < 0
                && !fileName.Any(char.IsControl);
        }
    }
}