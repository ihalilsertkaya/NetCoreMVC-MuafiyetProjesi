using Microsoft.AspNetCore.Mvc;

namespace MuafiyetProjesi2024.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminPanel()
        {
            return View();
        }

   
    }
}