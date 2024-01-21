using ServiceManagement.Models;

namespace ServiceManagement.DAL
{
    public class DocumentRepository : BaseRepository<Document>
    {
        private ApplicationDbContext _context;
        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}