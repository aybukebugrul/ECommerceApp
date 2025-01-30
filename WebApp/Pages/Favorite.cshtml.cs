using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class FavoriteModel : PageModel
    {
        public List<Products> Favorites { get; set; } = new List<Products>();

        public void OnGet()
        {
            List<Products> lst=new List<Products>();
            var Counter=0;
            foreach(var x in clsGlobal.favlistProduct)
            {
               lst.Add(new Products(){
                Id = Counter++, Name = x.Name, Description = x.Name, Price = (decimal)x.Price
               });
            }
            Favorites = lst;
            
        }
    

    public void OnPostRemove(string productName)
    {
 var product = clsGlobal.favlistProduct.FirstOrDefault(x => x.Name == productName);
        if (product != null)
        {
            clsGlobal.favlistProduct.Remove(product);
            
        }
       
    }

    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    }
}
