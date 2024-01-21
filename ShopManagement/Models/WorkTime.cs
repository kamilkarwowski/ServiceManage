using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.Models
{
    public class WorkTime
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }

        public TimeSpan TotalHours { get; set; }

        public int TaskId { get; set; }
        public Zadanie Task { get; set; }
    }
}
