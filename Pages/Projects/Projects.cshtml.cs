using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages.Projects
{
    public class ProjectsModel : PageModel
    {
        public ProjectsViewModel Vm { get; set; } = new();


        private readonly IPageService _pageService;

        public ProjectsModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task OnGet()
        {
            var cards = await _pageService.GetProjectCardsAsync();

            Vm.CardsSection.Cards = cards;
        }
    }
}
