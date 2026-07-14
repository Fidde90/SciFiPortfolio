using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        public IndexViewModel Vm { get; set; } = new();

        private readonly IImageService _imageService;
        private readonly IPageService _pageService;

        public IndexModel(IImageService imageService, IPageService pageService)
        {
            _imageService = imageService;
            _pageService = pageService;
        }

        public async Task OnGet()
        {
            ViewData["Title"] = "Fredrik Bengtsson | .NET Developer Portfolio";

            string slug = Request.Path;

            Vm.TechIconsSlider = await _imageService.GetTechIcons();
            Vm.Page = await _pageService.GetPageAsync(slug);
        }
    }
}
