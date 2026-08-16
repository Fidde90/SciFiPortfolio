using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages
{
    public class PagebuilderModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public TestViewModel Vm { get; set; } = new();

        public PagebuilderModel(IWebHostEnvironment env) => _env = env;

        public IActionResult OnGet()
        {

            if (!_env.IsDevelopment())
                return RedirectToPage("Error/404");

            var sections = new List<ContentSection>()
            {

            };
            
            if(Vm.Sections.Any())
                Vm.Sections.AddRange(sections);

            return null!;
        }
    }
}
