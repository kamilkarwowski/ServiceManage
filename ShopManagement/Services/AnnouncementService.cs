using ServiceManagement.Models;
using static ServiceManagement.DAL.AnnouncementRepository;

namespace ShopManagement.Services
{
    public class AnnouncementService
    {
        private readonly AnnouncementRepositiory _announcementRepository;
        public AnnouncementService(AnnouncementRepositiory announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public Announcement? GetById(int id)
        {
            Announcement? absence = _announcementRepository.GetById(id);
            return absence;
        }

        public List<Announcement> GetAll()
        {
            List<Announcement> announcement = _announcementRepository.GetAll().ToList();
            return announcement;
        }

        public Announcement Add(Announcement announcement)
        {
            Announcement? newAnnouncement = _announcementRepository.AddAndSaveChanges(announcement);

            return newAnnouncement;
        }

        public void Update(Announcement announcement)
        {
            _announcementRepository.UpdateAndSaveChanges(announcement);
        }

        public void Delete(int id)
        {
            _announcementRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}