using SciFiPortfolio.Models;

namespace SciFiPortfolio.Interfaces.Services
{
    public interface IPageService
    {
         Task<Page> GetPageAsync(string slug);
    }
}
