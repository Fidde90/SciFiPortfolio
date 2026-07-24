using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Helpers;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages.Projects
{
    public class ReceptakutenBackendModel : PageModel
    {
        public ReceptakutenBackendViewModel Vm { get; set; } = new();

        private readonly IPageService _pageService;

        public ReceptakutenBackendModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task OnGetAsync([FromRoute] string slug)
        {
            ViewData["Title"] = "Receptakuten - Backend";

            var page = await _pageService.GetPageAsync(slug);

            if (page is null)
                return;

            Vm.Hero = new HeroSection
            {
                Heading = "Receptakuten - Backend",
                TextColor = "sci-fi-glow",
            };
            page.Sections = PageHelper.FilterHero(page);
            Vm.Page = page;
            Vm.ShowTitle = false;
        }
    }
}
