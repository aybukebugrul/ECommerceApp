using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalPrice => CartItems.Sum(item => item.Price * item.Quantity);

        private static List<CartItem> CartStorage = new List<CartItem>
        {
           // new CartItem { Id = 1, Name = "Product 1", Price = 10.00M, Quantity = 2, ImageUrl = "/images/product1.jpg" },
           // new CartItem { Id = 2, Name = "Product 2", Price = 15.00M, Quantity = 1, ImageUrl = "/images/product2.jpg" }
        };
   List<Product> list=  clsGlobal.listProduct;
        public void OnGet()
        {
            CartStorage.Clear();
        
           int counter=1;
            foreach(var x in list)
            {
                CartStorage.Add( new CartItem { Id = counter++, Name = x.Name, Price = decimal.Parse(x.Price.ToString()), Quantity = 1, ImageUrl = x.ImageUrl });
            }
            CartItems = CartStorage;
        }

        public IActionResult OnPostRemove(int productId)
        {
            var item = CartStorage.FirstOrDefault(i => i.Id == productId);
           clsGlobal.listProduct=clsGlobal.listProduct.Where(x=>x.Name!=item.Name).ToList();
           
            

            return RedirectToPage();
        }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}