using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models.ContentSections;
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

        public async Task OnGet()
        {
            ViewData["Title"] = "Fredrik Bengtsson | .NET Developer Portfolio";

            Vm.Hero = new HeroSection
            {
                Heading = "Fredrik Bengtsson",
                SubHeading = "Fullstack .Net Utvecklare",
                SpaceBottom = true
            };

            Vm.Carousel = new CarouselSection
            {
                Images =
                {
                    new() { ImageUrl = "images/vue.svg", AltText = "vue icon" },
                    new() { ImageUrl = "images/javascript.svg", AltText = "javascript icon" },
                    new() { ImageUrl = "images/net.svg", AltText = ".net icon" },
                    new() { ImageUrl = "images/csharp.svg", AltText = "c# icon" },
                    new() { ImageUrl = "images/azure.svg", AltText = "azure icon" },
                    new() { ImageUrl = "images/api.svg", AltText = "api icon" },
                    new() { ImageUrl = "images/docker.svg", AltText = "docker icon" },
                    new() { ImageUrl = "images/nginx.svg", AltText = "nginX icon" },
                    new() { ImageUrl = "images/postgres-ub.png", AltText = "postgresql icon" },
                },
            };

            Vm.Cards = new CardSection()
            {
                Cards = await _pageService.GetProjectCardsAsync(3)
            };

            Vm.ShowTitle = false;
        }
    }
}
