using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Helpers;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages.Projects
{
    public class ReceptakutenFrontendModel : PageModel
    {
        public ReceptakutenFrontendViewModel Vm { get; set; } = new();

        private readonly IPageService _pageService;

        public ReceptakutenFrontendModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task OnGetAsync([FromQuery] string slug)
        {
            ViewData["Title"] = "Receptakuten - Frontend";

            var page = await _pageService.GetPageAsync(slug);

            if (page is null)
                return;

            Vm.Hero = new HeroSection
            {
                Heading = "Receptakuten - Frontend",
                TextColor = "sci-fi-glow",
            };
            page.Sections = PageHelper.FilterHero(page);
            Vm.Page = page;
            Vm.ShowTitle = false;
        }
    }
}
