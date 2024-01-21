using ServiceManagement.Models;

namespace ServiceManagement.ViewModels
{
    public class ZadanieViewModel
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }

        
        public string Description { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Deadline { get; set; }

        public int AreaId { get; set; }
       

        public int StatusId { get; set; }
        
        public int UserId { get; set; }
       

        public double latitude { get; set; }
        public double longtitude { get; set; }
    }
}
