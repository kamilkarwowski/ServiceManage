using System.ComponentModel.DataAnnotations;

namespace ServiceManagement.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        public string Description { get; set; }
        [Required]
        public byte[] Content { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
