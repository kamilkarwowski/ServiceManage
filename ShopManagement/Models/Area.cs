using ServiceManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.Models
{
    public class Area
    {
        [Key] public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; } = new List<User>();
        public ICollection<Zadanie> Zadania { get; } = new List<Zadanie>();
    }
}
