using Microsoft.AspNetCore.Mvc;
using PerschoolTaskIMGCrud.DAL;
using PerschoolTaskIMGCrud.Models;

namespace PerschoolTaskIMGCrud.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
