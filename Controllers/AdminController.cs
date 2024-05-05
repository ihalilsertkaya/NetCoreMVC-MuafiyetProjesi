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

        
        public IActionResult AdminLogin(AdminKullanicilar adminKullanici)
        {
            var admin = _context.AdminKullanicilars
                .Any(x => x.UserName == adminKullanici.UserName && x.Parola == adminKullanici.Parola);

            if (admin)
            {
                return RedirectToAction("AdminPanel", "Admin");
            }

            ModelState.AddModelError(string.Empty, "Geçersiz kullanýcý adý veya parola.");

            return View();
        }


        [HttpGet]
        public IActionResult AdminPanel()
        {
            return View();
        }

   
    }
}