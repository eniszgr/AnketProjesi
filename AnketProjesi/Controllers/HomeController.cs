using AnketProjesi.Models;
using AnketProjesi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
            var ankets = await _context.Ankets.Include(x => x.Tur).Include(x => x.Tip).ToListAsync();
            return View(ankets);
        }

        public async Task<IActionResult> Charts()
        {
            var aCount = await _context.Cevaplars.CountAsync(c => c.Cevap == "a");
            var bCount = await _context.Cevaplars.CountAsync(c => c.Cevap == "b");
            var cCount = await _context.Cevaplars.CountAsync(c => c.Cevap == "c");

            var total = aCount + bCount + cCount;

            var percentA = ((double)aCount / total) * 100;
            var percentB = ((double)bCount / total) * 100;
            var percentC = ((double)cCount / total) * 100;

            ViewData["percentA"] = percentA;
            ViewData["percentB"] = percentB;
            ViewData["percentC"] = percentC;

            return View();
        }

        public async Task<IActionResult> Sorular()
        {
            List<SelectListItem> tip = _context.Tips
                .Select(x => new SelectListItem
                {
                    Value = x.TipId.ToString(),
                    Text = x.TipName
                })
                .ToList();

            ViewBag.Tip = tip;

            List<SelectListItem> meslek = _context.Turs
                .Select(x => new SelectListItem
                {
                    Value = x.TurId.ToString(),
                    Text = x.TurName
                })
                .ToList();
            ViewBag.Tur = meslek;

            List<SelectListItem> sorular = _context.Sorulars
               .Select(x => new SelectListItem
               {
                   Value = x.SoruId.ToString(),
                   Text = x.Soru
               })
               .ToList();
     
            ViewBag.Sorular = sorular;
            return View();
        }

        public IActionResult Deneme() {
            List<SelectListItem> tip = _context.Tips
               .Select(x => new SelectListItem
               {
                   Value = x.TipId.ToString(),
                   Text = x.TipName
               })
               .ToList();

            ViewBag.Tip = tip;

            List<SelectListItem> meslek = _context.Turs
                .Select(x => new SelectListItem
                {
                    Value = x.TurId.ToString(),
                    Text = x.TurName
                })
                .ToList();
            ViewBag.Tur = meslek;

            List<SelectListItem> sorular = _context.Sorulars
               .Select(x => new SelectListItem
               {
                   Value = x.SoruId.ToString(),
                   Text = x.Soru
               })
               .ToList();

            ViewBag.Sorular = sorular;
            return View();
        }
        [HttpPost]
        public IActionResult SubmitForm(FormData formData)
        {
            if (ModelState.IsValid)
            {
                // Form verilerini işleyin
                // Örneğin, veritabanına kaydedin veya başka işlemler yapın

                // İşlemden sonra bir onay sayfasına yönlendirin
                
            }

            // Hata durumunda formu tekrar gösterin
            return View(formData);
        }

        public IActionResult Confirmation()
        {
            return View();
        }


    }
}
