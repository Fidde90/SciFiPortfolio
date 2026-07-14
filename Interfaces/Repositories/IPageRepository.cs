using SciFiPortfolio.Entities;

namespace SciFiPortfolio.Interfaces.Repositories
{
    public interface IPageRepository
    {
        Task<PageEntity?> GetPageBySlugAsync(string slug);
    }
}
