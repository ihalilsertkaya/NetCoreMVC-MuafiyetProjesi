using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MuafiyetProjesi2024.Data;
using MuafiyetProjesi2024.Models;

namespace MuafiyetProjesi2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public HomeController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Kullanicilars kullanici)
        { 
            var result = _context.Kullanicilar
                .Any(x => x.Mail == kullanici.Mail && x.Parola == kullanici.Parola);
            //sorguyu yukaridan yapiyoruz. result true veya false dönüyor. Models'daki veri ile veritabani kiyaslaniyor. ??

            if (result)
            {
                return RedirectToAction("BasvuruFormu", "Student");
            }

            ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya parola.");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Kullanicilars uc)
        {
            string connectionString = _configuration.GetConnectionString("SqlCon");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Kontrol için sorgu
                string checkQuery = "SELECT COUNT(*) FROM [dbo].[Kullanicilar] WHERE Mail = @Mail ";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    connection.Open();
                    checkCommand.Parameters.AddWithValue("@Mail", uc.Mail);
                    int existingUserCount = (int)checkCommand.ExecuteScalar();
                    if (existingUserCount > 0)
                    {
                        // Eğer e-posta adresi veritabanında varsa, kullanıcıya uygun bir mesaj göster
                        ModelState.AddModelError(string.Empty, "Bu e-posta adresi zaten kullanılmaktadır.");
                        return View();
                    }
                }

                // Eğer e-posta adresi veritabanında yoksa, kayıt işlemine devam et
                string insertQuery = "INSERT INTO [dbo].[Kullanicilar] (TCKimlik,Mail,Parola) VALUES (@TCKimlik,@Mail, @Parola)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@TCKimlik", uc.TCKimlik);
                    command.Parameters.AddWithValue("@Mail", uc.Mail);
                    command.Parameters.AddWithValue("@Parola", uc.Parola);
                    command.ExecuteNonQuery();
                }
            }
            // Başarılı kayıt durumunda yönlendirme
            return RedirectToAction("Index");
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
