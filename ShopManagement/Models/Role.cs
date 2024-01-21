using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ServiceManagement.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<User> Users { get;} = new List<User>();
    }
}
