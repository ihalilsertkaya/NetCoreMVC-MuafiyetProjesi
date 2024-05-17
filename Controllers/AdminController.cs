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
            HttpContext.Session.Clear();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(AdminKullanici adminGiris)
        {

            var admin = _context.AdminKullanicilar
                .Any(x => x.Mail == adminGiris.Mail && x.Sifre == adminGiris.Sifre);

            if (admin)
            {
                // Oturum verisi oluşturma ve kullanıcı adını saklama
                HttpContext.Session.SetString("UserMail", adminGiris.Mail);
                HttpContext.Session.SetString("KullaniciYetki", "1"); //1 stringi admindir

                return RedirectToAction("AdminPanel", "Admin");  

            }
            ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya parola.");

            return View();
        }


        [HttpGet]
        public IActionResult AdminPanel()
        {
            if (HttpContext.Session.GetString("UserMail") == null || HttpContext.Session.GetString("KullaniciYetki") == "0") //oturum kapalıysa veya öğrenci ise
            {
                return RedirectToAction("AdminLogin", "Admin"); //admin girişe at
            }

            // Oturum açık ise, gerekli işlemleri yap

            return View();
        }

   
    }
}