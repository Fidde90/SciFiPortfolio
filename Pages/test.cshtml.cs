using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages
{
    public class TestModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public TestViewModel Vm { get; set; } = new();

        public TestModel(IWebHostEnvironment env) => _env = env;

        public void OnGet()
        {

            if (!_env.IsDevelopment())
                return;

            var sections = new List<ContentSection>()
            {
               
        
            };

            Vm.Sections.AddRange(sections);
        }
    }
}
