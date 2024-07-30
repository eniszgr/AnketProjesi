﻿using AnketProjesi.Models;
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
        
        
        public async Task<IActionResult> Katilimcilar()
        {
            var ankets = await _context.Ankets.Include(x => x.Tur).Include(x => x.Tip).ToListAsync();
            return View(ankets);
        }

        public IActionResult Hakkimizda()
        {
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
            return RedirectToAction("Katilimcilar");
        }

      

    }
}
