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
                
            }

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
        [HttpGet]
        public async Task<IActionResult> UsersPanel()
        {
            var basvurular = _context.Basvurular.AsQueryable();

            var basvuruList = await basvurular.ToListAsync();

            return View("UsersPanel", basvuruList);
        }

    }
}