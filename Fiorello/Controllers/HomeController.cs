using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel
            {
                Slides = _context.Slides.ToList(),
                SlideSummary = _context.SlideSummary.FirstOrDefault(),
                Categories = _context.Categories.Where(c => !c.isDeleted).ToList(),
                Products = _context.Products.Where(c => !c.isDeleted)
                .Include(p => p.Images).Include(p => p.Category).ToList()
            };
            return View(home);
        }
    }
}
