using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages
{
    public class ProductsModel : PageModel
    {
        public List<ProductItem> Products { get; set; } = new List<ProductItem>
        {
            new ProductItem { Id = 1, Name = "Product 1", ImageUrl = "/images/product1.jpg", Price = 10.00M },
            new ProductItem { Id = 2, Name = "Product 2", ImageUrl = "/images/product2.jpg", Price = 15.00M },
        };

        public void OnGet()
        {
            // Örnek ürünler (veritabanından gelebilir)
        }

        public IActionResult OnPostAddToCart(int productId, string name, string imageUrl, decimal price)
        {
            // Session'dan mevcut ürünleri al
            var cartSession = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cartSession)
                ? new List<ProductItem>()
                : JsonSerializer.Deserialize<List<ProductItem>>(cartSession);

            // Sepette aynı üründen varsa miktarını artır
            var existingProduct = cartItems.Find(p => p.Id == productId);
            if (existingProduct != null)
            {
                existingProduct.Quantity++;
            }
            else
            {
                // Yeni ürün ekle
                cartItems.Add(new ProductItem
                {
                    Id = productId,
                    Name = name,
                    ImageUrl = imageUrl,
                    Price = price,
                    Quantity = 1
                });
            }

            // Güncellenmiş listeyi session'a kaydet
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));

            return RedirectToPage();
        }
    }

    public class ProductItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}