using Microsoft.AspNetCore.Mvc;
using PerschoolTaskIMGCrud.DAL;
using PerschoolTaskIMGCrud.Models;

namespace PerschoolTaskIMGCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<PopularTeacher> popularTeachers = _context.PopularTeachers.ToList();
            return View(popularTeachers);
        }
    }
}
