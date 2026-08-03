using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Helpers;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages.Projects
{
    public class ReceptakutenModel : PageModel
    {
        public ReceptakutenViewModel Vm { get; set; } = new();

        private readonly IPageService _pageService;

        public ReceptakutenModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task OnGetAsync([FromRoute] string slug = "receptakuten")
        {
            ViewData["Title"] = "Receptakuten";

            var page = await _pageService.GetPageAsync(slug);

            if (page is null)
                return;

            Vm.Hero = new HeroSection
            {
               Image = new() { ImageUrl ="images/recept-bil-ub.png" },
               HeroImageSizeCssClass = "large",
            };  
            page.Sections = PageHelper.FilterHero(page);
            Vm.Page = page;
            Vm.ShowTitle = false;
        }
    }
}
