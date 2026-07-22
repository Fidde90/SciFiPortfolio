using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Helpers;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages.Projects
{
    public class HostingPageModel : PageModel
    {
        public HostingViewModel Vm { get; set; } = new();

        private readonly IPageService _pageService;

        public HostingPageModel(IPageService pageService)
        {
            _pageService = pageService;
        }


        public async Task OnGetAsync([FromRoute] string slug)
        {
            ViewData["Title"] = "Vps - Hosting";

            var page = await _pageService.GetPageAsync(slug);

            if (page is null)
                return;

            Vm.Hero = PageHelper.GetHeroSection(page);
            page.Sections = PageHelper.FilterHero(page);
            Vm.Page = page;
        }
    }
}
