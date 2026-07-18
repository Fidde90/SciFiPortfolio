using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Enums;
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
               new ImageWithPositionSection
                {
                    Heading = "Infrastrukturdesign",
                    Paragraphs =
                    {
                        new(){ Text = "Servern är uppbyggd enligt en lagerindelad arkitektur där Nginx körs direkt på värdmaskinen och fungerar som central ingresspunkt för all inkommande trafik." },
                    },
                    DesktopImage = { ImageUrl = "images/infra2.png"},
                    MobileImage = { ImageUrl="images/infrastrukturdesign.png" },
                    ImagePosition = ImagePosition.Bottom
                },
            };

            Vm.Sections.AddRange(sections);

        }
    }
}
