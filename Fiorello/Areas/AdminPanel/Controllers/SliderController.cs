using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }
        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Slides);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(slide.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo","File size must be less than 200 KB");
                return View();
            }
            if (!slide.Photo.ContentType.Contains("img/"))
            {
                ModelState.AddModelError("Photo", "File type must be image ");
                return View();
            }
            var filename = Guid.NewGuid().ToString();
            //using(FileStream filestream = new FileStream(@"C:\Users\HP\Desktop\asp.net\Fiorello\Fiorello\wwwroot\img\" + slide.Photo.FileName, FileMode.Create))
            //{
            //    slide.Photo.CopyTo(filestream);
            //};
            return Json("ok");
        }
    }
}
