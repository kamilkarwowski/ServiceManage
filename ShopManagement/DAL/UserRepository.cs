using Microsoft.EntityFrameworkCore;
using ServiceManagement.Models;

namespace ServiceManagement.DAL
{
    public class UserRepository : BaseRepository<User>
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
