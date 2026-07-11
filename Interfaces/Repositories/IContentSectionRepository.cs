using SciFiPortfolio.Models;

namespace SciFiPortfolio.Interfaces.Repositories
{
    public interface IContentSectionRepository
    {
        Task<List<ContentSection>> GetAllSections(string page);
    }
}
