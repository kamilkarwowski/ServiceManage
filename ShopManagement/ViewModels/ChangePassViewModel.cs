using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.ViewModels
{
    public class ChangePassViewModel
    {
        public int Id { get; set; }
        [DisplayName("Stare hasło")]
        [Required(ErrorMessage = "Podaj stare hasło")]
        public string OldPassword { get; set; }
        [DisplayName("Nowe hasło")]
        [Required(ErrorMessage = "Podaj nowe hasło")]
        public string NewPassword { get; set; }
        [DisplayName("Powtórz hasło")]
        [Required(ErrorMessage = "Powtórz nowe hasło")]
        public string ConfirmPassword { get; set; }
    }
}