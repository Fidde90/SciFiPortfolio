using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Helpers;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages.Projects
{
    public class ReceptakutenPageModel : PageModel
    {
        public ReceptakutenViewModel Vm { get; set; } = new();

        private readonly IPageService _pageService;

        public ReceptakutenPageModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task OnGetAsync([FromRoute] string slug)
        {
            ViewData["Title"] = "Receptakuten";

            var page = await _pageService.GetPageAsync(slug);

            if (page is null)
                return;

            Vm.Hero = PageHelper.GetHeroSection(page);
            page.Sections = PageHelper.FilterHero(page);
            Vm.Page = page;
            Vm.ShowTitle = false;
        }
    }
}
