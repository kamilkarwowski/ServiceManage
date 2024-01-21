using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
