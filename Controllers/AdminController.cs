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
            TempData["adminYetkisi"] = admin.Yetkisi;
            switch (admin.Yetkisi)
            {
                case "1":
                    return RedirectToAction("UsersPanel", "Admin");
                case "0":
                    return RedirectToAction("AdminPanel", "Admin");
                default:
                    return RedirectToAction("AdminLogin", "Admin");
            }


        }

        [HttpGet]
        public async Task<IActionResult> UsersPanel()
        {
            var bolumBilgisi = TempData["userBolumBilgisi"] as string;
            var adminYetkisi = TempData["adminYetkisi"] as string;
            if (adminYetkisi!="1") {
                return RedirectToAction("AdminLogin", "Admin");
            }


            if (string.IsNullOrEmpty(bolumBilgisi))
            {
                ModelState.AddModelError(string.Empty, "Bölüm bilgisi bulunamadı.");
                return View("AdminLogin", "Admin");
            }

            var basvurularBolumeGore = _context.Basvurular
                .Where(b => b.GeldigiBolum == bolumBilgisi);

            var basvuruListBolumeGore = await basvurularBolumeGore.ToListAsync();

            return View("UsersPanel", basvuruListBolumeGore);
        }


        [HttpGet]
        public async Task<IActionResult> AdminPanel()
        {
            var adminYetkisi = TempData["adminYetkisi"] as string;
            if (adminYetkisi != "0")
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

            var basvurular = await _context.Basvurular.ToListAsync();
            var adminKullanicilar = await _context.AdminKullanicilar.ToListAsync();

            var viewModel = new AdminViewModel
            {
                Basvurular = basvurular,
                AdminKullanicilar = adminKullanicilar
            };

            return View("AdminPanel", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> BasvuruFiltrele(string filtreSelect)
        {
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

        [HttpPost]

        public async Task<IActionResult> DeleteUser(string email)
        {
            var adminKullanici = await _context.AdminKullanicilar.FindAsync(email);
            if (adminKullanici == null)
            {
                return NoContent(); 
            }

            _context.AdminKullanicilar.Remove(adminKullanici);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        public async Task<IActionResult> HocaEkleme(AdminKullanici kullanici)
        {
            ModelState.Clear();
            // Veritabanında kullanıcı var mı kontrol ediyoruz
            var existingUser = await _context.AdminKullanicilar.FirstOrDefaultAsync(x => x.Mail == kullanici.Mail ) ;

            if (existingUser != null)
            {
                // Eğer kullanıcı varsa, hata mesajı ekleyip aynı görünümü tekrar gösteriyoruz
                ModelState.AddModelError(string.Empty, "Böyle bir Kullanıcı zaten kullanılmaktadır.");
                return View();
            }

            // Eğer kullanıcı yoksa, yeni kullanıcıyı ekliyoruz
            _context.AdminKullanicilar.Add(kullanici);
            await _context.SaveChangesAsync();

            // Başarılı kayıt durumunda yönlendirme
            return RedirectToAction("Index");
        }
    }
}
