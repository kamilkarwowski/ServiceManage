using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceManagement.Services;
using ServiceManagement.Models;
using ServiceManagement.Services;
using System.Security.Claims;

namespace ShopManagement.Controllers
{
    public class DocumentController : Controller
    {
        private DocumentService _documentService;
        private UserService _userService;

        public DocumentController(DocumentService documentService, UserService userService)
        {
            _documentService = documentService;
            _userService = userService;
        }

        public IActionResult Index()
        {

            if (User.IsInRole("admin"))
            {
                var documents = _documentService.GetAll();
                var users = _userService.GetAll();
                return View(documents);

            }
            else
            {
                var documents = _documentService.GetPublicDocument();
                var users = _userService.GetAll();
                return View(documents);

            }
        }


        [HttpGet]
        public IActionResult Upload()
        {
            
            return View("Upload");
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int documentTypeId, string description, bool isPrivate)
        {
            if (file != null && file.Length > 0)
            {
                if (!User.IsInRole("admin")) isPrivate = false;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileData = memoryStream.ToArray();

                    var document = new Document()
                    {
                        UserId = int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value),
                        FileName = file.FileName,
                        UploadDate = DateTime.UtcNow,
                        IsPrivate = isPrivate,
                        Description = description,
                        Content = fileData
                    };
                    _documentService.Add(document);
                }
                return RedirectToAction("Index");
            }


            else
            {

                return View();
            }
        }

        public IActionResult Download(int id)
        {
            var document = _documentService.GetAll().FirstOrDefault(d => d.Id == id);

            if (document != null)
            {
                var stream = new MemoryStream(document.Content);
                return File(stream, "application/octet-stream", document.FileName);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var doc = _documentService.GetById(id);
            if (doc == null)
                return NotFound();
            _documentService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
