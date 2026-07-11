using SciFiPortfolio.Models;

namespace SciFiPortfolio.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<List<Image>> GetTechImages(); 
    }
}
