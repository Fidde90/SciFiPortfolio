using Microsoft.AspNetCore.Mvc.ViewEngines;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewComponents;
using static System.Collections.Specialized.BitVector32;

namespace SciFiPortfolio.Services
{
    public class RendererService : IRendererService
    {
        private readonly ICompositeViewEngine _engine;
        private readonly ILogger<RendererViewComponent> _logger;

        public RendererService(ICompositeViewEngine engine, ILogger<RendererViewComponent> logger)
        {
            _engine = engine;
            _logger = logger;
        }

        public List<SectionPartialModel> GetSectionPartialData(List<ContentSection> sections)
        {
            var model = new List<SectionPartialModel>();

            foreach (var section in sections)
            {
                string typeName = section.GetType().Name;
                string path = $"/Pages/Shared/SectionPartials/_{typeName}Partial.cshtml";
                var view = _engine.GetView(null, path, false);

                if (view.Success)
                {
                    model.Add(new SectionPartialModel
                    {
                        PartialName = view.ViewName,
                        Section = section
                    });
                }
                else
                {
                    _logger.LogError("EN PARTIAL KUNNDE EJ HITTAS: {0}, SÖKVÄGEN: {1}", typeName, path);
                }
            }

            return model;
        }
    }
}
