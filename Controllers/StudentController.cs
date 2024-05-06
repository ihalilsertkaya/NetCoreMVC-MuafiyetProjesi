using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuafiyetProjesi2024.Data;
using MuafiyetProjesi2024.Models;

namespace MuafiyetProjesi2024.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _hostingEnvironment;
        


        public StudentController(AppDbContext context, IWebHostEnvironment env)
        {
            _hostingEnvironment = env;
            _context = context;
            
        }   
        
        public IActionResult BasvuruFormu()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitApplication(Basvuru basvuruBilgisi, Evrak evrakBilgisi, Ders dersBilgisi, IFormFile Transkript, IFormFile DersIcerik)
        {

            if (ModelState.IsValid)
            {
                
                // Evrak dosyalarını yükle
                if (Transkript != null && Transkript.Length > 0)
                {
                    var transkriptDosyaYolu = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads", $"{basvuruBilgisi.OgrNo}-Transkript.pdf");
                    using (var stream = new FileStream(transkriptDosyaYolu, FileMode.Create))
                    {
                        await Transkript.CopyToAsync(stream);
                    }
                    evrakBilgisi.Transkript = transkriptDosyaYolu;
                }

                if (DersIcerik != null && DersIcerik.Length > 0)
                {
                    var dersDokumDosyaYolu = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads", $"{basvuruBilgisi.OgrNo}-DersDokum.pdf");
                    using (var stream = new FileStream(dersDokumDosyaYolu, FileMode.Create))
                    {
                        await DersIcerik.CopyToAsync(stream);
                    }
                    evrakBilgisi.DersIcerik = dersDokumDosyaYolu;
                }

                var oturumuAcanTc = TempData["oturumAcanTc"] as String;
                var oturumuAcankullanici = _context.Kullanicilar.SingleOrDefault(x => x.Tckimlik == oturumuAcanTc);
                
                basvuruBilgisi.Kullanici = oturumuAcankullanici;
                _context.Basvurular.Add(basvuruBilgisi);
                
                
                // Diğer iki modeli de kaydedin
                evrakBilgisi.Kullanici = oturumuAcankullanici;
                evrakBilgisi.Tckimlik = basvuruBilgisi.Tckimlik;
                _context.Evraklar.Add(evrakBilgisi);

                dersBilgisi.Kullanici = oturumuAcankullanici;
                dersBilgisi.Tckimlik = basvuruBilgisi.Tckimlik;
                _context.Dersler.Add(dersBilgisi);
                
                    // Değişiklikleri kaydetmeye çalışın
                    await _context.SaveChangesAsync();
                
                return RedirectToAction("Index", "Home"); // Başka bir sayfaya yönlendir
            }

            // Model geçerli değilse formu tekrar göster
            return RedirectToAction("Register", "Home"); 
        }


   
    }
}