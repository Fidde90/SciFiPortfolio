using SciFiPortfolio.Models;

namespace SciFiPortfolio.Interfaces.Services
{
    public interface IImageService
    {
        Task<List<Image>> GetTechIcons();
    }
}
