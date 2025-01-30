using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
namespace WebApp.Pages
{ 
public class ReportsModel : PageModel
{

    private readonly string _connectionString = "Server=localhost;Database=YourDatabaseName;User=root;Password=YourPassword;";

    public List<ProductReport> ReportsRapor { get; set; } = new List<ProductReport>();

    public void OnGet(string reportType = "daily")
    {
        LoadReports(reportType);
    }

    private void LoadReports(string reportType)
    {
        string query = reportType switch
        {
            "daily" => "SELECT Name, Price, LastUpdated FROM Fish WHERE DAY(LastUpdated)= DAY(CURDATE()) and Month(LastUpdated)= Month(CURDATE()) and Year(LastUpdated)= YEAR(CURDATE());",
            "monthly" => "SELECT Name, Price, LastUpdated FROM Fish WHERE  Month(LastUpdated)= Month(CURDATE()) and Year(LastUpdated)= YEAR(CURDATE());",
            "yearly" => "SELECT Name, Price, LastUpdated FROM Fish WHERE  Year(LastUpdated)= YEAR(CURDATE());",
            _ => "SELECT Name, Price, LastUpdated FROM Fish WHERE LastUpdated = CURDATE();"
        };
          MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        conn_string.Server = "127.0.0.1";
        conn_string.UserID = "root";
        conn_string.Database = "sys";
        conn_string.Port=3306;
        conn_string.SslMode=MySqlSslMode.None;
        using (var connection = new MySqlConnection(conn_string.ToString()))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ReportsRapor.Add(new ProductReport
                    {
                        Name = reader["Name"].ToString(),
                        Price = Convert.ToDouble(reader["Price"]),
                         
                        LastUpdated = DateTime.Parse(reader["LastUpdated"].ToString())
                    });
                }
            }
        }
    }
}
}


public class ProductReport
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int SalesCount { get; set; }
    public DateTime LastUpdated { get; set; }
}
