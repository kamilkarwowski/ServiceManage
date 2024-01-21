using ServiceManagement.Models;

namespace ServiceManagement.DAL
{
    public class RoleRepository : BaseRepository<Role>
    {
        private ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
