using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi.Entities.Search.Video.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using ServiceManagement.DAL;
using ServiceManagement.Models;
using ServiceManagement.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceManagement.Controllers
{
    public class WorkTimesController : Controller
    {

        private WorkTimeService _workTimeService;
        private UserService _userService;
        private ZadaniaService _zadaniaService;

        public WorkTimesController(WorkTimeService workTimeService, UserService userService, ZadaniaService zadaniaService)
        {
            _userService = userService;
            _workTimeService = workTimeService;
            _zadaniaService = zadaniaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            if (User.IsInRole("Serwisant"))
            {
                


                var workTimes = _workTimeService.GetAll()
                 .Where(t => t.UserId == int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value))
                .ToList();
                DateTime dateNow = DateTime.Now;
                var month = dateNow.Month;

                var user = _userService.GetAll;
                var tasks = _zadaniaService.GetAll()
                    .Where(t => t.UserId == int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value))
                    .Where(t => t.Deadline.Month == month);

                var time = 0;
                int x;

                foreach(var item in workTimes)
                {
                    x = (int)item.TotalHours.TotalMinutes;
                    time += x;
                    x = 0;
                }

                ViewBag.Time = time/60;
                

                return View(workTimes);
            }
            else
            {
                var workTimes = _workTimeService.GetAll()
              
               .ToList();
                DateTime dateNow = DateTime.Now;
                var month = dateNow.Month;

                var user = _userService.GetAll();
                var tasks = _zadaniaService.GetAll()
                    .Where(t => t.Deadline.Month == month);

                return View(workTimes); 
            }
            

           
        }


        [HttpGet]
        public IActionResult Create()

        {
           var users = _userService.GetAll().Select(u => new SelectListItem
           {
               Value = u.Id.ToString(),
               Text = u.Name + " " + u.LastName,
           }).ToList();
            ViewBag.Users = users;

            var tasks = _zadaniaService.GetAll()

                .Where(t => t.UserId == int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value))
                .Where(t => t.StatusZadania == "Zakończone - Zaakceptowane")
                .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name + " "+ t.Created,
            }).ToList();
           ViewBag.Tasks = tasks;

            return View("Create");
        }

        
        [HttpPost]
        public IActionResult Create( WorkTime workTime)
        {

            var user = _userService.GetById(workTime.UserId);
            var task = _zadaniaService.GetById(workTime.TaskId);
            workTime.TotalHours = (workTime.EndTime - workTime.StartTime);
            workTime.UserId = task.UserId;
            _workTimeService.Add(workTime);
            ModelState.Clear();
            return RedirectToAction("Index");
        }

      
      
    }
}
