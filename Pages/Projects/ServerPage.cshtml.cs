using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages.Projects
{
    public class ServerPage2Model : PageModel
    {
        public ServerPage2ViewModel Vm { get; set; } = new();

        private readonly IPageService _pageService;

        public ServerPage2Model(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task OnGet()
        {
            ViewData["Title"] = "Vps - Hosting";

            //string slug = Request.Path;

            string slug = "vps-hosting";

            var page = await _pageService.GetPageAsync(slug);
            Vm.Hero = page.Sections.OfType<HeroSection>().FirstOrDefault();

            page.Sections = page.Sections
                .Where(s => s is not HeroSection)
                .ToList();

            Vm.Page = page;
        }
    }
}
