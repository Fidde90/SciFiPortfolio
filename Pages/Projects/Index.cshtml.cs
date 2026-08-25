using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages.Projects
{
    public class ProjectsModel : PageModel
    {
        public ProjectsViewModel Vm { get; set; } = new();

        private readonly IPageService _pageService;
        private readonly IMemoryCache _cache;

        public ProjectsModel(
            IPageService pageService,
            IMemoryCache cache)
        {
            _pageService = pageService;
            _cache = cache;
        }

        public async Task OnGetAsync()
        {
            Console.WriteLine($"EXECUTED {DateTime.UtcNow:O}");

            var cards = await _cache.GetOrCreateAsync(
                "project-cards",
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(30);

                    Console.WriteLine("DATABASE QUERY");

                    return await _pageService.GetProjectCardsAsync();
                });

            Vm.CardsSection.Cards = cards!;
            Vm.CardsSection.PaddingsCss = "pt-2 pb-2";
            Vm.CardsSection.SpaceBottom = false;
        }
    }
}