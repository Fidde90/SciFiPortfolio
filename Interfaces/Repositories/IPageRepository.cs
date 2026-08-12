using SciFiPortfolio.Entities;

namespace SciFiPortfolio.Interfaces.Repositories
{
    public interface IPageRepository
    {
        Task<PageEntity?> GetPageBySlugAsync(string slug);
        Task<List<ProjectCardEntity>?> GetProjcetCardsAsync();
        Task<List<ProjectCardEntity>?> GetProjcetCardsAsync(int cardCount);
        Task<List<ProjectCardEntity>?> GetProjcetCardsAsync(List<string> cardIds);
    }
}
