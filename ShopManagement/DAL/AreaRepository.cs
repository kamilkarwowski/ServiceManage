using ServiceManagement.Models;

namespace ServiceManagement.DAL
{
    public class AreaRepository : BaseRepository<Area>
    {
        private ApplicationDbContext _context;
        public AreaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
