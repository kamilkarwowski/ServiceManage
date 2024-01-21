using ServiceManagement.Models;

namespace ServiceManagement.DAL
{
    public class AnnouncementRepository
    {
        public class AnnouncementRepositiory : BaseRepository<Announcement>
        {
            private ApplicationDbContext _context;
            public AnnouncementRepositiory(ApplicationDbContext context) : base(context)
            {
                _context = context;
            }
        }
    }
}
