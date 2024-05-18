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
                .Any(x => x.Mail == adminGiris.Mail && x.Sifre == adminGiris.Sifre);

            if (admin)
            {
                TempData["oturumAcanYoneticiTc"] = adminGiris.Mail;
                return RedirectToAction("AdminPanel", "Admin");
            }
            ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya parola.");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminPanel()
        {
            var oturumTC = TempData["oturumAcanYoneticiTc"] as string;
            if (oturumTC == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

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

            TempData["oturumAcanYoneticiTc"] = "yoneticiTC";

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
            var oturumTC = TempData["oturumAcanYoneticiTc"] as string;
            if (oturumTC == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

            var basvurular = _context.Basvurular.AsQueryable();

            var basvuruList = await basvurular.ToListAsync();

            return View("UsersPanel", basvuruList);
        }

    }
}