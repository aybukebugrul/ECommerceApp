using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class ProfileModel : PageModel
{
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public decimal WalletBalance { get; set; }
    public List<string> Addresses { get; set; } = new List<string>();
    public List<string> Cards { get; set; } = new List<string>();

    public void OnGet()
    {
        // Örnek veriler
        UserName = "John";
        UserSurname = "Doe";
        WalletBalance = 100.50m;
        Addresses = new List<string> { "123 Main St", "456 Elm St" };
        Cards = new List<string> { "Visa **** 1234", "MasterCard **** 5678" };
    }

    public IActionResult OnPostUpdateName(string NewName, string NewSurname)
    {
        UserName = NewName;
        UserSurname = NewSurname;
        return RedirectToPage();
    }

    public IActionResult OnPostAddAddress(string NewAddress)
    {
        Addresses.Add(NewAddress);
        return RedirectToPage();
    }

    public IActionResult OnPostAddCard(string NewCard)
    {
        Cards.Add(NewCard);
        return RedirectToPage();
    }
}
