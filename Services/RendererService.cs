using Microsoft.AspNetCore.Mvc.ViewEngines;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Services
{
    public class RendererService : IRendererService
    {
        private readonly ICompositeViewEngine _engine;

        public RendererService(ICompositeViewEngine engine)
        {
            _engine = engine;
        }

        public string? GetPartial(ContentSection section)
        {
            string path = $"/Pages/Shared/SectionPartials/_{section.GetType().Name}Partial.cshtml";

            return _engine.GetView(null, path, false).Success 
                ? path 
                : null;
        }
    }
}
