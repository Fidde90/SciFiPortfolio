using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Interfaces.Services;
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
            string slug = "vps-hosting-2";

            Vm.Page = await _pageService.GetPageAsync(slug);
        }
    }
}
