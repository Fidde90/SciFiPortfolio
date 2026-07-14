using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Interfaces.Services
{
    public interface IContentSectionService
    {
        Task<List<ContentSection>> GetContentSections(string page);
    }
}
