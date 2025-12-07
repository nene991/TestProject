using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestProject.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ? わざと危険なコード（SQLインジェクション）
        public IActionResult Dangerous(string userInput)
        {
            string connectionString = "Server=localhost;Database=TestDb;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // ?? ユーザー入力を直接 SQL に連結（超危険）
                string sql = "SELECT * FROM Users WHERE Name = '" + userInput + "'";

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                // 本来は何もしない（検出目的）
                return Content("Executed");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
