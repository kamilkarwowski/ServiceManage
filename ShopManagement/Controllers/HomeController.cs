using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Models;
using ServiceManagement.Services;
using ServiceManagement.ViewModels;
using ShopManagement.Services;
using System.Diagnostics;

namespace ShopManagement.Controllers
{
    public class HomeController : Controller
    {
        private AnnouncementService _announcementService;
        private IEmailService _emailService;

        public HomeController(AnnouncementService announcementService, IEmailService emailService)
        {
            _announcementService = announcementService;
            _emailService = emailService;
        }
        
        public IActionResult Index()
        {
          
            var announcement = _announcementService.GetAll();
            
            
             

            
            var announcements = announcement.OrderByDescending(x => x.Id);
            return View(announcements);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnnouncementViewModel announcement)
        {
            if (ModelState.IsValid)
            {
                var date = DateTime.Now.AddDays(announcement.Time);
                var newAnnouncement = new Announcement()
                {
                    Title = announcement.Title,
                    Description = announcement.Description,
                    Time = announcement.Time,
                    Date = DateTime.Now,
                };

                _announcementService.Add(newAnnouncement);
                return RedirectToAction("Index");

            }
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var announcement = _announcementService.GetById(id);
            if (announcement != null)
                _announcementService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var announcement = _announcementService.GetById(id);
            return View(announcement);
        }

        [HttpPost]
        public IActionResult Edit(Announcement announcement)
        {
            var editAnnoucement = _announcementService.GetById(announcement.Id);
            editAnnoucement.Title = announcement.Title;
            editAnnoucement.Description = announcement.Description;
            editAnnoucement.Date = DateTime.Now;
            _announcementService.Update(editAnnoucement);
            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}