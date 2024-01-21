using ServiceManagement.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Imię")]
        [Required(ErrorMessage = "Imie jest wymagane")]
        [StringLength(50)] 
        public string Name { get; set; }

        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email jest wymagany")]
        [StringLength(50)]
        public string Email { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "Login jest wymagany")]
        [StringLength(25)]
        public string Login { get; set; }

        [DisplayName("Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$"
        //    , ErrorMessage = "Hasło musi mieć minimum 8 znaków, zawierać conajmniej 1 cyfrę, conajmniej 1 małą literę, conajmniej 1 dużą literę oraz znak specjalny")]
        public string Password { get; set; }

        [DisplayName("Numer telefonu")]
        [StringLength(20)]
        public string Number { get; set; }

        [DisplayName("Rola")]
        [Required(ErrorMessage = "Rola jest wymagana")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        
        [DisplayName("Area")]
        [Required(ErrorMessage = "Area jest wymagana")]
        public int AreaId { get; set; }
        public Area Area { get; set; }

        public ICollection<Document> Documents { get; } = new List<Document>();
        public ICollection <Zadanie> Zadania { get;}= new List<Zadanie>();
        public ICollection<WorkTime> WorkTimes { get; } = new List<WorkTime>();

    }
}
