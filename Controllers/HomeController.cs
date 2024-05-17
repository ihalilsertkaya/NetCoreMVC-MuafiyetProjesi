﻿using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MuafiyetProjesi2024.Data;
using MuafiyetProjesi2024.Models;

namespace MuafiyetProjesi2024.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public HomeController(IConfiguration configuration,AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Index(Kullanici kullanici)
        {

             var result = _context.Kullanicilar
                .Any(x => x.Mail == kullanici.Mail && x.Parola == kullanici.Parola);
            //sorguyu yukaridan yapiyoruz. result true veya false dönüyor. Models'daki veri ile veritabani kiyaslaniyor. ??

            if (result)
            {
                var OturumAcanKullanici= _context.Kullanicilar
                    .SingleOrDefault(x => x.Mail == kullanici.Mail && x.Parola == kullanici.Parola);
                TempData["oturumAcanTc"] = OturumAcanKullanici.Tckimlik;

                // Oturum verisi oluşturma ve kullanıcı adını saklama
                HttpContext.Session.SetString("UserMail", kullanici.Mail);
                HttpContext.Session.SetString("KullaniciYetki", "0"); //0 stringi öğrencidir
                return RedirectToAction("BasvuruFormu", "Student");
            }

            ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya parola.");
            
            return View();
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            HttpContext.Session.Clear();
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(Kullanici kullanici)
        {
            ModelState.Clear();
            // Veritabanında kullanıcı var mı kontrol ediyoruz
            var existingUser = await _context.Kullanicilar.FirstOrDefaultAsync(x => x.Mail == kullanici.Mail || x.Tckimlik == kullanici.Tckimlik ) ;

            if (existingUser != null)
            {
                // Eğer kullanıcı varsa, hata mesajı ekleyip aynı görünümü tekrar gösteriyoruz
                ModelState.AddModelError(string.Empty, "Bu e-posta adresi veya Tc kimlik no zaten kullanılmaktadır.");
                return View();
            }

            // Eğer kullanıcı yoksa, yeni kullanıcıyı ekliyoruz
            _context.Kullanicilar.Add(kullanici);
            await _context.SaveChangesAsync();

            // Başarılı kayıt durumunda yönlendirme
            return RedirectToAction("Index");
        }
        
         public IActionResult Error()
         
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    }
    
}
