using System.ComponentModel.DataAnnotations;

namespace LogisticaSolutions.Attributies
{
    public class ExcelFileValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                return new ValidationResult("File is required.");
            }

            var allowedExtensions = new[] { ".xls", ".xlsx", ".xlsm" };
            var fileExtension = System.IO.Path.GetExtension(file.FileName)?.ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return new ValidationResult("Only Excel files (.xls, .xlsx, .xlsm) are allowed.");
            }

            return ValidationResult.Success;
        }
    }
}
