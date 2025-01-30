using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class OrderConfirmationModel : PageModel
{
    public string OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    public void OnGet()
    {
        // Örnek veri - Gerçek bir projede bu veriler bir veritabanından veya API'den alınır
        OrderId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(); // Örnek Sipariş Numarası
        OrderDate = DateTime.Now;
    }
}
