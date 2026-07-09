using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SciFiPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Fredrik Bengtsson | .NET Developer Portfolio";
        }
    }
}
