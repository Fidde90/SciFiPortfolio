using SciFiPortfolio.Models;

namespace SciFiPortfolio.Interfaces.Services
{
    public interface IPageService
    {
        Task<Page?> GetPageAsync(string slug);

        Task<List<ProjectCard>> GetProjectCardsAsync();

        Task<List<ProjectCard>> GetProjectCardsAsync(int cardCount);

        Task<List<ProjectCard>> GetProjectCardsAsync(List<string> cardIds);
    }
}
