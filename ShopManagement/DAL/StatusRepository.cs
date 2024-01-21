using ServiceManagement.Models;

namespace ServiceManagement.DAL
{
    public class StatusRepository : BaseRepository<Status>
    {
        private ApplicationDbContext _context;
        public StatusRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
