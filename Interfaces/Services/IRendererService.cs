using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Interfaces.Services
{
    public interface IRendererService
    {
        string? GetPartial(ContentSection section);
    }
}
