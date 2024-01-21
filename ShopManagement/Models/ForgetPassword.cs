using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.Models
{
    public class ForgetPassword
    {
        [Required, EmailAddress, Display(Name = "Twój adres e-mail")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
