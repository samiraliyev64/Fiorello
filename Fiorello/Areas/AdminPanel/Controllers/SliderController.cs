using Fiorello.DAL;
using Fiorello.Helpers;
using Fiorello.Models;
using Microsoft.AspNetCore.Hosting;
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
        private IWebHostEnvironment _env { get; }
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
        public async Task<IActionResult> Create(Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(!slide.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo","File size must be less than 200 KB");
                return View();
            }
            if (!slide.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image ");
                return View();
            }
            slide.Url = await slide.Photo.SaveFileAsync(_env.WebRootPath,"img");
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var slider = _context.Slides.Find(id);

            if(slider == null)
            {
                return NotFound();
            }
            var path = Helper.GetPath(_env.WebRootPath, "img", slider.Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Slides.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Slide slide = _context.Slides.Find(id);
            if(slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slide slide)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Slide slideDb = _context.Slides.Find(id);
            if(slideDb == null)
            {
                return NotFound();
            }
            slide.Url = await slide.Photo.SaveFileAsync(_env.WebRootPath, "img");
            var pathDb = Helper.GetPath(_env.WebRootPath, "img", slideDb.Url);
            if (System.IO.File.Exists(pathDb))
            {
                System.IO.File.Delete(pathDb);
            }
            slideDb.Url = slide.Url;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
