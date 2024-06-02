using System.ComponentModel.DataAnnotations;

namespace sait.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Введите ваш Email")]
        [EmailAddress(ErrorMessage = "Неверный формат Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите ваш пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}