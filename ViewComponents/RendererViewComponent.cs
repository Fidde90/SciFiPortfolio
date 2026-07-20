using Microsoft.AspNetCore.Mvc;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.ViewComponents
{
    public class RendererViewComponent : ViewComponent
    {
        private readonly IRendererService _rendererService;

        public RendererViewComponent(IRendererService renderer) =>  _rendererService = renderer; 

        public async Task<IViewComponentResult> InvokeAsync(List<ContentSection> sections)
        {
            var partialData = _rendererService.GetSectionPartialData(sections);

            return View(partialData);
        }
    }
}
