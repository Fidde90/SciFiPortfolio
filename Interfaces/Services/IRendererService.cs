using SciFiPortfolio.Models;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Interfaces.Services
{
    public interface IRendererService
    {
        List<SectionPartialModel> GetSectionPartialData(List<ContentSection> sections);
    }
}
