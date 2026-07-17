using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages
{
    public class testModel : PageModel
    {

        public TestViewModel Vm { get; set; } = new();

        public void OnGet()
        {
            var sections = new List<ContentSection>()
            {
              new ListSection
              {
                  Heading = "en heading",
                  SubHeading ="en subis",
                  BackgroundColor = true,
                  ListItems = {
                      new(){Text="Ubuntu Server (minimal installation)", Icon = new(){ IconText = "" } },
                      new(){Text="Docker & Docker Compose", Icon = new(){ IconText = "" }},
                      new(){Text="Nginx Reverse Proxy", Icon = new(){ IconText = "" }},
                      new(){Text="PostgreSQL", Icon = new(){ IconText = "" }},
                      new(){Text="Certbot (Let´s Encrypt)", Icon = new(){ IconText = "" }},
                      new(){Text="Ubuntu Server (minimal installation)", Icon = new(){ IconText = "" }},
                      new(){Text="Docker & Docker Compose", Icon = new(){ IconText = "" }},
                  }
              }
            };

            Vm.Sections.AddRange(sections);

        }
    }
}
