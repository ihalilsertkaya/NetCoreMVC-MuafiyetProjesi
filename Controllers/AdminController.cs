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
        public async Task<IActionResult> AdminLogin(AdminKullanici adminGiris)
        {
            var admin = _context.AdminKullanicilar
                .Any(x => x.UserName == adminGiris.UserName && x.Parola == adminGiris.Parola);

            if (admin)
            {
                var OturumAcanKullanici = _context.AdminKullanicilar
             .SingleOrDefault(x => x.UserName == adminGiris.UserName && x.Parola == adminGiris.Parola);
                TempData["oturumAcanYoneticiTc"] = OturumAcanKullanici.Tckimlik;
                return RedirectToAction("AdminPanel", "Admin");
            }

            ModelState.AddModelError(string.Empty, "Ge�ersiz kullan�c� ad� veya parola.");

            return View();
        }


        [HttpGet]
        public IActionResult AdminPanel()
        {
            var oturumTC = TempData["oturumAcanYoneticiTc"] as String;
            if (oturumTC == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            return View();
        }

   
    }
}