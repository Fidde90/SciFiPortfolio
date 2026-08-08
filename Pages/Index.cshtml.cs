using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using SciFiPortfolio.Helpers;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        public IndexViewModel Vm { get; set; } = new();

        private readonly IPageService _pageService;

        public IndexModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task OnGet([FromRoute] string slug = "/")
        {
            ViewData["Title"] = "Fredrik Bengtsson | .NET Developer Portfolio";

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
