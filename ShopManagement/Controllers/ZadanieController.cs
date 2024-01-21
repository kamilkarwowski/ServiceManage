using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceManagement.Models;
using ServiceManagement.DAL;
using ServiceManagement.Models;
using ServiceManagement.Services;
using System.Net.Mail;
using System.Net;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace ServiceManagement.Controllers
{
    public class ZadanieController : Controller
    {
        private ZadaniaService _ZadaniaService;
        private StatusService _statusService;
        private AreaService _areaService;
        private UserService _userService;
        private IEmailService _emailService;
        private DocumentService _documentService;

        public ZadanieController(ZadaniaService ZadaniaService, StatusService statusService, AreaService areaService, UserService userService, IEmailService emailService, DocumentService documentService)
        {
            _ZadaniaService = ZadaniaService;
            _statusService = statusService;
            _areaService = areaService;
            _userService = userService;
            _emailService = emailService;
            _documentService = documentService;
        }

        // GET: Zadanie

        [HttpGet]
        public IActionResult Index()
        {
            var Zadania = _ZadaniaService.GetAll();
            var statuses = _statusService.GetAll();
            var areas = _areaService.GetAll();
            var users = _userService.GetAll();


            return View(Zadania);
        }

        // GET: Zadanie/Details/5

        [HttpGet]
        public IActionResult Details(int id)
        {
            var Zadanie = _ZadaniaService.GetById(id);
            var statuses = _statusService.GetAll();
            var areas = _areaService.GetAll();
            var users = _userService.GetAll();
           


            return View(Zadanie);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var statuses = _statusService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name

            }).ToList();
            ViewBag.Statuses = statuses;


            var areas = _areaService.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToList();

            ViewBag.Areas = areas;
            var users = _userService.GetAll().Select(U => new SelectListItem
            {
                Value = U.Id.ToString(),
                Text = U.Name + " " + U.LastName,
            }).ToList();
            ViewBag.Users = users;

            return View("Create");
        }


        [HttpPost]
        public  async Task<RedirectToActionResult> Create(Zadanie Zadanie, User user)
        {
            var status = _statusService.GetById(Zadanie.StatusId);
            var email = _userService.GetById(Zadanie.UserId);
            Zadanie.Deadline = Zadanie.Created + status.time;
            Zadanie.StatusZadania = "Dodane";

            _ZadaniaService.Add(Zadanie);

            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string>() { email.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", email.Name),
                    new KeyValuePair<string, string>("{{Message}}", "Zostało dodane nowe zadanie"),
                    new KeyValuePair<string, string>("{{Link}}", "https://localhost:44320/Zadanie/Details/"+Zadanie.Id),
                    new KeyValuePair<string, string>("{{Lat}}", Zadanie.Latitude),
                    new KeyValuePair<string, string>("{{Lng}}", Zadanie.Longtitude),
                }
            };
            await _emailService.SendNewTaskEmail(userEmailOptions);
            ModelState.Clear();
            return RedirectToAction("Index");



        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Zadanie = _ZadaniaService.GetById(id);
            var status = _statusService.GetById(Zadanie.StatusId);
            if (Zadanie == null)
                return NotFound();
            var areas = _areaService.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToList();

            ViewBag.Areas = areas;
            var statuses = _statusService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name

            }).ToList();
            ViewBag.Statuses = statuses;
            var users = _userService.GetAll().Select(U => new SelectListItem
            {
                Value = U.Id.ToString(),
                Text = U.Name + " " + U.LastName,
            }).ToList();
            ViewBag.Users = users;
            var model = new Zadanie
            {
                Id = id,
                Name = Zadanie.Name,
                Description = Zadanie.Description,
                Created = Zadanie.Created,
                Deadline = Zadanie.Created + status.time,
                StatusId = Zadanie.StatusId,
                UserId = Zadanie.UserId,
                AreaId = Zadanie.AreaId,
               
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var zad = _ZadaniaService.GetById(id);
            if (zad == null)
                return NotFound();
            _ZadaniaService.Delete(id);
            return RedirectToAction("Index");
        }

        

        [HttpGet]
        public IActionResult Finish(int id)
        {
            var zad = _ZadaniaService.GetById(id);
            if (DateTime.Now > zad.Deadline)
                zad.StatusZadania = "Zakończone zbyt póżno!";
            zad.StatusZadania = "Zakończone";
            _ZadaniaService.Update(zad);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Accept(int id)
        {
            var zad = _ZadaniaService.GetById(id);
            zad.StatusZadania = "Zakończone - Zaakceptowane";
            _ZadaniaService.Update(zad);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Cancel(int id)
        {
            var zad = _ZadaniaService.GetById(id);
            zad.StatusZadania = "Dodane";
            _ZadaniaService.Update(zad);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FinishedTask ()
        {
            var zad = _ZadaniaService.GetAll().Where(z => z.StatusZadania == "Zakończone - Zaakceptowane");
            return View(zad);

        }


    }
}
