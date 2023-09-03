using System.ComponentModel.DataAnnotations;


namespace KESKO_task2.Models
{
    public class IndexModel
    {
        [Required(ErrorMessage = "Нельзя отправлять пустое поле")]
        [MaxLength(8)]
        [MinLength(1)]
        [RegularExpression("^\\s*(?=.*[1-9])\\d*(?:\\.\\d{1,3})?\\s*$", ErrorMessage = "Только числа больше нуля, три знака после точки")]
        public string? NumberI { get; set; }

        [Required(ErrorMessage = "Нельзя отправлять пустое поле")]
        [MaxLength(8)]
        [MinLength(1)]
        [RegularExpression("^\\s*(?=.*[1-9])\\d*(?:\\.\\d{1,3})?\\s*$", ErrorMessage = "Только числа больше нуля, три знака после точки")]
        public string? NumberJ { get; set; }
    }
}




