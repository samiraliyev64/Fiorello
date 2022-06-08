using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorello.Models;
using Fiorello.ViewModels.Categories;

namespace Fiorello.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<Category> categories { get; set; }
        public CategoryController(AppDbContext context)
        {
            _context = context;
            categories = _context.Categories.Where(ct => !ct.isDeleted);
        }
        public IActionResult Index()
        {
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid) { return View(); }
            bool isExist = categories.Where(c => !c.isDeleted).Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", $"{category.Name} does exist");
                return View();
            }
            Category newCategory = new Category { Name = category.Name };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Category category = _context.Categories.Where(c=>!c.isDeleted).FirstOrDefault(c => c.Id == id );
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Category category)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category categoryDb = _context.Categories.Where(c => !c.isDeleted).FirstOrDefault(c => c.Id == id);
            if (categoryDb == null)
            {
                return NotFound();
            }
            //if(categoryDb.Name.ToLower() == category.Name.ToLower())
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            bool isExist = categories.Where(c => !c.isDeleted).Any(c => c.Name.ToLower() == category.Name.ToLower() && c.Id != categoryDb.Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", $"{category.Name} does exist");
                return View();
            }
            categoryDb.Name = category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Category categoryDb = _context.Categories.Where(c => !c.isDeleted).FirstOrDefault(c=>c.Id == id);
            if(categoryDb == null)
            {
                return NotFound();
            }
            //_context.Categories.Remove(categoryDb);
            categoryDb.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
