using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Pages
{
    public class AboutModel : PageModel
    {
        public List<ContentSection> Sections = [];

        public async Task OnGet()
        {
            Sections = 
            [
                new HeroSection
                {
                    Heading = "Om mig",
                }
            ];      
        }
    }
}
