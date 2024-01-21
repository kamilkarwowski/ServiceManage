namespace ServiceManagement.ViewModels
{
    public class WorkTimeViewModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; } 
        public int UserId { get; set; }
        public TimeSpan Hours { get; set; }
        public int ZadanieId { get; set; }
    }
}
