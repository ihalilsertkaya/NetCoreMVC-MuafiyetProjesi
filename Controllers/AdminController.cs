using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MuafiyetProjesi2024.Data;
using MuafiyetProjesi2024.Models;


namespace MuafiyetProjesi2024.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AdminController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult AdminLogin()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(String Mail, String Sifre)
        {

            var admin = _context.AdminKullanicilar
                .FirstOrDefault(x => x.Mail == Mail && x.Sifre == Sifre);
            
            
            if (admin == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya parola.");
                return View();
            }
            
            string bolumBilgisi = admin.BolumBilgisi;
            TempData["userBolumBilgisi"] = bolumBilgisi;
            switch (admin.Yetkisi)
            {
                case "1":
                    return RedirectToAction("UsersPanel", "Admin");
                case "0":
                    return RedirectToAction("AdminPanel", "Admin");
                default:
                    return RedirectToAction("AdminLogin", "Admin");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UsersPanel()
        {
            // TempData'dan userBolumBilgisi'ni alıyoruz ve string türüne dönüştürüyoruz
            var bolumBilgisi = TempData["userBolumBilgisi"] as string;
            // Eğer bolumBilgisi null veya boş ise, hata gösterebiliriz veya başka bir işlem yapabiliriz
            if (string.IsNullOrEmpty(bolumBilgisi))
            {
                ModelState.AddModelError(string.Empty, "Bölüm bilgisi bulunamadı.");
                return View("AdminLogin", "Admin"); // Hata view'ını döndürebilirsiniz
            }

            // Basvurular tablosunu bolumBilgisi'ne göre filtreliyoruz
            var basvurularBolumeGore = _context.Basvurular
                .Where(b => b.GeldigiBolum == bolumBilgisi);

            // Filtrelenmiş basvurular listesi
            var basvuruListBolumeGore = await basvurularBolumeGore.ToListAsync();

            // View'a filtrelenmiş basvuruları döndürüyoruz
            return View("UsersPanel", basvuruListBolumeGore);
        }


        [HttpGet]
        public async Task<IActionResult> AdminPanel()
        {

            var basvurular = _context.Basvurular.AsQueryable();

            var basvuruList = await basvurular.ToListAsync();

            return View("AdminPanel", basvuruList);
        }

        [HttpPost]
        public async Task<IActionResult> BasvuruFiltrele(string filtreSelect)
        {
            /*var oturumTC = TempData["oturumAcanYoneticiTc"] as string;
            if (oturumTC == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }*/
            
            var basvurular = _context.Basvurular.AsQueryable();

            if (!string.IsNullOrEmpty(filtreSelect))
            {
                filtreSelect = filtreSelect.ToLower();
                basvurular = basvurular.Where(b =>
                    b.GeldigiBolum.ToLower().Contains(filtreSelect)
                );
            }

            var basvuruList = await basvurular.ToListAsync();

            return View("AdminPanel", basvuruList);
        }
       
        

    }
}