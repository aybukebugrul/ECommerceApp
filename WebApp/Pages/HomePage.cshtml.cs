using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace WebApp.Pages;

public class HomePageModel : PageModel
{
    public List<Product> Products { get; set; } = new List<Product>();

    [TempData]
    public string Message { get; set; }

    [BindProperty]
    public string NewProductName { get; set; }

    [BindProperty]
    public double NewProductPrice { get; set; }

    [BindProperty]
    public string NewProductImageUrl { get; set; }
        public List<Product> FilteredProducts { get; set; } = new List<Product>();

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? MaxPrice { get; set; }
    public List<Product> cart=new List<Product>();
    public void OnGet()
    {
        
      
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

           string query = $"SELECT * FROM Fish;";

            using (var command = new MySqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                
                while (reader.Read())
                {
                    var fish = new Product
            {
                Name = reader["Name"].ToString(),
                Price = Convert.ToDouble(reader["Price"]),
                ImageUrl = reader["ImageUrl"].ToString()
            };

            Products.Add(fish);
                }
                 FilteredProducts = Products.Where(p =>
                (string.IsNullOrEmpty(SearchName) || p.Name.Contains(SearchName, System.StringComparison.OrdinalIgnoreCase)) &&
                (!MinPrice.HasValue || p.Price >= MinPrice.Value) &&
                (!MaxPrice.HasValue || p.Price <= MaxPrice.Value)
            ).ToList();
          TempData["ErrorMessage"] = "Hatali Giris Bilgileri Tekrar Deneyiniz!";
        //    Response.Redirect("/Index",false);
            
            }
        // Varsayılan ürünler
       // Products = new List<Product>
        //{
           /* new Product { Name = "Annularis Angel", Price = 4.00, ImageUrl = "/images/product1.jpeg" },
            new Product { Name = "Mandarin", Price = 5.00, ImageUrl = "/images/product2.jpeg" },
            new Product { Name = "Nemo", Price = 4.50, ImageUrl = "/images/product3.jpeg" },
            new Product { Name = "Coral Reef", Price = 3.45, ImageUrl = "/images/product4.jpeg" },
            new Product { Name = "Celestial Eye Goldfish", Price = 9.00, ImageUrl = "/images/product5.jpeg" },
            new Product { Name = "Starfish", Price = 7.00, ImageUrl = "/images/product6.jpeg" },
            new Product { Name = "Red Beta", Price = 5.00, ImageUrl = "/images/product7.jpeg" },
            new Product { Name = "Pufferfish", Price = 5.45, ImageUrl = "/images/product8.jpeg" },
            new Product { Name = "Volitan", Price = 8.00, ImageUrl = "/images/product9.jpeg" },
            new Product { Name = "Hipokampus", Price = 13.00, ImageUrl = "/images/product10.jpeg" },
            new Product { Name = "Seahorse", Price = 13.00, ImageUrl = "/images/product11.jpeg" },
            new Product { Name = "Caretta", Price = 15.00, ImageUrl = "/images/product12.jpeg" },
            new Product { Name = "Octopus", Price = 15.95, ImageUrl = "/images/product13.jpeg" },
            new Product { Name = "Goldfish", Price = 2.00, ImageUrl = "/images/product14.jpeg" },
            new Product { Name = "Bubble Eye", Price = 5.00, ImageUrl = "/images/product15.jpeg" }*/
       // };
    }
    }
     public IActionResult OnPostAddFavorite(string productName, double productPrice, string productImageUrl)
    {       
        // Add the new product to the cart
        clsGlobal.favlistProduct.Add( new Product()
        {
            Name = productName,
            Price = productPrice,
            ImageUrl = productImageUrl,
        });
        return RedirectToPage();
    }

    public IActionResult OnPostAddProduct()
    {
        if (!string.IsNullOrWhiteSpace(NewProductName) && !string.IsNullOrWhiteSpace(NewProductImageUrl))
        {
            Products.Add(new Product
            {
                Name = NewProductName,
                Price = NewProductPrice,
                ImageUrl = NewProductImageUrl
            });
            TempData["Message"] = $"{NewProductName} has been added!";
        }
        return RedirectToPage();
    }

    public IActionResult OnPostAddToCart(string productName, double productPrice, string productImageUrl)
    {
     
          


        // Add the new product to the cart
        clsGlobal.listProduct.Add( new Product()
        {
            Name = productName,
            Price = productPrice,
            ImageUrl = productImageUrl,
        });

     
   
        return RedirectToPage();

    }
}
public class ProductModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
}

