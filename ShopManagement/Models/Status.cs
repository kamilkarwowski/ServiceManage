using ServiceManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public TimeSpan time {  get; set; } 
        



       public ICollection<Zadanie> Zadania { get; } = new List<Zadanie>();

    }
}
