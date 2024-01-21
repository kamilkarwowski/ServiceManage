using ServiceManagement.Models;

namespace ServiceManagement.DAL
{
    public class WorkTimeRepository : BaseRepository<WorkTime>
    {
        private ApplicationDbContext _context;
        public WorkTimeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}