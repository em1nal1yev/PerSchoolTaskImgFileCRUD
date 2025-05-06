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
        private readonly IWebHostEnvironment _environment;
        public FavoriteTeacherController(AppDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }
        public IActionResult Index()
        {
            List<PopularTeacher> popularTeachers = _context.PopularTeachers.ToList();
            return View(popularTeachers);
        }

        public IActionResult Create() //burdaki if ler xaric qalanlari extentiona tokmek lazimdi. eslinde ifleride elemek lazimdi 
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
            if (!popularTeacher.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "image daxil edin basqa fal yox"); //birinci deyer keydi viewda goturduyumuze gore yaziriq o biri errormessage
                return View();
            }
            if(!(popularTeacher.File.Length > 2097152))
            {
                ModelState.AddModelError("File", "Sekilin olcusu 2 mb dan az olmalidi");
                return View();
            }
            


            string fileName = Guid.NewGuid() + popularTeacher.File.FileName; //faylin adinin uzunlugunada gedib limit qoya bilersen. qoysan meslehetdi
            string path = Path.Combine(_environment.WebRootPath,"Upload/PopularTeacher"); // luboy kompda acsin deye environment elave etdik onu ayrica oyrenersen nedi ne deyil

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
            string fileName = Guid.NewGuid() + favoriteTeacher.File.FileName;
            string path = Path.Combine(_environment.WebRootPath, "Upload/PopularTeacher");
            if (favoriteTeacher.File.Exists(path)) favoriteTeacher.File.Delete(path); //Sile bilmedim niyese exists da delete de error verir

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
