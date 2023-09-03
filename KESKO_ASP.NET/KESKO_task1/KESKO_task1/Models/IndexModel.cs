using System.ComponentModel.DataAnnotations;


namespace KESKO_task1.Models
{
    public class IndexModel
    {
        [Required(ErrorMessage = "Нельзя отправлять пустое поле")]
        public double? NumberI { get; set; }

        [Required(ErrorMessage = "Нельзя отправлять пустое поле")]
        public double? NumberJ { get; set; }
    }
}
