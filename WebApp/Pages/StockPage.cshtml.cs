using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Pages
{
    public class StockPageModel : PageModel
    {
        private readonly string _connectionString = "Server=localhost;Database=YourDatabaseName;User=root;Password=YourPassword;";

        public List<CategoryStock> CategoryStocks { get; set; } = new List<CategoryStock>();

        public void OnGet()
        {
            LoadStockByCategory();
        }

        private void LoadStockByCategory()
        {
                      MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        conn_string.Server = "127.0.0.1";
        conn_string.UserID = "root";
        conn_string.Database = "sys";
        conn_string.Port=3306;
        conn_string.SslMode=MySqlSslMode.None;
            using (var connection = new MySqlConnection(conn_string.ToString()))
            {
                connection.Open();
                var query = @"
                    SELECT Category, SUM(Stock) AS TotalStock
                    FROM Fish
                    GROUP BY Category";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CategoryStocks.Add(new CategoryStock
                        {
                            Category = reader["Category"].ToString(),
                            TotalStock = int.Parse(reader["TotalStock"].ToString())
                        });
                    }
                }
            }
        }
    }

    public class CategoryStock
    {
        public string Category { get; set; }
        public int TotalStock { get; set; }
    }
}
