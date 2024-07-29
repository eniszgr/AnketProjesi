using AnketProjesi.Models;
using AnketProjesi.Repository;
using AnketProjesi.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AnketProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly anket3DbContext _context;

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
            //toplam veri
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


            //mühendislerin verileri
            var anketIDs = await _context.Ankets.Where(a=>a.TurId ==1)
                                                .Select(a=>a.AnketId)
                                                .ToListAsync();

           var aCountMuhendis = await _context.Cevaplars
                               .Where(c => anketIDs.Contains((int)c.AnketId) && c.Cevap == "a")
                               .CountAsync();
            var bCountMuhendis = await _context.Cevaplars
                               .Where(c => anketIDs.Contains((int)c.AnketId) && c.Cevap == "b")
                               .CountAsync();
            var cCountMuhendis = await _context.Cevaplars
                               .Where(c => anketIDs.Contains((int)c.AnketId) && c.Cevap == "c")
                               .CountAsync();
            var totalmuh = aCountMuhendis + bCountMuhendis+ cCountMuhendis;
            if (totalmuh == 0)
                totalmuh = 1;

            var percentAMuhendis = ((double)aCountMuhendis / totalmuh) * 100;
            var percentBMuhendis = ((double)bCountMuhendis / totalmuh) * 100;
            var percentCMuhendis = ((double)cCountMuhendis / totalmuh) * 100;

            ViewData["percentAMuhendis"]=percentAMuhendis;
            ViewData["percentBMuhendis"]= percentBMuhendis;
            ViewData["percentCMuhendis"] = percentCMuhendis;

            var anketIDsKurumici = await _context.Ankets.Where(a => a.TipId == 1)
                                                        .Select(a => a.AnketId)
                                                        .ToListAsync();
            var aCountKurumici = await _context.Cevaplars
                               .Where(c => anketIDsKurumici.Contains((int)c.AnketId) && c.Cevap == "a")
                               .CountAsync();
            var bCountKurumici = await _context.Cevaplars
                               .Where(c => anketIDsKurumici.Contains((int)c.AnketId) && c.Cevap == "b")
                               .CountAsync();
            var cCountKurumici = await _context.Cevaplars
                               .Where(c => anketIDsKurumici.Contains((int)c.AnketId) && c.Cevap == "c")
                               .CountAsync();
            var totalKurumici = aCountMuhendis + bCountMuhendis + cCountMuhendis;

            var percentAKurumici = ((double)aCountKurumici / totalKurumici) * 100;
            var percentBKurumici = ((double)bCountKurumici / totalKurumici) * 100;
            var percentCKurumici = ((double)cCountKurumici / totalKurumici) * 100;

            ViewData["percentAKurumici"] = percentAKurumici;
            ViewData["percentBKurumici"] = percentBKurumici;
            ViewData["percentCKurumici"] = percentCKurumici;


            return View();
        }
        public IActionResult Anket()
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

        [HttpPost]
        public IActionResult SubmitForm([FromForm] FormData formData)
        {
       
            // Yeni Anket nesnesi oluştur
            var anket = new Anket
            {
                TipId = int.Parse(formData.Tip),
                TurId = int.Parse(formData.Meslek)
            };

            // Anket veritabanına ekle
            _context.Ankets.Add(anket);
            _context.SaveChanges();

            // Cevapları ekle
            foreach (var soru in formData.Sorular)
            {
                var cevap = new Cevaplar
                {
                    AnketId = anket.AnketId,
                    SoruId = soru.QuestionId,
                    Cevap = soru.Rating
                };

                _context.Cevaplars.Add(cevap);
            }

            _context.SaveChanges();

            // İşlemden sonra bir onay sayfasına yönlendirin
            return RedirectToAction("Index");
        }

        public IActionResult Confirmation()
        {
            return View();
        }

    }
}
