using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using ServiceManagement.Helpers;
using ServiceManagement.Models;
using ServiceManagement.Services;
using ServiceManagement.ViewModels;
using ShopManagement.Controllers;
using System.Data;
using System.Security.Claims;

namespace ServiceManagement.Controllers
{

    public class UserController : Controller
    {
        public const string SessionKeyUserId = "_UserId";
        public const string SessionKeyRole = "_Role";
        public const string SessionKeyArea = "_Area";

        private UserService _userService;
        private RoleService _roleService;
        private AreaService _areaService;
        private IEmailService _emailService;

        public UserController(UserService userService, RoleService roleService, AreaService areaService, IEmailService emailService)
        {
            _userService = userService;
            _roleService = roleService;
            _areaService = areaService;
            _emailService = emailService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userService.GetAll();
            var roles = _roleService.GetAll();
            var area = _areaService.GetAll();




            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roles = _roleService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
            ViewBag.Roles = roles;


            var areas = _areaService.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToList();

            ViewBag.Areas = areas;

            return View("Create");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(User user)
        {
            string hashedPassword = PasswordHasher.HashPassword(user.Password);
            user.Password = hashedPassword;
            _userService.Add(user);
            ModelState.Clear();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        
        

        

        [HttpPost]
        public ActionResult Login(User usr)
        {

            var user = _userService.GetAll().FirstOrDefault(u => u.Login == usr.Login);
            var roles = _roleService.GetAll();
            var roleDictionary = roles.ToDictionary(role => role.Id.ToString(), role => role.Name);
            if (user == null)
            {
                return View("Login");
            }
            bool isPassValid = PasswordHasher.VerifyPassword(usr.Password, user.Password);

            if (isPassValid)
            {
                var time = DateTime.Now;
                _userService.Update(user);
                var roleName = roleDictionary.ContainsKey(user.RoleId.ToString()) ? roleDictionary[user.RoleId.ToString()] : "Unknown Role";
                HttpContext.Session.SetInt32(SessionKeyUserId, user.Id);
                HttpContext.Session.SetString(SessionKeyRole, roleName);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim(ClaimTypes.Role, roleName),
                    };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction("Index","Home");

            }
            return View("Login");

        }
         [HttpGet]
          public IActionResult SendEmailPasswd()
          {


              return View("SendEmailPasswd");
          }

          [HttpPost]

          public IActionResult SendEmailPasswd(string Email, User user)
          {

              var address = Email;

              UserEmailOptions userEmailOptions = new UserEmailOptions
              {
                  ToEmails = new List<string> { address },
                  PlaceHolders = new List<KeyValuePair<string, string>>()
                  {
                       new KeyValuePair<string, string>("{{Message}}", "Zapomniałeś hasła?"),
                      new KeyValuePair<string, string>("{{Link}}", "https://localhost:44320/User/NewPasswd")

                  }
              };
            
               _emailService.SendForgetPasswordEmail(userEmailOptions);
              ModelState.Clear();
               return RedirectToAction("Login");
          }

        [HttpGet]
        public IActionResult ChangePassword()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePassViewModel changePass)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetById(
                       int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value));


               
                
                    if (changePass.NewPassword != changePass.ConfirmPassword)
                    {
                        ViewBag.Valid = "Hasła się różnią";
                        return View();
                    }

                    var hashedPass = PasswordHasher.HashPassword(changePass.NewPassword);
                    user.Password = hashedPass;
                    _userService.Update(user);
                
                return RedirectToAction("Index", "Home");
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            var roles = _roleService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles;
            var areas = _areaService.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToList();

            ViewBag.Areas = areas;

            var model = new User
            {
                Id = id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Login,
                Number = user.Number,
                RoleId = user.RoleId,
                AreaId = user.AreaId,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(User usr)
        {

            var user = _userService.GetById(usr.Id);
            if (user == null)
                return NotFound();
            user.Name = usr.Name;
            user.LastName = usr.LastName;
            user.Email = usr.Email;
            user.Login = usr.Login;
            user.Number = usr.Number;
            user.RoleId = usr.RoleId;
            user.AreaId = usr.AreaId;

            _userService.Update(user);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var roles = _roleService.GetAll();
            var areas = _areaService.GetAll();
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();


            return View(user);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            _userService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}