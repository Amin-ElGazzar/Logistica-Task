using System.ComponentModel.DataAnnotations;
using LogisticaSolutions.Attributies;


namespace LogisticaSolutions.Dtos
{
    public class Uploadfile
    {
        [ExcelFileValidation]
        [Required(ErrorMessage = "Please upload an Excel file.")]
        public IFormFile File { get; set; }
    }
}
