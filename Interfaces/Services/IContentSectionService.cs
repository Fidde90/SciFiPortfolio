using SciFiPortfolio.Models;

namespace SciFiPortfolio.Interfaces.Services
{
    public interface IContentSectionService
    {
        Task<List<ContentSection>> GetContentSections(string page);
    }
}
