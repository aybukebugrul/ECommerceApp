using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Pages
{ 
    public class AdminPanelModel : PageModel
{
 
     string _connectionString = "Server=127.0.0.1;Database=sys;User=root;Port=3306";

    public List<fisher> Productsion { get; set; } = new List<fisher>();

    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public double Price { get; set; }

    [BindProperty]
    public string ImageUrl { get; set; }

    [BindProperty]
    public string Category { get; set; }
        [BindProperty]
    public string Stock { get; set; }
    public void OnGet()
    {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        conn_string.Server = "127.0.0.1";
        conn_string.UserID = "root";
        conn_string.Database = "sys";
        conn_string.Port=3306;
        conn_string.SslMode=MySqlSslMode.None;
        _connectionString=conn_string.ToString();
        LoadProductsFromDatabase();
    }

    public IActionResult OnPostAdd()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = "INSERT INTO Fish (Name, Price, ImageUrl,Category,Stock) VALUES (@Name, @Price, @ImageUrl,@Category,@Stock)";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Price", Price);
                command.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                command.Parameters.AddWithValue("@Category", Category);
                command.Parameters.AddWithValue("@Stock", Stock);
                command.ExecuteNonQuery();
            }
        }

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(string name)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = "DELETE FROM Fish WHERE Name = @Name";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.ExecuteNonQuery();
            }
        }

        return RedirectToPage();
    }

    private void LoadProductsFromDatabase()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT Name, Price, ImageUrl,Category,Stock FROM Fish";
            using (var command = new MySqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new fisher
                    {
                        Name = reader["Name"].ToString(),
                        Price = double.Parse(reader["Price"].ToString()),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        Category=reader["Category"].ToString(),
                         Stock=reader["Stock"].ToString(),
                    };
                    Productsion.Add(product);
                }
            }
        }
    }
}
    }
    public class fisher
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
     public string Category { get; set; }
     public string Stock {get;set;}
}



