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
            if(total == 0) {
                total= 1;
            }

            var percentA = ((double)aCount / total) * 100;
            var percentB = ((double)bCount / total) * 100;
            var percentC = ((double)cCount / total) * 100;

            ViewData["percentA"] = percentA;
            ViewData["percentB"] = percentB;
            ViewData["percentC"] = percentC;

            //Ogrenciler
            var anketIDsOgrenci = await _context.Ankets.Where(a => a.TurId == 5)
                                                        .Select(a => a.AnketId)
                                                        .ToListAsync();
            var aCountOgrenci = await _context.Cevaplars
                               .Where(c => anketIDsOgrenci.Contains((int)c.AnketId) && c.Cevap == "a")
                               .CountAsync();
            var bCountOgrenci = await _context.Cevaplars
                               .Where(c => anketIDsOgrenci.Contains((int)c.AnketId) && c.Cevap == "b")
                               .CountAsync();
            var cCountOgrenci = await _context.Cevaplars
                               .Where(c => anketIDsOgrenci.Contains((int)c.AnketId) && c.Cevap == "c")
                               .CountAsync();
            var totalOgrenci = aCountOgrenci + bCountOgrenci + cCountOgrenci;

            if(totalOgrenci == 0)
                totalOgrenci = 1;

            var percentAOgrenci = ((double)aCountOgrenci / totalOgrenci) * 100;
            var percentBOgrenci = ((double)bCountOgrenci / totalOgrenci) * 100;
            var percentCOgrenci = ((double)cCountOgrenci / totalOgrenci) * 100;

            ViewData["percentAOgrenci"] = percentAOgrenci;
            ViewData["percentBOgrenci"] = percentBOgrenci;
            ViewData["percentCOgrenci"] = percentCOgrenci;

            //Kurum ici
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
            var totalKurumici = aCountKurumici + bCountKurumici + cCountKurumici;
            if(totalKurumici == 0)
                totalKurumici = 1;

            var percentAKurumici = ((double)aCountKurumici / totalKurumici) * 100;
            var percentBKurumici = ((double)bCountKurumici / totalKurumici) * 100;
            var percentCKurumici = ((double)cCountKurumici / totalKurumici) * 100;

            ViewData["percentAKurumici"] = percentAKurumici;
            ViewData["percentBKurumici"] = percentBKurumici;
            ViewData["percentCKurumici"] = percentCKurumici;


            //Kurumdisi
            var anketIDsKurumdisi = await _context.Ankets.Where(a => a.TipId == 2)
                                                        .Select(a => a.AnketId)
                                                        .ToListAsync();
            var aCountKurumdisi = await _context.Cevaplars
                               .Where(c => anketIDsKurumdisi.Contains((int)c.AnketId) && c.Cevap == "a")
                               .CountAsync();
            var bCountKurumdisi = await _context.Cevaplars
                               .Where(c => anketIDsKurumdisi.Contains((int)c.AnketId) && c.Cevap == "b")
                               .CountAsync();
            var cCountKurumdisi = await _context.Cevaplars
                               .Where(c => anketIDsKurumdisi.Contains((int)c.AnketId) && c.Cevap == "c")
                               .CountAsync();
            var totalKurumdisi = aCountKurumdisi + bCountKurumdisi + cCountKurumdisi;
            
            if(totalKurumdisi == 0)
                totalKurumdisi = 1;

            var percentAKurumdisi = ((double)aCountKurumdisi / totalKurumdisi) * 100;
            var percentBKurumdisi = ((double)bCountKurumdisi / totalKurumdisi) * 100;
            var percentCKurumdisi = ((double)cCountKurumdisi / totalKurumdisi) * 100;

            ViewData["percentAKurumdisi"] = percentAKurumdisi;
            ViewData["percentBKurumdisi"] = percentBKurumdisi;
            ViewData["percentCKurumdisi"] = percentCKurumici;

            //mühendislerin verileri
            var anketIDs = await _context.Ankets.Where(a => a.TurId == 1)
                                                .Select(a => a.AnketId)
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
            var totalmuh = aCountMuhendis + bCountMuhendis + cCountMuhendis;
            if (totalmuh == 0)
                totalmuh = 1;

            var percentAMuhendis = ((double)aCountMuhendis / totalmuh) * 100;
            var percentBMuhendis = ((double)bCountMuhendis / totalmuh) * 100;
            var percentCMuhendis = ((double)cCountMuhendis / totalmuh) * 100;

            ViewData["percentAMuhendis"] = percentAMuhendis;
            ViewData["percentBMuhendis"] = percentBMuhendis;
            ViewData["percentCMuhendis"] = percentCMuhendis;


            //Doktor verileri
            var anketIDsDoktor = await _context.Ankets.Where(a => a.TurId == 2)
                                                .Select(a => a.AnketId)
                                                .ToListAsync();

            var aCountDoktor = await _context.Cevaplars
                                .Where(c => anketIDsDoktor.Contains((int)c.AnketId) && c.Cevap == "a")
                                .CountAsync();
            var bCountDoktor = await _context.Cevaplars
                               .Where(c => anketIDsDoktor.Contains((int)c.AnketId) && c.Cevap == "b")
                               .CountAsync();
            var cCountDoktor = await _context.Cevaplars
                               .Where(c => anketIDsDoktor.Contains((int)c.AnketId) && c.Cevap == "c")
                               .CountAsync();
            var totaldoktor = aCountDoktor + bCountDoktor + cCountDoktor;
            if (totaldoktor == 0)
                totaldoktor = 1;

            var percentADoktor = ((double)aCountDoktor / totaldoktor) * 100;
            var percentBDoktor = ((double)bCountDoktor / totaldoktor) * 100;
            var percentCDoktor = ((double)cCountDoktor / totaldoktor) * 100;

            ViewData["percentADoktor"] = percentADoktor;
            ViewData["percentBDoktor"] = percentBDoktor;
            ViewData["percentCDoktor"] = percentCDoktor;

            // Memur verileri
            var anketIDsMemur = await _context.Ankets
                .Where(a => a.TurId == 3)
                .Select(a => a.AnketId)
                .ToListAsync();

            var aCountMemur = await _context.Cevaplars
                .Where(c => anketIDsMemur.Contains((int)c.AnketId) && c.Cevap == "a")
                .CountAsync();

            var bCountMemur = await _context.Cevaplars
                .Where(c => anketIDsMemur.Contains((int)c.AnketId) && c.Cevap == "b")
                .CountAsync();

            var cCountMemur = await _context.Cevaplars
                .Where(c => anketIDsMemur.Contains((int)c.AnketId) && c.Cevap == "c")
                .CountAsync();

            var totalMemur = aCountMemur + bCountMemur + cCountMemur;

            // Yüzde hesaplama
            var percentAMemur = ((double)aCountMemur / totalMemur) * 100;
            var percentBMemur = ((double)bCountMemur / totalMemur) * 100;
            var percentCMemur = ((double)cCountMemur / totalMemur) * 100;

            // ViewData ayarlama
            ViewData["percentAMemur"] = percentAMemur;
            ViewData["percentBMemur"] = percentBMemur;
            ViewData["percentCMemur"] = percentCMemur;


            //Müdür verileri
            var anketIDsMudur = await _context.Ankets.Where(a => a.TurId == 4)
                                                .Select(a => a.AnketId)
                                                .ToListAsync();

            var aCountMudur = await _context.Cevaplars
                                .Where(c => anketIDsMudur.Contains((int)c.AnketId) && c.Cevap == "a")
                                .CountAsync();

            var bCountMudur = await _context.Cevaplars
                               .Where(c => anketIDsMudur.Contains((int)c.AnketId) && c.Cevap == "b")
                               .CountAsync();
            var cCountMudur = await _context.Cevaplars
                               .Where(c => anketIDsMudur.Contains((int)c.AnketId) && c.Cevap == "c")
                               .CountAsync();
            var totalmudur = aCountMudur + bCountMudur + cCountMudur;
            if (totalmudur == 0)
                totalmudur = 1;

            var percentAMudur = ((double)aCountMudur / totalmudur) * 100;
            var percentBMudur = ((double)bCountMudur / totalmudur) * 100;
            var percentCMudur = ((double)cCountMudur / totalmudur) * 100;

            ViewData["percentAMudur"] = percentAMudur;
            ViewData["percentBMemur"] = percentBMudur;
            ViewData["percentCMudur"] = percentCMudur;


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
