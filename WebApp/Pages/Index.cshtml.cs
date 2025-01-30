using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
        
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string password { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
     List<string> Data=new  List<string>();
     public async void OnPostGetData()
    {
        
        // MySQL bağlantı bilgileriroot@127.0.0.1:3306root@127.0.0.1:3306
      MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        conn_string.Server = "127.0.0.1";
        conn_string.UserID = "root";
        conn_string.Database = "sys";
        conn_string.Port=3306;
        conn_string.SslMode=MySqlSslMode.None;
//MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());
        using (var connection = new MySqlConnection(conn_string.ToString()))
        {
            connection.Open();

           string query = $"SELECT * FROM Users WHERE Email='{Email}' AND Password='{password}';";

            using (var command = new MySqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                Data = new List<string>();
                while (reader.Read())
                {
                   if(reader.GetString("Name").Length>0)
                   {
                    Response.Redirect("/HomePage");
                    return;
                   }
                }
          TempData["ErrorMessage"] = "Hatali Giris Bilgileri Tekrar Deneyiniz!";
        //    Response.Redirect("/Index",false);
            
            }
        }
    }

    public void OnGet()
    {
          TempData["ErrorMessage"] =null;

    }
}
