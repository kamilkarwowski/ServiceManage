using ServiceManagement.Models;

namespace ServiceManagement.DAL
{
    public class ZadanieRepository : BaseRepository<Zadanie>

    {
        private ApplicationDbContext _context;
        public ZadanieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
