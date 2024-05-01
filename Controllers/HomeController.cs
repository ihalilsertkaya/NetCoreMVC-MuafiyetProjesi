using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MuafiyetProjesi2024.Models;

namespace MuafiyetProjesi2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult Index(Kullanicilar uc)
        {
            string connectionString = _configuration.GetConnectionString("SqlCon");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM [dbo].[Kullanicilar] WHERE Mail = @Mail AND Parola = @Parola";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Mail", uc.Mail);
                    command.Parameters.AddWithValue("@Parola", uc.Parola);
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        return RedirectToAction("BasvuruFormu", "Student");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya parola.");
                        return View();
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Kullanicilar uc)
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
