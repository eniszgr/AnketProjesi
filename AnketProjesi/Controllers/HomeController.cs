using AnketProjesi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnketProjesi.Controllers
{
    public class HomeController : Controller
    {
        public readonly anket3DbContext _context;
        public HomeController(anket3DbContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            var ankets= await _context.Ankets.Include(x=>x.Tur).Include(x=>x.Tip).ToListAsync();
            return View(ankets);
        }
        public IActionResult Charts()
        {
            return View();
        }
    }
}
