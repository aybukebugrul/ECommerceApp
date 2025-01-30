using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages;

public class OrderModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Postal code is required")]
    public string PostalCode { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Payment method is required")]
    public string PaymentMethod { get; set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Here you can process the order (e.g., save to database)

        TempData["SuccessMessage"] = "Your order has been placed successfully!";
        return RedirectToPage("/OrderConfirmation");
    }
}
