using Microsoft.Identity.Client;
using ServiceManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.Models
{
    public class Zadanie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public DateTime Deadline { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        

        public string? Comment { get; set; }

        public string StatusZadania { get; set; }
        public ICollection<WorkTime> WorkTimes { get; } = new List<WorkTime>();
        public ICollection<Document> Documents { get; } = new List<Document>();
    }
}
