using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

public class RegisterModel : PageModel
{
    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            // MySQL bağlantısı ve kayıt işlemi
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                UserID = "root",
                Database = "sys",
                Port = 3306,
                SslMode = MySqlSslMode.None
            };

            try
            {
                using (var connection = new MySqlConnection(conn_string.ToString()))
                {
                    connection.Open();

                    // SQL sorgusu: Kullanıcıyı tabloya ekleme
                    string query = "INSERT INTO Users (Name, Surname, Email, Password) " +
                                   "VALUES (@Name, @Surname, @Email, @Password);";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Parametre ekleme
                        command.Parameters.AddWithValue("@Name", FirstName);
                        command.Parameters.AddWithValue("@Surname", LastName);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Password", Password);

                        // Komutu çalıştır
                        command.ExecuteNonQuery();
                    }

                    TempData["SuccessMessage"] = "Kayit Basarili!";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Hata mesajı göster
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                return Page(); // Aynı formu tekrar yükle
            }
        }

        return Page(); // ModelState hataları varsa form yeniden yüklenir
    }
}
