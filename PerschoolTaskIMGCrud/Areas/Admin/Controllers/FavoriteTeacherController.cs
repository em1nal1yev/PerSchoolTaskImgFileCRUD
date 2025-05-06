using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerschoolTaskIMGCrud.DAL;
using PerschoolTaskIMGCrud.Models;
using System.Collections.Generic;

namespace PerschoolTaskIMGCrud.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FavoriteTeacherController : Controller
    {
        private readonly AppDbContext _context;
        public FavoriteTeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<PopularTeacher> popularTeachers = _context.PopularTeachers.ToList();
            return View(popularTeachers);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PopularTeacher popularTeacher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string fileName = popularTeacher.File.FileName;
            string path = "C:\\Users\\Emin\\Desktop\\backEndTasks\\PerschoolTaskIMGCrud\\PerschoolTaskIMGCrud\\wwwroot\\Upload\\PopularTeacher\\";

            using (FileStream stream = new FileStream(path + fileName,FileMode.Create))
            {
                popularTeacher.File.CopyTo(stream);
            }

            popularTeacher.ImgUrl = fileName;












            await _context.PopularTeachers.AddAsync(popularTeacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var favoriteTeacher = await _context.PopularTeachers.FirstOrDefaultAsync(x => x.Id == id);
            if (favoriteTeacher == null) { return View(); }
            _context.PopularTeachers.Remove(favoriteTeacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var popularTeacher = await _context.PopularTeachers.FirstOrDefaultAsync(x => x.Id == id);
            return View(popularTeacher);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, PopularTeacher updatedPopularTeacher)
        {
            if (!ModelState.IsValid) return BadRequest();

            var popularTeacher = await _context.PopularTeachers.FirstOrDefaultAsync(x => x.Id == id);

            popularTeacher.FullName = updatedPopularTeacher.FullName;
            popularTeacher.Designation = updatedPopularTeacher.Designation;
            popularTeacher.ImgUrl = updatedPopularTeacher.ImgUrl;
            
            _context.PopularTeachers.Update(popularTeacher);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
